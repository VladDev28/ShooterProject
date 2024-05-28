using System;

public class HealthSystem
{
    public event EventHandler HealthChanged;
    private int hp;
    private int hpMax;
    public HealthSystem(int hpMax) {
        this.hpMax = hpMax;
        hp = hpMax;
    }
    public int getHP() { 
        return hp; 
    }
    public float GetHpPercent() { return (float) hp / hpMax; }
    public void Damage(int damageAmount)
    {
        hp -= damageAmount;
        if (hp < 0) hp = 0;
        if (HealthChanged != null) HealthChanged(this, EventArgs.Empty);

    }
    public void Heal(int healAmount)
    {
        hp += healAmount;
        if(hp > hpMax) hp = hpMax;
    }
    public bool IsDead()
    {
        return hp <= 0;
    }
}
