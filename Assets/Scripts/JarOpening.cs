using UnityEngine;

public class JarOpening : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("fly"))
        {
            GetComponent<AudioSource>()?.Play();
        }
    }
}