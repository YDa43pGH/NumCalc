using NumCalc.App.ViewModels;

namespace NamCalc.App.Services.Interfaces;

public interface INavigationService
{
    void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;

    ViewModelBase CurrentViewModel { get;}
}
