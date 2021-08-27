/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Opens and closes the elevator door.                             |
===============================================================================================*/

using UnityEngine;
using System.Collections;

public class O_ElevatorDoor : MonoBehaviour
{
    int m_isActive;
    bool m_doorsOpen;
    public bool IsDoorOpen { get { return m_doorsOpen; } }

    void Start()
    {
        m_isActive = 0;
        m_doorsOpen = false;
    }
    void Update()
    {
        if (m_isActive == 1)
        {
            //open
            Vector3 eular = transform.eulerAngles;
            eular.y -= 180.0f * Time.deltaTime;
            if (eular.y <= 0.0f)
            {
                eular.y = 0.0f;
                m_isActive = 0;
                m_doorsOpen = true;
            }
            transform.eulerAngles = eular;
        }
        else if (m_isActive == -1)
        {
            //close
            Vector3 eular = transform.eulerAngles;
            eular.y += 180.0f * Time.deltaTime;
            if (eular.y >= 180.0f)
            {
                eular.y = 180.0f;
                m_isActive = 0;
                m_doorsOpen = false;
            }
            transform.eulerAngles = eular;
        }
    }

    public void OpenDoor()
    {
        if (m_doorsOpen)
            return;
        m_isActive = 1;
    }
    public void CloseDoor()
    {
        if (!m_doorsOpen)
            return;
        m_isActive = -1;
    }
}