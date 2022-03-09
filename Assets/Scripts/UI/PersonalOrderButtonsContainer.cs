using UnityEngine;

public class PersonalOrderButtonsContainer : MonoBehaviour
{
    HQ playerHQ;

    public void Initialize(Soldier soldier)
    {
        HQ[] HQs = FindObjectsOfType<HQ>();
        foreach (HQ hq in HQs)
        {
            if (hq.GetBelongingType() == BelongingType.Player)
            {
                playerHQ = hq;
                break;
            }
        }
        playerHQ.SetFocusedSoldier(soldier);
    }

    public void ChangeFocusedSoldier(Soldier newSoldier)
    {
        playerHQ.SetFocusedSoldier(newSoldier);
    }

    public HQ GetPlayerHQ()
    {
        return playerHQ;
    }
}
