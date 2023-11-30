using UnityEngine;
using UnityEngine.AI;

public class FighterUnit : UnitController, ISelectable
{
    // INSPECTOR VARIABLES
    [SerializeField] private UnitData mData;
    [SerializeField] private AudioSource mGunSource;

    private GameObject mEnemyContainer;
    private AIFighterUnit[] mEnemyList;
    private AIFighterUnit mEnemyTarget;
    private float mReloadTime;
    private float mCountTime;

    private void Awake()
    {
        mNavAgent = GetComponent<NavMeshAgent>();
        mAnimator = GetComponent<Animator>();
        mEnemyContainer = GameObject.Find("ObjectPools");
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        mNavAgent.speed = mData.GetMovementSpeed;
        mSelected = false;
        mPlayerControled = false;
        mCurrentState = State.Idle;
        mCurrentPosition = transform.position;
        mCurrentRotation = transform.rotation;
        mEnemyList = mEnemyContainer.GetComponentsInChildren<AIFighterUnit>();
        Debug.Log(mEnemyList.Length);
        mEnemyTarget = null;
        mReloadTime = 1.5f;
        mCountTime = 0;
    }

    new private void Update()
    {
        mCountTime += Time.deltaTime;

        switch (mCurrentState)
        {
            case State.Idle:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                transform.position = new Vector3(mCurrentPosition.x, transform.position.y, mCurrentPosition.z);
                transform.rotation = mCurrentRotation;
                mAnimator.SetBool("IsWalking", false);
                mAnimator.SetBool("IsShootAndWalk", false);
                mAnimator.SetBool("IsShooting", false);
                mPlayerControled = false;
                mEnemyTarget = null;
                Debug.Log("I am Idle. Please do something with me."); // this is just for testing purposes
                break;

            case State.Moving:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                mAnimator.SetBool("IsShooting", false);
                mAnimator.SetBool("IsShootAndWalk", false);
                mAnimator.SetBool("IsWalking", true);

                if (mNavAgent.pathStatus == NavMeshPathStatus.PathComplete && mNavAgent.remainingDistance <= 1)
                {
                    mCurrentPosition = transform.position;
                    mCurrentRotation = transform.rotation;
                    mNavAgent.isStopped = true;
                    mCurrentState = State.Idle;
                }

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
                mAnimator.SetBool("IsWalking", false);
                mAnimator.SetBool("IsShooting", false);
                mAnimator.SetBool("IsShootAndWalk", true);

                if (Vector3.Distance(mEnemyTarget.transform.position, transform.position) <= mData.GetAttackDistance)
                {
                    mCurrentPosition = transform.position;
                    mCurrentRotation = transform.rotation;
                    mNavAgent.isStopped = true;
                    mCurrentState = State.Attacking;
                }

                Debug.Log("I'm gonna get him."); // this is just for testing purposes
                break;

            case State.Fleeing:
                // idle animation?
                // idle sound effects?
                // checking range for bad guys
                mAnimator.SetBool("IsWalking", true);
                mAnimator.SetBool("IsShooting", false);
                mAnimator.SetBool("IsShootAndWalk", false);
                Debug.Log("He's scary im leaving."); // this is just for testing purposes
                break;

            case State.Attacking:
                // idle animation?
                // idle sound effects?
                mAnimator.SetBool("IsShooting", true);
                mAnimator.SetBool("IsWalking", false);
                mAnimator.SetBool("IsShootAndWalk", false);

                var testPosition = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);

                if (Physics.Raycast(testPosition, transform.forward, out RaycastHit target, Mathf.Infinity))
                {
                    Debug.Log("Im hitting an enemy " + target.collider.gameObject);
                    Debug.Log("Should be " + mEnemyTarget);
                    if (target.collider.gameObject == mEnemyTarget.gameObject)
                    {
                        if (mCountTime > mReloadTime)
                        {
                            if (!mGunSource.isPlaying)
                            {
                                mGunSource.Play();
                            }
                            Debug.Log("PewPew");
                            mCountTime = 0;
                        }
                    }
                }
                Debug.DrawRay(testPosition, transform.forward * 100, Color.magenta);
                 
                Debug.Log("Kill kill kill"); // this is just for testing purposes
                break;

            case State.Dead:
                // idle animation?
                // idle sound effects?
                mAnimator.SetBool("IsShooting", false);
                mAnimator.SetBool("IsWalking", false);
                mAnimator.SetBool("IsShootAndWalk", false);
                Debug.Log("Ohh well maybe next time"); // this is just for testing purposes
                break;
        }

        if (mSelected)
        {
            // What happens when selected??
            // Sound? Image change? Menu Pop Up?
            mRenderer.material.color = Color.green; // this is just for testing purposes
        }
        else
        {
            // What happens when unselecting??
            // Sound? Image change? Menu Pop Up?
            mRenderer.material.color = Color.white; // this is just for testing purposes
        }

        DetectAIEnemy();
        MouseHover();
        base.Update();
    }

    private void MouseHover()
    {
        // mouse hover over unit to light it up
        var hoverMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(hoverMouse, out RaycastHit target))
        {
            if (target.collider.gameObject == this.gameObject)
            {
                mRenderer.material.color = Color.green;
            }
        }
    }

    public void Selected()
    {
        if (mSelected && Input.GetKey(KeyCode.LeftShift))
        {
            var otherUnits = FindObjectsOfType<FighterUnit>();
            foreach (var otherUnit in otherUnits)
            {
                otherUnit.mSelected = true;
            }
        }
        else
        {
            mSelected = true;
            //mSelected = !mSelected;
        }
    }

    private void DetectAIEnemy()
    {
        foreach (AIFighterUnit enemyPosition in mEnemyList)
        {
            var distanceBetween = Vector3.Distance(enemyPosition.transform.position, transform.position);

            if(distanceBetween > mData.GetAttackDistance && distanceBetween < mData.GetViewDistance)
            {
                if (!mPlayerControled && mEnemyTarget == null)
                {
                    mEnemyTarget = enemyPosition;
                    mNavAgent.SetDestination(mEnemyTarget.transform.position);
                    mNavAgent.speed = mData.GetMovementSpeed;
                    mNavAgent.isStopped = false;
                    mCurrentState = State.Chasing;
                }
            }
        }
    }
}
