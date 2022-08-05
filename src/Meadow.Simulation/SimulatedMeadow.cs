﻿using Meadow.Devices;
using Meadow.Hardware;
using Meadow.Units;
using System;
using System.Linq;

namespace Meadow.Simulation
{
    public partial class SimulatedMeadow<TPinDefinitions> : ISimulatedDevice<TPinDefinitions>
        where TPinDefinitions : IPinDefinitions, new()
    {
        private SimulationEngine<TPinDefinitions> _simulationEngine;
        private IPlatformOS _platformOS;

        public event PowerTransitionHandler BeforeReset;
        public event PowerTransitionHandler BeforeSleep;
        public event PowerTransitionHandler AfterWake;
        public event NetworkConnectionHandler NetworkConnected;
        public event NetworkDisconnectionHandler NetworkDisconnected;

        public SimulatedMeadow()
        {
            Pins = new TPinDefinitions();
            _platformOS = new SimulatedPlatformOS();
            _simulationEngine = new SimulationEngine<TPinDefinitions>(this);
            Information = new SimulationInformation();

            LaunchUI();
        }

        public TPinDefinitions Pins { get; }
        public IDeviceInformation Information { get; }

        public IPlatformOS PlatformOS => _platformOS;
        public DeviceCapabilities Capabilities => throw new NotImplementedException();

        public INetworkAdapterCollection NetworkAdapters => throw new NotImplementedException();

        private void LaunchUI()
        {

        }

        public void DrivePinVoltage(IPin pin, Voltage voltage)
        {
            _simulationEngine.SetPinVoltage(pin, voltage);
        }

        public void DrivePinState(IPin pin, bool state)
        {
            _simulationEngine.SetDiscrete(pin, state);
        }

        public bool ReadPinState(IPin pin)
        {
            return _simulationEngine.GetDiscrete(pin);
        }

        public IAnalogInputPort CreateAnalogInputPort(IPin pin, int sampleCount, TimeSpan sampleInterval, Meadow.Units.Voltage voltageReference)
        {
            var dc = pin.SupportedChannels.FirstOrDefault(i => i is IAnalogChannelInfo) as AnalogChannelInfo;
            if (dc != null)
            {
                return new SimulatedAnalogInputPort(pin, dc, sampleCount, sampleInterval, voltageReference);
            }

            throw new NotSupportedException();
        }

        public IBiDirectionalPort CreateBiDirectionalPort(IPin pin, bool initialState, InterruptMode interruptMode, ResistorMode resistorMode, PortDirectionType initialDirection, TimeSpan debounceDuration, TimeSpan glitchDuration, OutputType output = OutputType.PushPull)
        {
            var dc = pin.SupportedChannels.FirstOrDefault(i => i is IDigitalChannelInfo) as DigitalChannelInfo;
            if (dc != null)
            {
                return new SimulatedBiDirectionalPort(pin, dc, initialState, interruptMode, resistorMode, initialDirection, debounceDuration, glitchDuration);
            }

            throw new NotSupportedException();
        }

        public IDigitalInputPort CreateDigitalInputPort(IPin pin, InterruptMode interruptMode, ResistorMode resistorMode, TimeSpan debounceDuration, TimeSpan glitchDuration)
        {
            var dci = pin.SupportedChannels.FirstOrDefault(i => i is IDigitalChannelInfo) as DigitalChannelInfo;
            if (dci != null)
            {
                return new SimulatedDigitalInputPort(pin, dci, interruptMode);
            }

            throw new NotSupportedException();
        }

        public IDigitalOutputPort CreateDigitalOutputPort(IPin pin, bool initialState = false, OutputType initialOutputType = OutputType.PushPull)
        {
            var dco = pin.SupportedChannels.FirstOrDefault(i => i is IDigitalChannelInfo) as DigitalChannelInfo;
            if (dco != null)
            {
                return new SimulatedDigitalOutputPort(pin, dco, false, OutputType.PushPull);
            }

            throw new NotSupportedException();
        }

        public II2cBus CreateI2cBus(int busNumber = 0)
        {
            throw new NotImplementedException();
        }

        public II2cBus CreateI2cBus(int busNumber, Meadow.Units.Frequency frequency)
        {
            throw new NotImplementedException();
        }

        public II2cBus CreateI2cBus(IPin[] pins, Meadow.Units.Frequency frequency)
        {
            throw new NotImplementedException();
        }

        public II2cBus CreateI2cBus(IPin clock, IPin data, Meadow.Units.Frequency frequency)
        {
            throw new NotImplementedException();
        }

        public IPwmPort CreatePwmPort(IPin pin, float frequency = 100, float dutyCycle = 0.5F, bool invert = false)
        {
            throw new NotImplementedException();
        }

        public ISerialMessagePort CreateSerialMessagePort(SerialPortName portName, byte[] suffixDelimiter, bool preserveDelimiter, int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int readBufferSize = 512)
        {
            throw new NotImplementedException();
        }

        public ISerialMessagePort CreateSerialMessagePort(SerialPortName portName, byte[] prefixDelimiter, bool preserveDelimiter, int messageLength, int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int readBufferSize = 512)
        {
            throw new NotImplementedException();
        }

        public ISerialPort CreateSerialPort(SerialPortName portName, int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int readBufferSize = 1024)
        {
            throw new NotImplementedException();
        }

        public ISpiBus CreateSpiBus(IPin clock, IPin mosi, IPin miso, SpiClockConfiguration config)
        {
            throw new NotImplementedException();
        }

        public ISpiBus CreateSpiBus(IPin clock, IPin mosi, IPin miso, Meadow.Units.Frequency speed)
        {
            throw new NotImplementedException();
        }

        public IPin GetPin(string name)
        {
            return Pins[name];
        }

        public void Initialize()
        {
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void SetClock(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void WatchdogEnable(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public void WatchdogReset()
        {
            throw new NotImplementedException();
        }

        public void Sleep(int seconds = -1)
        {
            throw new NotImplementedException();
        }

        public BatteryInfo GetBatteryInfo()
        {
            throw new NotImplementedException();
        }

        public Temperature GetProcessorTemperature()
        {
            throw new NotImplementedException();
        }

        public IPwmPort CreatePwmPort(IPin pin, Frequency frequency, float dutyCycle = 0.5F, bool invert = false)
        {
            throw new NotImplementedException();
        }

        public ICounter CreateCounter(IPin pin, InterruptMode edge)
        {
            throw new NotImplementedException();
        }
    }
}
