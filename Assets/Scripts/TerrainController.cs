using UnityEngine;

public class TerrainController : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [SerializeField] private LayerMask mGround;

    // LOCAL VARIABLES
    private bool mAreUnitsSelected;

    private void OnEnable()
    {
        Actions.UnitSelected += SelectedUnits;
    }

    private void Start()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        mAreUnitsSelected = false;
    }

    private void SelectedUnits()
    {
        mAreUnitsSelected = true;
    }

    private void OnMouseDown()
    {
        if (mAreUnitsSelected)
        {
            Ray targetLocation = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(targetLocation, out hit, 100, mGround))
            {
                Actions.UnitMove?.Invoke(hit);
            }
        }
    }

    private void OnDisable()
    {
        Actions.UnitSelected -= SelectedUnits;
    }
}
