using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BulletPattern))]
public class BulletPatternEditor : Editor
{
    private Texture2D image;

    public override void OnInspectorGUI()
    {
        BulletPattern bulletPattern = (BulletPattern)target;

        // EnumÇï\é¶ÇµÇƒïœçXÇåüím
        EditorGUI.BeginChangeCheck();
        bulletPattern.formation = (BulletsManager.ShotFormation)EditorGUILayout.EnumPopup("Formation", bulletPattern.formation);
        if (EditorGUI.EndChangeCheck())
        {
            // EnumÇÃïœçXÇ™Ç†Ç¡ÇΩèÍçáÇ…âÊëúÇçXêV
            image = null;
        }

        // âÊëúï\é¶
        viewImage(bulletPattern.formation.ToString());

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("---íeÇÃãììÆê›íË---", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField("óUì±", EditorStyles.boldLabel);
        bulletPattern.isMissile = EditorGUILayout.Toggle("Is Missile", bulletPattern.isMissile);

        EditorGUILayout.LabelField("èâë¨ìx", EditorStyles.boldLabel);
        bulletPattern.speed = EditorGUILayout.FloatField("Speed", bulletPattern.speed);

        EditorGUILayout.LabelField("â¡ë¨ìx", EditorStyles.boldLabel);
        bulletPattern.acceleration = EditorGUILayout.FloatField("Acceleration", bulletPattern.acceleration);

        EditorGUILayout.LabelField("ë∂ç›éûä‘", EditorStyles.boldLabel);
        bulletPattern.existTime = EditorGUILayout.FloatField("Exist Time", bulletPattern.existTime);

        EditorGUILayout.Space(20);
        EditorGUILayout.LabelField("---íeñãê›íË---", EditorStyles.boldLabel);
        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField("é¿çsâÒêî", EditorStyles.boldLabel);
        bulletPattern.executionCount = EditorGUILayout.IntField("Execution Count", bulletPattern.executionCount);

        EditorGUILayout.LabelField("é¿çsä‘äu", EditorStyles.boldLabel);
        bulletPattern.interval = EditorGUILayout.FloatField("Interval", bulletPattern.interval);

        EditorGUILayout.LabelField("ÇPé¿çsÇ≈î≠éÀÇ∑ÇÈíeêî", EditorStyles.boldLabel);
        bulletPattern.numBullets = EditorGUILayout.IntField("Num Bullets", bulletPattern.numBullets);

        if (bulletPattern.formation == BulletsManager.ShotFormation.SpreadShot)
        {
            EditorGUILayout.LabelField("ägéUäpìx", EditorStyles.boldLabel);
            bulletPattern.spreadAngle = EditorGUILayout.FloatField("Spread Angle", bulletPattern.spreadAngle);
        }
        else if (bulletPattern.formation == BulletsManager.ShotFormation.SiegeShot)
        {
            EditorGUILayout.LabelField("ïÔàÕîºåa", EditorStyles.boldLabel);
            bulletPattern.siegeRadius = EditorGUILayout.FloatField("Siege Radius", bulletPattern.siegeRadius);
        }

        EditorUtility.SetDirty(bulletPattern);
    }

    private void viewImage(string fileName)
    {
        // âÊëúÇÃì«Ç›çûÇ›Ç∆ï\é¶
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