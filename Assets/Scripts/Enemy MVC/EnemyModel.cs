using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemySO;

namespace EnemyServices
{
    public class EnemyModel
    {
        public int health { get; set; }
        public int maxHealth { get; }
        public float movementSpeed { get; }

        public float rotationSpeed { get; }

        public float minLaunchForce { get; } // Minimum bullet launch force.
        public float maxLaunchForce { get; } // Maximum bullet launch force.
        public float fireRotationRate { get; }

        public bool b_IsDead { get; set; }
        public bool b_IsFired { get; set; }

        // Patrolling
        public Vector3 walkPoint { get; set; } // Desired position of enemy.
        public Vector2 distanceToWalkPoint {get; set;} // calculate the distance from the walk point
        public float walkPointRange { get; set; }
        public bool walkPointSet { get; set; } // Is walk point selected.

        // Attacking
        public float fireRate { get; set; } // Bullet fire rate.

        // States
        public float patrollingRange { get; set; }
        public float patrolTime { get; }
        public float attackRange { get; set; }
        public bool b_PlayerInSightRange { get; set; }
        public bool b_PlayerInAttackRange { get; set; }

        public Color enemyColor { get; set; }

        public EnemyModel(EnemyScriptableObject enemyData)
        {
            health = enemyData.health;
            maxHealth = enemyData.health;

            movementSpeed = enemyData.movementSpeed;
            rotationSpeed = enemyData.rotationSpeed;
            fireRotationRate = enemyData.fireRotationRate;

            walkPointSet = false;
            b_IsDead = false;
            walkPointRange = enemyData.walkPointRange;

            patrolTime = enemyData.patrolTime;
            patrollingRange = enemyData.patrollingRange;
            attackRange = enemyData.attackRange;

            fireRate = enemyData.fireRate;
            minLaunchForce = enemyData.minLaunchForce;
            maxLaunchForce = enemyData.maxLaunchForce;
        }
    }

}