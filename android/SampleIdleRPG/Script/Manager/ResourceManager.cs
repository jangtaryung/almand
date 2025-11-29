using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using ICSharpCode.SharpZipLib.GZip;
using LitJson;
using Debug = UnityEngine.Debug;
using System.Text.RegularExpressions;
using Unity.VisualScripting;


public class ResourceManager : UnitySingleton<ResourceManager>
{
    private TextAsset cache;

    private const string CacheName = "Cache";

    private const string Backgrounds = "Backgrounds";
    private const string BattleBg = "BattleBg";
    private const string BattleEffect = "BattleEffect";
    private const string LeagueEventBg = "leagueWar";
    private const string LeagueBg = "leagueBg";
    private const string MysteryBg = "MysteryBg";
    private const string UIbg = "UIbg";
    private const string worldWar = "worldWar";
    private const string Login = "Login";
//    private const string Role = "Role";
    private const string Avatar = "Avatar";
    private const string HeadIcon = "HeadIcon";
    private const string SkillIcon = "SkillIcon";
    private const string ItemIcon = "ItemIcon";
    private const string QuestIcon = "QuestIcon";
    private const string GemIcon = "GemIcon";

    private const string extension = ".assetBundle";
    private bool debugMode = true;
    public bool compress = true;
    // The class constructor is called when the class instance is created

    public CacheData cacheData { get; set; }

    private WWW w;
    public bool hasCache;
    private UILabel showTxt;
    private Action<int> _callback;
    private byte[] cacheBytes;

    public void syncResource(UILabel txt, Action<int> callback)
    {
        _callback = callback;
        showTxt = txt;
        debugMode = GlobalTools.LoadLocal;
//        Debuger.Log("debugMode->" + debugMode);
        if (debugMode)
        {
            initIcons();
            //            getCacheUrl();x7.yongzhe.jiuwan.com
            if(HoolaiSDK.instance != null)
                GlobalTools.GameUrl = HoolaiSDK.instance.GetChannelUrl();//"sl.youyi.yongzhe.jiuwan.com";

            _callback(100);
            return;
        }
//        string[] sceneList = { CacheName, Backgrounds, Face, Role, Avatar };
        ArrayList sceneList = new ArrayList {CacheName, Backgrounds, Avatar};

        float difTime = Time.realtimeSinceStartup;

        foreach (string sceneBundleName in sceneList)
        {
            string url = sceneBundleName + extension;
            DownloadManager.Instance.StartDownload(url);

            if (!DownloadManager.Instance.IsUrlRequested(url))
                Debuger.LogError(sceneBundleName + ":IsUrlRequested() ERORR" + url);
            else
                Debuger.Log(sceneBundleName + "TEST:IsUrlRequested() test finished." + url);
        }

        bool sceneBundleSucceed;
        do
        {
            sceneBundleSucceed = true;
            foreach (string sceneBundleName in sceneList)
            {
                if (DownloadManager.Instance.GetWWW(sceneBundleName + extension) == null)
                    sceneBundleSucceed = false;
            }

            List<string> sceneBundles = new List<string>();
            foreach (string sceneBundleName in sceneList)
            {
                string url = sceneBundleName + extension;
                sceneBundles.Add(url);
            }
            float progressOfScenes = DownloadManager.Instance.ProgressOfBundles(sceneBundles.ToArray());
            Debuger.Log("Loading Progress " + progressOfScenes);
            return;
        } while (!sceneBundleSucceed);

        difTime = Time.realtimeSinceStartup - difTime;

        cache = DownloadManager.Instance.GetWWW(CacheName + extension).assetBundle.LoadAsset("Cache") as TextAsset;

        difTime = Time.realtimeSinceStartup;
        initIcons();
        initCache();
        difTime = Time.realtimeSinceStartup - difTime;
//        Debuger.Log(cache);
    }

    public void syncCache(UILabel txt, Action<int> callback)
    {
        _callback = callback;
        showTxt = txt;
//        getVersion();
        checkCacheVersion();
    }

    private void checkCacheUrl()
    {
        if (w.isDone)
        {
            if (w.error != null)
            {
                showTxt.text = Manager.Language.GetString("ResourceManager.checkCacheUrl.1");
            }
            else
            {
                showTxt.text = Manager.Language.GetString("ResourceManager.checkCacheUrl.2");
                GlobalTools.GameUrl = w.text;
                
                _callback(100);
//                getVersion();
            }

            CancelInvoke("checkCacheUrl");
        }
    }

    private void getCacheUrl()
    {
        WWWForm form = new WWWForm();
        form.AddField("cache", "getCacheServer");
        w = new WWW("getCacheServer", form);
        showTxt.text = Manager.Language.GetString("ResourceManager.getCacheUrl");
        InvokeRepeating("checkCacheUrl", 0.01f, 0.01f);

    }

    private void checkVersion()
    {
        if (w.isDone)
        {
            if (w.error != null)
            {
                showTxt.text = Manager.Language.GetString("ResourceManager.checkVersion.5");
            }
            else
            {
                showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.checkVersion.2"), w.text);
            }

            string getStr = w.text;
            string cVer = StringUtil.Split(getStr,",")[0];
            long clientVer = Convert.ToInt64(StringUtil.Split(getStr,",")[1]);
            TextAsset ver = Resources.Load<TextAsset>("version");
            long localClient = Convert.ToInt64(ver.text);
            if (clientVer > localClient)
            {
             //   showTxt.text = Manager.Language.GetString("ResourceManager.checkVersion.3");
                CancelInvoke("checkVersion");
             //   return;
            }
//            GlobalTools.ClientTime = localClient;
            if (PlayerPrefsUtility.HasKey(GlobalTools.CacheVersion))
            {
                string localVersion = PlayerPrefsUtility.GetString(GlobalTools.CacheVersion);
                showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.checkVersion.4"), localVersion);

//                if (true)
                if (localVersion != cVer)
                {
                    PlayerPrefsUtility.SetString(GlobalTools.CacheVersion, cVer);
                    getCache();
                }
                else
                {
                    if (HasFile(GetWritePathUrl(), "cache.bytes"))
                    {
                        _callback(50);
                        StartCoroutine(initLocalCache());
                    }
                    else
                    {
                        getCache();
                    }
                }
            }
            else
            {
                PlayerPrefsUtility.SetString(GlobalTools.CacheVersion, cVer);
                getCache();
            }
            CancelInvoke("checkVersion");
        }
    }

    private void delayCheckCache()
    {
        w = new WWW(GlobalTools.CacheUrl);
    }
    private int testCount;
    private void checkCache()
    {
        if (w.isDone)
        {
            if (w.error != null)
            {
                showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.checkCache.1"), w.error);
                Debuger.Log(w.error);
                Invoke("delayCheckCache", 2);
            }
            else
            {
                showTxt.text = Manager.Language.GetString("ResourceManager.checkCache.2");
                _callback(99);
                StartCoroutine(initCache());
                CancelInvoke();
            }
        }
        else
        {
            int per = (int)(w.progress * 100);
            if (testCount < 99)
            {
                testCount++;
            }
//            Debuger.Log(w.error);
            showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.checkCache.3"), per);
            //showTxt.text = "load ->" + per + "%" + testCount;
            _callback(testCount);
        }
        
    }

    private void checkCacheVersion()
    {
        long clientVer = GlobalTools.ClientVer;
        TextAsset ver = Resources.Load<TextAsset>("version");
        long localClient = Convert.ToInt64(ver.text);
        Debug.LogError(" !!!!! checkCachVersion = " + clientVer + " , " + localClient + " , " + GlobalTools.CacheVersion);
        if (clientVer > localClient)
        {
           // showTxt.text = Manager.Language.GetString("ResourceManager.checkCacheVersion.1");
           // return;
        }

        if (PlayerPrefsUtility.HasKey(GlobalTools.CacheVersion))
        {
            string localVersion = PlayerPrefsUtility.GetString(GlobalTools.CacheVersion);
            showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.checkCacheVersion.2"), localVersion);

            //                if (true)
            if (localVersion != GlobalTools.CacheVer+"")
            {
                getCache();
            }
            else
            {
                if (HasFile(GetWritePathUrl(), "cache.bytes"))
                {
                    _callback(50);
                    StartCoroutine(initLocalCache());
                }
                else
                {
                    getCache();
                }
            }
        }
        else
        {
            getCache();
        }
    }

    private void initResCache()
    {
        cache = Resources.Load<TextAsset>("cache");
        if (!cache)
        {
            Debuger.LogError("Can't loading cacheData correctly.");
        }
        Debuger.Log("res cache len->" + cache.bytes.Length);
        byte[] data = GameSocket.Decrypt(cache.bytes);
        var getStr = compress ? parseData(ref data) : Encoding.UTF8.GetBytes(w.text);
        
        StartCoroutine(parseCache(getStr)); 
        showTxt.text = Manager.Language.GetString("ResourceManager.initResCache");
        Debuger.Log("res cache!");

        hasCache = true;

        cacheData.InitCache();
        _callback(100);
    }

    private void getCache()
    {
        TextAsset localVersionStr = Resources.Load<TextAsset>("cacheVersion");
        int localVersion = Convert.ToInt32(localVersionStr.text);
        if (localVersion == Convert.ToInt32(GlobalTools.CacheVer))
        {
            Invoke("initResCache", 0.2f);
            return;
        }


        //        WWWForm form = new WWWForm();
        //        form.AddField("cache", "hehe");
        //        w = new WWW("http://" + GlobalTools.CacheUrl + "/Tool_CreateCdn/getCache/compress/1", form);

        
        string[] cdnPath = GlobalTools.CacheUrl.Split("/v/", StringSplitOptions.RemoveEmptyEntries);
        string filename = string.Format("cache1.json.{0}", cdnPath[1]);
        string downloadFullPath = string.Format("{0}/v/{1}", cdnPath[0], filename);
        Debug.LogError(" !!!!! downloadFullPath = " + downloadFullPath);
        w = new WWW(downloadFullPath);


        //https://storage.googleapis.com/nextlv-game-bucket-idlehero/assets/zh_cn/v/12693

        //w = new WWW(GlobalTools.CacheUrl);
        showTxt.text = Manager.Language.GetString("ResourceManager.getCache");
        testCount = 0;
        InvokeRepeating("checkCache", 0.1f, 0.1f);
    }

    public static byte[] decompression(byte[] msg)
    {
        GZipInputStream gzi = new GZipInputStream(new MemoryStream(msg));

        MemoryStream re = new MemoryStream(1024 * 1024 * 8);
        int count;
        byte[] data = new byte[4096];
        while ((count = gzi.Read(data, 0, data.Length)) != 0)
        {
            re.Write(data, 0, count);
        }
        byte[] depress = re.ToArray();
        return depress;
        //        return null;
    }

    private byte[] parseData(ref byte[] data)
    {
        GC.Collect();
        data = decompression(data);
        //string msg = Encoding.UTF8.GetString(data);
        //return msg;
        return data;
    }

    private IEnumerator initCache()
    {
        int len = w.bytes.Length;
        Debuger.Log("Cache size->" + w.bytes.Length);
        showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.initCache.1"), len/1024);

        yield return new WaitForFixedUpdate();
        byte[] data = GameSocket.Decrypt(w.bytes);
        var getStr = compress ? parseData(ref data) : Encoding.UTF8.GetBytes(w.text);
        StartCoroutine(parseCache(getStr)); 
        
        showTxt.text = Manager.Language.GetString("ResourceManager.initCache.2");
        Debug.Log("mysql cache!");

        hasCache = true;

//#if !UNITY_EDITOR
//        PlayerPrefsUtility.SetString(GlobalTools.Cache, getStr);
//#endif
        
        cacheData.InitCache();

        if (CreateByteFile(GetWritePathUrl(), "cache.bytes", w.bytes))
        {
            PlayerPrefsUtility.SetString(GlobalTools.CacheVersion, GlobalTools.CacheVer + "");
        }
        w.Dispose();
        _callback(100);
    }


    private IEnumerator initLocalCache()
    {
        showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.initLocalCache"), "");
        
        yield return new WaitForFixedUpdate();

//        cacheBytes = Encoding.UTF8.GetBytes(PlayerPrefsUtility.GetString(GlobalTools.Cache));

        yield return StartCoroutine(LoadBytesFromLocal(GetReadPathUrl(), "cache.bytes"));
        showTxt.text = "로컬 파일 압축 풀기->" + cacheBytes.Length;
        byte[] data = GameSocket.Decrypt(cacheBytes);
        parseData(ref data);
        Debug.Log("LOCAL LEN->" + cacheBytes.Length);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        StartCoroutine(parseCache(data)); 
        stopwatch.Stop();
        showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.initLocalCache"), stopwatch.ElapsedMilliseconds);
        
        if (w != null)
        {
            w.Dispose();
        }

        if (cacheData == null)
        {
            Debuger.LogError("local cache error.");
        }
        else
        {
            Debug.Log("local cache!");
            hasCache = true;
            cacheData.InitCache();
            _callback(100);
        }
        
    }

	private class CacheHeader
	{
		internal int pos;
		internal int size;
		internal JsonData data;
		
		internal CacheHeader(int _pos, int _size, JsonData _data)
		{
			pos = _pos;
			size = _size;
			data = _data;
		}
	}

	private CacheHeader parseCacheHeader(byte[] value)
	{
        Debug.LogError(" !!!!! parseCacheHeader = " + value.Length);

        Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();

        int pos = -1;
        for(int i = 0; i < value.Length; ++i)
        {
            ++pos;
            if(value[i] == '{')
            {
                break;
            }
        }

        //parse head size
        byte[] sizebuf = new byte[pos];
        Array.Copy(value, sizebuf, pos);
        int size = Convert.ToInt32(Encoding.UTF8.GetString(sizebuf));

        //parse head body
        byte[] headbuf = new byte[size];
        Array.Copy(value, pos, headbuf, 0, size);
		JsonData json = JsonMapper.ToObject(Encoding.UTF8.GetString(headbuf));
		stopwatch.Stop();
		Debuger.Log("json decode time=" + stopwatch.ElapsedMilliseconds);
		
		CacheHeader hdr = new CacheHeader(pos, size, json);
		return hdr;
	}

    private IEnumerator parseCache(byte[] getStr)
    {
		//parse header
		CacheHeader hdr = parseCacheHeader(getStr);
		
		//parse body
		var stopwatch = new Stopwatch();
		stopwatch.Start();
        IDictionary dic = hdr.data;
        var iter = dic.GetEnumerator();
        cacheData = new CacheData();
        byte[] text;
        while (iter.MoveNext())
        {
			string key = iter.Key.ToString();
            PropertyInfo pinfo = cacheData.GetType().GetProperty(key);
            if (pinfo != null)
            {
				JsonData val = (JsonData)iter.Value;
				int offset = (int)val["offset"];
				int size = (int)val["size"];
                text = new byte[size];
                Array.Copy(getStr, hdr.pos + hdr.size + offset, text, 0, size);
				cacheData.setSegment(key, text);
            }
            else
            {
                Debuger.Log("cacheData not key->" + iter.Key);
            }
        }

        var t1 = cacheData.MainEventList;
        showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.parseCache"), "main_event");
        yield return new WaitForEndOfFrame();

        var t2 = cacheData.SkillInfo;
        showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.parseCache"), "skill_info");
        yield return new WaitForEndOfFrame();

        var t3 = cacheData.heroList;
        showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.parseCache"), "hero_info");
        yield return new WaitForEndOfFrame();

        var t4 = cacheData.mainList;
        showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.parseCache"), "main");
        yield return new WaitForEndOfFrame();

        var t5 = cacheData.QuestInfoList;
        showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.parseCache"), "quest_info");
        yield return new WaitForEndOfFrame();

        var t6 = cacheData.ItemList;
        showTxt.text = string.Format(Manager.Language.GetString("ResourceManager.parseCache"), "item_info");

        stopwatch.Stop();
        Debuger.Log("init CacheJson->" + stopwatch.ElapsedMilliseconds + "ms");
    }
		
    private void initIcons()
        {
        skillIcon = getAtlas("skill", SkillIcon);
        headIcon = getAtlas("head", HeadIcon);
        headIcon2 = getAtlas("head2", HeadIcon);
        itemIcon = getAtlas("item", ItemIcon);
        itemIcon2 = getAtlas("item2", ItemIcon);
        itemIcon3 = getAtlas("item3", ItemIcon);
    }
			
    public UIAtlas skillIcon { get; private set; }

    public UIAtlas headIcon { get; private set; }
    public UIAtlas headIcon2 { get; private set; }

    public UIAtlas itemIcon { get; private set; }
    public UIAtlas itemIcon2 { get; private set; }
    public UIAtlas itemIcon3 { get; private set; }

    public bool showSkillIcon(UISprite icon, string iconID, bool makePixel = true)
            {
        return showIcon(icon, iconID, skillIcon, makePixel);
    }

    public bool showHeadIcon(UISprite icon, string iconID, bool makePixel = true)
    {
        if (headIcon.GetSprite(iconID) == null && headIcon2.GetSprite(iconID) == null)
        {
            icon.atlas = headIcon;
            icon.spriteName = "99999";
            //if (makePixel)
            //{
            //    icon.MakePixelPerfect();
            //}
            Debuger.Log(iconID + "아이콘이 없습니다. type->" + headIcon.name);
            return false;
        }
        var atlas = headIcon.GetSprite(iconID) != null ? headIcon : headIcon2;
        icon.atlas = atlas;
        icon.spriteName = iconID;
        //        icon.name = iconID;
        //if (makePixel)
        //{
        //    icon.MakePixelPerfect();
        //}
        return true;
    }

    public bool showItemIcon(UISprite icon, string iconID, bool makePixel = true)
    {
        if (itemIcon.GetSprite(iconID) == null && itemIcon2.GetSprite(iconID) == null && itemIcon3.GetSprite(iconID) == null)
        {
            icon.atlas = itemIcon;
            icon.spriteName = "99999";
            //if (makePixel)
            //{
            //    icon.MakePixelPerfect();
            //}
            Debuger.Log(iconID + "아이콘이 없습니다. type->" + headIcon.name);
            return false;
        }
        UIAtlas atlas;
        if (itemIcon.GetSprite(iconID) != null)
        {
            atlas = itemIcon;
        }
        else if (itemIcon2.GetSprite(iconID) != null)
        {
            atlas = itemIcon2;
        }
        else
        {
            atlas = itemIcon3;
        }
        icon.atlas = atlas;
        icon.spriteName = iconID;
        //        icon.name = iconID;
        //if (makePixel)
        //{
        //    icon.MakePixelPerfect();
        //}
        return true;
    }

    public bool showItemIcon(UISprite icon, ItemInfo info, bool makePixel = true)
    {
        if (info.baseInfo.type == ItemInfo.TypeHero)
        {
            return showHeadIcon(icon, info.baseInfo.icon, makePixel);
        }
        return showItemIcon(icon, info.baseInfo.icon, makePixel);
    }

    protected bool showIcon(UISprite icon, string iconID, UIAtlas atlas, bool makePixel)
    {
        if (atlas != null && atlas.GetSprite(iconID) == null)
        {
            icon.atlas = atlas;
            icon.spriteName = "99999";
//            icon.name = "noIcon";
            //if (makePixel)
            //{
            //    icon.MakePixelPerfect();
            //}
            Debuger.Log(iconID + "아이콘이 없습니다. type->" + atlas.name);
            return false;
        }
        icon.atlas = atlas;
        icon.spriteName = iconID;
//        icon.name = iconID;
        //if (makePixel)
        //{
        //    icon.MakePixelPerfect();
        //}
        return true;
    }

    public Texture2D getBackGround(string TextureName)
    {
         return getTexture2D(TextureName, Backgrounds);
    }

    public Texture2D getBattleBg(string TextureName)
    {
        return getTexture2D(TextureName, BattleBg);
    }

    public Texture2D getBattleEffect(string TextureName)
    {
        return getTexture2D(TextureName, BattleEffect);
    }

    public Texture2D getLeagueEventBg(string TextureName)
    {
        return getTexture2D(TextureName, LeagueEventBg);
    }

    public Texture2D getLeagueBg(string TextureName)
    {
        return getTexture2D(TextureName, LeagueBg);
    }

    public Texture2D getUIBg(string TextureName)
    {
        return getTexture2D(TextureName, UIbg);
    }

    public Texture2D getLogin(string TextureName)
    {
        return getTexture2D(TextureName, Login);
    }
    
    public Texture2D getMysteryBg(string TextureName)
    {
        return getTexture2D(TextureName, MysteryBg);
    }

    private Texture2D getTexture2D(string TextureName, string resName)
    {
        Texture2D image;
        if (debugMode)
        {
            string url = resName + "/" + TextureName + "";

            image = (Texture2D) Resources.Load(url);
        }
        else
        {
            image =
                DownloadManager.Instance.GetWWW(resName + extension).assetBundle.LoadAsset(TextureName) as Texture2D;
        }

//        print(image + "  " + resName + TextureName);
        return image;
    }


    public UIAtlas getAvatar(string AvatarName)
    {
        return getAtlas(AvatarName, Avatar);
    }

    public void showSkillEffect(UISpriteAnimation ani, string skillName, bool loop = false)
    {
        UISprite sp = ani.gameObject.GetComponent<UISprite>();
        sp.atlas = getSkillEffect(skillName);

        if (sp.atlas == null)
        {
            Debuger.Log("전투 중 피격 시 특수 효과를 찾을 수 없습니다.->" + skillName);
//            return;
            sp.atlas = getSkillEffect("wind");
        }
        ani.framesPerSecond = 24;
        ani.namePrefix = "moive";
        ani.loop = false;
        ani.ResetToBeginning();
        ani.Play();
    }

    public UIAtlas getSkillEffect(string skillName)
    {
        return getAtlas(skillName + "Avatar", "BattleHit");
    }

    public UIAtlas getAtlas(string AvatarName, string resName)
    {
        GameObject go;
        if (debugMode)
        {
            AvatarName = Regex.Replace(AvatarName, @"\s", "");
            string url = resName + "/" + AvatarName + "";
            go = Resources.Load(url) as GameObject;
            if (go == null)
            {
                url = resName + "/" + "99999Avatar";
                go = Resources.Load(url) as GameObject;
                Debuger.Log(AvatarName + " 모델을 찾을 수 없음！");
            }
//            print(url + " " + Resources.Load(url));
        }
        else
        {
            string url = AvatarName + "";
            WWW www = DownloadManager.Instance.GetWWW(resName + extension);
            go = www.assetBundle.LoadAsset(url) as GameObject;
            www.Dispose();
            
        }
        if (go != null)
        {
            var atlas = go.GetComponent<UIAtlas>();
            return atlas;
        }
        Debuger.Log(AvatarName + " " + resName + " is null!");
        return null;
    }

    public void showSecMapTexture(UITexture texture, string textureName)
    {
        string url = MysteryBg + "/" + textureName + "";
        setETCMat(texture, url);
    }

    public void showMapTexture(UITexture texture, string textureName)
    {
        string url = Backgrounds + "/" + textureName + "";
        setETCMat(texture, url);
    }

    public void showLoginTexture(UITexture texture, string textureName)
    {
        string url = Login + "/" + textureName + "";
        setETCMat(texture, url);
    }

    public void showBattleEffect(UITexture texture, string textureName)
    {
        string url = BattleEffect + "/" + textureName + "";
        setETCMat(texture, url);
    }

    public void showUIbgTexture(UITexture texture, string textureName) 
    {
        string url = UIbg + "/" + textureName + "";
        setETCMat(texture, url);
    }

    public void showWorldWarTexture(UITexture texture, string textureName) 
    {
        string url = worldWar + "/" + textureName + "";
        setETCMat(texture, url);
    }

    public void showGemIcon(UITexture texture, string textureName) 
    {
        string url = GemIcon + "/" + textureName + "";
        setETCMat(texture, url);
    }

    

    private void setETCMat(UITexture texture,string url)
    {
        if (!texture)
        {
            return;
        }
        Material mat = (Material)Resources.Load("materials/ui_etc1");

        Material matClone = Instantiate(mat);
        texture.material = matClone;

        string fileName = string.Format("{0}_a", url);

        Texture mainTexture = (Texture)Resources.Load(url);
        Texture alphaTexture = (Texture)Resources.Load(fileName);

        if (!mainTexture || !alphaTexture)
        {
            return ;
        }
        matClone.SetTexture("_MainTex", mainTexture);
        matClone.SetTexture("_AlphaTex", alphaTexture);
    }

    private bool CreateByteFile(string path, string fname, byte[] data)
    {
        // 캐시 저장 
        string filePath = path + "/" + fname;
        Debug.Log("서버로 부터 받은 캐시 파일 저장 ->" + filePath);

        DirectoryInfo dic = new DirectoryInfo(path);
        if (!dic.Exists)
        {
            dic.Create();
        }

        BinaryWriter bw = new BinaryWriter(new FileStream(filePath, FileMode.Create));
        
        try
        {
            bw.Write(data.Length);
            bw.Write(data);
        }
        catch (IOException e)
        {
            Debuger.Log(e.Message);
        }
        bw.Close();
        Debuger.Log("SAVED!");
        return true;
    }

    private bool HasFile(string path, string fname)
    {
        FileInfo t = new FileInfo(path + "/" + fname);
        if (t.Exists)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator LoadBytesFromLocal(string path, string fname)
    {
        Debuger.Log("LOAD->" + path + "/" + fname);
        BinaryReader rw = new BinaryReader(new FileStream(path + "/" + fname, FileMode.Open));
        int len = rw.ReadInt32();
        cacheBytes = rw.ReadBytes(len);
        rw.Close();
        yield return null;
//        WWW ww = new WWW(path + "/" + fname);
//        print("LOAD->" + ww.url);
//        yield return ww;
//        if (ww.isDone)
//        {
//            if (ww.error != null)
//            {
//                Debug.Log("LOAD ERROR->" + ww.error);
//            }
//            else
//            {
//                print("LOAD OVER->" + ww.url);
//                cacheBytes = ww.bytes;
//            }
//            
//            ww.Dispose();
//        }
    }

    public byte[] LoadMapFromLocal()
    {
        TextAsset mapRes = Resources.Load<TextAsset>("bigMap");
        return mapRes.bytes;
    }


    private void DeleteFile(string path, string fname)
    {
        File.Delete(path + "/" + fname);
    }

    private string GetWritePathUrl()
    {
        string rtstr = string.Empty;
#if UNITY_EDITOR
        rtstr = Application.persistentDataPath;
#elif UNITY_IOS
        rtstr = Application.persistentDataPath;
#elif UNITY_ANDROID
        rtstr = Application.persistentDataPath;
#endif
        return rtstr;
    }

    private string GetReadPathUrl()
    {
        string rtstr = string.Empty;
#if UNITY_EDITOR
        rtstr =  Application.persistentDataPath;
#elif UNITY_IOS
        rtstr = Application.persistentDataPath;
#elif UNITY_ANDROID
        rtstr = Application.persistentDataPath;
#endif
        return rtstr;
    }
}