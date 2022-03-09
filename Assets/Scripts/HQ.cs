using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///  Отражает подчиненность солдат (Игрок/Противник)
/// </summary>
public enum BelongingType
{
    Player,
    Enemy
}

public enum OrderType
{
    AttackClosest,
    AttackStrongest,
    AttackWeakest,
    AttackMinHealth,
    AttackMaxHealth
}

/// <summary>
///  Штаб - отвечает за учет солдат
///  и отдачу им приказов
/// </summary>
public class HQ : MonoBehaviour
{
    [SerializeField] BelongingType belongingType;
    [SerializeField] HQ opposingHQ;
    [SerializeField] Area dropZone;
    List<Soldier>soldiers;

    Soldier focusedSoldier;
    OrderType armyOrder;

    private void Awake()
    {
        soldiers = new List<Soldier>();
    }

    public void AddSoldierToHQ(Soldier soldier)
    {
        soldiers.Add(soldier);
        soldier.soldierKilled.AddListener(SoldierKilled);
        soldier.GetComponent<SoldierRepresentation>().Initialize(belongingType, soldier);

        float xPosition = Random.Range(dropZone.minCoords.x, dropZone.maxCoords.x);
        float yPosition = Random.Range(dropZone.minCoords.y, dropZone.maxCoords.y);
        Vector2 dropPosition = new Vector2(xPosition, yPosition);
        soldier.Move(dropPosition);
    }

    public void SetFocusedSoldier(Soldier soldier)
    {
        focusedSoldier = soldier;
    }

    public void SendRandomOrder()
    {
        int orderInt = Random.Range(0, 5);
        OrderType order = (OrderType)orderInt;
        SendOrder(order);
    }

    public void SendOrder(OrderType order)
    {
        if (opposingHQ.GetSoldiers().Count == 0)
            return;


        switch (order)
        {
            case OrderType.AttackClosest:
                Soldier closestSoldier = opposingHQ.GetClosestSoldier(focusedSoldier);
                focusedSoldier.SetTarget(closestSoldier.gameObject);
                break;
            case OrderType.AttackMaxHealth:
                focusedSoldier.SetTarget(opposingHQ.GetMaxHealthSoldier().gameObject);
                break;
            case OrderType.AttackMinHealth:
                focusedSoldier.SetTarget(opposingHQ.GetMinHealthSoldier().gameObject);
                break;
            case OrderType.AttackStrongest:
                focusedSoldier.SetTarget(opposingHQ.GetStrongestSoldier().gameObject);
                break;
            case OrderType.AttackWeakest:
                focusedSoldier.SetTarget(opposingHQ.GetWeakestSoldier().gameObject);
                break;
        }
        focusedSoldier = null;
    }

    public void SendOrderToAllSoldiers(OrderType order)
    {
        foreach(Soldier soldier in soldiers)
        {
            SetFocusedSoldier(soldier);
            SendOrder(order);
        }
        armyOrder = order;
    }

    public void Winner()
    {
        foreach(Soldier soldier in soldiers)
        {
            soldier.Win();
        }
    }

    public Soldier GetClosestSoldier(Soldier opposingSoldier)
    {
        float minDist = float.PositiveInfinity;
        Soldier closestSoldier = null;
        foreach(Soldier soldier in soldiers)
        {
            float tmpDist = Vector2.Distance(soldier.GetPosition(), opposingSoldier.GetPosition());
            if (tmpDist < minDist)
            {
                minDist = tmpDist;
                closestSoldier = soldier;
            }
        }
        return closestSoldier;
    }

    public Soldier GetStrongestSoldier()
    {
        if (soldiers.Count == 0)
        {
            return null;
        }
        else
        {
            Soldier soldier = soldiers.Aggregate((s1, s2) => s1.GetCharacteristic().dps > s2.GetCharacteristic().dps ? s1 : s2);
            return soldier;
        }
    }

    public Soldier GetWeakestSoldier()
    {
        if (soldiers.Count == 0)
        {
            return null;
        }
        else
        {
            Soldier soldier = soldiers.Aggregate((s1, s2) => s1.GetCharacteristic().dps < s2.GetCharacteristic().dps ? s1 : s2);
            return soldier;
        }
        
    }

    public Soldier GetMinHealthSoldier()
    {
        if (soldiers.Count == 0)
        {
            return null;
        }
        else
        {
            Soldier soldier = soldiers.Aggregate((s1, s2) => s1.GetCharacteristic().health < s2.GetCharacteristic().health ? s1 : s2);
            return soldier;
        }
           
    }

    public Soldier GetMaxHealthSoldier()
    {
        if(soldiers.Count == 0)
        {
            return null;
        }
        else
        {
            Soldier soldier = soldiers.Aggregate((s1, s2) => s1.GetCharacteristic().health > s2.GetCharacteristic().health ? s1 : s2);
            return soldier;
        }
    }

    public BelongingType GetBelongingType()
    {
        return belongingType;
    }

    public OrderType GetArmyOrder()
    {
        return armyOrder;
    }

    public List<Soldier> GetSoldiers()
    {
        return soldiers;
    }

    private void SoldierKilled(Soldier soldier)
    {
        soldiers.Remove(soldier);

        if (soldiers.Count == 0)
        {
            opposingHQ.Winner();
            GameManager.Instance.GameOver(opposingHQ.GetBelongingType());
        }
    }

}
