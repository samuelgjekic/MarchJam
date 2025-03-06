using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;

    }

    public void RestartGameScene()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;

    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void ContinueGameScene()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
    public void CreditScene()
    {
        SceneManager.LoadScene(2);
    }
    public void quit()
    {
        Application.Quit();
    }
}
