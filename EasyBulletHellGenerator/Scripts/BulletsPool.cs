using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace EasyBulletHellGenerator
{
    public class BulletsPool : MonoBehaviour
    {
        private static BulletsPool instance;
        private Dictionary<string, ObjectPool<BulletEntity>> bulletPools = new Dictionary<string, ObjectPool<BulletEntity>>();

        public static BulletsPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<BulletsPool>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject("BulletsPool");
                        instance = obj.AddComponent<BulletsPool>();
                    }
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        public BulletEntity InstBullet(GameObject bulletPrefab)
        {
            string bulletName = bulletPrefab.name.Replace("(Clone)", "").Trim();

            if (!bulletPools.ContainsKey(bulletName))
            {
                // êVÇµÇ¢ÉvÅ[ÉãÇçÏê¨
                bulletPools[bulletName] = new ObjectPool<BulletEntity>(
                    createFunc: () => CreateBullet(bulletPrefab),
                    actionOnGet: (b) => OnGetBullet(b),
                    actionOnRelease: (b) => OnReleaseBullet(b),
                    actionOnDestroy: (b) => DestroyBullet(b),
                    collectionCheck: true,
                    defaultCapacity: 10,
                    maxSize: 1000
                );
            }

            return bulletPools[bulletName].Get();
        }

        private BulletEntity CreateBullet(GameObject bulletPrefab)
        {
            if (bulletPrefab.GetComponent<BulletEntity>() == null)
            {
                return Instantiate(bulletPrefab, transform).AddComponent<BulletEntity>();
            }
            return Instantiate(bulletPrefab, transform).GetComponent<BulletEntity>();
        }

        private void OnGetBullet(BulletEntity bullet)
        {
            bullet.gameObject.SetActive(true);
            bullet.Initialize(() => bulletPools[bullet.gameObject.name.Replace("(Clone)", "").Trim()].Release(bullet));
        }

        private void OnReleaseBullet(BulletEntity bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        private void DestroyBullet(BulletEntity bullet)
        {
            Destroy(bullet.gameObject);
        }
    }
}