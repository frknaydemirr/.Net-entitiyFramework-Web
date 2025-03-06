using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesii.Models.Model
{
	[Table("Hakkimizda")]
	public class Hakkimizda
	{

        [Key]
        public int Hakkimizdaıd { get; set; }

        [Required]
        [DisplayName(" Hakkımızda Açıklama")]
        public string Aciklama { get; set; }
    }
}