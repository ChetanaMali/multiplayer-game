using UnityEngine;

namespace EnemySO
{
    [CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObject/Enemy/EnemyScriptableObject")]
    public class EnemyScriptableObject : ScriptableObject
    {
        [Header("Health Parameters")]
        public int health;

        [Header("Movement Parameters")]
        public float movementSpeed;
        public float rotationSpeed;
        public float fireRotationRate;
        public float walkPointRange;
        public float patrollingRange;
        public float patrolTime;

        [Header("Attack Parameters")]
        public float fireRate;
        public float attackRange;
        public float minLaunchForce;
        public float maxLaunchForce;
    }
}