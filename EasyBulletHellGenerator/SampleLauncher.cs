using UnityEngine;
using EasyBulletHellGenerator;

public class SampleLauncher : BulletsLauncher
{
    [SerializeField] private BulletPattern explosionA;
    [SerializeField] private BulletPattern explosionB;
    [SerializeField] private BulletPattern spreadA;
    [SerializeField] private BulletPattern spreadB;
    [SerializeField] private BulletPattern spreadC;
    [SerializeField] private BulletPattern siegeA;

    private void Update()
    {
        Debug.Log("Input:J Shot Test");

        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha1))
        {
            float[] angleOffsetsX = { 0, 15, 30, 45, 60, 75, 90, 105, 120, 135, 150, 165 };
            foreach (float angleOffset in angleOffsetsX)
            {
                GenerateBulletHell(explosionA).SetRotate(17).SetAngleOffsetX(angleOffset);
            }
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            GenerateBulletHell(spreadA).SetPositionOffset(new Vector3(0, 15, 0)).SetAngleOffsetX(90)
                .SetRotate(8).SetChangeTarget(targetObject, 2.25f);
            GenerateBulletHell(spreadA).SetPositionOffset(new Vector3(0, 15, 0)).SetAngleOffsetX(90)
                .SetAngleOffsetZ(180).SetRotate(8).SetChangeTarget(targetObject, 2.25f);
            GenerateBulletHell(spreadA).SetPositionOffset(new Vector3(0, 15, 0)).SetAngleOffsetX(90)
                .SetAngleOffsetY(270).SetRotate(8).SetChangeTarget(targetObject, 2.25f);
            GenerateBulletHell(spreadA).SetPositionOffset(new Vector3(0, 15, 0)).SetAngleOffsetX(90)
                .SetAngleOffsetY(90).SetAngleOffsetZ(180).SetRotate(8).SetChangeTarget(targetObject, 2.25f);
            GenerateBulletHell(spreadA).SetPositionOffset(new Vector3(0, 15, 0)).SetRotate(8)
                .SetChangeTarget(targetObject, 2.25f);
            GenerateBulletHell(spreadA).SetPositionOffset(new Vector3(0, 15, 0)).SetAngleOffsetY(180)
                .SetRotate(8).SetChangeTarget(targetObject, 2.25f);
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha3))
        {
            GenerateBulletHell(siegeA).SetAngleOffsetX(90).SetRotate(15).SetPositionOffset(new Vector3(0, 8, 0))
                .SetChangeGravity(2.0f);
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha4))
        {
            GenerateBulletHell(spreadB).SetPositionOffset(new Vector3(0, 15, 0)).SetAngleOffsetZ(-45).SetRotate(9)
                .SetChangeTarget(targetObject, 0.4f).SetStartPauseTime(0.4f).SetEndPauseAllFire();
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha5))
        {
            GenerateBulletHell(explosionB).SetRotate(18).SetStartPauseTime(1f).SetEndPauseAllFire();
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha6))
        {
            GenerateBulletHell(explosionA).SetPositionOffset(new Vector3(0, 15, 0)).SetRotate(15).SetAngleOffsetZ(60);
            GenerateBulletHell(explosionA).SetPositionOffset(new Vector3(0, 15, 0)).SetRotate(15);
            GenerateBulletHell(explosionA).SetPositionOffset(new Vector3(0, 15, 0)).SetRotate(15).SetAngleOffsetX(60);
            GenerateBulletHell(explosionA).SetPositionOffset(new Vector3(0, 15, 0)).SetRotate(15).SetAngleOffsetX(120);
            GenerateBulletHell(explosionA).SetPositionOffset(new Vector3(0, 15, 0)).SetRotate(15).SetAngleOffsetZ(120);
        }
        if (Input.GetKey(KeyCode.J) && Input.GetKeyDown(KeyCode.Alpha7))
        {
            GenerateBulletHell(spreadC).SetPositionOffset(new Vector3(30, 15, 0)).SetRotate(-31).SetAngleOffsetX(90).SetAngleOffsetZ(-75)
                .SetStartPauseTime(2.2f).SetEndPauseTime(2.7f).SetChangeTarget(targetObject, 2.5f);
            GenerateBulletHell(spreadC).SetPositionOffset(new Vector3(-30, 15, 0)).SetRotate(31).SetAngleOffsetX(90).SetAngleOffsetZ(75)
                .SetStartPauseTime(2.2f).SetEndPauseTime(2.7f).SetChangeTarget(targetObject, 2.5f);
        }
    }
}
