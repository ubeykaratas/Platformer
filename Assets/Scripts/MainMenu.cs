using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.LogWarning("Game has quit!");
    }
}
