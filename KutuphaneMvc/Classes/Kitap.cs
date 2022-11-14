namespace KutuphaneMvc.Classes
{
    public class Kitap
    {
        public string Isbn { get; set; }
        public string Ad { get; set; }
        public DateTime BasimYili { get; set; }
        public int BasimSayisi { get; set; }
        public int SayfaSayisi { get; set; }

        public virtual List<Yazar> Yazarlar { get; set; }
        public virtual List<Tur> Turler { get; set; }
        public Guid YayinEviId { get; set; }
        public virtual YayinEvi YayinEvi { get; set; }
    }
}
