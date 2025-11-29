using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Threading.Tasks;


public enum E_STATE
{
    NONE = 0,
    INIT,
    READY,
    START,
    PLAY,
    END,
    MAX
}


public class Main : MonoBehaviour
{
    /// <summary>
    /// VARIABLES......
    /// </summary>

    #region VARIABLES......

    public static Main Instance;

    public Transform m_tMapRoot;
    public Transform m_tEnemyRoot;
    public Transform m_tActorRoot;
    public Transform m_tBulletRoot;

    public List<Button> m_cBtns;

    public Animator m_cAnimChar;
    public VideoPlayer m_videoPlayer;


    private MapManager m_cMapManager;
    //private EnemyManager m_cEnemyManager;
    //private ActorManager m_cActorManager;
    //private BulletManager m_cBulletManager;
    //private CollisionManager m_cCollisionManager;

    //public EnemyManager Enemys
    //{
    //    get
    //    {
    //        return m_cEnemyManager;
    //    }
    //}

    //public ActorManager Actors
    //{
    //    get
    //    {
    //        return m_cActorManager;
    //    }
    //}

    //public BulletManager Bullets
    //{
    //    get
    //    {
    //        return m_cBulletManager;
    //    }
    //}


    private E_STATE m_eState;

    private double m_dVideoTime;
    private double m_dVideoLength;
    private bool m_bIsVideoPlay;

    #endregion


    /// <summary>
    /// BASE FUNCTIONS......
    /// </summary>

    #region BASE FUNCTIONS......

    void Awake()
    {
        Instance = this;

        if(m_cAnimChar != null )
            m_cAnimChar.Play("idle");

        m_bIsVideoPlay = false;
        m_dVideoTime = 0.0f;
        m_dVideoLength = 0.0f;
        m_videoPlayer.gameObject.SetActive(m_bIsVideoPlay);

        if (null != m_cBtns)
        {
            for (int i = 0; i < m_cBtns.Count; i++)
                m_cBtns[i].onClick.RemoveAllListeners();

            if (null != m_cBtns[0])
                m_cBtns[0].onClick.AddListener(() => doDance(0));

            if (null != m_cBtns[1])
                m_cBtns[1].onClick.AddListener(() => doDance(1));

            if (null != m_cBtns[2])
                m_cBtns[2].onClick.AddListener(() => doDance(2));

        }

        //reset();

        //changeState(E_STATE.INIT);
    }

    public void doDance1()
    {
        doDance(0);
    }

    public void doDance2()
    {
        doDance(1);
    }

    public void doDance3()
    {
        doDance(2);
    }

    private void doDance(int index)
    {
        if (null == m_cAnimChar)
            return;


        //switch(index)
        //{
        //    case 0: { m_cAnimChar.Play("test01"); break;  }
        //    case 1: { m_cAnimChar.Play("test02"); break;  }
        //    case 2: { m_cAnimChar.Play("test03"); break;  }
        //    default: { m_cAnimChar.Play("test01"); break;  }
        //}

        //m_videoPlayer.Play();
        m_bIsVideoPlay = true;
        m_videoPlayer.gameObject.SetActive(m_bIsVideoPlay);
        string clipName = "Swivel_Chair_Z_ORG.mp4";
        
        switch (index)
        {
            case 0:
                {
                    clipName = "Swivel_Chair_Z_High.mp4";
                    break;
                }

            case 1:
                {
                    clipName = "Swivel_Chair_Z_Middle.mp4";
                    break;
                }

            case 2:
                {
                    clipName = "Swivel_Chair_Z_Low.mp4";
                    break;
                }

            default:
                {
                    break;
                }
        }

        string videoPath = "Avatar/Video/" + clipName;

//#if UNITY_EDITOR
        //videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Resources/Prefab/Video/" + clipName);//"Avatar/Video/" + clipName;
//#elif UNITY_WEBGL
        videoPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Resources/Prefab/Video/"+ clipName);
//#endif


        m_videoPlayer.url = videoPath;
        m_dVideoLength = m_videoPlayer.clip.length;
        m_videoPlayer.isLooping = false;
        m_videoPlayer.Play();

    }

    private void Update()
    {

        if(true == m_bIsVideoPlay)
        {
            m_dVideoTime += Time.deltaTime * Time.timeScale;

            //Debug.LogError(" !!!!! " + m_dVideoTime + " , " + m_dVideoLength);

            if(m_dVideoLength <= m_dVideoTime) 
            {
                m_dVideoTime = 0.0f;
                m_bIsVideoPlay = false;
                m_videoPlayer.gameObject.SetActive(m_bIsVideoPlay);
            }
        }

        if (m_eState == E_STATE.NONE || m_eState == E_STATE.MAX)
            return;

        if (m_eState == E_STATE.INIT)
            return;


        switch (m_eState)
        {
            case E_STATE.READY:
                {
                    break;
                }
            case E_STATE.START:
                {
                    break;
                }
            case E_STATE.PLAY:
                {
                    break;
                }

            case E_STATE.END:
                {
                    break;
                }

            default:
                {
                    break;
                }
        }

    }

#endregion


    /// <summary>
    /// PUBLIC FUNCTIONS......
    /// </summary>

    #region PUBLIC FUNCTIONS......

    

    #endregion


    /// <summary>
    /// PRIVATE FUNCTIONS......
    /// </summary>

    #region PRIVATE FUNCTIONS......

    private void changeState(E_STATE newState)
    {
        if (m_eState == newState)
            return;

        m_eState = newState;

        //Debug.LogError(" MAIN changeState NEW STATE = " + newState);

        doActCurState();

    }

    private void doActCurState()
    {
        switch (m_eState)
        {
            case E_STATE.NONE:
                {
                    break;
                }

            case E_STATE.INIT:
                {
                    if (null == m_cMapManager)
                    {
                        m_cMapManager = new MapManager();
                        m_cMapManager.Initialize(m_tMapRoot);
                    }


                    changeState(E_STATE.READY);

                    break;
                }

            case E_STATE.READY:
                {
                    changeState(E_STATE.START);
                    break;
                }

            case E_STATE.START:
                {
                    changeState(E_STATE.PLAY);
                    break;
                }

            case E_STATE.PLAY:
                {
                    break;
                }

            case E_STATE.END:
                {
                    break;
                }

            default:
                {
                    break;
                }
        }
    }

    private void reset()
    {
        m_eState = E_STATE.NONE;
    }

    #endregion

}
