using menu.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu.Model
{
    class StoreModel
    {
        public int Id { get; set; }
        public int IngredientsId { get; set; }
        public int Quantity { get; set; }
        public string Ingredients { get; set; }

        public StoreModel(Store store)
        {
            Id = store.Id;
            IngredientsId = store.IngredientsId;
            Quantity = store.Quantity;
            Ingredients = new EsaDbContext().IngredientList.Where(i => i.Id == IngredientsId).Single().Name;
        }


    }
}
