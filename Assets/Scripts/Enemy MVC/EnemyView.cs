using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyServices
{
    public class EnemyView : MonoBehaviour
    {
        public NavMeshAgent navAgent;

        public Transform fireTransform; // Bullet spawn position.
        public LayerMask playerLayerMask; // For player detection.
        public LayerMask groundLayerMask; // For ground detection.

        public Transform playerTransform; // Reference to player position.
        [HideInInspector] public EnemyController enemyController;

        public EnemyPatrollingState patrollingState; // Patrolling behaviour script.
        public EnemyChasingState chasingState; // Chasing behaviour script.
        public EnemyAttackingState attackingState; // Attacking behaviour script.

        [SerializeField] private EnemyState initialState;
        [HideInInspector] public EnemyState activeState;
        [HideInInspector] public EnemyStateServices currentState;

        private void Start()
        {
            playerTransform = GameObject.Find("Player").transform;
            navAgent = GetComponent<NavMeshAgent>();
            InitializeState();
        }
        public void SetEnemyControllerReference(EnemyController controller)
        {
            enemyController = controller;
        }

        private void FixedUpdate()
        {
            enemyController.UpdateEnemyController();
        }

        // To set initial state of enemy.
        private void InitializeState()
        {
            switch (initialState)
            {
                case EnemyState.Attacking:
                    {
                        currentState = attackingState;
                        break;
                    }
                case EnemyState.Chasing:
                    {
                        currentState = chasingState;
                        break;
                    }
                case EnemyState.Patrolling:
                    {
                        currentState = patrollingState;
                        break;
                    }
                default:
                    {
                        currentState = null;
                        break;
                    }
            }
            currentState.OnStateEnter();
        }
        
        // Returns random launch force value between minimum and maximum lauch force.
        public float GetRandomLaunchForce()
        {
            return Random.Range(enemyController.enemyModel.minLaunchForce, enemyController.enemyModel.maxLaunchForce);
        }

        // Implementation of IDamagable interface. 
        public void TakeDamage(int damage)
        {
            enemyController.TakeDamage(damage);
        }

        public void Death()
        {
            Destroy(gameObject);
        }
    }
}
