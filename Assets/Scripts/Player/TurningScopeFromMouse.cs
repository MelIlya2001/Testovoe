using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurningScopeFromCamera : MonoBehaviour
{
    public void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
