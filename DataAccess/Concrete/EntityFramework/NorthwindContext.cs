using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // Context : Database tabloları ile entities classları ilişkilendirilir.
    public class NorthwindContext : DbContext
    {
        // Projenin hangi veritabanı ile ilişkili olduğu OnConfguring içerisinde belirtilir.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;"); 
            // Bağlanılacak olan veritabanı için connection string girilir.

            // Gerçekte : Server = 175.45.2.12 (ip adresi girilir.), Database Adı, Güçlü bir domain yönetimi var ise Trusted connection (integrated security ) True /  olarak kullanılır. Ancak güçlü bir domain yönetimi yoksa kullanıcı adı ve şifre girilir.
            // Proje çalıştığında EF ilk buraya bakar ve nereye bağlanacağını öğrenir.
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
