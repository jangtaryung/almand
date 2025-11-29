// #define USE_THIRD_PARTY_CHANNEL

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

using LuaInterface;

namespace Water
{

    public class LoginView : BaseView 
    {		
        public enum LoginState{
            E_SDK_NOT_LOGINED,   
            E_SDK_LOGINED,  
            E_ON_LOGIN_SERVER,
            E_CONNECTING_LOGIN_SERVER,
            E_UPDATE_RES,   
        }
        public static LoginState loginState = LoginState.E_SDK_NOT_LOGINED;

        FakeLoadingSlider loadingSlider;    

        CurrentServerDisplay selectedServer;
        Button  connectBtn;
        Button gonggao_panel;
        Button qiehuanzhanghao_panel;

        Button support_panel;

        Button del_panel;

        TextMeshProUGUI banbenhao_txt;  
        TextMeshProUGUI pizhunhao_txt;
        TextMeshProUGUI tishi_lbl;

        Image normal_bg_img;
        Image img_age;
        Transform special_bg_root;

        public override HoverViewType ViewHoverType
        {
            get
            {
                return HoverViewType.UnShow;
            }
        }


		public LoginView()
			: base(UIType.LoginUI)
		{
        }


#if USE_THIRD_PARTY_CHANNEL
        static bool isFirstLogin = true;    
#endif

        
        protected override void AwakeInit(object bgTrans_ = null)
        {
            base.AwakeInit();

            //XAsset.BundleRequest.Load(XAsset.Assets.Manifests.abManifest.GetBundle("scene_maincity2.ab"));
            
            special_bg_root = getUIComponent<Transform>("bg_root");
            normal_bg_img =  getUIComponent<Image>("img_bg");
            img_age =  getUIComponent<Image>("img_age");
            
            loadingSlider  = getUIComponent<FakeLoadingSlider>("laoding_di_img");
            selectedServer = getUIComponent<CurrentServerDisplay>("xuanqu_panel");
            //yonghuzhongxin_panel = getUIComponent<Button>("yonghuzhongxin_panel");
            //yonghuxieyi_panel = getUIComponent<Button>("yonghuxieyi_panel");
            gonggao_panel = getUIComponent<Button>("gonggao_panel");
            qiehuanzhanghao_panel = getUIComponent<Button>("qiehuanzhanghao_panel");
            connectBtn    = getUIComponent<Button>("startGame_btn");
            //connectBtn = getUIComponent<Button>("LoginGo_btn");
            banbenhao_txt = getUIComponent<TextMeshProUGUI> ("banbenhao_txt");
            pizhunhao_txt = getUIComponent<TextMeshProUGUI>("pizhunhao_txt");
            tishi_lbl     = getUIComponent<TextMeshProUGUI>("tishi_lbl");
            support_panel = getUIComponent<Button>("support_panel");
            del_panel = getUIComponent<Button>("del_panel");

            //yonghuzhongxin_panel.onClick.AddListener(onUserCenter);
            //yonghuxieyi_panel.onClick.AddListener(onUserAgreement);
            gonggao_panel.onClick.AddListener(onNotice);
            qiehuanzhanghao_panel.onClick.AddListener(onChangeUser);
            connectBtn.onClick.AddListener(onConnectBtnClicked);

            support_panel.onClick.AddListener(OnCustomServiceClick);
            del_panel.onClick.AddListener(OnDelAccountClick);

            // PersistentEventManager.Instance.AddEventListener(LoginSystem.LoginRecvServerList, onRecvServerList);
            //PersistentEventManager.Instance.AddEventListener(LoginSystem.LoginUpdateResFail, onUpdateError);
            //PersistentEventManager.Instance.AddEventListener(LoginSystem.LoginStartUpdateRes, onStartUpdateRes);
            PersistentEventManager.Instance.AddEventListener(LoginSystem.DebugLoginSucessed, onDebugLogin);
            PersistentEventManager.Instance.AddEventListener(ChannelManager.CallShowChangeUser, onChangeUser);
            PersistentEventManager.Instance.AddEventListener(ChannelManager.SDKLogout, onSDKLogout);
            PersistentEventManager.Instance.AddEventListener(LoginSystem.ReconnectLoginEvt, ReconnectLogin);

            if (null != SingletonFactory<UIManager>.Instance.DebugUIView)
            {
                showDebugBtns();

                showClearMemBtn();
            }

                     
            SoundManager.Instance.BGMComponent.PlayBgm (SoundManager.BgmType.Login);

                     
            Transform bgTrans = bgTrans_ as Transform;
            StartUpBGComponent bgComp = bgTrans.GetComponent<StartUpBGComponent>();
            string iconPath = Config.PropertyData.GetData(90003).Value;
            if (normal_bg_img)
            {
            	normal_bg_img.gameObject.SetActive(false);
            }
            if (iconPath != null && iconPath != "0" && iconPath != "")
            {
                 XAsset.Assets.LoadWithOwner<Sprite>(iconPath, (Sprite sp) =>
                {
                    if (sp != null)
                    {
                        normal_bg_img.sprite = sp;
                        normal_bg_img.gameObject.SetActive(true);
                    }
                }, gameObject);
            }
            bgComp.showLoginBGView(special_bg_root);

            if (img_age)
                img_age.gameObject.SetActive(ChannelManager.isGuoNeiShuaBang());

#if USE_THIRD_PARTY_CHANNEL
            
            if(!ChannelManager.isLogined() || ChannelManager.userSwitched){
                ChannelManager.userSwitched = false;
                loginState = LoginState.E_SDK_NOT_LOGINED;
            }
#endif

            if (loginState == LoginState.E_SDK_NOT_LOGINED){
                
#if USE_THIRD_PARTY_CHANNEL
                
                if(isFirstLogin)
                    showChannelLogin();
                else
                    onSDKLogout();
#else
                showBuiltInLogin();
#endif
            }
            else if(loginState == LoginState.E_SDK_LOGINED){
                sendChannelLogin();
            }
            else if(loginState == LoginState.E_ON_LOGIN_SERVER || loginState == LoginState.E_UPDATE_RES){
                
                //onRecvServerList();
                // onConnectingLoginServer();
                onRecvServerList();
            }

			banbenhao_txt.text = $"v{UnityEngine.Application.version }.{XAsset.Assets.Manifests.assetVersion}";
            hideDescriptions();
#if USE_THIRD_PARTY_CHANNEL
            isFirstLogin = false;
#endif      
          
            // StartCoroutine(wait());
            //CheckServerBlockState();
        }

		//public void CheckServerBlockState()
  //      {
		//	//LoginSystem.Instance.ClearPing();
  //          foreach(var blocks in ChannelManager.ChannelServerBlockModArys){
  //              for (int i = 0; i < LoginSystem.Instance.pingMaxNum; i++)
		//		{
  //                  //StartCoroutine(UnityPing.PingConnect(blocks[0].mIP, blocks[0].mId));
		//		}
  //          }
  //      }
        protected override void OnEnter()
        {
            base.OnEnter();
        }

        protected override void OnExit()
        {
            base.OnExit();
        }

        public override void Dispose(){
            base.Dispose();

            // PersistentEventManager.Instance.RemoveEventListener(LoginSystem.LoginRecvServerList, onRecvServerList);
            //PersistentEventManager.Instance.RemoveEventListener(LoginSystem.LoginUpdateResFail, onUpdateError);
            //PersistentEventManager.Instance.RemoveEventListener(LoginSystem.LoginStartUpdateRes, onStartUpdateRes);
            PersistentEventManager.Instance.RemoveEventListener(ChannelManager.SDKLogout, onSDKLogout);
            PersistentEventManager.Instance.RemoveEventListener(ChannelManager.CallShowChangeUser, onChangeUser);
            PersistentEventManager.Instance.RemoveEventListener(LoginSystem.DebugLoginSucessed,onDebugLogin);
            PersistentEventManager.Instance.RemoveEventListener(LoginSystem.ReconnectLoginEvt, ReconnectLogin);

            removeDebugBtns();
        }

        
        int _roleInput;
        int _roleGameServerInput;
        int _scriptInput;
        void removeDebugBtns(){
#if !USE_THIRD_PARTY_CHANNEL || GM_SETTING_PACKAGE
            DebugUtils.RemoveInputField(_roleInput);
            DebugUtils.RemoveInputField(_roleGameServerInput);
            DebugUtils.RemoveInputField(_scriptInput);
#endif
        }

        void createLoginServerBtn(ChannelServerBlockMod mod, int idx_){
            float width = SingletonFactory<UIManager>.Instance.gameWidth;
            float height = SingletonFactory<UIManager>.Instance.gameHeight;
            DebugUtils.AddButton(mod.name, this, new Vector2(width - 400, height - 42 * (idx_ + 1)), () =>
            {
                // string[] split = loginServers[idx_].Split(':');
                // LoginSystem.Instance.LoginServerIp = mod.mIP;
                // LoginSystem.Instance.LoginServerPort = mod.mPort;
                SelectDebugBlockId = mod.mId;
                GameConfig.SaveServerBlockModId(mod.mId);
            });
        }
        
        void createChannelBtn(string channelName_, int idx_, int channelId_){
            float width = SingletonFactory<UIManager>.Instance.gameWidth;
            float height = SingletonFactory<UIManager>.Instance.gameHeight;
            DebugUtils.AddButton(channelName_, this, new Vector2(width - 250, height - 42 * (idx_ + 1)), () =>
            {
                ChannelManager.editorChannelSet = true;
                ChannelManager.editorChannelID = channelId_;
            });
        }

        private int SelectDebugBlockId = 0;

       

        void onUserCenter(){

        }

        void onUserAgreement(){

        }

        void onNotice(){
            if(LoginSystem.Instance.noticeInfos.Count > 0)
				SingletonFactory<UIManager>.Instance.OpenUI(UIType.NoticeUI, LoginSystem.Instance.noticeInfos);
            else
            {
                if(!string.IsNullOrEmpty(LoginSystem.Instance.ServNotice))
                    SingletonFactory<UIManager>.Instance.OpenUI(UIType.NoticeUI, LoginSystem.Instance.ServNotice);
            }
        }

        void onChangeUser(){
            
#if USE_THIRD_PARTY_CHANNEL
            showChannelSwitchUser();
#else
            LoginSystem.Instance.DisconnectServer();

            loginState = LoginState.E_SDK_NOT_LOGINED;

            showBuiltInLogin();
#endif
        }

        void showBuiltInLogin(){
            loginState = LoginState.E_SDK_LOGINED;  

            selectedServer.Visible(false);
            loadingSlider.Visible(false);
          
            if(GameConfig.GetPersonalConsentAccept())
                AddSubUI(UIType.InputAccountUI);
            else
                LuaScriptMgr.Instance.CallLuaFunction_Void_Object("water.showUIPersonalConsentView", this.gameObject.transform);

            //Ori
            //AddSubUI(UIType.InputAccountUI);

            //yonghuzhongxin_panel.Visible(false);
            //yonghuxieyi_panel.Visible(false);
            gonggao_panel.Visible(false);
            support_panel.Visible(false);
            del_panel.Visible(false);
            qiehuanzhanghao_panel.Visible(false);
            connectBtn.Visible(false);

            loadingSlider.Visible(false);

            selectedServer.Visible(false);
        }

        void showChannelLogin()
        {
            selectedServer.Visible(false);
            loadingSlider.Visible(false);

            //yonghuzhongxin_panel.Visible(false);
            //yonghuxieyi_panel.Visible(false);
            gonggao_panel.Visible(false);
            qiehuanzhanghao_panel.Visible(false);
            connectBtn.Visible(false);
            support_panel.Visible(false);
            del_panel.Visible(false);
            loadingSlider.Visible(false);

            selectedServer.Visible(false);
            
            //Water.ChannelManager.SendChannelEventAll("xian_shi_ping_tai_deng_lu_jie_mian");
#if !GM_SETTING_PACKAGE
            ChannelManager.login(this.onChannelLogined);
            ChannelManager.getTodayHolidayStatus();
#endif
        }

        void showChannelSwitchUser(){
            ChannelManager.switchUser(this.onChannelLogined);
        }

        void onChannelLogined(){
            connectBtn.Visible(false);
            ChannelManager.NewAFDot("sdk_login_end");
            //ChannelManager.SendChannelEventAll("ping_tai_deng_lu_jie_shu");
            if(!string.IsNullOrEmpty(ChannelManager.uid)){
                loginState = LoginState.E_SDK_LOGINED;
                sendChannelLogin();
            }
            else{
                onSDKLogout();
            }
        }

        void onDebugLogin(){
            connectBtn.Visible(false);
            if(!string.IsNullOrEmpty(ChannelManager.uid)){
                loginState = LoginState.E_SDK_LOGINED;
                sendChannelLogin();
            }
            else{
                onSDKLogout();
            }
        }

        void sendChannelLogin(){
            StartCoroutine(WaitSendChannelLogin());
        }

        //private void Start()
        //{
        //    XAsset.Scene.LoadAsync("Maps/City2/City2", false);
        //}
        IEnumerator WaitSendChannelLogin()
        {
            if (ChannelManager.isGuoNeiShuaBang())
            {
                int count = 0;
                while(!ChannelManager.holidayStatusReceived && count < 50)
                {
                    count++;
                    yield return new WaitForSeconds(0.1f);
                }
            }
           
            sendChannelLoginAct();
        }

        private void ReconnectLogin()
        {
            LoginSystem.Instance.SendLastLoginGetServerList(
                LoginSystem.Instance.connectModRecord,
                true,
                (bool succ_, int mid) => {
                    if (succ_)
                    {
                        onRecvServerList();
                    }
                    else
                    {
                        MessageBoxUtils.ShowMessgeBox(Config.CodeTextData.AUTOSTR("10001001") + "\t:(" + mid + ")");
                    }
                });
        }

        void sendChannelLoginAct()
        {
            ChannelServerBlockMod[] mod = ChannelManager.getNearServerBlockMod();
            if(loginState == LoginState.E_SDK_LOGINED){
                LoginSystem.Instance.CreateChannelLogin(ChannelManager.uid, ChannelManager.getChannelId());
                LoginSystem.Instance.last_login_time = -1;
                LoginSystem.Instance.isInitNormalServer = true;
                //Water.ChannelManager.SendChannelEventAll("huo_qu_fu_wu_qi_lie_biao_kai_shi");
                LoginSystem.Instance.SendLastLoginGetServerList(
                    mod,
                    true,
                    (bool succ_, int mid) =>{
                        if(succ_){
                            onRecvServerList();
                        }
                        else{
                            MessageBoxUtils.ShowMessgeBox(Config.CodeTextData.AUTOSTR("10001001") +"\t:("+ mid+")");
                        }
                });
            }
        }


        void onRecvServerList(){
            loginState = LoginState.E_ON_LOGIN_SERVER;

            RemoveAllSubUI();

            loadingSlider.Visible(false);
            selectedServer.Visible(true);

            //yonghuzhongxin_panel.Visible(false);
            //yonghuxieyi_panel.Visible(false);
            gonggao_panel.Visible(true);
            qiehuanzhanghao_panel.Visible(true);
            connectBtn.Visible(false);

            checkCustomServiceBtn();
#if UNITY_IPHONE
            del_panel.Visible(true);
#endif

            //Water.ChannelManager.SendChannelEventAll("xian_shi_fu_wu_qi_lie_biao");
            //Water.ChannelManager.NewAFDot("show_server_list");
        }

        //void onUpdateError(){
        //    onRecvServerList();
        //}

        // void onConnectingLoginServer()
        // {
        //     loginState = LoginState.E_CONNECTING_LOGIN_SERVER;
        //     loadingSlider.Visible(false);
        //     selectedServer.Visible(false);

        //     yonghuzhongxin_panel.Visible(false);
        //     yonghuxieyi_panel.Visible(false);
        //     gonggao_panel.Visible(false);
        //     qiehuanzhanghao_panel.Visible(false);
        //     support_panel.Visible(false);

        //     LoginSystem.Instance.SendLastLogin((bool succ_) =>
        //     {
        //         if (succ_)
        //         {
        //         }
        //         else
        //         {
        //             connectBtn.Visible(true);
        //         }
        //     });
        // }
        
        void onCompleteCallback(bool result, string messages)
        {
            Debug.LogError(" !!!!!! onCompleteCallback = " + result + " , " + messages );
        }

        void onConnectBtnClicked(){
            if(loginState == LoginState.E_CONNECTING_LOGIN_SERVER){
            }
            else if(loginState == LoginState.E_SDK_NOT_LOGINED){
#if USE_THIRD_PARTY_CHANNEL
                showChannelLogin();
#else
                showBuiltInLogin();
#endif
            }
        }

        void OnCustomServiceClick()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.ShowCustomServiceUIView");
        }

        void OnDelAccountClick()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.ShowDelAccountUIView");
        }

        void onSDKLogout(){
            LoginSystem.Instance.DisconnectServer();

            selectedServer.Visible(false);
            loadingSlider.Visible(false);

            //yonghuzhongxin_panel.Visible(false);
            //yonghuxieyi_panel.Visible(false);
            gonggao_panel.Visible(false);
            qiehuanzhanghao_panel.Visible(false);
            support_panel.Visible(false);
            del_panel.Visible(false);

            loadingSlider.Visible(false);

            selectedServer.Visible(false);

            loginState = LoginState.E_SDK_NOT_LOGINED;
            connectBtn.Visible(true);
        }

        void hideDescriptions(){
            //int channelId = ChannelManager.getChannelId();
            if(ChannelManager.isMengGuoHaiWai())
            {
                pizhunhao_txt.gameObject.SetActive(false);
                tishi_lbl.transform.parent.gameObject.SetActive(false);
            }

        }

		void checkCustomServiceBtn()
		{
            if (ChannelManager.isVa())
                support_panel.Visible(false);
            else
                support_panel.Visible(true);
		}
    }
}