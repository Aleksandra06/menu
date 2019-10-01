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
    class MenuDishViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<MenuDishModel> MenuDishModelCollection { get; set; } = new ObservableCollection<MenuDishModel>();

        public MenuDishViewModel()
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.MenuDishList)
            {
                MenuDishModelCollection.Add(new MenuDishModel(e));
            }
        }

        public static void Update()
        {
            MenuDishModelCollection.Clear();
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.MenuDishList)
            {
                MenuDishModelCollection.Add(new MenuDishModel(e));
            }
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

        public string SelectMenu { get; set; }
        public string SelectDish { get; set; }
        public string SelectCategorie { get; set; }

        private ICommand add;
        public ICommand Add
        {
            get
            {
                return add ?? (add = new RelayCommand(() =>
                {
                    if (SelectMenu != "" && SelectCategorie != "" && SelectDish != "")
                    {
                        MenuDish newMD = new MenuDish();
                        EsaDbContext db = new EsaDbContext();
                        newMD.Id = MenuDishModelCollection.Last().Id;
                        newMD.MenuId = db.MenuList.Where(i => i.Name == SelectMenu).Single().Id;
                        newMD.DishId = db.DishList.Where(i => i.Name == SelectDish).Single().Id;
                        newMD.CategorieId = db.CategoriesList.Where(i => i.Categorie == SelectCategorie).Single().Id;
                        db.MenuDishAdd(newMD);
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
                    MenuDish parametrtmp = new MenuDish();
                    MenuDishModel tmp = (MenuDishModel)parametr;
                    parametrtmp.CategorieId = tmp.CategorieId;
                    parametrtmp.MenuId = tmp.MenuId;
                    parametrtmp.DishId = tmp.DishId;
                    parametrtmp.Id = tmp.Id;
                    Delete del = new Delete();
                    del.MenuDishDelete(parametrtmp);
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
                    var MD = (MenuDishModel)parametr;
                    MenuDishChangeWindowViewModel modelChange = new MenuDishChangeWindowViewModel(MD);
                    MenuDishChangeWindow viewChange = new MenuDishChangeWindow();
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
