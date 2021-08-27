/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Custom Distance Attenuation                                     |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Flickers lights.                                                |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class FlickerLight : MonoBehaviour
{
    public float m_minDuration = 0.0f;
    public float m_maxDuration = 5.0f;
    float m_currentDuration = 0;
    float m_elapsed;
    Color m_emission;

    void Start()
    {
        m_emission = GetComponent<Renderer>().material.GetColor("_EmissionColor");
        m_currentDuration = Random.Range(m_minDuration, m_maxDuration);
    }
    void Update()
    {
        if (m_elapsed >= m_currentDuration)
        {
            m_currentDuration = Random.Range(m_minDuration, m_maxDuration);     //Select a random flicker duration for next cycle.
            int rng = Random.Range(1, 100);
            if (rng <= 20)
            {
                GetComponent<Renderer>().material.SetColor("_EmissionColor", m_emission * 0.2f);
            }
            else
            {
                GetComponent<Renderer>().material.SetColor("_EmissionColor", m_emission);
            }
            m_elapsed = 0.0f;
        }
        m_elapsed += Time.deltaTime;
    }
}