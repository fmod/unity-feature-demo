/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Surfaces and Event Cone Angle                                   |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Calls Move on the Actor when it enters the trigger              |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class MovePlayer : MonoBehaviour
{
    public float m_speed;

	void Start () {
	
	}
	void Update () {

    }

    void OnTriggerStay(Collider a_col)
    {
        if (a_col.gameObject.tag == "Player")
        {
            a_col.gameObject.GetComponent<ActorControls>().MoveActor(-transform.up * m_speed * Time.deltaTime);
        }
    }
}