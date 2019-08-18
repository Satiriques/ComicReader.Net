using Autofac;
using ComicReader.Net.UICommon.Interfaces;
using ComicReader.Net.UICommon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicReader.Net.UICommon
{
    public class UICommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WindowService>().As<IWindowService>().SingleInstance();
        }
    }
}