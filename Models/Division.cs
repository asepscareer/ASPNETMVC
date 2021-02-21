using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASPNETMVC.Models
{
    [Table("Tb_Division")]
    public class Division
    {
        [Key]
        public int Id { get; set; }
        public string Division_Name { get; set; }
    }
}