using KutuphaneMvc.Classes;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace KutuphaneMvc.Models
{
    public class KitapEkleViewModel
    {
        public Kitap Kitap { get; set; }
        public List<Guid>? Yazarlar { get; set; }
        public List<Guid>? Turler { get; set; }
        public Guid? YayinEvi { get; set; }

        [ValidateNever]
        public List<Yazar> YazarlarDb { get; set; }
        [ValidateNever]
        public List<Tur> TurlerDb { get; set; }
        [ValidateNever]
        public List<YayinEvi> YayinEvleriDb { get;set; }
    }
}
