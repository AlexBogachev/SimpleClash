using UnityEngine;
using UnityEngine.UI;

public class PersonalOrderButton : MonoBehaviour
{
    [SerializeField] OrderType order;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { GetComponentInParent<PersonalOrderButtonsContainer>().GetPlayerHQ().SendOrder(order); });
        GetComponent<Button>().onClick.AddListener(ClosePanel);
    }

    private void ClosePanel()
    {
        Destroy(transform.parent.gameObject);
    }
}
