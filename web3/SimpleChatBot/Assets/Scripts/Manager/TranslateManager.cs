using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.UIElements;


// 응답 JSON을 처리할 구조체
[System.Serializable]
public class DeepLResponse
{
    public Translation[] translations;
}

[System.Serializable]
public class Translation
{
    public string detected_source_language;
    public string text;
}

public class TranslateManager : MonoBehaviour
{
    public static TranslateManager instance;

    private string apiKey = "3138fa4a-88ac-478a-b048-340cbd8fc73b:fx";
    private string apiUrl = "https://api-free.deepl.com/v2/translate";
    private string targetLanguage = "EN"; // 번역할 언어 코드

    void Awake()
    {
        instance = this;

        Init();

    }

    public void Init()
    {
        //Translate("Hello, world");

        //Task<string> taskM = TranslateAsync("안녕?");
        //Debug.LogError("!!!!! " + taskM.Result);

        Translate("안녕?", onCompleteTranslateDone);

        reset();
    }

    private void onCompleteTranslateDone(string translatedMessage)
    {
        Debug.LogError(translatedMessage);
    }

    public void Translate(string text, System.Action<string> onCallbackTranslateDone)
    {
        StopAllCoroutines();
        StartCoroutine(TranslateText(text, targetLanguage, onCallbackTranslateDone));
    }

    public async Task<string> TranslateAsync(string text)
    {
        //string translatedText = await TranslateTextAsync(text, "EN");

        //return translatedText;

        return await TranslateTextAsync(text, "EN");
    }

    // 비동기적으로 텍스트 번역 요청
    private async Task<string> TranslateTextAsync(string text, string targetLang)
    {
        // 요청의 POST 데이터 형식 설정
        WWWForm form = new WWWForm();
        form.AddField("auth_key", apiKey); // 인증 키
        form.AddField("text", text); // 번역할 텍스트
        form.AddField("target_lang", targetLang); // 타겟 언어 코드

        // UnityWebRequest를 비동기적으로 처리
        using (UnityWebRequest request = UnityWebRequest.Post(apiUrl, form))
        {
            // 요청을 비동기적으로 보내고, 응답을 기다립니다.
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield(); // 비동기 대기
            }

            // 응답 완료 후 처리
            if (request.result == UnityWebRequest.Result.Success)
            {
                // 성공적인 응답이 왔을 때
                string responseText = request.downloadHandler.text;
                Debug.Log("Response: " + responseText);

                //var response = JsonConvert.DeserializeObject<DeepLResponse>(responseText);

                //if (response.translations.Length > 0)
                //{
                //    string translatedText = response.translations[0].text;
                //    Debug.Log("Translated text: " + translatedText);
                    
                //    return translatedText;
                //}

                // 응답 처리 (JSON 파싱 등)
                return ParseTranslationResponse(responseText);
            }
            else
            {
                // 오류 처리
                Debug.LogError(" Exception Error: " + request.error);
                return null;
            }
        }

    }


    IEnumerator TranslateText(string text, string targetLang, System.Action<string> onCallbackTranslateDone)
    {
        // 요청의 POST 데이터 형식 설정
        WWWForm form = new WWWForm();
        form.AddField("auth_key", apiKey); // 인증 키
        form.AddField("text", text); // 번역할 텍스트
        form.AddField("target_lang", targetLang); // 타겟 언어 코드

        yield return new WaitForSeconds(0.1f);

        // POST 요청 보내기
        UnityWebRequest request = UnityWebRequest.Post(apiUrl, form);

        // 요청이 완료될 때까지 기다림
        yield return request.SendWebRequest();

        // 요청이 완료되면 응답 처리
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 성공적인 응답이 왔을 때
            string responseText = request.downloadHandler.text;
            Debug.Log("Response: " + responseText);
            var response = JsonConvert.DeserializeObject<DeepLResponse>(responseText);

            if (response.translations.Length > 0)
            {
                string translatedText = response.translations[0].text;
                Debug.Log("Translated text: " + translatedText);
                onCallbackTranslateDone(translatedText);
            }

            // 응답 처리 (JSON 파싱 등)
            //ParseTranslationResponse(responseText);
        }
        else
        {
            // 오류 처리
            Debug.LogError("Error: " + request.error);
        }

    }

    

    // DeepL API 응답 처리
    //private void ParseTranslationResponse(string responseText)
    //{
    //    // 응답은 JSON 형태입니다. 예: {"translations":[{"detected_source_language":"EN","text":"Hallo, wie geht's?"}]}
    //    //var json = JsonUtility.FromJson<DeepLResponse>(response);
    //    var response = JsonConvert.DeserializeObject<DeepLResponse>(responseText);

    //    if (response.translations.Length > 0)
    //    {
    //        string translatedText = response.translations[0].text;
    //        Debug.Log("Translated text: " + translatedText);
    //    }
    //}

    // DeepL API 응답 처리
    private string ParseTranslationResponse(string responseText)
    {
        // 응답은 JSON 형태입니다. 예: {"translations":[{"detected_source_language":"EN","text":"Hallo, wie geht's?"}]}
        //var json = JsonUtility.FromJson<DeepLResponse>(response);
        var response = JsonConvert.DeserializeObject<DeepLResponse>(responseText);

        if (response.translations.Length > 0)
        {
            return response.translations[0].text;
        }
        else
        {
            return "Error: No translation found";
        }

        return string.Empty;
    }

    private void reset()
    {

    }


}
