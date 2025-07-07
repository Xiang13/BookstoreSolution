using BookstoreApi.Models.EFModels;

namespace BookstoreApi.Models.Infrastructures.Repositories.Base
{
    public abstract class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
