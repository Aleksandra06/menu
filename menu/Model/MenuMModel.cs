using menu.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu.Model
{
    class MenuMModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Datum { get; set; }
        public int MenuCategoryId { get; set; }
        public string MenuCategory { get; set; }
        public string DatumStr { get; set; }

        public MenuMModel(MenuModel Menu)
        {
            Id = Menu.Id;
            Name = Menu.Name;
            Datum = Menu.Datum;
            MenuCategoryId = Menu.MenuCategoryId;
            MenuCategory = new EsaDbContext().MenuCategorieList.Where(i => i.Id == MenuCategoryId).Single().Name;
            DatumStr = Datum.ToString("dd.MM.yyyy");
        }
        
    }
}
