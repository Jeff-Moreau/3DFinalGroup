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
    [SerializeField] private GameObject[] mMaps;
    [SerializeField] private GameObject mPlayerStartBase;
    [SerializeField] private GameObject mAIStartBase;
    [SerializeField] private Camera mMainCamera;

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
    }

    private void Update()
    {
        
    }

    private void InitializeVariables()
    {
        mMapSpawnPoints = mMaps[0].GetComponent<MapController>().GetSpawnPoints;
    }
}
