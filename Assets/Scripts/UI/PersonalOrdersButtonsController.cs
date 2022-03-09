using UnityEngine;


/// <summary>
///  Создает и обновляет кнопки
///  персональных приказов для солдат
/// </summary>
public class PersonalOrdersButtonsController : MonoBehaviour
{
    public static PersonalOrdersButtonsController Instance;

    [SerializeField] GameObject personalOrdersButtonsContainerPrefab;
    PersonalOrderButtonsContainer personalOrdersButtons;

    Transform parent;

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

        parent = FindObjectOfType<Canvas>().transform;
    }

    public void UpdatePersonalOrdersButtons(Soldier soldier)
    {
        if(personalOrdersButtons == null)
        {
            GameObject buttonsObject = Instantiate(personalOrdersButtonsContainerPrefab, parent);
            personalOrdersButtons = buttonsObject.GetComponent<PersonalOrderButtonsContainer>();
            print("personal = " + personalOrdersButtons);
            personalOrdersButtons.Initialize(soldier);
        }
        else
        {
            personalOrdersButtons.ChangeFocusedSoldier(soldier);
        }
    }
}
