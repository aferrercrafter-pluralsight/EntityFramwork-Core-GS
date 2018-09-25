using System;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace SamuraiApp.Data
{
    public class MyLoggerProvider : ILoggerProvider
    {
        ILogger ILoggerProvider.CreateLogger(string categoryName)
        {
            return new MyLogger();
        }

        private class MyLogger : ILogger
        {
            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if(eventId == RelationalEventId.CommandExecuted.Id)
                {
                    Console.WriteLine(formatter(state, exception));
                }                
            }
        }

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }
        




    }
}