using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class SavedCoursesRepository : BaseRepository<SavedCoursesEntity> 
    {
        private readonly DataContext _dataContext;
        public SavedCoursesRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }
    }
}
