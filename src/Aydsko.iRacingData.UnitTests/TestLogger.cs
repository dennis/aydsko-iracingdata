﻿// © 2023 Adrian Clark
// This file is licensed to you under the MIT license.

using Microsoft.Extensions.Logging;

namespace Aydsko.iRacingData.UnitTests;

public class TestLogger<T> : ILogger<T>
{
    public IDisposable BeginScope<TState>(TState state)
    {
        return new TestScope();
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel,
                            EventId eventId,
                            TState state,
                            Exception? exception,
                            Func<TState, Exception?, string> formatter)
    {
        if (formatter is null)
        {
            throw new ArgumentNullException(nameof(formatter));
        }

        var message = formatter(state, exception);

        if (string.IsNullOrWhiteSpace(message))
        {
            return;
        }

        message = $"{logLevel}: {message}";

        if (exception is not null)
        {
            message += Environment.NewLine + Environment.NewLine + exception.ToString();
        }

        Console.WriteLine(message);
    }
}

public sealed class TestScope : IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
