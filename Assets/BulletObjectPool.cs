using UnityEngine;
using UnityEngine.Pool;

public class BulletObjectPool : MonoBehaviour
{
    private static BulletObjectPool instance;
    [SerializeField] private BulletEntity bulletObject;  // プールで管理するオブジェクト
    private ObjectPool<BulletEntity> bulletPool;

    public static BulletObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BulletObjectPool>();
            }
            return instance;
        }
    }

    private void Start()
    {
         bulletPool = new ObjectPool<BulletEntity>(
            createFunc: () => CreateBullet(),
            actionOnGet: (b) => GetBullet(b),
            actionOnRelease: (b) => OnBulletReleased(b),
            actionOnDestroy: (b) => DestroyBullet(b),
            collectionCheck: true,
            defaultCapacity: 3,
            maxSize: 1000
        );
    }
    
    public BulletEntity GetBullet() => bulletPool.Get(); // プールからbulletを取得

    public void ClearBullet() => bulletPool.Clear(); // プールの中身を空に

    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////

    // プールに入れるbulletのインスタンス
    private BulletEntity CreateBullet() => Instantiate(bulletObject, transform);

    private void GetBullet(BulletEntity bulletObject) // プールからbulletが取得されたときの処理
    {
        bulletObject.Initialize(() => bulletPool.Release(bulletObject));
        bulletObject.gameObject.SetActive(true);
    }
    
    private void OnBulletReleased(BulletEntity bulletObject) 
    {
        // プールにbulletが返却されたときの処理
    }

    private void DestroyBullet(BulletEntity bulletObject) // プールからbulletが削除されるときの処理
    {
        Destroy(bulletObject.gameObject);
    }
}
