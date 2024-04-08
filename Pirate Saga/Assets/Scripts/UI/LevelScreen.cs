using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScreen : MonoBehaviour
{


    public void Forset()
    {
        SceneManager.LoadScene("Forest");
    }

    public void Desert()
    {
        SceneManager.LoadScene("Desert");
    }
    public void Ice()
    {
        SceneManager.LoadScene("Ice");
    }

}
