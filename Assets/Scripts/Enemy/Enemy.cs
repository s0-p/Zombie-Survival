using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : LivingEntity
{
    Animator _animator;
    Renderer _renderer;
    //--------------------------------------
    public AudioClip _acDeath;
    public AudioClip _acHit;
    AudioSource _audioSrc;
    //--------------------------------------
    public ParticleSystem _hitEffect;
    //--------------------------------------
    public float _damage = 20f;
    public float _attackTime = 0.5f;
    public float _lastAttackTime = 0f;
    //--------------------------------------
    public LayerMask _targetLaterMask;
    LivingEntity _targetEntity;
    NavMeshAgent _pathFinder;
    //--------------------------------------
    bool HasTarget
    {
        get
        {
            if (_targetEntity != null && !_targetEntity.IsDead)
                return true;

            return false;
        }
    }
    public void Setup(float health, float damage, float speed, Color skinColor)
    {
        _startingHealth = health;
        Health = health;
        _damage = damage;
        _pathFinder.speed = speed;
        _renderer.material.color = skinColor;
    }
    //-------------------------------------------------------------------
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponentInChildren<Renderer>();
        _audioSrc = GetComponent<AudioSource>();
        _pathFinder = GetComponent<NavMeshAgent>();
    }
    //-------------------------------------------------------------------
    void Start()
    {
        StartCoroutine(Crt_UpdatePath());
    }
    IEnumerator Crt_UpdatePath()
    {
        while (!IsDead)
        {
            if (HasTarget)
            {
                _pathFinder.isStopped = false;
                _pathFinder.SetDestination(_targetEntity.transform.position);
            }
            else
            {
                _pathFinder.isStopped = true;

                Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, _targetLaterMask);
                for (int cur = 0; cur < colliders.Length; ++cur)
                {
                    LivingEntity entity = colliders[cur].GetComponent<LivingEntity>();
                    if (entity != null && !entity.IsDead)
                    {
                        _targetEntity = entity;
                        break;
                    }
                }
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
    //-------------------------------------------------------------------
    void Update()
    {
        _animator.SetBool(nameof(HasTarget), HasTarget);
    }
    //-------------------------------------------------------------------
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!IsDead)
        {
            _hitEffect.transform.SetPositionAndRotation(
                        hitPoint,
                        Quaternion.LookRotation(hitNormal)
                    );
            _hitEffect.Play();
            base.OnDamage(damage, hitPoint, hitNormal);
        }
    }
    //-------------------------------------------------------------------
    public override void Die()
    {
        base.Die();

        Collider[] myColliders = GetComponents<Collider>();
        foreach (Collider tmpColl in myColliders)
            tmpColl.enabled = false;

        _pathFinder.isStopped = true;
        _pathFinder.enabled = false;

        _animator.SetTrigger(nameof(Die));
        _audioSrc.PlayOneShot(_acDeath);
    }
    //-------------------------------------------------------------------
    void OnTriggerStay(Collider other)
    {
        if (!IsDead && Time.time >= _lastAttackTime + _attackTime)
        {
            LivingEntity attackTarget = other.GetComponent<LivingEntity>();
            if (attackTarget != null && attackTarget == _targetEntity)
            {
                _lastAttackTime = Time.time;

                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;
                attackTarget.OnDamage(_damage, hitPoint, hitNormal);
            }
        }
    }
}
