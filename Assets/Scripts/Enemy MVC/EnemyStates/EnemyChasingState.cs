using UnityEngine;

namespace EnemyServices
{
    // Handles behaviour of enemyin chasing state.
    public class EnemyChasingState : EnemyStateServices
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemyView.activeState = EnemyState.Chasing;
        }

        private void Update()
        {
            // Checks for state transition conditions. // If condition is satisfied, transitions into desired state.
            if (!enemyModel.b_PlayerInSightRange && !enemyModel.b_PlayerInAttackRange) enemyView.currentState.ChangeState(enemyView.patrollingState);
            else if (enemyModel.b_PlayerInSightRange && enemyModel.b_PlayerInAttackRange) enemyView.currentState.ChangeState(enemyView.attackingState);

            ChasePlayer();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        private void ChasePlayer()
        {
            if (!enemyView.playerTransform)
            {
                // If player is dead, we set enemy state to patrolling state.
                enemyView.currentState.ChangeState(enemyView.patrollingState);
                return;
            }

            // We set walk point as player position.
            enemyView.navAgent.SetDestination(enemyView.playerTransform.position);

            Vector3 direction = enemyView.playerTransform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.15f);
        }
    }
}
