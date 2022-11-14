using KutuphaneMvc.Classes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace KutuphaneMvc.DataAccess
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Yazar> Yazar { get; set; }
        public DbSet<Kitap> Kitap { get; set; }
        public DbSet<Tur> Tur { get; set; }
        public DbSet<YayinEvi> YayinEvi { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Yazar Özellikleri
            modelBuilder.Entity<Yazar>()
                .Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Yazar>()
                .Property(x => x.Ad).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Yazar>()
                .Property(x => x.Soyad).IsRequired().HasMaxLength(20);
            modelBuilder.Entity<Yazar>()
                .Property(x => x.Adres).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Yazar>()
                .Property(x => x.Cinsiyet).IsRequired();
            modelBuilder.Entity<Yazar>()
                .Property(x => x.DogumTarihi).IsRequired();
            modelBuilder.Entity<Yazar>()
                .Property(x => x.Mail).IsRequired().HasMaxLength(40);
            modelBuilder.Entity<Yazar>()
                .Property(x => x.Telefon).IsRequired().HasMaxLength(11);
            modelBuilder.Entity<Yazar>()
                .Ignore(x => x.AdSoyad);
            #endregion

            #region Kitap Özellikleri
            modelBuilder.Entity<Kitap>()
                .HasKey(x => x.Isbn);
            modelBuilder.Entity<Kitap>()
                .Property(x => x.Isbn).HasMaxLength(13);
            modelBuilder.Entity<Kitap>()
                .Property(x => x.Ad).IsRequired().HasMaxLength(40);
            modelBuilder.Entity<Kitap>()
                .Property(x => x.BasimSayisi).IsRequired();
            modelBuilder.Entity<Kitap>()
                .Property(x => x.BasimYili).IsRequired();
            modelBuilder.Entity<Kitap>()
                .Property(x => x.SayfaSayisi).IsRequired();
            #endregion

            #region Yayın Evi Özellikleri
            modelBuilder.Entity<YayinEvi>()
                .Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<YayinEvi>()
                .Property(x => x.KurulusYili).IsRequired();
            modelBuilder.Entity<YayinEvi>()
                .Property(x => x.Adres).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<YayinEvi>()
                .Property(x => x.Telefon).IsRequired().HasMaxLength(11);
            modelBuilder.Entity<YayinEvi>()
                .Property(x => x.Mail).IsRequired().HasMaxLength(40);
            #endregion

            #region Tür Özellikleri
            modelBuilder.Entity<Tur>()
                .Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Tur>()
                .Property(x => x.Ad).IsRequired().HasMaxLength(50);
            #endregion

            #region İlişkiler
            modelBuilder.Entity<Kitap>().HasMany(x => x.Yazarlar).WithMany(x => x.Kitaplar);
            modelBuilder.Entity<Kitap>().HasMany(x => x.Turler).WithMany(x => x.Kitaplar);
            modelBuilder.Entity<Kitap>().HasOne(x => x.YayinEvi).WithMany(x => x.Kitaplar).HasForeignKey(x => x.YayinEviId).OnDelete(DeleteBehavior.NoAction);
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
