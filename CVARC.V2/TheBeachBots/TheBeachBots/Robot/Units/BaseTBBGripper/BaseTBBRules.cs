namespace CVARC.V2
{
    public class BaseTBBGripperRules : IGripperRules 
    {
        public double GrippingTime { get; private set; }
        public double ReleasingTime { get; private set; }
        
        public BaseTBBGripperRules(double grippingTime, double releaseTime)
        {
            GrippingTime = grippingTime;
            ReleasingTime = releaseTime;
        }
    }
}
