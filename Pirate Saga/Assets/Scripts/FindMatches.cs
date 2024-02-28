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
    private List<GameObject> isRowBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(dot1.row));
        }
        if (dot2.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(dot2 .row));
        }
        if (dot3.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(dot3.row));
        }
        return currentDots;
    }
    private List<GameObject> isColumnBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(dot1.column));
        }
        if (dot2.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(dot2.column));
        }
        if (dot3.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(dot3.column));
        }
        return currentDots;
    }

    private void AddToListAndMAtch(GameObject dot)
    {
        if (!currentMatches.Contains(dot))
        {
            currentMatches.Add(dot);
        }

        dot.GetComponent<Dot>().isMatched = true;
    }

    private void GetNearbyPieces(GameObject dot1,GameObject dot2,GameObject dot3)
    {
        AddToListAndMAtch(dot1);
        AddToListAndMAtch(dot2);
       AddToListAndMAtch(dot3);
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
                    Dot currentDotDot = currentDot.GetComponent<Dot>();
                    if (i > 0 && i < boards.width - 1)
                    {
                        GameObject leftDot = boards.allDots[i - 1, j];
                        
                        
                        GameObject rightDot = boards.allDots[i + 1, j];
                        
                        if (leftDot != null && rightDot != null)
                        {
                            Dot leftDotDot = leftDot.GetComponent<Dot>();
                            Dot rightDotDot = rightDot.GetComponent<Dot>();
                            if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                            {
                                currentMatches.Union(isRowBomb(leftDotDot, currentDotDot, rightDotDot));

                                currentMatches.Union(isColumnBomb(leftDotDot, currentDotDot, rightDotDot));

                                GetNearbyPieces(leftDot, currentDot, rightDot);

                                
                            }
                        }
                    }

                    if (j > 0 && j < boards.height - 1)
                    {
                        GameObject upDot = boards.allDots[i, j - 1];
                        
                        GameObject downDot = boards.allDots[i, j + 1];
                        
                        if (upDot != null && downDot != null)
                        {
                            Dot upDotDot = upDot.GetComponent<Dot>();
                            Dot downDotDot = downDot.GetComponent<Dot>();
                            if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                            {

                                currentMatches.Union(isColumnBomb(upDotDot, currentDotDot, downDotDot));
                                currentMatches.Union(isRowBomb(upDotDot,currentDotDot,downDotDot));

                                GetNearbyPieces(upDot, currentDot,downDot);


                            }
                        }
                    }
                }
            }
        }
    }

    public void MatchPiecesOfColor(string color)
    {
        for(int i = 0;i<boards.width;i++)
        {
            for (int j = 0; j < boards.height; j++)
            {
                //check if that piece that exists
                if (boards.allDots[i, j] != null)
                {
                    if (boards.allDots[i, j].tag == color)
                    {
                        //set that dot to be matched
                        boards.allDots[i,j].GetComponent<Dot>().isMatched = true;
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

    public void CheckBombs()
    {
        //Did the player move something?
        if(boards.currentDot != null)
        {
            //Is the piece they moved matched?
            if (boards.currentDot.isMatched)
            {
                //make it unmatched
                boards.currentDot.isMatched = false;
                //Decide what kind of bomb to make
              /*
                int typeOfBomb = Random.Range(0, 100);
                if (typeOfBomb < 50)
                {
                    //make row bomb
                    boards.currentDot.MakeRowBomb();
                } 
                else if (typeOfBomb <= 50)
                {
                    //make column bomb
                    boards.currentDot.MakeColumnBomb();
                }*/
              if((boards.currentDot.swipeAngle>-45 && boards.currentDot.swipeAngle <= 45)
                    ||(boards.currentDot.swipeAngle > -135||boards.currentDot.swipeAngle >= 135))
                {
                    boards.currentDot.MakeRowBomb();
                }
                else
                {
                    boards.currentDot.MakeColumnBomb();
                }
             
            }
            //Is the other piece matched?
            else if (boards.currentDot.otherDot != null) 
            {
                Dot otherDot = boards.currentDot.otherDot.GetComponent<Dot>();
                if (otherDot.isMatched)
                {
                    //Make it unmatched
                    otherDot.isMatched = false;
                    /*
                      int typeOfBomb = Random.Range(0, 100);
                      if (typeOfBomb < 50)
                      {
                          //make row bomb
                          otherDot.MakeRowBomb();
                      }
                      else if (typeOfBomb <= 50)
                      {
                          //make column bomb
                          otherDot.MakeColumnBomb();
                      }
                    */

                    if ((boards.currentDot.swipeAngle > -45 && boards.currentDot.swipeAngle <= 45)
                    || (boards.currentDot.swipeAngle > -135 || boards.currentDot.swipeAngle >= 135))
                    {
                        otherDot.MakeRowBomb();
                    }
                    else
                    {
                        otherDot.MakeColumnBomb();
                    }

                }
            }
        }
    }

}
