using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComicReader.Net.Shell.Services
{
    public class TaskSchedulerService
    {
        private Task _currentTask;
        private object @lock = new object();

        public void QueueTask(Task task)
        {
            lock (@lock)
            {
                if (_currentTask?.Status == TaskStatus.Running)
                {
                    _currentTask.ContinueWith((t) => task.Start());
                }
                else
                {
                    task.Start();
                }
                _currentTask = task;
            }
        }
    }
}