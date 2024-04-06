using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    private Boards boards;
    private bool paused = false;
    [SerializeField] private Image soundButton;
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    private SoundManager sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = FindAnyObjectByType<SoundManager>();
        boards = FindAnyObjectByType<Boards>();
        pausePanel.SetActive(false);
        //In player prefs the sound key is for sound
        //IF sounde ==0, then mute,or else sound ==1 then unmute
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                soundButton.sprite = musicOffSprite;
            }
            else
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                soundButton.sprite = musicOnSprite;
            }
        }
        else
        {
            soundButton.sprite = musicOnSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (paused && !pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            boards.currentState = GameState.pause;
        }
        if(!paused && pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            boards.currentState = GameState.move;
        }
    }

    public void SoundButton()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetInt("Sound") == 0)
            {
                soundButton.sprite = musicOnSprite;
                PlayerPrefs.SetInt("Sound", 1);
                sound.AdjustVolume();
            }
            else
            if (PlayerPrefs.GetInt("Sound") == 1)
            {
                soundButton.sprite = musicOffSprite;
                PlayerPrefs.SetInt("Sound", 0);
                sound.AdjustVolume();
            }
        }
        else
        {
            soundButton.sprite = musicOffSprite;
            PlayerPrefs.SetInt("Sound", 1);
            sound.AdjustVolume();
        }
    }

    public void PauseGame()
    {
        paused = !paused;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Splash");
    }
}