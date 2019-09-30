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
    class StoreViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<StoreModel> StoreModelCollection { get; set; } = new ObservableCollection<StoreModel>();

        public StoreViewModel()
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.StoreList)
            {
                StoreModelCollection.Add(new StoreModel(e));
            }
        }

        public static void Update()
        {
            StoreModelCollection.Clear();
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.StoreList)
            {
                StoreModelCollection.Add(new StoreModel(e));
            }
        }

        public ObservableCollection<string> IngredientsSpis
        {
            get
            {
                ObservableCollection<string> tmpIngSpis = new ObservableCollection<string>();
                EsaDbContext db = new EsaDbContext();
                foreach (var e in db.IngredientList)
                {
                    tmpIngSpis.Add(e.Name);
                }
                return tmpIngSpis;
            }
        }
        
        public string NewQuantity { get; set; }
        public string SelectIngredients { get; set; }

        private ICommand add;
        public ICommand Add
        {
            get
            {
                return add ?? (add = new RelayCommand(() =>
                {
                    if (NewQuantity != "" && SelectIngredients != "")
                    {
                        Store newstore = new Store();
                        EsaDbContext db = new EsaDbContext();
                        newstore.Id = StoreModelCollection.Last().Id;
                        newstore.Quantity = Convert.ToInt32(NewQuantity);
                        newstore.IngredientsId = db.IngredientList.Where(i => i.Name == SelectIngredients).Single().Id;
                        db.StoreAdd(newstore);
                        NewQuantity = "";
                        OnPropertyChanged("NewQuantity");
                        Update();
                    }
                    else
                        MessageBox.Show("Проверте корректность введенных данных");
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
                    Store parametrtmp = new Store();
                    StoreModel tmp = (StoreModel)parametr;
                    parametrtmp.IngredientsId = tmp.IngredientsId;
                    parametrtmp.Id = tmp.Id;
                    parametrtmp.Quantity = tmp.Quantity;
                    Delete del = new Delete();
                    del.StoreDelete(parametrtmp);
                    OnPropertyChanged("StoreModelCollection");
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
                    var store = (StoreModel)parametr;
                    StoreChangeWindowViewModel modelChange = new StoreChangeWindowViewModel(store);
                    StoreChangeWindow viewChange = new StoreChangeWindow();
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
