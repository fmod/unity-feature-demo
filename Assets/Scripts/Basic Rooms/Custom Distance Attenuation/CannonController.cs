/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Custom Distance Attenuation                                     |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Angles the cannon to the selected angle then fires at the given |
|   fire rate.                                                                                  |
===============================================================================================*/
using UnityEngine;
using System;

public class CannonController : MonoBehaviour
{
    /*===============================================Fmod====================================================
    |   Store the cannon movement sound inside a string.                                                    |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    public string m_cannonSoundPath;
    /*===============================================Fmod====================================================
    |   Store the cannonEvent that is created in Start().                                                   |
    =======================================================================================================*/
    FMOD.Studio.EventInstance m_cannonEvent;
    /*===============================================Fmod====================================================
    |   Store the parameter IDs from the event.                                                             |
    =======================================================================================================*/
    FMOD.Studio.PARAMETER_ID m_cannonStopParameterID;
    FMOD.Studio.PARAMETER_ID m_cannonDirectionParameterID;
    /*===============================================Fmod====================================================
    |   This is where the playstate of the event will be stored.                                            |
    =======================================================================================================*/
    FMOD.Studio.PLAYBACK_STATE m_playState;
    /*===============================================Fmod====================================================
    |   Store the cannon Fire sound inside a string.                                                        |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    /*===============================================Fmod====================================================
    |   Store the cannonFireEvent that is created in Start().                                               |
    =======================================================================================================*/
    public string m_cannonFireSound;
    FMOD.Studio.EventInstance m_cannonFireEvent;

    public GameObject m_cannonBall;
    public GameObject m_cannon;
    public float m_fireDelay = 0.5f;

    float m_currentAngle;
    float m_power;
    float m_selectedAngle;
    bool m_isActive;
    float m_elapsedFireDelay;
    float m_cannonChangeSpeed = 10.0f;

    void Start()
    {
        /*===============================================Fmod====================================================
        |  Create the event using the path stored.                                                              |
        =======================================================================================================*/
        m_cannonEvent = FMODUnity.RuntimeManager.CreateInstance(m_cannonSoundPath);

        /*===============================================Fmod====================================================
        |  Grab the parameters and store the IDs to use later.                                                  |
        =======================================================================================================*/
        FMOD.Studio.EventDescription m_cannonDesc;
        m_cannonEvent.getDescription(out m_cannonDesc);
        FMOD.Studio.PARAMETER_DESCRIPTION paramDesc;
        
        m_cannonDesc.getParameterDescriptionByName("Stop Point", out paramDesc);
        m_cannonStopParameterID = paramDesc.id;

        m_cannonDesc.getParameterDescriptionByName("Direction", out paramDesc);
        m_cannonDirectionParameterID = paramDesc.id;
        /*===============================================Fmod====================================================
        |  Set the 3D attributes. Or in other terms, tell which transform the event should follow.              |
        =======================================================================================================*/
        m_cannonEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

        /*===============================================Fmod====================================================
        |  Create the event using the path stored.                                                              |
        =======================================================================================================*/
        m_cannonFireEvent = FMODUnity.RuntimeManager.CreateInstance(m_cannonFireSound);

        /*===============================================Fmod====================================================
        |  Set the 3D attributes. Or in other terms, tell which transform the event should follow.              |
        =======================================================================================================*/
        m_cannonFireEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));

        m_currentAngle = 30.0f;
        m_selectedAngle = 30.0f;
        m_power = 10.0f;
        m_isActive = false;
    }

    void FixedUpdate()
    {
        if (m_isActive)
        {
            if (m_currentAngle != m_selectedAngle)
            {
                /*===============================================Fmod====================================================
                |   Check to see if the event is already playing, if not then start the event from the beginning.       |
                =======================================================================================================*/
                m_cannonEvent.getPlaybackState(out m_playState);
                if (m_playState != FMOD.Studio.PLAYBACK_STATE.PLAYING)
                {
                    /*===============================================Fmod====================================================
                    |   The parameter that tells the event that the cannon is moving.                                       |
                    =======================================================================================================*/
                    m_cannonEvent.setParameterByID(m_cannonStopParameterID, 0.0f);

                    /*===============================================Fmod====================================================
                    |   Starts the sound, or resumes.                                                                       |
                    =======================================================================================================*/
                    m_cannonEvent.start();
                }

                if (m_selectedAngle < m_currentAngle)
                {
                    m_currentAngle -= m_cannonChangeSpeed * Time.fixedDeltaTime;
                    if (m_currentAngle < m_selectedAngle)
                    {
                        m_currentAngle = m_selectedAngle;
                    }
                }
                else
                {
                    m_currentAngle += m_cannonChangeSpeed * Time.fixedDeltaTime;
                    if (m_currentAngle > m_selectedAngle)
                    {
                        m_currentAngle = m_selectedAngle;
                    }
                }
                Vector3 rot = m_cannon.transform.eulerAngles;
                rot.x = m_currentAngle;
                m_cannon.transform.eulerAngles = rot;
            }
            else
            {
                /*===============================================Fmod====================================================
                |   The parameter that tells the event that the cannon is no longer moving, reached it's destination.   |
                |   This will play that loading sound at the end of the event.                                          |
                =======================================================================================================*/
                m_cannonEvent.setParameterByID(m_cannonStopParameterID, 1.0f);
                m_elapsedFireDelay += Time.fixedDeltaTime;
                if (m_elapsedFireDelay >= m_fireDelay)
                {
                    m_elapsedFireDelay = 0.0f;
                    /*===============================================Fmod====================================================
                    |   Starts the fire sound.                                                                              |
                    =======================================================================================================*/
                    m_cannonFireEvent.start();
                    m_isActive = false;
                    GameObject ball = Instantiate(m_cannonBall, m_cannon.transform.GetChild(0).position - (m_cannon.transform.GetChild(0).up), Quaternion.identity) as GameObject;
                    ball.transform.SetParent(transform);
                    ball.GetComponent<Rigidbody>().AddForce(-m_cannon.transform.GetChild(0).up * m_power, ForceMode.Impulse);
                }
            }
        }
    }

    public void Fire(int a_index)
    {
        if (m_isActive)
            return;

        switch (a_index)
        {
            case 1:
                m_selectedAngle = 30.0f;
                m_power = 20.0f;
                m_isActive = true;
                break;
            case 2:
                m_selectedAngle = 45.0f;
                m_power = 15.0f;
                m_isActive = true;
                break;
            case 3:
                m_selectedAngle = 60.0f;
                m_power = 10.0f;
                m_isActive = true;
                break;
            default:
                break;
        }
        if (m_selectedAngle > m_currentAngle)
        {
            /*===============================================Fmod====================================================
            |   The Direction of the cannon. For a different sound.                                                 |
            =======================================================================================================*/
            m_cannonEvent.setParameterByID(m_cannonDirectionParameterID, 1.0f);
        }
        else
        {
            /*===============================================Fmod====================================================
            |   The Direction of the cannon. For a different sound.                                                 |
            =======================================================================================================*/
            m_cannonEvent.setParameterByID(m_cannonDirectionParameterID, -1.0f);
        }
    }
}