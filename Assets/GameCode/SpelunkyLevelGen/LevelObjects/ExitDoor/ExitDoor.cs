﻿using GameCode.Messages;
using GameCode.MessagingFramework;
using UnityEngine;

namespace SpelunkyLevelGen.LevelGenerator.LevelObjects.ExitDoor
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class ExitDoor : MonoBehaviour
    {
        private void Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sortingOrder = 1;
        }
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag != "Player")
            {
                return;
            }

            Debug.Log("Collision Game finish");
            MessageBus.Publish(new PlayerWonMessage());
        }
    }
}