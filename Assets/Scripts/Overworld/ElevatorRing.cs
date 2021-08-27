/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Rotates the text on the ring.                                   |
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class ElevatorRing : MonoBehaviour
{
    public float m_speed;
    Material m_mat;
	void Start () {
        m_mat = GetComponent<Renderer>().materials[0];
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = m_mat.mainTextureOffset;
        offset.x += m_speed * Time.deltaTime;
        m_mat.mainTextureOffset = offset;
    }
}
