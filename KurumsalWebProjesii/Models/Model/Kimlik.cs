using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesii.Models.Model
{
	[Table("Kimlik")]
	public class Kimlik
	{

		[Key]
        public int Kimlikıd { get; set; }

        [Required,StringLength(100,ErrorMessage ="100 karekter içermeli!")]
        [DisplayName("Site Başlık")]
        public String Title { get; set; }
        [DisplayName("Anahtar Kelimeler")]
        [Required, StringLength(200, ErrorMessage = "200 karekter içermeli!")]
        public String KeyWords { get; set; }
        [DisplayName("Site Açıklama")]
        [Required, StringLength(300, ErrorMessage = "300 karekter içermeli!")]
        public String Description { get; set; }
        [DisplayName("Site Logo")]
        public String LogoURL { get; set; }
        [DisplayName("Site Unvan")]
        public String Unvan { get; set; }

    }
}