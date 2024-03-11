using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : SingletonBehaviour<ObjectPoolManager>
{
    public List<Pool> Pools;

    public override void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        Pools = new List<Pool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetGameObjectFromPool(GameObject prefabObject, Vector3 position, Quaternion rotation)
    {
        Pool objectPool = Pools.Find(p=> p.LookUpString == prefabObject.tag);
        if (objectPool == null)
        {
            objectPool = new Pool() { LookUpString = prefabObject.tag, InactiveObjects = new List<GameObject>() };
            Pools.Add(objectPool);
        }

        GameObject spawnedObj = objectPool.InactiveObjects.FirstOrDefault();
        if (spawnedObj == null)
        {
            spawnedObj = Instantiate(prefabObject);
            spawnedObj.SetActive(false);
        }
        else
        {
            objectPool.InactiveObjects.Remove(spawnedObj);
        }

        spawnedObj.transform.position = position;
        spawnedObj.transform.rotation = rotation;
        spawnedObj.SetActive(true);
        return spawnedObj;
    }

    public void ReturnObjectToPool(GameObject spawnedObj)
    {
        Pool objectPool = Pools.Find(p => p.LookUpString == spawnedObj.tag);
        if (objectPool == null)
        {
            Debug.Log("pool does not exist");
            return;
        }
        spawnedObj.SetActive(false );
        objectPool.InactiveObjects.Add(spawnedObj);
    }
}

public class Pool
{
    public List<GameObject> InactiveObjects;
    public string LookUpString;
}
