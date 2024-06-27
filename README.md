# EasyBulletHellGenerater
3D Unityゲーム制作向けの弾幕生成アセット【かんたん弾幕ジェネレーター】  
誰でも1分未満で簡単に、あらゆる形、あらゆる動きの弾の雨を降らせるようになります。

![sc1](https://github.com/NoranekoSan1000/EasyBulletHellGenerater/blob/main/img/sc1.png)

  
## 使い方
以下４つのオブジェクトを作成し以下のようにアタッチ作業を行ってください。

①[弾を発射するオブジェクト]にBulletsLauncher.csをアタッチする。
∟ BulletsManagerObject　に②をアタッチ  
∟ Bullet Object　に③をアタッチ
∟ Target Object　に[任意の狙う対象オブジェクト]をアタッチ

②[弾幕管理用オブジェクト] EmptyObject

③[発射したい弾のオブジェクト]にBulletEntity.csをアタッチする。
∟ BulletEntity.csをアタッチ

④BulletObjectPool(EmptyObjectでよい)  
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
