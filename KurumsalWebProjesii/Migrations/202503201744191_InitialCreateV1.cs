namespace KurumsalWebProjesii.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admin",
                c => new
                    {
                        Adminıd = c.Int(nullable: false, identity: true),
                        Eposta = c.String(nullable: false, maxLength: 50),
                        Sifre = c.String(nullable: false, maxLength: 50),
                        Yetki = c.String(),
                    })
                .PrimaryKey(t => t.Adminıd);
            
            CreateTable(
                "dbo.Blog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Baslık = c.String(),
                        Icerik = c.String(),
                        ResimURL = c.String(),
                        Kategoriıd = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategori", t => t.Kategoriıd, cascadeDelete: true)
                .Index(t => t.Kategoriıd);
            
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        Kategoriıd = c.Int(nullable: false, identity: true),
                        KategoriAd = c.String(nullable: false, maxLength: 50),
                        Acıklama = c.String(),
                    })
                .PrimaryKey(t => t.Kategoriıd);
            
            CreateTable(
                "dbo.Yorum",
                c => new
                    {
                        YorumId = c.Int(nullable: false, identity: true),
                        AdSoyad = c.String(nullable: false, maxLength: 50),
                        Eposta = c.String(),
                        Içerik = c.String(),
                        Onay = c.Boolean(nullable: false),
                        Blogıd = c.Int(),
                        Blog_Id = c.Int(),
                    })
                .PrimaryKey(t => t.YorumId)
                .ForeignKey("dbo.Blog", t => t.Blog_Id)
                .Index(t => t.Blog_Id);
            
            CreateTable(
                "dbo.Hakkimizda",
                c => new
                    {
                        Hakkimizdaıd = c.Int(nullable: false, identity: true),
                        Aciklama = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Hakkimizdaıd);
            
            CreateTable(
                "dbo.Hizmet",
                c => new
                    {
                        Hizmetıd = c.Int(nullable: false, identity: true),
                        Baslık = c.String(nullable: false, maxLength: 150),
                        Acıklama = c.String(),
                        ResimURL = c.String(),
                    })
                .PrimaryKey(t => t.Hizmetıd);
            
            CreateTable(
                "dbo.İletişim",
                c => new
                    {
                        Iletisimıd = c.Int(nullable: false, identity: true),
                        Adres = c.String(maxLength: 250),
                        Telefon = c.String(),
                        Fax = c.String(),
                        Whatsapp = c.String(),
                        Facebook = c.String(),
                        Twitter = c.String(),
                        Instagram = c.String(),
                    })
                .PrimaryKey(t => t.Iletisimıd);
            
            CreateTable(
                "dbo.Kimlik",
                c => new
                    {
                        Kimlikıd = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        KeyWords = c.String(nullable: false, maxLength: 200),
                        Description = c.String(nullable: false, maxLength: 300),
                        LogoURL = c.String(),
                        Unvan = c.String(),
                    })
                .PrimaryKey(t => t.Kimlikıd);
            
            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        SliderId = c.Int(nullable: false, identity: true),
                        Baslık = c.String(maxLength: 30),
                        Aciklama = c.String(maxLength: 150),
                        ResimURL = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.SliderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorum", "Blog_Id", "dbo.Blog");
            DropForeignKey("dbo.Blog", "Kategoriıd", "dbo.Kategori");
            DropIndex("dbo.Yorum", new[] { "Blog_Id" });
            DropIndex("dbo.Blog", new[] { "Kategoriıd" });
            DropTable("dbo.Slider");
            DropTable("dbo.Kimlik");
            DropTable("dbo.İletişim");
            DropTable("dbo.Hizmet");
            DropTable("dbo.Hakkimizda");
            DropTable("dbo.Yorum");
            DropTable("dbo.Kategori");
            DropTable("dbo.Blog");
            DropTable("dbo.Admin");
        }
    }
}
