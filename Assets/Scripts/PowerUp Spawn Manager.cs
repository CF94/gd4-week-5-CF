using UnityEngine;

public class PowerUpSpawnManager : MonoBehaviour
{
    [SerializeField] float spawnRange = 20;
    public GameObject[] powerUpPrefab;
    public Transform powerUpParent;
    private float spawnDelay = 10f;
    private float initialSpawnDelay = 5f;
    void Start()
    {
        InvokeRepeating("PowerUpSpawn", initialSpawnDelay, spawnDelay);
    }
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.X))
        {
            PowerUpSpawn();
        }
    }
    Vector3 spawnPosition()
    {
        float spawnPositionX = Random.Range(-spawnRange, spawnRange);
        float spawnPositionZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomSpawnPosition = new Vector3(spawnPositionX, 0, spawnPositionZ);
        return randomSpawnPosition;
    }
    void PowerUpSpawn()
    {
        int randomIndex = Random.Range(0, powerUpPrefab.Length);
        Instantiate(powerUpPrefab[randomIndex], spawnPosition(), Quaternion.identity, powerUpParent);
    }
}
