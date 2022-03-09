using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierRepresentation : MonoBehaviour
{
    [SerializeField] SpriteRenderer topSprite;
    [SerializeField] SpriteRenderer baseSprite;

    public void Initialize(BelongingType belonging, Soldier soldier)
    {

        if(belonging == BelongingType.Player)
        {
            baseSprite.color = Color.green;
        }
        else if (belonging == BelongingType.Enemy)
        {
            baseSprite.color = Color.red;
        }

        switch (soldier.GetCharacteristic().type)
        {
            case SoldierType.Bazooka:
                topSprite.color = Color.yellow;
                break;
            case SoldierType.Machinegunner:
                topSprite.color = Color.grey;
                break;
            case SoldierType.Sniper:
                topSprite.color = Color.white;
                break;
        }

        soldier.soldierKilled.AddListener(KilledRepresentation);
    }

    private void KilledRepresentation(Soldier soldier)
    {
        baseSprite.color = Color.black;
    }
}
