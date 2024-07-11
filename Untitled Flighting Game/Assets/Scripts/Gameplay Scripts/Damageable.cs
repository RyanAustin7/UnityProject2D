using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float damageTaken = 0f; 

    public void IncreaseDamage(float amount)
    {
        damageTaken += amount;
    }

    public void ResetDamage()
    {
        damageTaken = 0f;
    }
}
