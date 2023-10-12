using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    public float _speed = 60f;
    void Update() { transform.Rotate(0f, _speed * Time.deltaTime, 0f); }
}
