using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalServices;
using EnemySO;

namespace EnemyServices
{
    public class EnemyService : GenericSingleton<EnemyService>
    {
        public EnemyView enemyView;
        public EnemyScriptableObject enemySO;

        private void Start()
        {
            CreateEnemy();
        }

        private EnemyController CreateEnemy()
        {
            EnemyModel enemyModel = new EnemyModel(enemySO);
            EnemyController enemyController = new EnemyController(enemyModel, enemyView);
            enemyController.enemyView.SetEnemyControllerReference(enemyController);
            return enemyController;
        }
    }
}
