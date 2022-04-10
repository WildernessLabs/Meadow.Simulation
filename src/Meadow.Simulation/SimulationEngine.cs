﻿using Meadow.Hardware;
using Meadow.Logging;
using Meadow.Units;
using System;
using System.Collections.Generic;

namespace Meadow.Simulation
{
    internal class SimulationEngine<TPinDefinitions> : SimulationEnvironment, IMeadowIOController, IPlatformOS
        where TPinDefinitions : IPinDefinitions
    {
        private ISimulatedDevice<TPinDefinitions> _device;
        private WebSocketServer _wsServer;

        public SimulationEngine(ISimulatedDevice<TPinDefinitions> device, Logger logger)
        {
            _device = device;
            _wsServer = new WebSocketServer(logger);
        }

        private Dictionary<IPin, bool> _discreteStates = new Dictionary<IPin, bool>();
        private Dictionary<IPin, double> _analogStates = new Dictionary<IPin, double>();

        public IDeviceChannelManager DeviceChannelManager => throw new NotImplementedException();

        public string OSVersion => throw new NotImplementedException();

        public string OSBuildDate => throw new NotImplementedException();

        public string MonoVersion => throw new NotImplementedException();

        public event InterruptHandler Interrupt;

        public void Initialize()
        {
            foreach (var pin in _device.Pins)
            {
                // discretes
                if (pin.Supports<IDigitalChannelInfo>())
                {
                    _discreteStates.Add(pin, false);
                }

                // analog inputs
                if (pin.Supports<IDigitalChannelInfo>())
                {
                    _analogStates.Add(pin, 0d);
                }
            }

            _wsServer.Start();
        }

        void IMeadowIOController.Initialize()
        {
        }

        public void SetDiscrete(IPin pin, bool state)
        {
            SetPinVoltage(pin, state ? SimulationEnvironment.ActiveVoltage : SimulationEnvironment.InactiveVoltage);
        }

        public bool GetDiscrete(IPin pin)
        {
            return GetPinVoltage(pin) == SimulationEnvironment.ActiveVoltage; // TODO: do we want an active threshold below 3.3V?
        }

        public void SetPinVoltage(IPin pin, Voltage voltage)
        {
            if (pin is SimulatedPin { } sp)
            {
                if (voltage.Volts < 0) throw new ArgumentOutOfRangeException();

                var rising = voltage > sp.Voltage;

                sp.Voltage = voltage;

                Interrupt?.Invoke(pin, rising);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public Voltage GetPinVoltage(IPin pin)
        {
            if (pin is SimulatedPin { } sp)
            {
                return sp.Voltage;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public void SetResistorMode(IPin pin, ResistorMode mode)
        {
            throw new NotImplementedException();
        }

        public void ConfigureOutput(IPin pin, bool initialState, OutputType outputType)
        {
            throw new NotImplementedException();
        }

        public void ConfigureInput(IPin pin, ResistorMode resistorMode, InterruptMode interruptMode, double debounceDuration, double glitchDuration)
        {
            throw new NotImplementedException();
        }

        public void WireInterrupt(IPin pin, InterruptMode interruptMode, ResistorMode resistorMode, double debounceDuration, double glitchDuration)
        {
            throw new NotImplementedException();
        }

        public bool UnconfigureGpio(IPin pin)
        {
            throw new NotImplementedException();
        }

        public void ConfigureAnalogInput(IPin pin)
        {
            throw new NotImplementedException();
        }

        public int GetAnalogValue(IPin pin)
        {
            throw new NotImplementedException();
        }

        public void ReassertConfig(IPin pin)
        {
            throw new NotImplementedException();
        }

        public Temperature GetTemperature()
        {
            throw new NotImplementedException();
        }

        public T GetConfigurationValue<T>(IPlatformOS.ConfigurationValues item) where T : struct
        {
            throw new NotImplementedException();
        }

        public void SetConfigurationValue<T>(IPlatformOS.ConfigurationValues item, T value) where T : struct
        {
            throw new NotImplementedException();
        }
    }
}