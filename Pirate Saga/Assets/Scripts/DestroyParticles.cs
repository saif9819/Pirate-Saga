using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class DestroyParticles : MonoBehaviour
{
    private Boards board;
    [SerializeField] private GameObject destroyEffect;

    // Start is called before the first frame update
    void Start()
    {
        board=FindAnyObjectByType<Boards>();
    }

    public void Particles(int column, int row)
    {
        GameObject particle = Instantiate(destroyEffect,board.allDots[column, row].transform.position, Quaternion.identity);
        Destroy(particle, .5f);
    }


}
