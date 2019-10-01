using GalaSoft.MvvmLight.Command;
using menu.Data;
using menu.Model;
using menu.View.Change;
using menu.ViewModel.Change;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace menu.ViewModel
{
    class DishIngredientViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<DishIngredientModel> DishIngredientModelCollection { get; set; } = new ObservableCollection<DishIngredientModel>();

        public DishIngredientViewModel()
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.DishIngredientList)
            {
                DishIngredientModelCollection.Add(new DishIngredientModel(e));
            }
        }

        public static void Update()
        {
            DishIngredientModelCollection.Clear();
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.DishIngredientList)
            {
                DishIngredientModelCollection.Add(new DishIngredientModel(e));
            }
        }

        public ObservableCollection<string> IngredientsSpis
        {
            get
            {
                EsaDbContext db = new EsaDbContext();
                ObservableCollection<string> tmpIngredientsSpis = new ObservableCollection<string>();
                foreach (var e in db.IngredientList)
                {
                    tmpIngredientsSpis.Add(e.Name.ToString());
                }
                return tmpIngredientsSpis;
            }
        }

        public ObservableCollection<string> DishSpis
        {
            get
            {
                EsaDbContext db = new EsaDbContext();
                ObservableCollection<string> tmpDishsSpis = new ObservableCollection<string>();
                foreach (var e in db.DishList)
                {
                    tmpDishsSpis.Add(e.Name.ToString());
                }
                return tmpDishsSpis;
            }
        }

        public string NewQuantity { get; set; }
        public string SelectIngredients { get; set; }
        public string SelectDish { get; set; }

        private ICommand add;
        public ICommand Add
        {
            get
            {
                return add ?? (add = new RelayCommand(() =>
                {
                    if (NewQuantity != "" && SelectIngredients != "" && SelectDish != "")
                    {
                        DishIngredient newDI = new DishIngredient();
                        EsaDbContext db = new EsaDbContext();
                        newDI.Id = DishIngredientModelCollection.Last().Id;
                        newDI.IngredientsId = db.IngredientList.Where(i => i.Name == SelectIngredients).Single().Id;
                        newDI.DishId = db.DishList.Where(i => i.Name == SelectDish).Single().Id;
                        newDI.Quantity = Convert.ToInt32(NewQuantity);
                        db.DishIngredientAdd(newDI);
                        Update();
                    }
                    else
                        MessageBox.Show("Завершите выбор");
                }
                ));
            }
        }

        private ICommand updateClick;
        public ICommand UpdateClick
        {
            get
            {
                return updateClick ?? (updateClick = new RelayCommand(() =>
                {
                    Update();
                }
                ));

            }
        }

        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                return delete ?? (delete = new RelayCommand<object>((object parametr) =>
                {
                    DishIngredient parametrtmp = new DishIngredient();
                    DishIngredientModel tmp = (DishIngredientModel)parametr;
                    parametrtmp.DishId = tmp.DishId;
                    parametrtmp.IngredientsId = tmp.IngredientsId;
                    parametrtmp.Quantity = tmp.Quantity;
                    parametrtmp.Id = tmp.Id;
                    Delete del = new Delete();
                    del.DishIngredientDelete(parametrtmp);
                    Update();
                }
                ));

            }
        }

        private ICommand redactClick;
        public ICommand RedactClick
        {
            get
            {
                return redactClick ?? (redactClick = new RelayCommand<object>((object parametr) =>
                {
                    var DI = (DishIngredientModel)parametr;
                    DishIngredientChangeWindowViewModel modelChange = new DishIngredientChangeWindowViewModel(DI);
                    DishIngredientChangeWindow viewChange = new DishIngredientChangeWindow();
                    viewChange.DataContext = modelChange;
                    viewChange.Show();
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
