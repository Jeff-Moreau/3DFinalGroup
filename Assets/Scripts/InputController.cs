using UnityEngine;

public class InputController : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [SerializeField] private LayerMask mGround;
    [SerializeField] private LayerMask mUnit;

    [Header("Camera Stuff")]
    [SerializeField] private Camera mMainCamera;
    [SerializeField] private CameraData mCameraData;

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
        MoveCameraMouseEdge();
        mMainCamera.transform.position = mCameraPosition;
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
        if (Input.mouseScrollDelta.y < 0 && mMainCamera.transform.position.y < mCameraData.GetMaxZoomDistance)
        {
            Debug.Log("Scroll Plus");
            mCameraPosition.y += mCameraData.GetZoomSpeed;
        }
        else if (Input.mouseScrollDelta.y > 0 && mMainCamera.transform.position.y > mCameraData.GetMinZoomDistance)
        {
            Debug.Log("Scroll Minus");
            mCameraPosition.y -= mCameraData.GetZoomSpeed;
        }
    }

    private void MoveCameraMouseEdge()
    {
        if (Input.mousePosition.x >= Screen.width - 100)
        {
            mCameraPosition += Vector3.right * Time.deltaTime * 15;
        }
        else if (Input.mousePosition.x <= (Screen.width / Screen.width) + 100)
        {
            mCameraPosition += -Vector3.right * Time.deltaTime * 15;
        }
        if (Input.mousePosition.y >= Screen.height - 100)
        {
            mCameraPosition += Vector3.forward * Time.deltaTime * 15;
        }
        else if (Input.mousePosition.y <= (Screen.height / Screen.height) + 100)
        {
            mCameraPosition += -Vector3.forward * Time.deltaTime * 15;
        }
    }
}