using Infrastructure.Entities;
using SilliconASPWebApp.Models.Forms;
using SilliconASPWebApp.Models.Sections;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class AccountDetailsViewModel
    {
        public string Title = "Account details";

        public ProfileMenuModel ProfileMenuModel { get; set; } = new();

        public AccountBasicInfoFormModel BasicInfo { get; set; } = new();

        public AccountAddressFormModel AddressInfo { get; set; } = new();

        public string ErrorMessage { get; set; } = string.Empty;


        public void GetUserDetailsData(AppUserEntity user)
        {
            if(user != null)
            {
                ProfileMenuModel.Image = "/images/John-doe.svg";
                ProfileMenuModel.Name = $"{user.FirstName} {user.LastName}";
                ProfileMenuModel.Email = user.Email!;

                BasicInfo.FirstName = user.FirstName!;
                BasicInfo.LastName = user.LastName!;
                BasicInfo.Email = user.Email!;
                BasicInfo.Phone = user.PhoneNumber!;
                BasicInfo.Bio = user?.Bio;
            }
        }

        public void GetUserAddressData(AddressEntity address)
        {
            if(address != null)
            {
                AddressInfo.Addressline_1 = address.AddressLine_1;
                AddressInfo.Addressline_2 = address.AddressLine_2;
                AddressInfo.City = address.City;
                AddressInfo.PostalCode = address.PostalCode;
            }
        }
    }
}
