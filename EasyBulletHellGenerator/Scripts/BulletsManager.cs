using UnityEngine;

namespace EasyBulletHellGenerator
{
    public class BulletsManager : MonoBehaviour
    {
        public enum ShotFormation
        {
            StraightShot = 100,
            ExplosionShot = 200,
            SpreadShot = 300,
            SiegeShot = 400,
        } //直線弾、爆発弾、拡散弾、包囲弾
        private ShotFormation formation = ShotFormation.StraightShot; //発射形態
        private GameObject bullet;
        private GameObject target;
        private float interval; //射撃間隔
        private int executionCount; //実行回数
        private Vector3 direction;//移動の向き
        private ObjectDirection objDirection; //オブジェクトの向き
        public enum ObjectDirection
        {
            Front = 100,
            Back = 200,
            Left = 300,
            Right = 400,
            Up = 500,
            Down = 600,
            Direction_of_movement = 700, //進行方向
        }
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
        public void InitialBulletStatus(bool ismissile, float speed, float acceleration, float existtime, ObjectDirection objdirection)
        {
            isMissile = ismissile;
            this.speed = speed;
            this.acceleration = acceleration;
            existTime = existtime;
            objDirection = objdirection;

            this.direction = Vector3.back;
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
        public void StraightShot(int executioncount, float interval)
        {
            executionCount = executioncount;
            this.interval = interval;
            formation = ShotFormation.StraightShot;
            numBullets = 1;
        }

        /// <summary> 爆発弾 </summary>
        public void ExplosionShot(int executioncount, float interval, int numbullets)
        {
            executionCount = executioncount;
            this.interval = interval;
            formation = ShotFormation.ExplosionShot;
            numBullets = numbullets;
        }

        /// <summary> 拡散弾 </summary>
        public void SpreadShot(int executioncount, float interval, float spreadangle, int numbullets){
            executionCount = executioncount;
            this.interval = interval;
            formation = ShotFormation.SpreadShot;
            spreadAngle = spreadangle;
            numBullets = numbullets;
        }

        /// <summary> 包囲弾 </summary>
        public void SiegeShot(int executioncount, float interval, float radius, int numbullets)
        {
            executionCount = executioncount;
            this.interval = interval;
            formation = ShotFormation.SiegeShot;
            numBullets = numbullets;
            siegeRadius = radius;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary> 追加設定　初期角度X変更 </summary>
        public BulletsManager SetAngleOffsetX(float angle)
        {
            rotationOffset = Quaternion.AngleAxis(angle, Vector3.right) * rotationOffset;
            return this;
        }

        /// <summary> 追加設定　初期角度Y変更 </summary>
        public BulletsManager SetAngleOffsetY(float angle)
        {
            rotationOffset = Quaternion.AngleAxis(angle, Vector3.up) * rotationOffset;
            return this;
        }

        /// <summary> 追加設定　初期角度Z変更 </summary>
        public BulletsManager SetAngleOffsetZ(float angle)
        {
            rotationOffset = Quaternion.AngleAxis(angle, Vector3.forward) * rotationOffset;
            return this;
        }

        /// <summary> 追加設定　初期位置変更 </summary>
        public BulletsManager SetPositionOffset(Vector3 addpos)
        {
            positionOffset = addpos;
            return this;
        }

        /// <summary> 追加設定　1発ごとに回転 </summary>
        public BulletsManager SetRotate(float angle)
        {
            rotationIncrement = angle;
            return this;
        }

        /// <summary> 追加設定　指定秒数後ターゲットへ向き変更 </summary>
        public BulletsManager SetChangeTarget(GameObject newtarget, float changetime)
        {
            target = newtarget;
            changeTargetTime = changetime;
            return this;
        }

        /// <summary> 追加設定　指定秒数後重力ON </summary>
        public BulletsManager SetChangeGravity(float changetime)
        {
            changeGravityTime = changetime;
            return this;
        }


        /// <summary> 追加設定　指定秒数後一時停止 </summary>
        public BulletsManager SetStartPauseTime(float startpausetime)
        {
            startpauseTime = startpausetime;
            return this;
        }

        /// <summary> 追加設定　指定秒数後一時停止終了 </summary>
        public BulletsManager SetEndPauseTime(float endpausetime)
        {
            endPauseTime = endpausetime;
            return this;
        }

        /// <summary> 追加設定　全弾発射後一時停止終了 </summary>
        public BulletsManager SetEndPauseAllFire()
        {
            endPauseAllFire = true;
            return this;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void straightShot()
        {
            BulletEntity shotA = BulletsPool.Instance.InstBullet(bullet);
            shotA.Initialize(direction, transform.position + positionOffset, isMissile, target, speed,
            acceleration, existTime, changeTargetTime, changeGravityTime, startpauseTime, endPauseTime, objDirection);
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

                    BulletEntity shotA = BulletsPool.Instance.InstBullet(bullet);
                    shotA.Initialize(newDirection, transform.position + positionOffset, isMissile, target, speed,
                      acceleration, existTime, changeTargetTime, changeGravityTime, startpauseTime, endPauseTime, objDirection);
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

                    BulletEntity shotA = BulletsPool.Instance.InstBullet(bullet);
                    shotA.Initialize(newDirection, transform.position + positionOffset, isMissile, target,
                    speed, acceleration, existTime, changeTargetTime, changeGravityTime, startpauseTime, endPauseTime, objDirection);
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

                BulletEntity shotA = BulletsPool.Instance.InstBullet(bullet);
                shotA.Initialize((target.transform.position - siegePosition).normalized, siegePosition + positionOffset, isMissile, target,
                speed, acceleration, existTime, changeTargetTime, changeGravityTime, startpauseTime, endPauseTime, objDirection);
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
}
