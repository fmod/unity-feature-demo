/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Scatter                                                         |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Class that is used in the mobile class.                         |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{
    public float m_xOffset, m_zOffset;
    public float m_speed;
    public Vector3 m_lastLocalPosition;
	void Start ()
    {
	
	}
	void Update ()
    {
	
	}
    void LateUpdate()
    {
        m_lastLocalPosition = transform.localPosition;
    }
}