using Autofac;
using ComicReader.Net.ApplicationMenu.Views;
using ComicReader.Net.Common.Interfaces;
using System;
using System.Windows.Forms;

namespace ComicReader.Net.Shell.Services
{
    public class WindowService : IWindowService
    {
        private readonly ILifetimeScope _scope;

        public WindowService(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public string ShowFolderDialog()
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                return dialog.SelectedPath;
            }
            return null;
        }

        public void ShowPreferences()
        {
            _scope.Resolve<PreferencesView>().ShowDialog();
        }
    }
}