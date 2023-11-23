// See https://aka.ms/new-console-template for more information
using System.Text.Json;

public static class TemperatureHandler
{
    public static void LogIfSuccess<T>(T temp)
    {
        Console.WriteLine($"Logged the temp: {JsonSerializer.Serialize(temp)}");
    }

    public static CelsiusTemperature IncreaseTemp(CelsiusTemperature temp, double temperatureNumber)
    {
        temp.Celsius = temp.Celsius + temperatureNumber;

        Console.WriteLine($"Calculated age: {temp.Celsius.ToString("N")}");

        return temp;
    }
    public static FahrenheitTemperature GetFahrenheitFromCelsius(CelsiusTemperature temp)
    {
        return temp;
    }

    public static async Task<KelvinTemperature> GetKelvinFromFahrenheit(FahrenheitTemperature temp)
    {
        await Task.Delay(1);

        return temp;
    }

    public static async Task DoSomethingReturnTaskAsync<T>(T value)
    {
        Console.WriteLine("Process DoSomethingReturnTaskAsync");

        await Task.Delay(1);
    }

    public static async Task<KelvinTemperature> DoSomethingReturnTaskTAsync(KelvinTemperature temp)
    {
        Console.WriteLine("Process DoSomethingReturnTaskTAsync");

        await Task.Delay(1);

        return temp;
    }

    public static void ThrowException(KelvinTemperature temp)
    {
        Console.WriteLine("Process doing something");

        throw new InvalidDataException("Crashhhh... on doing something");
    }

    public static T DoSomethingReturnTThrowException<T>(T value)
    {
        Console.WriteLine("Process DoSomethingReturnTThrowException");

        throw new Exception("Crashhhh... on DoSomethingReturnTThrowException");

        return value;
    }

    public static async Task TaskThrowExceptionAsync<T>(T value)
    {
        Console.WriteLine("Process TaskThrowExceptionAsync");

        throw new Exception("Crashhhh... on TaskThrowExceptionAsync");

        await Task.Delay(1);
    }

    public static async Task<T> TaskTThrowExceptionAsync<T>(T value)
    {
        Console.WriteLine("Process TaskTThrowExceptionAsync");

        await Task.Delay(1);

        throw new Exception("Crashhhh... on TaskTThrowExceptionAsync");

        return value;
    }
}
