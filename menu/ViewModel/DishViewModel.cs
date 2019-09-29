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
using GalaSoft.MvvmLight.Command;
using menu.Model;
using menu.View.Change;
using menu.ViewModel.Change;
using menu.Data;

namespace menu.ViewModel
{
    class DishViewModel : INotifyPropertyChanged
    {
        private string name;
      //  private string categorieId;
       // private string id;
        private string categorie;

        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
      //  public string CategorieId { get { return categorieId; } set { categorieId = value; OnPropertyChanged("categorieId"); } }
       // public string Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        public string Categorie { get { return categorie; } set { categorie = value; OnPropertyChanged("Categorie"); } }

        public ObservableCollection<DishModel> DishModelCollection { get; set; } = new ObservableCollection<DishModel>();
        public ObservableCollection<Categories> CategoriesCollection { get; set; } = new ObservableCollection<Categories>();
        
        public DishViewModel()
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.CategoriesList)
            {
                CategoriesCollection.Add(e);
            }
            foreach (var e in db.DishList)
            {
                DishModelCollection.Add(new DishModel(e));
            }
        }

        public void Update()
        {
            CategoriesCollection.Clear();
            DishModelCollection.Clear();
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.CategoriesList)
            {
                CategoriesCollection.Add(e);
            }
            foreach (var e in db.DishList)
            {
                DishModelCollection.Add(new DishModel(e));
            }
        }

        public ObservableCollection<string> CategoriesSpis
        {
            get
            {
                ObservableCollection<string> tmpCatSpis = new ObservableCollection<string>();
                foreach(var e in CategoriesCollection)
                {
                    tmpCatSpis.Add(e.ToString());
                }
                return tmpCatSpis;
            }
        }

        private string newDish;
        public string NewDish
        {
            get
            {
                return newDish;
            }
            set
            {
                newDish = value;
                OnPropertyChanged("NewDish");
            }
        }

        public string SelectCategories { get; set; }

        private ICommand add;
        public ICommand Add
        {
            get
            {
                return add ?? (add = new RelayCommand(() =>
                {
                    if (NewDish != "" && SelectCategories != "")
                    {
                        Dish newdish = new Dish();
                        EsaDbContext db = new EsaDbContext();
                        newdish.Id = DishModelCollection.Last().Id;
                        newdish.Name = newDish;
                        newdish.CategorieId = CategoriesCollection.Where(i => i.ToString() == SelectCategories).Single().Id;
                        db.DishAdd(newdish);
                        NewDish = "";
                        OnPropertyChanged("newDish");
                        Update();
                    }
                    else
                        if(NewDish == "")
                            MessageBox.Show("Введите блюдо");
                    else MessageBox.Show("Выберите категорию");
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
                    Dish parametrtmp = new Dish();
                    DishModel tmp = (DishModel)parametr;
                    parametrtmp.CategorieId = tmp.CategorieId;
                    parametrtmp.Id = tmp.Id;
                    parametrtmp.Name = tmp.Name;
                    Delete del = new Delete();
                    del.DishDelete(parametrtmp);
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
                    var cat = (DishModel)parametr;
                    DishChangeWindowViewModel modelChange = new DishChangeWindowViewModel(cat);
                    DishChangeWindow viewChange = new DishChangeWindow();
                    viewChange.DataContext = modelChange;
                    viewChange.Show();
                }));
            }
        }

        public void Redact(DishModel ChangeDish)
        {
            //DishModel Item = DishModelCollection.Where(i => i.Id == ChangeDish.Id).Single();
            Dish changeDish = new Dish();
            changeDish.CategorieId = ChangeDish.CategorieId;
            changeDish.Id = ChangeDish.Id;
            changeDish.Name = ChangeDish.Name;
            EsaDbContext db = new EsaDbContext();
            db.DishChange(changeDish);
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
