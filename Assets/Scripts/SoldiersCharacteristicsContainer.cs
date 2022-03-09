using System.Collections.Generic;

public class SoldiersCharacteristicsContainer
{
    List<SoldierCharacteristic> characteristics;

    public SoldiersCharacteristicsContainer()
    {
        characteristics = new List<SoldierCharacteristic>() {
        new SoldierCharacteristic(SoldierType.Bazooka ,50.0f, 0.25f, 4.0f, 5.0f),
        new SoldierCharacteristic(SoldierType.Machinegunner ,40.0f, 0.4f, 6.0f, 3.0f),
        new SoldierCharacteristic(SoldierType.Sniper ,30.0f, 0.5f, 7.0f, 2.0f)};
    }

    public SoldierCharacteristic GetCharacteristics(SoldierType type)
    {
        return characteristics.Find(x => x.type == type);
    }
}
