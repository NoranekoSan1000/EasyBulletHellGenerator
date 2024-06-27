# EasyBulletHellGenerater
3D Unityゲーム制作向けの弾幕生成アセット【かんたん弾幕ジェネレーター】  
誰でも1分未満で簡単に、あらゆる形、あらゆる動きの弾の雨を降らせるようになります。

![sc1](https://github.com/NoranekoSan1000/EasyBulletHellGenerater/blob/main/img/sc1.png)

  
## 使い方

・弾を発射するオブジェクトにBulletsLauncher.csをアタッチする
∟ BulletsLauncher.csをアタッチ

・BulletsManagerObject(弾幕管理用オブジェクト)

・BulletObject(発射したい弾のオブジェクト)  
∟ BulletEntity.csをアタッチ

・BulletObjectPool(EmptyObjectでよい)  
∟ BulletObjectPool.csをアタッチ

  
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
