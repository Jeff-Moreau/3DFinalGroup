using UnityEngine;
using UnityEngine.AI;

public class AIFighterUnit : UnitController, ISelectable
{
    // INSPECTOR VARIABLES
    [SerializeField] private UnitData mData;

    private float mCurrentHealth;
    private bool mIsAlive;
    [SerializeField] private bool mIsBasePatrol;
    private GameObject[] mAIBaseWaypointContainer;
    private Transform[] mAIBaseWaypointsToFollow;
    private int mCurrentBaseWaypointPosition;
    private float mAIBaseWaypointDistanceCheck;

    public float GetHealth => mCurrentHealth;
    public bool IsAlive => mIsAlive;
    public void SetCurrentHealth(float damage) => mCurrentHealth = damage;
    public void SetIsBasePatrol(bool yesno) => mIsBasePatrol = yesno;

    private void Awake()
    {
        mNavAgent = GetComponent<NavMeshAgent>();
        mAnimator = GetComponent<Animator>();
        mAIBaseWaypointContainer = LoadingManager.Load.GetCurrentMap.GetAIBaseWaypointContainer;
    }

    private void Start()
    {
        InitializeVariables();
        SetupBasePatrolWaypoints();
    }

    private void SetupBasePatrolWaypoints()
    {
        var totalWaypoints = mAIBaseWaypointContainer[0].GetComponentsInChildren<Transform>();
        mAIBaseWaypointsToFollow = new Transform[totalWaypoints.Length - 1];

        for (int i = 1; i < totalWaypoints.Length; i++)
        {
            mAIBaseWaypointsToFollow[i - 1] = totalWaypoints[i];
        }
    }

    private void InitializeVariables()
    {
        mNavAgent.speed = mData.GetMovementSpeed;
        mSelected = false;
        mCurrentState = State.Idle;
        mCurrentPosition = transform.position;
        mCurrentRotation = transform.rotation;
        mCurrentHealth = mData.GetMaxHealth;
        mIsAlive = true;
        mCurrentBaseWaypointPosition = 0;
        mAIBaseWaypointDistanceCheck = 2 * 2;
    }

    private void Update()
    {
        if (mCurrentHealth <= 0)
        {
            mCurrentState = State.Dead;
        }

        switch (mCurrentState)
        {
            case State.Idle:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                if (mIsBasePatrol)
                {
                    mCurrentState = State.Patrol;
                }
                transform.position = new Vector3(mCurrentPosition.x, transform.position.y, mCurrentPosition.z);
                transform.rotation = mCurrentRotation;
                mAnimator.SetBool("IsWalking", false);
                Debug.Log("I am Idle. Please do something with me."); // this is just for testing purposes
                break;

            case State.Moving:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                if (mNavAgent.pathStatus == NavMeshPathStatus.PathComplete && mNavAgent.remainingDistance <= 1.5f)
                {
                    mCurrentPosition = transform.position;
                    mCurrentRotation = transform.rotation;
                    mNavAgent.isStopped = true;
                    mCurrentState = State.Idle;
                }
                mAnimator.SetBool("IsWalking", true);
                Debug.Log("I am Runnin."); // this is just for testing purposes
                break;

            case State.Working:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                Debug.Log("Work work work all day long."); // this is just for testing purposes
                break;

            case State.Chasing:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                mAnimator.SetBool("IsWalking", true);
                mAnimator.SetBool("IsShootAndWalk", true);
                Debug.Log("I'm gonna get him."); // this is just for testing purposes
                break;

            case State.Fleeing:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                mAnimator.SetBool("IsWalking", true);
                Debug.Log("He's scary im leaving."); // this is just for testing purposes
                break;

            case State.Attacking:
                // idle animation?
                // idle sound effects?
                mAnimator.SetBool("IsShooting", true);
                Debug.Log("Kill kill kill"); // this is just for testing purposes
                break;

            case State.Patrol:
                // idle animation?
                // idle sound effects

                var waypointPosition = mAIBaseWaypointsToFollow[mCurrentBaseWaypointPosition].position;
                var nearNextWaypoint = transform.InverseTransformPoint(new Vector3(waypointPosition.x, transform.position.y, waypointPosition.z));
                mNavAgent.SetDestination(waypointPosition);
                mNavAgent.speed = mData.GetMovementSpeed;
                mAnimator.SetBool("IsWalking", true);
                if (nearNextWaypoint.sqrMagnitude < mAIBaseWaypointDistanceCheck)
                {
                    mCurrentBaseWaypointPosition += 1;

                    if (mCurrentBaseWaypointPosition == mAIBaseWaypointsToFollow.Length)
                    {
                        mCurrentBaseWaypointPosition = 0;
                    }
                }
                Debug.Log("Patroling"); // this is just for testing purposes
                break;

            case State.Dead:
                // idle animation?
                // idle sound effects?
                mIsAlive = false;
                gameObject.SetActive(false);
                Debug.Log("Ohh well maybe next time"); // this is just for testing purposes
                break;
        }

        if (mSelected)
        {
            // What happens when selected??
            // Sound? Image change? Menu Pop Up?
            mRenderer.material.color = Color.red; // this is just for testing purposes
        }
        else
        {
            // What happens when unselecting??
            // Sound? Image change? Menu Pop Up?
            var normalColor = new Color(1f, 0.61f, 0.61f, 1);
            mRenderer.material.color = normalColor; // this is just for testing purposes
        }

        //base.Update();
    }


    public void Selected()
    {
        mSelected = !mSelected;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            other.gameObject.GetComponent<FighterUnit>().SetAttack(gameObject);
            mNavAgent.SetDestination(mEnemyTarget.transform.position);
            mNavAgent.speed = mData.GetMovementSpeed;
            mNavAgent.isStopped = false;
            mCurrentState = State.Chasing;
        }
    }*/
}
