using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnim : MonoBehaviour
{
    private ParticleSystem _system;

    private void Awake()
    {
        _system = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(_system && !_system.IsAlive())
        {
            DestroySelf();
        }
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
