using UnityEngine;

public class BulletsLauncher : MonoBehaviour
{
    [SerializeField] private BulletPattern explosionA;
    [SerializeField] private BulletPattern explosionB;
    [SerializeField] private BulletPattern spreadA;
    [SerializeField] private BulletPattern spreadB;
    [SerializeField] private BulletPattern spreadC;
    [SerializeField] private BulletPattern siegeA;
    [SerializeField] private GameObject bulletsManagerObject;
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private GameObject targetObject;  

    private void Update()
    {
        Debug.Log("Input:J Shot Test");
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            float[] angleOffsetsX = { 0, 15, 30, 45, 60, 75, 90, 105, 120, 135, 150, 165 };
            foreach (float angleOffset in angleOffsetsX)
            {
                CreateAndConfigureBulletsManager(explosionA).SetRotate(17).SetAngleOffsetX(angleOffset);
            }
        }
        if(Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            CreateAndConfigureBulletsManager(spreadA).SetPositionOffset(new Vector3(0, 15, 0)).SetAngleOffsetX(90)
                .SetRotate(8).SetChangeTarget(targetObject, 2.25f);
            CreateAndConfigureBulletsManager(spreadA).SetPositionOffset(new Vector3(0, 15, 0)).SetAngleOffsetX(90)
                .SetAngleOffsetZ(180).SetRotate(8).SetChangeTarget(targetObject, 2.25f);
            CreateAndConfigureBulletsManager(spreadA).SetPositionOffset(new Vector3(0, 15, 0)).SetAngleOffsetX(90)
                .SetAngleOffsetY(270).SetRotate(8).SetChangeTarget(targetObject, 2.25f);
            CreateAndConfigureBulletsManager(spreadA).SetPositionOffset(new Vector3(0, 15, 0)).SetAngleOffsetX(90)
                .SetAngleOffsetY(90).SetAngleOffsetZ(180).SetRotate(8).SetChangeTarget(targetObject, 2.25f);
            CreateAndConfigureBulletsManager(spreadA).SetPositionOffset(new Vector3(0, 15, 0)).SetRotate(8)
                .SetChangeTarget(targetObject, 2.25f);
            CreateAndConfigureBulletsManager(spreadA).SetPositionOffset(new Vector3(0, 15, 0)).SetAngleOffsetY(180)
                .SetRotate(8).SetChangeTarget(targetObject, 2.25f);
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            CreateAndConfigureBulletsManager(siegeA).SetAngleOffsetX(90).SetRotate(15).SetPositionOffset(new Vector3(0, 8, 0))
                .SetChangeGravity(2.0f);
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            CreateAndConfigureBulletsManager(spreadB).SetPositionOffset(new Vector3(0, 15, 0)).SetAngleOffsetZ(-45).SetRotate(9)
                .SetChangeTarget(targetObject, 0.4f).SetStartPauseTime(0.4f).SetEndPauseAllFire();
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            CreateAndConfigureBulletsManager(explosionB).SetRotate(18).SetStartPauseTime(1f).SetEndPauseAllFire();
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha6))
        {
            CreateAndConfigureBulletsManager(explosionA).SetPositionOffset(new Vector3(0, 15, 0)).SetRotate(15).SetAngleOffsetZ(60);
            CreateAndConfigureBulletsManager(explosionA).SetPositionOffset(new Vector3(0, 15, 0)).SetRotate(15);
            CreateAndConfigureBulletsManager(explosionA).SetPositionOffset(new Vector3(0, 15, 0)).SetRotate(15).SetAngleOffsetX(60);
            CreateAndConfigureBulletsManager(explosionA).SetPositionOffset(new Vector3(0, 15, 0)).SetRotate(15).SetAngleOffsetX(120);
            CreateAndConfigureBulletsManager(explosionA).SetPositionOffset(new Vector3(0, 15, 0)).SetRotate(15).SetAngleOffsetZ(120);
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha7))
        {
            CreateAndConfigureBulletsManager(spreadC).SetPositionOffset(new Vector3(30, 15, 0)).SetRotate(-31).SetAngleOffsetX(90).SetAngleOffsetZ(-75)
                .SetStartPauseTime(2.2f).SetEndPauseTime(3.5f).SetChangeTarget(targetObject, 2.5f);
            CreateAndConfigureBulletsManager(spreadC).SetPositionOffset(new Vector3(-30, 15, 0)).SetRotate(31).SetAngleOffsetX(90).SetAngleOffsetZ(75)
                .SetStartPauseTime(2.2f).SetEndPauseTime(3.5f).SetChangeTarget(targetObject, 2.5f);
        }
    }

    private BulletsManager CreateAndConfigureBulletsManager(BulletPattern bulletpattern)
    {
        GameObject bullets = Instantiate(bulletsManagerObject, transform.position, transform.rotation);
        BulletsManager s = bullets.AddComponent<BulletsManager>();
        s.Initialize(bulletObject, targetObject); // ÉIÉuÉWÉFÉNÉgê›íË
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
