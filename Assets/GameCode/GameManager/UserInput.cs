using GameCode.Messages;
using GameCode.MessagingFramework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameCode
{
    public enum InputPhase
    {
        None,
        Began,
        Stationary,
        Ended
    }

    public class UserInput : MonoBehaviour
    {
        public EventSystem currentEventSystem;

        public InputPhase InputPhase { get; private set; }

        [SerializeField] private Camera _camera;
        [SerializeField] [Range(0, 1)] private float _inputHeldUpdateInterval;
        [SerializeField] [Range(0, 1)] private float _maxDelayForDoubleClick;

        private float _timeOfLastClick;
        private float _timeOfLastHeldUpdate;

        // Messages
        private UserInputBeganMessage _userInputBeganMessage = new UserInputBeganMessage();
        private UserInputDoubleClickMessage _userInputDoubleClickMessage = new UserInputDoubleClickMessage();
        private UserInputHeldMessage _userInputHeldMessage = new UserInputHeldMessage();

        public void Awake()
        {
            _camera = _camera == null ? Camera.main : _camera;
            currentEventSystem = currentEventSystem == null ? 
                EventSystem.current : currentEventSystem;

            if (currentEventSystem == null)
            {
                throw new UnityException("No EventSystem exists in the scene. Please add an EventSystem");
            }
        }

        private void Update()
        {
            if (currentEventSystem.IsPointerOverGameObject())
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                InputPhase = InputPhase.Began;
                var mouseInGameLocation = _camera.ScreenToWorldPoint(Input.mousePosition);
                var clickedOnObject = DetectObject(mouseInGameLocation);

                if ((Time.time - _timeOfLastClick) < _maxDelayForDoubleClick)
                {
                    MessageBus.Publish(_userInputDoubleClickMessage
                        .WithLcoation(mouseInGameLocation)
                        .WithTransform(clickedOnObject));
                }
                else
                {
                    MessageBus.Publish(_userInputBeganMessage
                        .WithLcoation(mouseInGameLocation)
                        .WithTransform(clickedOnObject));
                }

                _timeOfLastHeldUpdate = _timeOfLastClick = Time.time;
            }
            else if (Input.GetMouseButton(0))
            {
                InputPhase = InputPhase.Stationary;

                if ((Time.time - _timeOfLastHeldUpdate) < _inputHeldUpdateInterval)
                {
                    return;
                }
                var mouseInGameLocation = _camera.ScreenToWorldPoint(Input.mousePosition);
                var clickedOnObject = DetectObject(mouseInGameLocation);

                var inputHeldTime = Time.time - _timeOfLastClick;
                MessageBus.Publish(_userInputHeldMessage
                    .WithLcoation(mouseInGameLocation)
                    .WithTransform(clickedOnObject)
                    .WithHeldTime(inputHeldTime));

                _timeOfLastHeldUpdate = Time.time;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                InputPhase = InputPhase.Ended;
            }
        }

        private Transform DetectObject(Vector2 location)
        {
            var collider = Physics2D.OverlapPoint(location);

            if (collider == null )
            {
                return null;
            };

            return collider.transform;
        }

    }
}
