using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public void Home()
    {
        SceneManager.LoadScene("Home");
    }
    public void Back()
    {
        SceneManager.LoadScene("Level Screen");
    }

    public void ForestNext()
    {
        SceneManager.LoadScene("Desert");
    }
    public void DesertNext()
    {
        SceneManager.LoadScene("Ice");
    }
    public void IceNext()
    {
        SceneManager.LoadScene("Level Screen");
    }
}
