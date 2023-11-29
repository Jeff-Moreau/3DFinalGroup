using UnityEngine;

public class MapController : MonoBehaviour
{
    // INSPECTOR VARIABLES
    [SerializeField] private GameObject mSpawnPointContainer;
    [SerializeField] private GameObject[] mSpawnPoints;

    // GETTERS
    public GameObject GetSpawnPointContainer => mSpawnPointContainer;
    public GameObject[] GetSpawnPoints => mSpawnPoints;
}
