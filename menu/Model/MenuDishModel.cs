using menu.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu.Model
{
    class MenuDishModel
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int DishId { get; set; }
        public int CategorieId { get; set; }
        public string Menu { get; set; }
        public string Dish { get; set; }
        public string Categorie { get; set; }
        public MenuDish MenuDish { get; set; }

        public MenuDishModel(MenuDish menuDish)
        {
            Id = menuDish.Id;
            MenuId = menuDish.MenuId;
            DishId = menuDish.DishId;
            CategorieId = menuDish.CategorieId;
            MenuDish = menuDish;
            Menu = new EsaDbContext().MenuList.Where(i => i.Id == MenuId).Single().Name;
            Dish = new EsaDbContext().DishList.Where(i => i.Id == DishId).Single().Name;
            Categorie = new EsaDbContext().CategoriesList.Where(i => i.Id == CategorieId).Single().Categorie;

        }
    }
}
