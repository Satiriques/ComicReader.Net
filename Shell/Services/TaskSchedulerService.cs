using ComicReader.Net.Common.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComicReader.Net.Shell.Services
{
    public class TaskSchedulerService : ITaskSchedulerService, IDisposable
    {
        public int CurrentlyQueuedTasks { get { return _Queue.Count; } }

        private BlockingCollection<Func<Task>> _Queue = new BlockingCollection<Func<Task>>();

        public TaskSchedulerService()
        {
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        var action = _Queue.Take();
                        await action();
                    }
                    catch (InvalidOperationException)
                    {
                        break;
                    }
                    catch
                    {
                        // TODO log me
                    }
                }
            });
        }

        public void QueueTask(Func<Task> action)
        {
            _Queue.Add(action);
        }

        public void Dispose()
        {
            _Queue.CompleteAdding();
        }
    }
}