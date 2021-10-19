/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Programmer Sound                                                |
|   Fmod Related Scripting:     Yes                                                             |
|   Description:                Programmer sounds give the ability to select a sound from an    |
|   audio table. In order to use programmer sounds a callback is required which will occur      |
|   eveytime an event is created, destroyed, etc. This however uses Marshalling and is quite an |
|   advanced topic.                                                                             |
===============================================================================================*/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    static FMOD.Studio.EVENT_CALLBACK m_dialogueCallback;

    public FMODUnity.EventReference m_dialogueRef;

    void Start()
    {
        /*===============================================Fmod====================================================
        |   Create the delegate and assign it to a member variable, so the garbage collector does not free its  |
        |   memory.                                                                                             |
        =======================================================================================================*/
        m_dialogueCallback = new FMOD.Studio.EVENT_CALLBACK(DialogueEventCallback);
    }

    /*===============================================Fmod====================================================
    |   PlayDialogue's parameter is of type string. What gets passed into here is the "key" value from the  |
    |   "audio table" that was created inside "Fmod Studio". The key by default is the name of the file     |
    |   without the extension. To change this simply place a keys.txt inside the same folder as the file(s),|
    |   and to change the key simply write the key, comma, then the filename(.extenstion). You can look at  |
    |   the keys.txt that this audio table is using as a reference.                                         |
    =======================================================================================================*/
    public void PlayDialogue(string a_key)
    {
        /*===============================================Fmod====================================================
        |   Create an instance of the dialogue event. The same way you would create an editable one shot sound  |
        | from this example:
        | http://www.fmod.org/documentation/#content/generated/engine_new_unity/script_example_basic.html.      |
        =======================================================================================================*/
        FMOD.Studio.EventInstance dialogueInstance = FMODUnity.RuntimeManager.CreateInstance(m_dialogueRef);

        /*===============================================Fmod====================================================
        |   The reason why, in C#, that all the GCHandling and marshalling is used is because C# is a managed   |
        |   language and Fmod uses an unmanaged language, so in order to use Fmod in C#, some conversion is     |
        |   necessary.                                                                                          |   
        |   What this line does is stops the Garbage Collector from destroying the string so the dialogue can   |
        |   store it in it's userdata which will be used in the callback.                                       |
        =======================================================================================================*/
        GCHandle stringHandle = GCHandle.Alloc(a_key, GCHandleType.Pinned);
        dialogueInstance.setUserData(GCHandle.ToIntPtr(stringHandle));

        /*===============================================Fmod====================================================
        |   This line will set the callback of the Event Instance. So even though it's being released and we no |
        |   longer have control over it, when something happens inside the event, such as: when it's being      |
        |   created or when it creates the programmer sound, it will call the callback function and that's where|
        |   we can access that instance and set properties.                                                     |
        =======================================================================================================*/
        dialogueInstance.setCallback(m_dialogueCallback);
        dialogueInstance.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(dialogueInstance, transform, GetComponent<Rigidbody>());
        dialogueInstance.release();
    }
    /*===============================================Fmod====================================================
    |   This is the callback function that will get called when the Event Instance does something.          | 
    |   The param a_type is an enum telling the function why it had been called. More info on what actions  |
    |   inside Event Instance call this function can be found here:                                         |
    |   http://www.fmod.org/docs/content/generated/FMOD_STUDIO_EVENT_CALLBACK_TYPE.                         |
    |   The param a_instancePtr is the eventInstance itself.                                                |   
    |   The Param a_parameterPtr is a parameter that's type varies depending on what the callback is, in    |
    |   this case, if the type is CREATE_PROGRAMMER_SOUND then the parameter will be of type                |
    |   PROGRAMMER_SOUND_PROPERTIES.
    =======================================================================================================*/
    static FMOD.RESULT DialogueEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE a_type, IntPtr a_instancePtr, IntPtr a_parameterPtr)
    {
        /*===============================================Fmod====================================================
        |   These lines of code convert the string data back from unmanaged code to managed code so that we can |
        |   read the string.                                                                                    |
        =======================================================================================================*/
        var a_instance = new FMOD.Studio.EventInstance(a_instancePtr);
        IntPtr stringPtr;
        a_instance.getUserData(out stringPtr);

        GCHandle stringHandle = GCHandle.FromIntPtr(stringPtr);
        string key = stringHandle.Target as string;
        
        switch (a_type)
        {
            case FMOD.Studio.EVENT_CALLBACK_TYPE.CREATE_PROGRAMMER_SOUND:
                {
                    /*===============================================Fmod====================================================
                    |   When the instance creates the programmer sound. We can set what what sound it will use from the     |
                    |   audio table. This line is simply getting the parameters of the programmer sound. It will be used to |
                    |   set which sound, from the audio table, the programmer sound will use.                               |
                    =======================================================================================================*/
                    FMOD.Studio.PROGRAMMER_SOUND_PROPERTIES parameter = (FMOD.Studio.PROGRAMMER_SOUND_PROPERTIES)Marshal.PtrToStructure(a_parameterPtr, typeof(FMOD.Studio.PROGRAMMER_SOUND_PROPERTIES));

                    /*===============================================Fmod====================================================
                    |   GetSoundInfo will get the information from a sound inside the audio table, using the string key that|
                    |   was stored inside the Event Instanaces userData variable. It will store that information inside the |
                    |   Sound Info variable                                                                                 |
                    =======================================================================================================*/
                    FMOD.Studio.SOUND_INFO dialogueSoundInfo;
                    FMOD.RESULT result = FMODUnity.RuntimeManager.StudioSystem.getSoundInfo(key, out dialogueSoundInfo);
                    if (result != FMOD.RESULT.OK)
                    {
                        break;
                    }

                    /*===============================================Fmod====================================================
                    |   Using the Sound Info, the createSound function will create the sound. Because the sound is inside an|
                    |   audio table, the first param is the name of the bank and the next two will contain information,     |
                    |   about the audio table.
                    =======================================================================================================*/
                    FMOD.Sound dialogueSound;
                    result = FMODUnity.RuntimeManager.CoreSystem.createSound(dialogueSoundInfo.name_or_data, dialogueSoundInfo.mode, ref dialogueSoundInfo.exinfo, out dialogueSound);
                    if (result == FMOD.RESULT.OK)
                    {
                        /*===============================================Fmod====================================================
                        |   The sound is the audio table. The subsoundindex is the index of the sound inside of the audio table.|
                        =======================================================================================================*/
                        parameter.sound = dialogueSound.handle;
                        parameter.subsoundIndex = dialogueSoundInfo.subsoundindex;

                        /*===============================================Fmod====================================================
                        |   The paramater needs to then be passed pack to the original pointer which the eventInstance will be  |
                        |   used to play the correct sound after this function.                                                 |
                        =======================================================================================================*/
                        Marshal.StructureToPtr(parameter, a_parameterPtr, false);
                    }
                }
                break;
            case FMOD.Studio.EVENT_CALLBACK_TYPE.DESTROY_PROGRAMMER_SOUND:
                {
                    /*===============================================Fmod====================================================
                    |   Get the sound before the programmer sound is destroyed so that it can be released.                  |
                    =======================================================================================================*/
                    FMOD.Studio.PROGRAMMER_SOUND_PROPERTIES parameter = (FMOD.Studio.PROGRAMMER_SOUND_PROPERTIES)Marshal.PtrToStructure(a_parameterPtr, typeof(FMOD.Studio.PROGRAMMER_SOUND_PROPERTIES));
                    FMOD.Sound sound = new FMOD.Sound();
                    sound.handle = parameter.sound;
                    sound.release();
                }
                break;
            case FMOD.Studio.EVENT_CALLBACK_TYPE.DESTROYED:
                /*===============================================Fmod====================================================
                |   Just before the Event Instance has been destroyed, give back the string to the garbage collector so |
                |   it can free up the memory.                                                                          |
                =======================================================================================================*/
                stringHandle.Free();
                break;
        }
        return FMOD.RESULT.OK;
    }
}