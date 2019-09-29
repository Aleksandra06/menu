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
using RGR.Data;

namespace menu.ViewModel
{
    class DishViewModel : INotifyPropertyChanged
    {
        private string name;
        private string categorieId;
        private string id;
        private string categorie;

        public string Name { get { return name; } set { name = value; OnPropertyChanged("Name"); } }
        public string CategorieId { get { return categorieId; } set { categorieId = value; OnPropertyChanged("categorieId"); } }
        public string Id { get { return id; } set { id = value; OnPropertyChanged("Id"); } }
        public string Categorie { get { return categorie; } set { categorie = value; OnPropertyChanged("Categorie"); } }

        public ObservableCollection<Dish> DishCollection { get; set; } = new ObservableCollection<Dish>();
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
                DishCollection.Add(e);
            }
        }

        public string ReturnCategorie()///?
        {
            IEnumerable<Categories> listItems = CategoriesCollection.Where(i => i.Id.ToString() == categorieId);
            string cat = listItems.ElementAt(0).Categorie.ToString();
            return cat;
        }

        public void Update()
        {
            CategoriesCollection.Clear();
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.DishList)
            {
                DishCollection.Add(e);
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
        private string NewDish
        {
            get
            {
                return newDish;
            }
            set
            {
                newDish = value;
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
                        newdish.Id = DishCollection.Last().Id;
                        newdish.Name = newDish;
                        newdish.CatigorieId = CategoriesCollection.Where(i => i.ToString() == SelectCategories).Single().Id;
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
                    var parametrtmp = (Dish)parametr;
                    EsaDbContext db = new EsaDbContext();
                    db.DishDelete(parametrtmp);
                    Update();
                }
                ));

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
