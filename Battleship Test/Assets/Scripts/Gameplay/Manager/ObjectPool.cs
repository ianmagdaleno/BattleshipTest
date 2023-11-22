using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private int poolSize = 15;
    [SerializeField] private Transform objectParent;

    private Queue<GameObject> objectPool;

    private void Awake()
    {
        objectPool = new Queue<GameObject>();
    }

    public void Initialize(GameObject objectToPool, int poolSize = 15)
    {
        this.objectToPool = objectToPool;
        this.poolSize = poolSize;
    }

    public GameObject CreateObject()
    {
        CreateNewObject();

        GameObject spawnedObject = null;

        if (objectPool.Count < poolSize)
        {
            spawnedObject = Instantiate(objectToPool, transform.position, Quaternion.identity);
            spawnedObject.name = $"{transform.root.name}_{objectToPool.name}_{objectPool.Count}";
            spawnedObject.transform.SetParent(objectParent);
        }
        else
        {
            spawnedObject = objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }

        objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }

    public void CreateNewObject()
    {
        if (objectParent == null)
        {
            string name = $"Pool_{objectToPool.name}";

            var parentObject = GameObject.Find(name);
            if (parentObject != null)
            {
                objectParent = parentObject.transform;
            }
            else
            {
                objectParent = new GameObject(name).transform;
            }
        }
    }
}
