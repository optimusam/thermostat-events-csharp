using System;
namespace ThemostatAppEvents
{
	public interface IHeatSensor
	{
		event EventHandler<TemperatureEventArgs> TemperatureAboveWarningLevel;
		event EventHandler<TemperatureEventArgs> TemperatureBelowWarningLevel;
		event EventHandler<TemperatureEventArgs> TemperatureAboveEmergencyLevel;
		void RunHeatSensor();
	}
}

