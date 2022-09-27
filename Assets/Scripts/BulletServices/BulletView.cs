using GlobalServices;
using UnityEngine;

namespace BulletServices
{
    // Script is present on visual instance of bullet.
    [RequireComponent(typeof(Rigidbody))]
    public class BulletView : MonoBehaviour
    {
        private BulletController bulletController;

        // To set bullet controller reference in bullet view.
        public void SetBulletController(BulletController controller)
        {
            bulletController = controller;
        }

        private void OnTriggerEnter(Collider other)
        {
            bulletController.OnCollisionEnter(other);
        }

        public void DestroyBullet()
        {
            Destroy(gameObject);
        }
    }
}
