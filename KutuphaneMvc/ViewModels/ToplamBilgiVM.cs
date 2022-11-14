namespace KutuphaneMvc.ViewModels
{
    public class ToplamBilgiVM
    {
        public int ToplamKitap { get; private set; }
        public int ToplamYazar { get; private set; }
        public int ToplamYayinEvi { get; private set; }
        public int ToplamTur { get; private set; }

        public ToplamBilgiVM(int toplamKitap, int toplamYazar, int toplamYayinEvi, int toplamTur)
        {
            ToplamKitap = toplamKitap;
            ToplamYazar = toplamYazar;
            ToplamYayinEvi = toplamYayinEvi;
            ToplamTur = toplamTur;
        }
    }
}
