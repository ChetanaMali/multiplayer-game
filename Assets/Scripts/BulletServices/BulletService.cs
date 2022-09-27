using BulletSO;
using GlobalServices;
using UnityEngine;

namespace BulletServices
{ 
    // Handles spawning of bullet and communication of bullet service with other services.
    public class BulletService : GenericSingleton<BulletService>
    {
        public BulletScriptableObject bulletSO;

        // To fire bullet. // Returns bullet controller.
        public BulletController FireBullet(Transform bulletTransform, float launchForce)
        {
            return CreateBullet(bulletTransform, launchForce);
        }

        // Spawns specified type of bullet at given position and sets its velocity as per launch force. 
        private BulletController CreateBullet(Transform bulletTransform, float launchForce)
        {
            BulletModel bulletModel = new BulletModel(bulletSO.damage,
                                                              bulletSO.maxLifeTime,
                                                              bulletSO.explosionRadius,
                                                              bulletSO.explosionForce);

            BulletController bulletController = new BulletController(bulletModel, bulletSO.bulletView, bulletTransform, launchForce);
            return bulletController;
        }
    }
}

