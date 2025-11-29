using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Game.Util;
using Water.Game.Util;

using LuaInterface;
using Cysharp.Threading.Tasks;
using XAsset;
using Water.Config;
using System;
using UnityEngine.Networking;

namespace Water
{
    public class StartUpBGComponent : MonoBehaviour
    {
        static string getPreloadLoginViewPath()
        {
            if (GameConfig.Language == "zh" || GameConfig.Language == "tw")
                return "LoginUI_preload";
            else if (GameConfig.Language == "kr")
                return "lang_kr/LoginUI_preload";
            else if (GameConfig.Language == "jp")
                return "lang_jp/LoginUI_preload";

            return "lang_en/LoginUI_preload";
        }

        public static void showStartUpLoginView(Transform pTrans_)
        {
            GameObject startUpUI = GameObject.Instantiate(Resources.Load<GameObject>(getPreloadLoginViewPath()) as GameObject);
            startUpUI.transform.SetParent(pTrans_, false);
            var startUpCom = startUpUI.AddComponent<StartUpBGComponent>();
            startUpCom.showGameInitWaitView();
        }

        public static IEnumerator showLoginViewCO()
        {
            var op = Resources.LoadAsync<GameObject>(getPreloadLoginViewPath());
            if (!op.isDone)
                yield return op;

            GameObject startUpUI = GameObject.Instantiate(op.asset as GameObject);
            startUpUI.AddComponent<StartUpBGComponent>();
            var openUIOp = SingletonFactory<UIManager>.Instance.OpenUI(UIType.LoginUI, startUpUI.transform, true);
            if (openUIOp != null)
                yield return openUIOp;
        }

        public static IEnumerator restartLoginViewCO()
        {
            var op = Resources.LoadAsync<GameObject>(getPreloadLoginViewPath());
            if (!op.isDone)
                yield return op;

            GameObject startUpUI = GameObject.Instantiate(op.asset as GameObject);
            startUpUI.transform.SetParent(Main.Instance.mainCanvas.transform, false);
            var startUpCom = startUpUI.AddComponent<StartUpBGComponent>();
            startUpCom.restartGameInitWaitView();
        }



        FakeLoadingSlider loadingSlider;    
        FakeLoadingSlider downloadSlider;   

        public void showGameInitWaitView()
        {
            Transform sliderTrans = TransformUtil.FindChildAccurately(this.transform, "laoding_di_img");
            loadingSlider = sliderTrans.gameObject.AddComponent<FakeLoadingSlider>();
            loadingSlider.InitUIView(null);
            showBGAnimation(false);

            startInitFinishCheck().Forget();
        }

        public void restartGameInitWaitView()
        {
            Transform sliderTrans = TransformUtil.FindChildAccurately(this.transform, "laoding_di_img");
            loadingSlider = sliderTrans.gameObject.AddComponent<FakeLoadingSlider>();
            loadingSlider.InitUIView(null);

            showBGAnimation(false);
            
            // show init state
            restartInitFinishCheck().Forget();

        }

        private async UniTask startInitFinishCheck()
        {
            Assets.CurLang = Water.GameConfig.Language;
            loadingSlider.Visible(true);
            loadingSlider.startLoading(30);

            //var initializeSDKs = await isIsInitializedSDKs();

            //if (false == initializeSDKs)
            //    return;
            
            
            var initializeAsync = Assets.InitializeAsync();
            await initializeAsync;
            ShowFPS();

#if !UNITY_EDITOR
            //update resources
            var cdnDownloadAsync = await cdnDownloadCheck();

            if (false == cdnDownloadAsync)
                return;

            //여기서 실패 떨어지면... 로그인 버튼을 띄워야됨.
            //자동 로그인 테스트~.
            await SingletonFactory<GameBaseSDKManager>.Instance.TryGoogleLoginSignInSilently_ASYNC(OnCompleteGoogleSignInCallback);
#endif

#if DEV_SERVER || UNITY_EDITOR
            Debug.LogError(" !!!!!! StartUPBGComponent startInitFinishCheck ..... 00000000000 ");

            SingletonFactory<UIManager>.Instance.OpenUI(UIType.LoginUI, this.transform, true);
#endif

        }

        private void OnCompleteGoogleSignInCallback(bool isSuccess, string resultMessage)
        {
            Debug.LogError(" !!!!!!!!!!! OnCompleteGoogleSignInCallback " + isSuccess + " , " + resultMessage);

            if (false == isSuccess)
            {
                //fail google signin silently or manually 
                if( resultMessage == "FALSE==SilentlySignIn")
                {
                    //failed Signin silently
                    //show google login btn...
                    //TryGoogleLoginSignInSilently_ASYNC is Fail...
                    SingletonFactory<GameBaseSDKManager>.Instance.TryGoogleSignIn_ASYNC(OnCompleteGoogleSignInCallback).Forget();
                }
                else if (resultMessage == "UserCancel")
                {
                    //user cancel
                }
                else
                {
                    // retryCnt = 0 
                    // show signin failed...
                    // TryGoogleLoginSignIn
                }
            }
            else
            {
                //google silently or manually signin success...
                //3kds sdk GoogleAuth success
                //proceed next step... download cdn data...
                Debug.LogError(" !!!!!!!!!!!!!!! StartUpBGComponent ConnectWithAuthCore !!!!!!!!!!!!!!! " + SingletonFactory<SDKMemberManager>.Instance.ParticleJWTToken);
                         //test particle jwt로 변경
                SingletonFactory<GameBaseSDKManager>.Instance.AuthCoreConnectWithJWT(SingletonFactory<SDKMemberManager>.Instance.ParticleJWTToken, onWalletReferenceCallback);
                
            }
        }


        private void onWalletReferenceCallback(bool result, string message)
        {
            Debug.LogError(" !!!!!!!!!!!!!!! onWalletReferenceCallback !!!!!!!!!!!!!!! " + result + " , " + message);

            //Debug.LogError(" !!!!!!!!!!!!!!! success !!!!!!!!!!!!!!! " + message);

            //if (true == result)
            //{
            //    Debug.LogError(" !!!!!!!!!!!!!!! success !!!!!!!!!!!!!!! " + message);
            //    //SingletonFactory<UIManager>.Instance.OpenUI(UIType.LoginUI, this.transform, true);
            //}
            //else
            //{
            //    Debug.LogError(" !!!!!!!!!!!!!!! failed !!!!!!!!!!!!!!! " + message);
            //}

            Debug.LogError(" !!!!!! StartUPBGComponent startInitFinishCheck ..... 1111111111 ");

#if USE_THIRD_PARTY_CHANNEL
            SingletonFactory<UIManager>.Instance.OpenUI(UIType.LoginUI, this.transform, true);
#endif

        }

        // private async UniTask<bool> isIsInitializedSDKs()
        // {

        //     Debug.LogError(" !!!!!!!!!!!!!!! isIsInitializedSDKs GameBaseSDKManager Firebase && Google  IsInitialized    = " + SingletonFactory<GameBaseSDKManager>.Instance.IsInitialized);
        //     Debug.LogError(" !!!!!!!!!!!!!!! isIsInitializedSDKs GameBaseSDKManager Particle IsInitializedParticle       = " + SingletonFactory<GameBaseSDKManager>.Instance.IsInitializedParticle);
        //     Debug.LogError(" !!!!!!!!!!!!!!! isIsInitializedSDKs IAPManager IsInitialized                                = " + SingletonFactory<IAPManager>.Instance.IsInitialized());
        //     Debug.LogError(" !!!!!!!!!!!!!!! isIsInitializedSDKs SDKMemberManager IsInitialized                          = " + SingletonFactory<SDKMemberManager>.Instance.IsInitialized);

        //     return true;
        // }

        private async UniTask<bool> cdnDownloadCheck()
        {
            Debug.LogError(" !!!!!!!!!!!!!!! StartInitFinish Check !!!!!!!!!!!!!!! ");

            downloadHandler();
            await downloadAssets();


            if (Assets.DownloadState == XAsset.AssetDownloadState.DownloadFail)
            {
                return false;
            }

            return true;
        }

        private async UniTask<bool> downloadAssets()
        {
            var updateInfoReqAsync = Assets.UpdateInfoReqAsync();
            await updateInfoReqAsync;
            if (updateInfoReqAsync.result != Request.EResult.Success)
            {
                //Debug.LogError("updateInfoReqAsync failed");
                return false;
            }
            //Debug.LogError("updateInfoReqAsync succ");
            string appVersion = SingletonFactory<SDKMemberManager>.Instance.AppVersion;
            string cdnVersion = SingletonFactory<SDKMemberManager>.Instance.CdnVersion;

            Debug.LogError(" 0000000000 " + Assets.ClientVersion + " , " + Application.version + " , " + appVersion + " , " + cdnVersion);
            if (string.IsNullOrEmpty(Assets.ClientVersion))
            {
                if (!System.Version.TryParse(Assets.ClientVersion, out var updateVersion) ||
                    !System.Version.TryParse(Application.version, out var playerVersion))
                {
                    MessageBoxUtils.ShowSureMessgeBox(CodeTextData.AUTOSTR("1033225"), (PopWinVO vo_) => { });
                    Assets.DownloadState = AssetDownloadState.DownloadFail;
                    PersistentEventManager.Instance.DispatchEvent(Assets.LoginUpdateResFail, null); 

                    return false;
                }

                if (updateVersion.Major > playerVersion.Major || (updateVersion.Major == playerVersion.Major && updateVersion.Minor > playerVersion.Minor))
                {
                    ChannelManager.NewAFDot("receive_choose_result_need_update_client");
                    string showstr = CodeTextData.AUTOSTR("{0}");

                    // no direct download channel for China
                    if (ChannelManager.isMengGuoHaiWai() == false)
                    {
                        showstr = string.Format("{0}", showstr);
                    }

                    string displayInfo = Assets.ClientRemotePath;
                    MessageBoxUtils.ShowSureMessgeBox(string.Format(showstr, Assets.ClientVersion), (PopWinVO pop_) =>
                    {
                        Application.OpenURL(displayInfo);
                    });
                    Assets.DownloadState = AssetDownloadState.DownloadFail;
                    PersistentEventManager.Instance.DispatchEvent(Assets.LoginUpdateResFail, null);
                    return false;
                }
            }

            //bool needDownload = false;// CheckDownloadAssets(Assets.AssetVersion);
            Debug.LogError(" DDDDDDDDDD = " + Assets.AssetVersion + " , " + appVersion + " ,  " +cdnVersion);
            bool needDownload = CheckDownloadAssets(Assets.AssetVersion);
            if (!needDownload && XAsset.Assets.Manifests.fullManifest)
            {
                ChannelManager.NewAFDot("no_need_download_asset");
                return false;
            }

            var downloadManifestAsync = Assets.DownloadManifestAsync();

            await downloadManifestAsync;

            if (downloadManifestAsync.result != Request.EResult.Success)
                return false;

            var updateResourcesAsync = Assets.UpdateResourceToLatestAsync(downloadManifestAsync.serverManifest);
            await updateResourcesAsync;
            if (updateResourcesAsync.result != Request.EResult.Success)
            {
                return false;
            }

            var reloadRequestAsync = Assets.ReloadAsync();
            await reloadRequestAsync;
            if( reloadRequestAsync.result != Request.EResult.Success ) 
            {
                return false; 
            }

            return true;
        }

        private void downloadHandler()
        {
            GameObject sliderObj = GameObject.Instantiate(Resources.Load<GameObject>("laoding_di_img") as GameObject);
            sliderObj.transform.SetParent(this.transform, false);
            sliderObj.name = "laoding_di_img";
            downloadSlider = sliderObj.AddComponent<FakeLoadingSlider>();
            downloadSlider.Visible(false);
            downloadSlider.InitUIView(null);

            PersistentEventManager.Instance.AddEventListener(Assets.LoginStartUpdateRes, onStartUpdateRes);
            PersistentEventManager.Instance.AddEventListener(Assets.LoginUpdateResFail, onUpdateError);
            PersistentEventManager.Instance.AddEventListener(Assets.LoginStartRetryUpdateRes, onRetryUpdate);
        }

        private bool CheckDownloadAssets(string version)
        {
            if (string.IsNullOrEmpty(version) || string.Equals(version, "0"))
            {
                return false;
            }
            int curVer, srvVer = 0;
            curVer = Assets.Manifests.assetVersion;
            try
            {
                ResVersionParser param = new ResVersionParser(version);
                string parsedVerStr = param.getValue(Assets.CurLang);
                if (!string.IsNullOrEmpty(parsedVerStr))
                    srvVer = int.Parse(parsedVerStr);
            }
            catch (Exception e)
            {
                Debug.Log($"read version error {e}");
                return false;
            }

            Assets.VersionMD5 = srvVer.ToString();

#if UNITY_EDITOR
            // not update in editor
            //Debug.LogError(" zzzzzzzzzz curVer =" + curVer + " , srvVer =" + srvVer + " , Assets.VersionMD5 =" + Assets.VersionMD5);
            return false;

#endif


            //강제 update
            if (true == SingletonFactory<SDKMemberManager>.Instance.ForceUpdate)
            {
                return true;
            }
            
            return curVer != srvVer;
        }

        [System.Diagnostics.Conditional("SHOW_FPS")]
        void ShowFPS()
        {
            float gameHeight = SingletonFactory<UIManager>.Instance.gameHeight;
            Main.Instance.fpsTips = DebugUtils.AddTxt("fps:", new Vector2(20, gameHeight - 30));
        }

        void onRetryUpdate()
        {
            retryDownloadAssets().Forget();
        }

        private async UniTask retryDownloadAssets()
        {
            await downloadAssets();
            if (Assets.DownloadState == XAsset.AssetDownloadState.DownloadFail)
            {
                return;
            }

            Debug.LogError(" !!!!!! StartUPBGComponent retryDownLoadAssets ..... 22222 ");
            SingletonFactory<UIManager>.Instance.OpenUI( UIType.LoginUI, this.transform, true );
        }


        public void OnDisable()
        {
            PersistentEventManager.Instance.RemoveEventListener(LoginSystem.LoginStartUpdateRes, onStartUpdateRes);
            PersistentEventManager.Instance.RemoveEventListener(LoginSystem.LoginUpdateResFail, onUpdateError);
            PersistentEventManager.Instance.RemoveEventListener(LoginSystem.LoginStartRetryUpdateRes, onRetryUpdate);
        }

        void onUpdateError()
        {
            if (showDownloadSliderCor != null) StopCoroutine(showDownloadSliderCor);
            downloadSlider.Visible(true);
            loadingSlider.Visible(false);
            downloadSlider.loadingTo(0);
            downloadSlider.showTips(Water.Config.CodeTextData.AUTOSTR("1022355"));
        }

        Coroutine showDownloadSliderCor;
        void onStartUpdateRes()
        {
            showDownloadSliderCor = StartCoroutine(showUpdateResView());
        }

        IEnumerator showUpdateResView()
        {
            downloadSlider.Visible(true);
            loadingSlider.Visible(false);
            downloadSlider.loadingTo(0);

            int state = (int)XAsset.AssetDownloadState.GetVersion;

            downloadSlider.showTips(Water.Config.CodeTextData.AUTOSTR("1022588"));
            while (state <= (int)XAsset.AssetDownloadState.GetVersion)
            {
                yield return null;
                state = (int)Assets.DownloadState;
            }

            state = (int)XAsset.AssetDownloadState.DownloadMainifest;

            // manifest
            downloadSlider.showTips(Water.Config.CodeTextData.AUTOSTR("1023554"));
            while (state <= (int)XAsset.AssetDownloadState.DownloadMainifest)
            {
                yield return null;
                state = (int)Assets.DownloadState;
            }

            // check diff
            downloadSlider.showTips(Water.Config.CodeTextData.AUTOSTR("1023311"));
            while (state <= (int)XAsset.AssetDownloadState.CheckFileDiff)
            {
                yield return null;
                state = (int)Assets.DownloadState;
            }

            // download asset
            ulong downloadedSize = 0;
            ulong downloadAllSize = Assets.downloadAllSize;
            string AllSize = WaterGameConst.BytesSizeSuffix(downloadAllSize);
            downloadSlider.showTips(string.Format(Config.CodeTextData.AUTOSTR("{0}/{1}"), WaterGameConst.BytesSizeSuffix(downloadedSize), AllSize));
            while (state <= (int)XAsset.AssetDownloadState.DownloadAssets)
            {

                if (downloadedSize != Assets.downloadedSize)
                {
                    downloadedSize = Assets.downloadedSize;
                    float ratio = (float)downloadedSize / downloadAllSize;
                    downloadSlider.loadingTo(ratio, 0.1f);
                    downloadSlider.showTips(string.Format(Config.CodeTextData.AUTOSTR("{0}/{1}"), WaterGameConst.BytesSizeSuffix(downloadedSize), AllSize));
                }

                state = (int)Assets.DownloadState;
                yield return null;
            }

            // reload script
            string applyUpdateStr = Water.Config.CodeTextData.AUTOSTR("1024481");
            downloadSlider.showTips(applyUpdateStr);
            while (state <= (int)XAsset.AssetDownloadState.UpdateScript)
            {
                yield return null;
                state = (int)Assets.DownloadState;
            }

            // finish up
            downloadSlider.loadingTo(1, 0.1f);
        }

        async UniTaskVoid restartInitFinishCheck()
        {
            Assets.CurLang = Water.GameConfig.Language;

            // we need to reload "waitingforloading" view, since pic has chinese words 
            UIManager.Instance.reloadWaitForLoad();

            Config.CodeTextData.Clear();
            Config.LocalizedTextData.Clear();

            // reload package content
            if (Assets.Manifests.looseFileType == (byte)LooseFileType.package)
            {
                string url ;
                if (Assets.Manifests.isPackageInDownloadFolder)
                    url = Assets.GetDownloadDataURL(Assets.PackageFileName);
                else
                    url = Assets.GetPlayerDataURL(Assets.PackageFileName);

                var www = UnityWebRequest.Get(url);
                await www.SendWebRequest();
                if (!string.IsNullOrEmpty(www.error))
                {
                    www.Dispose();
                    return;
                }
                var data = www.downloadHandler.data;
                www.Dispose();
                Assets.Manifests.LoadPackedData(data);
            }


            StartUpCommand.configCSVInitCo();
            LuaPacketData.initPacketHandlerFinished = false;

            LuaScriptMgr.Instance.ClearCachedLuas();
            LuaScriptMgr.Instance.RestoreMgrLuaFunctions();
            LuaScriptMgr.Instance.fileList.Clear();
            LuaMain.Instance.luaMgr.DoFile("water/Main.lua");

            while (true)
            {
                // init lua finished and config load finished
                if (LuaMain.Instance != null && LuaMain.Instance.isLuaInitFinished() && StartUpCommand.configLoadCOFinished)
                {
                    break;
                }
                await UniTask.Yield();
            }

            WaitingForLoad.Instance.ReloadTips();
            await loadingSlider.finishLoading();

            Debug.LogError(" !!!!!! StartUPBGComponent restartInitFinishCheck ..... 3333333333 ");

            SingletonFactory<UIManager>.Instance.OpenUI(UIType.LoginUI, this.transform, true).ToUniTask().Forget();
        }

        public void showLoginBGView(Transform pTrans_){
            this.transform.SetParent(pTrans_, false);

            Transform sliderTrans = TransformUtil.FindChildAccurately(this.transform, "laoding_di_img");
            Destroy(sliderTrans.gameObject);

            if (downloadSlider != null)
            {
                downloadSlider.Visible(false);
            }

            showBGAnimation(true);
        }

        void showBGAnimation(bool show_){
            List<ParticleSystem> pss = new List<ParticleSystem>();
            TransformUtil.findComponentsInAllChildren<ParticleSystem>(this.transform, pss);
            if(pss != null){
                for(int i = 0; i < pss.Count; ++i){
                    if(show_)
                        pss[i].Play();
                    else
                        pss[i].Pause();
                }
            }

            List<Animator> anims = new List<Animator>();
            TransformUtil.findComponentsInAllChildren<Animator>(this.transform, anims);
            if(anims != null){
                for(int i = 0; i < anims.Count; ++i){
                    if(show_)
                        anims[i].speed = 1;
                    else
                        anims[i].speed = 0;
                }
            }

            List<MeshRenderer> mrs = new List<MeshRenderer>();
            TransformUtil.findComponentsInAllChildren<MeshRenderer>(this.transform, mrs);
            if(mrs != null){
                for(int i = 0; i < mrs.Count; ++i){
                    if(show_)
                        mrs[i].gameObject.SetActive(true);
                    else
                        mrs[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
