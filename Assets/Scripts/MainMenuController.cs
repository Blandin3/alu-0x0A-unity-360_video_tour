using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void LoadIntranetTour()
    {
        SceneManager.LoadScene("IntranetTour");
    }

    public void LoadCustomCampusTour()
    {
        SceneManager.LoadScene("CustomCampusTour");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
