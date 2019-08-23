using Autofac;
using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Shell.Database;
using ComicReader.Net.Shell.Interfaces;
using ComicReader.Net.Shell.Services;
using ComicReader.Net.Shell.ViewModels;
using System;

namespace ComicReader.Net.Shell
{
    internal class ShellModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ComicReaderDbContext>().AsSelf();
            builder.RegisterType<DataService>().As<IDataService>();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<WindowService>().As<IWindowService>().SingleInstance();
        }
    }
}