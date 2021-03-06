﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace menu.Model
{
    [Table("Dish")]
    public class Dish
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        public string Name { get; set; }

        public int CatigorieId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
