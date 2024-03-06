using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<AppUserEntity>
    { 
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext): base(dataContext) 
        {
           _dataContext = dataContext;
        }
    }
}
