using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : LivingEntity
{
    public Slider _healthSlider;
    public int _upgradeHealth;
    //-------------------------------
    public AudioClip _acDeath;
    public AudioClip _acHit;
    public AudioClip _acItemPickup;
    AudioSource _audioSrc;
    //-------------------------------
    Animator _animator;
    //-------------------------------
    PlayerMovement _playerMovement;
    PlayerShooter _playerShooter;
    //-----------------------------------------------------------------
    void Awake()
    {
        _audioSrc = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerShooter = GetComponent<PlayerShooter>();

    }
    void Start()
    {
        UIManager.Instance._upgradeHealthButton.onClick.AddListener(UpgradeHealth);
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        _healthSlider.gameObject.SetActive(true);
        _healthSlider.maxValue = _startingHealth;
        _healthSlider.value = _startingHealth;

        _playerMovement.enabled = true;
        _playerShooter.enabled = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (IsDead) return;

        IItem item = other.GetComponent<IItem>();
        if (item != null)
        {
            item.Use(gameObject);
            _audioSrc.PlayOneShot(_acItemPickup);
        }
    }
    //-----------------------------------------------------------------
    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        _healthSlider.value = Health;
    }
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (IsDead) return;
        _audioSrc.PlayOneShot(_acHit);

        base.OnDamage(damage, hitPoint, hitNormal);

        _healthSlider.value = Health;
        transform.rotation = Quaternion.LookRotation(hitNormal);
    }
    public override void Die()
    {
        base.Die();

        _healthSlider.gameObject.SetActive(false);
        _audioSrc.PlayOneShot(_acDeath);
        _animator.SetTrigger("Die");

        _playerMovement.enabled = false;
        _playerShooter.enabled = false;
    }
    //-----------------------------------------------------------------
    public void UpgradeHealth() 
    { 
        _startingHealth += _upgradeHealth;
        UIManager.Instance.SetActiveUpgradeUI(false);
    }
}
