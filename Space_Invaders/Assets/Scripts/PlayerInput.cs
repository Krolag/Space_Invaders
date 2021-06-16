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
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float reloadTime;
    [SerializeField] private Color color;
    private float cReloadTime;

    void Update()
    {
        /* Get user movement */ 
        var horMov = Input.GetAxis("Horizontal");

        transform.position += new Vector3(horMov * speed, 0, 0) * Time.deltaTime;
        transform.position =  new Vector3(Mathf.Clamp(transform.position.x, maxBoundries.x, maxBoundries.y), transform.position.y, transform.position.z);
        
        /* Check if user clicked attack button */
        if (Input.GetKeyDown(keyCode) && cReloadTime >= reloadTime)
        {
            cReloadTime = 0;

            /* Instantiate projectile */
            Debug.Log("Pew pew pew");
            var newProjectile = ObjectPool.Instance.GetPooledProjectile();

            /* Check if returned object is not a null */
            if (newProjectile != null)
            {
                /* Set projectile's speed, attack, color, position and rotation */
                newProjectile.GetComponent<Projectile>().SetInitVariables(projectileSpeed, 0, color, true);
                newProjectile.transform.position = transform.position;
                newProjectile.transform.rotation = Quaternion.identity;
                newProjectile.SetActive(true);
            }
        }

        cReloadTime += Time.deltaTime;
    }
}
