/*=================================================================
Project:		AIE FMOD
Developer:		Cameron Baron
Company:		#COMPANY#
Date:			02/08/2016
==================================================================*/

using UnityEngine;

public class HighlightableObject : MonoBehaviour
{
	public bool m_highlighted = false;

	public string m_helpInfo;

	Material m_mat;

	// Use this for initialization
	void Awake ()
	{
		m_mat = GetComponent<Renderer>().material;
		m_mat.SetFloat("_Outline", 0.0005f);
	}	

	void OnMouseEnter()
	{
		m_mat.SetFloat("_OutlineEnabled", 1.0f);
	}

	void OnMouseExit()
	{
		m_mat.SetFloat("_OutlineEnabled", 0.0f);
	}
}
