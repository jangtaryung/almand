using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMessageManager : MonoBehaviour
{
    public static ChatMessageManager instance;

    public List<ChatMessage> m_listChats;

    public Button m_btnClose;

    private void Awake()
    {
        instance = this;

        Initialize();
    }

    public void Initialize()
    {

        if (null != m_btnClose)
        {
            m_btnClose.onClick.RemoveAllListeners();
            m_btnClose.onClick.AddListener(OnClickClose);
        }

        if (null != m_listChats) 
        {
            for(int i=0; i< m_listChats.Count; i++) 
                m_listChats[i].Initialize(i);
        }
    }

    public void ShowSendChat(string sendMessage)
    {
        if (null != m_listChats)
            m_listChats[1].SetShowActive(string.Empty, false);

        if (null != m_listChats)
            m_listChats[0].SetShowActive(sendMessage, true);

    }

    public void ShowResponseChat(string responseMessage)
    {
        if (null != m_listChats)
            m_listChats[0].SetShowActive(string.Empty, false);

        if (null != m_listChats)
            m_listChats[1].SetShowActive(responseMessage, true);
    }

    public void OnClickClose()
    {
        if (null != m_listChats)
            m_listChats[1].SetShowActive(string.Empty, false);

    }

}
