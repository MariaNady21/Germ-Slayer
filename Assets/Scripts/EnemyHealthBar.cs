using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] Slider healthSlider;
    public Transform target;
    public Vector3 offset;       

    public void SetHealth(float current, float max)
    {
        if (healthSlider != null)
        {
            healthSlider.value = current / max;
        }
    }
    public void Hide()
{
    gameObject.SetActive(false);
}

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(target.position + offset);
            GetComponent<RectTransform>().position = screenPos;
        }
    }
}

