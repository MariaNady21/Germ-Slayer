using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] EnemyDeath enemyDeath;

    public void TriggerDeathVFX()
    {
        if (enemyDeath != null)
        {
            enemyDeath.TriggerDeathVFX();
        }
    }
}
