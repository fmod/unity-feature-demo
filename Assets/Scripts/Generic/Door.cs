/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Transceiver                                                     |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                When the door is open, the transceiver is enabled.              |
===============================================================================================*/
using UnityEngine;

public class Door : ActionObject
{
    /*===============================================Fmod====================================================
    |   The Transceiver.                                                                                    |
    =======================================================================================================*/
    public FMODUnity.StudioEventEmitter m_event;
    public float m_angle;
    public float m_originalAngle;
    public float m_duration;

    float m_elapsed;
    bool m_doorOpen;
    int m_isActive; //0 no, 1 opening, 2 closing

    void Start()
    {
        InitGlow();
        m_elapsed = 0.0f;
        m_originalAngle = transform.eulerAngles.y;
        m_isActive = 0;
    }
    void Update()
    {
        UpdateGlow();
        switch (m_isActive)
        {
            case 1:
                {
                    OpeningDoor();
                }
                break;
            case 2:
                {
                    ClosingDoor();
                }
                break;
            default:
                break;
        }
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        if (!m_doorOpen)
        {
            m_elapsed = 0.0f;
            m_isActive = 1;
        }
        else
        {
            m_elapsed = 0.0f;
            m_isActive = 2;
        }
    }
    void OpeningDoor()
    {
        m_elapsed += Time.deltaTime;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_originalAngle + (m_angle * (m_elapsed / m_duration)), transform.eulerAngles.z);
        if (m_elapsed >= m_duration)            //Door is open
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_originalAngle + m_angle, transform.eulerAngles.z);
            m_isActive = 0;
            m_doorOpen = true;
            m_elapsed = m_duration;
            /*===============================================Fmod====================================================
            |   Turning on the transceiver event through parameters.                                                |
            =======================================================================================================*/
            m_event.SetParameter("Enabled", (m_elapsed / m_duration));   //set enabled from 0 > 1
        }
    }
    void ClosingDoor()
    {
        m_elapsed += Time.deltaTime;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, (m_originalAngle + m_angle) - (m_angle * (m_elapsed / m_duration)), transform.eulerAngles.z);
        if (m_elapsed >= m_duration)            //Door is closed
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, m_originalAngle, transform.eulerAngles.z);
            m_isActive = 0;
            m_doorOpen = false;
            m_elapsed = m_duration;
        }
        /*===============================================Fmod====================================================
        |   Turning off the transceiver event through parameters.                                               |
        =======================================================================================================*/
        m_event.SetParameter("Enabled", 1.0f - (m_elapsed / m_duration));   //set enabled from 1 > 0
    }
}
