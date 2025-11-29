using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Threading.Tasks;
using UnityEngine.Rendering;


public class SAMKDS : MonoBehaviour
{
    private string apiKey = ""; // 발급받은 API 키를 여기에 넣으세요.
    private string apiUrl = "https://api.openai.com/v1/chat/completions";

    private const string ENDPOINT = "https://api.openai.com/v1/embeddings";

    public bool m_bIsResponing;
    public short m_sTotalTokenUsage;
  
    private List<Message> m_strHistories = new List<Message>();
    private Dictionary<int, Script> m_scriptHistoryDic = new Dictionary<int, Script>();
    private Dictionary<int, Script> m_scriptSummarizeHistoryDic = new Dictionary<int, Script>();

    [System.Serializable]
    public class OpenAIResponse
    {
        public Choice[] choices { get; set; }
    }

    [System.Serializable]
    public class Choice
    {
        public Message message { get; set; }
    }

    [System.Serializable]
    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    [System.Serializable]
    public class OpenAIEmbeddingsRespnse
    {
        public Usage usage { get; set; }
    }

    [System.Serializable]
    public class Usage
    {
        //public short prompt_tokens { get; set; }
        public short total_tokens { get; set; }
    }

    [System.Serializable]
    public class Script
    {
        public int Type { get; set; }
        public Message Message { get; set; }
    }


    //public IEnumerator GetChatGPTResponse(string prompt, System.Action<string> callback)
    //{
    //    m_bIsResponing = true;

    //    // Setting OpenAI API Request Data
    //    var jsonData = new
    //    {
    //        //model = "gpt-4o",
    //        model = "gpt-3.5-turbo",
    //        messages = getHistories(prompt, -1),
    //        //messages = new[]
    //        //{
    //        //    new { role = "system", content = "" },
    //        //    new { role = "user", content = prompt }
    //        //},
    //        max_tokens = 1000
    //    };

    //    //yield return checkPrevTokeCount(jsonData.messages);

    //    yield return new WaitForSeconds(0.1f);

    //    string jsonString = JsonConvert.SerializeObject(jsonData);

    //    // HTTP request settings
    //    UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
    //    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
    //    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    //    request.downloadHandler = new DownloadHandlerBuffer();
    //    request.SetRequestHeader("Content-Type", "application/json");
    //    request.SetRequestHeader("Authorization", "Bearer " + apiKey);

    //    yield return request.SendWebRequest();

    //    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    //    {
    //        Debug.LogError("Error: " + request.error);
    //    }
    //    else
    //    {
    //        //m_bIsResponing = false;

    //        var responseText = request.downloadHandler.text;
    //        Debug.Log("Response: " + responseText);
    //        // Parse the JSON response to extract the required parts

    //        var response = JsonConvert.DeserializeObject<OpenAIResponse>(responseText);
    //        makeHistory(response.choices[0].message, -1);

    //        // 0, prompt,
    //        // 1, asking.
    //        // 2, answer.
    //        // 3, summarize...
    //        //yield return summarizeResponse("전체 대화를 요약 해 줘. ", ()=> callback(response.choices[0].message.content));

    //        yield return summarizeResponse("다음 대화를 요약해줘.", () => callback(response.choices[0].message.content));

    //    }


    //    yield return null;
    //}


    //public IEnumerator summarizeResponse(string prompt, System.Action callback)
    //{
    //    m_bIsResponing = true;

    //    // Setting OpenAI API Request Data
    //    var jsonData = new
    //    {
    //        //model = "gpt-4o",
    //        model = "gpt-3.5-turbo",
    //        messages = getHistories(prompt, 0),
    //        temperature = 0.3,
    //        max_tokens = 1000
    //    };

    //    yield return new WaitForSeconds(0.1f);

    //    string jsonString = JsonConvert.SerializeObject(jsonData);

    //    // HTTP request settings
    //    UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
    //    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
    //    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    //    request.downloadHandler = new DownloadHandlerBuffer();
    //    request.SetRequestHeader("Content-Type", "application/json");
    //    request.SetRequestHeader("Authorization", "Bearer " + apiKey);

    //    yield return request.SendWebRequest();

    //    if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    //    {
    //        Debug.LogError("Error: " + request.error);
    //    }
    //    else
    //    {
    //        m_bIsResponing = false;

    //        var responseText = request.downloadHandler.text;
    //        Debug.Log(" summarize Response: " + responseText);
    //        // Parse the JSON response to extract the required parts
    //        var response = JsonConvert.DeserializeObject<OpenAIResponse>(responseText);

    //        !!--//m_strHistoryDic.Clear();

    //        makeHistory(response.choices[0].message, 0);
    //        callback();
    //        //callback(response.choices[0].message.content);
    //    }
    //}


    private IEnumerator checkPrevTokeCount(Message[] message)
    {
        m_sTotalTokenUsage = 0;


        for (int i=0; i< message.Length; i++)
        {
            var requestData = new
            {
                model = "text-embedding-ada-002",  // 모델 이름 (예시로 text-embedding-ada-002 사용)
                input = message[i].content
            };
        
            Debug.LogError(" !!!!! " + i + " , " + message[i].content);
            // JSON 요청 형식
            //var requestData = new
            //{
            //    model = "text-embedding-ada-002",  // 모델 이름 (예시로 text-embedding-ada-002 사용)
            //    input = message
            //};

            string jsonRequestData = JsonConvert.SerializeObject(requestData);          //JsonUtility.ToJson(requestData);
            byte[] jsonToSend = System.Text.Encoding.UTF8.GetBytes(jsonRequestData);    // new System.Text.UTF8Encoding().GetBytes(jsonRequestData);

            // UnityWebRequest 설정
            UnityWebRequest request = new UnityWebRequest(ENDPOINT, "POST");
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);

            // 요청 전송 및 응답 대기
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                // 성공적으로 응답을 받으면 응답에서 토큰 수를 추출
                string responseText = request.downloadHandler.text;
                //JObject jsonResponse = JObject.Parse(responseText);
                //Debug.LogError(" 11111 " + jsonResponse);
                // 응답 데이터에서 토큰 수를 추출 (Embedding 배열의 첫 번째 항목에서 확인)
                //int tokenCount = jsonResponse["data"].Count();
                var tokenCount = JsonConvert.DeserializeObject<OpenAIEmbeddingsRespnse>(responseText);
                Debug.LogError("Token Count: " + tokenCount.usage.total_tokens);
                m_sTotalTokenUsage += tokenCount.usage.total_tokens;

                Debug.LogError("Total Token Count: " + m_sTotalTokenUsage);

            }
            else
            {
                // 오류 처리
                Debug.LogError("Error: " + request.error);
            }

            yield return new WaitForSeconds(0.1f);

        }

        yield return null;

    }

    public IEnumerator CommunicateWithChatGPT(string strDescription, int type, bool isSkipAddScript, System.Action<string> callback)
    {
        m_bIsResponing = true;

        Message[] newMessages = null;

        if (type == 2)
        {
            if (getAllScripts().Length <= 0)
                newMessages = makeMessage(strDescription, type, isSkipAddScript);
            else
                newMessages = makeSummarizeMessage(strDescription, type, isSkipAddScript);
        }
        else
        {
            //0, prompt
            //1, question
            //2, answer
            Debug.LogError(" !!!!!!! getSummarizeScripts " + getSummarizeScripts().Length);

            if (getSummarizeScripts().Length <= 0)
                newMessages = makeMessage(strDescription, type, isSkipAddScript);
            else
                newMessages = makeSummarizeMessage(strDescription, type, isSkipAddScript);
                
        }

        for(int i=0; i< newMessages.Length; i++) 
            Debug.LogError(" !!!!! 33333 " + i + " , " + newMessages[i].role + " , " + newMessages[i].content);

        yield return SendChatGPTAPI(newMessages, type, callback); 


    }


    private IEnumerator SendChatGPTAPI(Message[] messages, int type, System.Action<string> callback)
    {
        m_bIsResponing = true;

        // Setting OpenAI API Request Data
        var jsonData = new
        {
            //model = "gpt-4o",
            model = "gpt-3.5-turbo",
            messages = messages,
            max_tokens = 1000
        };

        //yield return checkPrevTokeCount(jsonData.messages);

        yield return new WaitForSeconds(0.1f);

        string jsonString = JsonConvert.SerializeObject(jsonData);

        // HTTP request settings
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + apiKey);

        yield return request.SendWebRequest();

        m_bIsResponing = false;

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            var responseText = request.downloadHandler.text;
            Debug.Log("Response: " + responseText);
            // Parse the JSON response to extract the required parts

            var response = JsonConvert.DeserializeObject<OpenAIResponse>(responseText);
            makeScript(response.choices[0].message, type);
            //callback(response.choices[0].message.content);

            //Debug.LogError(" !!!!!!! "  + type + " , " + response.choices[0].message.content);

            if (type == 2)
                yield return CommunicateWithChatGPT("이전 대화를 요약해줘.", 1, true, callback);
            else
            {
                m_scriptSummarizeHistoryDic.Clear();

                foreach (KeyValuePair<int, Script> script in m_scriptHistoryDic)
                {
                    Debug.LogError(" !!!!! allScript = " + script.Key + " , " + script.Value.Type + " , " + script.Value.Message.role + " , " + script.Value.Message.content);

                    if (script.Value.Type == 2)
                        continue;

                    //if(false == m_scriptSummarizeHistoryDic.Contains(script))
                    m_scriptSummarizeHistoryDic.Add(script.Key, script.Value);
                }

                foreach (KeyValuePair<int, Script> summarizeScript in m_scriptSummarizeHistoryDic)
                    Debug.LogError(" !!!!! summarizeScript = " + summarizeScript.Key + " , " + summarizeScript.Value.Type + " , " + summarizeScript.Value.Message.role + " , " + summarizeScript.Value.Message.content);
                

                callback(findScript(2, "assistant").content);

            }
        }

        yield return null;
    }

    private Message setPrompt()
    {
        Message prompt = new Message();
        prompt.role = "system";
        prompt.content = "";
        

        return prompt;
    }

    private void onCallbackTranslated(string translatedMessage)
    {
        //Message prompt = new Message();
        //prompt.role = "system";
        //prompt.content = translatedMessage;

        //makeScript(prompt, 0);

        Debug.LogError(" !!!!! onCallbackTranslated = " + translatedMessage);

    }

    private Message[] makeMessage(string strDescription, int type, bool isSkipAddScript)
    {
        if (null == m_scriptHistoryDic)
            return null;

        List<Message> tmpList = new List<Message>();
        if (getAllScripts().Length <= 0)
            makeScript(setPrompt(), 0);
        

        Message message = new Message();
        message.role = "user";
        message.content = strDescription;

        //TranslateManager.instance.Translate(strDescription, onCallbackTranslated);

        if (false == isSkipAddScript)
        {
            //요약해달라는 요청을 script로 들고있을 필요는 없기에..
            makeScript(message, type);
            tmpList.AddRange(getAllScripts());
        }
        else
        {
            //하지만 Message에는 채워서 chatgpt api에다가 보내야 됨.
            tmpList.AddRange(getAllScripts());
            tmpList.Add(message);
        }

        return tmpList.ToArray();

    }



    private Message[] makeSummarizeMessage(string strDescription, int type, bool isSkipAddScript)
    {
        if (null == m_scriptHistoryDic)
            return null;

        List<Message> tmpSummarizeMessage = new List<Message>();
        tmpSummarizeMessage.AddRange(getSummarizeScripts());

        if(type == 1)
        {
            //요약된 script에는 2번 타입의 user의 질문은 당연히 없을꺼라 추가...
            tmpSummarizeMessage.Add(findScript(2, "user"));         //제일 마지막 했던 질문...
            tmpSummarizeMessage.Add(findScript(2, "assistant"));    //제일 마지막 했던 질문의 응답...
        }

        Message message = new Message();
        message.role = "user";
        message.content = strDescription;                       

        if (false == isSkipAddScript)
            makeScript(message, type);
        
        tmpSummarizeMessage.Add(message);

        return tmpSummarizeMessage.ToArray();

    }


    private void makeScript(Message message, int type)
    {
        if (null == m_scriptHistoryDic)
            return;

        Script script = new Script();
        script.Type = type;
        script.Message = message;

        //Task<string> task = TranslateManager.instance.TranslateAsync(message.content);
        ////중간에 번역기로 인터셉트...
        //if(null != task && string.Empty != task.Result) 
        //    message.content = task.Result;

        //if(true == task.IsCompletedSuccessfully)
        //    script.Message = message;

        //Debug.LogError(" !!!!! script message content = " + script.Message.content);

        //switch (type) 
        //{
        //    case 0:
        //        {
        //            //prompt
        //            break;
        //        }

        //    case 1:
        //        {
        //            //summarize
        //            break;
        //        }

        //    case 2:
        //        {
        //            // 사용자 질문
        //            // chatGPT 응답.
        //            break;
        //        }

        //    default:
        //        {
        //            break;
        //        }
        //}

        //Debug.LogError(" !!!!! Make Script = " + m_scriptHistoryDic.Count + " , " + script.Type + " , " + script.Message.role + " , " + script.Message.content );

        m_scriptHistoryDic.Add(m_scriptHistoryDic.Count, script);

    }

    private Message[] getSummarizeScripts()
    {
        if (null == m_scriptSummarizeHistoryDic)
            return null;

        List<Message> scriptList = new List<Message>();

        foreach (KeyValuePair<int, Script> script in m_scriptSummarizeHistoryDic)
            scriptList.Add(script.Value.Message);
        

        return scriptList.ToArray();
    }

    private Message[] getAllScripts()
    {
        if (null == m_scriptHistoryDic)
            return null;

        List<Message> scriptList = new List<Message>();

        foreach(KeyValuePair<int, Script> script in m_scriptHistoryDic)
            scriptList.Add(script.Value.Message);

        //Debug.LogError(" !!!!! scriptList cnt = " + scriptList.Count);

        //for (int i = 0; i < scriptList.Count; i++)
        //{
        //    Debug.LogError(" !!!!! scriptList = " + i + " , " + scriptList[i].role + " , " + scriptList[i].content);
        //}

        return scriptList.ToArray();
    }


    private Message findScript(int type, string targetRole)
    {
        if (null == m_scriptHistoryDic)
            return null;

        List<Script> scriptList = new List<Script>();

        foreach (KeyValuePair<int, Script> script in m_scriptHistoryDic)
        {
            
            if (script.Value.Type != type)
                continue;

            if (script.Value.Message.role != targetRole)
                continue;

            //Debug.LogError(" !!!!! findscript = " + script.Key + " , " + script.Value.Type + " , " + script.Value.Message.role + " , " + script.Value.Message.content);

            scriptList.Add(script.Value);

        }

        Message message = new Message();
        for(int i = (scriptList.Count -1); i >= 0; i--)
        {
            //Debug.LogError(" !!!!!!!!! Message = " + i + " , " + scriptList[i].Type + " , " + scriptList[i].Message.role + " , " + scriptList[i].Message.content);

            if (scriptList[i].Message.role == targetRole)
            {
                message = scriptList[i].Message;
                break;
            }
        }


        return message;
    }

}