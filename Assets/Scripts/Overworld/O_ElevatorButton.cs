/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Tells the elevator to change floors.                            |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class O_ElevatorButton : ActionObject
{
    /*===============================================FMOD====================================================
    |   Call this to display it in Unity Inspector.                                                         |
    =======================================================================================================*/
    [FMODUnity.EventRef]
    public string m_event;
    public O_Elevator m_elevator;
    public int m_floor;
    public float m_floorY;
    
    void Start()
    {
        InitButton();
        InitGlow();
    }
    void Update()
    {
        UpdateGlow();
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        m_elevator.ChangeFloor(m_floor, m_floorY);        
        /*===============================================FMOD====================================================
        |   You can play a oneshot sound using the RuntimeManager, it just needs the event name and a position. |
        =======================================================================================================*/
        FMODUnity.RuntimeManager.PlayOneShot(m_event, transform.position);
    }
    public override void ActionDown(GameObject sender, KeyCode a_key)
    {
        m_elevator.ChangeFloor(m_floor, m_floorY);
    }
}