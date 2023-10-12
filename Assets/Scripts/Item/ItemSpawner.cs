using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
    public Transform _trsfPlayer;
    //----------------------------------
    public float _spawnMaxRadius = 5f;
    //----------------------------------
    public float _spawnTimeMax = 7f;
    public float _spawnTimeMin = 2f;
    float _spawnTime;
    float _lastSpawnTime;
    //----------------------------------
    public GameObject[] _itemPrefs;
    //--------------------------------------------------------------------
    void Start()
    {
        _spawnTime = Random.Range(_spawnTimeMin, _spawnTimeMax);
        _lastSpawnTime = 0f;
    }
    void Update()
    {
        if (Time.time >= _lastSpawnTime + _spawnTime && _trsfPlayer != null)
        {
            _lastSpawnTime = Time.time;
            _spawnTime = Random.Range(_spawnTimeMin, _spawnTimeMax);
            Spawn();
        }
    }
    //--------------------------------------------------------------------
    void Spawn()
    {
        Vector3 spawnPos = GetRandomPointOnNavMesh(_trsfPlayer.position, _spawnMaxRadius);
        spawnPos += Vector3.up * 0.5f;
        
        GameObject selectedItem = _itemPrefs[Random.Range(0, _itemPrefs.Length)];
        GameObject item = Instantiate(selectedItem, spawnPos, Quaternion.identity);
        Destroy(item, 5f);
    }
    Vector3 GetRandomPointOnNavMesh(Vector3 center, float dist)
    {
        Vector3 randomPos = center + Random.insideUnitSphere * dist;

        NavMesh.SamplePosition(randomPos, out NavMeshHit hit, dist, NavMesh.AllAreas);
        return hit.position;
    }
}
