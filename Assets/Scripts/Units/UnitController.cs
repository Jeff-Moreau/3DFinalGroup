using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour, IHighlightable
{
    // ENUMS FOR STATE MACHINE
    protected enum State
    {
        Idle,
        Moving,
        Working,
        Chasing,
        Fleeing,
        Attacking,
        Dead
    }

    // INSPECTOR VARIABLES
    [SerializeField] protected Rigidbody mRigidBody = null;
    [SerializeField] protected Renderer mRenderer = null;

    // MEMBERS VARIABLES
    protected bool mSelected;
    protected bool mPlayerControled;
    protected Animator mAnimator;
    protected NavMeshAgent mNavAgent;
    protected State mCurrentState;
    protected LayerMask mMask;
    protected Vector3 mCurrentPosition;
    protected Quaternion mCurrentRotation;

    // GETTERS
    public bool GetSelected => mSelected;
    public Vector3 GetCurrentPosition => mCurrentPosition;

    // SETTERS
    public void SetSelected(bool yesno) => mSelected = yesno;

    private void OnEnable()
    {
        Actions.DeSelect += DeSelectUnit;
        Actions.UnitMove += MoveUnit;
    }

    protected void Update()
    {
        if (InputManager.Load.IsSelecting)
        {
            var myLocationOnCamera = Camera.main.WorldToScreenPoint(transform.position);
            myLocationOnCamera.y = Screen.height - myLocationOnCamera.y;
            mSelected = InputManager.Load.GetUnitSelectionBox.Contains(myLocationOnCamera);
        }
    }

    protected void DeSelectUnit()
    {
        if (mSelected)
        {
            mSelected = false;
        }
    }

    protected void MoveUnit(RaycastHit location)
    {
        if (mSelected)
        {
            mNavAgent.SetDestination(location.point);
            mNavAgent.isStopped = false;
            mCurrentState = State.Moving;
            mPlayerControled = true;
        }
    }

    public void MouseHover()
    {
        mRenderer.material.color = Color.green;
    }

    private void OnDisable()
    {
        Actions.DeSelect -= DeSelectUnit;
        Actions.UnitMove -= MoveUnit;
    }
}