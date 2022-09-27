using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalServices;

namespace GamePlayServices
{
    // To get random spawn transform for enemy.
    public class SpawnPointService : GenericSingleton<SpawnPointService>
    {
        public Transform[] totalSpawnPositions;

        public Transform GetEnemySpawnPoint()
        {
            int randomPosition = Random.Range(0, totalSpawnPositions.Length);

            return totalSpawnPositions[randomPosition];
        }
    }
}
