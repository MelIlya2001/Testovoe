using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unithit : MonoBehaviour
{
    [SerializeField] private LayerMask _bulletLayer;
    public static event Action<Color> OnUnitHit;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool checkHitUnitByBullet = (_bulletLayer & (1 << collision.gameObject.layer)) != 0;
        if (checkHitUnitByBullet)
        {
            OnUnitHit?.Invoke(gameObject.GetComponent<SpriteRenderer>().color);
            BulletPool.Instance.DeactivateAllBullets();
        }
    }
}
