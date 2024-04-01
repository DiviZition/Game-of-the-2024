public interface IDamageable
{
    public int BaseHealth { get; }
    public int CurrentHealth { get; }

    public void TakeDamaget(int damage);
}