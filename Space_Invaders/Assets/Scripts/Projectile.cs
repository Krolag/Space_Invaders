using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile basic values")]
    private float speed;
    private bool sendByPlayer;
    private float attackDamage;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        /* Fetch the SpriteRenderer from the GameObject */
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Set projectile speed and damage 
    public void SetInitVariables(float _speed, float _attackDamage, Color _color, bool _sendByPlayer)
    {
        speed = _speed;
        attackDamage = _attackDamage;
        spriteRenderer.color = _color;
        sendByPlayer = _sendByPlayer;
    }

    private void Update()
    {
        /* Move projectile up on y */
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);

        /* Check if projectile is out of screen view */
        if (transform.position.y > 7.0f || transform.position.y < -5.0f)
            Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !sendByPlayer)
        {
            GameEvents.Instance.PlayerTakeDamage();
        }
        if (collision.tag == "Enemy" && sendByPlayer)
        {
            collision.gameObject.SetActive(false);
            Deactivate();
        }
        if (collision.tag == "DefenseTower")
        {
            collision.gameObject.SetActive(false);
            Deactivate();
        }
    }
}
