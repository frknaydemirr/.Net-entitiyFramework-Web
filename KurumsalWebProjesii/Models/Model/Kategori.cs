using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesii.Models.Model
{

	[Table("Kategori")]
	public class Kategori
	{

        [Key]
        public int Kategoriıd { get; set; }


        [Required,StringLength(50,ErrorMessage ="en fazla 50 karekter olmalıdır!")]
        public string  KategoriAd { get; set; }

        public string Acıklama { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}

