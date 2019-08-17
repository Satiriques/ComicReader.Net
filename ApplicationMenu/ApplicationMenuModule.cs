using Autofac;
using Autofac.Core;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.ApplicationMenu.ViewModels;
using ComicReader.Net.ApplicationMenu.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicReader.Net.ApplicationMenu
{
    public class ApplicationMenuModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileMenuViewModel>().As<IFileMenuViewModel>();
            builder.RegisterType<FileMenuView>();
        }
    }
}
