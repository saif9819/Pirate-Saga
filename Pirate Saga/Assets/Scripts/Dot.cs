using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] int column;
    [SerializeField] int row;
    [SerializeField] int targetX;
    [SerializeField] int targetY;
    private GameObject otherDot;
    private Boards board;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 temPosition;
    private float swipeAngle = 0;
    // Start is called before the first frame update
    void Start()
    {
        board=FindAnyObjectByType<Boards>();
        targetX=(int)transform.position.x;
        targetY=(int)transform.position.y;
        row = targetY;
        column = targetX;
    }

    // Update is called once per frame
    void Update()
    {
        targetX = column;
        targetY = row;
        Move();
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
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x)*180/Mathf.PI;
        MovePieces();
    }
    void MovePieces()
    {
        if(swipeAngle > -45 && swipeAngle <= 45 && column<board.width)
        {
            //Right swipe
            otherDot = board.allDots[column + 1, row];
            otherDot.GetComponent<Dot>().column -= 1;
            column += 1;
        } else if (swipeAngle > 45 && swipeAngle <= 135 && row<board.height)
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
    //test
}
