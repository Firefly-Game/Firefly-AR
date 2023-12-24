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
        Vector3 point = Vector3.zero;
        while (point == Vector3.zero)
        {
            point = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            );
        }

        float normalizer = 1 / Mathf.Sqrt(Mathf.Pow(point.x, 2) + Mathf.Pow(point.y, 2) + Mathf.Pow(point.z, 2));

        point *= normalizer;

        return point * spawnRadius;
    }
}
