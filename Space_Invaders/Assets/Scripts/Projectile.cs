using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile basic values")]
    private float speed;
    private float attackDamage;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        /* Fetch the SpriteRenderer from the GameObject */
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Set projectile speed and damage 
    public void SetSpeedAndDamage(float _speed, float _attackDamage, Color _color)
    {
        speed = _speed;
        attackDamage = _attackDamage;
        spriteRenderer.color = _color;
    }

    private void Update()
    {
        /* Move projectile up on y */
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);

        /* Check if projectile is out of screen view */
        if (transform.position.y > 7.0f)
            Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
