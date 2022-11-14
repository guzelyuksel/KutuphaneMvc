using KutuphaneMvc.Classes;
using KutuphaneMvc.DataAccess;

namespace KutuphaneMvc.Repositories
{
    public class TurRepository
    {
        private readonly ApplicationDbContext _context;

        public TurRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Insert(Tur tur)
        {
            try
            {
                if (_context.Tur.Any(x => x.Ad == tur.Ad))
                    return false;
                _context.Tur.Add(tur);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Tur tur)
        {
            try
            {
                var turBul = _context.Tur.Find(tur.Id);
                if (turBul == null)
                    return false;
                turBul.Ad = tur.Ad;
                return _context.SaveChanges() > 0;
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
                var turBul = _context.Tur.Find(id);
                if (turBul == null) return false;
                _context.Tur.Remove(turBul);
                return _context.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
