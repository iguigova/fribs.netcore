using System;
using Newtonsoft.Json;

namespace fribs.netcore.exceptions
{
    public class ExceptionParser
    {
        public string Message { get; private set; }

        public string Source { get; private set; }

        public string TargetSite { get; private set; }

        public string StackTrace { get; private set; }

        public string Type { get; private set; }

        public ExceptionParser InnerException { get; private set; }

        public ExceptionParser(Exception ex)
        {
            if (ex.InnerException != null)
            {
                InnerException = new ExceptionParser(ex.InnerException); //recursively get all of them
            }

            Message = ex.Message;
            Source = ex.Source;
            TargetSite = (ex.TargetSite != null) ? ex.TargetSite.ToString() : null;
            StackTrace = ex.StackTrace;
            Type = ex.GetType().Name;
        }

        public override string ToString()
        {
			return JsonConvert.SerializeObject(this);
        }
    }
}
