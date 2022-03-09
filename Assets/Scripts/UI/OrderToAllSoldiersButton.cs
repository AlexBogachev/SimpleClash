using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderToAllSoldiersButton : MonoBehaviour
{
    [SerializeField] OrderType order;

    HQ playerHQ;

    private void Awake()
    {
        HQ[] HQs = FindObjectsOfType<HQ>();
        foreach(HQ hq in HQs)
        {
            if (hq.GetBelongingType() == BelongingType.Player)
            {
                playerHQ = hq;
                break;
            }
        }
        GetComponent<Button>().onClick.AddListener(delegate { playerHQ.SendOrderToAllSoldiers(order); });
    }
}
