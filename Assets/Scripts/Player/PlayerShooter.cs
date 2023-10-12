using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    PlayerInput _playerInput;
    Animator _animator;
    //---------------------------------
    public Gun _gun;
    //---------------------------------
    public Transform _trsfGunPivot;
    public Transform _trsfLHandMount;
    public Transform _trsfRHandMount;
    //--------------------------------------------------------------------
    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }
    void OnEnable() { _gun.gameObject.SetActive(true); }
    void OnDisable() { _gun.gameObject.SetActive(false); }
    void Update()
    {
        if (_playerInput.Fire)
            _gun.Fire();

        else if (_playerInput.Reload)
        {
            if (_gun.Reload())
                _animator.SetTrigger("Reload");
        }
        UpdateUI();
    }
    void UpdateUI()
    {
        if (_gun != null && UIManager.Instance != null)
            UIManager.Instance.UpdateAmmoText(_gun._magAmo, _gun._ammoRemain);
    }
    void OnAnimatorIK(int layerIndex)
    {
        _trsfGunPivot.position = _animator.GetIKHintPosition(AvatarIKHint.RightElbow);

        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        _animator.SetIKPosition(AvatarIKGoal.LeftHand, _trsfLHandMount.position);
        _animator.SetIKRotation(AvatarIKGoal.LeftHand, _trsfLHandMount.rotation);

        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        _animator.SetIKPosition(AvatarIKGoal.RightHand, _trsfRHandMount.position);
        _animator.SetIKRotation(AvatarIKGoal.RightHand, _trsfRHandMount.rotation);
    }
}
