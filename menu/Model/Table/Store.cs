using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu.Model
{
    [Table("Store")]
    public class Store
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public int IngredientsId { get; set; }
        public int Quantity { get; set; }

    }
}
