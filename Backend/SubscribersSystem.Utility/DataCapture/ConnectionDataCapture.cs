using SubscribersSystem.Business.Models;
using System;

namespace SubscribersSystem.Utility.DataCapture
{
    public interface IConnectionDataCapture
    {
        ConnectionBl Capture(double timeElapsed);
    }

    public class ConnectionDataCapture : IConnectionDataCapture
    {
        public ConnectionBl Capture(double timeElapsed)
        {
            var newConnection = new ConnectionBl
            {
                DateOfBeginning = DateTime.Now,
                TimeOfConnectionInSeconds = (int)Math.Ceiling(timeElapsed)
            };
            return newConnection;
        }
    }
}
