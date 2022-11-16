using KutuphaneMvc.Classes;

namespace KutuphaneMvc.Models
{
    public class KitapEkleViewModel
    {
        public Kitap Kitap { get; set; }
        public List<Yazar>? Yazarlar { get; set; }
        public List<Tur>? Turler { get; set; }
        public List<YayinEvi>? YayinEvleri { get; set; }
    }
}
