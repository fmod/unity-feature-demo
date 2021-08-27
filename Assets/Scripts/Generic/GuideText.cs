/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Plays the attached guide.                                       |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class GuideText : ActionObject
{
    public Guide m_guide;

    public override void ActionPressed(GameObject sender, KeyCode a_key)
    {
        m_guide.Play();
    }
}