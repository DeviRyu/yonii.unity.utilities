using System;
// ReSharper disable InconsistentNaming

namespace Yonii.Unity.Utilities.Timers
{
    public abstract class Timer
    {
        internal float _initialTime;

        #region Public Properties

        public float Time { get; set; }
        public bool IsRunning { get; protected set; }
        public float Progress => Time / _initialTime;

        #region Actions

        public Action OnTimerStart = delegate { };
        public Action OnTimerEnd = delegate { };

        #endregion

        #endregion

        protected Timer(float time)
        {
            _initialTime = time;
            IsRunning = false;
        }

        #region Public Methods (Start/Stop/etc..)

        public void Start()
        {
            Time = _initialTime;
            if (IsRunning) 
                return;

            IsRunning = true;
            OnTimerStart();
        }

        public void Stop()
        {
            if(!IsRunning)
                return;
            
            IsRunning = false;
            OnTimerEnd();
        }
        
        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;
        public abstract void Tick(float deltaTime);

        #endregion
    }
}