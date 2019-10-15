using menu.Data;
using menu.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace menu
{
    class SaveBase
    {
        public async void SaveJson()
        {
            EsaDbContext db = new EsaDbContext();
            await Task.Run(() =>
            {
                while (true)
                {
                    using (StreamWriter file = new StreamWriter(@"C:\Users\Public\Documents\menu\Categories.json"))
                    {
                        var json = JsonConvert.SerializeObject(db.CategoriesList);
                        file.Write(json);
                    }
                    using (StreamWriter file = new StreamWriter(@"C:\Users\Public\Documents\menu\DishIngredient.json"))
                    {
                        var json = JsonConvert.SerializeObject(db.DishIngredientList);
                        file.Write(json);
                    }
                    using (StreamWriter file = new StreamWriter(@"C:\Users\Public\Documents\menu\Dish.json"))
                    {
                        var json = JsonConvert.SerializeObject(db.DishList);
                        file.Write(json);
                    }
                    using (StreamWriter file = new StreamWriter(@"C:\Users\Public\Documents\menu\Ingredient.json"))
                    {
                        var json = JsonConvert.SerializeObject(db.IngredientList);
                        file.Write(json);
                    }
                    using (StreamWriter file = new StreamWriter(@"C:\Users\Public\Documents\menu\MenuCategorie.json"))
                    {
                        var json = JsonConvert.SerializeObject(db.MenuCategorieList);
                        file.Write(json);
                    }
                    using (StreamWriter file = new StreamWriter(@"C:\Users\Public\Documents\menu\MenuDish.json"))
                    {
                        var json = JsonConvert.SerializeObject(db.MenuDishList);
                        file.Write(json);
                    }
                    using (StreamWriter file = new StreamWriter(@"C:\Users\Public\Documents\menu\Menu.json"))
                    {
                        var json = JsonConvert.SerializeObject(db.MenuList);
                        file.Write(json);
                    }
                    using (StreamWriter file = new StreamWriter(@"C:\Users\Public\Documents\menu\Store.json"))
                    {
                        var json = JsonConvert.SerializeObject(db.StoreList);
                        file.Write(json);
                    }
                    Task.Delay(2000);
                }
            });
        }
    }
}