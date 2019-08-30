using System;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.CenterGrid.Interfaces;
using ComicReader.Net.Shell.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ShellTests
{
    [TestClass]
    public class ViewModelTests
    {
        [TestMethod]
        public void MainViewModelTest()
        {
            var fileMenuViewModel = new Mock<IFileMenuViewModel>();
            var centerGridViewModel = new Mock<ICenterGridViewModel>();
            var viewModel = new MainViewModel(fileMenuViewModel.Object, centerGridViewModel.Object);

            Assert.IsNotNull(viewModel.CenterGridViewModel);
            Assert.IsNotNull(viewModel.FileMenuViewModel);
        }
    }
}