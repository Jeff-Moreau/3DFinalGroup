using UnityEngine;
using UnityEngine.AI;

public class TestUnit : UnitController
{
    // INSPECTOR VARIABLES
    [SerializeField] private UnitData mData;

    private mState mCurrentState;

    private void OnEnable()
    {
        Actions.UnitMove += MoveToLocation;
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        mNavAgent = GetComponent<NavMeshAgent>();
        mSelected = false;
        mCurrentState = mState.Idle;
    }

    private void Update()
    {
        switch (mCurrentState)
        {
            case mState.Idle:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                Debug.Log("I am Idle. Please do something with me."); // this is just for testing purposes
                break;

            case mState.Moving:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                Debug.Log("I am Runnin."); // this is just for testing purposes
                break;

            case mState.Working:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                Debug.Log("Work work work all day long."); // this is just for testing purposes
                break;

            case mState.Chasing:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                Debug.Log("I'm gonna get him."); // this is just for testing purposes
                break;

            case mState.Fleeing:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                Debug.Log("He's scary im leaving."); // this is just for testing purposes
                break;

            case mState.Attacking:
                // idle animation?
                // idle sound effects?
                Debug.Log("Kill kill kill"); // this is just for testing purposes
                break;

            case mState.Dead:
                // idle animation?
                // idle sound effects?
                Debug.Log("Ohh well maybe next time"); // this is just for testing purposes
                break;
        }

        if (mSelected)
        {
            // What happens when selected??
            // Sound? Image change? Menu Pop Up?
            Actions.UnitSelected?.Invoke();
            mRenderer.material.color = Color.green; // this is just for testing purposes
        }
        else
        {
            // What happens when unselecting??
            // Sound? Image change? Menu Pop Up?
            mRenderer.material.color = Color.white; // this is just for testing purposes
        }
    }

    private void MoveToLocation(RaycastHit hit)
    {
        if (mSelected)
        {
            mNavAgent.SetDestination(hit.point);
            mCurrentState = mState.Moving;
        }
    }

    private void OnMouseDown()
    {
        mSelected = !mSelected;
        Debug.Log(mData.GetName + " Was clicked on!"); // testing purposes
    }

    private void OnDisable()
    {
        Actions.UnitMove -= MoveToLocation;
    }
}