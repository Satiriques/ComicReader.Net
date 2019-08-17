using Autofac;
using ComicReader.Net.ApplicationMenu;

namespace ComicReader.Net.Shell.Startup
{
    public class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ShellModule>();
            builder.RegisterModule<ApplicationMenuModule>();

            return builder.Build();
        }
    }
}