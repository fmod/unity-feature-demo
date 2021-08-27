/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Scatter                                                         |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Rotate the keys when object enters trigger.                     |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class KeyRotate : MonoBehaviour
{
    bool m_downRotation;
    Collider m_col;

    void Start()
    {
        m_downRotation = false;
    }
    void Update()
    {
        Vector3 rot = transform.GetChild(0).localEulerAngles;
        rot.x -= 4 * Time.deltaTime;
        if (rot.x <= 0)
        {
            rot.x = 0;
        }

        if (!m_col)
        {
            m_downRotation = false;
        }
        if (m_downRotation)
        {
            rot.x += 8 * Time.deltaTime;
            if (rot.x >= 4)
            {
                rot.x = 4;
            }
        }
        transform.GetChild(0).localEulerAngles = rot;
    }

    void OnTriggerEnter(Collider a_col)
    {
        m_col = a_col;
        m_downRotation = true;
    }
    void OnTriggerStay(Collider a_col)
    {
        m_downRotation = true;
    }
    void OnTriggerExit(Collider a_col)
    {
        m_downRotation = false;
    }
}