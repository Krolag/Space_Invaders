using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        var horMov = Input.GetAxis("Horizontal");

        transform.position += new Vector3(horMov * speed, 0, 0) * Time.deltaTime;
    }
}
