using UnityEngine;

public enum SoldierType
{
    Bazooka,
    Sniper,
    Machinegunner
}

public class SoldiersFactory : MonoBehaviour
{
    public static SoldiersFactory Instance;

    [SerializeField] HQ playerHQ;
    [SerializeField] HQ enemyHQ;

    [SerializeField] GameObject soldierPrefab;
    int oneSideSoldiersCount = 3; // Количество солдат с каждой стороны

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlaceSoldiers()
    {
        // В качестве упрощения здесь мы просто создаем на каждой стороне 
        // по три солдата каждого типа (SoldierType)

        for(int i = 0; i < oneSideSoldiersCount; i++)
        {
            SoldierType type = (SoldierType)i;

            Soldier playerSoldier = CreateSoldier(type, playerHQ);
            playerSoldier.soldierClicked.AddListener(delegate { PersonalOrdersButtonsController.Instance.UpdatePersonalOrdersButtons(playerSoldier); });
            playerHQ.AddSoldierToHQ(playerSoldier);

            Soldier enemySoldier = CreateSoldier(type, enemyHQ);
            enemyHQ.AddSoldierToHQ(enemySoldier);
        }
        playerHQ.SendOrderToAllSoldiers(OrderType.AttackClosest);
        enemyHQ.SendOrderToAllSoldiers(OrderType.AttackClosest);
    }

    private Soldier CreateSoldier(SoldierType soldierType, HQ hq)
    {
        GameObject soldierObject = Instantiate(soldierPrefab);
        Soldier soldier = soldierObject.GetComponent<Soldier>();
        soldier.Initialize(soldierType, hq);
        return soldier;
    }
}
