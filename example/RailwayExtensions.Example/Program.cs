// See https://aka.ms/new-console-template for more information
using RailwayExtensions.Extensions;
using System.Text.Json;

CelsiusTemperature celsiusTemperature = new(25);

var result = await celsiusTemperature.ToResult()
        .OnSuccess(x               => TemperatureHandler.LogIfSuccess(x))
        .OnSuccess(x               => TemperatureHandler.IncreaseTemp(x, 5))
        .OnBoth(x                  => Console.WriteLine($"Calculated temp after increasing the temp: {JsonSerializer.Serialize(x)}"))
        .OnSuccess(x               => TemperatureHandler.GetFahrenheitFromCelsius(x))
        .OnBoth(x                  => Console.WriteLine($"Transformed to fahrenheit from celsius temp: {JsonSerializer.Serialize(x)}"))
        .OnSuccessAsync(async x    => await TemperatureHandler.GetKelvinFromFahrenheit(x))
        .OnBoth(x                  => Console.WriteLine($"Transformed to kelvin from fahrenheit temp: {JsonSerializer.Serialize(x)}"))
        .OnSuccessTry(x            => TemperatureHandler.ThrowException(x))
        .OnBoth(x                  => TemperatureHandler.LogIfSuccess(x))
        .OnSuccessTry(x            => TemperatureHandler.DoSomethingReturnTThrowException(x))
        .OnSuccessTryAsync(async x =>
        {
            await TemperatureHandler.TaskThrowExceptionAsync(x);

            return x;
        })
        .OnSuccessTryAsync(async x => await TemperatureHandler.TaskTThrowExceptionAsync(x))
        ;

if (result.IsFailure)
{
    if (result.Exception.GetType() == typeof(InvalidDataException))
    {
        Console.WriteLine($"InvalidDataException: {result.Exception}");
    }

    Console.WriteLine($"Result: {result.Error}");
}
