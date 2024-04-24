using UnityEngine;

public class ButtonNavigation : MonoBehaviour
{
    public void LoadMain()
    {
        SceneNavigation.LoadScene("Main");
    }
    // Load Scene A
    public void LoadForest()
    {
        SceneNavigation.LoadScene("Forest");
    }

    // Load Scene B
    public void LoadIce()
    {
        SceneNavigation.LoadScene("Ice");
    }

    // Load Scene C
    public void LoadDesert()
    {
        SceneNavigation.LoadScene("Desert");
    }


    // Load Scene D
    public void LoadLevelScreen()
    {
        SceneNavigation.LoadScene("Level Screen");
    }

    // Load Previous Scene
    public void LoadPreviousScene()
    {
        SceneNavigation.LoadPreviousScene();
    }
}
