using SilliconASPWebApp.Models.Forms;

namespace SilliconASPWebApp.ViewModels.Views
{
    public class AccountDetailsViewModel
    {
        public string Title { get; set; } = "Account details";

        public AccountBasicInfoFormModel BasicInfo { get; set; } = new AccountBasicInfoFormModel();

        public AccountAddressFormModel AddressInfo { get; set; } = new AccountAddressFormModel();
    }
}
