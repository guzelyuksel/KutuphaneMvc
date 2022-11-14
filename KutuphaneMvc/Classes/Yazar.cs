using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace KutuphaneMvc.Classes
{
    public class Yazar
    {
        public Guid Id { get; set; } = new Guid();
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string AdSoyad => $"{Ad} {Soyad}";
        public DateTime DogumTarihi { get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }

        public virtual List<Kitap> Kitaplar { get; set; }




    }
}
