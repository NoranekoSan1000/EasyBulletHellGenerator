# EasyBulletHellGenerater ver1.0
3D Unityゲーム制作向けの弾幕生成アセット【かんたん弾幕ジェネレーター】  
誰でも1分未満で簡単に、あらゆる形、あらゆる動きの弾の雨を降らせるようになります。

![sc1](https://github.com/NoranekoSan1000/EasyBulletHellGenerater/blob/main/img/sc1.png)

  
## 使い方
以下４つのオブジェクトを作成し、アタッチ作業を行ってください。

①[弾を発射するオブジェクト]にBulletsLauncher.csをアタッチ  
∟ BulletsManagerObject　に②をアタッチ  
∟ Bullet Object　に③をアタッチ  
∟ Target Object　に[任意の狙う対象オブジェクト]をアタッチ  
  
②[弾幕管理用オブジェクト] EmptyObject  

③[発射したい弾のオブジェクト]にBulletEntity.csをアタッチ  

④[オブジェクトプール用オブジェクト]にBulletObjectPool.csをアタッチ  
∟ Bullet Object　に③をアタッチ  

以下はBulletsLauncherの記述例です。    
private void Update()  
{  
　　if (Input.GetKey(KeyCode.J)) //Jキー入力時  
　　{  
　　　　BulletsManager s1 = explosionA();  
　　　　s1.SetRotate(10);  
　　}  
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


  
## 弾幕の種類  


### StraightShot() 直線弾

### SpreadShot() 拡散弾

### ExplosionShot() 爆発弾

### SiegeShot() 包囲弾　

  
## 拡張設定

### SetAngleOffsetX(float angle)

### SetAngleOffsetY(float angle)

### SetAngleOffsetZ(float angle)

### SetPositionOffset(Vector3 addpos)

### SetRotate(float angle)

### SetStartPauseTime(float startpausetime)

### SetEndPauseTime(float endpausetime)

### SetEndPauseAllFire()
