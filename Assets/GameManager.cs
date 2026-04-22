using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    public int totalCollected = 0;
    public int totalCollectiblesInScene = 0;

    public TMP_Text scoreText;
    public string nextSceneName = "fase2";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddPoints(int points)
    {
        score += points;
        totalCollected++;
        UpdateScoreUI();

        if (totalCollected >= totalCollectiblesInScene)
        {
            LoadNextScene();
        }
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Pontos: " + score + " | Coletados: " + totalCollected + "/" + totalCollectiblesInScene;
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}