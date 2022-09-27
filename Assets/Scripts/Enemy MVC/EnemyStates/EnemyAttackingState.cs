using UnityEngine;
using GlobalServices;
using BulletServices;

namespace EnemyServices
{
    // Handles behaviour of enemyin attack state.
    public class EnemyAttackingState : EnemyStateServices
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemyView.activeState = EnemyState.Attacking;
        }

        private void Update()
        {
            // Checks for state transition conditions. // If condition is satisfied, transitions into desired state.
            if (!enemyModel.b_PlayerInSightRange && !enemyModel.b_PlayerInAttackRange) enemyView.currentState.ChangeState(enemyView.patrollingState);
            else if (enemyModel.b_PlayerInSightRange && !enemyModel.b_PlayerInAttackRange) enemyView.currentState.ChangeState(enemyView.chasingState);

            AttackPlayer();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        private async void AttackPlayer()
        {
            // If player is dead, we set enemy state to patrolling state.
            if (!enemyView.playerTransform)
            {
                enemyView.currentState.ChangeState(enemyView.patrollingState);
                return;
            }

            // We set walk point to current position so that enemy should not change its position in attack state. 
            enemyView.navAgent.SetDestination(enemyView.transform.position);

            // If enemy fire transform is not facing towards player, we rotate fire transform towards player.
            if (!IsPlayerPosition())
            {
                enemyView.fireTransform.transform.Rotate(GetRequiredFireRotation(), Space.Self);
            }

            // If enemy fire transform is facing towards player, fire bullet.
            if (!enemyModel.b_IsFired)
            {
                enemyModel.b_IsFired = true;
                FireBullet();

                // Enemyfires bullet after certain interval of time. // FireRate.
                await new WaitForSeconds(enemyModel.fireRate);
                ResetAttack();
            }
        }

        // Checks whether the enemy fire transform is facing towards player.
        private bool IsPlayerPosition()
        {
            // Forward direction of enemy fire transform.
            Vector3 forward = enemyView.fireTransform.transform.TransformDirection(Vector3.forward);

            // We cast a ray in forward direction of fire transform from center of enemy. 
            return Physics.Raycast(enemyView.transform.position, forward, enemyModel.attackRange, enemyView.playerLayerMask);
        }

        // Returns desired rotation of enemy.
        private Vector3 GetRequiredFireRotation()
        {
            Vector3 desiredRotation = new Vector3(0, 0, 0);
            Vector3 targetDir = enemyView.playerTransform.position - enemyView.fireTransform.transform.position;

            // Decides the direction of rotaion of fireTransform. // Whether to rotate from left side or right side.
            float angle = Vector3.SignedAngle(targetDir, enemyView.fireTransform.transform.forward, Vector3.up);

            if (angle < 0)
            {
                desiredRotation = Vector3.up * enemyModel.fireRotationRate * Time.deltaTime;
            }
            else if (angle > 0)
            {
                desiredRotation = -Vector3.up * enemyModel.fireRotationRate * Time.deltaTime;
            }

            return desiredRotation;
        }

        private void FireBullet()
        {
            BulletService.Instance.FireBullet(enemyView.fireTransform, enemyView.GetRandomLaunchForce());
            
            /* ///Attack code here
            Rigidbody rb = Instantiate(enemyView.fireTransform, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code  */
        }

        private void ResetAttack()
        {
            enemyModel.b_IsFired = false;
        }
    }
}
