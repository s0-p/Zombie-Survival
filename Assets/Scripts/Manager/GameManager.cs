using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();

            return _instance;
        }
    }
    //---------------------------------
    int _coin = 0;
    //---------------------------------
    public int _maxStage = 2;
    int _stage = 1;
    //---------------------------------
    public GameObject _spawnPoints;
    //---------------------------------
    public bool IsGameOver { get; private set; }
    //-------------------------------------------------------------
    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        FindObjectOfType<PlayerHealth>().OnDeath +=
            () =>
            {
                IsGameOver = true;
                UIManager.Instance.SetActiveGameoverUI(true);
            };
    }
    //-------------------------------------------------------------
    public void AddScore(int score)
    {
        if (!IsGameOver)
        {
            _coin += score;
            UIManager.Instance.UpdateCoinText(_coin);
        }
    }
}
