using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour, IItem
{
    public int _ammo = 30;
    public void Use(GameObject target)
    {
        PlayerShooter player = target.GetComponent<PlayerShooter>();
        if (player != null && player._gun)
            player._gun._ammoRemain += _ammo;

        Destroy(gameObject);
    }
}
