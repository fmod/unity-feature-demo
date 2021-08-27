/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Sound Spin                                                      |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Sound Spinning is a sound has a start, a loop and an end sound. |
|   E.g. An elevator starts to lift(the spin up) until it reaches a maximum velocity which is   |
|   where the sound will loop. The tigger is then applied which will exit the loop, into the    |
|   spin down segment of the event.                                                             |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour
{
    /*===============================================FMOD====================================================
    |   Get an existing StudioEventEmitter to control it from a script                                      |
    =======================================================================================================*/
    public FMODUnity.StudioEventEmitter m_event;
    public FMODUnity.StudioEventEmitter m_elevatorMusic;

    public ElevatorDoors m_doors;

    GameObject m_player;
    Vector3 m_selectedFloorPosition;
    Vector3 m_originalTransform;
    int m_currentFloor, m_selectedFloor;
    int m_isActive; //0 off, 1 close, 2 lift, 3 open
    float m_elapsed;

    void Start()
    {
        m_currentFloor = 0;
        m_selectedFloor = 0;
        m_isActive = 0;
        m_elapsed = 0.0f;
        m_player = Camera.main.transform.parent.gameObject;
        m_originalTransform = transform.position;
    }
    void Update()
    {
        m_elapsed += Time.deltaTime;
        switch (m_isActive)
        {
            case 1:
                {
                    ClosingDoors();
                }
                break;
            case 2:
                {
                    Lifting();
                }
                break;
            case 3:
                {
                    OpeningDoors();
                }
                break;
            default:
                break;
        }
    }
    public void ChangeFloor(int a_floor, Vector3 a_position)
    {
        if (m_currentFloor == a_floor || m_isActive > 0)
            return;

        m_selectedFloorPosition = a_position;
        m_isActive = 1;
        m_elapsed = 0.0f;
        m_selectedFloor = a_floor;
        m_doors.CloseDoors();
    }

    void ClosingDoors()
    {
        if (m_elapsed >= 1.0f)
        {
            /*===============================================FMOD====================================================
            |   Set the elevators end parameter to 0.                                                               |
            =======================================================================================================*/
            m_event.SetParameter("End", 0);
            /*===============================================FMOD====================================================
            |   Play the elevator sound. This will play the spin Up and then fall into the mid-sections loop.       |
            =======================================================================================================*/
            m_event.Play();
            /*===============================================FMOD====================================================
            |   Set the elevators music end parameter to 0.                                                         |
            =======================================================================================================*/
            m_elevatorMusic.SetParameter("End", 0);
            /*===============================================FMOD====================================================
            |   Play the elevator music. This will play the spin Up and then fall into the mid-sections loop, like  |
            |   the elevator sound above.                                                                           |
            =======================================================================================================*/
            m_elevatorMusic.Play();

            m_isActive++;
            m_elapsed = 0.0f;
        }
    }
    void OpeningDoors()
    {
        if (m_elapsed >= 2.0f)
        {
            m_doors.OpenDoors();
            m_isActive = 0;
        }
        else
        {
            transform.position = m_originalTransform + (transform.forward * Mathf.Sin(m_elapsed * 10.0f) * 0.05f);
            /*===============================================FMOD====================================================
            |   The best way to exit a sound spin, is to simply add a parameter to the event. The elevator has      |
            |   finished so set end to 1 meaning exit the mid-section loop and fall to the exit sound.              |
            =======================================================================================================*/
            m_event.SetParameter("End", 1);
            m_elevatorMusic.SetParameter("End", 1);
        }
    }
    void Lifting()
    {
        if (m_elapsed >= 2.0f * (Mathf.Abs(m_currentFloor - m_selectedFloor)))
        {
            m_currentFloor = m_selectedFloor;
            Vector3 move = m_selectedFloorPosition - transform.position;
            //Move Elevator
            transform.position = m_selectedFloorPosition;
            m_originalTransform += move;
            //Move Player
            m_player.transform.Translate(move);

            m_isActive++;
            m_elapsed = 0.0f;
        }
        else
        {
            transform.position = m_originalTransform + (transform.forward * Mathf.Sin(m_elapsed * 10.0f) * 0.05f);
        }
    }
}