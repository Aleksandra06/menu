using menu.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu.Model
{
    class DishModel
    {
        public Dish Dish { get; set; } = new Dish();
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategorieId { get; set; }
        public string Categorie { get; set; }

        public DishModel(Dish dish)
        {
            Id = dish.Id;
            Name = dish.Name;
            CategorieId = dish.CategorieId;
            Dish = dish;
            Categorie = new EsaDbContext().CategoriesList.Where(i => i.Id == CategorieId).Single().Categorie;
        }

        //public DishModel(DishModel dish)
        //{
        //    Dish.Id = Id = dish.Id;
        //    Dish.Name = Name = dish.Name;
        //    Categorie = dish.Categorie;
        //    dish.CategorieId = CategorieId = new EsaDbContext().CategoriesList.Where(i => i.Categorie == dish.Categorie).Single().Id;
        //}
    }
}
