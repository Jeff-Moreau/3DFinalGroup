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
    [SerializeField] private AIFighterUnitPool mAIFighterUnitPool = null;

    //MEMBER VARIABLES
    private GameObject[] mBaseSpawnpoints;
    private GameObject[] mUnitSpawnpoints;
    private SpawnpointTaken[] mUnitSpawnpointsTaken;
    private int mTotalUnitsSpawned;
    private GameObject[] mAIBaseSpawnpoints;
    private GameObject[] mAIUnitSpawnpoints;
    private SpawnpointTaken[] mAIUnitSpawnpointsTaken;
    private int mTotalAIUnitsSpawned;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        InitializeVariables();
        CreatePlayerStartLocation();
        CreatePlayerStartingUnits();
        CreateAIStartLocation();
        CreateAIStartingUnits();
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

        mAIBaseSpawnpoints = mMaps[0].GetComponent<MapController>().GetAIBaseSpawnpoints;
        mAIUnitSpawnpoints = mMaps[0].GetComponent<MapController>().GetAIUnitSpawnpoints;
        mAIUnitSpawnpointsTaken = new SpawnpointTaken[mAIUnitSpawnpoints.Length];

        for (int i = 0; i < mAIUnitSpawnpoints.Length; i++)
        {
            mAIUnitSpawnpointsTaken[i] = mAIUnitSpawnpoints[i].GetComponent<SpawnpointTaken>();
            mAIUnitSpawnpointsTaken[i].SetIsTaken(false);
        }

        mTotalAIUnitsSpawned = 0;

        mAIFighterUnitPool.SetTotalPrefabsNeeded(mMaps[0].GetComponent<MapController>().GetTotalUnits);
    }

    private void CreateAIStartLocation()
    {
        var randomSpawnLocation = Random.Range(0, mAIBaseSpawnpoints.Length);
        var newSpawnLocation = new Vector3(mAIBaseSpawnpoints[randomSpawnLocation].transform.position.x, mAIBaseSpawnpoints[randomSpawnLocation].transform.position.y, mAIBaseSpawnpoints[randomSpawnLocation].transform.position.z);
        Instantiate(mAIStartBase, newSpawnLocation, mAIBaseSpawnpoints[randomSpawnLocation].transform.rotation);
    }

    private void CreatePlayerStartLocation()
    {
        var randomSpawnLocation = Random.Range(0, mBaseSpawnpoints.Length);
        var newSpawnLocation = new Vector3(mBaseSpawnpoints[randomSpawnLocation].transform.position.x, mBaseSpawnpoints[randomSpawnLocation].transform.position.y, mBaseSpawnpoints[randomSpawnLocation].transform.position.z);
        mMainCamera.transform.position = new Vector3(mBaseSpawnpoints[randomSpawnLocation].transform.position.x, mMainCamera.transform.position.y, mBaseSpawnpoints[randomSpawnLocation].transform.position.z);
        mMainCamera.fieldOfView = 60;
        Instantiate(mPlayerStartBase, newSpawnLocation, mBaseSpawnpoints[randomSpawnLocation].transform.rotation);
    }

    private void CreateAIStartingUnits()
    {
        for (int i = 0; i < mAIUnitSpawnpoints.Length; i++)
        {
            if(!mAIUnitSpawnpointsTaken[i].GetIsTaken && mTotalAIUnitsSpawned < mAIUnitSpawnpoints.Length)
            {
                var newUnit = mAIFighterUnitPool.GetAvailablePrefabs();

                if (newUnit != null)
                {
                    newUnit.transform.SetPositionAndRotation(mAIUnitSpawnpoints[i].transform.position, mAIUnitSpawnpoints[i].transform.rotation);
                    mAIUnitSpawnpointsTaken[i].SetIsTaken(true);
                    mTotalAIUnitsSpawned++;
                }

                for (int j = 0; j < mAIFighterUnitPool.GetPrefabList.Count; j++)
                {
                    newUnit.SetActive(true);
                }
            }
        }
    }

    private void CreatePlayerStartingUnits()
    {
        for (int i = 0; i < mUnitSpawnpoints.Length; i++)
        {
            if (!mUnitSpawnpointsTaken[i].GetIsTaken && mTotalUnitsSpawned < mUnitSpawnpoints.Length)
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