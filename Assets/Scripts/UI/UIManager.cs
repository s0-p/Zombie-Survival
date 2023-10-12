using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UIManager>();
            return _instance;
        }
    }
    //----------------------------------
    public Text _ammoText;
    public Text _scoreText;
    public Text _waveText;
    public GameObject _gameoverUI;
    public Button _onRestart;
    //------------------------------------------------------------
    void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject); return;
    }
    void Start()
    {
        SetActiveGameoverUI(false);
        _onRestart.onClick.AddListener(GameRestart);
    }
    //------------------------------------------------------------
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    { _ammoText.text = magAmmo + "/" + remainAmmo; }
    public void UpdateScoreText(int newScore)
    { _scoreText.text = "Score : " + newScore; }
    public void UpdateWaveText(int waves, int enemyLeftCount)
    { _waveText.text = "Wave : " + waves + "\nEnemy Left : " + enemyLeftCount; }
    public void SetActiveGameoverUI(bool isActive)
    { _gameoverUI.SetActive(isActive); }
    public void GameRestart()
    { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
}
