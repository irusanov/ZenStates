using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZenStates.ViewModels;

namespace ZenStates.State.Navigators
{
    public enum ViewType
    {
        HOME,
        CPU,
        GPU,
        POWER,
        TWEAKS,
        OPTIONS,
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        ICommand UpdateCurrentViewModelCommand { get; }
    }
}
