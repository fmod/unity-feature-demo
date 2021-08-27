/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                16/08/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Initialize the FMOD System and load the sound bank for the      |
|   Overworld scene.                                                                            |
===============================================================================================*/

using UnityEngine;


public class LoadOverworldSounds : MonoBehaviour 
{
    // Public Vars
    [FMODUnity.BankRef]
    public string m_bankString;

    // Private Vars
    FMOD.Studio.System m_studioSystem;

	void Start () 
	{
        FMODUnity.RuntimeManager.StudioSystem.initialize(512, FMOD.Studio.INITFLAGS.NORMAL, FMOD.INITFLAGS.NORMAL, (System.IntPtr)0);
        FMODUnity.RuntimeManager.LoadBank(m_bankString, true);
	}

    void OnDestroy()
    {
        FMODUnity.RuntimeManager.UnloadBank(m_bankString);
    }
}
