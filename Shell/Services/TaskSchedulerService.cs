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

        private BlockingCollection<Action> _Queue = new BlockingCollection<Action>();

        public TaskSchedulerService()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        var action = _Queue.Take();
                        action();
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

        public void QueueTask(Action action)
        {
            _Queue.Add(action);
        }

        public void Dispose()
        {
            _Queue.CompleteAdding();
        }
    }
}