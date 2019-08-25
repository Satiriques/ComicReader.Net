﻿using ComicReader.Net.Shell.Startup;
using System.Windows;
using Autofac;
using System.IO;
using System.Reflection;
using System;
using ComicReader.Net.Shell.Services;

namespace ComicReader.Net.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();

            var mainWindow = container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}