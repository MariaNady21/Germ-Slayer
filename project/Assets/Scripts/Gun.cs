using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Transform firePoint;
    [SerializeField] LineRenderer lineRenderer;

    [Header("Settings")]
    [SerializeField] float fireDistance = 100f;
    [SerializeField] float lineDuration = 0.02f;

    [Header("VFX")]
    [SerializeField] GameObject hitVFX;


    [Header("Input System")]
    [SerializeField] InputActionAsset inputActions;

    private InputAction shootAction;
    private float lineTimer = 0f;

    void Start()
    {
        
        shootAction = inputActions.FindAction("Player/Shoot");
        shootAction.performed += ctx => Fire();
        shootAction.Enable();

        lineRenderer.enabled = false;
    }

    void Update()
    {
        // مؤقت إخفاء الخط
        if (lineRenderer.enabled)
        {
            lineTimer -= Time.deltaTime;
            if (lineTimer <= 0f)
                lineRenderer.enabled = false;
        }
    }

    void Fire()
    {
        SoundManager soundManager = FindAnyObjectByType<SoundManager>();
        if (soundManager != null)
        {
            soundManager.PlaySound("GunShot");
        }

        Vector3 direction = firePoint.forward;
        Vector3 start = firePoint.position;
        Vector3 end = start + direction * fireDistance;

        RaycastHit hit;  // عرفه هنا حتى تستخدمه في كل المكان

        if (Physics.Raycast(start, direction, out hit, fireDistance))
        {
            end = hit.point;
            // لوخبط ف اى object
            if (hitVFX != null)
            {
                // الدوران بحيث يواجه السطح
                Quaternion rot = Quaternion.LookRotation(hit.normal);
                Instantiate(hitVFX, hit.point, rot);
            }

            if (hit.collider.CompareTag("Target"))
            {
                EnemyDeath enemy = hit.collider.GetComponent<EnemyDeath>();

                if (enemy != null)
                {
                    enemy.TakeDamage(1);
                  
                    if (soundManager != null)
                    {
                        soundManager.PlaySound("EnemyHit");
                    }
                }
                
            }
        }

        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        lineRenderer.enabled = true;
        lineTimer = lineDuration;
    }

}
