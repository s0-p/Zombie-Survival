using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float _damageMax = 10f;
    public float _damageMin = 5f;
    public float _damageIncrement = 10f;
    //------------------------------------
    [Space()]
    public float _healthMax = 75f;
    public float _healthMin = 25f;
    public float _healthIncrement = 10f;
    //------------------------------------
    [Space()]
    public float _speedMax = 30f;
    public float _speedMin = 5f;
    public float _speedIncrement = 10f;
    //------------------------------------
    [Space()]
    public Color _strongEnemyColor = Color.red;
    //------------------------------------
    [Space()]
    public Enemy _enemyPref;
    public Transform[] _spawnPoints;
    //------------------------------------
    List<Enemy> _enemies = new List<Enemy>();
    int _maxWave = 1;
    int _curWave = 0;
    //-------------------------------------------------------------
    void Start()
    {
        _curWave = 0;
    }
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        if (_enemies.Count <= 0)
            SpawnWave();

        UpdateUI();
    }
    void UpdateUI() { UIManager.Instance.UpdateWaveText(_curWave, _enemies.Count); }
    void SpawnWave()
    {
        ++_curWave;
        if (_curWave > _maxWave)
        {
            UpStage();
            return;
        }

        int spawnCount = Mathf.RoundToInt(_curWave * 1.5f);
        for (int cur = 0; cur < spawnCount; cur++)
        {
            float enemyIntensity = Random.Range(0f, 1f);
            CreateEnemy(enemyIntensity);
        }
    }
    void CreateEnemy(float intensity)
    {
        float health = Mathf.Lerp(_healthMin, _healthMax, intensity);
        float damage = Mathf.Lerp(_damageMin, _damageMax, intensity);
        float speed = Mathf.Lerp(_speedMin, _speedMax, intensity);
        Color skinColor = Color.Lerp(Color.white, _strongEnemyColor, intensity);

        Transform spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        Enemy enemy = Instantiate(_enemyPref, spawnPoint.position, spawnPoint.rotation);
        enemy.Setup(health, damage, speed, skinColor);
        _enemies.Add(enemy);

        enemy.OnDeath += () => _enemies.Remove(enemy);
        enemy.OnDeath += () => Destroy(enemy.gameObject, 10f);
        enemy.OnDeath += () => GameManager.Instance.AddScore(100);
    }
    void UpStage()
    {
        _curWave = 0;

        _damageMin += _damageIncrement;
        _damageMax += _damageIncrement;

        _healthMin += _healthIncrement;
        _healthMax += _healthIncrement;

        _speedMin += _speedIncrement;
        _speedMax += _speedIncrement;
    }
}
