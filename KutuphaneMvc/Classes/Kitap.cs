using System.ComponentModel.DataAnnotations;

namespace KutuphaneMvc.Classes
{
    public class Kitap
    {
        [Display(Name = "ISBN")]
        [MaxLength(11)]
        public string Isbn { get; set; }
        [Display(Name = "Kitap Adı")]
        [MaxLength(40)]
        public string Ad { get; set; }
        [DisplayFormat(DataFormatString = "{0:D}")]
        [Display(Name = "Yayın Tarihi")]
        [DataType(DataType.Date)]
        public DateTime BasimYili { get; set; }
        [Display(Name = "Basım Sayısı")]
        [Range(1, byte.MaxValue)]
        public int BasimSayisi { get; set; }
        [Display(Name = "Sayfa Sayısı")]
        [Range(1, byte.MaxValue)]
        public int SayfaSayisi { get; set; }

        public virtual List<Yazar>? Yazarlar { get; set; }
        public virtual List<Tur>? Turler { get; set; }
        public Guid? YayinEviId { get; set; }
        public virtual YayinEvi? YayinEvi { get; set; }
    }
}
