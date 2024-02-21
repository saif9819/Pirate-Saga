using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    public int column;
    public int row;
    private int previousColumn;
    private int previousRow;
    private int targetX;
    private int targetY;
    public bool isMatched = false;
    private FindMatches findMatches;
    private GameObject otherDot;
    private Boards board;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 temPosition;

    [Header("Swipe Stuff")]
    private float swipeAngle = 0;
    private float swipeResist = 1f;

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
        board =FindAnyObjectByType<Boards>();
        findMatches=FindAnyObjectByType<FindMatches>();
       // targetX=(int)transform.position.x;
       // targetY=(int)transform.position.y;
       // row = targetY;
       // column = targetX;
       // previousRow = row;
      //  previousColumn=column;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRowBomb = true;
            GameObject arrow = Instantiate(rowArrow, transform.position, Quaternion.identity);
            arrow.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMatched)
        {
            SpriteRenderer mysprite = GetComponent<SpriteRenderer>();
            mysprite.color= new Color(mysprite.color.r, mysprite.color.g, mysprite.color.b, .3f);
        }
        targetX = column;
        targetY = row;
        Move();
    }

    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.9f);
        if(otherDot != null)
        {
            if(!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;
                row = previousRow;
                column=previousColumn;
                yield return new WaitForSeconds(.5f);
                board.cureentState = GameState.move;
            }
            else
            {
                board.DestroyMatches();
                
            }
            otherDot = null;
        }
       /* else
        {
            board.currentState = GameState.move;   // i added this line because otherDot is NULL 

        }*/


    }
    private void OnMouseDown()
    {
        if (board.cureentState == GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    void OnMouseUp()
    {
        if (board.cureentState == GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalcuteAngle();
        }
        
    }

    void CalcuteAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
            board.cureentState = GameState.wait;
        }
        else
        {
            board.cureentState = GameState.move;
        }
    }
    void MovePieces()
    {
        if(swipeAngle > -45 && swipeAngle <= 45 && column<board.width - 1)
        {
            //Right swipe
            otherDot = board.allDots[column + 1, row];
            previousRow = row;
            previousColumn=column;
            otherDot.GetComponent<Dot>().column -= 1;
            column += 1;
        } else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            //Up swipe
            otherDot = board.allDots[column, row + 1];
            previousRow = row;
           previousColumn = column;
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column>0)
        {
            //Left swipe
            otherDot = board.allDots[column - 1, row];
            previousRow = row;
           previousColumn = column;
            otherDot.GetComponent<Dot>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row>0)
        {
            //Down swipe
            otherDot = board.allDots[column, row - 1];
           previousRow = row;
           previousColumn = column;
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMoveCo());
    }

    void Move()
    {
        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //Move Towards Tagert
            temPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, temPosition, .6f);
            if (board.allDots[column,row]!= this.gameObject)
            {
                board.allDots[column,row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else
        {
            //Directly set the position
            temPosition = new Vector2(targetX, transform.position.y);
            transform.position = temPosition;
            
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //Move Towards Tagert
            temPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, temPosition, .6f);
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else
        {
            //Directly set the position
            temPosition = new Vector2(transform.position.x, targetY);
            transform.position = temPosition;
            
        }
    }

    void FindMatches()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftDot1 = board.allDots[column - 1, row];
            GameObject rightDot1 = board.allDots[column + 1, row];
            if (leftDot1 != null && rightDot1 != null)
            {

                if (leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag)
                {
                    leftDot1.GetComponent<Dot>().isMatched = true;
                    rightDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject upDot1 = board.allDots[column, row + 1];
            GameObject downDot1 = board.allDots[column, row - 1];
            if (upDot1 != null && downDot1 != null)
            {


                if (upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag)
                {
                    upDot1.GetComponent<Dot>().isMatched = true;
                    downDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }

}
