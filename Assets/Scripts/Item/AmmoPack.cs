using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour, IItem
{
    public int _ammo = 30;
    public void Use(GameObject target)
    {
        Debug.Log("ź�� ���� : " + _ammo);
    }
}
