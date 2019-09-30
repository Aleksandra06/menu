using menu.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu.Model
{
    class DishIngredientModel
    {
        public int Id { get; set; }
        public int IngredientsId { get; set; }
        public int DishId { get; set; }
        public int Quantity { get; set; }
        public string Ingredients { get; set; }
        public string Dish { get; set; }

        public DishIngredientModel(DishIngredient DI)
        {
            Id = DI.Id;
            IngredientsId = DI.IngredientsId;
            DishId = DI.DishId;
            Quantity = DI.Quantity;
            Ingredients = new EsaDbContext().IngredientList.Where(i => i.Id == IngredientsId).Single().Name;
            Dish = new EsaDbContext().DishList.Where(i => i.Id == DishId).Single().Name;
        }
    }
}
