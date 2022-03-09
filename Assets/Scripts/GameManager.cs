using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    SoldiersCharacteristicsContainer characteristicsContainer;
    [HideInInspector] public UnityEvent assignSoldiersToHQ = new UnityEvent();

    float timer;

    [SerializeField] GameObject mainWindowPrefab;
    Transform windowParent;

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

        characteristicsContainer = new SoldiersCharacteristicsContainer();
        windowParent = FindObjectOfType<Canvas>().transform;
    }

    private void Start()
    {
        GameObject windowObject = Instantiate(mainWindowPrefab, windowParent);
        windowObject.GetComponent<MainWindow>().Initialize();
        Time.timeScale = 0.0f;
    }

    public void StartGame()
    {
        SoldiersFactory.Instance.PlaceSoldiers();
        timer = 0.0f;
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void GameOver(BelongingType winner)
    {
        Time.timeScale = 0.0f;
        GameObject windowObject = Instantiate(mainWindowPrefab, windowParent);
        windowObject.GetComponent<MainWindow>().Initialize(winner, timer);
    }

    public SoldiersCharacteristicsContainer GetSoldiersCharacteristicsContainer()
    {
        return characteristicsContainer;
    }
}
