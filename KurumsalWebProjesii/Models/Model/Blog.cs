using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesii.Models.Model
{


	[Table("Blog")]
	public class Blog
	{
        [Key]
        public int Blogıd { get; set; }

        public string Baslık { get; set; }

        public string Icerik { get; set; }

        public string ResimURL { get; set; }

        public int Kategoriıd { get; set; }

        public Kategori Kategori { get; set; }

    }
}