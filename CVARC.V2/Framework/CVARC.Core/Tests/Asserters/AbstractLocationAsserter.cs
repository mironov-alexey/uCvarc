using AIRLab.Mathematics;

namespace CVARC.V2
{
    abstract class AbstractLocationAsserter<TSensorData> : ISensorAsserter<TSensorData>
    {
        public class LocationSensorData
        {
            public double X;
            public double Y;
            public Angle Angle;
        }

        public readonly LocationSensorData ExpextedLocation;
        public readonly double Delta;
        
        protected AbstractLocationAsserter(double x, double y, Angle angle, double delta)
        {
            ExpextedLocation = new LocationSensorData { X = x, Y = y, Angle = angle };
            Delta = delta;
        } 
        
        public abstract LocationSensorData GetSensorData(TSensorData data);

        public void Assert(TSensorData data, IAsserter asserter)
        {
            var actualLocation = GetSensorData(data);
            asserter.IsEqual(ExpextedLocation.X, actualLocation.X, Delta);
            asserter.IsEqual(ExpextedLocation.Y, actualLocation.Y, Delta);            
            asserter.IsEqual(ExpextedLocation.Angle.Grad, actualLocation.Angle.Grad, Delta);
        }
    }
}
