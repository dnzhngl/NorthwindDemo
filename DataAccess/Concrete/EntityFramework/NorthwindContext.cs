using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // Context : Db tabloları ile entities classlarımızı ilişkilendiriyoruz.
    public class NorthwindContext : DbContext
    {
        // Projenin hangi veritabanı ile ilişkili olduğunu OnConfguring içerisinde belirtir.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;"); // Bağlanacağımız veritabanı için connection string girilir.
            //Gerçekte : Server = 175.45.2.12 (ip adresi girilir.), Database Adı, Güçlü bir domain yönetimi var ise Trusted connection (integrated security ) True /  olarak kullanılır. Ancak güçlü bir domain yönetimi yoksa kullanıcı adı ve şifre girilir.
            // Proje çalıştığında EF ilk buraya bakıyor nereye bağlanacağını öğreniyor.
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
