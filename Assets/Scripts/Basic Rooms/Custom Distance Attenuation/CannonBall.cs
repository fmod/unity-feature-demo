/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Custom Distance Attenuation                                     |
|   Fmod Related Scripting:     No                                                              |
|   Description:                When the cannon ball collides with the ground it will destroy   |
|   itself, play the particle emitter and play the explosion sound through FMOD base scripts in |
|   Unity.                                                                                      |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour
{
    public float m_explosionRadius = 3.0f;
    public float m_explosionForce = 3.0f;

    public GameObject m_explosion;

	void Start ()
    {
        
	}

    void OnCollisionEnter(Collision a_collision)
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, m_explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(m_explosionForce, explosionPos, m_explosionRadius, 3.0f);
            }
        }
        
        Destroy(this.gameObject);
    }

    void OnDestroy()
    {
        m_explosion.SetActive(true);
        m_explosion.transform.SetParent(null);
    }
}