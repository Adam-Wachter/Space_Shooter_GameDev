using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyPrefab;
    [SerializeField]
    GameObject _enemyContainer;
    [SerializeField]
    List<GameObject> _powerupsToSpawn;
    [SerializeField]
    List<GameObject> _asteroidsToSpawn;
    [SerializeField]
    float _minTimeAsteroids = 7f;
    [SerializeField]
    float _maxTimeAsteroids = 10;

    bool _stopSpawning = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawning()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(SpawnPowerups());
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator EnemySpawn()
    {
        float minTime = 2f;
        float maxTime = 5f;

        yield return new WaitForSeconds(1);

        while (_stopSpawning == false)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9f, 9f), 8, 0);
            GameObject enemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }

    IEnumerator SpawnPowerups()
    {
        float minTime = 7f;
        float maxTime = 14f;

        yield return new WaitForSeconds(Random.Range(minTime, maxTime));

        while (_stopSpawning == false)
        {
            if (_powerupsToSpawn.Count > 0)
            {
                int randomIndex = Random.Range(0, _powerupsToSpawn.Count);
                GameObject prefabToSpawn = _powerupsToSpawn[randomIndex];
                Vector3 spawnPosition = new Vector3(Random.Range(-9f, 9f), 8, 0);
                GameObject powerup = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        }
    }

    IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(Random.Range(_minTimeAsteroids, _maxTimeAsteroids));

        while (_stopSpawning == false)
        {
            if (_asteroidsToSpawn.Count > 0)
            {
                int randomIndex = Random.Range(0, _asteroidsToSpawn.Count);
                GameObject prefabToSpawn = _asteroidsToSpawn[randomIndex];
                Vector3 spawnPosition = new Vector3(Random.Range(-9f, 9f), 8, 0);
                GameObject asteroid = Instantiate(prefabToSpawn,spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(Random.Range(_minTimeAsteroids, _maxTimeAsteroids));
        }
    }

    public void StopEnemySpawn()
    {
        _stopSpawning = true;
    }
}
