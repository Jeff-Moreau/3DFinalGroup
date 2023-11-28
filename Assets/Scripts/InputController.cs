using UnityEngine;

public class InputController : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [SerializeField] private LayerMask mGround;
    [SerializeField] private LayerMask mUnit;
    [SerializeField] private Camera mMainCamera;

    private Vector3 mCameraPosition;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        mCameraPosition = mMainCamera.transform.position;
    }

    private void Update()
    {
        CameraZoom();
        SelectDeselectUnit();
        MoveUnitToLocation();
        MouseHover();
    }

    private static void SelectDeselectUnit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var location = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(location, out RaycastHit hit))
            {
                hit.collider.gameObject.TryGetComponent<ISelectable>(out ISelectable item);
                item?.Selected();

                if (hit.collider.gameObject.layer == 3)
                {
                    Actions.DeSelect.Invoke();
                }
            }
        }
    }

    private static void MoveUnitToLocation()
    {
        if (Input.GetMouseButtonDown(1))
        {
            var location = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(location, out RaycastHit hit))
            {
                Actions.UnitMove.Invoke(hit);
            }
        }
    }

    private static void MouseHover()
    {
        var hoverMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(hoverMouse, out RaycastHit target))
        {
            target.collider.gameObject.TryGetComponent<IHighlightable>(out IHighlightable item);
            item?.MouseHover();
        }
    }

    private void CameraZoom()
    {
        if (Input.mouseScrollDelta.y < 0 && mMainCamera.transform.position.y < 30)
        {
            Debug.Log("Scroll Plus");
            mCameraPosition.y += 1;
        }
        else if (Input.mouseScrollDelta.y > 0 && mMainCamera.transform.position.y > 10)
        {
            Debug.Log("Scroll Minus");
            mCameraPosition.y -= 1;
        }

        mMainCamera.transform.position = mCameraPosition;
    }
}