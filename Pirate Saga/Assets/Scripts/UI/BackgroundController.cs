using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public Canvas canvas; // Reference to the Canvas
    public Sprite defaultBackgroundImage; // The default background image to set if no level-specific background is available
    private Boards boards;

    void Start()
    {
        boards = FindObjectOfType<Boards>();
        Image background = canvas.GetComponentInChildren<Image>();

        if (boards != null && boards.world != null && boards.level < boards.world.levels.Length)
        {
            Sprite levelBackground = boards.world.levels[boards.level].background;
            if (levelBackground != null)
            {
                background.sprite = levelBackground;
            }
            else
            {
                background.sprite = defaultBackgroundImage;
            }
        }
        else
        {
            background.sprite = defaultBackgroundImage;
        }

        // Calculate and set the scale of the background image to fit the Canvas size
        float scaleX = canvas.pixelRect.width / background.sprite.texture.width;
        float scaleY = canvas.pixelRect.height / background.sprite.texture.height;
        float scaleFactor = Mathf.Max(scaleX, scaleY);
        background.rectTransform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);
    }
}



