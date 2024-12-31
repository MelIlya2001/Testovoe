using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private LayerMask _unitLayer;
    [SerializeField] private LayerMask _ignoreRaycastLayer;
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _attackCD;
    [SerializeField] private Transform _spawnBulletPosition;

    private float _timer;
    private Vector2 _direction;
    private Color _unitColor => GetComponent<SpriteRenderer>().color;
    
    
    private void Update()
    {
        _timer += Time.deltaTime;
        _direction = _targetTransform.position - _spawnBulletPosition.position;


        if (CheckFreePathToUnit())
        {
            if (_timer > _attackCD)
            {
                ShootBullet();
            }  
        }
    }
    

    private bool CheckFreePathToUnit()
    {        
        RaycastHit2D hit = Physics2D.Raycast(_spawnBulletPosition.position, _direction, Mathf.Infinity, ~_ignoreRaycastLayer);
        if (hit.collider == null)
            return false;
        

        bool checkUnit = (_unitLayer & (1 << hit.collider.gameObject.layer)) != 0;
        return checkUnit;

    }

    private void ShootBullet()
    {
        BulletPool.Instance.GetObject(_unitColor, _spawnBulletPosition, _direction);
        _timer = 0;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_spawnBulletPosition.position,  _direction);
    }
    
}
