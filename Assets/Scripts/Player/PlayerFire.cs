using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerFire : MonoBehaviour
{
    private int CODE_LEFT_MOUSE_BUTTON = 0;
    [SerializeField] private float _attackCD;
    [SerializeField] private Transform _spawnBulletPosition;

    private float _timer;
    private Color _unitColor => transform.GetComponent<SpriteRenderer>().color;
    

    void Awake()
    {
        _timer = _attackCD;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(CODE_LEFT_MOUSE_BUTTON))
        {
            if (_timer > _attackCD)
            {
                Vector2 directionBulletMove = CalculateBulletDirection();
                ShootBullet(directionBulletMove);
            }            
        }
    }

    private Vector2 CalculateBulletDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 directionBulletMove = mousePosition - (Vector2)transform.position;

        return directionBulletMove;
    }

    private void ShootBullet(Vector2 direction)
    {
        BulletPool.Instance.GetObject(_unitColor, _spawnBulletPosition, direction);
        _timer = 0;
    }

}
