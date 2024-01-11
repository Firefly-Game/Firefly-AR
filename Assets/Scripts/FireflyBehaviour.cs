using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyBehaviour : MonoBehaviour
{
    public GameObject target;
    public ScoreLabel scoreLabel;
    protected Vector3 direction;
    public float speed = 1.0f;
    private bool collected = false;
    private Vector3 desiredScale = Vector3.one * 0.1f;

    public FireflyType Type { get; protected set; } = FireflyType.Common;

    private Dictionary<FireflyType, int> typeDistribution = new Dictionary<FireflyType, int>
    {
        { FireflyType.Common,    100 },
        { FireflyType.Rare,      25 },
        { FireflyType.Epic,      5 },
        { FireflyType.Legendary, 1 },

    };

    private Dictionary<FireflyType, Color> typeColors = new Dictionary<FireflyType, Color>
    {
        { FireflyType.Common,    new Color(1f, 1f, 1f, 1f) },
        { FireflyType.Rare,      new Color(0.12f, 0.64f, 1f) },
        { FireflyType.Epic,      new Color(0.48f, 0.32f, 0.89f) },
        { FireflyType.Legendary, new Color(0.95f, 0.77f, 0.06f) },
        { FireflyType.Moth,      new Color(0f, 1f, 0f, 1f) },
    };

    public enum FireflyType
    {
        Common,
        Rare,
        Epic,
        Legendary,
        Moth
    }

    protected virtual void Start()
    {
        target = FindAnyObjectByType<Camera>().gameObject;
        scoreLabel = FindAnyObjectByType<ScoreLabel>();
        SetType();
        SetColor();
        StartCoroutine(ChangeDirection());
    }

    // Randomly generate the type based on typeDistribution
    protected virtual void SetType()
    {
        int total = 0;
        foreach (var item in typeDistribution)
        {
            total += item.Value;
        }

        int random = (int)(Random.value * total);
        int current = 0;
        foreach (var item in typeDistribution)
        {
            current += item.Value;
            if (current > random)
            {
                Type = item.Key;
                break;
            }
        }
    }

    protected void SetColor()
    {
        var renderer = GetComponent<MeshRenderer>();
        renderer.material.SetColor("_Color", typeColors[Type]);
        renderer.material.SetColor("_EmissionColor", typeColors[Type]);
    }

    protected virtual void FixedUpdate()
    {
        if (!collected)
        {
            speed = Mathf.Pow(Time.timeSinceLevelLoad, 0.05f);
            GetComponent<Rigidbody>().AddForce(direction * 0.0000005f * speed);
            PutBackOntoSphere();
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, transform.parent.position, 0.1f);
            transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, 0.1f);
        }
    }

    protected virtual IEnumerator ChangeDirection()
    {
        while (!collected)
        {
            Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            direction = Vector3.Cross(transform.position, randomDirection).normalized;

            yield return new WaitForSeconds(Random.Range(0.3f, 1f));
        }
    }

    // Puts the Firefly back on to the sphere
    protected virtual void PutBackOntoSphere()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        Vector3 directionToTarget = Vector3.Normalize(target.transform.position - transform.position);
        GetComponent<Rigidbody>().position += directionToTarget * (distance - Spawner.spawnRadius);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (collected) return; 

        if (other.gameObject.CompareTag("opening"))
        {
            HandleCollisionWithOpening(other);
        }
    }

    protected virtual void HandleCollisionWithOpening(Collider other)
    {
        collected = true;
        gameObject.layer = LayerMask.NameToLayer("Collected");
        transform.parent = GameObject.Find("Jar").transform;

        Vector3 parentScale = GameObject.Find("Jar").transform.localScale;
        desiredScale = new Vector3(
            1f / parentScale.x * transform.localScale.x,
            1f / parentScale.y * transform.localScale.y,
            1f / parentScale.z * transform.localScale.z
            ) * 0.5f;

        //gameObject.SetActive(false);
        scoreLabel.OnCatch(this);
    }
}