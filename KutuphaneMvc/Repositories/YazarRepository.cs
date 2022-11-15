using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;
using KutuphaneMvc.Models;

namespace KutuphaneMvc.Repositories
{
    public class YazarRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public YazarRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Yazar> GetAll() => _dbContext.Yazar.ToList();

        public Yazar? GetById(Guid id) => _dbContext.Yazar.Find(id);

        public bool Insert(Yazar yazar)
        {
            try
            {
                if (_dbContext.Yazar.Any(x => x.Ad.ToLower() == yazar.Ad.ToLower() && x.Soyad.ToLower() == yazar.Soyad.ToLower())) return false;
                _dbContext.Yazar.Add(yazar);
                return _dbContext.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Yazar yazar)
        {
            try
            {
                _dbContext.Yazar.Update(yazar);
                return _dbContext.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                var yazarBul = GetById(id);
                if (yazarBul == null) return false;
                _dbContext.Yazar.Remove(yazarBul);
                return _dbContext.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
