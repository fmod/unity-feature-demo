/*=================================================================
Project:		AIE FMOD   
Developer:		Cameron Baron
Company:		#COMPANY#
Date:			15/08/2016
==================================================================*/

using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PlayOneShotSound : MonoBehaviour 
{
    //---------------------------------Fmod-------------------------------
    //  Using the EventRef attribute will present the event selection
    //  UI in the Unity Editor.
    //--------------------------------------------------------------------
    [FMODUnity.EventRef]
    public string m_soundString;
    

    void OnTriggerEnter(Collider col)
    {
        //---------------------------------Fmod-------------------------------
        //  PlayOneShot plays a sound from start to finish and then releases
        //  all the resources for us. Ideal for short sound effects that you  
        //  don't need to keep track of or pause.
        //
        //  PlayOneShot(string) - used for 2D sounds.
        //  PlayOneShotAttached(string, gameobject) - plays sound from objects
        //  location, best for 3D sounds.
        //--------------------------------------------------------------------
        FMODUnity.RuntimeManager.PlayOneShotAttached(m_soundString, gameObject);
    }
}
