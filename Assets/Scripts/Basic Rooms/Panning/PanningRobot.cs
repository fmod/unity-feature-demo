/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Panning                                                         |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Rotates the robot depending on the scene.                       |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class PanningRobot : MonoBehaviour
{
    int m_isActive;
    float m_elapsed;
    /*===============================================Fmod====================================================
    |   The Studio Event Emitter is an FMOD script that can play, stop, change parameters.                  |
    =======================================================================================================*/
    FMODUnity.StudioEventEmitter m_event;
    Transform m_actor;
    Quaternion m_oldRotation;

	void Start ()
    {
        m_event = GetComponent<FMODUnity.StudioEventEmitter>();
        m_oldRotation = transform.rotation;
        m_isActive = 0;
        m_elapsed = 0.0f;
        m_actor = Camera.main.transform.parent;
	}
	void Update ()
    {
        if(m_isActive == 1)
        {
            m_elapsed += Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(m_actor.position - transform.position, Vector3.up), 40.0f * Time.deltaTime);
            if (m_elapsed >= 6.0f)
                m_isActive = -1;
        }
        else if(m_isActive == -1)
        {
            m_elapsed -= Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, m_oldRotation, 140.0f * Time.deltaTime);
            if (m_elapsed <= 4.0f)
            {
                m_isActive = 0;
                /*===============================================Fmod====================================================
                |   Simply Play the Studio Event Emitter by calling Play.                                               |
                =======================================================================================================*/
                m_event.Play();
                transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>().EnableEmission(true);
            }
        }
	}

    public void FacePlayer()
    {
        if (m_isActive != 0)
            return;
        m_isActive = 1;
        m_elapsed = 0.0f;
        /*===============================================Fmod====================================================
        |   Simply Stop the Studio Event Emitter by calling Stop.                                               |
        =======================================================================================================*/
        m_event.Stop();
    }
}