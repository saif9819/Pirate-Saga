using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private int column;
    private int row;
    private int previousColumn;
    private int previousRow;
    private int targetX;
    private int targetY;
    private bool isMatched = false;
    
    private GameObject otherDot;
    private Boards board;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 temPosition;
    private float swipeAngle = 0;
    private float swipeResist = 1f;
    // Start is called before the first frame update
    void Start()
    {
        board=FindAnyObjectByType<Boards>();
        targetX=(int)transform.position.x;
        targetY=(int)transform.position.y;
        row = targetY;
        column = targetX;
        previousRow = row;
        previousColumn=column;
    }

    // Update is called once per frame
    void Update()
    {
        FindMatches();
        if (isMatched)
        {
            SpriteRenderer mysprite = GetComponent<SpriteRenderer>();
            mysprite.color = Color.white;
        }
        targetX = column;
        targetY = row;
        Move();
    }

    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.7f);
        if(otherDot != null)
        {
            if(!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;
                row = previousRow;
                column=previousColumn;
            }
         otherDot = null;
        }
    }
    private void OnMouseDown()
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }
    void OnMouseUp()
    {
        finalTouchPosition= Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalcuteAngle();    
    }

    void CalcuteAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
        }
    }
    void MovePieces()
    {
        if(swipeAngle > -45 && swipeAngle <= 45 && column<board.width - 1)
        {
            //Right swipe
            otherDot = board.allDots[column + 1, row];
            otherDot.GetComponent<Dot>().column -= 1;
            column += 1;
        } else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            //Up swipe
            otherDot = board.allDots[column, row + 1];
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column>0)
        {
            //Left swipe
            otherDot = board.allDots[column - 1, row];
            otherDot.GetComponent<Dot>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row>0)
        {
            //Down swipe
            otherDot = board.allDots[column, row - 1];
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
            transform.position = Vector2.Lerp(transform.position, temPosition, .4f);
        }
        else
        {
            //Directly set the position
            temPosition = new Vector2(targetX, transform.position.y);
            transform.position = temPosition;
            board.allDots[column, row] = this.gameObject;
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //Move Towards Tagert
            temPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, temPosition, .4f);
        }
        else
        {
            //Directly set the position
            temPosition = new Vector2(transform.position.x, targetY);
            transform.position = temPosition;
            board.allDots[column, row] = this.gameObject;
        }
    }

    void FindMatches()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftDot1 = board.allDots[column - 1, row];
            GameObject rightDot1 = board.allDots[column + 1, row];
            if(leftDot1.tag==this.gameObject.tag && rightDot1.tag == this.gameObject.tag)
            {
                leftDot1.GetComponent<Dot>().isMatched = true;
                rightDot1.GetComponent<Dot>().isMatched = true;
                isMatched = true;
            }
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject upDot1 = board.allDots[column, row + 1];
            GameObject downDot1 = board.allDots[column, row - 1];
            if (upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag)
            {
                upDot1.GetComponent<Dot>().isMatched = true;
                downDot1.GetComponent<Dot>().isMatched = true;
                isMatched = true;
            }
        }
    }

}
