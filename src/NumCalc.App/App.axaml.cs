using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using NumCalc.App.Services.Fakes;
using NumCalc.App.Services.Settings;
using NumCalc.Core;
using NumCalc.App.ViewModels;
using NumCalc.App.Views;

namespace NumCalc.App;

public partial class NumCalcApp : Application
{

    public new static NumCalcApp? Current => Application.Current as NumCalcApp;
    public IServiceProvider Services { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        Services = serviceCollection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = Services.GetRequiredService<MainWindow>();
        }
        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<ICalculatorService, MockCalculatorService>();

        services.AddTransient<MainViewModel>();
        services.AddTransient<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainViewModel>()
        });
    }


    private IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

    }

    private void ConfigureAppSettings(IServiceCollection services, IConfiguration configuration)
    {
        var calculatorSection = configuration.GetSection(nameof(CalculatorSettings));
        services.Configure<CalculatorSettings>(calculatorSection);
    }

}
