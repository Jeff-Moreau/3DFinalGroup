using UnityEngine;

public class LoadingManager : MonoBehaviour
{
    // SINGLETON STARTS
    private static LoadingManager myInstance;
    private void Singleton()
    {
        if (myInstance != null && myInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            myInstance = this;
        }
    }

    public static LoadingManager Load => myInstance;
    // SINGLETON ENDS

    // INSPECTOR VARIABLES
    [SerializeField] private GameObject[] mMaps = null;
    [SerializeField] private GameObject mPlayerStartBase = null;
    [SerializeField] private GameObject mAIStartBase = null;
    [SerializeField] private Camera mMainCamera = null;
    [SerializeField] private FighterUnitPool mFighterUnitPool = null;

    //MEMBER VARIABLES
    private GameObject[] mBaseSpawnpoints;
    private GameObject[] mUnitSpawnpoints;
    private SpawnpointTaken[] mUnitSpawnpointsTaken;
    private int mTotalUnitsSpawned;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        InitializeVariables();
        CreatePlayerStartLocation();
        CreatePlayerStartingUnits();
        Actions.CameraLoadedPosition.Invoke(mMainCamera.transform.position);
    }
    private void InitializeVariables()
    {
        mBaseSpawnpoints = mMaps[0].GetComponent<MapController>().GetBaseSpawnpoints;
        mUnitSpawnpoints = mMaps[0].GetComponent<MapController>().GetUnitSpawnpoints;
        mUnitSpawnpointsTaken = new SpawnpointTaken[mUnitSpawnpoints.Length];

        for (int i = 0; i < mUnitSpawnpoints.Length; i++)
        {
            mUnitSpawnpointsTaken[i] = mUnitSpawnpoints[i].GetComponent<SpawnpointTaken>();
            mUnitSpawnpointsTaken[i].SetIsTaken(false);
        }

        mTotalUnitsSpawned = 0;

        mFighterUnitPool.SetTotalPrefabsNeeded(mMaps[0].GetComponent<MapController>().GetTotalUnits);
    }

    private void CreatePlayerStartLocation()
    {
        var randomSpawnLocation = Random.Range(0, mBaseSpawnpoints.Length);
        Debug.Log(randomSpawnLocation);
        var newSpawnLocation = new Vector3(mBaseSpawnpoints[randomSpawnLocation].transform.position.x, mBaseSpawnpoints[randomSpawnLocation].transform.position.y, mBaseSpawnpoints[randomSpawnLocation].transform.position.z);
        mMainCamera.transform.position = new Vector3(mBaseSpawnpoints[randomSpawnLocation].transform.position.x, mMainCamera.transform.position.y, mBaseSpawnpoints[randomSpawnLocation].transform.position.z);
        mMainCamera.fieldOfView = 60;
        Instantiate(mPlayerStartBase, newSpawnLocation, mBaseSpawnpoints[randomSpawnLocation].transform.rotation);
    }

    private void CreatePlayerStartingUnits()
    {
        for (int i = 0; i < mUnitSpawnpoints.Length; i++)
        {
            if(!mUnitSpawnpointsTaken[i].GetIsTaken && mTotalUnitsSpawned < mUnitSpawnpoints.Length)
            {
                var newUnit = mFighterUnitPool.GetAvailablePrefabs();

                if (newUnit != null)
                {
                    newUnit.transform.SetPositionAndRotation(mUnitSpawnpoints[i].transform.position, mUnitSpawnpoints[i].transform.rotation);
                    mUnitSpawnpointsTaken[i].SetIsTaken(true);
                    mTotalUnitsSpawned++;
                }

                for (int j = 0; j < mFighterUnitPool.GetPrefabList.Count; j++)
                {
                    newUnit.SetActive(true);
                }
            }
        }
    }
}