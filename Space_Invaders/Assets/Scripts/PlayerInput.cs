using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private Vector2 maxBoundries;

    [Header("Attack and projectile values")]
    [SerializeField] private KeyCode keyCode;
    [SerializeField] private float attackDamage;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Color color;

    void Update()
    {
        /* Get user movement */ 
        var horMov = Input.GetAxis("Horizontal");

        transform.position += new Vector3(horMov * speed, 0, 0) * Time.deltaTime;
        transform.position =  new Vector3(Mathf.Clamp(transform.position.x, maxBoundries.x, maxBoundries.y), transform.position.y, transform.position.z);
        
        /* Check if user clicked attack button */
        if (Input.GetKeyDown(keyCode))
        {
            /* Instantiate projectile */
            Debug.Log("Pew pew pew");
            var newProjectile = ObjectPool.Instance.GetPooledObject();

            /* Check if returned object is not null */
            if (newProjectile != null)
            {
                /* Set projectile's speed, attack, color, position and rotation */
                newProjectile.GetComponent<Projectile>().SetSpeedAndDamage(projectileSpeed, attackDamage, color);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = Quaternion.identity;
                newProjectile.SetActive(true);
            }
        }
    }
}
