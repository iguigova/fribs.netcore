using fribs.netcore.configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Diagnostics;

namespace fribs.netcore.logging
{
    public static class Log
    {
        public static void Error(string message)
        {
            Error(null, message);
        }

        public static void Error(Exception ex, string message = null)
        {
            if (ex != null)
            {
                LogAll(ex);

                if (ex.Data != null)
                {
                    foreach (var value in ex.Data.Values)
                    {
                        LogAll(value);
                    }
                }
            }

            if (!string.IsNullOrEmpty(message))
            {
                LogAll(message);
            }
        }

        public static void Info<T>(T message)
        {
            LogAll(message, Verbosity.Info);
        }

        private enum Verbosity
        {
            Error,
            Info
        }

        private static void LogAll<T>(T value, Verbosity verbosity = Verbosity.Error)
        {
            var log = $"";

            log = JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Error = delegate (object sender, ErrorEventArgs args)
                {
                    log = string.Format("Failed to serialiaze passed in value of type {0}: {1}", value?.GetType(), args.ErrorContext.Error.Message);
                    args.ErrorContext.Handled = true;
                },
            });

            switch (verbosity)
            {

                case Verbosity.Error: LogManager.Logger.Error(log); break;
                default: LogManager.Logger.Info(log); break;
            }
        }

		public static T Time<T>(Func<T> func, string message = null, bool catchException = false, string errorMessage = null, Func<T, string> details = null)
		{
			var timeInFunc = Stopwatch.StartNew();

			var result = default(T);

			if (catchException)
			{
				try
				{
					result = func();
				}
				catch (Exception ex)
				{
					Log.Error(ex);

					if (!string.IsNullOrEmpty(errorMessage))
					{
						Log.Info(errorMessage);
					}
				}
			}
			else
			{
				result = func();
			}

			timeInFunc.Stop();

			var detailsMessage = details != null ? details(result) : null;

			Log.Info($"({timeInFunc.Elapsed}) {message ?? typeof(T).Name} {detailsMessage}");

			return result;
		}

		public static void Time(Action func, string message, bool catchException = false, string errorMessage = null)
		{
			Time(() => { func(); return true; }, message, catchException, errorMessage);
		}

		private static LogManager _logManager; 
		public static LogManager LogManager { get { return _logManager ?? (_logManager = new LogManager(Configuration.Instance.PapertrailUrl, Configuration.Instance.PapertrailPort)); } }
    }
}
