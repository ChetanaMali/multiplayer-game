using GlobalServices;
using UnityEngine;

namespace BulletServices
{
    // Handles all behaviour of bullet. 
    public class BulletController 
    {
        public BulletModel bulletModel { get; }
        public BulletView bulletView { get; }

        
        public BulletController(BulletModel model, BulletView bulletPrefab, Transform fireTransform, float launchForce)
        {
            // Holds all data of bullet. 
            bulletModel = model;

            // Visual instance of bullet.
            bulletView = GameObject.Instantiate<BulletView>(bulletPrefab, fireTransform.position, fireTransform.rotation);
            bulletView.SetBulletController(this);

            // Set's velocity of bullet as per input launch force.
            bulletView.GetComponent<Rigidbody>().velocity = fireTransform.forward * launchForce;
        }

        // Applies damage to the object collided with bullet using IDamagable interface.
        public void OnCollisionEnter(Collider other)
        { 
            // Check's whether collided object implements IDamagable interface.
            IDamagable damagable = other.GetComponent<IDamagable>();

            if(damagable != null)
            {
                ApplyDamage(damagable, other);
            }

            // To destroy bullet after collision.
            bulletView.DestroyBullet();
        }

        // Applies damage only if the collided object has rigidbody component.
        private void ApplyDamage(IDamagable damagable, Collider other)
        {
            Rigidbody targetRigidbody = other.GetComponent<Rigidbody>();

            if (targetRigidbody)
            {
                damagable.TakeDamage(bulletModel.bulletDamage);
            }
        }
    }
}
