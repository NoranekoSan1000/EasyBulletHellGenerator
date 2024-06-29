using UnityEngine;

namespace EasyBulletHellGenerator
{
    public class BulletsLauncher : MonoBehaviour
    {
        private GameObject bulletsManagerObject;
        private GameObject bulletsPool;
        public GameObject targetObject;  

        private void Awake()
        {
            bulletsManagerObject = new GameObject("bulletsManagerObject");
            bulletsPool = new GameObject("bulletsPool");
            bulletsPool.AddComponent<BulletsPool>();
        }

        /// <summary>
        /// ScriptableObjectÇópÇ¢Çƒê›íË
        /// </summary>
        public BulletsManager GenerateBulletHell(BulletPattern bulletpattern)
        {
            GameObject bullets = Instantiate(bulletsManagerObject, transform.position, transform.rotation);
            BulletsManager s = bullets.AddComponent<BulletsManager>();
            s.Initialize(bulletpattern.bulletObject, targetObject); // ÉIÉuÉWÉFÉNÉgê›íË
            s.InitialBulletStatus(bulletpattern.isMissile, bulletpattern.speed, bulletpattern.acceleration, bulletpattern.existTime); // íeÇÃãììÆê›íË

            if (bulletpattern.formation == BulletsManager.ShotFormation.StraightShot)
            {
                s.StraightShot(bulletpattern.executionCount, bulletpattern.interval); // íeñãê›íË
            }
            else if (bulletpattern.formation == BulletsManager.ShotFormation.ExplosionShot)
            {
                s.ExplosionShot(bulletpattern.executionCount, bulletpattern.interval, bulletpattern.numBullets); // íeñãê›íË
            }
            else if (bulletpattern.formation == BulletsManager.ShotFormation.SpreadShot)
            {
                s.SpreadShot(bulletpattern.executionCount, bulletpattern.interval, bulletpattern.spreadAngle, bulletpattern.numBullets); // íeñãê›íË
            }
            else if (bulletpattern.formation == BulletsManager.ShotFormation.SiegeShot)
            {
                s.SiegeShot(bulletpattern.executionCount, bulletpattern.interval, bulletpattern.siegeRadius ,bulletpattern.numBullets); // íeñãê›íË
            }
            else
            {
                Debug.LogError("BulletPattern is Missing");
                s.StraightShot(bulletpattern.executionCount, bulletpattern.interval); // íeñãê›íË
            }
            return s;
        }
    }
}