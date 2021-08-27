/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Controls the overworld elevator.                                |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class O_Elevator : MonoBehaviour
{
    /*===============================================FMOD====================================================
    |   Get an existing StudioEventEmitter to control it from a script                                      |
    =======================================================================================================*/
    public FMODUnity.StudioEventEmitter m_event;

    public GameObject m_outerDoors;
    public O_ElevatorDoor m_elevatorDoor;
    public float m_speed;
    public float m_startFloorY;

    ActorControls m_actor;
    int m_isActive; //0 off, 1 close, 2 lift, 3 open
    int m_currentFloor;
    int m_selectedFloor;
    float m_selectedFloorY;
    float m_elapsed;
    bool m_playerInElevator;

    void Start()
    {
        m_actor = Camera.main.transform.parent.gameObject.GetComponent<ActorControls>();
        m_isActive = -1;
        m_currentFloor = -1;
        m_selectedFloor = -1;
        m_selectedFloorY = m_startFloorY;
        m_playerInElevator = false;
    }
    void Update()
    {
        switch (m_isActive)
        {
            case -1:                    //Tutorial stage
                {
                    InTutorial();
                }
                break;
            case 1:                     //Close
                {
                    ClosingDoors();
                }
                break;
            case 2:                     //Lift
                {
                    Lifting();
                }
                break;
            case 3:                     //Open
                {
                    OpeningDoors();
                }
                break;
            default:
                break;
        }
    }

    public void ChangeFloor(int a_floor, float a_floorY)
    {
        if (m_isActive != 0 || m_currentFloor == a_floor)
            return;

        m_isActive = 1;                                                                                     //Closing Doors
        m_selectedFloor = a_floor;
        m_selectedFloorY = a_floorY;
        m_elevatorDoor.CloseDoor();
        if (m_currentFloor >= 0)                                                                            //If not in tutorial stage
            m_outerDoors.transform.GetChild(m_currentFloor).GetComponent<O_ElevatorDoor>().CloseDoor();     //Shut outer door on this floor
    }

    void CenterActor()
    {
        Vector3 playerPos = m_actor.transform.position;                                                     //Player Position
        Vector3 elevatorMiddleDirection = transform.position - playerPos;
        elevatorMiddleDirection.y = 0;                                                                      //Don't take 0 into account
        if (elevatorMiddleDirection.magnitude > 1.0f)                                                       //If the direction is greater than 1.0f normalize
            elevatorMiddleDirection.Normalize();
        if (elevatorMiddleDirection.magnitude != 0.0f)
            m_actor.GetComponent<CharacterController>().Move(elevatorMiddleDirection * 0.05f);
    }
    void InTutorial()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_selectedFloor = 0;
            m_isActive = 2;
            m_actor.DisableMovement = true;
            m_event.Play();
            m_event.SetParameter("End", 0.0f);
        }
    }
    void ClosingDoors()
    {
        if (!m_elevatorDoor.IsDoorOpen)
        {
            m_isActive = 2;
            m_event.Play();
            m_event.SetParameter("End", 0.0f);
        }
    }
    void OnTriggerEnter(Collider a_col)
    {
        if (m_isActive == 0)
            return;
        if (a_col.gameObject.tag == "Player")
        {
            CenterActor();
            m_actor.DisableMovement = true;

        }
    }
    void OnTriggerStay(Collider a_col)
    {
        if (m_isActive == 0)
            return;
        if (a_col.gameObject.tag == "Player")
        {
            CenterActor();
            m_actor.DisableMovement = true;
            if (m_isActive == 2)    //If elevator is lifting move player with it
            {
                m_playerInElevator = true;
            }
        }
    }
    void OpeningDoors()
    {
        if (m_elevatorDoor.IsDoorOpen)
        {
            m_isActive = 0;
            m_actor.DisableMovement = false;
            m_playerInElevator = false;
        }
    }
    void Lifting()
    {
        Vector3 pos = transform.position;
        bool finished = false;

        if (m_selectedFloor > m_currentFloor)                                   //UP
        {
            transform.Translate(0.0f, m_speed * Time.deltaTime, 0.0f);          //Move Elevator
            if (pos.y >= m_selectedFloorY)                                      //Arrived at destination
            {
                pos.y = m_selectedFloorY;
                transform.position = pos;
                finished = true;
            }
            else if (pos.y >= m_selectedFloorY - 2.0f)                          //Almost at destination, trigger exit sound
            {
                /*===============================================FMOD====================================================
                |   This is how to set a parameter of an external eventEmitter.                                         |
                =======================================================================================================*/
                m_event.SetParameter("End", 1.0f);
            }
        }
        else //DOWN
        {
            transform.Translate(0.0f, -m_speed * Time.deltaTime, 0.0f);         //Move Elevator
            if (pos.y <= m_selectedFloorY)                                      //Arrived at destination
            {
                pos.y = m_selectedFloorY;
                transform.position = pos;
                finished = true;
            }
            else if (pos.y <= m_selectedFloorY + 2.0f)                          //Almost at destination, trigger exit sound
            {
                /*===============================================FMOD====================================================
                |   This is how to set a parameter of an external eventEmitter.                                         |
                =======================================================================================================*/
                m_event.SetParameter("End", 1.0f);
            }
        }

        if (finished)
        {
            m_currentFloor = m_selectedFloor;
            m_isActive = 3;
            m_elevatorDoor.OpenDoor();
            if (m_currentFloor >= 0)
                m_outerDoors.transform.GetChild(m_currentFloor).GetComponent<O_ElevatorDoor>().OpenDoor();
        }

        if (m_playerInElevator)
        {
            Vector3 playerPos = m_actor.transform.position;
            playerPos.y = transform.position.y - 1.0f + 0.7f;
            m_actor.transform.position = playerPos;
        }
    }
}