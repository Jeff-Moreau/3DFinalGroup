using UnityEngine;

public class BunkerController : MonoBehaviour
{
    [SerializeField] private GameObject mRubble = null;
    [SerializeField] private ParticleSystem mExplosion = null;

    private float mCurrentHealth;

    public float GetHealth => mCurrentHealth;
    public void SetHealth(float amount) => mCurrentHealth -= amount;

    private void Start()
    {
        mCurrentHealth = 1000;
    }

    private void Update()
    {
        Debug.Log(mCurrentHealth);

        if (mCurrentHealth <= 0)
        {
            mExplosion.gameObject.SetActive(true);
            mRubble.SetActive(true);
            gameObject.SetActive(false);
            Debug.Log("Dead");
        }
    }
}