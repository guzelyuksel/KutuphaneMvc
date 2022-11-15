using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace KutuphaneMvc.Classes
{
    public class Yazar
    {
        public Guid Id { get; set; } = new Guid();
        [MaxLength(20)]
        public string Ad { get; set; }
        [MaxLength(20)]
        public string Soyad { get; set; }
        public string AdSoyad => $"{Ad} {Soyad}";
        [DisplayFormat(DataFormatString = "{0:D}")]
        [Display(Name = "Doğum Tarihi")]
        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        [MaxLength(100)]
        public string Adres { get; set; }
        [MaxLength(11)]
        public string Telefon { get; set; }
        [MaxLength(40)]
        public string Mail { get; set; }

        public virtual List<Kitap>? Kitaplar { get; set; }




    }
}
