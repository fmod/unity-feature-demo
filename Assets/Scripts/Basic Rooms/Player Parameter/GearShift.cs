/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Player Parameter                                                |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Passes the gear to Car script.                                  |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class GearShift : ActionObject
{
    public Car m_car;
    int m_gear;
    Vector3 m_startRotation;
    void Start ()
    {
        InitGlow();
        StopGlow();

        m_gear = 1;
        m_startRotation = transform.eulerAngles;
        Vector3 rot = transform.eulerAngles;
        rot.x = m_startRotation.x + (Mathf.Cos((m_gear * Mathf.PI) + Mathf.PI) * 8.0f);
        transform.eulerAngles = rot;
    }
    void Update()
    {
        UpdateGlow();
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        if(a_key == m_actionKeys[0])
        {
            m_car.UpGear();
            m_gear = Mathf.Min(++m_gear, 5);
        }
        else if(a_key == m_actionKeys[1])
        {
            m_car.DownGear();
            m_gear = Mathf.Max(--m_gear, 1);
        }
        Vector3 rot = transform.eulerAngles;
        rot.x = m_startRotation.x + (Mathf.Cos((m_gear * Mathf.PI) + Mathf.PI) * 8.0f);
        rot.z = m_startRotation.z - Mathf.FloorToInt((m_gear - 1) * 0.5f) * 8.0f;
        transform.eulerAngles = rot;
    }
    public void Reset()
    {
        m_gear = 1;
        Vector3 rot = transform.eulerAngles;
        rot.x = m_startRotation.x + (Mathf.Cos((m_gear * Mathf.PI) + Mathf.PI) * 8.0f);
        rot.z = m_startRotation.z - Mathf.FloorToInt((m_gear - 1) * 0.5f) * 8.0f;
        transform.eulerAngles = rot;
    }
}