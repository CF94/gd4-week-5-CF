using Mono.Cecil;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] float spawnRange = 20;
    public GameObject[] enemyPrefab;
    public GameObject enemyBossPrefab;
    public int enemyBossCount;
    public int maxEnemyBossCount = 1;
    public Transform enemyParent;
    public int enemyCount;
    public int waveNumber;
    void Start()
    {
        //InvokeRepeating("EnemySpawn", initialSpawnDelay, spawnDelay);
        SpawnEnemyWave(waveNumber);
    }
    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            EnemySpawn();
        }

        //enemyCount = FindObjectsByType<EnemyMovement>(FindObjectsSortMode.None).Length;
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        //enemyCount = FindFirstObjectByType<Enemy>().Length;
        //enemyCount = GameObject.FindWithTag(EnemyMovement);
        if(enemyCount <= 0)
        {
            //spawn new wave, increase no. of enemies to spawn
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
        if(waveNumber == 5 && enemyBossCount <= 0)
        {
            EnemySpawn();
        }
    }
    Vector3 spawnPosition()
    {
        float spawnPositionX = Random.Range(-spawnRange, spawnRange);
        float spawnPositionZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomSpawnPosition = new Vector3(spawnPositionX, 0, spawnPositionZ);
        return randomSpawnPosition;
    }
    
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomIndex], spawnPosition(), Quaternion.identity, enemyParent);
        }
    }
    void EnemySpawn()
    {
        //int randomIndex = Random.Range(0, enemyPrefab.Length);
        Instantiate(enemyBossPrefab, spawnPosition(), Quaternion.identity, enemyParent);
        enemyBossCount++;
    }
}
