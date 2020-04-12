using UnityEngine;

namespace GameCode.Messages
{
    public class UserInputMessage// : IMessage
    {
        public Transform ClickOnTransform;
        public Vector2 InputLocationInGameSpace;
    }

    public class UserInputBeganMessage : UserInputMessage
    {
        public UserInputBeganMessage WithLcoation(Vector2 location)
        {
            InputLocationInGameSpace = new Vector2(location.x, location.y);

            return this;
        }

        public UserInputBeganMessage WithTransform(Transform transform)
        {
            ClickOnTransform = transform;

            return this;
        }
    }

    public class UserInputDoubleClickMessage : UserInputMessage
    {
        public UserInputDoubleClickMessage WithLcoation(Vector2 location)
        {
            InputLocationInGameSpace = new Vector2(location.x, location.y);

            return this;
        }

        public UserInputDoubleClickMessage WithTransform(Transform transform)
        {
            ClickOnTransform = transform;

            return this;
        }
    }

    public class UserInputHeldMessage : UserInputMessage
    {
        public float InputHeldTime;

        public UserInputHeldMessage WithLcoation(Vector2 location)
        {
            InputLocationInGameSpace = new Vector2(location.x, location.y);

            return this;
        }

        public UserInputHeldMessage WithTransform(Transform transform)
        {
            ClickOnTransform = transform;

            return this;
        }

        public UserInputHeldMessage WithHeldTime(float inputHeldTime)
        {
            InputHeldTime = inputHeldTime;

            return this;
        }
    }
}
