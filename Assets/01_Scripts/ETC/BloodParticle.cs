using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodParticle : MonoBehaviour
{
    private ParticleSystem particle;
    private ParticleSystemRenderer pr;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
        pr       = GetComponent<ParticleSystemRenderer>();
    }

    public void SetParticleColor(Color color)
    {
        pr.material.color = color;
    }

    public void Play(Vector3 pos)
    {
        transform.position = pos;
        particle.Play();
        Invoke(nameof(Disable), 2.0f);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetRotation(Vector2 normal)
    {
        transform.rotation = Quaternion.LookRotation(normal);
    }
}
