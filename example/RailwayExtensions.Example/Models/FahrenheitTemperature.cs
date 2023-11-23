// See https://aka.ms/new-console-template for more information
public class FahrenheitTemperature
{
    public double Fahrenheit { get; set; }

    public FahrenheitTemperature(double fahrenheit)
    {
        Fahrenheit = fahrenheit;
    }

    // implicit operator from Fahrenheit to Kelvin
    public static implicit operator KelvinTemperature(FahrenheitTemperature fahrenheitTemperature)
    {
        double kelvinTemperature = (fahrenheitTemperature.Fahrenheit + 459.67) * 5 / 9;

        return new KelvinTemperature(kelvinTemperature);
    }
}
