using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string _moveAxisName = "Vertical";
    public string _rotateAxisName = "Horizontal";
    public string _fireButtonName = "Fire1";
    public string _reloadButtonName = "Reload";

    public float Move { get; private set; }
    public float Rotate { get; private set; }
    public bool Fire { get; private set; }
    public bool Reload { get; private set; }

    void Update()
    {
        Move = Input.GetAxis(_moveAxisName);
        Rotate = Input.GetAxis(_rotateAxisName);

        Fire = Input.GetButton(_fireButtonName);
        Reload = Input.GetButtonDown(_reloadButtonName);
    }
}
