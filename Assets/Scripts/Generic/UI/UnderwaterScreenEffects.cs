/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                06/09/2016                                                      |
|   Scene:                      Snapshot Room                                                   |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Used to enable an underwater shader effect to the camera.       |
===============================================================================================*/

using UnityEngine;


public class UnderwaterScreenEffects : MonoBehaviour 
{
    // Public Vars
    public GameObject m_underwaterUI;

	// Private Vars

	void Start () 
	{
	    if (m_underwaterUI)
        {
            m_underwaterUI.SetActive(false);
        }
	}

	#region Private Functions

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Water"))
        {
            m_underwaterUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Water"))
        {
            m_underwaterUI.SetActive(false);
        }
    }

    #endregion
}
