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
    public Text _coinText;
    public Text _waveText;
    //----------------------------------
    public GameObject _gameoverUI;
    public Button _onRestart;
    //----------------------------------
    public GameObject _upgradeUI;
    public Button _upgradeHealthButton;
    public Button _upgradeDamageButton;
    public Button _upgradeAmmoButton;
    //------------------------------------------------------------
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        SetActiveGameoverUI(false);
        _onRestart.onClick.AddListener(GameRestart);
        
        SetActiveUpgradeUI(false);
    }
    //------------------------------------------------------------
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    { _ammoText.text = magAmmo + "/" + remainAmmo; }
    public void UpdateCoinText(int newCoin)
    { _coinText.text = "Score : " + newCoin; }
    public void UpdateWaveText(int waves, int enemyLeftCount)
    { _waveText.text = "Wave : " + waves + "\nEnemy Left : " + enemyLeftCount; }
    //--------------------------------------------------------------------
    public void SetActiveGameoverUI(bool isActive)
    { _gameoverUI.SetActive(isActive); }
    public void GameRestart()
    { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }
    //--------------------------------------------------------------------
    public void SetActiveUpgradeUI(bool isActive) { _upgradeUI.SetActive(isActive); }
}
