using System;
using UnityEngine;

namespace EasyBulletHellGenerator
{
    public class BulletEntity : MonoBehaviour
    {
        private Action onDisable;

        private BulletsManager.ObjectDirection objDirection;
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
            float existtime, float changedirectiontime, float changegravitytime, float startpausetime, float endpausetime,
            BulletsManager.ObjectDirection objdirection)
        {
            objDirection = objdirection;
            transform.rotation = SetQuaternionFromObjectDirection(objdirection);
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
                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(directionToTarget);
                }
                    
            }
            else
            {
                // ターゲットの向きを変えるタイミングに達したら、ターゲットの方向に向かう
                if (!hasChangedDirection && elapsedTime >= changeDirectionTime && missileTarget != null)
                {
                    ChangeDirectionToTarget();
                }

                // ターゲットの方向に向かった後はターゲットの方向に進む
                transform.position += direction * currentSpeed * Time.deltaTime;
                if (hasChangedDirection && direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(direction);
                }
                else if (!hasChangedDirection && direction != Vector3.zero)
                {
                    transform.rotation = SetQuaternionFromObjectDirection(objDirection);
                }
            }
        }

        private void ChangeDirectionToTarget()
        {
            Vector3 directionToTarget = (missileTarget.transform.position - transform.position).normalized;
            direction = directionToTarget; // 方向をターゲットの方向に変更
            transform.rotation = Quaternion.LookRotation(directionToTarget);
            hasChangedDirection = true;
        }

        private Quaternion SetQuaternionFromObjectDirection(BulletsManager.ObjectDirection objdirection)
        {
            if(objdirection == BulletsManager.ObjectDirection.Front) return Quaternion.LookRotation(Vector3.forward);
            else if (objdirection == BulletsManager.ObjectDirection.Back) return Quaternion.LookRotation(Vector3.back);
            else if (objdirection == BulletsManager.ObjectDirection.Right) return Quaternion.LookRotation(Vector3.right);
            else if (objdirection == BulletsManager.ObjectDirection.Left) return Quaternion.LookRotation(Vector3.left);
            else if (objdirection == BulletsManager.ObjectDirection.Up) return Quaternion.LookRotation(Vector3.up);
            else if (objdirection == BulletsManager.ObjectDirection.Down) return Quaternion.LookRotation(Vector3.down);
            else if (objdirection == BulletsManager.ObjectDirection.Direction_of_movement) return Quaternion.LookRotation(direction);
            else return Quaternion.LookRotation(Vector3.forward);

        }

    }
}