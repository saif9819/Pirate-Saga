using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boards : MonoBehaviour
{
    public int height;
    public int width;
    public GameObject tilesPrefab;
    public GameObject[] dots;
    
    public GameObject[,] allDots;

    // Start is called before the first frame update
    void Start()
    {
        allDots = new GameObject[width, height];
        Setup();
    }
    private void Setup()
    {
        for (int i = 0; i < width; i++) 
        {
            for(int j = 0; j < height; j++)
            {
                Vector2  tempPosition = new Vector2(i,j);
               GameObject backgroundTile =   Instantiate(tilesPrefab,tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name="("+ i+","+j+")";
                int dotToUse = Random.Range(0, dots.Length);
                GameObject dot = Instantiate(dots[dotToUse], tempPosition, Quaternion.identity);
                dot.transform.parent = this.transform;
                dot.name = "(" + i + "," + j + ")";
                allDots[i,j] = dot;
            }
        }
    }
}
