using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField]
    Transform[] spawnPoints;

    [SerializeField]
    float _minY;

    [SerializeField]
    float _maxY;

    [SerializeField]
    float spawnTime = 5.0F;

    [SerializeField]
    GameObject[] spawnPrefabs;

    [SerializeField]
    float _currentTime;

    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime >= spawnTime)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Vector3 spawnPosition = spawnPoint.position;
            spawnPosition.y = Random.Range(_minY, _maxY);
            spawnPoint.position = spawnPosition;

            GameObject spawnPrefab = spawnPrefabs[Random.Range(0, spawnPrefabs.Length)];
            GameObject spawnObject = Instantiate(spawnPrefab, spawnPoint.position, Quaternion.identity);
            _currentTime = 0.0F;
        }
    }
}
