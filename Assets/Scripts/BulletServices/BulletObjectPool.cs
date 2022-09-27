using GlobalServices;
using UnityEngine;

namespace BulletServices
{
    public class BulletObjectPool : ObjectPoolService<BulletController>
    {
        protected override BulletController CreateItem()
        {
            BulletController bulletController = BulletService.Instance.FireBullet(transform, 0f);
            return bulletController;
        }
    }
}
