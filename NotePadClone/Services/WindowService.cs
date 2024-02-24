using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.Extensions.DependencyInjection;

namespace NotePadClone.Services;
internal class WindowService(IServiceProvider serviceProvider) : WpfWindowHandling.Services.WindowService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public virtual void OpenNewWindow<T>() where T : Window
    {
        var window = _serviceProvider.GetService<T>() ?? throw new InvalidOperationException($"Could not resolve {typeof(T)}.");
        window.Show();
    }
}
