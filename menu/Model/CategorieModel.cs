using menu.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu.Model
{
    class CategorieModel
    {
        public bool Activity { get; set; }
        public string Id { get; set; }
        public string Categorie { get; set; }

        public CategorieModel(Categories cat)
        {
            Id = cat.Id.ToString();
            Categorie = cat.Categorie;
            EsaDbContext db = new EsaDbContext();
            var rn = db.DishList.Where(i => i.CatigorieId == cat.Id);
            if (!db.DishList.Any(i => i.CatigorieId == cat.Id) &&
                !db.MenuDishList.Any(i => i.CategorieId == cat.Id))
                Activity = true;
            else Activity = false;
        }
    }
}
