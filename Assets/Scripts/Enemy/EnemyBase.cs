using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    bool _hitFlg = false;

    public bool HitFlg => _hitFlg;
    public void Damage()
    {
        Debug.Log("Cut");
        _hitFlg = true;
    }
}
