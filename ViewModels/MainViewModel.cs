using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenStates.State.Navigators;

namespace ZenStates.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            Navigator.UpdateCurrentViewModelCommand.Execute(ViewType.HOME);
        }

        public INavigator Navigator { get; set; } = new Navigator();
    }
}
