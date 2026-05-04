using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        gameState = GameState.Game;
    }

    public enum GameState
    {
        Game,
        NotGame
    }


    public GameState gameState;

    public bool IsPlaying()
    {
        return gameState == GameState.Game;
    }
}
