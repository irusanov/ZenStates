using System.Windows;
using ZenStates.State.Navigators;
using ZenStates.ViewModels;

namespace ZenStates.Commands
{
    public class UpdateCurrentViewModelCommand : CommandBase
    {
        private readonly AppSettings appSettings = (Application.Current as App)?.appSettings;

        private readonly INavigator _navigator;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;
        }

        public override void Execute(object parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;
                switch (viewType)
                {
                    case ViewType.HOME:
                        _navigator.CurrentViewModel = new HomeViewModel();
                        break;
                    case ViewType.CPU:
                        _navigator.CurrentViewModel = new CpuViewModel();
                        break;
                    case ViewType.GPU:
                        _navigator.CurrentViewModel = new GpuViewModel();
                        break;
                    case ViewType.POWER:
                        _navigator.CurrentViewModel = new PowerViewModel();
                        break;
                    case ViewType.TWEAKS:
                        _navigator.CurrentViewModel = new TweaksViewModel();
                        break;
                    case ViewType.OPTIONS:
                        _navigator.CurrentViewModel = new OptionsViewModel(appSettings);
                        break;

                    default:
                        _navigator.CurrentViewModel = new HomeViewModel();
                        break;
                }
            }
        }
    }
}
