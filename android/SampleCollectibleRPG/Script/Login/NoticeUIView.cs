using UnityEngine.UI;
using TMPro;

using System;
using System.Collections.Generic;
using UnityEngine;


namespace Water{
	public class NoticeUIView : BaseView {

        public const string NoticeChange = "NoticeChange";
		public override bool IsShowBG
        {
            get
            {
                return false;
            }
        }

		Button	closeBtn;
		TextMeshProUGUI noticeTxt;
        // Transform item_panel;
        BaseList button_panel;
        Image jiantou1_img;
        List<NoticeListInfo> noticeInfo = new List<NoticeListInfo>();  
		public NoticeUIView(): base(UIType.NoticeUI){}
        private bool isFirstToggle = true;

        private void OnCutScenePlayEnd(string tname)
        {
            foreach(var v in noticeInfo)
            {
                if(v.Tname == tname)
                    noticeTxt.text = v.Content;
            }
        }

     
		public override void InitUIView(object param_){
			base.InitUIView(param_);

			closeBtn  = getUIComponent<WaterButton>("close_btn");
			noticeTxt = getUIComponent<TextMeshProUGUI> ("gonggao_txt");
            jiantou1_img = getUIComponent<Image>("jiantou1_img");

            button_panel = getUIComponent<BaseList>("button_neirong_panel");
            button_panel.SetListInit(typeof(NoticeListItem));

			closeBtn.onClick.AddListener(onCloseClicked);
            UITextLinkTrigger.Get(noticeTxt.gameObject).SetLinkEnable(true);

			List<NoticeInfo> noticeInfoList = param_ as List<NoticeInfo>;

            string nowShowLan = "en";
            if (noticeInfoList != null)
            {
                for(int i = 0; i < noticeInfoList.Count; i++)
                {
                    string[] noticeStr = noticeInfoList[i].Context.Split(new string[]{"@@"},StringSplitOptions.RemoveEmptyEntries);
                    nowShowLan = "en";
                    for(int j = 0; j < noticeStr.Length; j+=2)
                    {
                        if(noticeStr[j] == Water.GameConfig.Language)
                        {
                            nowShowLan = Water.GameConfig.Language;
                            break;
                        }
                    }

                    for (int j = 0 ; j < noticeStr.Length; j += 2 )
                    {                      
                        if (noticeStr[j] == nowShowLan)
                        {
                            noticeInfo.Add(new NoticeListInfo(noticeInfoList[i].Title,noticeStr[j],noticeStr[j+1]));
                            
                            if(isFirstToggle)
                            {
                                noticeTxt.text = noticeStr[j+1];
                                isFirstToggle = false;
                                break;
                            }
                        }
                    }
                }
                button_panel.DataProvider = new DataProvider<NoticeListInfo>(noticeInfo);
                jiantou1_img.Visible(true);

                SingletonFactory<EventManager>.Instance.AddEventListener(NoticeUIView.NoticeChange, OnNoticeChange);
            }
            else
            {
                string txt = param_ as string;
                if(string.IsNullOrEmpty(txt))
                {
                    noticeTxt.text = "";
                }
                else
                {
                    string[] noticeStr = txt.Split(new string[]{"@@"},StringSplitOptions.RemoveEmptyEntries); 

                    nowShowLan = "en";
                    for(int j = 0; j < noticeStr.Length; j+=2)
                    {
                        if(noticeStr[j] == Water.GameConfig.Language)
                        {
                            nowShowLan = Water.GameConfig.Language;
                            break;
                        }
                    }
                    for(int j = 0 ; j < noticeStr.Length; j += 2 )
                    {                      
                        if (noticeStr[j] == nowShowLan) 
                        {
                            noticeTxt.text = noticeStr[j+1];
                            break;
                        }
                    }
                }
            }   
            
		}


        private void OnNoticeChange(object sender, WaterEventArgs args)
        {
            NoticeListInfo info = (NoticeListInfo)sender;
            foreach(var v in noticeInfo)
            {
                if(v.Tname == info.Tname)
                    noticeTxt.text = v.Content;
            }
        }
   
        protected void CloseWhenAble()
        {
            if (UIEffectUtil.isUIClickable(closeBtn.transform as UnityEngine.RectTransform))
                closeBtn.onClick.Invoke();
        }
        protected override void OnEnter()
        {
            base.OnEnter();
            Main.Instance.phoneDevice.registerBackHandler(CloseWhenAble);
        }
        protected override void OnExit()
        {
            Main.Instance.phoneDevice.unRegisterBackHandler(CloseWhenAble);
            base.OnExit();
        }
		public override void Dispose(){
			base.Dispose();
            SingletonFactory<EventManager>.Instance.RemoveEventListener(NoticeUIView.NoticeChange, OnNoticeChange);
            button_panel = null;
			closeBtn = null;
		}

		void onCloseClicked(){
			Exit();
		}
	}


    public class NoticeListInfo
    {
        private string tname;
        public string Tname{get{return tname;}set{tname = value;}}
        private string lang;
        public string Lang{get{return lang;}set{lang = value;}}
        private string content;
        public string Content{get{return content;}set{content = value;}}

        public NoticeListInfo(string t,string l,string c)
        { 
            Tname = t;
            Lang = l;
            Content = c;
        }
    }

    public class NoticeListItem : BaseListItem
    {
        TextMeshProUGUI title_txt;
        Transform reddot_panel;
        WaterToggle toggle_btn;

        NoticeListInfo myvo
        {
            get{
                return data as NoticeListInfo;
            }
        }
       public override void InitUIView(object param_ = null)
       {
            base.InitUIView(param_);
            title_txt = getUIComponent<TextMeshProUGUI>("activity_txt");
            reddot_panel = getUIComponent<RectTransform>("reddot_panel");
            toggle_btn = getUIComponent<WaterToggle>("item_tog");
            toggle_btn.onValueChanged.AddListener((bool value) => OnToggleClick(toggle_btn, value));
       }

        public void OnToggleClick(Toggle toggle, bool value)
        {    
            SingletonFactory<EventManager>.Instance.DispatchEvent(NoticeUIView.NoticeChange, myvo);
        }
       protected override void FillData(object vo = null)
       {
           base.FillData(vo);

           title_txt.Visible(true);
        // reddot_panel.Visible(false);
           title_txt.text = myvo.Tname;
       }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
