using Autofac;
using ComicReader.Net.ApplicationMenu;
using Prism.Events;

namespace ComicReader.Net.Shell.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterModule<ShellModule>();
            builder.RegisterModule<ApplicationMenuModule>();

            return builder.Build();
        }
    }
}