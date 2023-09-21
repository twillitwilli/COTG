namespace QTArts.Interfaces
{
    public interface iDamagable<T>
    {
        float Health { get; set; }

        void Damage(T damageAmount);

        void Death();
    }
}
