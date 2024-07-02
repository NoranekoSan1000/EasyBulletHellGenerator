using System;
using UnityEditor;
using UnityEngine;

namespace EasyBulletHellGenerator
{
    [CustomEditor(typeof(BulletPattern))]
    public class BulletPatternEditor : Editor
    {
        private Texture2D image;

        public override void OnInspectorGUI()
        {
            BulletPattern bulletPattern = (BulletPattern)target;

            EditorGUILayout.LabelField("弾のオブジェクト", EditorStyles.boldLabel);
            bulletPattern.bulletObject = (GameObject)EditorGUILayout.ObjectField("bulletObject", bulletPattern.bulletObject, typeof(GameObject), true);

            EditorGUILayout.LabelField("オブジェクトの向き", EditorStyles.boldLabel);
            bulletPattern.objDirection = (BulletsManager.ObjectDirection)EditorGUILayout.EnumPopup("ObjDirection", bulletPattern.objDirection);

            // Enumを表示して変更を検知
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.LabelField("発射形態", EditorStyles.boldLabel);
            bulletPattern.formation = (BulletsManager.ShotFormation)EditorGUILayout.EnumPopup("Formation", bulletPattern.formation);
            if (EditorGUI.EndChangeCheck())
            {
                // Enumの変更があった場合に画像を更新
                image = null;
            }

            // 画像表示
            viewImage(bulletPattern.formation.ToString());

            EditorGUILayout.LabelField("初期位置のズレ", EditorStyles.boldLabel);
            bulletPattern.positionOffset = EditorGUILayout.Vector3Field("PositionOffset", bulletPattern.positionOffset);

            EditorGUILayout.LabelField("実行回数", EditorStyles.boldLabel);
            bulletPattern.executionCount = EditorGUILayout.IntField("Execution Count", bulletPattern.executionCount);

            EditorGUILayout.LabelField("実行間隔", EditorStyles.boldLabel);
            bulletPattern.interval = EditorGUILayout.FloatField("Interval", bulletPattern.interval);

            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("---弾の挙動設定---", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("誘導", EditorStyles.boldLabel);
            bulletPattern.isMissile = EditorGUILayout.Toggle("Is Missile", bulletPattern.isMissile);

            EditorGUILayout.LabelField("初速度", EditorStyles.boldLabel);
            bulletPattern.speed = EditorGUILayout.FloatField("Speed", bulletPattern.speed);

            EditorGUILayout.LabelField("加速度", EditorStyles.boldLabel);
            bulletPattern.acceleration = EditorGUILayout.FloatField("Acceleration", bulletPattern.acceleration);

            EditorGUILayout.LabelField("存在時間", EditorStyles.boldLabel);
            bulletPattern.existTime = EditorGUILayout.FloatField("Exist Time", bulletPattern.existTime);

            EditorGUILayout.LabelField("重力設定", EditorStyles.boldLabel);
            bulletPattern.setGravity = EditorGUILayout.Toggle("Set Gravity", bulletPattern.setGravity);

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup(!bulletPattern.setGravity);
            EditorGUILayout.LabelField("重力開始時間", GUILayout.Width(100), GUILayout.Height(20));
            bulletPattern.setGravityTime = EditorGUILayout.FloatField(bulletPattern.setGravityTime, GUILayout.Width(50));
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("---弾幕設定---", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);

            EditorGUILayout.LabelField("１実行で発射する弾数", EditorStyles.boldLabel);
            bulletPattern.numBullets = EditorGUILayout.IntField("Num Bullets", bulletPattern.numBullets);

            if (bulletPattern.formation == BulletsManager.ShotFormation.SpreadShot)
            {
                EditorGUILayout.LabelField("拡散角度", EditorStyles.boldLabel);
                bulletPattern.spreadAngle = EditorGUILayout.FloatField("Spread Angle", bulletPattern.spreadAngle);
            }
            else if (bulletPattern.formation == BulletsManager.ShotFormation.SiegeShot)
            {
                EditorGUILayout.LabelField("包囲半径", EditorStyles.boldLabel);
                bulletPattern.siegeRadius = EditorGUILayout.FloatField("Siege Radius", bulletPattern.siegeRadius);
            }

            EditorUtility.SetDirty(bulletPattern);
        }

        private void viewImage(string fileName)
        {
            // 画像の読み込みと表示
            if (image == null)
            {
                string[] guids = AssetDatabase.FindAssets(fileName);
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    image = AssetDatabase.LoadMainAssetAtPath(path) as Texture2D;
                    if (image != null)
                    {
                        break;
                    }
                }
            }

            if (image != null)
            {
                const float maxLogoWidth = 430.0f;
                EditorGUILayout.Separator();
                float w = EditorGUIUtility.currentViewWidth;
                Rect r = new Rect();
                r.width = Math.Min(w - 40.0f, maxLogoWidth);
                r.height = r.width / 4f;
                Rect r2 = GUILayoutUtility.GetRect(r.width, r.height);
                r.x = ((EditorGUIUtility.currentViewWidth - r.width) * 0.5f) - 4.0f;
                r.y = r2.y;
                GUI.DrawTexture(r, image, ScaleMode.StretchToFill);
                EditorGUILayout.Separator();
            }
        }
    }
}