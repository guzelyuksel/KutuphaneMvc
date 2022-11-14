using System.ComponentModel.DataAnnotations;

namespace KutuphaneMvc.Classes
{
    public class YayinEvi
    {
        public Guid Id { get; set; } = new Guid();
        [Display(Name = "Yayın Evi Adı")]
        [MaxLength(50)]
        public string Ad { get; set; }
        [DisplayFormat(DataFormatString = "{0:D}")]
        [Display(Name = "Kuruluş Tarihi")]
        [DataType(DataType.Date)]
        public DateTime KurulusYili { get; set; }
        [MaxLength(100)]
        public string Adres { get; set; }
        [MaxLength(11)]
        public string Telefon { get; set; }
        [MaxLength(40)]
        public string Mail { get; set; }

        public virtual List<Yazar>? Yazarlar { get; set; }
        public virtual List<Kitap>? Kitaplar { get; set; }
    }
}
