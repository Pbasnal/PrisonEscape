using UnityEngine.Events;

namespace LockdownGames.GameCode.MessagingFramework
{
    public class HandlerEvent : UnityEvent<TransportMessage>
    {}

    public class TransportMessage
    {
        public object message;

        public T ConvertTo<T>()
        {
            if (message is T)
            {
                return (T)message;
            }

            return default(T);
        }
    }
}
