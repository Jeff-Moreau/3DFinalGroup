using UnityEngine;

public class MapController : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [Header("Player Spawnpoints")]
    [SerializeField] private GameObject mBaseSpawnpointContainer = null;
    [SerializeField] private GameObject[] mBaseSpawnpoints = null;
    [SerializeField] private GameObject mUnitSpawnpointContainer = null;
    [SerializeField] private GameObject[] mUnitSpawnpoints = null;

    [Header("AI Spawnpoints")]
    [SerializeField] private GameObject mAIBaseSpawnpointContainer = null;
    [SerializeField] private GameObject[] mAIBaseSpawnpoints = null;
    [SerializeField] private GameObject mAIUnitSpawnpointContainer = null;
    [SerializeField] private GameObject[] mAIUnitSpawnpoints = null;

    [Header("Starting Units on Map")]
    [SerializeField] private int mTotalUnits = 0;

    // GETTERS
    public GameObject GetBaseSpawnpointContainer => mBaseSpawnpointContainer;
    public GameObject[] GetBaseSpawnpoints => mBaseSpawnpoints;
    public GameObject GetUnitSpawnpointContainer => mUnitSpawnpointContainer;
    public GameObject[] GetUnitSpawnpoints => mUnitSpawnpoints;
    public GameObject GetAIBaseSpawnpointContainer => mAIBaseSpawnpointContainer;
    public GameObject[] GetAIBaseSpawnpoints => mAIBaseSpawnpoints;
    public GameObject GetAIUnitSpawnpointContainer => mAIUnitSpawnpointContainer;
    public GameObject[] GetAIUnitSpawnpoints => mAIUnitSpawnpoints;
    public int GetTotalUnits => mTotalUnits;
}
