using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class RandomVideo : MonoBehaviour
{
    VideoPlayer m_cVideo;
    Animation m_cAnimation;

    void Awake()
    {
        m_cVideo = GetComponent<VideoPlayer>();
        //m_cAnimation = GetComponent<Animation>();

        PlayVideo();

        //playAnimation();
    }

    void PlayVideo()
    {
        if(null != m_cVideo) 
        {
            //Debug.LogError(" AAAA " + Application.streamingAssetsPath);
            string videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Resources/Prefab/Video/B.mp4");
            m_cVideo.url = videoPath;
            m_cVideo.isLooping = true;
            m_cVideo.Play();
        }
    }

    void playAnimation()
    {
        if (null == m_cAnimation)
            return;


        m_cAnimation.Play();
    }

}
