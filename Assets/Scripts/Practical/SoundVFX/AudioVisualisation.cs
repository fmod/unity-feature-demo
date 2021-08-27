/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                09/08/2016                                                      |
|   Scene:                      Sound VFX                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                This script is to be attached to a plane/quad/object that will  |
|   show the visualisation texture, all we do in this is set the shader	and pass it the texture | 
|   from the MainSound Script. All the calculations for the data are either done in the         |
|   MainSound script or one of the shaders.                                                     |
===============================================================================================*/

using UnityEngine;

public enum SHADERTYPE
{
    BAR_VIS,
    LINE_VIS,
	STRING_VIS,
}

public class AudioVisualisation : MonoBehaviour 
{
    // Public Vars

    // Private Vars
    [SerializeField] SHADERTYPE shaderType;	// Enum to define the type of shader to use.
    [SerializeField]	Color m_color;		// Color variable used for albedo and emission.
    MainSound soundRef;						// Reference to the Main Sound Script which holds the sound data and texture.
    string shaderName;						// String of current shader.
    Material m_mat;							// Current material reference.

	void Start()
    {
        soundRef = FindObjectOfType<MainSound>();	// Find the main sound script to then access the sound information.
        if (soundRef == null)						// If not found, log error, and destroy this script.
        {
            Debug.LogError("No MainSound in scene!!!");
            DestroyImmediate(this);
            return;
        }

        switch (shaderType)
        {
            case SHADERTYPE.BAR_VIS: shaderName = "Custom/BarSurfaceShader";
                break;
            case SHADERTYPE.LINE_VIS: shaderName = "Custom/LineSurfaceShader";
                break;
			case SHADERTYPE.STRING_VIS: shaderName = "Custom/StringVFXShader";
				break;
        }

        m_mat = GetComponent<Renderer>().material;
        m_mat.shader = Shader.Find(shaderName);

        if (m_mat.shader.name != shaderName)	// If the shader is not found, log error and destroy this script.
        {
            Debug.Log("Shader " + shaderName + " not found!");
            DestroyImmediate(this);
            return;
        }

        m_mat.SetVector("_Color", m_color);		// Update the color.
        m_mat.SetTexture("_SoundImage", soundRef.m_soundTex);	// Get the texture that is made from the sound spectrum data.
        m_mat.SetFloat("_Emission", 1.0f);		// Enable Emission as default, nice and bright.
    }
	
	void Update()
    {
        m_mat.SetVector("_Color", m_color);		// Update the color, incase it is changed.
    }
}