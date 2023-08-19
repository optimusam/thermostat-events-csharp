using System;
using System.ComponentModel;

namespace ThemostatAppEvents
{
    public class HeatSensor : IHeatSensor
    {
        double _warningLevel = 0;
        double _emergencyLevel = 0;
        double[]? _temperatureData;
        Boolean _isAboveWarningLevel = false;
        static readonly object _temperatureAboveWarningLevel = new();
        static readonly object _temperatureBelowWarningLevel = new();
        static readonly object _temperatureAboveEmergencyLevel = new();
        EventHandlerList _eventDelegateList = new EventHandlerList();

        public HeatSensor(double warningLevel, double emergencyLevel)
        {
            _warningLevel = warningLevel;
            _emergencyLevel = emergencyLevel;
            SeedData();
        }

        private void SeedData()
        {
            _temperatureData = new double[] { 16.5, 15, 20, 27.7, 28.5, 25, 23, 65, 77, 70, 25, 16 };
        }

        private void MonitorTemperature()
        {
            if (_temperatureData is null) throw new ArgumentException("Missing temperature data");
            foreach (double temperature in _temperatureData)
            {
                TemperatureEventArgs e = new TemperatureEventArgs { Temperature = temperature, CurrentDateTime = DateTime.Now };

                if (temperature > _emergencyLevel)
                {
                    _isAboveWarningLevel = true;
                    OnTemperatureAboveEmergencyLevel(e);
                }
                else if (temperature > _warningLevel)
                {
                    _isAboveWarningLevel = true;
                    OnTemperatureAboveWarningLevel(e);
                }
                else if(temperature < _warningLevel && _isAboveWarningLevel)
                {
                    _isAboveWarningLevel = false;
                    OnTemperatureBelowWarningLevel(e);
                }
                Thread.Sleep(1000);
            }
        }

        private void OnTemperatureAboveWarningLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs>? eventHandler = _eventDelegateList[_temperatureAboveWarningLevel] as EventHandler<TemperatureEventArgs>;
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }
        private void OnTemperatureBelowWarningLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs>? eventHandler = _eventDelegateList[_temperatureBelowWarningLevel] as EventHandler<TemperatureEventArgs>;
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }
        private void OnTemperatureAboveEmergencyLevel(TemperatureEventArgs e)
        {
            EventHandler<TemperatureEventArgs>? eventHandler = _eventDelegateList[_temperatureAboveEmergencyLevel] as EventHandler<TemperatureEventArgs>;
            if (eventHandler != null)
            {
                eventHandler(this, e);
            }
        }

        public event EventHandler<TemperatureEventArgs> TemperatureAboveWarningLevel
        {
            add
            {
                _eventDelegateList.AddHandler(_temperatureAboveWarningLevel, value);
            }

            remove
            {
                _eventDelegateList.RemoveHandler(_temperatureAboveWarningLevel, value);
            }
        }
        public event EventHandler<TemperatureEventArgs> TemperatureBelowWarningLevel
        {
            add
            {
                _eventDelegateList.AddHandler(_temperatureBelowWarningLevel, value);
            }

            remove
            {
                _eventDelegateList.RemoveHandler(_temperatureBelowWarningLevel, value);
            }
        }
        public event EventHandler<TemperatureEventArgs> TemperatureAboveEmergencyLevel
        {
            add
            {
                _eventDelegateList.AddHandler(_temperatureAboveEmergencyLevel, value);
            }

            remove
            {
                _eventDelegateList.RemoveHandler(_temperatureAboveEmergencyLevel, value);
            }
        }

        public void RunHeatSensor()
        {
            Console.WriteLine("Heat Sensor is running...");
            MonitorTemperature();
        }
    }
}

