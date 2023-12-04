using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> Objects { get; private set; }

    public GameObject objectToPool;

    void Awake()
    {
        Objects = new List<GameObject>();
    }

    void Start()
    { }

    public GameObject InstantiateObject(Vector3 position, Quaternion rotation)
    {
        // Get the first inactive object
        for (int i = 0; i < Objects.Count; i++)
        {
            if (!Objects[i].activeInHierarchy)
            {
                SetupObject(i, position, rotation);
                return Objects[i];
            }
        }

        // If no inactive objects found...
        // Double the pool size
        AddObjectsToPool(Objects.Count == 0 ? 1 : Objects.Count);

        SetupObject(Objects.Count / 2, position, rotation);
        return Objects[Objects.Count / 2];
    }

    private void SetupObject(int index, Vector3 position, Quaternion rotation)
    {
        Objects[index].transform.position = position;
        Objects[index].transform.rotation = rotation;
        Objects[index].SetActive(true);
    }

    // Add *amount* number of *objectToPool* to the list of pooled objects
    private void AddObjectsToPool(int amount)
    {
        if (amount <= 0) return;

        GameObject temporary;
        for (int i = 0; i < amount; i++)
        {
            temporary = Instantiate(objectToPool);
            temporary.SetActive(false);
            Objects.Add(temporary);
        }
    }
}
