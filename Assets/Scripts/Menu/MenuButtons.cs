using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Village");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyundan çıkıldı");
    }
}
