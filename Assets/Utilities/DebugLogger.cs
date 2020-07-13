using System;
using UnityEngine;

namespace LockdownGames.Utilities
{
    [Serializable]
    public class DebugLogger
    {
        public bool LogToConsole;

        public void Log(string msg, params object[] args)
        {
            if (!LogToConsole)
            {
                return;
            }

            if (args.Length != 0)
            {
                msg = string.Format(msg, args);
            }
            Debug.Log(msg);
        }
    }
}
