using UnityEngine;
using UnityEngine.AI;

public class UnitController : MonoBehaviour
{
    protected enum mState
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
    protected NavMeshAgent mNavAgent;
}