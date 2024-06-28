using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletPattern", menuName = "Bullet Pattern")]
public class BulletPattern : ScriptableObject
{
    public BulletsManager.ShotFormation formation;
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
