using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ChatMessage : MonoBehaviour
{
    public RectTransform m_rtBG;
    public RectTransform m_rtText;
    public TextMeshProUGUI m_text;
    

    private bool m_isShowActive = false;
    private int m_iType;

    private Vector2 m_vOrgSize;
    private float m_fMaxDelayTime = 10.0f;
    private float m_fCurrShowTime;

    public void Initialize(int type)
    {
        m_iType = type;
        m_vOrgSize = m_rtText.sizeDelta;

        reset();

    }

    //public void Update()
    //{
    //    if (false == m_isShowActive)
    //        return;

    //    if (m_iType == 1)
    //        return;

    //    m_fCurrShowTime += Time.deltaTime * 1.0f;

    //    if (m_fMaxDelayTime <= m_fCurrShowTime)
    //    {
    //        reset();
    //    }
    //}


    public void SetShowActive(string message, bool isActive)
    {
        adjustChatRt(message);
        setActive(isActive);

    }

    public void SetShowResponseActive(string message, bool isActive)
    {
        adjustChatRt(message);
        setActive(isActive);

    }

    private void adjustChatRt(string message)
    {
        m_text.text = message;

        //Debug.LogError(" !!!!! " + m_iType + " , " + m_vOrgSize.x + " , " + m_vOrgSize.y + " , " + m_text.preferredWidth + " , " + m_text.preferredHeight);

        float textWidth = m_vOrgSize.x;
        float textHeight = m_vOrgSize.y;

        //if(m_iType == 1)
        //{
        //    //if (m_text.preferredWidth < m_vOrgSize.x)
        //        //textWidth = m_text.preferredWidth;

        //    if (m_text.preferredHeight < m_vOrgSize.y)
        //        textWidth = m_text.preferredWidth;


        //    textHeight = m_text.preferredHeight;

        //}
        //else
        //{
        //    if (m_text.preferredWidth < m_vOrgSize.x)
        //        textWidth = m_text.preferredWidth;

        //    //textHeight = m_text.preferredHeight;

        //}

        
        m_rtBG.sizeDelta = new Vector2(textWidth, textHeight);
        m_rtText.sizeDelta = new Vector2(textWidth, textHeight);

    }


    private void setActive(bool isActive) 
    {
        if (this.gameObject.activeSelf == isActive)
            return;

        m_isShowActive = isActive;

        this.gameObject.SetActive(isActive);

    }

    private void reset()
    {
        m_fCurrShowTime = 0.0f;
        m_fMaxDelayTime = 5.0f;

        setActive(false);
    }
}
