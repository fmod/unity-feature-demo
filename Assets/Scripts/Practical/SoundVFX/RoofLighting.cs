/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                03/10/2016                                                      |
|   Scene:                      Sound VFX                                                       |
|   Fmod Related Scripting:                                                                   |
|   Description:                                                  |
===============================================================================================*/

using UnityEngine;


public class RoofLighting : MonoBehaviour 
{
    // Public Vars
    public Color m_lightColor;
    public Color m_lightOff;
    public bool m_partyMode = false;

    // Private Vars
    Light m_lightRef;
    Material m_materialRef;

	void OnEnable () 
	{
        m_materialRef = GetComponentInChildren<Renderer>().sharedMaterials[1];
        m_lightRef = GetComponent<Light>();

        if (!m_partyMode)
        {
            TurnOnLight();
        }
    }
	
	void Update ()
    {      
	    if (m_lightRef.color != m_lightOff && m_partyMode)
        {
            m_lightRef.color = Color.Lerp(m_lightRef.color, m_lightOff, 0.2f);
            m_materialRef.SetColor("_EmissionColor", m_lightRef.color);
        }
	}

    public void TurnOnLight()
    {
        m_lightRef.color = m_lightColor;
        m_materialRef.SetColor("_EmissionColor", m_lightRef.color);
    }

	#region Private Functions

    void OnValidate()
    {
        m_materialRef = GetComponentInChildren<Renderer>().sharedMaterials[1];
        m_lightRef = GetComponent<Light>();

        m_materialRef.SetColor("_EmissionColor", m_lightColor);
        m_lightRef.color = m_lightColor;
    }

	#endregion
}
