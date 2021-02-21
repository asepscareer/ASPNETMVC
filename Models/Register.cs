using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASPNETMVC.Models
{
    public class Register
    {
        public List<Account> Accounts { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Division> Divisions { get; set; }

        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "DateTime2")]
        public DateTime JoinDate { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public int DivisionID { get; set; }
        public string Division_Name { get; set; }
    }
}