/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Scatter                                                         |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Spawn birds with random properties that circle around like a    |
|   mobile.                                                                                     |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class BirdMobile : MonoBehaviour
{
    Transform m_actor;
    public Bird m_bird;
    public float m_xRadius, m_zRadius;
    public float m_heightOffset, m_xOffset, m_zOffset;
    public int m_minBirds, m_maxBirds;
    public float m_minSpeed, m_maxSpeed;
    public bool m_billboard;

    int m_numOfBirds;

    void Start()
    {
        m_actor = Camera.main.transform;
        m_numOfBirds = Random.Range(m_minBirds, m_maxBirds);
        for (int i = 0; i < m_numOfBirds; ++i)  //Spawns birds in a cricle pattern with offsets to make it look interesting
        {
            Vector3 localPosition = new Vector3();

            float xOffset = Random.Range(0.0f, m_xOffset) - m_xOffset * 0.5f;
            localPosition.x = Mathf.Sin(Mathf.PI * (i / (float)m_numOfBirds * Time.time) * 2.0f) * m_xRadius + xOffset;

            localPosition.y = Random.Range(0.0f, m_heightOffset) - m_heightOffset * 0.5f;

            float zOffset = Random.Range(0.0f, m_zOffset) - m_zOffset * 0.5f;
            localPosition.z = Mathf.Cos(Mathf.PI * (i / (float)m_numOfBirds * Time.time) * 2.0f) * m_zRadius + zOffset;

            Bird bird = Instantiate(m_bird);
            bird.transform.parent = transform;
            bird.transform.localPosition = localPosition;
            bird.m_xOffset = xOffset;
            bird.m_zOffset = zOffset;
            bird.m_speed = Random.Range(m_minSpeed, m_maxSpeed);
        }
    }
    void Update()
    {
        for (int i = 0; i < m_numOfBirds; ++i)  //rotate birds
        {
            Bird bird = transform.GetChild(i).gameObject.GetComponent<Bird>();
            Vector3 localPosition = bird.transform.localPosition;
            localPosition.x = Mathf.Sin(Mathf.PI * (i / (float)m_numOfBirds) * 2.0f + Time.time * bird.m_speed) * m_xRadius + bird.m_xOffset;
            localPosition.z = Mathf.Cos(Mathf.PI * (i / (float)m_numOfBirds) * 2.0f + Time.time * bird.m_speed) * m_zRadius + bird.m_zOffset;
            bird.transform.localPosition = localPosition;

            if (m_billboard)
            {
                bird.transform.forward = (m_actor.position - bird.transform.position).normalized;
            }
            else
            {
                Vector3 diff = bird.transform.localPosition - bird.m_lastLocalPosition;
                if (diff != Vector3.zero)
                {
                    bird.transform.forward = Vector3.Cross(diff, Vector3.up);
                }
            }
        }
    }
}