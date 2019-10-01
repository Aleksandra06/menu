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
    class MenuCategorieViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<MenuCategorie> MenuCategorieCollection { get; set; } = new ObservableCollection<MenuCategorie>();

        public MenuCategorieViewModel()
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.MenuCategorieList)
            {
                MenuCategorieCollection.Add(e);
            }
        }


        public static void Update()
        {
            MenuCategorieCollection.Clear();
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.MenuCategorieList)
            {
                MenuCategorieCollection.Add(e);
            }
        }
        
        public string NewMenuCategorie { get; set; }

        private ICommand add;
        public ICommand Add
        {
            get
            {
                return add ?? (add = new RelayCommand(() =>
                {
                    if (NewMenuCategorie != "")
                    {
                        MenuCategorie newCat = new MenuCategorie();
                        EsaDbContext db = new EsaDbContext();
                        newCat.Id = MenuCategorieCollection.Last().Id;
                        newCat.Name = NewMenuCategorie;
                        db.MenuCategoriesAdd(newCat);
                        NewMenuCategorie = "";
                        OnPropertyChanged("NewMenuCategorie");
                        Update();
                    }
                    else
                        MessageBox.Show("Введите категорию меню");
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
                    var parametrtmp = (MenuCategorie)parametr;
                    Delete del = new Delete();
                    del.MenuCategorieDelete(parametrtmp);
                    OnPropertyChanged("MenuCategorieCollection");
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
                    var cat = (MenuCategorie)parametr;
                    MenuCategorieChangeWindowViewModel modelChange = new MenuCategorieChangeWindowViewModel(cat);
                    MenuCategorieChangeWindow viewChange = new MenuCategorieChangeWindow();
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
