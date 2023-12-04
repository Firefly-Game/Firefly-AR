using System.Collections;
using UnityEngine;

public class Moth : FireflyBehaviour
{
    protected override void Start()
    {
        base.Start();
        Type = FireflyType.Moth;
        SetColor();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    protected override void SetType()
    {
        base.SetType();
    }

    protected override IEnumerator ChangeDirection()
    {
        return base.ChangeDirection();
    }

    protected override void PutBackOntoSphere()
    {
        base.PutBackOntoSphere();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

    protected override void HandleCollisionWithJar(Collider other)
    {
        base.HandleCollisionWithJar(other);
    }
}
