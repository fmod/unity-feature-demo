using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pianoParticles : MonoBehaviour
{
    ParticleSystem m_particleSys;


	void Start ()
    {
        m_particleSys = GetComponentInChildren<ParticleSystem>();
        m_particleSys.SetEmissionRate(0);
	}

    private void OnTriggerEnter(Collider other)
    {
        m_particleSys.Emit(1);
    }
}
