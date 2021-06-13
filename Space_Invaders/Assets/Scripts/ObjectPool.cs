using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField] private GameObject pooledObjectsParent;
    
    [Header("GameObject to pool")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private int amountToPool;
    [SerializeField] private List<GameObject> pooledObjects;

    public GameObject GetPooledObject()
    {
        /* Iterate through all objects in hierarchy */
        for (int i = 0; i < amountToPool; i++)
        {
            /* If current object is not active, return it */
            if (!pooledObjects[i].activeInHierarchy)
                return pooledObjects[i];
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

        /* Initialize pooledObjects List */
        pooledObjects = new List<GameObject>();
    }

    private void Start()
    {
        GameObject tmp;

        /* Instantiate new object, deactivate it and add to pooledObjects List */
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(projectile);
            tmp.transform.parent = pooledObjectsParent.transform;
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
}
