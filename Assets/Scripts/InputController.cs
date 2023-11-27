using UnityEngine;

public class InputController : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [SerializeField] private LayerMask mGround;
    [SerializeField] private LayerMask mUnit;

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {

    }

    private void Update()
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

        if (Input.GetMouseButtonDown(1))
        {
            var location = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(location, out RaycastHit hit))
            {
                Actions.UnitMove.Invoke(hit);
            }
        }

        var hoverMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(hoverMouse, out RaycastHit target))
        {
            target.collider.gameObject.TryGetComponent<IHighlightable>(out IHighlightable item);
            item?.MouseHover();
        }
    }
}