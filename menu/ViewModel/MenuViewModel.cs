using GalaSoft.MvvmLight.Command;
using menu.Data;
using menu.Model;
using menu.View.Change;
using menu.ViewModel.Change;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace menu.ViewModel
{
    class MenuViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<MenuMModel> MenuMModelCollection { get; set; } = new ObservableCollection<MenuMModel>();

        public MenuViewModel()
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.MenuList)
            {
                MenuMModelCollection.Add(new MenuMModel(e));
            }
        }

        public static void Update()
        {
            MenuMModelCollection.Clear();
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.MenuList)
            {
                MenuMModelCollection.Add(new MenuMModel(e));
            }
        }

        public ObservableCollection<string> MenuCategorySpis
        {
            get
            {
                EsaDbContext db = new EsaDbContext();
                ObservableCollection<string> tmpMCSpis = new ObservableCollection<string>();
                foreach (var e in db.MenuCategorieList)
                {
                    tmpMCSpis.Add(e.Name.ToString());
                }
                return tmpMCSpis;
            }
        }

        public string NewName { get; set; }
        public string SelectMenuCategory { get; set; }

        private ICommand add;
        public ICommand Add
        {
            get
            {
                return add ?? (add = new RelayCommand(() =>
                {
                    if (NewName != "" && SelectMenuCategory != "")
                    {
                        MenuModel newMenu = new MenuModel();
                        EsaDbContext db = new EsaDbContext();
                        newMenu.Id = MenuMModelCollection.Last().Id;
                        newMenu.Name = NewName;
                        newMenu.Datum = DateTime.Now;
                        newMenu.MenuCategoryId = db.MenuCategorieList.Where(i => i.Name == SelectMenuCategory).Single().Id;
                        db.MenuAdd(newMenu);
                        Update();
                    }
                    else
                        MessageBox.Show("Проверьте правильность введенных данных");
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
                    MenuModel parametrtmp = new MenuModel();
                    MenuMModel tmp = (MenuMModel)parametr;
                    parametrtmp.Name = tmp.Name;
                    parametrtmp.MenuCategoryId = tmp.MenuCategoryId;
                    parametrtmp.Datum = tmp.Datum;
                    parametrtmp.Id = tmp.Id;
                    Delete del = new Delete();
                    del.MenuDelete(parametrtmp);
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
                    var menu = (MenuMModel)parametr;
                    MenuChangeWindowViewModel modelChange = new MenuChangeWindowViewModel(menu);
                    MenuChangeWindow viewChange = new MenuChangeWindow();
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
