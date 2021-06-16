using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [Header("GameObject to pool")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private int projectilesToPool;
    [SerializeField] private List<GameObject> pooledProjectiles;

    public GameObject GetPooledProjectile()
    {
        /* Iterate through all objects in hierarchy */
        for (int i = 0; i < projectilesToPool; i++)
        {
            /* If current object is not active, return it */
            if (!pooledProjectiles[i].activeInHierarchy)
                return pooledProjectiles[i];
        }

        /* Return null if objects are currently unavailable */
        return null;
    }

    private void Awake()
    {
        /* Check if instance already exist */
        if (Instance == null) // if not, set this one as instance
            Instance = this;
        else if (Instance != null) // if yes, destroy unused object
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }

        /* Initialize pooledObjects Lists */
        pooledProjectiles = new List<GameObject>();
    }

    private void Start()
    {
        GameObject projectileTMP;

        /* Instantiate new projectiles, deactivate it and add to pooledProjectiles List */
        for (int i = 0; i < projectilesToPool; i++)
        {
            projectileTMP = Instantiate(projectile);
            projectileTMP.transform.parent = transform;
            projectileTMP.SetActive(false);
            pooledProjectiles.Add(projectileTMP);
        }
    }
}
