/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      All                                                             |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Swings and flickers the hangling lights.                        |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class PingPongRotate : MonoBehaviour
{
    public Vector3 m_rotationAxis;          //Usually z and/or x axis.
    public float m_rotationAngle;           //How much swing per swing duration
    public float m_swingDuration;           //How quickly the angle is swung
    public bool m_flicker;                  //Flicker lights?

    Vector3 m_originalForward;              //Store the default forward of the object. used for rotating with quaternions
    Light m_light;                          //The light to flicker
    float m_lightIntensity;                 //Store the default light intensity as the max
    int m_count;                            //Flicker every n frames.

	void Start ()
    {
        m_light = GetComponentInChildren<Light>();
        m_rotationAxis = m_rotationAxis.normalized;
        m_originalForward = transform.forward;
        m_lightIntensity = m_light.intensity;
	}
    void FixedUpdate()
    {
        if (!m_flicker)
            return;
        if (m_count == 5)
        {
            int rng = Random.Range(1, 100);
            if (rng <= 20)                  //If random n is less than 20% turn light down
            {
                m_light.intensity = m_lightIntensity * 0.4f;
            }
            else                            //Turn light to max
            {
                m_light.intensity = m_lightIntensity;
            }
            m_count = 0;
        }
        m_count++;
    }
	void Update ()
    {
        //Swing the light over time with Sine
        Quaternion rot = Quaternion.AngleAxis(Mathf.Sin(Time.time * (m_swingDuration == 0 ? 0 : (1 / m_swingDuration))) * m_rotationAngle, m_rotationAxis);
        transform.forward = rot * m_originalForward;
	}
}