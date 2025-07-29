using UnityEngine;
using UnityEngine.EventSystems;

public class Animation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Animator animator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetTrigger("Hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetTrigger("Normal");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        animator.SetTrigger("Pressed");
    }
}

