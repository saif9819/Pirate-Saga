using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToSplash : MonoBehaviour
{
    public string sceneToLoad;
    private GameData gameData;
    private Boards boards;

    

    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        boards = FindObjectOfType<Boards>();
    }

   

    public void WinOk()
    {
        if (gameData != null)
        {
            gameData.saveData.isActive[boards.level + 1] = true;
            gameData.Save();
        }
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoseOk()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
