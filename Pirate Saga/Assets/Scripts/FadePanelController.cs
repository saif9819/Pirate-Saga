using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadePanelController : MonoBehaviour
{
    [SerializeField] private Animator panelAnim;
    [SerializeField] private Animator gameInfoAnim;

    public void OK()
    {
        if (panelAnim != null && gameInfoAnim != null)
        {
            panelAnim.SetBool("Out", true);
            gameInfoAnim.SetBool("Out", true);
        }
    }
}
