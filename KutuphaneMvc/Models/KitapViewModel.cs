using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;

namespace KutuphaneMvc.Models
{
    public class KitapViewModel
    {
        public Kitap Kitap { get; set; }
        public List<Yazar> Yazarlar { get; set; }
        public List<Tur> Turler { get; set; }
        public List<YayinEvi> YayinEvleri { get; set; }
    }
}
