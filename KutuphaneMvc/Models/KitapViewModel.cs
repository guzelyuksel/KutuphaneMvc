using KutuphaneMvc.Classes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KutuphaneMvc.Models
{
    public class KitapViewModel
    {
        public Kitap Kitap { get; set; }
        public List<SelectListItem> Yazarlar { get; set; }
        public List<SelectListItem> Turler { get; set; }
        public List<SelectListItem> YayinEvleri { get; set; }
    }
}
