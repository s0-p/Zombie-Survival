using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float _moveSpeed;
    public float _rotSpeed;

    PlayerInput _playerInput;
    Rigidbody _rigidBody;
    Animator _animator;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidBody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        Rotate();
        Move();
        _animator.SetFloat("Speed", _playerInput.Move);
    }
    void Move()
    {
        Vector3 moveDist = _playerInput.Move * transform.forward * _moveSpeed * Time.deltaTime;
        _rigidBody.MovePosition(_rigidBody.position + moveDist);
    }
    void Rotate()
    {
        float turn = _playerInput.Rotate * _rotSpeed * Time.deltaTime;
        _rigidBody.rotation = _rigidBody.rotation * Quaternion.Euler(0, turn, 0);
    }
}
