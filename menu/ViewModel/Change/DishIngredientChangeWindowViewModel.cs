using GalaSoft.MvvmLight.Command;
using menu.Data;
using menu.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace menu.ViewModel.Change
{
    class DishIngredientChangeWindowViewModel : INotifyPropertyChanged
    {
        public DishIngredientModel MeaningDI { get; set; }
        public string SelectDish { get; set; }
        public string SelectIngredients { get; set; }
        public string Quantity { get; set; }

        public DishIngredientChangeWindowViewModel(DishIngredientModel DI)
        {
            MeaningDI = DI;
            SelectDish = DI.Dish;
            SelectIngredients = DI.Ingredients;
            Quantity = DI.Quantity.ToString();
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

        private ICommand newMeaning;
        public ICommand NewMeaning
        {
            get
            {
                return newMeaning ?? (newMeaning = new RelayCommand(() =>
                {
                    DishIngredient changeDI = new DishIngredient();
                    EsaDbContext db = new EsaDbContext();
                    changeDI.IngredientsId = db.IngredientList.Where(i => i.Name == SelectIngredients).Single().Id;
                    changeDI.DishId = db.DishList.Where(i => i.Name == SelectDish).Single().Id;
                    changeDI.Quantity = Convert.ToInt32(Quantity);
                    changeDI.Id = MeaningDI.Id;
                    db.DishIngredientChange(changeDI);
                    DishIngredientViewModel.Update();
                    Close();
                }));
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

        public event EventHandler Closed;
        private void Close()
        {
            if (Closed != null) Closed(this, EventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
