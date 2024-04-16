using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Factories;
using Microsoft.EntityFrameworkCore;

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
