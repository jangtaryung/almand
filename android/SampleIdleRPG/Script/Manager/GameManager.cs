
#define CLOSE_IOS
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;

public class GameManager : UnitySingleton<GameManager>
{
    public IniFile GameSetting;
    public IniFile Language;

    public LoginMsg loginMsg;

    static List<string> mLines;
    static bool openLog;

    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    private Action<string, string> _loginCallback = null;

    private Action<bool> _initback;

    private GUIStyle bb;
    private WWW ww;
    private WWW wwpay;
    private string lprice;
    private string litemName;
    private string lgame_area;
    private string lrole_name;
    private string lrole_id;
    private string llevel;
    private string linfo_data;
    private string lorderId;
    private string lgameSign;
    private pay_info lpinfo;


    public override void Awake()
    {
        base.Awake();
        //Application.persistentDataPath Unity
        
        Application.logMessageReceived+=HandleLog;
        mLines = new List<string>();

        openLog = false;
        bb = new GUIStyle();
        bb.normal.background = null;  
        bb.normal.textColor = new Color(1, 0, 0);  
        bb.fontSize = 24;
        bb.wordWrap = true;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            Log(logString);
            Log(stackTrace);
        }
    }

    public void Log(params object[] objs)
    {
        string text = "";
        for (int i = 0; i < objs.Length; ++i)
        {
            if (i == 0)
            {
                text += objs[i].ToString();
            }
            else
            {
                text += ", " + objs[i];
            }
        }
        if (Application.isPlaying)
        {
            showLog(text);
        }
    }

    private void showLog(string text)
    {
        if (mLines.Count >= 12)
        {
            mLines.RemoveAt(0);
        }
        mLines.Add(text);
    }

    void OnGUI()
    {
        if (openLog == false)
            return;

//        GUI.color = Color.red;

        int len = mLines.Count;
        
        if (openLog && len > 0)
        {
            if (GUILayout.Button("Log", GUILayout.Width(100), GUILayout.Height(30)))
            {
                openLog = false;
            }
        }
        for (int i = 0, imax = len; i < imax; ++i)
        {
            GUILayout.Label("-----------------------------------");
            GUILayout.Label(mLines[i], bb);
        }
    }

    public bool isInit;
 
    public void initSDK(Action<bool> initback)
    {
        _initback = initback;
        if (isInit)
            return;
        //#if !UNITY_EDITOR
        //        HoolaiSDK.instance.init(gameObject.name, "initCallback", "loginCallback", "payCallback");
        //#else
        //        _initback(true);
        //#endif
        _initback(true);
    }

    public void login(string username = "", Action<string, string> callback = null)
    {
        if (GlobalTools.isBanshu)
        {
            return;
        }
//#if !UNITY_EDITOR
//        if (!isInit)
//        {
//            initSDK(_initback);
//            Invoke("checkInitSdk", 3);
//        }
//        _loginCallback = callback;
//        HoolaiSDK.instance.login("login");
//#endif
    }

    //public void checkInitSdk()
    //{
    //    if (isInit)
    //    {
    //        HoolaiSDK.instance.login("login");
    //        CancelInvoke();
    //    }
    //    else
    //    {
    //        initSDK(_initback);
    //        Invoke("checkInitSdk", 3);
    //    }
    //}

    public void logout()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect();

#if UNITY_EDITOR
        PlayerPrefsUtility.DelString("lastlogin");
        PlayerPrefs.Save();
        SceneManager.LoadScene(GlobalTools.LoginStage);
        return;
#else
        GoogleSignInManager.Instance.SignOutFromGoogle(x => {
            //Debug.Log(x);
            PlayerPrefsUtility.DelString("lastlogin");
            PlayerPrefs.Save();
            Restart();
        });
#endif


    }

    public void Restart()
    {
        //PlayerPrefsUtility.DelString("lastlogin");
        //Debug.Log("마지막 로그인 정보 저장정보 확인  " + PlayerPrefsUtility.GetString("lastlogin"));
        using (var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            const int kIntent_FLAG_ACTIVITY_CLEAR_TASK = 0x00008000;
            const int kIntent_FLAG_ACTIVITY_NEW_TASK = 0x10000000;

            var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            var pm = currentActivity.Call<AndroidJavaObject>("getPackageManager");
            var intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", Application.identifier);

            intent.Call<AndroidJavaObject>("setFlags", kIntent_FLAG_ACTIVITY_NEW_TASK | kIntent_FLAG_ACTIVITY_CLEAR_TASK);
            currentActivity.Call("startActivity", intent);
            currentActivity.Call("finish");
            var process = new AndroidJavaClass("android.os.Process");
            int pid = process.CallStatic<int>("myPid");
            process.CallStatic("killProcess", pid);
        }
        //using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        //{
        //    AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        //    AndroidJavaObject pm = currentActivity.Call<AndroidJavaObject>("getPackageManager");
        //    AndroidJavaObject intent = pm.Call<AndroidJavaObject>("getLaunchIntentForPackage", Application.identifier);
        //    intent.Call<AndroidJavaObject>("setFlags", 0x20000000);//Intent.FLAG_ACTIVITY_SINGLE_TOP

        //    AndroidJavaClass pendingIntent = new AndroidJavaClass("android.app.PendingIntent");
        //    AndroidJavaObject contentIntent = pendingIntent.CallStatic<AndroidJavaObject>("getActivity", currentActivity, 0, intent, 0x8000000); //PendingIntent.FLAG_UPDATE_CURRENT = 134217728 [0x8000000]
        //    AndroidJavaObject alarmManager = currentActivity.Call<AndroidJavaObject>("getSystemService", "alarm");
        //    AndroidJavaClass system = new AndroidJavaClass("java.lang.System");
        //    long currentTime = system.CallStatic<long>("currentTimeMillis");
        //    alarmManager.Call("set", 1, currentTime + 1000, contentIntent); // android.app.AlarmManager.RTC = 1 [0x1]

        //    Debug.LogError("alarm_manager set time " + currentTime + 1000);
        //    currentActivity.Call("finish");

        //    AndroidJavaClass process = new AndroidJavaClass("android.os.Process");
        //    int pid = process.CallStatic<int>("myPid");
        //    process.CallStatic("killProcess", pid);
        //}
    }

    public void pay(/*int amount, string itemName, */pay_info pinfo /*= null*/)
    {
        //string price = Convert.ToString(amount);
        //string game_area = Convert.ToString(Data.role.server_id);
        //string role_name = Data.role.role_name;
        //string role_id = Data.Manager.loginMsg.userName;
        //string level = Convert.ToString(Data.role.level);
        //string info_data = Convert.ToString(pinfo.id);

        //#if UNITY_IOS
        //       // payww(price, itemName, game_area, role_name, role_id, level, info_data, "", "", pinfo);
        //        HoolaiSDK.instance.pay(price, itemName, game_area, role_name, role_id, level, info_data, "", "", pinfo);

        //#elif UNITY_ANDROID
        //        HoolaiSDK.instance.pay(price, itemName, game_area, role_name, role_id, level, info_data,"","", pinfo);
        ////      HoolaiSDK.instance.pay(1, itemName, callbackInfo, customParams);
        //        if (pinfo != null)
        //        {
        //            Game_SetPaymentStart(pinfo.id + "", "rmb", "gold", amount/100f, pinfo.gold_now, pinfo.name, 1);
        //        }
        //#endif

        //GameBaseSDK.Instance.RequestPurchase(pinfo.aos_id);
        Debug.LogError(" !!!!! pay = " + pinfo.aos_id);
#if !UNITY_EDITOR
        IAPManager.Instance.Purchase(pinfo.aos_id);
#endif


    }
    
    public void payww (string amount, string itemName, string game_area, string role_name, string role_id, string level, string info_data, string orderId, string gameSign, pay_info pinfo = null)
    {
 
        lprice = amount; litemName = itemName; lgame_area = game_area; lrole_name = role_name; lrole_id = role_id; llevel = level; linfo_data = info_data;  lpinfo = pinfo;
        HoolaiSDK.instance.pay(lprice, litemName, lgame_area, lrole_name, lrole_id, llevel, linfo_data, "", "", lpinfo);
                
    }
    private void checkpay()
    {
        if (wwpay.isDone)
        {
            CancelInvoke("checkpay");
            if (wwpay.error != null)
            {

            }
            else
            {
                JsonData recMsg = JsonMapper.ToObject(wwpay.text);
                wwpay.Dispose();
                 lorderId = recMsg["orderId"].ToString();
                 lgameSign = recMsg["gameSign"].ToString();

                HoolaiSDK.instance.pay(lprice, litemName, lgame_area, lrole_name, lrole_id, llevel, linfo_data, lorderId, lgameSign, lpinfo);
            }
        }
    }

    public void exit()
    {
#if !UNITY_EDITOR
        //HoolaiSDK.instance.exit(gameObject.name, "exitCallback");
        JsonData msg = new JsonData();
        msg["resultCode"] = 1;
        exitCallback(msg.ToJson());
#endif
    }

    public void exit(bool yes )
    {
        Debug.Log("exit 11111");
        if(!yes)
        {

        }
        else
        {
            // 앱 종료여
#if !UNITY_EDITOR
        //HoolaiSDK.instance.exit(gameObject.name, "exitCallback");
        JsonData msg = new JsonData();
        msg["resultCode"] = 1;
        exitCallback(msg.ToJson());
#endif
        }
    }

    void initCallback(string result)
    {
//        showLog(HoolaiSDK.LOG_TAG + "initCallback result:" + result);
        JsonData jsonObj = JsonMapper.ToObject(result);
        int resultCode = (int)jsonObj["resultCode"];

        HoolaiSDK.instance.SetChannel((string)jsonObj["channel"] , (string)jsonObj["url"]);
        string resultMsg;
        if (HoolaiSDK.Type_Init_Success == resultCode)
        {
//            Debuger.Log(HoolaiSDK.LOG_TAG + "init success");
            resultMsg = HoolaiSDK.LOG_TAG + "init success";
            isInit = true;
        }
        else
        {
//            Debuger.Log(HoolaiSDK.LOG_TAG + "init fail");
            resultMsg = HoolaiSDK.LOG_TAG + "init fail";
            isInit = false;
        }
        _initback(isInit);
        
        Debuger.Log(resultMsg);
//        showLog(resultMsg);
    }

    public void loginCallbackios(string result)
    {
    	Debug.Log("logcalbackk"+result);
        JsonData jsonObj = JsonMapper.ToObject(result);
        string token = (string)jsonObj["data"];
        string url = "token/" + token+ "/type/" + 2;

        Debug.Log("url----------"+url);
         ww = new WWW(url);
        InvokeRepeating("checklogin", 0.1f, 0.1f);
    }

    public void SendLogin()
    {
        loginMsg.channel = HoolaiSDK.instance.GetChannel();
  
#if APK_DEBUG
        if( loginMsg!=null)
        {
            loginMsg.useruid = Manager.loginMsg.useruid;//HoolaiSDK.instance.GetAuthinfoGoogle().UserId;
            loginMsg.idtoken = Manager.loginMsg.idtoken;//HoolaiSDK.instance.GetAuthinfoGoogle().IdToken
        }
        else
        {
            Debug.Log("loginMsg is null ");
            return;
        }
#else
        if(HoolaiSDK.instance.GetAuthinfoGoogle() != null && HoolaiSDK.instance.GetCurrentUserType().Equals("google"))
        {
            loginMsg.useruid = HoolaiSDK.instance.GetAuthinfoGoogle().UserId;
            loginMsg.idtoken = HoolaiSDK.instance.GetAuthinfoGoogle().IdToken;
        }
        else if(HoolaiSDK.instance.GetCurrentUserType().Equals("guest"))
        {
            Debug.Log("게스트는 이미 세팅된 로그인 데이터를 보낸다.");
        }

#endif
        //로그인 정보 저장
        Debug.Log("============ Mapping loaing set ");
        PlayerPrefsUtility.SetString("lastlogin", HoolaiSDK.instance.GetCurrentUserType());

        Socket.Send(CMD.Role_loginsg, loginMsg);


    }

    private string getProp(JsonData jsonObj,string pName)
    {
        IDictionary dic = jsonObj;
        if (dic.Contains(pName))
        {
            return jsonObj[pName]+"";
        }
        return "";
    }

    void payCallback(string result)
    {
        Debuger.Log(HoolaiSDK.LOG_TAG + "payCallback result:" + result);

        JsonData jsonObj = JsonMapper.ToObject(result);
        int resultCode = (int)jsonObj["resultCode"];
        if (HoolaiSDK.Type_Pay_Success == resultCode)
        {
            Debuger.Log(HoolaiSDK.LOG_TAG + "pay success");
        }
        else
        {
            Debuger.Log(HoolaiSDK.LOG_TAG + "pay fail");
        }
    }

    void exitCallback(string result)
    {
        Debug.Log(HoolaiSDK.LOG_TAG + "exitCallback result:" + result);
        Debug.Log("logcalbackk"+result);

        JsonData jsonObj = JsonMapper.ToObject(result);
        int resultCode = (int)jsonObj["resultCode"];
        if (HoolaiSDK.Type_Exit_Channel == resultCode)
        {
            Debug.Log(HoolaiSDK.LOG_TAG + "onChannelExit");
#if UNITY_ANDROID
            HoolaiSDK.instance.releaseResource();
#endif
            HoolaiSDK.instance.quitGame();
        }
        else
        {
            Debug.Log(HoolaiSDK.LOG_TAG + "onGameExit" + Dialog);

            Dialog.show(Language.GetString("GameManager.exitCallback.title"), Language.GetString("GameManager.exitCallback.descTxt"), DialogBack);
        }
    }

    private void DialogBack(bool yes)
    {
        if (yes)
        {
#if UNITY_ANDROID
            HoolaiSDK.instance.releaseResource();
#endif
            HoolaiSDK.instance.quitGame();
        }
    }

    void getServersCallback(string result)
    {
        Debuger.Log(HoolaiSDK.LOG_TAG + "getServersCallback result:" + result);

        JsonData jsonObj = JsonMapper.ToObject(result);
        int resultCode = (int)jsonObj["resultCode"];
        if (HoolaiSDK.Type_GetServers_Success == resultCode)
        {
            Debuger.Log(HoolaiSDK.LOG_TAG + "onGetServers_Success");

            //서버 목록을 성공적으로 가져왔습니다.
            JsonData serverInfos = jsonObj["serverInfos"];
            JsonData allServers = serverInfos["serverList"];
            JsonData userServers = serverInfos["userServerList"];

            for (int i = 0; i < allServers.Count; i++)
            {
                JsonData s = allServers[i];
                Debuger.Log(HoolaiSDK.LOG_TAG + "serverId:" + s["serverId"] + " productId:" + s["productId"] +
                       " serverName:" + s["serverName"]);
            }
            for (int i = 0; i < userServers.Count; i++)
            {
                JsonData s = userServers[i];
                Debuger.Log(HoolaiSDK.LOG_TAG + "serverId:" + s["serverId"] + " productId:" + s["productId"] +
                       " serverName:" + s["serverName"]);
            }
        }
        else
        {
            Debuger.Log(HoolaiSDK.LOG_TAG + "onGetServers_Fail");
            string desc = (string)jsonObj["desc"];
            Debuger.Log(HoolaiSDK.LOG_TAG + "desc:" + desc);
        }
    }

    void selectServerCallback(string result)
    {
        JsonData jsonObj = JsonMapper.ToObject(result);
        int resultCode = (int)jsonObj["resultCode"];
        if (HoolaiSDK.Type_SelectServer_Success == resultCode)
        {
            Debuger.Log(HoolaiSDK.LOG_TAG + "onSelectServer_Success");
        }
        else
        {
            Debuger.Log(HoolaiSDK.LOG_TAG + "onSelectServer_Fail");
            string desc = (string)jsonObj["desc"];
            Debuger.Log(HoolaiSDK.LOG_TAG + "desc:" + desc);
        }
    }

    public void setExtData(string userExtData)
    {
#if !UNITY_EDITOR
        HoolaiSDK.instance.setExtData(userExtData);
#endif
    }
    public enum Gender
    {
        reyun_m = 0,
        reyun_f = 1,
        reyun_o = 2,
    }

    public enum QuestStatus
    {
        reyun_start = 0,
        reyun_done = 1,
        reyun_fail = 2,
    }

    public struct strutDict
    {
        public string key;
        public string value;
    }


#if UNITY_ANDROID
    public static AndroidJavaObject getApplicationContext()
    {

        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                return jo.Call<AndroidJavaObject>("getApplicationContext");
            }
        }

        return null;
    }
#endif

    public void Game_Init(string appId, string channelId)
    {
#if UNITY_EDITOR
        return;
#endif
//#if UNITY_IOS	
//		initWithAppId (appId, channelId);
//#endif

#if UNITY_ANDROID
        /*using (AndroidJavaClass reyun = new AndroidJavaClass("com.reyun.sdk.ReYunGame"))
        {
            reyun.CallStatic("initWithKeyAndChannelId", getApplicationContext(), appId, channelId);
        }*/
#endif
    }

    public void Game_Register(string account, Gender gender, string age, string serverId, string accountType, string rolename)
    {
#if UNITY_ANDROID
         string strGender = System.Enum.GetName(typeof(Gender), gender);
        //HoolaiSDK.instance.androidContext().Call("registerReport",   account,   strGender,   age,   serverId,   accountType,   rolename );
        Debug.Log("Game_Register_플레이어 서버 등록");
#endif

    }

    public void Game_Login(string account, Gender genders, string age, string serverId, int level, string rolename)
    {
        Debug.Log("Game_Login=================");
        return;
//#if UNITY_EDITOR
//        return;
//#endif


        //    #if UNITY_ANDROID
        //    Debug.Log("Game_Login=================");
        //        /*using (AndroidJavaClass reyun = new AndroidJavaClass("com.reyun.sdk.ReYunGame"))
        //        {
        //            string strGender = System.Enum.GetName(typeof(Gender), genders);
        //            reyun.CallStatic("setNLoginWithAccountID", account, level, serverId, rolename, strGender, age);
        //        }*/
        //           string strGender = System.Enum.GetName(typeof(Gender), genders);
        //        HoolaiSDK.instance.androidContext().Call("loginReport",   account,   strGender,   age,   serverId,   level,   rolename);
        //    #endif
    }

    public void Game_SetPaymentStart(string transactionId, string paymentType, string currencyType, float currencyAmount, float virtualCoinAmount, string iapName, int iapAmount)
    {
        return;
    }

    public void Game_SetPayment(string transactionId, string paymentType, string currencyType, float currencyAmount, float virtualCoinAmount, string iapName, int iapAmount, int level)
    {
        return;
    }


    public void Game_SetEconomy(string itemName, int itemAmount, float itemTotalPrice)
    {
        return;
    }


    public void Game_Quest(string questId, QuestStatus questStatu, string questType)
    {
        return;
    }

    public void Game_SetEvent(string eventName, Dictionary<string, string> dict)
    {
//#if UNITY_IOS
//		if (dict == null){
//			strutDict[] dicArr1 = new strutDict[0];
////			setNewEvent(eventName,"{}");
			
//		}else{
//			int nLength = dict.Count;
//			strutDict[] dicArr = new strutDict[nLength];
//			List<string> dicKey = new List<string> (dict.Keys);
//			List<string> dicValue = new List<string> (dict.Values);
//			string json = "{";
//			for (int i = 0; i < nLength; i++) {
//				string subKeyValue = "\""+dicKey [i]+"\""+":"+"\""+dicValue[i]+"\"";
//				json += subKeyValue;
//				if (i != nLength-1) {
//					json+=",";
//				}
//			}
//			json +="}"; 
////			setNewEvent (eventName,json);
//		}
		
//#endif
    }


    public string Game_getDeviceId()
    {
        return "unknown";
    }

    public void Game_setPrintLog(bool print)
    {
    }

#if UNITY_EDITOR
    public int GetBatteryLevel()
    {
        return 100;
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern float GetBatteryLevel();

    public float getBatteryLevel()
    {
        return GetBatteryLevel();
    }
#elif UNITY_ANDROID
    public int GetBatteryLevel()
    {
        try
        {
#if UNITY_EDITOR
            return 100;
#endif
            string CapacityString = System.IO.File.ReadAllText("/sys/class/power_supply/battery/capacity");
            return int.Parse(CapacityString);
        }
        catch (Exception e)
        {
            Debug.Log("Failed to read battery power; " + e.Message);
        }
        return 1;
    }
#endif




}
