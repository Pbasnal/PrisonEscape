using System.Collections.Generic;
using LockdownGames.GameAi.StateMachineAi;
using LockdownGames.Mechanics.ActorMechanics.CombatMechanics.BossStates;
using LockdownGames.Mechanics.ActorMechanics.CombatMechanics.TargetApproach;
using UnityEngine;

namespace LockdownGames.Mechanics.ActorMechanics.CombatMechanics
{
    public class Enemy : StateMachine
    {
        public NpcStats enemyStats;
        public Transform virtualTarget;

        [HideInInspector] public Transform target;

        [Space]
        [Header("Components")]
        public ApproachTarget approachTarget;

        [Space]
        [Header("States")]
        public WanderingState wanderingState;
        public ApproachTargetState ApproachTargetState;
        public SearchLastKnownPositionState searchLastKnownPositionState;

        [Space]
        [Header("ChildComponents")]
        public EnemyDetector enemyDetector;

        [Space]
        [Header("Debug")]
        public string CurrentStateName;

        [Range(0, 1)]
        public float TimeSpeed = 1.0f;

        private new Rigidbody2D rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
            InitializeStateMachine(new List<IState>
            {
                wanderingState,
                ApproachTargetState,
                searchLastKnownPositionState
            }, wanderingState);
        }

        private void OnDisable()
        {
            enemyDetector.foundATarget -= TargetFound;
            enemyDetector.targetLost -= TargetLost;
        }

        private void OnEnable()
        {
            enemyDetector.foundATarget += TargetFound;
            enemyDetector.targetLost += TargetLost;
        }

        private void TargetLost(Transform target)
        {
            SetStateTo<SearchLastKnownPositionState>();
            this.target = null;
        }

        private void TargetFound(Transform target)
        {
            this.target = target;
            SetStateTo<ApproachTargetState>();
        }

        private void Update()
        {
            currentState.Update();
            CurrentStateName = currentState.GetType().Name;

            enemyDetector.FindTargetInRange(rigidbody);
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdate();
            CurrentStateName = currentState.GetType().Name;
            Time.timeScale = TimeSpeed;
        }

        private void OnDrawGizmosSelected()
        {
            if (enemyDetector == null)
            {
                enemyDetector = AddChildBehaviour<EnemyDetector>(nameof(enemyDetector));
            }

            if (CheckComponent(ref approachTarget) == null || approachTarget.npcApproachData == null)
            {
                return;
            }

            enemyDetector.detectionRadius = approachTarget.npcApproachData.detectionDistance;

            wanderingState.SetState(this);
            searchLastKnownPositionState.SetState(this);
        }

        private T CheckComponent<T>(ref T comp) where T : MonoBehaviour
        {
            if (comp != null)
            {
                return comp;
            }

            comp = gameObject.GetComponent<T>();
            return comp;
        }

        private T AddChildBehaviour<T>(string name) where T : MonoBehaviour
        {
            var gameObject = new GameObject(name);
            var addedBehaviour = gameObject.AddComponent<T>();
            gameObject.transform.position = transform.position;
            gameObject.transform.parent = transform;

            return addedBehaviour;
        }
    }
}