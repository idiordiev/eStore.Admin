using System;
using eStore_Admin.Application.Interfaces.Services;
using NLog;

namespace eStore_Admin.Infrastructure.Logging;

public class LoggingService : ILoggingService
{
    private readonly ILogger _logger;

    public LoggingService(ILogger logger)
    {
        _logger = logger;
    }

    public void LogDebug(Exception exception)
    {
        if (_logger.IsDebugEnabled)
        {
            _logger.Debug(exception);
        }
    }

    public void LogDebug(string message)
    {
        if (_logger.IsDebugEnabled)
        {
            _logger.Debug(message);
        }
    }

    public void LogDebug<T0>(string message, T0 arg0)
    {
        if (_logger.IsDebugEnabled)
        {
            _logger.Debug(string.Format(message, arg0));
        }

        _logger.Error(new Exception());
    }

    public void LogDebug<T0, T1>(string message, T0 arg0, T1 arg1)
    {
        if (_logger.IsDebugEnabled)
        {
            _logger.Debug(string.Format(message, arg0, arg1));
        }
    }

    public void LogDebug<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
    {
        if (_logger.IsDebugEnabled)
        {
            _logger.Debug(string.Format(message, arg0, arg1, arg2));
        }
    }

    public void LogInformation(Exception exception)
    {
        if (_logger.IsInfoEnabled)
        {
            _logger.Info(exception);
        }
    }

    public void LogInformation(string message)
    {
        if (_logger.IsInfoEnabled)
        {
            _logger.Info(message);
        }
    }

    public void LogInformation<T0>(string message, T0 arg0)
    {
        if (_logger.IsInfoEnabled)
        {
            _logger.Info(string.Format(message, arg0));
        }
    }

    public void LogInformation<T0, T1>(string message, T0 arg0, T1 arg1)
    {
        if (_logger.IsInfoEnabled)
        {
            _logger.Info(string.Format(message, arg0, arg1));
        }
    }

    public void LogInformation<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
    {
        if (_logger.IsInfoEnabled)
        {
            _logger.Info(string.Format(message, arg0, arg1, arg2));
        }
    }

    public void LogWarning(Exception exception)
    {
        if (_logger.IsWarnEnabled)
        {
            _logger.Warn(exception);
        }
    }

    public void LogWarning(string message)
    {
        if (_logger.IsWarnEnabled)
        {
            _logger.Warn(message);
        }
    }

    public void LogWarning<T0>(string message, T0 arg0)
    {
        if (_logger.IsWarnEnabled)
        {
            _logger.Warn(string.Format(message, arg0));
        }
    }

    public void LogWarning<T0, T1>(string message, T0 arg0, T1 arg1)
    {
        if (_logger.IsWarnEnabled)
        {
            _logger.Warn(string.Format(message, arg0, arg1));
        }
    }

    public void LogWarning<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
    {
        if (_logger.IsWarnEnabled)
        {
            _logger.Warn(string.Format(message, arg0, arg1, arg2));
        }
    }

    public void LogError(Exception exception)
    {
        if (_logger.IsErrorEnabled)
        {
            _logger.Error(exception);
        }
    }

    public void LogError(string message)
    {
        if (_logger.IsErrorEnabled)
        {
            _logger.Error(message);
        }
    }

    public void LogError<T0>(string message, T0 arg0)
    {
        if (_logger.IsErrorEnabled)
        {
            _logger.Error(string.Format(message, arg0));
        }
    }

    public void LogError<T0, T1>(string message, T0 arg0, T1 arg1)
    {
        if (_logger.IsErrorEnabled)
        {
            _logger.Error(string.Format(message, arg0, arg1));
        }
    }

    public void LogError<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
    {
        if (_logger.IsErrorEnabled)
        {
            _logger.Error(string.Format(message, arg0, arg1, arg2));
        }
    }

    public void LogCritical(Exception exception)
    {
        if (_logger.IsFatalEnabled)
        {
            _logger.Fatal(exception);
        }
    }

    public void LogCritical(string message)
    {
        if (_logger.IsFatalEnabled)
        {
            _logger.Fatal(message);
        }
    }

    public void LogCritical<T0>(string message, T0 arg0)
    {
        if (_logger.IsFatalEnabled)
        {
            _logger.Fatal(string.Format(message, arg0));
        }
    }

    public void LogCritical<T0, T1>(string message, T0 arg0, T1 arg1)
    {
        if (_logger.IsFatalEnabled)
        {
            _logger.Fatal(string.Format(message, arg0, arg1));
        }
    }

    public void LogCritical<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2)
    {
        if (_logger.IsFatalEnabled)
        {
            _logger.Fatal(string.Format(message, arg0, arg1, arg2));
        }
    }
}