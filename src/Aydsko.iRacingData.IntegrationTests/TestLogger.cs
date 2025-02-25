﻿// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Logging;

namespace Aydsko.iRacingData.IntegrationTests;

public class TestLogger<TCategoryName> : ILogger<TCategoryName>
{
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (formatter is null)
        {
            throw new ArgumentNullException(nameof(formatter));
        }

        TestContext.Out.WriteLine($"[{logLevel,-11} | {eventId.Id} | {eventId.Name,-26}] {formatter(state, exception)}");
    }
}

public class TestLoggerFactory : ILoggerFactory
{
    public void AddProvider(ILoggerProvider provider)
    {
        throw new NotImplementedException();
    }

    public ILogger CreateLogger(string categoryName)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
    }
}
