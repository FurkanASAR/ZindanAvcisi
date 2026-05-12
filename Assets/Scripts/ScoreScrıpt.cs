using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;

    private Player player;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        LoadScore();
        UpdateScore();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = FindObjectOfType<Player>();
    }

    public void UpdateScore()
    {
        scoreText.text = " " + player.Inventory.CalculateTotalValue();
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", player.Inventory.totalValue);
        PlayerPrefs.Save();
    }


    public void LoadScore()
    {
        player.Inventory.totalValue= PlayerPrefs.GetInt("Score", 0);
    }
}