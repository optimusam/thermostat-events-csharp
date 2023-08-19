using ThemostatAppEvents;

HeatSensor heatSensor = new(27, 75);
heatSensor.TemperatureAboveWarningLevel += HeatSensor_TemperatureAboveWarningLevel;
heatSensor.TemperatureBelowWarningLevel += HeatSensor_TemperatureBelowWarningLevel;
heatSensor.TemperatureAboveEmergencyLevel += HeatSensor_TemperatureAboveEmergencyLevel;
heatSensor.RunHeatSensor();

void HeatSensor_TemperatureAboveEmergencyLevel(object? sender, TemperatureEventArgs e)
{
    Console.ForegroundColor = ConsoleColor.DarkRed;
    Console.WriteLine($"Time {e.CurrentDateTime} : Temperature {e.Temperature} is above emergency level.");
    Console.ResetColor();
}

void HeatSensor_TemperatureBelowWarningLevel(object? sender, TemperatureEventArgs e)
{
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.WriteLine($"Time {e.CurrentDateTime} : Temperature {e.Temperature} is below warning level.");
    Console.ResetColor();
}

void HeatSensor_TemperatureAboveWarningLevel(object? sender, TemperatureEventArgs e)
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine($"Time {e.CurrentDateTime} : Temperature {e.Temperature} is above warning level.");
    Console.ResetColor();
}
