using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [Header("Active Stuff")]
    public bool isActive;
    public Sprite activeSprite;
    public Sprite lockedSprite;
    private Image buttonImage;
    private Button myButton;

    [Header("Level UI")]
    public Image[] stars;
    public Text levelText;
    public int level;
    public GameObject confirmPanel;
    private int starActive;

    private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameData=FindObjectOfType<GameData>();
        buttonImage=GetComponent<Image>();
        myButton=GetComponent<Button>();
        LoadData();
        ActivateStar();
        ShowLevel();
        DecideSprite();
    }
    void LoadData()
    {
        //Is GameData present
        if (gameData != null)
        {
            //Decide If the level is active
            if (gameData.saveData.isActive[level - 1])
            {
                isActive = true;
            } else
            {
                isActive = false;
            }
            //Decide how many stars activate
            starActive = gameData.saveData.stars[level - 1];
        }
    }
    void ActivateStar()
    {
        for (int i = 0; i < starActive; i++)
        {
            stars[i].enabled = true;
        }
    }
    void DecideSprite()
    {
        if (isActive)
        {
            buttonImage.sprite = activeSprite;
            myButton.enabled = true;
            levelText.enabled = true;
        } 
        else
        {
            buttonImage.sprite= lockedSprite;
            myButton.enabled = false;
            levelText.enabled = false;
        }
    }

    void ShowLevel()
    {
        levelText.text = "" + level;
    }

    public void ConfirmPanel(int level)
    {
        confirmPanel.GetComponent<ConfirmPanel>().level = level;
        confirmPanel.SetActive(true);
    }
}
