using UnityEngine.Events;

namespace GameCode.MessagingFramework
{
    public class HandlerEvent : UnityEvent<TransportMessage>
    {}

    public class TransportMessage
    {
        public object message;

        public T ConvertTo<T>()
        {
            return (T)message;
        }
    }

    //public interface IMessage
    //{ }
}
