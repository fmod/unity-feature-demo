/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Scatter                                                         |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Rotates GameObjects over time.                                  |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public Vector3 m_rotationPerSecond;
    public bool m_randomStart;
	
    void Start ()
    {
	    if(m_randomStart)
        {
            transform.Rotate(m_rotationPerSecond.normalized * Random.Range(45, 315));
        }
	}
	void Update ()
    {
        transform.Rotate(m_rotationPerSecond * Time.deltaTime);
	}
}