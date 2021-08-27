/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Cameron Baron                                                   |
|   Company:		            Firelight Technologies                                          |
|   Date:		                24/08/2016                                                      |
|   Scene:                      Overworld                                                       |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Options dropdown in main menu.                                  |
===============================================================================================*/

using UnityEngine;
using UnityEngine.UI;

public class DropdownSettingsScript : MonoBehaviour 
{
    public Dropdown m_qualityDropdown;
    public Dropdown m_resolutionDropdown;
    public Toggle m_fullscreenToggle;

    Resolution[] m_resolutions;

    void Start()
    {
        m_qualityDropdown.value = QualitySettings.GetQualityLevel();
        m_qualityDropdown.onValueChanged.AddListener(delegate { OnQualityValueChange(); });


        m_resolutionDropdown.ClearOptions();
        m_resolutions = Screen.resolutions;

        for (int i = 0; i < m_resolutions.Length; i++)
        {
            m_resolutionDropdown.options.Add(new Dropdown.OptionData(m_resolutions[i].ToString()));
            if (Screen.currentResolution.GetHashCode() == m_resolutions[i].GetHashCode())
            {
                m_resolutionDropdown.value = i;
                m_resolutionDropdown.captionText.text = m_resolutions[i].ToString();
            }
        }
        m_resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionValueChange(); });

        m_fullscreenToggle.isOn = Screen.fullScreen;
        m_fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggleChange(); });
    }

    void OnQualityValueChange()
    {
        QualitySettings.SetQualityLevel(m_qualityDropdown.value);
    }

    void OnResolutionValueChange()
    {
        Screen.SetResolution(m_resolutions[m_resolutionDropdown.value].width, m_resolutions[m_resolutionDropdown.value].height, m_fullscreenToggle.isOn);
    }

    void OnFullscreenToggleChange()
    {
        Screen.fullScreen = m_fullscreenToggle.isOn;
    }
}
