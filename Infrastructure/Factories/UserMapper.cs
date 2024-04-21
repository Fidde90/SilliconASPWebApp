using Infrastructure.Entities;
using SilliconASPWebApp.Models.Forms;
using SilliconASPWebApp.Models.Sections;

namespace Infrastructure.Factories
{
    public class UserMapper
    {
        public static AppUserEntity ToUser(SignUpFormModel model)
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

        public static AppUserEntity MapNewUserValues(AppUserEntity user, AccountBasicInfoFormModel model)
        {
            if(user != null && model != null)
            {
                user!.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;
                user.Bio = model.Bio;
                user.Email = model.Email;
                user.UserName = model.Email;

                return user;
            }
            return null!;
        }

        public static AddressEntity NewAddressMapping(AccountAddressFormModel model)
        {
            if (model != null)
            {
                var newAddress = new AddressEntity
                {
                    AddressLine_1 = model.Addressline_1,
                    AddressLine_2 = model.Addressline_2,
                    City = model.City,
                    PostalCode = model.PostalCode
                };

                return newAddress;
            }
            return null!;
        }
    }
}
