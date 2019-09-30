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
    class DishChangeWindowViewModel : INotifyPropertyChanged
    {
        public DishModel MeaningDish { get; set; }
        public string Name { get; set; }

        public DishChangeWindowViewModel(DishModel dish)
        {
            MeaningDish = dish;
            Name = dish.Name;
            SelectCategories = dish.Categorie;
        }

        public ObservableCollection<string> CategoriesSpis
        {
            get
            {
                ObservableCollection<string> tmpCatSpis = new ObservableCollection<string>();
                EsaDbContext db = new EsaDbContext();
                foreach (var e in db.CategoriesList)
                {
                    tmpCatSpis.Add(e.ToString());
                }
                return tmpCatSpis;
            }
        }

        public string SelectCategories { get; set; }

        private ICommand newMeaning;
        public ICommand NewMeaning
        {
            get
            {
                return newMeaning ?? (newMeaning = new RelayCommand(() =>
                {
                    Dish changeDish = new Dish();
                    changeDish.Id = MeaningDish.Id;
                    changeDish.Name = MeaningDish.Name;
                    MeaningDish.CategorieId = new EsaDbContext().CategoriesList.Where(i => i.Categorie == MeaningDish.Categorie).First().Id;
                    EsaDbContext db = new EsaDbContext();
                    db.DishChange(changeDish);
                    DishViewModel.Update();
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
