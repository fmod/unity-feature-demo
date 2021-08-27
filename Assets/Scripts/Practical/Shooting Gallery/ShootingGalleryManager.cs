/*===============================================================================================
|   Project:		            FMOD Demo                                                       |
|   Developer:	                Matthew Zelenko - http://www.mzelenko.com                       |
|   Company:		            Firelight Technologies                                          |
|   Date:		                20/09/2016                                                      |
|   Scene:                      Shooting Gallery                                                |
|   Fmod Related Scripting:     No                                                              |
|   Description:                Controls scoring, tracks, and rounds. Calls SG_MainSpeaker      |
|   functions.                                                                                  |
===============================================================================================*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShootingGalleryManager : MonoBehaviour
{
    public Cart m_cart;
    public int m_winScore;
    public SG_MainSpeaker m_mainSpeaker;
    public BaseTarget[] m_targets;
    public Text[] m_scores;
    public Track m_trackHolder;

    int m_currentScore;
    List<Transform> m_tracks;
    int m_currentTrackIndex;
    int m_nextRoomStartIndex;
    int m_active;
    public bool IsActive { get { return m_active > 0; } }
    bool m_isPaused;
    public bool IsPaused { get { return m_isPaused; } }

    void Start()
    {
        m_currentScore = 0;
        m_active = 0;
        m_nextRoomStartIndex = 0;
        m_isPaused = false;

        //store tracks
        m_tracks = new List<Transform>();
        for (int i = 0; i < m_trackHolder.transform.childCount; ++i)
        {
            m_tracks.Add(m_trackHolder.transform.GetChild(i));
        }
        m_currentTrackIndex = m_trackHolder.m_startingIndex;

        //set carts position and forward
        m_cart.transform.position = m_tracks[m_currentTrackIndex].transform.position;
        m_cart.transform.forward = m_tracks[++m_currentTrackIndex].transform.position - m_cart.transform.position;
    }
    void Update()
    {

    }

    public void Play()
    {
        if (m_active == 0)
        {
            Reset();
            m_mainSpeaker.Play();
            m_mainSpeaker.SetRound(m_nextRoomStartIndex + 1);
            m_active = 1;
        }
        else
        {
            m_isPaused = false;
            m_mainSpeaker.Pause(m_isPaused);
        }
        foreach (BaseTarget t in m_targets)
        {
            t.Play();
        }
    }
    public void Pause()
    {
        m_isPaused = true;
        m_mainSpeaker.Pause(m_isPaused);
        foreach (BaseTarget t in m_targets)
        {
            t.Stop();
        }
    }
    void Reset()
    {
        m_nextRoomStartIndex = 0;
        m_active = 0;
        //Clear score
        ClearScore();

        ResetTargets();
        m_isPaused = false;
        m_mainSpeaker.Pause(m_isPaused);

        //Reset game result
        m_mainSpeaker.SetGameResult(0);
        //Reset round
        m_mainSpeaker.SetRound(0);
        m_mainSpeaker.Stop();
    }
    void ResetTargets()
    {
        //Reset targets
        foreach (BaseTarget t in m_targets)
        {
            t.Reset();
        }
    }
    void ResetCart()
    {
        //Set position
        m_cart.transform.position = m_tracks[m_currentTrackIndex - 1].transform.position;
        //Set forward
        m_cart.transform.forward = m_tracks[m_currentTrackIndex].transform.position - m_cart.transform.position;
        //Zero out currentVelocity
        m_cart.CurrentVelocity = 0.0f;
    }
    public void AddScore(int a_points)
    {
        m_currentScore += a_points;
        foreach (Text t in m_scores)
        {
            t.text = m_currentScore.ToString();
        }
    }
    public void ClearScore()
    {
        m_currentScore = 0;
        foreach (Text t in m_scores)
        {
            t.text = m_currentScore.ToString();
        }
    }
    public void IncremetTrack()
    {
        m_currentTrackIndex++;
        //Wrap around track index if it gets out of range of the size of tracks count
        m_currentTrackIndex %= (m_tracks.Count - 1);

        if (m_currentTrackIndex - m_trackHolder.m_startingIndex == 1)
        {
            m_mainSpeaker.SetGameResult(m_currentScore >= m_winScore ? 1 : -1);
            Pause();
            ResetCart();
            m_active = 0;
        }
        else
        {
            if (m_currentTrackIndex == m_trackHolder.m_roomStartIndices[m_nextRoomStartIndex])
            {
                Mathf.Min(m_nextRoomStartIndex++, m_trackHolder.m_roomStartIndices.Length - 1);
                m_mainSpeaker.SetRound(m_nextRoomStartIndex + 1);
                m_nextRoomStartIndex %= 2;
            }
        }
    }
    public Transform GetCurrentTrack()
    {
        return m_tracks[m_currentTrackIndex];
    }
    public Transform GetNextTrack()
    {
        return m_tracks[m_currentTrackIndex + 1];
    }
}
