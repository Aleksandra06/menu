using GalaSoft.MvvmLight.Command;
using menu.Model;
using menu.View;
using RGR.Data;
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
    class CategoriesViewModel : INotifyPropertyChanged
    {
        private string id;
        private string categorie;

        public string Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        public string Categorie { get { return categorie; } set { categorie = value; OnPropertyChanged("Categorie"); } }

        public ObservableCollection<Categories> CategiriesCollection { get; set; } = new ObservableCollection<Categories>();
        public CategoriesViewModel()
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.CategoriesList)
            {
                CategiriesCollection.Add(e);
            }
        }

        public void Update()
        {
            CategiriesCollection.Clear();
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.CategoriesList)
            {
                CategiriesCollection.Add(e);
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
                        newCat.Id = CategiriesCollection.Last().Id;
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
        
        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                return delete ?? (delete = new RelayCommand<object>((object parametr) =>
                {
                    var parametrtmp = (Categories)parametr;
                    EsaDbContext db = new EsaDbContext();
                    db.CategoriesDelete(parametrtmp);
                    OnPropertyChanged("CategiriesCollection");
                    Update();
                }
                ));

            }
        }

        //private string redactColumn = "True";
        //public string RedactColumn
        //{
        //    get
        //    {
        //        return redactColumn;
        //    }
        //    set
        //    {
        //        redactColumn = value;
        //        Update();
        //        OnPropertyChanged("RedactColumn");
        //    }
        //}

        CategoriesChangeWindow viewChange;
        private ICommand redactClick;
        public ICommand RedactClick
        {
            get
            {
                return redactClick ?? (redactClick = new RelayCommand<object>((object parametr) =>
                {
                    var cat = (Categories)parametr;
                    CategoriesChangeWindowViewModel modelChange = new CategoriesChangeWindowViewModel(cat);
                    viewChange = new CategoriesChangeWindow();
                    viewChange.DataContext = modelChange;
                    viewChange.Show();
                }));
            }
        }
        
        public void Redact(Categories ChangeCat)
        {
            Categories Item = CategiriesCollection.Where(i => i.Id == ChangeCat.Id).Single();
            EsaDbContext db = new EsaDbContext();
            db.CategoriesDelete(Item);
            db.CategoriesAdd(ChangeCat);
            OnPropertyChanged("CategiriesCollection");
            Update();
            //viewChange.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
