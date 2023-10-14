using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pooling : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public Dictionary<string,Queue<GameObject>> object_pool;
    public List<Pool> list_of_pools;

    #region Singleton_Pattern
    public static Object_Pooling Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Instantiate_Objects();
    }

    public void Instantiate_Objects()
    {
        object_pool = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in list_of_pools)
        {
                Queue<GameObject> pool_of_objects = new Queue<GameObject>();
                for (int i = 0; i < pool.size; i++)
                {
                    GameObject spawn_object =  Instantiate(pool.prefab);
                    pool_of_objects.Enqueue(spawn_object);
                    spawn_object.SetActive(false);
                }
                object_pool.Add(pool.tag, pool_of_objects);
        }
    }
    
    public GameObject Spawn_Object(string tag,Vector3 position)
    {
        GameObject spawn_object = object_pool[tag].Dequeue();
        spawn_object.transform.position = position;
        spawn_object.SetActive(true);
        object_pool[tag].Enqueue(spawn_object);
        return spawn_object;
    }
}
