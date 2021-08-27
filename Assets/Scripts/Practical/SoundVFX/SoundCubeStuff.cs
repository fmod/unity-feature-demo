/*===============================================================================================
|   Project:                    FMOD Demo                                                       |
|   Developer:                  Cameron Baron                                                   |
|   Company:                    Firelight Technologies                                          |
|   Date:                       01/08/2016                                                      |
|   Scene:                      Sound VFX                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Passes the FFT information to the 'dancing cubes' shader.       |
===============================================================================================*/

using UnityEngine;

public class SoundCubeStuff : MonoBehaviour 
{
    // Public Vars
    public int m_gridWidth = 15;                        // Customisable grid size.
    public bool flipX;                                  // Used to mirror the grid on the X-axis
    public bool flipZ;                                  // Used to mirror the grid on the Z-axis

    // Private Vars
    [SerializeField]    GameObject m_cubePrefab;        // Prefab to use for visualisation.
    GameObject[][] m_cubes;                             // Array (grid) of prefabs.
    MainSound m_soundRef;                               // Reference to main sound script.
    float[] m_soundFFTData;                             // Array for sound fft data to be stored in.

    void Start () 
    {
        m_soundRef = FindObjectOfType<MainSound>();     // The first object found with the wanted script attached.
        
        if (m_soundRef == null)
        {
            Debug.LogError("No 'Main Sound' Script in scene!!!");
            DestroyImmediate(this);
            return;
        }

        // Create grid of cubes using an Array of Array's
        m_cubes = new GameObject[m_gridWidth][];

        // size on one axis = no of cubes * size of cube + space between cubes * (no of cubes - 1);
        float gridSize = m_gridWidth * m_cubePrefab.transform.localScale.z + (m_cubePrefab.transform.localScale.z * (m_gridWidth - 1));

        for (int i = 0; i < m_gridWidth; i++)
        {
            m_cubes[i] = new GameObject[m_gridWidth];
            for (int j = 0; j < m_gridWidth; j++)
            {                
                Vector3 pos = new Vector3(((flipX ? -1 : 1) * i * m_cubePrefab.transform.localScale.x) - (gridSize * 0.5f), 0, ((flipZ ? -1 : 1) * j * m_cubePrefab.transform.localScale.z) - (gridSize * 0.5f));

                m_cubes[i][j] = Instantiate(m_cubePrefab, transform.position + pos, transform.rotation) as GameObject;
                m_cubes[i][j].transform.SetParent(transform);
            }
        }
    }

    void  FixedUpdate () 
    {        
        // Get the update spectrum data from the main sound script;
        m_soundFFTData = m_soundRef.m_fftArray;
        // Loop through all cubes one at a time.
        for (int row = 0; row < m_gridWidth; row++)
        {
            for (int col = 0; col < m_gridWidth; col++)
            {
                int newCol = col;
                if (col < m_gridWidth - 1)
                {
                     newCol += 1;
                }

                // Assign the cube 
                m_cubes[row][col].GetComponent<Renderer>().material.SetFloat("_Amount", m_soundFFTData[(row + 1) * (newCol)]);
            }
        }
    }
}
