/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Thunder Dome                                                    |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Controls the wind, rotates the rain emitter aswell.             |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class WindSlider : ActionObject
{
    public Vector3 m_startPosition;
    public Vector3 m_endPosition;
    public ParticleSystem m_particleSystem;
    public Vector3 m_maxRainPosition;

    bool m_isActive;
    Vector3 m_minRainPosition;
    float m_windValue;
    public float WindValue { get { return m_windValue * 100.0f; } }
    float m_orignialX;

    void Start()
    {
        InitGlow();

        float diff = (transform.position - m_startPosition).magnitude / (m_endPosition - m_startPosition).magnitude;
        diff = Mathf.Clamp(diff, 0.0f, 1.0f);

        m_minRainPosition = m_particleSystem.transform.position;

        m_windValue = 0.0f;
        var vol = m_particleSystem.forceOverLifetime;
        var x = vol.x;
        m_orignialX = x.constantMax;
        x.constantMax = 0.0f;
        vol.x = x;
    }

    void Update()
    {
        UpdateGlow();
        if (Input.GetKeyUp(m_actionKeys[0]))
        {
            m_isActive = false;
        }
        if (m_isActive)
        {
            RaycastHit rh;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out rh, Mathf.Infinity, ~(1 << 10 | 1 << 2)))
            {
                if (rh.collider.gameObject.name == "Weather Controller")
                {
                    float diff = 0.0f;
                    float dot = Vector3.Dot((rh.point - m_startPosition).normalized, (m_endPosition - m_startPosition).normalized);
                    if (dot > 0)
                    {
                        diff = (rh.point - m_startPosition).magnitude / (m_endPosition - m_startPosition).magnitude;
                    }
                    transform.position = Vector3.Lerp(m_startPosition, m_endPosition, diff * dot);

                    m_windValue = diff;
                    m_windValue = Mathf.Clamp(m_windValue, 0.0f, 1.0f);

                    var vol = m_particleSystem.forceOverLifetime;
                    var x = vol.x;
                    x.constantMax = Mathf.Lerp(0.0f, m_orignialX, m_windValue);
                    vol.x = x;
                    m_particleSystem.transform.position = Vector3.Lerp(m_minRainPosition, m_maxRainPosition, m_windValue);
                }
            }
        }
    }

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        m_isActive = true;
    }
}