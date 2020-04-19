using UnityEngine;

namespace GameCode.Messages
{
    public class UserInputMessage
    {
        public bool ClickedOnAnObject => transformOfClickedObject != null;
        public Transform transformOfClickedObject;
        public Vector2 inputLocationInGameSpace;
    }

    public class UserInputBeganMessage : UserInputMessage
    {
        public UserInputBeganMessage WithLcoation(Vector2 location)
        {
            inputLocationInGameSpace = new Vector2(location.x, location.y);

            return this;
        }

        public UserInputBeganMessage WithTransform(Transform transform)
        {
            transformOfClickedObject = transform;

            return this;
        }
    }

    public class UserInputDoubleClickMessage : UserInputMessage
    {
        public UserInputDoubleClickMessage WithLcoation(Vector2 location)
        {
            inputLocationInGameSpace = new Vector2(location.x, location.y);

            return this;
        }

        public UserInputDoubleClickMessage WithTransform(Transform transform)
        {
            transformOfClickedObject = transform;

            return this;
        }
    }

    public class UserInputHeldMessage : UserInputMessage
    {
        public float InputHeldTime;

        public UserInputHeldMessage WithLcoation(Vector2 location)
        {
            inputLocationInGameSpace = new Vector2(location.x, location.y);

            return this;
        }

        public UserInputHeldMessage WithTransform(Transform transform)
        {
            transformOfClickedObject = transform;

            return this;
        }

        public UserInputHeldMessage WithHeldTime(float inputHeldTime)
        {
            InputHeldTime = inputHeldTime;

            return this;
        }
    }
}
