using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;

namespace KutuphaneMvc.Repositories
{
    public class KitapRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public KitapRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Kitap> GetAll() => _dbContext.Kitap.ToList();

        public Kitap? GetById(string isbn) => _dbContext.Kitap.Find(isbn);

        public bool Insert(Kitap kitap)
        {
            try
            {
                var kitapBul = GetById(kitap.Isbn);
                if (kitapBul != null) return false;
                _dbContext.Kitap.Add(kitap);
                return _dbContext.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        public bool Insert(Kitap kitap, List<Guid>? Turler, List<Guid>? Yazarlar, Guid? YayinEvi)
        {
            try
            {
                var kitapBul = GetById(kitap.Isbn);
                if (kitapBul == null)
                {
                    foreach (var tur in Turler)
                    {
                        var turBul = _dbContext.Tur.Find(tur);
                        kitap.Turler.Add(turBul);
                    }
                    foreach (var yazar in Yazarlar)
                    {
                        var yazarBul = _dbContext.Yazar.Find(yazar);
                        kitap.Yazarlar.Add(yazarBul);
                    }
                    kitap.YayinEviId = YayinEvi;
                }
                else
                {
                    return false;
                }
                _dbContext.Kitap.Add(kitap);
                return _dbContext.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        //public bool Insert(Kitap kitap, Guid[] turGuids, Guid[] yazarGuids, Guid yayinEviGuid)
        //{
        //    try
        //    {
        //        foreach (var tur in turGuids)
        //        {
        //            var turBul = _dbContext.Tur.Find(tur);
        //            if (turBul == null) return false;
        //            kitap.Turler.Add(turBul);
        //        }
        //        foreach (var yazar in yazarGuids)
        //        {
        //            var yazarBul = _dbContext.Yazar.Find(yazar);
        //            if (yazarBul == null) return false;
        //            kitap.Yazarlar.Add(yazarBul);
        //        }
        //        var yayinEviBul = _dbContext.YayinEvi.Find(yayinEviGuid);
        //        if (yayinEviBul == null) return false;
        //        kitap.YayinEvi = yayinEviBul;
        //        return Insert(kitap);
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        public bool Update(Kitap kitap, List<Guid>? Turler, List<Guid>? Yazarlar, Guid? YayinEvi)
        {
            try
            {
                var kitapBul = _dbContext.Kitap.Find(kitap.Isbn);
                _dbContext.Remove(kitapBul);
                _dbContext.SaveChanges();
                foreach (var tur in Turler)
                {
                    var turBul = _dbContext.Tur.Find(tur);
                    kitap.Turler.Add(turBul);
                }
                foreach (var yazar in Yazarlar)
                {
                    var yazarBul = _dbContext.Yazar.Find(yazar);
                    kitap.Yazarlar.Add(yazarBul);
                }
                kitap.YayinEviId = YayinEvi;

                _dbContext.Kitap.Add(kitap);
                return _dbContext.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(string isbn)
        {
            try
            {
                var kitap = GetById(isbn);
                if (kitap == null) return false;
                _dbContext.Kitap.Remove(kitap);
                return _dbContext.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

    }
}
