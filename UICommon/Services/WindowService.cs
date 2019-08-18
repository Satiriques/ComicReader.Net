using Autofac;
using ComicReader.Net.ApplicationMenu.Views;
using ComicReader.Net.UICommon.Interfaces;
using System.Windows;

namespace ComicReader.Net.UICommon.Services
{
    public class WindowService : IWindowService
    {
        private readonly IContainer _container;

        public WindowService(IContainer container)
        {
            _container = container;
        }

        public void ShowPreferences()
        {
            var view = _container.Resolve<PreferencesView>() as Window;
            view.ShowDialog();
        }
    }
}