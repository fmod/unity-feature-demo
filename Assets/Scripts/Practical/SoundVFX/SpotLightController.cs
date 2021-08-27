/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                10/10/2016                                                      |
|   Scene:                      Sound VFX                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Controls the roof mounted spotlights.                           |
===============================================================================================*/
using UnityEngine;

public class SpotLightController : MonoBehaviour 
{
    // Public Vars
    public MainSound m_soundRef;        // Reference to the script containing the music fft data.
    public GameObject[] m_signObjects;  // Sign to change the emission on.
    public GameObject[] m_wallObjects;  // Walls to change the emission on.
    public RoofLighting[] m_lights;     // Room mounted spotlights to turn on/off.

	// Private Vars
    int m_lastBeat = -1;                // Keep a reference to the last beat bar.

	void Start () 
	{
        if (m_lights.Length < 1 || m_soundRef == null)
        {
            Debug.Log("Missing the lights or sound!");
            Destroy(this);
        }
	}
	
	void Update () 
	{
        int beat = m_soundRef.m_timelineInfo.beat;
        if (m_soundRef.m_timelineInfo.tempo > 140 && m_lastBeat != beat)
        {
            ActivateRandomLights();
            m_lastBeat = beat;
        }
        else if (m_soundRef.m_timelineInfo.tempo < 140)
        {
            for (int i = 0; i < m_lights.Length; i++)
            {
                m_lights[i].m_partyMode = false;
            }
        }

        int index = Random.Range(0, 5);
        // Change the emission of the second material attached to the walls.
        for (int i = 0; i < m_wallObjects.Length; i++)
        {
            Renderer rend = m_wallObjects[i].GetComponent<Renderer>();
            rend.materials[1].SetFloat("_EmissionAmount", m_soundRef.m_fftArray[index] * 5);
        }
        // Change the emission of the sign material.
        for (int i = 0; i < m_signObjects.Length; i++)
        {
            Renderer rend = m_signObjects[i].GetComponent<Renderer>();
            rend.material.SetFloat("_EmissionAmount", m_soundRef.m_fftArray[index] * 5);
        }
    }

    /// <summary>
    /// Turn on two random roof mounted lights.
    /// </summary>
	public void ActivateRandomLights()
    {
        int index = Random.Range(0, m_lights.Length);
        m_lights[index].TurnOnLight();
        m_lights[index].m_partyMode = true;

        int index2 = Random.Range(0, m_lights.Length);
        while (index == index2)
        {
            index2 = Random.Range(0, m_lights.Length);
        }
        m_lights[index2].TurnOnLight();
        m_lights[index2].m_partyMode = true;
    }
}
