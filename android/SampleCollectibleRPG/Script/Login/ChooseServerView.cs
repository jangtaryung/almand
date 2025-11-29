using UnityEngine.UI;
using TMPro;

using Water.Config;

using System.Collections.Generic;
using System;
using UnityEngine;


namespace Water
{
    public class ChooseServerView : BaseView
    {
        ScrollList _serverlist_panel;
		BaseList _grouplist_panel;
		TextMeshProUGUI xianshi_txt;
		TextMeshProUGUI lastzhuangtai_txt;

        private bool needShowEmptyTips = false;
		public const string LoginSelectGroupServ = "LoginSelectGroupServEvt";
		public const int SpecialZoneNameIdx = -999;
		List<object> zoneList;
        public override HoverViewType ViewHoverType
        {
            get
            {
                return HoverViewType.UnShow;
            }
        }

        public override bool IsShowBG
        {
            get
            {
                return false;
            }
        }


        public ChooseServerView()
            : base(UIType.ChooseServerUI)
        { }


        public override void InitUIView(object param_)
        {
            base.InitUIView(param_);

			xianshi_txt = getUIComponent<TextMeshProUGUI>("xianshi_txt");
			lastzhuangtai_txt = getUIComponent<TextMeshProUGUI> ("lastzhuangtai_txt");

            _serverlist_panel = getUIComponent<ScrollList>("pailieceng2_panel");
            _serverlist_panel.SetListInit(typeof(SeverListItem), _serverlist_panel.transform.parent.transform);
			_grouplist_panel = getUIComponent<BaseList> ("pailiecheng_panel");
			_grouplist_panel.SetListInit (typeof(GroupListItem));

			this.AddEventManagerListener(ChooseServerView.LoginSelectGroupServ, OnSelectGroupServ);
            this.AddEventManagerListener("SelectServerListCall", OnSelectGroupServ1);
	
			ServerInfo lastLoginSrv = LoginSystem.Instance.LastLoginServer;
			if (lastLoginSrv == null) 
			{
				xianshi_txt.text = " ";
				lastzhuangtai_txt.text = " ";
			}
			else
			{
				#if UNITY_EDITOR
					string serverName = string.Format (Config.CodeTextData.AUTOSTR("{0}服"), lastLoginSrv.Id.ToString ()) + " " + lastLoginSrv.Name;
					xianshi_txt.text = WaterGameConst.GetColorTextStrByColorInt (serverName, (int)lastLoginSrv.ColorTypee);
				#else
					xianshi_txt.text = WaterGameConst.GetColorTextStrByColorInt (lastLoginSrv.Name, (int)lastLoginSrv.ColorTypee);  
				#endif
				lastzhuangtai_txt.text = WaterGameConst.GetColorTextStrByColorInt (lastLoginSrv.StateName, (int)lastLoginSrv.ColorTypee);
				UIEventTriggerListener.Get(xianshi_txt.transform.parent.gameObject).onTap += onChooseClick;
			}

			zoneList = new List<object>(ChannelManager.ChannelServerBlockModArys);
			zoneList.Insert(0, CodeTextData.AUTOSTR("10010041"));
			_grouplist_panel.DataProvider = new DataProvider<object>(zoneList);

			
            PersistentEventManager.Instance.AddEventListener(LoginSystem.PingUpdate, RefreshBlockState);
        }

		void RefreshBlockState()
		{
			(_grouplist_panel.DataProvider as  DataProvider<object>).Refresh();
		}

        void onChooseClick(GameObject target_){
			ServerInfo lastLoginSrv = LoginSystem.Instance.LastLoginServer;
			if(lastLoginSrv != null){
				LoginSystem.Instance.setSelectedServerInfo(lastLoginSrv);

				this.Exit();
			}
        }

        void OnSelectGroupServ(object ob, WaterEventArgs args)
		{
			int uniqueId = (int)args.eventArgument;
			if(uniqueId == ChooseServerView.SpecialZoneNameIdx){  
				_serverlist_panel.DataProvider = new DataProvider<ServerInfo>(LoginSystem.Instance.getSrvListWithRole());
			}
			else{
				ChannelServerBlockMod[] mod = null;
				foreach(var blocks in ChannelManager.ChannelServerBlockModArys){
					if(blocks[0].mId == uniqueId){
						mod = blocks;
						break;
					}
				} 
				if(mod == null)
					return;

				LoginSystem.Instance.GetGroupServsDPByIndex(mod, (bool sucess, int uniqueId_) => {
					if(sucess){
						if(LoginSystem.Instance.ServerListDictionary.ContainsKey(uniqueId_)) {
                            _serverlist_panel.DataProvider = new DataProvider<ServerInfo>(LoginSystem.Instance.ServerListDictionary[uniqueId_]);
						}
                    }
                        else{
                        _serverlist_panel.DataProvider = new DataProvider<ServerInfo>();
					}
				}, needShowEmptyTips);
			}
            needShowEmptyTips = true;
        }

        void OnSelectGroupServ1(object ob, WaterEventArgs args)
        {
            int uniqueId = (int)args.eventArgument;

            if (needShowEmptyTips)
                LoginSystem.Instance.showEmptyServerListTips(uniqueId);

            if (LoginSystem.Instance.ServerListDictionary.ContainsKey(uniqueId))
            {
                _serverlist_panel.DataProvider = new DataProvider<ServerInfo>(LoginSystem.Instance.ServerListDictionary[uniqueId]);
            }
            else
            {
                _serverlist_panel.DataProvider = new DataProvider<ServerInfo>();
            }
        }

        public override void Dispose()
		{
            _serverlist_panel = null;
			this.RemoveEventManagerListener(ChooseServerView.LoginSelectGroupServ, OnSelectGroupServ);
            this.RemoveEventManagerListener("SelectServerListCall", OnSelectGroupServ1);
            PersistentEventManager.Instance.RemoveEventListener(LoginSystem.PingUpdate, RefreshBlockState);
            base.Dispose();
		}

		protected override void OnEnter(){
            needShowEmptyTips = false;
            Main.Instance.phoneDevice.registerBackHandler(this.onCloseClick);
		}
		protected override void OnExit(){
			Main.Instance.phoneDevice.unRegisterBackHandler(this.onCloseClick);
		}

        void onCloseClick(){
            Exit();
        }
    }

	public class GroupListItem: BaseListItem
	{	
		TextMeshProUGUI type1_txt;
		Image signal;
		Toggle toggle;
		int fillFrame = 0;
		static Toggle _lastTog;
		static bool _skipCheck = false;

        long lastGetDataTime = 0;

		ChannelServerBlockMod[] myVo
		{
			get
			{
				return data as ChannelServerBlockMod[];
			}
		}

		bool isChannelServerBlock{
			get{
				return data is ChannelServerBlockMod[];
			}
		}

		public override void InitUIView(object param_ = null)
		{
			base.InitUIView(param_);
			type1_txt = getUIComponent<TextMeshProUGUI>("type1_txt");
			signal = getUIComponent<Image>("img_signal");
			toggle = this.gameObject.GetComponent<Toggle> ();
			toggle.onValueChanged.AddListener (OnToggleValueChanged);
		}

		void OnToggleValueChanged(bool isChange_)
		{
			if(_skipCheck) return;

			bool skipAlert = false;
			//var channelId = ChannelManager.getChannelId();
			// if(channelId == ChannelManager.PlatIdNFL || channelId == ChannelManager.PlatIdNFL2)
			// 	skipAlert = true;
			
			if(this.toggle.isOn == true)
			{
				if(isChannelServerBlock){
					ChannelServerBlockMod[] recommend = ChannelManager.getServerBlock();
					if(recommend != myVo && UnityEngine.Time.frameCount - this.fillFrame > 10){
						if(skipAlert)
                        {
                            DispacthManagerEvent(ChooseServerView.LoginSelectGroupServ, myVo[0].mId);
							_lastTog = this.toggle;
						}
						else{
							Water.MessageBoxUtils.ShowOKCancleMessageBox(Config.CodeTextData.AUTOSTR("10010667"), 
								(PopWinVO result_)=>{
									if(result_.VoHandleBackType == PopWinHandleBackType.Yes){
										DispacthManagerEvent(ChooseServerView.LoginSelectGroupServ,myVo[0].mId);
										_lastTog = this.toggle;
									}
									else{
										_skipCheck = true;
										this.toggle.isOn = false;
										_lastTog.isOn = true;
										_skipCheck = false;
									}
								});
						}
					}
					else
                    {
                        if (Water.Game.Util.WaterDateTime.Now.seconds - lastGetDataTime > 2 * 60)
                        {
                            DispacthManagerEvent(ChooseServerView.LoginSelectGroupServ,myVo[0].mId);
						    _lastTog = this.toggle;
                            lastGetDataTime = Water.Game.Util.WaterDateTime.Now.seconds;
                        }
                        else
                        {
                            DispacthManagerEvent("SelectServerListCall", myVo[0].mId);
                            _lastTog = this.toggle;
                        }
                    }
				}
				else{
                    if ( _lastTog != this.toggle || Water.Game.Util.WaterDateTime.Now.seconds - lastGetDataTime > 2*60)
                    {
					    DispacthManagerEvent(ChooseServerView.LoginSelectGroupServ, ChooseServerView.SpecialZoneNameIdx);
					    _lastTog = this.toggle;
                        lastGetDataTime = Water.Game.Util.WaterDateTime.Now.seconds;
                    }
                    else
                    {
                        // DispacthManagerEvent("SelectServerListCall", ChooseServerView.SpecialZoneNameIdx);
                        _lastTog = this.toggle;
                    }
				}

			}
		}

		protected override void FillData (object vo)
		{
			base.FillData (vo);
            this.lastGetDataTime = 0;
            fillFrame = UnityEngine.Time.frameCount;		
			if(isChannelServerBlock){
				string info = myVo[0].name;
				ChannelServerBlockMod[] recommend = ChannelManager.getServerBlock();
				
				ChannelServerBlockMod[] mod = ChannelManager.getNearServerBlockMod();
				this.toggle.isOn = mod == myVo;
#if !UNITY_EDITOR
				if(ChannelManager.isMengGuoHaiWai() && myVo[0].mId == 2)
				{
					info = "Global";
				}
#endif
				//if (ChannelManager.IsPingType)
				//{
				//	signal.gameObject.SetActive(true);
    //            	Game.NetQualityParam par = LoginSystem.Instance.GetPing(myVo[0].mId);
				//	string url = LoginSystem.Instance.GetSignUrlByPing(par);
				//	XAsset.Assets.LoadWithOwner<Sprite>(url, delegate(Sprite sp){
				//		if (sp != null)
    //            	    	signal.sprite = sp;
				//	},this.gameObject);
				//	// if (par != null)
				// 	// {
				//	//  info = info + " " + par.ping;
				//	// }
				//}
				//else
				//{
				signal.gameObject.SetActive(false);
				if(recommend == myVo)
				{
					info = info + "(" + Config.CodeTextData.AUTOSTR("10010608") + ")";
				}
				//}
				type1_txt.text = info;
				
				if(mod == myVo){
					_lastTog = this.toggle;
					// DispacthManagerEvent (ChooseServerView.LoginSelectGroupServ,myVo[0].mId);
				}
			}
			else{
				type1_txt.text = data as string;
			}
		}

		public override void Dispose()
		{
			_lastTog = null;
			toggle.onValueChanged.RemoveListener (OnToggleValueChanged);
		}
	}

    public class SeverListItem : BaseListItem
    {
        TextMeshProUGUI name_txt;
		TextMeshProUGUI zhuangtai_txt;
        Button chooseBtn;
        TextMeshProUGUI lv_txt;
		Image new_img;

        ServerInfo myVo
        {
            get
            {
                return data as ServerInfo;
            }
        }

        public override void InitUIView(object param_ = null)
        {
            base.InitUIView(param_);

            name_txt = getUIComponent<TextMeshProUGUI>("name_txt");
            lv_txt = getUIComponent<TextMeshProUGUI>("lv_txt");
			zhuangtai_txt = getUIComponent<TextMeshProUGUI> ("zhuangtai_txt");
			new_img = getUIComponent<Image> ("new_img");
            chooseBtn = GetComponent<Button>();
            chooseBtn.onClick.AddListener(onChooseClick);
        }

        protected override void FillData(object vo = null)
        {
            base.FillData(vo);

			new_img.Visible (false);
			lv_txt.Visible(true);

			string serverName = "";
			string playerlv = "";

            if (data == null) return;

            #if UNITY_EDITOR
				serverName = string.Format (Config.CodeTextData.AUTOSTR("{0}"), myVo.Id.ToString ()) + " " + myVo.Name;
            #else
				serverName = myVo.Name;  
            #endif

			if (myVo.Level > 0){
				playerlv = string.Format(Config.CodeTextData.AUTOSTR("{0}"), myVo.Level.ToString());
			}
            else
                lv_txt.Visible(false);

			if(myVo.State == ServerState.E_NEW_SERVER)
				new_img.Visible (true);

			SetInfo(serverName,playerlv,myVo.StateName,myVo.ColorTypee);		
        }

		void SetInfo(string serverName_,string playerlv_,string stateName_,ColorType colorType_)
		{
			zhuangtai_txt.text = WaterGameConst.GetColorTextStrByColorInt(stateName_,(int)colorType_);
			name_txt.text = WaterGameConst.GetColorTextStrByColorInt(serverName_,(int)colorType_);
			if (lv_txt.gameObject.activeInHierarchy)
				lv_txt.text = playerlv_;
		}

        public override void Dispose(){
            base.Dispose();

            name_txt = null;
            chooseBtn = null;
        }
		
        void onChooseClick(){

//          modify by Wxx,2017.01.18		
//			if (!myVo.CanLogin) 
//			{
//				GameShowNoticeUtil.ShowMsgByCodeID (10008);
//				return;
//			}
//			if(myVo.State == ServerState.E_IN_MAINTAIN && !LoginSystem.Instance.isGMAuth()){
//				GameShowNoticeUtil.ShowMsgByCodeID (10008);
//				return;
//			}

            // LoginSystem.Instance.SelectedServerId = myVo.Id;
			LoginSystem.Instance.setSelectedServerInfo(myVo);

            ChooseServerView view = this.gameObject.GetComponentInParent<ChooseServerView>();
            view.Exit();
        }
    }

}
