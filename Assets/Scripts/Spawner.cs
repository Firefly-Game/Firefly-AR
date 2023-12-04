using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public ObjectPool fireflyPool;
    public ObjectPool mothPool;
    public static readonly float spawnRadius = 2.5f;

    public int fireflyCount = 100;
    public int mothCount = 5;

    void Start()
    {
        for (int i = 0; i < fireflyCount; i++)
        {
            SpawnFirefly();
        }

        for (int i = 0; i < mothCount; i++)
        {
            SpawnMoth();
        }

        StartCoroutine(TimedSpawn());
    }

    IEnumerator TimedSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            SpawnFirefly();
        }
    }

    void SpawnFirefly()
    {
        fireflyPool.InstantiateObject(
            GetPositionOnSphere(),
            transform.rotation
            );
    }

    void SpawnMoth()
    {
        mothPool.InstantiateObject(
            GetPositionOnSphere(),
            transform.rotation
            );
    }

    public Vector3 GetPositionOnSphere()
    {
        float x = 0, y = 0, z = 0;
        while (x == 0 & y == 0 & z == 0)
        {
            x = Random.Range(-1f, 1f);
            y = Random.Range(-1f, 1f);
            z = Random.Range(-1f, 1f);
        }

        float normalizer = 1 / Mathf.Sqrt(x * x + y * y + z * z);

        x *= normalizer;
        y *= normalizer;
        z *= normalizer;

        return new Vector3(x, y, z) * spawnRadius;
    }
}
