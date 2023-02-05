using System;

namespace eStore_Admin.Application.Interfaces.Services;

public interface ILoggingService
{
    void LogDebug(Exception exception);
    void LogDebug(string message);
    void LogDebug<T0>(string message, T0 arg0);
    void LogDebug<T0, T1>(string message, T0 arg0, T1 arg1);
    void LogDebug<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);
    void LogInformation(Exception exception);
    void LogInformation(string message);
    void LogInformation<T0>(string message, T0 arg0);
    void LogInformation<T0, T1>(string message, T0 arg0, T1 arg1);
    void LogInformation<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);
    void LogWarning(Exception exception);
    void LogWarning(string message);
    void LogWarning<T0>(string message, T0 arg0);
    void LogWarning<T0, T1>(string message, T0 arg0, T1 arg1);
    void LogWarning<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);
    void LogError(Exception exception);
    void LogError(string message);
    void LogError<T0>(string message, T0 arg0);
    void LogError<T0, T1>(string message, T0 arg0, T1 arg1);
    void LogError<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);
    void LogCritical(Exception exception);
    void LogCritical(string message);
    void LogCritical<T0>(string message, T0 arg0);
    void LogCritical<T0, T1>(string message, T0 arg0, T1 arg1);
    void LogCritical<T0, T1, T2>(string message, T0 arg0, T1 arg1, T2 arg2);
}