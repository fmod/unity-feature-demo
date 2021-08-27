/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                10/10/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Musical note particle emitter.                                  |
===============================================================================================*/

using UnityEngine;


public class MusicNoteEmitter : MonoBehaviour 
{
    // Public Vars
    public FMODUnity.StudioEventEmitter m_event;
    public bool m_runEmitter = true;       // On/off switch
    public ParticleSystem m_particleSystem;

    // Private Vars
    float m_timer;

	void Start() 
	{
        m_particleSystem = GetComponent<ParticleSystem>();

        if (m_event == null)
        {
            // turn on emitter and destroy this script
            ParticleSystemEnable(true);
            Destroy(this);
        }
        
	}
	
	void Update()
    {
        if (m_event != null)
        {
            if (m_event.IsPlaying())
                m_runEmitter = true;
            else
                m_runEmitter = false;
        }

        ParticleSystemEnable(m_runEmitter);
    }

    #region Private Functions

    void ParticleSystemEnable(bool a_enable)
    {
		if (m_particleSystem.isPlaying == true) 
			m_particleSystem.EnableEmission (!a_enable);

		if (a_enable == true)
			m_particleSystem.Play ();
        else
			m_particleSystem.Stop ();

    }

    void OnValidate()
    {
        if (m_particleSystem != null)
            ParticleSystemEnable(m_runEmitter);
    }

    #endregion
}
