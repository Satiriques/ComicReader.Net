﻿using Autofac;
using ComicReader.Net.Common.Interfaces;
using ComicReader.Net.Shell.Services;
using ComicReader.Net.Shell.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicReader.Net.Shell
{
    internal class ShellModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();

            builder.RegisterType<WindowService>().As<IWindowService>().SingleInstance();
        }
    }
}