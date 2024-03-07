using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BlankGoal
{
    public int numNeeded;
    public int numcollected;
    public Sprite goalSprite;
    public string matchValue;
}

public class GoalManager : MonoBehaviour
{
    [SerializeField] private BlankGoal[] levelGoals;
    [SerializeField] List<GoalPanel> currentGoals=new List<GoalPanel>();
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private GameObject goalIntroParent;
    [SerializeField] private GameObject goalGameParent;

    // Start is called before the first frame update
    void Start()
    {
        SetupGoals();
    }

    void SetupGoals()
    {
        for (int i = 0; i < levelGoals.Length; i++)
        {
            //create a new goal panel at the goaLIntro parent
            GameObject goal = Instantiate(goalPrefab, goalIntroParent.transform.position, Quaternion.identity);
            goal.transform.SetParent(goalIntroParent.transform);
            //set the image and text of the goal;
            GoalPanel panel=goal.GetComponent<GoalPanel>();
            panel.thisSprite = levelGoals[i].goalSprite;
            panel.thisString = "0" + levelGoals[i].numNeeded;

            //create a new goal panel at the goaLGame parent
            GameObject gameGoal = Instantiate(goalPrefab, goalGameParent.transform.position, Quaternion.identity);
            gameGoal.transform.SetParent(goalGameParent.transform);
            panel = gameGoal.GetComponent<GoalPanel>();
            currentGoals.Add(panel);
            panel.thisSprite = levelGoals[i].goalSprite;
            panel.thisString = "0" + levelGoals[i].numNeeded;
        }
    }

    public void UpdateGoals()
    {
        int goalsCompleted = 0;
        for (int i = 0; i < levelGoals.Length; i++)
        {
            currentGoals[i].thisText.text = "" + levelGoals[i].numcollected + "/" + levelGoals[i].numNeeded;
            if (levelGoals[i].numcollected >= levelGoals[i].numNeeded)
            {
                goalsCompleted++;
                currentGoals[i].thisText.text = "" + levelGoals[i].numNeeded + "/" + levelGoals[i].numNeeded;
            }
        }
        if (goalsCompleted >= levelGoals.Length)
        {
            Debug.Log("You Win");
        }
        
    }

    public void ComapreGoal(string goalToComapre)
    {
        for(int i = 0;i < levelGoals.Length; i++)
        {
            if (goalToComapre == levelGoals[i].matchValue)
            {
                levelGoals[i].numcollected++;
            }
        }
    }

}
