using SubscribersSystem.Business.Models;
using SubscribersSystem.Utility.DataCapture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscribersSystem.Utility
{
    public interface ISimulatorDataCaptureService
    {
        ConnectionBl CaptureConnection(double timeElapsed);
        SmsBl CaptureSms();
    }
    public class SimulatorDataCaptureService : ISimulatorDataCaptureService
    {
        private readonly IConnectionDataCapture _connectionDataCapture;
        private readonly ISmsDataCapture _smsDataCapture;

        public SimulatorDataCaptureService(IConnectionDataCapture connectionDataCapture, ISmsDataCapture smsDataCapture)
        {
            _connectionDataCapture = connectionDataCapture;
            _smsDataCapture = smsDataCapture;
        }

        public ConnectionBl CaptureConnection(double timeElapsed)
        {
            return _connectionDataCapture.Capture(timeElapsed);
        }

        public SmsBl CaptureSms()
        {
            return _smsDataCapture.Capture();
        }
    }
}
