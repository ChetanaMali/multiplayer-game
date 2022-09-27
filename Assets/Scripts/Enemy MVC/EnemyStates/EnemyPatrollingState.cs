using UnityEngine;

namespace EnemyServices
{
    // Handles behaviour of enemyin patrolling state.
    public class EnemyPatrollingState : EnemyStateServices
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemyView.activeState = EnemyState.Patrolling;
        }

        protected override void Start()
        {
            base.Start();
            ChangeWalkPoint();
        }

        private void Update()
        {
            // Checks for state transition conditions. // If condition is satisfied, transitions into desired state.
            if (enemyModel.b_PlayerInSightRange && !enemyModel.b_PlayerInAttackRange) enemyView.currentState.ChangeState(enemyView.chasingState);
            else if (enemyModel.b_PlayerInSightRange && enemyModel.b_PlayerInAttackRange) enemyView.currentState.ChangeState(enemyView.attackingState);

            Patroling();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        // To patrol enemy on nav-mesh.
        private void Patroling()
        {
            //Calculates DistanceToWalkPoint
            enemyModel.distanceToWalkPoint = new Vector2(Mathf.Abs(enemyModel.walkPoint.x) - Mathf.Abs(transform.position.x), Mathf.Abs(enemyModel.walkPoint.z) - Mathf.Abs(transform.position.z));

            // Search for walk point if enemy has reached to previous walk point.
            if (!enemyModel.walkPointSet) SearchWalkPoint();

            // Sets destination point for nav-mesh agent. //Calculate direction and walk to Point
            if (enemyModel.walkPointSet)
            {
                enemyView.navAgent.SetDestination(enemyModel.walkPoint);

                Vector3 direction = enemyModel.walkPoint - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f);
            }

            // If distance is less than 1, enemyhas reached to walk point.
            if (enemyModel.distanceToWalkPoint.magnitude < 1f)
            {
                enemyModel.walkPointSet = false;
            }
        }

        // Changes walk point of enemy after fixed interval.
        public async void ChangeWalkPoint()
        {
            while (true)
            {
                await new WaitForSeconds(enemyModel.patrolTime);
                enemyModel.walkPointSet = false;
            }
        }

        // Search for walk point.
        private void SearchWalkPoint()
        {
            // Selects random walk point from given range.
            float randomZ = Random.Range(-enemyModel.walkPointRange, enemyModel.walkPointRange);
            float randomX = Random.Range(-enemyModel.walkPointRange, enemyModel.walkPointRange);

            // Setting walk point for enemy enemy.
            enemyModel.walkPoint = new Vector3(enemyView.transform.position.x + randomX, enemyView.transform.position.y, enemyView.transform.position.z + randomZ);

            // To ensure walk point is on the ground.
            if (Physics.Raycast(enemyModel.walkPoint, -enemyView.transform.up, 2f, enemyView.groundLayerMask))
            {
                enemyModel.walkPointSet = true;
            }
        }
    }
}