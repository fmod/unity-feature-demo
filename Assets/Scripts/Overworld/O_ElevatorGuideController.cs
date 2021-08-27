/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Brings up the guide or hides it, or calls the elevator.         |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class O_ElevatorGuideController : ActionObject
{
    public GuidePillar m_pillar;
    public O_Elevator m_elevator;
    public int m_floor;
    //public float m_guideY;
    public float m_elevatorY;
    [FMODUnity.EventRef]    public string m_buttonEvent;

    //bool m_guideIsCurrent;

    void Start()
    {
        InitButton();
        InitGlow();
        //m_guideIsCurrent = false;
    }
    void Update()
    {
        UpdateGlow();
    }

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        //if (m_guideIsCurrent)
        //{
        //    m_guideIsCurrent = false;
        m_elevator.ChangeFloor(m_floor, m_elevatorY);
        FMODUnity.RuntimeManager.PlayOneShot(m_buttonEvent, transform.position);
        //    m_pillar.Hide();
        //}
        //else
        //{
        //    m_guideIsCurrent = true;
        //    m_pillar.Summon(m_guideY);
        //}
    }
}