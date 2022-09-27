using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamePlayServices;

namespace EnemyServices
{
    public class EnemyController 
    {
        public EnemyModel enemyModel { get; }
        public EnemyView enemyView { get; }

        public EnemyController(EnemyModel enemyModel, EnemyView enemyPrefab)
        {
            this.enemyModel = enemyModel;

            // Spawn enemy.
            Transform tranform = SpawnPointService.Instance.GetEnemySpawnPoint();
            enemyView = GameObject.Instantiate<EnemyView>(enemyPrefab, tranform.position, tranform.rotation);
            enemyView.enemyController = this;
        }

        public void EnableenemyView()
        {
            enemyView.gameObject.SetActive(true);
            enemyModel.b_IsDead = false;
        }

        public void DisableenemyView()
        {
            enemyView.gameObject.SetActive(false);
            enemyModel.b_IsDead = true;
        }

        // This method is called on every fixed update. // To do all physics calculations.
        public void UpdateEnemyController()
        {
            // Checks whether the player is in sight range or attack range.
            enemyModel.b_PlayerInSightRange = Physics.CheckSphere(enemyView.transform.position, enemyModel.patrollingRange, enemyView.playerLayerMask);
            enemyModel.b_PlayerInAttackRange = Physics.CheckSphere(enemyView.transform.position, enemyModel.attackRange, enemyView.playerLayerMask);
        }

        // Reduce current health by the amount of damage done.
        public void TakeDamage(int damage)
        {
            enemyModel.health -= damage;

            // If health goes below zero, enemy dies.
            if (enemyModel.health <= 0 && !enemyModel.b_IsDead)
            {
                Death();
            }
        }

        public void Death()
        {
            enemyModel.b_IsDead = true;

            GameObject.Destroy(enemyView.gameObject);
        }

    }
}
