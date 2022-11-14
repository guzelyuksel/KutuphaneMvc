using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;
using Microsoft.Identity.Client;

namespace KutuphaneMvc.Repositories
{
    public class YayinEviRepository
    {
        private readonly ApplicationDbContext _context;

        public YayinEviRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<YayinEvi> GetAll() => _context.YayinEvi.ToList();

        public YayinEvi? GetById(Guid id) => _context.YayinEvi.Find(id);

        public bool Insert(YayinEvi yayinEvi)
        {
            try
            {
                if (_context.YayinEvi.Any(x => x.Ad == yayinEvi.Ad))
                    return false;
                _context.YayinEvi.Add(yayinEvi);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(YayinEvi yayinEvi)
        {
            try
            {
                if (!_context.YayinEvi.Any(x => x.Id == yayinEvi.Id))
                    return false;
                _context.YayinEvi.Update(yayinEvi);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                var yayinEvi = _context.YayinEvi.Find(id);
                if (yayinEvi == null) return;
                _context.YayinEvi.Remove(yayinEvi);
                _context.SaveChanges();
            }
            catch
            {

            }
        }
    }
}
