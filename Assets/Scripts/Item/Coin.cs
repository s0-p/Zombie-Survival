using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IItem
{
    public int _coin = 200;
    public void Use(GameObject target)
    {
        Debug.Log("���� ���� : " + _coin);
    }
}
