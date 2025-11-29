using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SamkdsUI : MonoBehaviour
{
    //[SerializeField, TextArea(3, 5)] private string prompt;

    //public TextMeshProUGUI m_textMeshPro;
    public Button m_btn;
    public TMP_InputField m_inputField;
    
    private SAMKDS m_cSAMGPT;
    private Vector3 m_vChatPos;


    void Start()
    {
        //SAMKDS chatGPT = gameObject.AddComponent<SAMKDS>();
        m_cSAMGPT = GetComponent<SAMKDS>();

        //StartCoroutine(m_cSAMGPT.GetChatGPTResponse(prompt, OnResponseReceived));

        if (null != m_btn)
        {
            m_btn.onClick.RemoveAllListeners();
            m_btn.onClick.AddListener(send);
        }
        
    }


    private void Update()
    {
        if (null == m_cSAMGPT)
            return;

        if (null == m_btn)
            return;

        if (true == m_cSAMGPT.m_bIsResponing)
        {
            if(true == m_btn.gameObject.activeSelf)
                m_btn.gameObject.SetActive(false);
        }
        else
        {
            if (false == m_btn.gameObject.activeSelf)
                m_btn.gameObject.SetActive(true);
        }
    
    }

    void send()
    {
        if (null == m_cSAMGPT) 
            return;

        if (null == m_inputField)
            return;

        //if (true == string.IsNullOrEmpty(m_inputField.text))
        //    return;

        if (null != ChatMessageManager.instance)
            ChatMessageManager.instance.ShowSendChat(m_inputField.text);

        StopAllCoroutines();
        //StartCoroutine(m_cSAMGPT.GetChatGPTResponse(m_inputField.text, OnResponseReceived));
        StartCoroutine(m_cSAMGPT.CommunicateWithChatGPT(m_inputField.text, 2, false, OnResponseReceived));

        m_inputField.text = string.Empty;
    }

    void OnResponseReceived(string response)
    {
        //Debug.Log("ChatGPT Response: " + response);

        //string tmpMessage = string.Empty;

        //string[] message = response.Split(".");

        //for (int i = 0; i < message.Length; i++) 
        //    tmpMessage += message[i] + "\n";

        //m_textMeshPro.text = tmpMessage;

        //m_textMeshPro.text = response;

        //if (null != m_cTfs[1])
        //{
        //    m_cTfs[0].sizeDelta = new Vector2(600, m_textMeshPro.preferredHeight);
        //    m_cTfs[1].sizeDelta = new Vector2(600, m_textMeshPro.preferredHeight);
        //}

        //if (null != m_cChatObj)
        //{
        //    //Vector3 orgPos = m_cChatObj.transform.localPosition;
        //    //m_cChatObj.transform.localPosition = new Vector3(m_vChatPos.x, -m_textMeshPro.preferredHeight, m_vChatPos.z);
        //    m_cChatObj.SetActive(true);
        //}

        //StopAllCoroutines();
        //StartCoroutine(typo(response));

        if(null != ChatMessageManager.instance)
            ChatMessageManager.instance.ShowResponseChat(response);
    }


    IEnumerator typo(string message)
    {
        //yield return new WaitForSeconds(0.1f);

        //for (int i = 0; i < (message.Length + 1); i++)
        //{
        //    m_textMeshPro.text = message.Substring(0, i);
        //    yield return new WaitForSeconds(0.05f);
        //}

        yield return null;
    }


   
}
