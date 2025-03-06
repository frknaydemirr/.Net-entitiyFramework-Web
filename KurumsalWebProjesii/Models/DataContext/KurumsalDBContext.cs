using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using KurumsalWebProjesii.Models.Model;

namespace KurumsalWebProjesii.Models.DataContext
{
    // Bu class bizim model classlarımızı veritabanında temsil eder ve eşler (map eder).
    public class KurumsalDBContext : DbContext  
    {
        public KurumsalDBContext() : base("KurumsalWebDB")
            //kurumsal db ile ilgili webconfig dosyamızda işlem yapmamız lazım!
        {


        }
        public DbSet<Admin> Admin { get; set; }

        public DbSet<Blog> Blog { get; set; }

        public DbSet<Hakkimizda> Hakkimizda { get; set; }

        public DbSet<Hizmet> Hizmet { get; set; }

        public DbSet<Iletisim> Iletisim { get; set; }

        public DbSet<Kategori> Kategori { get; set; }

        public DbSet<Kimlik> Kimlik { get; set; }


    }
}
