using UnityEngine;
using UnityEngine.Events;

namespace GameCode.Player
{
    public class CanRaiseEventsBehaviour<T> : MonoBehaviour where T: new()
    {
        protected GameEvent<T> EventListeners;// { get; private set; }

        public void Register(UnityAction<T> callback)
        {
            if (EventListeners == null)
            {
                EventListeners = new GameEvent<T>();
            }

            EventListeners.AddListener(callback);
        }

        public void Remove(UnityAction<T> callback)
        {
            EventListeners.RemoveListener(callback);
        }

        protected void RaiseEvent(T eventInfo)
        {
            EventListeners?.Invoke(eventInfo);
        }
    }
}
