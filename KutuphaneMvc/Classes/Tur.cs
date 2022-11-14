using System.ComponentModel.DataAnnotations;

namespace KutuphaneMvc.Classes
{
    public class Tur
    {
        public Guid Id { get; set; } = new Guid();
        [Display(Name = "Tür Adı")]
        [MaxLength(50)]
        public string Ad { get; set; }

        public virtual List<Kitap>? Kitaplar { get; set; }
    }
}
