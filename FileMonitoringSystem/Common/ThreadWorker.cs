using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileMonitoringSystem.Common
{
    public abstract class ThreadWorker : IWorker
    {
        private CancellationTokenSource _stop;
        private ManualResetEvent _stopWaiter = new ManualResetEvent(true);

        protected CancellationToken CancelFlag { get; private set; }

        public void Start()
        {
            // остановщик, позволяет просигналить об остановке
            _stop = new CancellationTokenSource();
            // токен, который дают всем, кто хочет узнать об остановке
            CancelFlag = _stop.Token;
            // ожидальщик завершения работы, изначально в несигнальном состоянии
            _stopWaiter.Reset();

            Task.Run(() => WorkInternal());
        }

        public void Stop()
        {
            // сигнализируем об остановке
            _stop.Cancel();
            // ждём завершения работы воркера
            _stopWaiter.WaitOne();
        }

        private void WorkInternal()
        {
            try
            {
                // работаем
                Work();                
            }
            finally
            {
                // сигнализируем о завершении работы воркера
                _stopWaiter.Set();
            }
        }

        protected void Sleep(int milliseconds)
        {
            // блокирует поток, пока не пройдет таймаут либо не прозвучит сигнал остановки
            CancelFlag.WaitHandle.WaitOne(milliseconds);
        }

        protected abstract void Work();
    }
}
