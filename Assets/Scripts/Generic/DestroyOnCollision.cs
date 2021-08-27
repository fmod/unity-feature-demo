/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      All                                                             |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Destroy a GameObject on Collision. Can Provide layers to ignore.|
===============================================================================================*/
using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour 
{
    public int[] m_ignoreLayers;
    int m_layerMask;
    void Start()
    {
        foreach (int l in m_ignoreLayers)
        {
            m_layerMask |= 1 << l;
        }
    }
    void OnCollisionEnter(Collision a_col)
    {
        if (m_layerMask == 0 || (m_layerMask & (1 << a_col.gameObject.layer)) != m_layerMask)
        {
            //Debug.Log(a_col.gameObject.name);
            Destroy(gameObject);
        }
    }
}
