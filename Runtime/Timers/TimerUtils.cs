using UnityEngine;

namespace Yonii.Unity.Utilities.Timers
{
    public static class TimerUtils
    {
        public static float CountDown(this float time)
        {
            time -= Time.deltaTime;
            return time;
        }
        
        public static bool HasFinished(this float time) => time <= 0;
    }
}