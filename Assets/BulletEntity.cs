using System;
using UnityEngine;

public class BulletEntity : MonoBehaviour
{
    private Action onDisable;  // 非アクティブ化するためのコールバック

    private Vector3 direction; //発射方向 o
    private bool isMissile; //誘導するか否か o
    private GameObject missileTarget; //誘導ターゲット o
    private float speed; //速度 o
    private float acceleration; //加速度 o
    private float existTime; //存在時間 o  
    private float changeDirectionTime;// ターゲット変更時間
    private float changeGravityTime;
    private float startPauseTime;//pause開始時間
    private float endPauseTime;//pause終了時間

    private float elapsedTime = 0f;
    private bool hasChangedDirection = false; // 方向を変えたかどうかのフラグ
    private bool isPaused = false; //一時停止中かどうかのフラグ

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void Initialize(Action ondisable)
    {
        onDisable = ondisable;
    }

    public void Initialize(Vector3 direction, Vector3 launchpos, bool ismissile, GameObject missiletarget, float speed, float acceleration, 
        float existtime, float changedirectiontime, float changegravitytime, float startpausetime, float endpausetime)
    {
        this.direction = direction;
        isMissile = ismissile;
        missileTarget = missiletarget;
        this.speed = speed;
        this.acceleration = acceleration;
        existTime = existtime;
        changeDirectionTime = changedirectiontime;
        changeGravityTime = changegravitytime;
        startPauseTime = startpausetime;
        endPauseTime = endpausetime;

        transform.position = launchpos;
        elapsedTime = 0f;
        hasChangedDirection = false;
        isPaused = false;

    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Update()
    {
        elapsedTime += Time.deltaTime;// 時間経過の追跡
        if (elapsedTime >= existTime)
        {
            onDisable?.Invoke();
            gameObject.SetActive(false);
        }

        if (elapsedTime >= startPauseTime && elapsedTime < endPauseTime) isPaused = true;
        else isPaused = false;
        if (isPaused) return;

        float currentSpeed = speed + acceleration * elapsedTime; // 速度の更新

        if (elapsedTime >= changeGravityTime && missileTarget != null)
        {
            direction += Vector3.down * 9.81f * Time.deltaTime; // 重力の影響を追加
        }     

        if (isMissile && missileTarget != null) //誘導弾
        {
            Vector3 directionToTarget = (missileTarget.transform.position - transform.position).normalized;
            transform.position += directionToTarget * currentSpeed * Time.deltaTime;
        }
        else
        {
            // ターゲットの向きを変えるタイミングに達したら、ターゲットの方向に向かう
            if (!hasChangedDirection && elapsedTime >= changeDirectionTime && missileTarget != null)
            {
                ChangeDirectionToTarget();
            }
            else
            {
                // ターゲットの方向に向かった後は初期の発射方向に進む
                transform.position += direction * currentSpeed * Time.deltaTime;
            }
        } 
    }

    private void ChangeDirectionToTarget()
    {
        Vector3 directionToTarget = (missileTarget.transform.position - transform.position).normalized;
        direction = directionToTarget; // 方向をターゲットの方向に変更
        transform.rotation = Quaternion.LookRotation(directionToTarget); // 弾の回転も変更 不要かも？
        hasChangedDirection = true;
    }
   
}