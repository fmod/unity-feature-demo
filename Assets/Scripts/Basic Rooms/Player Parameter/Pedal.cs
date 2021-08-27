/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Player Parameter                                                |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Passes the accelerator value to the Car script.                 |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Pedal : ActionObject
{
    public Car m_car;
    float m_acceleration;

    void Start()
    {
        InitGlow();
        StopGlow();

        m_acceleration = 0.0f;
    }
    void Update()
    {
        UpdateGlow();

    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        if (a_key == m_actionKeys[0])
        {
            m_acceleration = Mathf.Min(m_acceleration + (Time.deltaTime * 4.0f), 4.0f);
        }
        else if (a_key == m_actionKeys[1])
        {
            m_acceleration = Mathf.Max(m_acceleration - (Time.deltaTime * 4.0f), 0.0f);
        }
        m_car.Accelerate(m_acceleration);
        Vector3 rot = transform.eulerAngles;
        rot.x = 20.0f - (m_acceleration / 5.0f) * 30.0f;
        transform.eulerAngles = rot;
    }
    public override void ActionDown(GameObject sender, KeyCode a_key)
    {
        if (a_key == m_actionKeys[0])
        {
            m_acceleration = Mathf.Min(m_acceleration + (Time.deltaTime * 4.0f), 4.0f);
        }
        else if (a_key == m_actionKeys[1])
        {
            m_acceleration = Mathf.Max(m_acceleration - (Time.deltaTime * 4.0f), 0.0f);
        }
        m_car.Accelerate(m_acceleration);
        Vector3 rot = transform.eulerAngles;
        rot.x = 20.0f - (m_acceleration / 5.0f) * 30.0f;
        transform.eulerAngles = rot;
    }
    public void Reset()
    {
        m_acceleration = 0.0f;
        Vector3 rot = transform.eulerAngles;
        rot.x = 20.0f - (m_acceleration / 5.0f) * 30.0f;
        transform.eulerAngles = rot;
    }
}