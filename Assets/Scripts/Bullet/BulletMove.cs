using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _maxCountOfReflect;

    private Rigidbody2D _body;
    private float _currentCountOfReflect;


    public Vector2 Direction { get; set;} 


    private void OnEnable()
    {
        _currentCountOfReflect = 0;
    }

    private void Awake()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _body.velocity = Direction.normalized * _moveSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCountOfReflect();
        ChangeMoveDirectionAndRotation(collision);
    }


    private void CheckCountOfReflect()
    {
        if (_currentCountOfReflect >= _maxCountOfReflect)
            BulletPool.Instance.Remove(gameObject);
        else
            _currentCountOfReflect += 1;
    }

    private void ChangeMoveDirectionAndRotation(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        Direction = normal;


        float angle = Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
