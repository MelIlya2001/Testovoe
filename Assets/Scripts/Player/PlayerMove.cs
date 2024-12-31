using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _body;


    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized * _moveSpeed * Time.fixedDeltaTime;
        
        _body.velocity = movement;
    }
}
