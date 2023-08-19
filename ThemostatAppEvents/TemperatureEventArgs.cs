using System;
namespace ThemostatAppEvents
{
	public class TemperatureEventArgs
	{
		public double Temperature { get; set; }
		public DateTime CurrentDateTime { get; set; }
    }
}

