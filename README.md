# EasyBulletHellGenerator ver1.2
3D Unityゲーム制作向けの弾幕生成アセット【かんたん弾幕ジェネレーター】  
誰でも1分未満で簡単に、あらゆる形、あらゆる動きの弾の雨を降らせるようになります。

![sc1](https://github.com/NoranekoSan1000/EasyBulletHellGenerater/blob/main/img/sc1.png)

  
## How to use
Hierarchyタブで[発射用のオブジェクト]をCreateしてください。  

ProjectタブでScriptableObject:Bullet PatternをCreateしてください。  
ここで弾の挙動の設定や弾幕の設定を行えます。  

[発射用のオブジェクト]にアタッチするスクリプトを作成してください。  
スクリプトには'using EasyBulletHellGenerator;'を追加し、'BulletsLauncher'クラスを継承してください。  
'[SerializeField] private BulletPattern bp;'のように、先ほど作成したBullet Patternをアタッチできるようにしてください。  
Update関数内でキー入力待機等ができるようにし、その中に'GenerateBulletHell()'を追加してください。  
これで先ほど作成したBullet Patternの弾幕が発射できるようになります。  

'GenerateBulletHell()'はメソッドチェーンを使うことで、さらに機能を追加できます。  
例えば、'GenerateBulletHell(bp).SetPositionOffset(new Vector3(0, 15, 0));'と書けば、  
発射位置がy軸上方向に15離れたところから発射されるようになります。  
他の機能については、下の（メソッドチェーンで追加できる設定）を見てください。  

## 弾幕の種類  


### 直線弾  
StraightShot(int executioncount, float interval, Vector3 direction);  
executioncount...実行回数  
interval...射撃間隔  
direction...初期角度(不要説)  

![sc2](https://github.com/NoranekoSan1000/EasyBulletHellGenerater/blob/main/img/sc2.png)

### SpreadShot() 拡散弾  
SpreadShot(int executioncount, float interval, float spreadangle, Vector3 direction, int numbullets);  
executioncount...実行回数  
interval...射撃間隔  
spreadangle...拡散角度  
direction...初期角度(不要説)  
numbullets...1回の射撃で放たれる弾数 

![sc3](https://github.com/NoranekoSan1000/EasyBulletHellGenerater/blob/main/img/sc3.png)

### ExplosionShot() 爆発弾  
ExplosionShot(int executioncount, float interval, Vector3 direction, int numbullets);  
executioncount...実行回数  
interval...射撃間隔  
direction...初期角度(不要説)  
numbullets...1回の射撃で放たれる弾数  

![sc4](https://github.com/NoranekoSan1000/EasyBulletHellGenerater/blob/main/img/sc4.png)

### SiegeShot() 包囲弾  
SiegeShot(int executioncount, float interval, float radius, Vector3 direction, int numbullets);  
executioncount...実行回数  
interval...射撃間隔  
radius...包囲される半径  
direction...初期角度(不要説)  
numbullets...1回の射撃で放たれる弾数  

![sc5](https://github.com/NoranekoSan1000/EasyBulletHellGenerater/blob/main/img/sc5.png)

  
## メソッドチェーンで追加できる設定

### SetAngleOffsetX(float angle)  
初期角度Xを変更します

### SetAngleOffsetY(float angle)  
初期角度Yを変更します

### SetAngleOffsetZ(float angle)  
初期角度Zを変更します

### SetPositionOffset(Vector3 addpos)  
初期位置を変更します

### SetRotate(float angle)  
1発ごとに発射角度をずらします

### SetChangeTarget(GameObject newtarget, float changetime)  
指定秒数後ターゲットへ向き変更

### SetChangeGravity(float changetime)  
指定秒数後重力ON

### SetStartPauseTime(float startpausetime)  
指定秒数後一時停止します

### SetEndPauseTime(float endpausetime)  
指定秒数後一時停止を終了します

### SetEndPauseAllFire()  
全弾発射後一時停止を終了します


## 更新情報
2024/06/29 ver1.1  
弾幕の追加設定をメソッドチェーンで付け足せるように。  
ScriptableObjectで弾の挙動設定を設定できるように。  
2024/06/30 ver1.2  
オブジェクトプールの仕様変更。ScriptableObjectで発射するオブジェクトのアタッチが可能に。
