using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour, IItem
{
    public int _score = 200;
    public void Use(GameObject target)
    {
        GameManager.Instance.AddScore(_score);
        Destroy(gameObject);
    }
}
