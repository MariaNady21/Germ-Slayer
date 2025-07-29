using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] RectTransform playerInMap;
    [SerializeField] RectTransform map2dEnd;
    [SerializeField] Transform map3dParent;
    [SerializeField] Transform map3dEnd;

    [SerializeField] float iconSmoothSpeed = 5f; 

    private Vector3 normalized, mapped;

    private void Update()
    {
        normalized = Divide(
            map3dParent.InverseTransformPoint(this.transform.position),
            map3dEnd.position - map3dParent.position
        );

        normalized.y = normalized.z;
        mapped = Multiply(normalized, map2dEnd.localPosition);
        mapped.z = 0;

        
        playerInMap.localPosition = Vector3.Lerp(playerInMap.localPosition, mapped, Time.deltaTime * iconSmoothSpeed);
    }

    private static Vector3 Divide(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    private static Vector3 Multiply(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}