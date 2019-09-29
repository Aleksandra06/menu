using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu.Model
{
    [Table("Categorie")]
    public class Categories
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string Categorie { get; set; }

        public override string ToString()
        {
            return Categorie;
        }

    }
}
