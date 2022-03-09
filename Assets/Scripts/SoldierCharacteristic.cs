
public class SoldierCharacteristic
{
    public SoldierType type;
    public float health;
    public float speed;
    public float range;
    public float dps; // урон в секунду

    public SoldierCharacteristic(SoldierType type, float health, float speed, float range, float dps)
    {
        this.type = type;
        this.health = health;
        this.speed = speed;
        this.range = range;
        this.dps = dps;
    }

    public SoldierCharacteristic Clone()
    {
        SoldierCharacteristic characteristic = new SoldierCharacteristic(type, health, speed, range, dps);
        return characteristic;
    }
}
