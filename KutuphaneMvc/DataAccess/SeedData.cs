using Bogus;
using KutuphaneMvc.Classes;
using Microsoft.EntityFrameworkCore;
using static Bogus.DataSets.Name;

namespace KutuphaneMvc.DataAccess
{
    public static class SeedData
    {
        public static void Seed(IApplicationBuilder builder)
        {
            var scope = builder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            if (context != null)
            {
                List<Yazar> yazarlar = new();
                List<Tur> turler = new();
                List<YayinEvi> yayinEvleri = new();
                List<Kitap> kitaplar = new();
                context.Database.Migrate();
                if (!context.Yazar.Any())
                {
                    var testYazar = new Faker<Yazar>("tr")
                        .RuleFor(o => o.Cinsiyet, f => f.PickRandom<Cinsiyet>())
                        .RuleFor(o => o.Ad, (f, o) => f.Name.FirstName((Gender)o.Cinsiyet))
                        .RuleFor(o => o.Soyad, (f, o) => f.Name.LastName((Gender)o.Cinsiyet))
                        .RuleFor(o => o.DogumTarihi, f => f.Date.PastOffset(60, DateTime.Now.AddYears(-18)).Date)
                        .RuleFor(o => o.Adres, f => f.Address.FullAddress())
                        .RuleFor(o => o.Telefon, f => f.Phone.PhoneNumber("###########"))
                        .RuleFor(o => o.Mail, f => f.Internet.Email());
                    yazarlar = testYazar.Generate(10);
                    context.Yazar.AddRange(yazarlar);
                    context.SaveChanges();
                }
                if (!context.Tur.Any())
                {
                    turler = new List<Tur>()
                    {
                       new Tur(){ Id = new Guid(), Ad = "İletişim-Medya" },
                       new Tur(){ Id = new Guid(), Ad = "İnsan ve Toplum" },
                       new Tur(){ Id = new Guid(), Ad = "Kadın" },
                       new Tur(){ Id = new Guid(), Ad = "Kadın-Erkek" },
                       new Tur(){ Id = new Guid(), Ad = "Kişisel Gelişim" },
                       new Tur(){ Id = new Guid(), Ad = "Korku-Gerilim" },
                       new Tur(){ Id = new Guid(), Ad = "Kültür" },
                       new Tur(){ Id = new Guid(), Ad = "Macera-Aksiyon" },
                       new Tur(){ Id = new Guid(), Ad = "Manga" },
                       new Tur(){ Id = new Guid(), Ad = "Masal" },
                       new Tur(){ Id = new Guid(), Ad = "Mitolojiler" },
                       new Tur(){ Id = new Guid(), Ad = "Moda" },
                       new Tur(){ Id = new Guid(), Ad = "Müzik" },
                       new Tur(){ Id = new Guid(), Ad = "Özlü Sözler-Duvar Yazıları" },
                       new Tur(){ Id = new Guid(), Ad = "Parapsikoloji-Spiritüalizm" },
                       new Tur(){ Id = new Guid(), Ad = "Psikoloji" },
                       new Tur(){ Id = new Guid(), Ad = "Roman" },
                       new Tur(){ Id = new Guid(), Ad = "Sağlık-Tıp" },
                       new Tur(){ Id = new Guid(), Ad = "Sana" },
                       new Tur(){ Id = new Guid(), Ad = "Senaryo-Oyun" },
                       new Tur(){ Id = new Guid(), Ad = "Şiir" },
                       new Tur(){ Id = new Guid(), Ad = "Sinema" },
                       new Tur(){ Id = new Guid(), Ad = "Siyaset-Politika" },
                       new Tur(){ Id = new Guid(), Ad = "Sosyoloji"},
                       new Tur(){ Id = new Guid(), Ad = "Söyleşi-Röportaj" },
                       new Tur(){ Id = new Guid(), Ad = "Sözlük-Kılavuz Kitap-Ansiklopedi" },
                       new Tur(){ Id = new Guid(), Ad = "Spor" },
                       new Tur(){ Id = new Guid(), Ad = "Tarih" },
                       new Tur(){ Id = new Guid(), Ad = "Tasavvuf-Mezhepler-Tarikatlar" },
                       new Tur(){ Id = new Guid(), Ad = "Tiyatro" },
                       new Tur(){ Id = new Guid(), Ad = "Türk Klasikleri" },
                       new Tur(){ Id = new Guid(), Ad = "Yemek" },
                       new Tur(){ Id = new Guid(), Ad = "Yeraltı Edebiyatı" }
                    };
                    context.Tur.AddRange(turler);
                    context.SaveChanges();
                }
                if (!context.YayinEvi.Any())
                {
                    var testYayinEvi = new Faker<YayinEvi>("tr")
                        .RuleFor(o => o.Ad, f => f.Company.CompanyName())
                        .RuleFor(o => o.KurulusYili, f => f.Date.PastOffset(60, DateTime.Now.AddYears(-18)).Date)
                        .RuleFor(o => o.Adres, f => f.Address.FullAddress())
                        .RuleFor(o => o.Telefon, f => f.Phone.PhoneNumber("###########"))
                        .RuleFor(o => o.Mail, f => f.Internet.ExampleEmail());
                    yayinEvleri = testYayinEvi.Generate(5);
                    context.YayinEvi.AddRange(yayinEvleri);
                    context.SaveChanges();
                }
                if (!context.Kitap.Any())
                {
                    if (!yayinEvleri.Any()) yayinEvleri = context.YayinEvi.ToList();
                    if (!turler.Any()) turler = context.Tur.ToList();
                    if (!yazarlar.Any()) yazarlar = context.Yazar.ToList();
                    var testKitap = new Faker<Kitap>("tr")
                        .RuleFor(o => o.Isbn, f => f.Commerce.Ean13())
                        .RuleFor(o => o.Ad, f => f.Commerce.ProductName())
                        .RuleFor(o => o.BasimYili, f => f.Date.PastOffset(20, DateTime.Now.AddYears(-18)).Date)
                        .RuleFor(o => o.BasimSayisi, f => f.Random.Number(1, 10))
                        .RuleFor(o => o.SayfaSayisi, f => f.Random.Number(50, 300))
                        .RuleFor(o => o.YayinEviId, f => f.PickRandom(yayinEvleri).Id)
                        .RuleFor(o => o.YayinEvi, (f, o) => o.YayinEvi)
                        .RuleFor(o => o.Turler, f => f.PickRandom(turler, 2).ToList())
                        .RuleFor(o => o.Yazarlar, f => f.PickRandom(yazarlar, f.Random.Number(1, 3)).ToList());
                    kitaplar = testKitap.Generate(20);
                    context.Kitap.AddRange(kitaplar);
                    context.SaveChanges();
                }
            }
        }
    }
}
