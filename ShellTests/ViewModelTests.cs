using System;
using ComicReader.Net.ApplicationMenu.Interfaces;
using ComicReader.Net.CenterGrid.Interfaces;
using ComicReader.Net.Common.Models;
using ComicReader.Net.Shell.ViewModels;
using Moq;
using NUnit.Framework;

namespace ShellTests
{
    public class ViewModelTests
    {
        [Test]
        public void MainViewModelTest()
        {
            var fileMenuViewModel = new Mock<IFileMenuViewModel>();
            var centerGridViewModel = new Mock<ICenterGridViewModel>();
            var viewModel = new MainViewModel(fileMenuViewModel.Object, centerGridViewModel.Object);

            Assert.IsNotNull(viewModel.CenterGridViewModel);
            Assert.IsNotNull(viewModel.FileMenuViewModel);
        }

        [Test]
        public void BookViewModelTest()
        {
            var book = new Book() { Name = "test", Id = 1 };
            var bookViewModel = new BookViewModel(book);

            Assert.AreEqual(book.Name, bookViewModel.Name);
            Assert.AreEqual(book.Id, bookViewModel.Id);
        }
    }
}