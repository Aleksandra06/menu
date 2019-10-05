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
    class MenuDishChangeWindowViewModel : INotifyPropertyChanged
    {
        public MenuDishModel MeaningMD { get; set; }
        public string SelectDish { get; set; }
        public string SelectMenu { get; set; }
        public string SelectCategorie { get; set; }

        public MenuDishChangeWindowViewModel(MenuDishModel dish)
        {
            MeaningMD = dish;
            SelectDish = dish.Dish;
            SelectMenu = dish.Menu;
            SelectCategorie = dish.Categorie;
        }

        public ObservableCollection<string> MenuSpis
        {
            get
            {
                EsaDbContext db = new EsaDbContext();
                ObservableCollection<string> tmpMenuSpis = new ObservableCollection<string>();
                foreach (var e in db.MenuList)
                {
                    tmpMenuSpis.Add(e.Name.ToString());
                }
                return tmpMenuSpis;
            }
        }

        public ObservableCollection<string> DishSpis
        {
            get
            {
                EsaDbContext db = new EsaDbContext();
                ObservableCollection<string> tmpDishSpis = new ObservableCollection<string>();
                foreach (var e in db.DishList)
                {
                    tmpDishSpis.Add(e.Name.ToString());
                }
                return tmpDishSpis;
            }
        }

        public ObservableCollection<string> CategorieSpis
        {
            get
            {
                EsaDbContext db = new EsaDbContext();
                ObservableCollection<string> tmpCatSpis = new ObservableCollection<string>();
                foreach (var e in db.CategoriesList)
                {
                    tmpCatSpis.Add(e.Categorie.ToString());
                }
                return tmpCatSpis;
            }
        }

        private ICommand newMeaning;
        public ICommand NewMeaning
        {
            get
            {
                return newMeaning ?? (newMeaning = new RelayCommand(() =>
                {
                    MenuDish changeMD = new MenuDish();
                    EsaDbContext db = new EsaDbContext();
                    changeMD.CategorieId = db.CategoriesList.Where(i => i.Categorie == SelectCategorie).Single().Id;
                    changeMD.MenuId = db.MenuList.Where(i => i.Name == SelectMenu).Single().Id;
                    changeMD.DishId = db.DishList.Where(i => i.Name == SelectDish).Single().Id;
                    changeMD.Id = MeaningMD.Id;
                    db.MenuDishChange(changeMD);
                    MenuDishViewModel.Update();
                    Close();
                }));
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
