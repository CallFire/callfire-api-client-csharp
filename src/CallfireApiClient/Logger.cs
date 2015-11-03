using System;
using System.Diagnostics;
using System.Collections.Specialized;
using CallfireApiClient.Api.Common.Model;
using RestSharp.Serializers;

namespace CallfireApiClient
{
    /// <summary>
    /// Simple logging wrapper around TraceSource
    /// </summary>
    public class Logger
    {
        private readonly TraceSource TraceSource = new TraceSource(ClientConstants.LOG_TRACE_SOURCE_NAME);
        private readonly TraceListener CallfireLogFile;
        private readonly JsonSerializer Serializer;

        public Logger()
        {
            CallfireLogFile = TraceSource.Listeners[ClientConstants.LOG_FILE_LISTENER_NAME];
            Serializer = new JsonSerializer();
        }

        ~Logger()
        {
            TraceSource.Flush();
            TraceSource.Close();
        }

        public void Debug(string format, params object[] values)
        {
            if (TraceSource.Switch.Level == SourceLevels.Off)
            {
                return;
            }
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i].GetType() == typeof(CallfireModel))
                {
                    values[i] = Serializer.Serialize(values[i]);
                }
                else if (values[i].GetType() == typeof(NameValueCollection))
                {
                    values[i] = ClientUtils.ParamsToString((NameValueCollection)values[i]);
                }
            }

            CallfireLogFile.WriteLine(string.Format("{0} - {1} Debug: {2}", DateTime.Now.ToString(ClientConstants.LOG_DATETIME_PATTERN),
                    TraceSource.Name, string.Format(format, values)));
        }
    }
}

