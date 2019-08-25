using Autofac;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Shell.Database;
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
            builder.RegisterType<ZipService>().As<IZipService>();
            builder.RegisterType<ThumbnailCacheService>().As<IThumbnailCacheService>();
            builder.RegisterType<ImageService>().As<IImageService>();
            builder.RegisterType<FileService>().As<IFileService>();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<WindowService>().As<IWindowService>().SingleInstance();
        }
    }
}