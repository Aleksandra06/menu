using menu.Data;
using menu.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu
{
    class Delete
    {
        public void CategoriesDelete(Categories delCat)
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.DishList)
            {
                if (e.CategorieId == delCat.Id) DishDelete(e);
            }
            foreach (var e in db.MenuDishList)
            {
                if (e.CategorieId == delCat.Id) MenuDishDelete(e);
            }
            db.CategoriesDelete(delCat);
        }

        public void DishDelete (Dish delDish)
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.DishIngredientList)
            {
                if (e.DishId == delDish.Id) DishIngredientDelete(e);
            }
            foreach (var e in db.MenuDishList)
            {
                if (e.CategorieId == delDish.Id) MenuDishDelete(e);
            }
            db.DishDelete(delDish);
        }

        public void MenuDishDelete (MenuDish delMD)//?
        {
            EsaDbContext db = new EsaDbContext();
            db.MenuDishDelete(delMD);
        }

        public void DishIngredientDelete (DishIngredient delDI)
        {
            EsaDbContext db = new EsaDbContext();
            db.DishIngredientDelete(delDI);
        }

        public void IngredientDelete(Ingredient delI)
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.DishIngredientList)
            {
                if (e.IngredientsId == delI.Id) DishIngredientDelete(e);
            }
            foreach (var e in db.StoreList)
            {
                if (e.IngredientsId == delI.Id) StoreDelete(e);
            }
            db.IngredientDelete(delI);
        }

        public void StoreDelete(Store delStore)
        {
            EsaDbContext db = new EsaDbContext();
            db.StoreDelete(delStore);
        }

        public void MenuDelete(MenuModel delMenu)
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.MenuDishList)
            {
                if (e.MenuId == delMenu.Id) MenuDishDelete(e);
            }
            db.MenuDelete(delMenu);
        }

        public void MenuCategorieDelete(MenuCategorie delMC)
        {
            EsaDbContext db = new EsaDbContext();
            foreach (var e in db.MenuList)
            {
                if (e.MenuCategoryId == delMC.Id) MenuDelete(e);
            }
            db.MenuCategoriesDelete(delMC);
        }
    }
}
