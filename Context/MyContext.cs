using ASPNETMVC.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASPNETMVC.Context
{
    public class MyContext : DbContext
    {
        public MyContext() : base("ASPNETMVC") { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}