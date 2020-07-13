using System;
using System.Collections.Generic;

using Pathfinding;

using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.MovementMechanics
{
    [RequireComponent(typeof(Seeker))]
    public class AStarMover : MonoBehaviour
    {
        public float NextWaypointDistance = 0.5f;
        public ICanMove movementController;

        public Action onPathComplete;
        public bool IsMoving { get; private set; }
        public Vector2 target { get; private set; }
        public List<Vector3> path { get; private set; }
        
        private Vector2 _position;
        private Seeker seeker;

        private void Awake()
        {
            if (movementController == null)
            {
                throw new UnityException("Assign a movement controller to " + name);
            }

            seeker = GetComponent<Seeker>();

            target = transform.position;
            path = new List<Vector3>();
        }

        public bool SetPathTo(Vector2 target)
        {
            if (!seeker.IsDone())
            {
                return false;
            }

            if (path.Count == 0)
            {
                path.Add(transform.position);
            }

            this.target = target;
            seeker.StartPath(transform.position, target, OnPathFound);
            return true;
        }

        private void OnPathFound(Path p)
        {
            if (p.error)
            {
                IsMoving = false;
                return;
            }

            IsMoving = true;
            path = p.vectorPath;
            path.Add(target);
            path.RemoveAt(0);
        }

        public void MoveToNextWayPoint(float speed)
        {
            _position = transform.position;
            if (Vector2.Distance(_position, path[0]) > NextWaypointDistance)
            {
                movementController.Move(path[0], speed);
                return;
            }

            path.RemoveAt(0);
            if (path.Count != 0)
            {
                return;
            }

            path.Add(transform.position);
            onPathComplete?.Invoke();
            IsMoving = false;
        }

        public void ResetPath()
        {
            path.Clear();
            IsMoving = false;
        }
    }
}
