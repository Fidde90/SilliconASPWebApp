using Infrastructure.Entities;
using SilliconASPWebApp.Models.Sections;

namespace Infrastructure.Factories
{
    public class userFactory
    {
        public static AppUserEntity UserMapper(SignUpFormModel model)
        {
            var newUser = new AppUserEntity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            return newUser;
        }
    }
}
