using SilliconASPWebApp.Models.Forms;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class AccountDetailsViewModel
    {
        public string Title { get; set; } = "Account details";

        public AccountBasicInfoFormModel BasicInfo { get; set; } = new AccountBasicInfoFormModel()
        {
            ProfileImg = "/images/John-doe.svg",
            FirstName = "Fidde",
            LastName="Bengtsson",
            Email="hejsan@hotmail.com",
            Phone = "0709567654",
        };

        public AccountAddressFormModel AddressInfo { get; set; } = new AccountAddressFormModel();
    }
}
