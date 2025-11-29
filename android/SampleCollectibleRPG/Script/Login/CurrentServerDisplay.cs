using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using System;

namespace Water{
	public class CurrentServerDisplay : UIBaseComponent {
        Button _login_btn;
		Button changeServerBtn;
        TextMeshProUGUI _acountLabel;
		TextMeshProUGUI _newzhuangtaiLabel;

		public override void InitUIView(object param_){
            _login_btn = getUIComponent<Button>("LoginGo_btn");
			changeServerBtn = getUIComponent<Button>("New_btn");
            _acountLabel = getUIComponent<TextMeshProUGUI>("New_txt");
			_newzhuangtaiLabel = getUIComponent<TextMeshProUGUI> ("newzhuangtai_txt");
            
            _login_btn.onClick.AddListener(onLoginClick);
			changeServerBtn.onClick.AddListener(onChooseServer);
            _login_btn.Visible(true);

			base.InitUIView(param_);
		}

		protected override void OnEnter(){
			base.OnEnter();

			onSelectedServerChange();
			PersistentEventManager.Instance.AddEventListener(LoginSystem.LoginSelectedServerChangeEvt, onSelectedServerChange);
		}

		protected override void OnExit(){
			base.OnExit();

			PersistentEventManager.Instance.RemoveEventListener(LoginSystem.LoginSelectedServerChangeEvt, onSelectedServerChange);
		}

		public override void Dispose(){
			base.Dispose();			
		}

		void onSelectedServerChange(){
            ServerInfo selectedSrv = LoginSystem.Instance.SelectedServer;

			string serverName = "";

            if (selectedSrv != null)
            {
            	#if UNITY_EDITOR
					serverName =  string.Format (Config.CodeTextData.AUTOSTR("{0}"), selectedSrv.Id.ToString ()) + " " + selectedSrv.Name;
            	#else
					serverName =  selectedSrv.Name;  
            	#endif
            }
            else
            {
				serverName = Water.Config.CodeTextData.AUTOSTR("10010058");
            }				

			_acountLabel.text = WaterGameConst.GetColorTextStrByColorInt(serverName,(int)ColorType.ORANGE);
			if(selectedSrv != null)
				_newzhuangtaiLabel.text = WaterGameConst.GetColorTextStrByColorInt(selectedSrv.StateName, (int)selectedSrv.ColorTypee);
			else
				_newzhuangtaiLabel.text = "";
		}
        
		void onChooseServer(){
			SingletonFactory<UIManager>.Instance.OpenUI(UIType.ChooseServerUI);
		}

		void onLoginClick()
        {
            //Water.ChannelManager.SendChannelEventAll("dian_ji_kai_shi_you_xi");
            Water.ChannelManager.NewAFDot("click_start_game_btn");
			ServerInfo selected = LoginSystem.Instance.SelectedServer;
			if (selected == null) 
			{
                SingletonFactory<UIManager>.Instance.OpenUI (UIType.ChooseServerUI);
				return;
			} 
			else 
			{
				Debug.LogError(" !!!!! onClick = " + selected.CanLogin);

                if (!selected.CanLogin) 
				{
					GameShowNoticeUtil.ShowMsgByCodeID (10008);
					return;
				}

                if (selected.State == ServerState.E_IN_MAINTAIN && !LoginSystem.Instance.isGMAuth()) 
				{
					GameShowNoticeUtil.ShowMsgByCodeID (10008);
					return;
				}
				
			}
			
			LoginSystem.Instance.SendChooseServer();
        }

	}
}
