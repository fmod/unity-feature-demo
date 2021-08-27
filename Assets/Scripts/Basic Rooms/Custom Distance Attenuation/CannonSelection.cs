/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Custom Distance Attenuation                                     |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Passes in an index to the cannon controller onclick             |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class CannonSelection : ActionObject
{
    public int m_selection;
    public CannonController m_controller;

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
        m_controller.Fire(m_selection);
    }
}