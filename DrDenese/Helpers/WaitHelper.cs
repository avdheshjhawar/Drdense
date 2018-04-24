using System;
using System.Threading;
using System.Configuration;


namespace DrDenese.Helpers
{
    public static class WaitHelper
    {
        static readonly int _timeOut = int.Parse(ConfigurationManager.AppSettings["TimeOut"]);

        static readonly int _timeStep = int.Parse(ConfigurationManager.AppSettings["TimeStep"]);
        static readonly int _timeFactor = int.Parse(ConfigurationManager.AppSettings["TimeFactor"]);

        public static int TimeOut { get { return _timeOut; } }

        public static TResult WaitResult<TResult>(Func<TResult> action, string errorMessage = null) where TResult : class
        {
            var stopDate = DateTime.Now.AddMilliseconds(_timeOut);
            TResult result = null;
            do
            {
                try
                {
                    result = action();
                }
                catch
                {
                    //Logger can be added.
                }
                if (result != null)
                {
                    return result;
                }
                Thread.Sleep(_timeStep);
            } while (DateTime.Now < stopDate);

            if (result == null)
            {
                throw new WaitException(errorMessage);
            }

            return result;
        }

        public static TResult WaitResult<TResult>(Func<TResult> action, TimeSpan customTimeOut, string errorMessage = null) where TResult : class
        {
            var stopDate = DateTime.Now.Add(customTimeOut);
            TResult result = null;
            do
            {
                result = action();
                if (result != null)
                {
                    return result;
                }
                Thread.Sleep(_timeStep);
            } while (DateTime.Now < stopDate);

            if (result == null)
            {
                throw new WaitException(errorMessage);
            }

            return result;
        }

        public static TResult WaitResult<TResult>(Func<TResult> action, Func<TResult> secondAction, string errorMessage = null) where TResult : class
        {
            TResult result = null;
            var stopDate = DateTime.Now.AddMilliseconds(_timeOut);
            do
            {
                try
                {
                    result = action();
                }
                catch
                {
                    //Logger can be added.
                }
                if (result == null)
                {
                    try
                    {
                        result = secondAction();
                    }
                    catch
                    {
                        //Logger can be added.
                    }
                }
                if (result != null)
                {
                    return result;
                }
                Thread.Sleep(_timeStep);
            } while (DateTime.Now < stopDate);

            if (result == null)
            {
                throw new WaitException(errorMessage);
            }

            return result;
        }

        /// <summary>
        /// Waits the result.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="actions">The actions.</param>
        /// <returns></returns>
        /// <exception cref="CertzoneAutomation.Framework.Exceptions.WaitException"></exception>
        public static TResult WaitResult<TResult>(string errorMessage, params Func<TResult>[] actions) where TResult : class
        {
            TResult result = null;
            var stopDate = DateTime.Now.AddMilliseconds(_timeOut);
            do
            {
                foreach (var action in actions)
                {
                    try
                    {
                        result = action();
                    }
                    catch
                    {
                        //Logger can be added.
                    }
                    if (result != null)
                    {
                        return result;
                    }
                }
                Thread.Sleep(_timeStep);
            } while (DateTime.Now < stopDate);

            if (result == null)
            {
                throw new WaitException(errorMessage);
            }

            return result;
        }

        public static bool WaitUntil(Func<bool> action)
        {
            var stopDate = DateTime.Now.AddMilliseconds(_timeOut);
            bool result = false;
            do
            {
                try
                {
                    result = action();
                }
                catch
                {
                }
                if (!result)
                {
                    Thread.Sleep(_timeStep);
                }
            } while (!result && DateTime.Now < stopDate);

            return result;
        }

        public static bool WaitUntil(Func<bool> action, TimeSpan timeOut, TimeSpan timeStep)
        {
            var stopDate = DateTime.Now.Add(timeOut);
            bool result = false;
            do
            {
                try
                {
                    result = action();
                }
                catch
                {
                }
                if (!result)
                {
                    Thread.Sleep(timeStep);
                }
            } while (!result && DateTime.Now < stopDate);

            return result;
        }

        /// <summary>
        /// Wait until action result is true, but not more than some number of regular delays
        /// </summary>
        /// <param name="action">Action to complete</param>
        /// <param name="count">Number of tries, non-negative</param>
        /// <returns>True is success, False otherwise</returns>
        public static bool WaitUntil(Func<bool> action, int count)
        {
            int executionTry = 0;
            bool result = false;
            do
            {
                try
                {
                    result = action();
                }
                catch
                {
                    //Logger can be added
                }
                if (!result)
                {
                    WaitFor(1000);
                    executionTry++;
                }
            } while (!result && executionTry < count);

            return result;
        }

        public static bool WaitUntil(Func<bool> action, TimeSpan customTimeOut)
        {
            var stopDate = DateTime.Now.Add(customTimeOut);
            bool result = false;
            do
            {
                try
                {
                    result = action();
                }
                catch
                {
                }
                if (!result)
                {
                    Thread.Sleep(_timeStep);
                }
            } while (!result && DateTime.Now < stopDate);

            return result;
        }

        /// <summary>
        /// Wait some adjusted time interval
        /// </summary>
        /// <param name="timeOut">Adjusted time interval value</param>
        public static void WaitFor(int timeOut)
        {
            Thread.Sleep(timeOut * _timeFactor / 100);
        }
    }

    public class WaitException : Exception
    {
        private readonly string _message = "Waited, but result is null";

        public WaitException()
        {

        }

        public WaitException(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                _message = message;
            }
        }

        public override string Message
        {
            get
            {
                return _message;
            }
        }
    }
}
