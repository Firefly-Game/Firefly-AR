using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public ObjectPool fireflyPool;
    public ObjectPool mothPool;
    public static readonly float spawnRadius = 2.5f;

    public int fireflyCount = 100;
    public int mothCount = 5;

    public ARRaycastManager rays;
    public Camera myCamera;
    public ARAnchorManager anchorManager;
    private float cooldown = 2f, cooldownCount = 0f;
    void Start()
    {
        Debug.Log("Checking if rays is null: " + (rays == null));
        Debug.Log("Checking if myCamera is null: " + (myCamera == null));
        Debug.Log("Checking if fireflyPool is null: " + (fireflyPool == null));

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

    void Update()
    {
        Debug.Log("Checking if rays is null: " + (rays == null));
        Debug.Log("Checking if myCamera is null: " + (myCamera == null));
        Debug.Log("Checking if fireflyPool is null: " + (fireflyPool == null));

        cooldownCount += Time.deltaTime;

        if (cooldownCount > cooldown)
        {
            cooldownCount = 0;
            TrySpawnFireflyOnPlane();
        }
    }


    void TrySpawnFireflyOnPlane()
    {
        List<ARRaycastHit> myHits = new List<ARRaycastHit>();
        Vector3 screenCenter = myCamera.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        bool hit = rays.Raycast(screenCenter, myHits, TrackableType.PlaneWithinPolygon);

        if (hit)
        {
            ARRaycastHit nearest = myHits[0];
            GameObject firefly = fireflyPool.InstantiateObject(nearest.pose.position, nearest.pose.rotation);

            firefly.layer = LayerMask.NameToLayer("NotCollected");

            ARAnchor anchor = anchorManager.AttachAnchor(nearest.trackable as ARPlane, nearest.pose);
            if (anchor != null)
            {
                firefly.transform.parent = anchor.transform;

                FireflyBehaviour fireflyBehaviour = firefly.GetComponent<FireflyBehaviour>();
                if (fireflyBehaviour != null)
                {
                    fireflyBehaviour.isAnchored = true;
                }
            }
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
