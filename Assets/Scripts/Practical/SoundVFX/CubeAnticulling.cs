/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                22/08/2016                                                      |
|   Scene:                      Sound VFX                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Stops the dancing cubes from being culled when they are only    |
|	just outside the cameras view.																|
===============================================================================================*/

using UnityEngine;
using System.Collections;

public class CubeAnticulling : MonoBehaviour
{
	// Use this for initialization
	void Start ()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.bounds = new Bounds(transform.position, new Vector3(100, 100, 100));
	}
}
