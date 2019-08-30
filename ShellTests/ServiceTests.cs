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
        public void TaskScheduleServiceTest()
        {
            var strings = new List<string>();
            var taskScheduler = new TaskSchedulerService();
            var taskAddCharA = new Task(() =>
            {
                for (int i = 0; i < 100; i++)
                    strings.Add("a");
            });
            var taskAddCharB = new Task(() =>
            {
                for (int i = 0; i < 100; i++)
                    strings.Add("b");
            });

            var tasks = new Task[] { taskAddCharA, taskAddCharB };

            taskScheduler.QueueTask(taskAddCharA);

            Thread.Sleep(50);

            taskScheduler.QueueTask(taskAddCharB);

            Task.WaitAll(tasks);

            Assert.IsTrue(strings.Take(100).All(x => x == "a"), string.Join(Environment.NewLine, strings));
            Assert.IsTrue(strings.Skip(100).All(x => x == "b"));
        }
    }
}