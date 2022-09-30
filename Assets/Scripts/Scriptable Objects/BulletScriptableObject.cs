using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BulletServices;

namespace BulletSO
{
    [CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObject/Bullet/BulletScriptableObject")]
    public class BulletScriptableObject : ScriptableObject
    {
        [Header("Bullet Prefab")]
        public BulletView bulletView;

        [Header("Shooting Parameters")]
        public int damage;
        public float explosionRadius;
        public float explosionForce;

        [Header("Time Parameters")]
        public float maxLifeTime;
    }
}
