using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KurumsalWebProjesii.Models.Model
{
	[Table("Hizmet")]
	public class Hizmet
	{

        [Key]
        public int Hizmetıd { get; set; }
        [Required,StringLength(150,ErrorMessage ="150 karekter içermeli!")]
        [DisplayName("Hizmet Başlık")]
        public string Baslık { get; set; }
        [DisplayName("Hizmet Açıklama")]
        public string Acıklama { get; set; }
        [DisplayName("Hizmet Resim")]
        public string ResimURL { get; set; }

    }
}