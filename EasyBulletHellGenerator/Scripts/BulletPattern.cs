using UnityEngine;

namespace EasyBulletHellGenerator
{
    [CreateAssetMenu(fileName = "NewBulletPattern", menuName = "Bullet Pattern")]
    public class BulletPattern : ScriptableObject
    {
        public BulletsManager.ShotFormation formation;
        public GameObject bulletObject;
        public Vector3 positionOffset;
        public bool setGravity;
        public float setGravityTime;
        public BulletsManager.ObjectDirection objDirection;
        public bool isMissile;
        public float speed;
        public float acceleration;
        public float existTime;
        public int executionCount;
        public float interval;
        public int numBullets;
        public float spreadAngle;
        public float siegeRadius;

    }
}