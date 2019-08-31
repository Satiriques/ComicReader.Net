using ComicReader.Net.Shell.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShellTests
{
    public class ServiceTests
    {
        [Test]
        public void TaskSchedulerServiceTest()
        {
            var strings = new List<string>();
            var taskScheduler = new TaskSchedulerService();

            taskScheduler.QueueTask(() =>
            {
                Thread.Sleep(10);
                for (int i = 0; i < 100; i++)
                    strings.Add("a");
            });

            taskScheduler.QueueTask(() =>
            {
                for (int i = 0; i < 100; i++)
                    strings.Add("b");
            });

            var lastTask = new ManualResetEvent(false);
            taskScheduler.QueueTask(() => lastTask.Set());
            lastTask.WaitOne();

            Assert.IsTrue(strings.Take(100).All(x => x == "a"), string.Join(Environment.NewLine, strings));
            Assert.IsTrue(strings.Skip(100).All(x => x == "b"));
        }

        [Test]
        public void TaskSchedulerAsyncTest()
        {
            var strings = new List<string>();
            var taskScheduler = new TaskSchedulerService();

            taskScheduler.QueueTask(async () =>
            {
                await Task.Delay(100);
                await AddStringAsync(strings, "a");
            });

            taskScheduler.QueueTask(async () => await AddStringAsync(strings, "b"));

            var lastTask = new ManualResetEvent(false);
            taskScheduler.QueueTask(() => lastTask.Set());
            lastTask.WaitOne();

            Assert.IsTrue(strings.Take(100).All(x => x == "a"), string.Join(Environment.NewLine, strings));
            Assert.IsTrue(strings.Skip(100).All(x => x == "b"));
        }

        private async Task AddStringAsync(List<string> strings, string letter)
        {
            for (int i = 0; i < 100; i++)
            {
                strings.Add(letter);
            }
        }
    }
}