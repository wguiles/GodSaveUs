using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private bool isCompleted;
    private bool isFailed;
    private Vector2 goalLocation;

    //enum that stores the objective difficulty
    //easy  medium  hard
    public enum objectiveDifficulty { easy, medium, hard };
    private objectiveDifficulty difficulty;
    public string objectiveText;

    // Start is called before the first frame update
    void Start()
    {
        isCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 GetGoalLocation()
    {
        return goalLocation;
    }

    public objectiveDifficulty GetDifficulty()
    {
        return difficulty;
    }

    protected void SetGoalLocation(Vector2 goal)
    {
        goalLocation = goal;
    }

    public void SetDifficulty(objectiveDifficulty type)
    {
        difficulty = type;
    }

    public virtual void Complete()
    {
        isCompleted = true;
    }

    public bool GetIsCompleted()
    {
        return isCompleted;
    }

    public void Fail()
    {
        isFailed = true;
    }

    public bool GetIsFailed()
    {
        return isFailed;
    }

}
