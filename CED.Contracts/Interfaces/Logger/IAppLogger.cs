﻿namespace CED.Contracts.Interfaces.Logger;

/// <summary>
/// This type eliminates the need to depend directly on the ASP.NET Core logging types.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAppLogger<T>
{
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    public void LogError(string message, params object[] args);
    public void LogTrace(string message, params object[] args);
    public void LogDebug(string message, params object[] args);
}