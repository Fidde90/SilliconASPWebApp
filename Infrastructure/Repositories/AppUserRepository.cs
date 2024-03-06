using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUserEntity>
    { 

        private readonly DataContext _dataContext;

        public AppUserRepository(DataContext dataContext): base(dataContext) 
        {
           _dataContext = dataContext;
        }

        public override Task<AppUserEntity> AddToDB(AppUserEntity entity)
        {
            return base.AddToDB(entity);
        }
    }
}
