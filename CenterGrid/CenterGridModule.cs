using Autofac;
using ComicReader.Net.CenterGrid.ViewModels;
using ComicReader.Net.CenterGrid.Views;

namespace ComicReader.Net.CenterGrid
{
    public class CenterGridModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CenterGridViewModel>();
            builder.RegisterType<CenterGridView>();
        }
    }
}