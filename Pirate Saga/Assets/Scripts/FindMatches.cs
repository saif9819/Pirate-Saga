using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindMatches : MonoBehaviour
{
   // private PowerUp powerUp;
    private Boards boards;
    public List<GameObject> currentMatches = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        boards=FindAnyObjectByType<Boards>();
        //powerUp = FindAnyObjectByType<PowerUp>();
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }

    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0;i<boards.width;i++)
        {
            for (int j = 0;j<boards.height;j++) 
            {
                GameObject currentDot = boards.allDots[i, j]; 
                if (currentDot != null)
                {
                    if (i > 0 && i < boards.width - 1)
                    {
                        GameObject leftDot = boards.allDots[i - 1, j];
                        GameObject rightDot = boards.allDots[i + 1, j];
                        if(leftDot != null && rightDot != null)
                        {
                            if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                            {
                                if(currentDot.GetComponent<Dot>().isRowBomb
                                    || leftDot.GetComponent<Dot>().isRowBomb
                                    || rightDot.GetComponent<Dot>().isRowBomb)
                                {
                                    
                                    currentMatches.Union(GetRowPieces(j));
                                }

                                if(currentDot.GetComponent<Dot>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i));
                                }
                                if (leftDot.GetComponent<Dot>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i - 1));
                                }

                                if (rightDot.GetComponent<Dot>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i + 1));
                                }

                                if (!currentMatches.Contains(leftDot))
                                {
                                    currentMatches.Add(leftDot);
                                }
                               
                                leftDot.GetComponent<Dot>() .isMatched = true;
                                if (!currentMatches.Contains(rightDot))
                                {
                                    currentMatches.Add(rightDot);
                                }
                                rightDot.GetComponent<Dot>().isMatched = true;
                                if (!currentMatches.Contains(currentDot))
                                {
                                    currentMatches.Add(currentDot);
                                }
                                currentDot.GetComponent<Dot>().isMatched = true;
                            }
                        }
                    }

                    if (j > 0 && j < boards.height - 1)
                    {
                        GameObject upDot = boards.allDots[i, j - 1];
                        GameObject downDot = boards.allDots[i, j + 1];
                        if (upDot != null && downDot != null)
                        {
                            if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                            {

                                if (currentDot.GetComponent<Dot>().isColumnBomb
                                   || upDot.GetComponent<Dot>().isColumnBomb
                                   || downDot.GetComponent<Dot>().isColumnBomb)
                                {

                                    currentMatches.Union(GetColumnPieces(i));
                                }

                                if (currentDot.GetComponent<Dot>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j));
                                }
                                if (upDot.GetComponent<Dot>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j + 1));
                                }
                                if (downDot.GetComponent<Dot>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j - 1));
                                }

                                if (!currentMatches.Contains(upDot))
                                {
                                    currentMatches.Add(upDot);
                                }
                                upDot.GetComponent<Dot>().isMatched = true;
                                if (!currentMatches.Contains(downDot))
                                {
                                    currentMatches.Add(downDot);
                                }
                                downDot.GetComponent<Dot>().isMatched = true;
                                if (!currentMatches.Contains(currentDot))
                                {
                                    currentMatches.Add(currentDot);
                                }
                                currentDot.GetComponent<Dot>().isMatched = true;
                            }
                        }
                    }
                }
            }
        }
    }

    List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> gems = new List<GameObject>();
        for (int i = 0; i < boards.height; i++)
        {
            if (boards.allDots[column, i] != null)
            {
                gems.Add(boards.allDots[column, i]);
                boards.allDots[column, i].GetComponent<Dot>().isMatched = true;
            }
        }
        return gems;
    }


     List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> gems = new List<GameObject>();
        for (int i = 0; i < boards.width; i++)
        {
            if (boards.allDots[i, row] != null)
            {
                gems.Add(boards.allDots[i, row]);
                boards.allDots[i, row].GetComponent<Dot>().isMatched = true;
            }
        }
        return gems;
    }

}
