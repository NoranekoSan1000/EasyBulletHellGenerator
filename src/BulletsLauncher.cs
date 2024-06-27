using UnityEngine;

public class BulletsLauncher : MonoBehaviour
{
    [SerializeField] private GameObject bulletsManagerObject;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private GameObject targetObject;  

    private void Update()
    {
        Debug.Log("Input:J Shot Test");
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            BulletsManager s1 = explosionA();
            s1.SetRotate(10);
            BulletsManager s2 = explosionA();
            s2.SetRotate(10);
            s2.SetAngleOffsetX(30);
            BulletsManager s3 = explosionA();
            s3.SetRotate(10);
            s3.SetAngleOffsetX(60);
            BulletsManager s4 = explosionA();
            s4.SetRotate(10);
            s4.SetAngleOffsetX(90);
            BulletsManager s5 = explosionA();
            s5.SetRotate(10);
            s5.SetAngleOffsetX(120);
            BulletsManager s6 = explosionA();
            s6.SetRotate(10);
            s6.SetAngleOffsetX(150);
        }
        if(Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            BulletsManager s1 = spreadA();
            s1.SetPositionOffset(new Vector3(0, 15, 0));
            s1.SetAngleOffsetX(90);
            s1.SetRotate(8);
            s1.SetChangeTarget(targetObject, 2.25f);
            BulletsManager s2 = spreadA();
            s2.SetPositionOffset(new Vector3(0, 15, 0));
            s2.SetAngleOffsetX(90);
            s2.SetAngleOffsetZ(180);
            s2.SetRotate(8);
            s2.SetChangeTarget(targetObject, 2.25f);
            BulletsManager s3 = spreadA();
            s3.SetPositionOffset(new Vector3(0, 15, 0));
            s3.SetAngleOffsetX(90);
            s3.SetAngleOffsetY(270);
            s3.SetRotate(8);
            s3.SetChangeTarget(targetObject, 2.25f);
            BulletsManager s4 = spreadA();
            s4.SetPositionOffset(new Vector3(0, 15, 0));
            s4.SetAngleOffsetX(90);
            s4.SetAngleOffsetY(90);
            s4.SetAngleOffsetZ(180);
            s4.SetRotate(8);
            s4.SetChangeTarget(targetObject, 2.25f);
            BulletsManager s5 = spreadA();
            s5.SetPositionOffset(new Vector3(0, 15, 0));
            s5.SetRotate(8);
            s5.SetChangeTarget(targetObject, 2.25f);
            BulletsManager s6 = spreadA();
            s6.SetPositionOffset(new Vector3(0, 15, 0));
            s6.SetAngleOffsetY(180);
            s6.SetRotate(8);
            s6.SetChangeTarget(targetObject, 2.25f);

        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            BulletsManager s1 = siegeA();
            s1.SetAngleOffsetX(90);
            s1.SetRotate(15);
            s1.SetPositionOffset(new Vector3(0, 8, 0));
            s1.SetChangeGravity(2.0f);
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            BulletsManager s1 = spreadB();
            s1.SetPositionOffset(new Vector3(0, 15, 0));
            s1.SetAngleOffsetZ(-45);
            s1.SetRotate(9);
            s1.SetChangeTarget(targetObject, 0.4f);
            s1.SetStartPauseTime(0.4f);
            s1.SetEndPauseAllFire();
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            BulletsManager s1 = explosionB();
            s1.SetRotate(18);
            s1.SetStartPauseTime(1f);
            s1.SetEndPauseAllFire();
        }
    }

    private BulletsManager straightA() //直線弾Sample
    {
        GameObject bullets = Instantiate(bulletsManagerObject, transform.position, transform.rotation);
        BulletsManager s = bullets.AddComponent<BulletsManager>();
        s.Initialize(bulletObject, targetObject); //オブジェクト設定
        s.InitialBulletStatus(true, 5f, 10f, 4f); //弾の挙動設定
        s.StraightShot(5, 0.2f, Vector3.back); //弾幕設定
        return s;
    }
    private BulletsManager explosionA() //爆発弾Sample
    {
        GameObject bullets = Instantiate(bulletsManagerObject, transform.position, transform.rotation);
        BulletsManager s = bullets.AddComponent<BulletsManager>();
        s.Initialize(bulletObject, targetObject); //オブジェクト設定
        s.InitialBulletStatus(false, -15f, 15f, 5f); //弾の挙動設定
        s.ExplosionShot(18, 0.15f, Vector3.back, 30); //弾幕設定
        return s;
    }
    private BulletsManager explosionB() //爆発弾Sample
    {
        GameObject bullets = Instantiate(bulletsManagerObject, transform.position, transform.rotation);
        BulletsManager s = bullets.AddComponent<BulletsManager>();
        s.Initialize(bulletObject, targetObject); //オブジェクト設定
        s.InitialBulletStatus(false, -15f, 20f, 6f); //弾の挙動設定
        s.ExplosionShot(20, 0.15f, Vector3.back, 2); //弾幕設定
        return s;
    }
    private BulletsManager spreadA() //拡散弾Sample
    {
        GameObject bullets = Instantiate(bulletsManagerObject, transform.position, transform.rotation);
        BulletsManager s = bullets.AddComponent<BulletsManager>();
        s.Initialize(bulletObject, targetObject); //オブジェクト設定
        s.InitialBulletStatus(false, -25f, 30f, 4f); //弾の挙動設定
        s.SpreadShot(50, 0.065f, 45f, Vector3.back, 4); //弾幕設定
        return s;
    }
    private BulletsManager spreadB() //拡散弾Sample
    {
        GameObject bullets = Instantiate(bulletsManagerObject, transform.position, transform.rotation);
        BulletsManager s = bullets.AddComponent<BulletsManager>();
        s.Initialize(bulletObject, targetObject); //オブジェクト設定
        s.InitialBulletStatus(false, 20f, 50f, 4f); //弾の挙動設定
        s.SpreadShot(10, 0.1f, 40f, Vector3.back, 5); //弾幕設定
        return s;
    }
    private BulletsManager siegeA() //包囲弾Sample
    {
        GameObject bullets = Instantiate(bulletsManagerObject, transform.position, transform.rotation);
        BulletsManager s = bullets.AddComponent<BulletsManager>();
        s.Initialize(bulletObject, targetObject); //オブジェクト設定
        s.InitialBulletStatus(false, 7f, 0, 5f); //弾の挙動設定
        s.SiegeShot(7, 0.5f, 20f, Vector3.back, 12); //弾幕設定 
        return s;
    }
}
