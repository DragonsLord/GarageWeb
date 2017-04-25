using GarageWeb.Models.ViewModel.AdminPanel;

namespace GarageWeb.Models.ViewModel
{
    public class AdminPanelViewModel
    {
        public string Message { get; set; }
        private ChangeLoginViewModel _changeLoginBlock = new ChangeLoginViewModel();
        private MainPageSettingsViewModel _mainPageBlock = new MainPageSettingsViewModel();
        private OrderingSettingsViewModel _orderingBlock = new OrderingSettingsViewModel();
        public ChangeLoginViewModel ChangeLoginBlock {
            get { return _changeLoginBlock; }
            set { _changeLoginBlock = value; }
        }

        public MainPageSettingsViewModel MainPageBlock {
            get { return _mainPageBlock; }
            set { _mainPageBlock = value;}
        }

        public OrderingSettingsViewModel OrderingBlock {
            get { return _orderingBlock; }
            set { _orderingBlock = value;}
        }
    }
}