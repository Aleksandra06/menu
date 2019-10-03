using GalaSoft.MvvmLight.Command;
using menu.Model;
using menu.View;
using menu.ViewModel.Change;
using menu.Data;
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
using menu.View.Change;

namespace menu.ViewModel
{
    class CategoriesViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<Categories> CategoriesCollection { get; set; } = new ObservableCollection<Categories>();
        public CategoriesViewModel()
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.CategoriesList)
            {
                CategoriesCollection.Add(e);
            }
        }

        
        public static void Update()
        {
            CategoriesCollection.Clear();
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.CategoriesList)
            {
                CategoriesCollection.Add(e);
            }
        }

        private string newCategorie = "";
        public string NewCategorie
        {
            get
            {
                return newCategorie;
            }
            set
            {
                newCategorie = value;
                OnPropertyChanged("NewCategorie");
            }
        }

        public bool Activity
        {
            get
            {

                return false;
            }
        }

        private ICommand add;
        public ICommand Add
        {
            get
            {
                return add ?? (add = new RelayCommand(() =>
                {
                    if (newCategorie != "")
                    {
                        Categories newCat = new Categories();
                        EsaDbContext db = new EsaDbContext();
                        newCat.Id = CategoriesCollection.Last().Id;
                        newCat.Categorie = newCategorie;
                        db.CategoriesAdd(newCat);
                        newCategorie = "";
                        OnPropertyChanged("newCategorie");
                        Update();
                    }
                    else
                        MessageBox.Show("Введите категорию");
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
                    var parametrtmp = (Categories)parametr;
                    Delete del = new Delete();
                    del.CategoriesDelete(parametrtmp);
                    OnPropertyChanged("CategoriesCollection");
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
                    var cat = (Categories)parametr;
                    CategoriesChangeWindowViewModel modelChange = new CategoriesChangeWindowViewModel(cat);
                    CategoriesChangeWindow viewChange = new CategoriesChangeWindow();
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
