using UnityEngine;

public class BulletsManager : MonoBehaviour
{
    public enum ShotFormation { StraightShot, ExplosionShot, SpreadShot, SiegeShot, WaveShot } //直線弾、爆発弾、拡散弾、包囲弾、ウェーブ、（落下・追加予定）
    private ShotFormation formation = ShotFormation.StraightShot; //発射形態
    private GameObject bullet;
    private GameObject target;
    private float interval; //実行時間
    private int executionCount; //実行回数
    private Vector3 direction;
    private int numBullets;
    private float spreadAngle; //※拡散のみ
    private float siegeRadius; //※包囲のみ
    private float rotationIncrement; // 各発射時の角度増加量

    private Quaternion rotationOffset = Quaternion.identity;
    private Vector3 positionOffset = new Vector3(0, 0, 0);

    private bool isMissile; //誘導するか否か
    private float speed; //速度
    private float acceleration; //加速度
    private float existTime; //存在時間
    private float changeTargetTime;//ターゲット変更時間
    private float changeGravityTime;//重力ON時間
    private float startpauseTime;//pause開始時間
    private float endPauseTime;//pause終了時間

    private float elapsedTime = 999f;
    private int currentExecutionCount = 999;
    private float currentRotationAngle = 0;
    private bool endPauseAllFire;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        formation = ShotFormation.StraightShot;
        rotationOffset = Quaternion.identity;
        positionOffset = new Vector3(0, 0, 0);
        rotationIncrement = 0;
    }

    /// <summary> オブジェクト設定(必須) </summary>
    public void Initialize(GameObject bullet, GameObject target)
    {
        this.bullet = bullet;
        this.target = target;
    }

    /// <summary> 弾の挙動設定(必須) </summary>
    public void InitialBulletStatus(bool ismissile, float speed, float acceleration, float existtime)
    {
        isMissile = ismissile;
        this.speed = speed;
        this.acceleration = acceleration;
        existTime = existtime;

        changeTargetTime = 9999999999999;
        changeGravityTime = 9999999999999;
        startpauseTime = 9999999999999;
        endPauseTime = 9999999999999;
        elapsedTime = 0f;
        currentExecutionCount = 0;
        currentRotationAngle = 0;
        endPauseAllFire = false;
    }

    /// <summary> 直線弾 </summary>
    public void StraightShot(int executioncount, float interval, Vector3 direction)
    {
        executionCount = executioncount;
        this.interval = interval;
        formation = ShotFormation.StraightShot;
        this.direction = direction;
        numBullets = 1;
    }

    /// <summary> 爆発弾 </summary>
    public void ExplosionShot(int executioncount, float interval, Vector3 direction, int numbullets)
    {
        executionCount = executioncount;
        this.interval = interval;
        formation = ShotFormation.ExplosionShot;
        this.direction = direction;
        numBullets = numbullets;
    }

    /// <summary> 拡散弾 </summary>
    public void SpreadShot(int executioncount, float interval, float spreadangle, Vector3 direction, int numbullets)
    {
        executionCount = executioncount;
        this.interval = interval;
        formation = ShotFormation.SpreadShot;
        spreadAngle = spreadangle;
        this.direction = direction;
        numBullets = numbullets;
    }

    /// <summary> 包囲弾 </summary>
    public void SiegeShot(int executioncount, float interval, float radius, Vector3 direction, int numbullets)
    {
        executionCount = executioncount;
        this.interval = interval;
        formation = ShotFormation.SiegeShot;
        this.direction = direction;
        numBullets = numbullets;
        siegeRadius = radius;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /// <summary> 追加設定　初期角度X変更 </summary>
    public void SetAngleOffsetX(float angle)
    {
        rotationOffset = Quaternion.AngleAxis(angle, Vector3.right) * rotationOffset;
    }

    /// <summary> 追加設定　初期角度Y変更 </summary>
    public void SetAngleOffsetY(float angle)
    {
        rotationOffset = Quaternion.AngleAxis(angle, Vector3.up) * rotationOffset;
    }

    /// <summary> 追加設定　初期角度Z変更 </summary>
    public void SetAngleOffsetZ(float angle)
    {
        rotationOffset = Quaternion.AngleAxis(angle, Vector3.forward) * rotationOffset;
    }

    /// <summary> 追加設定　初期位置変更 </summary>
    public void SetPositionOffset(Vector3 addpos)
    {
        positionOffset = addpos;
    }

    /// <summary> 追加設定　1発ごとに回転 </summary>
    public void SetRotate(float angle)
    {
        rotationIncrement = angle;
    }

    /// <summary> 追加設定　指定秒数後ターゲットへ向き変更 </summary>
    public void SetChangeTarget(GameObject newtarget, float changetime)
    {
        target = newtarget;
        changeTargetTime = changetime;
    }

    /// <summary> 追加設定　指定秒数後重力ON </summary>
    public void SetChangeGravity(float changetime)
    {
        changeGravityTime = changetime;
    }


    /// <summary> 追加設定　指定秒数後一時停止 </summary>
    public void SetStartPauseTime(float startpausetime)
    {
        startpauseTime = startpausetime;
    }

    /// <summary> 追加設定　指定秒数後一時停止終了 </summary>
    public void SetEndPauseTime(float endpausetime)
    {
        endPauseTime = endpausetime;
    }

    /// <summary> 追加設定　全弾発射後一時停止終了 </summary>
    public void SetEndPauseAllFire()
    {
        endPauseAllFire = true;  
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void straightShot()
    {  
        BulletEntity shotA = BulletObjectPool.Instance.GetBullet();
        shotA.Initialize(direction, transform.position + positionOffset, isMissile, target, speed, 
            acceleration, existTime, changeTargetTime, changeGravityTime, startpauseTime, endPauseTime);
    }

    private void spreadShot()
    {

        if (numBullets == 1) straightShot();
        else
        {
            for (int i = 0; i < numBullets; i++)
            {
                var angle = -spreadAngle / 2f + i * (spreadAngle / (numBullets - 1)) + currentRotationAngle;
                var newDirection = Quaternion.AngleAxis(angle, Vector3.up) * direction;
                newDirection = rotationOffset * newDirection;

                BulletEntity shotA = BulletObjectPool.Instance.GetBullet();
                shotA.Initialize(newDirection, transform.position + positionOffset, isMissile, target, speed, 
                    acceleration, existTime, changeTargetTime, changeGravityTime, startpauseTime, endPauseTime);
            }

            // 次の発射時の角度増加量を設定
            currentRotationAngle += rotationIncrement;
        }
    }

    private void explosionShot()
    {
        float angleIncrement = 360f / numBullets;

        if (numBullets == 1) straightShot();
        else
        {
            for (int i = 0; i < numBullets; i++)
            {
                var angle = i * angleIncrement + currentRotationAngle;
                var newDirection = Quaternion.Euler(0, angle, 0) * direction;
                newDirection = rotationOffset * newDirection;

                BulletEntity shotA = BulletObjectPool.Instance.GetBullet();
                shotA.Initialize(newDirection, transform.position + positionOffset, isMissile, target, 
                    speed, acceleration, existTime, changeTargetTime, changeGravityTime, startpauseTime, endPauseTime);
            }

            // 次の発射時の角度増加量を設定
            currentRotationAngle += rotationIncrement;

        }
    }

    private void siegeShot()
    {
        float angleIncrement = 360f / numBullets;
        Vector3 baseOffset = Vector3.up * siegeRadius;

        for (int i = 0; i < numBullets; i++)
        {
            var angle = i * angleIncrement + currentRotationAngle;
            var offset = Quaternion.AngleAxis(angle, Vector3.forward) * baseOffset;
            offset = rotationOffset * offset;
            var siegePosition = target.transform.position + offset;

            BulletEntity shotA = BulletObjectPool.Instance.GetBullet();
            shotA.Initialize((target.transform.position - siegePosition).normalized, siegePosition + positionOffset, isMissile, target,
                speed, acceleration, existTime, changeTargetTime, changeGravityTime, startpauseTime, endPauseTime);
        }

        // 次の発射時の角度増加量を設定
        currentRotationAngle += rotationIncrement;

    }

    private void Update()
    {
        elapsedTime -= Time.deltaTime;
        if (elapsedTime <= 0f && currentExecutionCount < executionCount)
        {
            //一時停止終了時間を設定
            if (endPauseAllFire) endPauseTime = startpauseTime + interval * executionCount - interval * currentExecutionCount;

            if (formation == ShotFormation.StraightShot) straightShot();
            if (formation == ShotFormation.SpreadShot) spreadShot();
            if (formation == ShotFormation.ExplosionShot) explosionShot();
            if (formation == ShotFormation.SiegeShot) siegeShot();

            elapsedTime = interval;
            currentExecutionCount++;
        }

        if (currentExecutionCount >= executionCount)
        {
            Destroy(gameObject);
        }
    }

}

