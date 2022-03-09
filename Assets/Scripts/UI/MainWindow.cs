using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainWindow : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] Text header;
    [SerializeField] Text timerText;
    [SerializeField] Text description;

    public void Initialize()
    {
        button.onClick.AddListener(delegate { GameManager.Instance.StartGame(); });
        button.onClick.AddListener(ClosePanel);
    }

    public void Initialize(BelongingType type, float timer)
    {
        description.gameObject.SetActive(false);

        header.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);

        header.text = type == BelongingType.Player ? "Вы выиграли" : "Противник выиграл";

        int seconds = Mathf.RoundToInt(timer);
        int min = seconds / 60;
        int sec = seconds % 60;
        timerText.text = min + " : " + sec;

        button.GetComponentInChildren<Text>().text = "ЗАНОВО";
        button.onClick.AddListener(ReloadScene);
    }

    private void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void ClosePanel()
    {
        Destroy(gameObject);
    }
}
