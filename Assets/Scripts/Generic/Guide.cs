/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Iterates through all the guide children and pulses them.        |
===============================================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Guide : MonoBehaviour
{
    GameObject m_arrowsContainer;
    public float m_pulse = 0.75f;
    float m_pulseElapsed;
    public int m_iterations = 3;
    float m_currentIterations;
    int m_currentIndex;
    bool m_isPlaying;

    void Start()
    {
        m_arrowsContainer = transform.GetChild(0).gameObject;
        m_isPlaying = false;
        m_pulseElapsed = 0.0f;
        m_currentIterations = 0;
        m_currentIndex = 0;
    }
    void Update()
    {
        if (m_isPlaying)
        {
            m_pulseElapsed += Time.deltaTime;
            if (m_pulseElapsed <= m_pulse)
            {
                Color color = m_arrowsContainer.transform.GetChild(m_currentIndex).gameObject.GetComponent<Renderer>().material.GetColor("_Color");
                color.a = m_pulseElapsed / m_pulse;
                m_arrowsContainer.transform.GetChild(m_currentIndex).gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
            }
            else
            {
                Color color = m_arrowsContainer.transform.GetChild(m_currentIndex).gameObject.GetComponent<Renderer>().material.GetColor("_Color");
                color.a = 0.0f;
                m_arrowsContainer.transform.GetChild(m_currentIndex).gameObject.GetComponent<Renderer>().material.SetColor("_Color", color);
                m_currentIndex++;
                m_pulseElapsed = 0;
                if (m_currentIndex == m_arrowsContainer.transform.childCount)
                {
                    m_currentIndex = 0;
                    if (m_iterations != 0)
                    {
                        m_currentIterations++;
                        if (m_currentIterations == m_iterations)
                        {
                            m_isPlaying = false;
                        }
                    }
                }
            }
        }
    }

    public void Play()
    {
        m_isPlaying = true;
        m_currentIndex = 0;
        m_currentIterations = 0;
        m_pulseElapsed = 0;
        for (int i = 0; i < m_arrowsContainer.transform.childCount; ++i)
        {
            Color previousColor = m_arrowsContainer.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.GetColor("_Color");
            previousColor.a = 0.0f;
            m_arrowsContainer.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.SetColor("_Color", previousColor);
        }
    }
    public void Stop()
    {
        m_isPlaying = false;
        for (int i = 0; i < m_arrowsContainer.transform.childCount; ++i)
        {
            Color previousColor = m_arrowsContainer.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.GetColor("_Color");
            previousColor.a = 0.0f;
            m_arrowsContainer.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.SetColor("_Color", previousColor);
        }
    }
}
