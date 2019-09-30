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
    class StoreChangeWindowViewModel : INotifyPropertyChanged
    {
        public StoreModel MeaningStore { get; set; }
        public string Quantity { get; set; }
        public string SelectIngredients { get; set; }

        public StoreChangeWindowViewModel(StoreModel store)
        {
            MeaningStore = store;
            Quantity = store.Quantity.ToString();
            SelectIngredients = store.Ingredients;
        }

        public ObservableCollection<string> IngredientsSpis
        {
            get
            {
                ObservableCollection<string> tmpInSpis = new ObservableCollection<string>();
                EsaDbContext db = new EsaDbContext();
                foreach (var e in db.IngredientList)
                {
                    tmpInSpis.Add(e.Name);
                }
                return tmpInSpis;
            }
        }


        private ICommand newMeaning;
        public ICommand NewMeaning
        {
            get
            {
                return newMeaning ?? (newMeaning = new RelayCommand(() =>
                {
                    Store store = new Store();
                    EsaDbContext db = new EsaDbContext();
                    store.Quantity = Convert.ToInt32(Quantity);
                    store.Id = MeaningStore.Id;
                    store.IngredientsId = new EsaDbContext().IngredientList.Where(i => i.Name == SelectIngredients).Single().Id;
                    db.StoreChange(store);
                    StoreViewModel.Update();
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
