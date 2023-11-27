using UnityEngine;

public class TerrainController : MonoBehaviour
{
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
            if (Physics.Raycast(targetLocation, out hit))
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
