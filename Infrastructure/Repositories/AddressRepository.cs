using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class AddressRepository : BaseRepository<AddressEntity>
    {
        private readonly DataContext _context;
        public AddressRepository(DataContext dataContext) : base(dataContext) 
        {
            _context = dataContext;
        }
    }
}
