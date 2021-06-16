using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private Color color;

    [Header("Attack variables")]
    [SerializeField] private float attackDamage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float timeBetweenProjectiles;
    private float ctimeBetweenProjectiles = 0;
    private bool canShoot;

    public bool CanShoot { get => canShoot; 
        set 
        {
            ctimeBetweenProjectiles = 0;
            canShoot = value; 
        } 
    }

    public void SetInitVariables(Color _color, float _attackDamage, float _health, 
        float _projectileSpeed, float _timeBetweenProjectiles)
    {
        color = _color;
        attackDamage = _attackDamage;
        health = _health;
        projectileSpeed = _projectileSpeed;
        timeBetweenProjectiles = _timeBetweenProjectiles;
    }

    private void Start()
    {
        /* Find component in children and assign new color */ 
        var sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.color = color;
        canShoot = false;
    }

    private void Update()
    {
        /* Check if enemy can send projectile */
        if (canShoot && ctimeBetweenProjectiles >= timeBetweenProjectiles)
        {
            /* Reset current attack frequency */
            ctimeBetweenProjectiles = 0;

            var newProjectile = ObjectPool.Instance.GetPooledProjectile();

            /* Check if returned object is not null */
            if (newProjectile != null)
            {
                /* Set projectile's speed, attack, color, position and rotation */
                newProjectile.GetComponent<Projectile>().SetInitVariables(-projectileSpeed, attackDamage, color, false);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = Quaternion.identity;
                newProjectile.SetActive(true);
            }

            canShoot = false;
        }

        ctimeBetweenProjectiles += Time.deltaTime;
    }
}
