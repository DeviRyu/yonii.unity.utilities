namespace Yonii.Unity.Utilities.Timers
{
    public sealed class CountDownTime : Timer
    {
        public CountDownTime(float time) : base(time)
        {
        }

        public override void Tick(float deltaTime)
        {
            if(IsRunning && Time > 0)
                Time -= deltaTime;
            
            if(IsRunning && Time <= 0)
                Stop();
        }
        
        public bool IsFinished => Time <= 0;
        public void Reset() => Time = _initialTime;
        public void Reset(float newTime) 
        {
            _initialTime = newTime;
            Reset();
        }
    }
}