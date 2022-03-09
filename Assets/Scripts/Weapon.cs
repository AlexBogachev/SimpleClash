using UnityEngine;

public class Weapon : MonoBehaviour
{
    Soldier weaponOwner;
    LineRenderer fireLine;

    bool isFiring;

    public void Initialize(Soldier soldier)
    {
        fireLine = GetComponent<LineRenderer>();
        weaponOwner = soldier;
    }

    public void Fire(bool isFiring)
    {
        this.isFiring = isFiring;
        fireLine.enabled = isFiring;
    }

    private void Update()
    {
        if (isFiring)
        {
            fireLine.SetPosition(0, weaponOwner.GetPosition());
            fireLine.SetPosition(1, weaponOwner.GetTarget().GetPosition());
            weaponOwner.GetTarget().GetHit(weaponOwner.GetCharacteristic().dps * Time.deltaTime);
        }
    }
}
