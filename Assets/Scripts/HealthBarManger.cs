using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject healthBarPrefab;  // prefab لشريط الصحة (UI slider)

    [SerializeField] Canvas canvas;  // الكانفس اللى هيتضاف عليه HealthBars

    [Header("Settings")]
    [SerializeField] Vector3 offset = new Vector3(0, 2, 0);  // تعويض عن رأس العدو

    [Header("Enemies")]
    [SerializeField] List<Transform> enemies;  // هنا تضيفي كل اهدافك (الاعداء)

    private List<GameObject> healthBars = new List<GameObject>();

    void Start()
    {
        // اعمل HealthBar لكل عدو واحتفظ بيه
        foreach (var enemy in enemies)
        {
            GameObject hb = Instantiate(healthBarPrefab, canvas.transform);
            EnemyHealthBar ehb = hb.GetComponent<EnemyHealthBar>();
            if (ehb != null)
            {
                ehb.target = enemy;
                ehb.offset = offset;
            }
            healthBars.Add(hb);
        }
    }

    void LateUpdate()
    {
        // ممكن تعمل تحديث مخصص لو عايزة مثلاً تتحكم في ظهور HealthBars
        // أو لو في حاجة اضافية حابة تضيفيها
    }
}

