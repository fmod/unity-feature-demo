/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Sound Spin                                                      |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Opens and closes the elevator door.                             |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class ElevatorDoors : MonoBehaviour
{
    public GameObject m_leftDoor, m_rightDoor;
    int m_isActive;
    public bool m_doorsOpen;

    void Start()
    {
        m_isActive = 0;
        m_doorsOpen = true;
    }
    void Update()
    {
        switch (m_isActive)
        {
            case -1:
                {
                    ClosingDoors();
                }
                break;
            case 1:
                {
                    OpeningDoors();
                }
                break;
            default:
                break;
        }
    }

    public void OpenDoors()
    {
        if (m_doorsOpen)
            return;
        m_isActive = 1;
    }
    public void CloseDoors()
    {
        if (!m_doorsOpen)
            return;
        m_isActive = -1;
    }

    void OpeningDoors()
    {
        Vector3 leftDoorPos = m_leftDoor.transform.localPosition;
        Vector3 rightDoorPos = m_rightDoor.transform.localPosition;

        leftDoorPos.z -= Time.deltaTime;
        rightDoorPos.z += Time.deltaTime;
        if (leftDoorPos.z < -1.4f)
        {
            leftDoorPos.z = -1.4f;
            rightDoorPos.z = 1.4f;
            m_doorsOpen = true;
            m_isActive = 0;
        }
        m_leftDoor.transform.localPosition = leftDoorPos;
        m_rightDoor.transform.localPosition = rightDoorPos;
    }
    void ClosingDoors()
    {
        Vector3 leftDoorPos = m_leftDoor.transform.localPosition;
        Vector3 rightDoorPos = m_rightDoor.transform.localPosition;

        leftDoorPos.z += Time.deltaTime;
        rightDoorPos.z -= Time.deltaTime;
        if (leftDoorPos.z > -0.5f)
        {
            leftDoorPos.z = -0.5f;
            rightDoorPos.z = 0.5f;
            m_doorsOpen = false;
            m_isActive = 0;
        }
        m_leftDoor.transform.localPosition = leftDoorPos;
        m_rightDoor.transform.localPosition = rightDoorPos;
    }
}