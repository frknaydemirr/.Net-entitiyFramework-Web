using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesii.Models.Model
{
    [Table("Admin")]
	public class Admin
	{
        //primary key olan adminıd yi belirttik!
        [Key]
        public int Adminıd { get; set; }

        [Required,StringLength(50,ErrorMessage ="50 karakter olmalıdır!")]
        public String Eposta { get; set; }

        [Required, StringLength(50, ErrorMessage = "50 karakter olmalıdır!")]
        public String Sifre { get; set; }



        public String Yetki { get; set; }
    }
}