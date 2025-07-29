using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float attackRange = 5f;    
    [SerializeField] float attackCooldown = 2f;
    [SerializeField] float stopDistance = 2f;
    [SerializeField] GameObject bulletPrefab;  
    [SerializeField] Transform bulletSpawnPoint;  
    [SerializeField] float bulletSpeed = 20f;
    [SerializeField] LayerMask playerLayer;

    [Header("VFX")]
    [SerializeField] GameObject hitVFX;


    private float lastAttackTime;



    void Update()
    {
        // اتأكد ان اللاعب موجود
        if (player == null) return;

        // اتجاه العدو Position اللاعب
        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);


        bool hasClearPath = !Physics.Raycast(transform.position, direction, distance, LayerMask.GetMask("Obstacle"));

        if (!hasClearPath)
        {
            // في حاجز بين العدو واللاعب، العدو لا يتحرك
            return;
        }


        transform.LookAt(player.position);

        //  حركة — بناءً على المسافة فقط
        if (distance > stopDistance)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
        }


        float fieldOfViewAngle = 90f; 
        // نحسب الزاوية بين اتجاه العدو والاتجاه لللاعب
        float angleToPlayer = Vector3.Angle(transform.forward, direction);
        //   عمل شعاع Raycast من موقع العدو للاتجاه اللي قدام

        if (distance <= attackRange && angleToPlayer <= fieldOfViewAngle  /2f)
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, attackRange, playerLayer))
            {
                if (hit.transform == player && Time.time - lastAttackTime > attackCooldown)
                {
                    Attack(hit.point, hit.normal);
                    lastAttackTime = Time.time;
                }

            }
        }

        
    }
    void Attack(Vector3 hitPoint, Vector3 hitNormal)
    {
    
        //  نقصان صحة اللاعب
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(1); // نقص 1 نقطة صحة
        }
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = bulletSpawnPoint.forward * bulletSpeed;
            }
            Destroy(bullet, 2f);     // ندمر الرصاصة و متفضلش ف المشهد


        }

        SoundManager soundManager = FindAnyObjectByType<SoundManager>();
        if (soundManager != null)
        {
            soundManager.PlaySound("EnemyShoot");
        }

        if (hitVFX != null)
        {
            Quaternion rot = Quaternion.LookRotation(hitNormal);
            Instantiate(hitVFX, hitPoint, rot);
        }
    }
}
