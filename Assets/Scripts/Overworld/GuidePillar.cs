/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Shows and hides the guide pillar.                               |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class GuidePillar : MonoBehaviour
{
    public float m_hidingY;
    bool m_isActive;
    float m_selectionY;

    void Start()
    {
        m_selectionY = m_hidingY;
        m_isActive = false;
    }
    void Update()
    {
        if (m_isActive)
        {
            Vector3 pos = transform.position;
            float diff = m_selectionY - pos.y;
            pos.y += diff * Time.deltaTime * 2.0f;
            if (Mathf.Abs(diff) <= 0.005f)
            {
                pos.y = m_selectionY;
                m_isActive = false;
            }
            transform.position = pos;
        }
    }

    public void Hide()
    {
        m_isActive = true;
        m_selectionY = m_hidingY;
    }
    public void Summon(float a_y)
    {
        m_isActive = true;
        m_selectionY = a_y;
    }
}