namespace KutuphaneMvc.Classes
{
    public class YayinEvi
    {
        public Guid Id { get; set; } = new Guid();
        public string Ad { get; set; }
        public DateTime KurulusYili { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }

        public virtual List<Yazar> Yazarlar { get; set; }
        public virtual List<Kitap> Kitaplar { get; set; }
    }
}
