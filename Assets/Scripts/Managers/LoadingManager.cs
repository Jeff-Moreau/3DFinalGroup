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

    //LOCAL VARIABLES
    private GameObject[] mMapSpawnPoints;

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        InitializeVariables();
        CreatePlayerStartLocation();
    }

    private void CreatePlayerStartLocation()
    {
        var randomSpawnLocation = Random.Range(0, mMapSpawnPoints.Length);
        Debug.Log(randomSpawnLocation);
        var newSpawnLocation = new Vector3(mMapSpawnPoints[randomSpawnLocation].transform.position.x, mMapSpawnPoints[randomSpawnLocation].transform.position.y, mMapSpawnPoints[randomSpawnLocation].transform.position.z);
        mMainCamera.transform.position = new Vector3(mMapSpawnPoints[randomSpawnLocation].transform.position.x, mMainCamera.transform.position.y, mMapSpawnPoints[randomSpawnLocation].transform.position.z);
        Instantiate(mPlayerStartBase, newSpawnLocation, mMapSpawnPoints[randomSpawnLocation].transform.rotation);
        Debug.Log("Update Camera" + mMainCamera.transform.position);
        Actions.CameraLoadedPosition.Invoke(mMainCamera.transform.position);
    }

    private void Update()
    {
        
    }

    private void InitializeVariables()
    {
        mMapSpawnPoints = mMaps[0].GetComponent<MapController>().GetSpawnPoints;
    }
}
