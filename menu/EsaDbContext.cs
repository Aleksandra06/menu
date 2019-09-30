using menu.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace menu.Data
{
    public class EsaDbContext : DbContext
    {
        public EsaDbContext() : base("name=EsaDbConnectionString")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EsaDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }


        public virtual DbSet<Categories> CategoriesList { get; set; }
        public virtual DbSet<DishIngredient> DishIngredientList { get; set; }
        public virtual DbSet<Ingredient> IngredientList { get; set; }
        public virtual DbSet<Dish> DishList { get; set; }
        public virtual DbSet<MenuModel> MenuList { get; set; }
        public virtual DbSet<MenuCategorie> MenuCategorieList { get; set; }
        public virtual DbSet<MenuDish> MenuDishList { get; set; }
        public virtual DbSet<Store> StoreList { get; set; }


       

        public void IngredientAdd(Ingredient item)
        {
            try
            {
                this.IngredientList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void IngredientDelete(Ingredient ingredient)
        {
            try
            {
                IngredientList.Attach(ingredient);
                IngredientList.Remove(ingredient);
                
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        public void IngredientChange(Ingredient ingredient)
        {
            try
            {
                var ser = IngredientList.Where(x => x.Id == ingredient.Id).First();
                
                ser.Name = ingredient.Name;
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void CategoriesAdd(Categories item)
        {
            try
            {
                this.CategoriesList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void CategoriesDelete(Categories item)
        {
            try
            {

                CategoriesList.Attach(item);
                CategoriesList.Remove(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void CategoriesChange(Categories item)
        {
            try
            {
                var ser = CategoriesList.Where(x => x.Id == item.Id).First();
                
                ser.Categorie = item.Categorie;
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void MenuCategoriesAdd(MenuCategorie item)
        {
            try
            {
                this.MenuCategorieList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void MenuCategoriesDelete(MenuCategorie item)
        {
            try
            {

                MenuCategorieList.Attach(item);
                MenuCategorieList.Remove(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                MessageBox.Show("Невозможно удалить элемент!");
              
            }
        }
        public void MenuCategoriesChange(MenuCategorie item)
        {
            try
            {
                var ser = MenuCategorieList.Where(x => x.Id == item.Id).First();
                
                ser.Name = item.Name;
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void DishAdd(Dish item)
        {
            try
            {
                this.DishList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void DishDelete(Dish item)
        {
            try
            {
                DishList.Attach(item);
                DishList.Remove(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                MessageBox.Show("Невозможно изменить элемент!");

            }
        }

        public void DishChange(Dish item, string categorie)
        {

            try
            {
                var ser = DishList.Where(x => x.Id == item.Id).First();

                ser.CategorieId = CategoriesList.Where(x => x.Categorie == categorie).First().Id;
                ser.Name = item.Name;
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void MenuAdd(MenuModel item)
        {
            try
            {
                this.MenuList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Error");
                //throw;
            }
        }
        public void MenuDelete(MenuModel item)
        {
            try
            {

                MenuList.Attach(item);
                MenuList.Remove(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                MessageBox.Show("Невозможно удалить элемент!");

            }
        }
        public void MenuChange(MenuModel item)
        {
            try
            {
                var ser = MenuList.Where(x => x.Id == item.Id).First();
                ser.MenuCategoryId = item.MenuCategoryId;
                ser.Name = item.Name;
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void MenuDishDelete(MenuDish item)
        {
            try
            {

                MenuDishList.Attach(item);
                MenuDishList.Remove(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
                MessageBox.Show("Невозможно удалить элемент!");

            }
        }
        public void MenuDishAdd(MenuDish item)
        {
            try
            {
                this.MenuDishList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Error");
                //throw;
            }
        }
        public void MenuDishChange(MenuDish item)
        {
            try
            {
                var ser = MenuDishList.Where(x => x.Id == item.Id).First();

                ser.CategorieId = item.CategorieId;
                ser.MenuId = item.MenuId;
                ser.DishId = item.DishId;
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void DishIngredientAdd(DishIngredient item)
        {
            try
            {
                this.DishIngredientList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void DishIngredientDelete(DishIngredient item)
        {
            try
            {

                DishIngredientList.Attach(item);
                DishIngredientList.Remove(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void DishIngredientChange(DishIngredient item)
        {
            try
            {
                var ser = DishIngredientList.Where(x => x.Id == item.Id).First();

                ser.Quantity = item.Quantity;
                ser.IngredientsId = item.IngredientsId;
                ser.DishId = item.DishId;
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void StoreAdd(Store item)
        {
            try
            {
                this.StoreList.Add(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void StoreDelete(Store item)
        {
            try
            {

                StoreList.Attach(item);
                StoreList.Remove(item);
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void StoreChange(Store item)
        {
            try
            {
                var ser = StoreList.Where(x => x.Id == item.Id).First();

                ser.Quantity = item.Quantity;
                ser.IngredientsId = item.IngredientsId;
                this.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}