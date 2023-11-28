using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour, ISelectable, IHighlightable
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

    // LOCAL VARIABLES
    protected bool mSelected;
    protected State mCurrentState;
    protected NavMeshAgent mNavAgent;
    protected LayerMask mMask;

    // GETTERS
    public bool GetSelected => mSelected;

    // SETTERS
    public void SetSelected(bool yesno) => mSelected = yesno;

    private void OnEnable()
    {
        Actions.DeSelect += DeSelectUnit;
        Actions.UnitMove += MoveUnit;
    }

    public void Selected()
    {
        if (mSelected && Input.GetKey(KeyCode.LeftShift))
        {
            var otherUnits = FindObjectsOfType<TestUnit>();
            foreach (var otherUnit in otherUnits)
            {
                otherUnit.mSelected = true;
            }
        }
        else
        {
            mSelected = !mSelected;
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