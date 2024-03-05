using Microsoft.AspNetCore.Mvc.ModelBinding;
using SilliconASPWebApp.Models.Forms;
using SilliconASPWebApp.Models.Sections;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class AccountDetailsViewModel
    {
        public string Title = "Account details";

        public ProfileMenuModel ProfileMenuModel { get; set; } = new()
        {         
            Name = "John Doe",
            Email = "john.doe@hotmail.com"
        };

        public AccountBasicInfoFormModel BasicInfo { get; set; } = new AccountBasicInfoFormModel()
        {
            FirstName = "Fidde",
            LastName = "Bengtsson",
            Email = "hejsan@hotmail.com",
            Phone = "0709567654",
        };

        public AccountAddressFormModel AddressInfo { get; set; } = new AccountAddressFormModel();

        public string ErrorMessage { get; set; } = string.Empty;
    }
}
