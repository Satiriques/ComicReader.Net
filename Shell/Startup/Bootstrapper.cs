using Autofac;
using ComicReader.Net.ApplicationMenu;
using ComicReader.Net.CenterGrid;
using ComicReader.Net.Common.Classes;
using ComicReader.Net.Common.Interfaces;
using Prism.Events;

namespace ComicReader.Net.Shell.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();
            var config = UserConfig.Load();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterInstance(config).As<IUserConfig>().SingleInstance();

            builder.RegisterModule<ShellModule>();
            builder.RegisterModule<ApplicationMenuModule>();
            builder.RegisterModule<CenterGridModule>();

            return builder.Build();
        }
    }
}