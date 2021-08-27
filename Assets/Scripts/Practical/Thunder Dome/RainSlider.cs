/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Thunder Dome                                                    |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Controls the rain value and the particle emitter.               |
|   WeatherController script will access this script.                                           |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class RainSlider : ActionObject
{
    public Vector3 m_startPosition;
    public Vector3 m_endPosition;

    bool m_isActive;

    float m_rainValue;
    float m_waterValue;
    public float RainValue { get { return m_rainValue * 4.0f; } }
    public float WaterValue { get { return Mathf.Min(m_rainValue * 4.0f, m_waterValue); } }
    public ParticleSystem m_particleSystem;
    public Transform m_pond;
    float m_minPondHeight;
    public float m_maxPondHeight;
    float m_originalRate;

    void Start()
    {
        InitGlow();

        float diff = (transform.position - m_startPosition).magnitude / (m_endPosition - m_startPosition).magnitude;
        diff = Mathf.Clamp(diff, 0.0f, 1.0f);

        m_minPondHeight = m_pond.position.y;

        m_rainValue = 0.0f;
        m_originalRate = m_particleSystem.emission.rateOverTime.constantMax;

        m_particleSystem.SetEmissionRate(Mathf.Lerp(0, m_originalRate, m_rainValue));
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

                    m_rainValue = diff;
                    m_rainValue = Mathf.Clamp(m_rainValue, 0.0f, 1.0f);

                    m_particleSystem.SetEmissionRate(Mathf.Lerp(0, m_originalRate, m_rainValue));
                }
            }
        }
        if (m_waterValue != m_rainValue)
        {
            m_waterValue = Mathf.MoveTowards(m_waterValue, m_rainValue * 4.0f, Time.deltaTime * 0.25f);
            Vector3 pos = m_pond.position;
            pos.y = Mathf.Lerp(m_minPondHeight, m_maxPondHeight, Mathf.Clamp(m_waterValue - 2.0f, 0.0f, 2.0f) * 0.5f);
            m_pond.position = pos;
        }
    }

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        m_isActive = true;
    }
}