using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Boards boards;
    [Header("Powerup")]
    public bool isColumnBomb;
    public bool isRowBomb;
    [SerializeField] GameObject rowArrow;
    [SerializeField] GameObject columnArrow;


    // Start is called before the first frame update
    void Start()
    {
        isColumnBomb = false;
        isRowBomb = false;
        boards = FindAnyObjectByType<Boards>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            isRowBomb=true;
            GameObject arrow=Instantiate(rowArrow,transform.position,Quaternion.identity);
            arrow.transform.parent=this.transform;
        }
    }
    public List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> gems = new List<GameObject>();
        for(int i = 0;i<boards.height;i++)
        {
            if (boards.allDots[column, i] != null)
            {
                gems.Add(boards.allDots[column, i]);
                boards.allDots[column, i].GetComponent<Dot>().isMatched = true;
            }
        }
        return gems;
    }


   public List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> gems = new List<GameObject>();
        for (int i = 0; i < boards.width; i++)
        {
            if (boards.allDots[i,row] != null)
            {
                gems.Add(boards.allDots[i, row]);
                boards.allDots[i, row].GetComponent<Dot>().isMatched = true;
            }
        }
        return gems;
    }

    
}
