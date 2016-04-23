using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiDemo.Models
{
    //DbContext gives the class all the setup that is needed to do the operation 
    //you want to perform with DB Schema, or we can say it allows us to communicate with a DB.
    public class DAL:DbContext
    {

        // DB Set maintains the DB connection with database 
        public DbSet<Customer> customers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //mapping with the Customer Table 
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }


    }
}
