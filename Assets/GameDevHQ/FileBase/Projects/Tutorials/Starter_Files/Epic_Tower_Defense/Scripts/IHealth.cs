namespace GameDevHQ.Scripts
{
    public interface IHealth
    {
        int Health { get; set; }

        void TakeDamage(int damage);
    }
}