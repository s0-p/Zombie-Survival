using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum STATE
    {
        READY, 
        EMPTY,
        RELOADING
    }
    public STATE CurState { get; private set; }
    //-------------------------------------------
    public float _damage = 25f;
    float _fireDist = 50;
    //-------------------------------------------
    public int _ammoRemain = 100;
    public int _magCapacity = 25;
    public int _magAmo;
    //-------------------------------------------
    public float _timeBetfire = 0.12f;
    public float _reloadTime = 1.8f;
    float _lastFireTime;
    //-------------------------------------------
    public Transform _trsfFire;
    //-------------------------------------------
    public ParticleSystem _muzzleFlashEffect;
    public ParticleSystem _shellEjectEffect;
    //-------------------------------------------
    LineRenderer _bulletLineRenderer;
    //-------------------------------------------
    AudioSource _gunAudioPlayer;
    public AudioClip _shotClip;
    public AudioClip _reloadClip;
    //--------------------------------------------------------------------
    void Awake()
    {
        _gunAudioPlayer = GetComponent<AudioSource>();
        _bulletLineRenderer = GetComponent<LineRenderer>();
        _bulletLineRenderer.positionCount = 2;
        _bulletLineRenderer.enabled = false;
    }
    void OnEnable()
    {
        _magAmo = _magCapacity;
        CurState = STATE.READY;
        _lastFireTime = 0f;
    }
    //--------------------------------------------------------------------
    public void Fire()
    {
        if (CurState == STATE.READY && Time.time >= _lastFireTime + _timeBetfire)
        {
            _lastFireTime = Time.time;
            Shot();
        }
    }
    void Shot()
    {
        Vector3 hitPos = Vector3.zero;
        if (Physics.Raycast(_trsfFire.position, _trsfFire.forward, out RaycastHit hit, _fireDist))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(_damage, hit.point, hit.normal);
            }
            hitPos = hit.point;
        }
        else
            hitPos = _trsfFire.position + _trsfFire.forward * _fireDist;
        
        StartCoroutine(Crt_ShotEffect(hitPos));
        --_magAmo;

        if (_magAmo <= 0)
            CurState = STATE.EMPTY;
    }
    IEnumerator Crt_ShotEffect(Vector3 hitPos)
    {
        _muzzleFlashEffect.Play();
        _shellEjectEffect.Play();

        _gunAudioPlayer.PlayOneShot(_shotClip);
        _bulletLineRenderer.SetPosition(0, _trsfFire.position);
        _bulletLineRenderer.SetPosition(1, hitPos);
        _bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);
        _bulletLineRenderer.enabled = false;
    }
    //--------------------------------------------------------------------
    public bool Reload()
    {
        if (CurState == STATE.RELOADING || _ammoRemain <= 0 || _magAmo >= _magCapacity)
            return false;

        StartCoroutine(Crt_ReloadRoutine());
        return true;
    }
    IEnumerator Crt_ReloadRoutine()
    {
        CurState = STATE.RELOADING;
        _gunAudioPlayer.PlayOneShot(_reloadClip);
        yield return new WaitForSeconds(_reloadTime);

        int ammoToFill = _magCapacity - _magAmo;
        if (_ammoRemain < ammoToFill)
            ammoToFill = _ammoRemain;

        _magAmo += ammoToFill;
        _ammoRemain -= ammoToFill;

        CurState = STATE.READY;
    }
}
