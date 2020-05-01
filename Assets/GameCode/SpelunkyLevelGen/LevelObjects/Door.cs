using UnityEngine;

namespace LockdownGames.GameCode.SpelunkyLevelGen.LevelObjects
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Door : MonoBehaviour
    {
        [SerializeField] private Sprite openDoorSprite;
        [SerializeField] private Sprite closeDoorSprite;
        [SerializeField] private Sprite startDoorSprite;

        private new SpriteRenderer renderer;
        private new BoxCollider2D collider;

        private void Start()
        {
            renderer = GetComponent<SpriteRenderer>();
            collider = GetComponent<BoxCollider2D>();

            renderer.sprite = startDoorSprite;
        }

        public void OpenDoor()
        {
            renderer.sprite = openDoorSprite;
            collider.enabled = false;
        }

        public void CloseDoor()
        {
            renderer.sprite = closeDoorSprite;
            collider.enabled = true;
        }

    }
}
