using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using menu;
using menu.Model;
using RGR.Data;

namespace menu.ViewModel
{
    class TableChangeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Categories> CategiriesCollection { get; set; } = new ObservableCollection<Categories>();
        public ObservableCollection<DishIngredient> DishIngredientCollection { get; set; } = new ObservableCollection<DishIngredient>();
        public ObservableCollection<Ingredient> IngredientCollection { get; set; } = new ObservableCollection<Ingredient>();
        public ObservableCollection<Dish> DishCollection { get; set; } = new ObservableCollection<Dish>();
        public ObservableCollection<MenuModel> MenuModelCollection { get; set; } = new ObservableCollection<MenuModel>();
        public ObservableCollection<MenuCatigorie> MenuCatigorieCollection { get; set; } = new ObservableCollection<MenuCatigorie>();
        public ObservableCollection<MenuDish> MenuDishCollection { get; set; } = new ObservableCollection<MenuDish>();
        public ObservableCollection<Store> StoreCollection { get; set; } = new ObservableCollection<Store>();

        public TableChangeViewModel(string table_name)
        {
            EsaDbContext db = new EsaDbContext();
            //switch (table_name)
           // {
           //     case "Categorie":
                    foreach (var e in db.CategoriesList)
                    {
                        CategiriesCollection.Add(e);
                    }
             //       break;
             //   default:
              //      break;
           // }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
