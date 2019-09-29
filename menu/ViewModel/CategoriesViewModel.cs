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

        public ObservableCollection<Categories> CategoriesCollection { get; set; } = new ObservableCollection<Categories>();
        public CategoriesViewModel()
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.CategoriesList)
            {
                CategoriesCollection.Add(e);
            }
        }

        public void Update()
        {
            CategoriesCollection.Clear();
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.CategoriesList)
            {
                CategoriesCollection.Add(e);
            }
            OnPropertyChanged("CategoriesCollection");
            OnPropertyChanged("Categorie");
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
        
        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                return delete ?? (delete = new RelayCommand<object>((object parametr) =>
                {
                    var parametrtmp = (Categories)parametr;
                    EsaDbContext db = new EsaDbContext();
                    foreach(var e in db.DishList)
                    {
                        if (e.CatigorieId == parametrtmp.Id) db.DishDelete(e);
                    }
                    db.CategoriesDelete(parametrtmp);
                    OnPropertyChanged("CategoriesCollection");
                    Update();
                }
                ));

            }
        }

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
            Categories Item = CategoriesCollection.Where(i => i.Id == ChangeCat.Id).Single();
            EsaDbContext db = new EsaDbContext();
            db.CategoriesChange(ChangeCat);
            Update();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
