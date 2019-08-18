using Autofac;
using ComicReader.Net.ApplicationMenu.Views;
using ComicReader.Net.Common.Interfaces;
using System;

namespace ComicReader.Net.Shell.Services
{
    public class WindowService : IWindowService
    {
        private readonly ILifetimeScope _scope;

        public WindowService(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public void ShowPreferences()
        {
            _scope.Resolve<PreferencesView>().ShowDialog();
        }
    }
}