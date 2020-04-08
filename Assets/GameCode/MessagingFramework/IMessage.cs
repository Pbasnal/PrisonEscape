using UnityEngine.Events;

namespace GameCode.MessagingFramework
{
    public class HandlerEvent : UnityEvent<TransportMessage>
    {}

    public class TransportMessage
    {
        public IMessage message;

        public T ConvertTo<T>() where T : IMessage
        {
            return (T)message;
        }
    }

    public interface IMessage
    { }
}
