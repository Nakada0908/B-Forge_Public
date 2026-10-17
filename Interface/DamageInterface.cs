using UnityEngine;

public class DamageInterface
{
    public interface IPlayerDamage
    {
        void TakeDamage(int damage);
    }

    public interface IEnemyDamage
    {
        void TakeDamage(int damage);
    }
}
