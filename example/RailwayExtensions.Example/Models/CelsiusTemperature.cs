// See https://aka.ms/new-console-template for more information
public class CelsiusTemperature
{
    public double Celsius { get; set; }

    public CelsiusTemperature(double celsius)
    {
        Celsius = celsius;
    }

    // implicit operator from Celsius to Fahrenheit
    public static implicit operator FahrenheitTemperature(CelsiusTemperature celsiusTemperature)
    {
        double fahrenheit = celsiusTemperature.Celsius * 9 / 5 + 32;
        return new FahrenheitTemperature(fahrenheit);
    }
}
