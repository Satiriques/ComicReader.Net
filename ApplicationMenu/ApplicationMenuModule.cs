﻿using Autofac;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.ApplicationMenu.ViewModels;
using ComicReader.Net.ApplicationMenu.Views;

namespace ComicReader.Net.ApplicationMenu
{
    public class ApplicationMenuModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileMenuViewModel>().As<IFileMenuViewModel>();
            builder.RegisterType<FileMenuView>();

            builder.RegisterType<PreferencesViewModel>().AsSelf();
            builder.RegisterType<PreferencesView>();

            builder.RegisterType<LibrariesSettingsViewModel>().AsSelf();
            builder.RegisterType<LibrariesSettingsView>().AsSelf();

            builder.RegisterType<AdvancedSettingsViewModel>().AsSelf();
            builder.RegisterType<AdvancedSettingsView>().AsSelf();

            builder.RegisterType<GridSettingsViewModel>().AsSelf();
            builder.RegisterType<GridSettingsView>().AsSelf();
        }
    }
}