/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Panning                                                         |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Warps the scene when active. Also sets the panning to 2D to 3D  |
|   or vice versa.                                                                              |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Panning : ActionObject
{
    public GameObject m_tvs;
    public GameObject m_objects;
    public ParticleSystem[] m_particles;
    bool m_is3D;
    float m_elapsed;
    bool m_isActive;
    float m_originalFOV, m_originalNear;
    /*===============================================Fmod====================================================
    |   Store the Button Sound.                                                                             |
    =======================================================================================================*/
    FMODUnity.StudioEventEmitter m_buttonEvent;

    void Start()
    {
        InitGlow();
        InitButton();
        m_originalFOV = Camera.main.fieldOfView;
        m_originalNear = Camera.main.nearClipPlane;
        m_isActive = false;
        m_elapsed = 0.0f;
        m_is3D = true;
        for (int i = 0; i < m_tvs.transform.childCount; ++i)
        {
            m_tvs.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(false);
        }
        foreach (ParticleSystem ps in m_particles)
        {
            ps.EnableEmission(false);
        }
        m_buttonEvent = GetComponent<FMODUnity.StudioEventEmitter>();
    }
    void Update()
    {
        UpdateGlow();
        if (m_isActive)
        {
            m_elapsed += Time.deltaTime;
            if (m_elapsed <= 0.9f)
            {
                Camera.main.fieldOfView += 125.0f * Time.deltaTime;
            }
            else
            {
                Camera.main.fieldOfView -= 900.0f * Time.deltaTime;
            }

            /*===============================================Fmod====================================================
            |   Iterate through all the robots and change their panning from 2D to 3D.                              |
            =======================================================================================================*/
            for (int i = 0; i < m_objects.transform.childCount; ++i)
            {
                /*===============================================Fmod====================================================
                |   The Studio Event Emitter is an FMOD script that can play, stop, change parameters.                  |
                =======================================================================================================*/
                FMODUnity.StudioEventEmitter em = m_objects.transform.GetChild(i).gameObject.GetComponent<FMODUnity.StudioEventEmitter>();
                if (!m_is3D)
                {
                    /*===============================================Fmod====================================================
                    |   The setParamterValue function takes in the name of the parameter, and the value to give it.         |
                    |   Parameters can be used to change volumes, or to jump to sections in the sound.                      |
                    =======================================================================================================*/
                    em.SetParameter("Panning", m_elapsed / 1.0f);
                }
                else
                {
                    /*===============================================Fmod====================================================
                    |   The setParamterValue function takes in the name of the parameter, and the value to give it.         |
                    |   Parameters can be used to change volumes, or to jump to sections in the sound.                      |
                    =======================================================================================================*/
                    em.SetParameter("Panning", 1.0f - (m_elapsed / 1.0f));
                }
            }

            if (m_elapsed >= 1.0f)
            {
                Camera.main.fieldOfView = m_originalFOV;
                Camera.main.nearClipPlane = m_originalNear;
                m_elapsed = 0.0f;
                m_isActive = false;
                m_is3D = !m_is3D;

                for (int i = 0; i < m_tvs.transform.childCount; ++i)
                {
                    m_tvs.transform.GetChild(i).GetChild(0).GetChild(0).gameObject.SetActive(!m_is3D);
                }

                Vector3 pos = m_objects.transform.localPosition;
                pos.x = !m_is3D ? -20.0f : 0.0f;

                foreach (ParticleSystem ps in m_particles)
                {
                    ps.EnableEmission(!m_is3D);
                }

                m_objects.transform.localPosition = pos;
                if (m_is3D)
                {
                    for (int i = 0; i < m_objects.transform.childCount; ++i)
                    {
                        m_objects.transform.GetChild(i).GetComponent<PanningRobot>().FacePlayer();
                    }
                }
                else
                {
                    for (int i = 0; i < m_objects.transform.childCount; ++i)
                    {
                        m_objects.transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<ParticleSystem>().EnableEmission(false);
                    }
                }
            }
        }
    }

    public override void ActionPressed(GameObject a_sender, KeyCode a_key)
    {
        m_isActive = true;
        Camera.main.nearClipPlane = 0.02f;
        m_buttonEvent.Play();
    }
}