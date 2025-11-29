using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

using Water.Config;
using LuaInterface;
using TMPro;
using Game.Util;
using System.Collections;
using Water.Game.Util;

namespace Water
{

    public class MainUIView : BaseView
    {
        private bool Spring_On = true;

        private bool Activity_Spring_On = true;

        private bool AnimationUnDefault_qitakuozhan = true;

        private bool AnimationUnDefault_beibao_zishiyin_panel = true;

        private bool AnimationUnDefaultpeiyang = true;

        private bool AnimationUnDefault_zhaohuan = true;

        private bool AnimationUnDefault_activity = true;

        private bool AnimationMoLong_activity = true;

        public const string StartPathFindingAnimation = "MainUIView.StartPathFindingAnimation";

        public const string EndPathFindingAnimation = "MainUIView.EndPathFindingAnimation";

        public const string play_btn_lucky = "Challenge_Lucky_Chanage";


        

        Animator RightMove_panel;
        Animator BottomMoveFunc_panel;
        Animator qitakuozhan_panel;
        Animator peiyang_panel;    
        Animator beibao_panel;      
        Animator zhaohuan_panel;    
        Animator huodong_panel;     
        Animator molong_panel;      
        RoleInfoCom roleinfo_com;
        Button spring_btn;
        WaterButton huodongshoufang_btn;
        Animator huodongshoufang_btn_anim;
        Transform huodongshoufang_hot_panel;
        MainUIAnimtionCallBack huodongshoufang_animtion_callback;
        Button chouka_2_btn;
        Button signin_btn;
        Button email_btn;
        Button recommended_btn;
        // Button notice_btn;
        Button zhuzhan_btn; 
        Button boss_btn;
        
        Button huodong_btn;
        Button molong_btn;

        Button shop_btn;

        Button recharge_btn;
        
        TimeCheckVo timecheckvo;
        TimeCheckVo btnTipTimer;

        Button sevenDay_btn;
        Button sevenDay1_btn;
        // Button box_btn;

        Button hero_btn;

        Button equip_btn;

        Button hallows_btn;

        WaterButton heroComposeBtn;

        Transform bag_hongdian_panel;

        Button bag_btn;

        Button team_btn;

        Button Forge_btn;

        Button union_btn;

        Button daoju_btn;

        Button shoucang_btn;

        Button miracle_btn;

        TextMeshProUGUI time_lbl;
        

        public class ActivityButton
        {
            public Button btn;
            public TextMeshProUGUI buttonName;
            public Image buttonimg;
            public TextMeshProUGUI timeTxt;
            public int Tag;     
        }

        List<MainUIView.ActivityButton> sevenday_btns = new List<MainUIView.ActivityButton>();
        List<MainUIView.ActivityButton> firstcharge_btns = new List<MainUIView.ActivityButton>();     

        Transform chat_panel;

        PlayMakerFSM _main_PlayMakerFSM;

        PlayMakerFSM _chat_PlayMakerFSM;


        PlayMakerFSM _Pathfinding_panel_PlayMakerFSM;

        Transform exp_bar;

     

        public Button task_btn;

        Button chat_btn; 

        Button peiyang_btn;

        Transform chibang_panel;
        Transform shizhuang_panel;
        Transform chenghao_panel;
        Transform touxiang_panel;
        Transform horse_panel;

        Button wing_btn;
        Button horse_btn;

        Button shizhuang_btn;

        Button chenghao_btn;
        Button touxiang_btn;

       
        Button copy_btn;

        Button experience_btn;

        Button playinfo_btn;
        Transform playinfo_btn_img;

        Button other_btn;


        Button yewai_btn;

        Button paihang_btn; 
        Button Shezhi_btn;
        Button Vip_btn;
        Image vip_panel;
        Button Dianjin_btn;
        Button Tujian_btn;
        Button Bianqiang_btn;
        Button haoyou_btn;
        Button AICostomer_btn;


        PlayMakerFSM position_panel;


        Button tree_btn; 
        Transform tree_btn_harvest;
        TextMeshProUGUI harvest_txt;
        int tip_duration = 1;

        Button shenmoZhanchang_btn;
        Button shenmodouble_btn;
        Button shenmolongzu_btn;

        public override  HoverViewType ViewHoverType
        {
            get
            { 
                return HoverViewType.MainUIView;
                  
            }
        }

        public MainUIView()
            : base(UIType.MainUI)
        {


        }

        float _lastTimeLblUpdate = 0;
        // long overtime = 0;
        void Update()
        { 
            if (Guide.IsInForceGuiding())
            {
                if(!Spring_On)
                {
                    SpringUp();
                }
                if(!Activity_Spring_On){
                    huodongshoufang_btn_onClick_unfold();
                }

                
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.closeNoviceUIView");
            }else{
                //if(Role.Instance.baseInfo.isFirstEnterGame) { 
                    //if(ModuleUnlock.IsModuleUnlocked(ModuleId.ActivityNovice, false)){
                        //Role.Instance.baseInfo.isFirstEnterGame = false;
                        //LuaScriptMgr.Instance.CallLuaFunction_Void("water.showNoviceUIView");
                    //}
                //}
            }

            if(Time.realtimeSinceStartup - _lastTimeLblUpdate > 5){
                // overtime = WaterDateTime.Now.seconds - Main.Instance.lastUpdateDateTime;
                // if (Math.Abs(overtime) >= 60)
                //     WaterDateTime.RealDiff +=(long)(5 - overtime);

                // WaterDateTime t = WaterDateTime.Now.Reset(WaterDateTime.Now.seconds + WaterDateTime.RealDiff);
                WaterDateTime t = WaterDateTime.Now;
                time_lbl.text = string.Format("{0:d2}:{1:d2} {2}", t.Hour, t.Minute, WaterDateTime.zoneName);

                // Main.Instance.lastUpdateDateTime = WaterDateTime.Now.seconds;
                _lastTimeLblUpdate = Time.realtimeSinceStartup;
            }
		}
        protected override void AwakeInit(object param_ = null)
        {

           
            base.AwakeInit();

            time_lbl = getUIComponent<TextMeshProUGUI>("timez_txt");
            _main_PlayMakerFSM = this.GetComponent<PlayMakerFSM>();
            position_panel = getUIComponent<PlayMakerFSM>("position_panel");

            RightMove_panel = getUIComponent<Animator>("RightMove_panel"); 
            BottomMoveFunc_panel = getUIComponent<Animator>("BottomMoveFunc_panel");

            roleinfo_com = getUIComponent<RoleInfoCom>("roleinfo_com");

            spring_btn = getUIComponent<Button>("Spring_btn");
            huodongshoufang_btn = getUIComponent<WaterButton>("huodongshoufang_btn");
            huodongshoufang_btn_anim = getUIComponent<Animator>("huodongshoufang_btn");
            huodongshoufang_hot_panel = getUIComponentByParent<Transform>("reddot_panel",huodongshoufang_btn.transform);

            huodongshoufang_animtion_callback = getUIComponent<MainUIAnimtionCallBack>("huodongshoufang_btn");
            huodongshoufang_animtion_callback.setShadeTransform(getUIComponent<Transform>("zhedangdianji_panel"));

            gm_btn = getUIComponent<Button>("gm_btn");
            gm_btn.Visible(Role.Instance.baseInfo.gmAuth > 0 || Application.isEditor);

            qitakuozhan_panel = getUIComponent<Animator>("qitakuozhan_panel");
            zhaohuan_panel = getUIComponent<Animator>("zhaohuan_panel");
            huodong_panel = getUIComponent<Animator>("huodong_panel");
            molong_panel = getUIComponent<Animator>("molong_panel");


            shenmoZhanchang_btn = getUIComponent<WaterButton>("shenmozhanchang_btn");
            shenmodouble_btn = getUIComponent<WaterButton>("shenmodouble_btn");
            ScriptBaseComponent shenmoZhanchangCom = shenmodouble_btn.gameObject.AddComponent<ScriptBaseComponent>();
            shenmoZhanchangCom.ctorFunc = LuaScriptMgr.Instance.GetLuaFunction("water.CreateShenmoZhanchangIcon");
            shenmoZhanchangCom.InitUIView(null);

            var juntuanzhan_btn = getUIComponent<WaterButton>("juntuanzhan_btn");
            ScriptBaseComponent juntuanzhan_btnCon = juntuanzhan_btn.gameObject.AddComponent<ScriptBaseComponent>();
            juntuanzhan_btnCon.ctorFunc = LuaScriptMgr.Instance.GetLuaFunction("water.createUnionMainIcon");
            juntuanzhan_btnCon.InitUIView(null);


            shenmolongzu_btn = getUIComponent<WaterButton>("longzuruqin_btn");
            ScriptBaseComponent shenmolongzuCom = shenmolongzu_btn.gameObject.AddComponent<ScriptBaseComponent>();
            shenmolongzuCom.ctorFunc = LuaScriptMgr.Instance.GetLuaFunction("water.CreateShenmoLongzuIcon");
            shenmolongzuCom.InitUIView(null);

            GameObject firstBtn = getUIComponent<Button>("fristRecharge_btn").gameObject;
            firstBtn.SetActive(false);
            int _count = LuaScriptMgr.Instance.CallLuaFunction_Int("water.role.activity.getFirstChargeCount"); 
            for(int i = 0; i < _count; i++)
            {
                MainUIView.ActivityButton _btn = new MainUIView.ActivityButton();
                GameObject _btnObj = Instantiate(firstBtn) as GameObject;
                _btnObj.transform.SetParent(firstBtn.transform.parent,false);
                _btn.btn = _btnObj.GetComponent<Button>();
                _btn.timeTxt = _btn.btn.transform.Find("time_txt").GetComponent<TextMeshProUGUI>();
                _btn.buttonimg = _btn.btn.transform.Find("fristRecharge_img").GetComponent<Image>();
                _btn.buttonName = _btn.btn.transform.Find("fristRecharge_lbl").GetComponent<TextMeshProUGUI>();   
                firstcharge_btns.Add(_btn);
                registerTipBtns(EMainBtnTiptype.FirstCharge, _btn.btn);
            }

            signin_btn = getUIComponent<Button>("signin_btn");
            email_btn = getUIComponent<Button>("email_btn");
            zhuzhan_btn = getUIComponent<Button>("zhuzhan_btn");
            recommended_btn = getUIComponent<Button>("recommended_btn");
            huodong_btn = getUIComponent<Button>("huodong_btn");
            molong_btn = getUIComponent<Button>("molongbaozang_btn");
            shop_btn = getUIComponent<Button>("shop_btn");
            boss_btn = getUIComponent<Button>("boss_btn");
            recharge_btn = getUIComponent<Button>("recharge_btn"); 
            sevenDay_btn = getUIComponent<Button>("seven_btn");

            sevenDay1_btn = getUIComponent<Button>("seven1_btn");
            GameObject sevendayBtn = getUIComponent<Button>("sevendaysenior_btn").gameObject;
            sevendayBtn.SetActive(false);
            int _charegecount = LuaScriptMgr.Instance.CallLuaFunction_Int("water.role.qiRiZunXiang.getCount"); 
            for(int i = 0; i < _charegecount; i++)
            {
                MainUIView.ActivityButton _btn = new MainUIView.ActivityButton();
                GameObject _btnObj = Instantiate(sevendayBtn) as GameObject;
                _btnObj.transform.SetParent(sevendayBtn.transform.parent,false);
                _btn.btn = _btnObj.GetComponent<Button>();
                _btn.buttonimg = _btn.btn.transform.Find("sevendaysenior_img").GetComponent<Image>();
                _btn.buttonName = _btn.btn.transform.Find("sevendaysenior_txt").GetComponent<TextMeshProUGUI>();   
                sevenday_btns.Add(_btn);
                registerTipBtns(EMainBtnTiptype.SevenDay, _btn.btn);                
            }
            Animator ani =  sevenDay_btn.gameObject.GetComponentInChildren<Animator>();
            if (ani != null)
            {
                timecheckvo = Game.Util.TimeUtil.Instance.CreateRealTimeCheck(3, () => {
                    if (ani != null && ani.gameObject.activeInHierarchy)
                        ani.Play("seven");
                }, true);
                timecheckvo.Start();
            }

            hero_btn = getUIComponent<Button>("hero_btn");
            equip_btn = getUIComponent<Button>("equip_btn");
            hallows_btn = getUIComponent<Button>("hallows_btn");
            heroComposeBtn = getUIComponent<WaterButton>("hero_compose_btn");
            bag_btn = getUIComponent<Button>("bag_btn");
            team_btn = getUIComponent<Button>("team_btn");
            Forge_btn = getUIComponent<Button>("Forge_btn");
            union_btn = getUIComponent<Button>("union_btn");
            daoju_btn = getUIComponent<Button>("daoju_btn");
            beibao_panel = getUIComponent<Animator>("beibao_panel");
            bag_hongdian_panel = getUIComponent<Transform>("bag_hongdian_panel");
            shoucang_btn = getUIComponent<Button>("shoucang_btn");
            miracle_btn = getUIComponent<Button>("miracle_btn");

            exp_bar = getUIComponent<Transform>("exp_panel");
            LuaScriptMgr.Instance.CallLuaFunction_Void_Object("water.AddRoleExpBarToTransform", exp_bar.transform);
            #endregion


            task_btn = getUIComponent<Button>("task_btn");
            registerTipBtns(EMainBtnTiptype.Task, task_btn);            
            chat_panel = getUIComponent<Transform>("chat_panel");
            _chat_PlayMakerFSM = chat_panel.GetComponent<PlayMakerFSM>(); 

            chat_btn = getUIComponent<Button>("chat_btn");
            peiyang_btn = getUIComponent<Button>("peiyang_btn");
            chibang_panel = getUIComponent<Transform>("chibang_btn");
            shizhuang_panel = getUIComponent<Transform>("shizhuang_btn");
            chenghao_panel = getUIComponent<Transform>("chenghao_btn");  
            touxiang_panel = getUIComponent<Transform>("touxiang_btn");
            horse_panel = getUIComponent<Transform>("zuoqi_btn");
            peiyang_panel = getUIComponent<Animator>("peiyang_panel");

            wing_btn = getUIComponent<Button>("chibang_btn");
            horse_btn = getUIComponent<Button>("zuoqi_btn");

            shizhuang_btn = getUIComponent<Button>("shizhuang_btn");

            chenghao_btn = getUIComponent<Button>("chenghao_btn");

            touxiang_btn = getUIComponent<Button>("touxiang_btn");  

			//ZhuXian_btn  = getUIComponent<Button>("ZhuXian_btn");

            //king_way_btn = getUIComponent<Button>("wangzhezhilu_btn");


            if (Role.isTempClosed())
            { 
                recharge_btn.Visible(false);
            }

            copy_btn = getUIComponent<Button>("copy_btn");
            experience_btn = getUIComponent<Button>("experience_btn");
            playinfo_btn = getUIComponent<Button>("playinfo_btn");
            playinfo_btn_img = getUIComponentByParent<Transform>("discount_img",playinfo_btn.transform);
            other_btn = getUIComponent<Button>("other_btn");
            yewai_btn = getUIComponent<Button>("yewai_btn");

            paihang_btn = getUIComponent<Button>("paihang_btn");
            Shezhi_btn = getUIComponent<Button>("Shezhi_btn");
            Vip_btn = getUIComponent<Button>("Vip_btn");
            vip_panel = getUIComponent<Image>("vip_btn");
            Dianjin_btn = getUIComponent<Button>("Dianjin_btn");
            Tujian_btn = getUIComponent<Button>("Tujian_btn");
            Bianqiang_btn = getUIComponent<Button>("Bianqiang_btn");
            haoyou_btn = getUIComponent<Button>("haoyou_btn");
            AICostomer_btn =getUIComponent<Button>("AICostomer_btn");

            tree_btn = getUIComponent<Button>("tree_btn");
            tree_btn_harvest = tree_btn.transform.Find("keshouhuo_lbl");
            harvest_txt = getUIComponent<TextMeshProUGUI>("keshouhuo_lbl");

            Dictionary<uint,MainbubbleData> bubble = MainbubbleData.Load();
            foreach (var v in bubble)
            {
                tip_duration = v.Value.Time;
                break;
            }

            onRoleInfoUpdate();

            this.AddEventManagerListener(RoleBaseInfoMediator.RoleBaseInfo_Update, onRoleInfoUpdate);
            Role.Instance.hero.QualityUpComplete += this.onRoleInfoUpdate;
            this.AddEventManagerListener(RoleBaseInfoMediator.RoleBaseInfo_TeamGS, onRoleTeamGSUpdate);
            this.AddEventManagerListener("ChatViewClosed", () =>
            {
				if( beforeClickChatBtnSpringIsOn == true && Spring_On == false )
                	SpringUp();
                chat_panel.Visible(true);
            });
            this.AddEventManagerListener(BaseScene.EvtShowSceneStart, onShowSceneStart);

            #region init chat panel
            ScriptBaseComponent baseComp = chat_panel.gameObject.AddComponent<ScriptBaseComponent>();
            baseComp.ctorFunc = LuaScriptMgr.Instance.GetLuaFunction("water.createChatLeftBottomView");
            baseComp.InitUIView(null);
            #endregion

            #region init kingWay
            //ScriptBaseComponent kingWayCom = king_way_btn.gameObject.AddComponent<ScriptBaseComponent>();
            //kingWayCom.ctorFunc = LuaScriptMgr.Instance.GetLuaFunction("water.CreateKingWayMainCityIcon");
            //kingWayCom.InitUIView(null);
            #endregion

            #region init chouka
            chouka_2_btn = getUIComponent<Button>("chouka2_btn");
            registerTipBtns(EMainBtnTiptype.BaoXiang, chouka_2_btn);
            #endregion

            _Pathfinding_panel_PlayMakerFSM = getUIComponent<PlayMakerFSM>("Pathfinding_panel");

            requestHideJoystick(false);

            this.AddEventManagerListener(EndPathFindingAnimation, PathFindEnd);
            this.AddEventManagerListener(StartPathFindingAnimation, PathFindStart);
            RefreshSevenDayBtn();
            // RefreshRewardBox();
            // this.AddEventManagerListener(GameEventNames.GameRewardBoxUnLock, RefreshRewardBox);

            this.AddEventManagerListener(GameEventNames.EvtActivityUpdate, RefreshSevenDayBtn);
			RefreshFirstChargePanel ();
			this.AddEventManagerListener ("EvtFirstChargeUpdateToMainUI", RefreshFirstChargePanel);

            Transform buffTrans = getUIComponent<Transform>("buff_panel", null);
            LuaFunction showBuffFunc = LuaScriptMgr.Instance.GetLuaFunction("water.showMainCityBuffListView");
            ScriptBaseComponent.createComponent(buffTrans.gameObject, showBuffFunc, null);

            Transform vipTemp = getUIComponent<Transform>("tiyantequan_btn", null);
            LuaFunction showVipTemp = LuaScriptMgr.Instance.GetLuaFunction("water.showMainCityVipTempIcon");
            ScriptBaseComponent.createComponent(vipTemp.gameObject, showVipTemp, null);


            Role.Instance.map.InitNormalStageInfo();
            
#if UNITY_EDITOR
             



#endif

            this.PlayerButtonLuckyChanage();
            ScriptBaseComponent.createComponent(this.gameObject, LuaScriptMgr.Instance.GetLuaFunction("water.createMainUIScript"), null);

        }

        private void PlayerButtonLuckyChanage(){
            if(LuaScriptMgr.Instance.CallLuaFunction_Bool("water.role.challenge.GetLuckyStatus"))
            {
                playinfo_btn_img.transform.localScale = Vector3.one;
            }
            else
            {
                playinfo_btn_img.transform.localScale = Vector3.zero;                
            }
            // playinfo_btn_img.gameObject.SetActive(LuaScriptMgr.Instance.CallLuaFunction_Bool("water.role.challenge.GetLuckyStatus"));
        }

        private void PathFindEnd()
        {
            // Debug.LogError("path find end");
            if(_Pathfinding_panel_PlayMakerFSM)
                _Pathfinding_panel_PlayMakerFSM.gameObject.SetActive(false);
           // _Pathfinding_panel_PlayMakerFSM.SendEvent("show");
        }

        private void PathFindStart()
        {
            // Debug.LogError("path find start");
            _Pathfinding_panel_PlayMakerFSM.gameObject.SetActive(true);
            _Pathfinding_panel_PlayMakerFSM.SendEvent("show");
        }
        

        // private void RefreshRewardBox(){
        //     bool isSelf = ModuleUnlock.IsModuleUnlocked(ModuleId.Box);
        //     box_btn.gameObject.SetActive(false);
        // }

        private void RefreshSevenDayBtn()
        {
            object _dayActivity = LuaScriptMgr.Instance.CallLuaFunction_Object("water.GetDayActivity");
            object _rewardActivity = LuaScriptMgr.Instance.CallLuaFunction_Object("water.getRewardingActivity");
            string _title = LuaScriptMgr.Instance.CallLuaFunction_String("water.GetSevenDayActivityTitle");
            string _icon = LuaScriptMgr.Instance.CallLuaFunction_String("water.GetSevenDayActivityIcon");
            //sevenDay_btn.gameObject.SetActive(_dayActivity[0] != null);
            //sevenDay1_btn.gameObject.SetActive(_rewardActivity[0] != null);
            sevenDay_btn.gameObject.SetActive(_dayActivity != null);
            sevenDay1_btn.gameObject.SetActive(_rewardActivity != null);
            sevenDay_btn.GetComponentInChildren<TextMeshProUGUI>().text = _title;
            sevenDay1_btn.GetComponentInChildren<TextMeshProUGUI>().text = _title;
            Image image1 = sevenDay_btn.transform.Find("seven_btn").GetComponent<Image>();
            Image image2 = sevenDay1_btn.transform.Find("seven1_btn").GetComponent<Image>();
            if(!string.IsNullOrEmpty(_icon))
            {
                XAsset.Assets.LoadWithOwner<Sprite>(string.Format("UI/Sprites/CommonImg_keep/{0}", _icon),
                    (sprite) => { image1.sprite = sprite; image2.sprite = sprite; }, this.gameObject, false);
            }
           
        }

        private bool isFirstRechargeClose()
        {
            return false;
        }

		private void RefreshFirstChargePanel()
		{
            for(int i = 0 ; i < firstcharge_btns.Count; i++)
            {
                if (isFirstRechargeClose())
                {
                    firstcharge_btns[i].btn.gameObject.SetActive(false);
                    return;
                }
                
                if(i >= firstChargetimer.Count)
                {
                    firstChargetimer.Add(0);
                }


                int gotMark = LuaScriptMgr.Instance.CallLuaFunction_Int_Int("water.role.activity.IsFirstRechargeAllAwardsGot",i+1); 
                
                if (gotMark == 1){
                    firstcharge_btns[i].btn.gameObject.SetActive (false);
                    continue;
                }
                firstcharge_btns[i].btn.gameObject.SetActive (true);

                firstcharge_btns[i].buttonName.text = LuaScriptMgr.Instance.CallLuaFunction_String_Int("water.role.activity.GetButtonName",i+1);
                string url = LuaScriptMgr.Instance.CallLuaFunction_String_Int("water.role.activity.getEntryIcon",i+1);  
                XAsset.Assets.LoadWithOwner<Sprite>(string.Format("UI/Sprites/CommonImg_keep/{0}", url), (sprite) => { firstcharge_btns[i].buttonimg.sprite = sprite; }, this.gameObject, false);
                bool isNew = LuaScriptMgr.Instance.CallLuaFunction_Bool_Int("water.role.activity.isNew",i+1);        
                if(isNew && firstcharge_btns[i].timeTxt != null && firstChargetimer[i] == 0)
                {
                    int firstChargeLeftTime = LuaScriptMgr.Instance.CallLuaFunction_Int_Int("water.role.activity.GetLeftTime",i+1);
                    if(firstChargeLeftTime > 0)
                    {
                        int subtype = LuaScriptMgr.Instance.CallLuaFunction_Int_Int("water.role.activity.GetSubType",i+1);
                        string time_ = Water.Game.Util.TimeFormatter.FormatTimeToHMS_HouverAlwaysShow(firstChargeLeftTime);
                        firstcharge_btns[i].timeTxt.text = time_;
                        firstChargetimer[i] = Game.Util.GameTimer.Inst.CreateTimer(1.0f, true, CheckFrishChargeTimeCB(i), true);     
                        firstcharge_btns[i].timeTxt.Visible(subtype == 1);
                    }else{
                        firstcharge_btns[i].btn.gameObject.SetActive (false);
                    }
                }
            }
		}
        List<int> firstChargetimer = new List<int>();
        // int firstChargeLeftTime = 0;

        Game.Void_Void CheckFrishChargeTimeCB(int id)
        {
            return delegate{CheckFirstChargeTime(id);}; 
        }
        void CheckFirstChargeTime(int id)
        {
            
            int firstChargeLeftTime = LuaScriptMgr.Instance.CallLuaFunction_Int_Int("water.role.activity.GetLeftTime",id+1);
            if(firstChargeLeftTime <= 0)
            {
                GameTimer.Inst.Stop(firstChargetimer[id]);
                firstChargetimer[id] = 0;
                firstcharge_btns[id].btn.gameObject.SetActive(false);
            }
            // firstChargeLeftTime--;
            string time_ = Water.Game.Util.TimeFormatter.FormatTimeToHMS_HouverAlwaysShow(firstChargeLeftTime);
            firstcharge_btns[id].timeTxt.text = time_;
        }
        protected override void AwakeInitEnd()
        {
            base.AwakeInitEnd();
            InitBtnParent();
            SpringUp();
            huodongshoufang_btn_onClick_unfold();
            spring_btn.onClick.AddListener(SpringDown);
            huodongshoufang_btn.onClick.AddListener(huodongshoufang_btn_onclick_fold);
            gm_btn.onClick.AddListener(gm_btn_click);

            hero_btn.onClick.AddListener(hero_btn_click);
            equip_btn.onClick.AddListener(equip_btn_click);
            hallows_btn.onClick.AddListener(talisman_btn_click);
            heroComposeBtn.onClick.AddListener(hero_compose_btn_click);
            task_btn.onClick.AddListener(task_btn_click);
            copy_btn.onClick.AddListener(map_btn_click);
            team_btn.onClick.AddListener(team_btn_click);
            huodong_btn.onClick.AddListener(huodong_btn_click);
            molong_btn.onClick.AddListener(molong_btn_click);
            // activity_btn.onClick.AddListener(activity_click);
            // activity2_btn.onClick.AddListener(activity_click);
            sevenDay_btn.onClick.AddListener(sevenDay_click);
            sevenDay1_btn.onClick.AddListener(sevenDay1_click);
            signin_btn.onClick.AddListener(signin_btn_click);
            email_btn.onClick.AddListener(email_btn_click);
            zhuzhan_btn.onClick.AddListener(zhuzhan_btn_click);
            recommended_btn.onClick.AddListener(recommended_btn_click);
            shop_btn.onClick.AddListener(shop_btn_click);

            shenmoZhanchang_btn.onClick.AddListener(shenmoZhanchang_btn_click);

            bag_btn.onClick.AddListener(bag_btn_click);
            daoju_btn.onClick.AddListener(daoju_btn_click);
            for(int i = 0;i < firstcharge_btns.Count; i++)
            {
                firstcharge_btns[i].btn.onClick.AddListener(firstCharge_onClick(i));
            }

            shoucang_btn.onClick.AddListener(shoucang_btn_click);
            miracle_btn.onClick.AddListener(miracle_btn_click);

            boss_btn.onClick.AddListener(boss_btn_click);

            roleinfo_com.vip_btn.onClick.AddListener(Vip_btn_click);
        

            playinfo_btn.onClick.AddListener(playinfo_btn_click);

            chat_btn.onClick.AddListener(onChatClicked);

            Forge_btn.onClick.AddListener(Forge_btn_click);
			wing_btn.onClick.AddListener (wing_btn_click);
			horse_btn.onClick.AddListener (horse_btn_click);
            union_btn.onClick.AddListener(union_btn_click);

            Vector3 v3 = Forge_btn.transform.position; 

            Vector2 v2 = RectTransformUtility.WorldToScreenPoint(SingletonFactory<UIManager>.Instance.UICamera, v3);

            experience_btn.onClick.AddListener(experience_btn_click);
            other_btn.onClick.AddListener(other_btn_click);
            yewai_btn.onClick.AddListener(yewai_btn_click);

            shizhuang_btn.onClick.AddListener(shizhuang_btn_click);
            chenghao_btn.onClick.AddListener(chenghao_btn_click);
            peiyang_btn.onClick.AddListener(peiyang_btn_click);
            touxiang_btn.onClick.AddListener(touxiang_btn_click);
            //chengzhang_panel.onClick.AddListener(chengzhang_click);

            paihang_btn.onClick.AddListener(paihang_btn_click);
            Shezhi_btn.onClick.AddListener(Shezhi_btn_click);
            Vip_btn.onClick.AddListener(Vip_btn_click);
            Dianjin_btn.onClick.AddListener(Dianjin_btn_click);
            Tujian_btn.onClick.AddListener(Tujian_btn_click);
            Bianqiang_btn.onClick.AddListener(Bianqiang_btn_click);
            haoyou_btn.onClick.AddListener(haoyou_btn_click);
            AICostomer_btn.onClick.AddListener(AICostomer_btn_click);
            tree_btn.onClick.AddListener(tree_btn_click);

            // sevendaysenior_btn[0].onClick.AddListener(qi_ri_zun_xiang_click);
            // if(sevendaysenior_btn[1] != null)
            //     sevendaysenior_btn[1].onClick.AddListener(qi_ri_zun_xiang_limt_click);
            for(int i = 0; i < sevenday_btns.Count; i++)
            {
                sevenday_btns[i].btn.onClick.AddListener(sevenday_onClick(i));
            }

			onQiRiZunXiangUpdated ();

            RegisterRedDotHandle();
            RegisterGuide();
            RegisterModuleUnlock();


          
            SingletonFactory<EventManager>.Instance.AddEventListener("EvtShowUnlockModuleEffect", ShowEffect);
            SingletonFactory<EventManager>.Instance.AddEventListener("complete", EndUnlockModuleEffect);

			SingletonFactory<EventManager>.Instance.AddEventListener ("QI_RI_ZUN_XIANG_UPDATE", onQiRiZunXiangUpdated);
            //SingletonFactory<EventManager>.Instance.AddEventListener("Main_Mission_Need_Show_First_Recharge", checkMainMissionShowFirstRecharge);

            this.AddEventManagerListener("EvtCloseMailPanelAndCloseOtherPanel",qitakuozhan_panel_close);
        }

        }

        //private void SetPosition()
        //{
        //    Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(SingletonFactory<UIManager>.Instance.UICamera, rectTrans.position);
        //    Debug.LogWarning("screenPos:" + screenPos.ToString());
        //    Vector2 zuobiao = new Vector2();
        //    RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, screenPos, SingletonFactory<UIManager>.Instance.UICamera, out zuobiao);
        //    Debug.LogWarning("seting pos: " + zuobiao.ToString());

        //    position_panel.FsmVariables.GetFsmVector2("zuobiao").Value = zuobiao;
        //    //SingletonFactory<EventManager>.Instance.DispatchEvent("getzuobiao", null);
        //    position_panel.SendEvent("getzuobiao");
        //}

        Dictionary<GameObject,GameObject> btnParent = new Dictionary<GameObject, GameObject>();
        void InitBtnParent()
        {
            // btnParent.Add(lottery_btn.gameObject,chouka_btn.gameObject);
            // btnParent.Add(equipbox_btn.gameObject,chouka_btn.gameObject);
            // btnParent.Add(inscription_btn.gameObject,chouka_btn.gameObject);
            // btnParent.Add(box_btn.gameObject,chouka_btn.gameObject);

            btnParent.Add(Forge_btn.gameObject,other_btn.gameObject);

            btnParent.Add(daoju_btn.gameObject,bag_btn.gameObject);
            btnParent.Add(hallows_btn.gameObject,bag_btn.gameObject);
            btnParent.Add(heroComposeBtn.gameObject,bag_btn.gameObject);
            btnParent.Add(equip_btn.gameObject,bag_btn.gameObject);
            btnParent.Add(hero_btn.gameObject,bag_btn.gameObject);
        }
        GameObject GetParentBtn(GameObject obj)
        {
            if (btnParent.ContainsKey(obj))
            {
                return btnParent[obj];
            }
            else
            {
                return obj;
            }
        }
        RectTransform rectTrans;
        int comId;

        private void ShowEffect(object sender,WaterEventArgs args)
        {
            SingletonFactory<UIManager>.Instance.SetHoverViewUIParent(this.transform,this.transform);
            LuaTable table = sender as LuaTable;

            rectTrans = table["rectTrans"] as RectTransform;

            //Debug.LogWarning(rectTrans.name);

            comId = Convert.ToInt32(table["id"]);
            UnlockfunctionData fundata = UnlockfunctionData.GetData(Convert.ToUInt32(table["moduleId"]));
            XAsset.Assets.LoadWithOwner<Sprite>(string.Format("UI/Sprites/{0}", fundata.icon), (sprite) => { position_panel.FsmVariables.GetFsmObject("sproffuncdi").Value = sprite; }, this.gameObject, false);
            position_panel.FsmVariables.GetFsmString("TxTpro").Value = string.Format("{0}", fundata.Function_Name);
             
            position_panel.FsmVariables.GetFsmGameObject("targetNode").Value = GetParentBtn(rectTrans.gameObject);
            position_panel.SendEvent("unlock");
        }

        private void EndUnlockModuleEffect()
        {
            Guide.FinishFlyIconOperation();
            position_panel.SendEvent("complete");
            SingletonFactory<UIManager>.Instance.RecoverHoverViewUIParent(this.transform);
        }
         
        public override void Dispose()
        {

            RemoveRedDotHandle();
            if(firstChargetimer != null){
                for(int i = 0; i < firstChargetimer.Count; i++){
                    if(firstChargetimer[i] != 0){
                        GameTimer.Inst.Stop(firstChargetimer[i]);
                    }
                }
            }
            if(timecheckvo!=null)timecheckvo.Stop();
            if(btnTipTimer!=null) btnTipTimer.Stop();            
            gm_btn.onClick.RemoveListener(gm_btn_click);
            spring_btn.onClick.RemoveAllListeners();
            huodongshoufang_btn.onClick.RemoveAllListeners();
            hero_btn.onClick.RemoveListener(hero_btn_click);
            equip_btn.onClick.RemoveListener(equip_btn_click);
            hallows_btn.onClick.RemoveListener(talisman_btn_click);
            heroComposeBtn.onClick.RemoveListener(hero_compose_btn_click);
            task_btn.onClick.RemoveListener(task_btn_click);
            copy_btn.onClick.RemoveListener(map_btn_click);
            team_btn.onClick.RemoveListener(team_btn_click);
            
            molong_btn.onClick.RemoveListener(molong_btn_click);
            sevenDay_btn.onClick.RemoveListener(sevenDay_click);
            bag_btn.onClick.RemoveListener(bag_btn_click);
            daoju_btn.onClick.RemoveListener(daoju_btn_click);
            for(int i = 0;i < firstcharge_btns.Count; i++)
            {
                firstcharge_btns[i].btn.onClick.RemoveListener(firstCharge_onClick(i));
            }
            shoucang_btn.onClick.RemoveListener(shoucang_btn_click);
            miracle_btn.onClick.RemoveListener(miracle_btn_click);
            boss_btn.onClick.RemoveListener(boss_btn_click);

            chat_btn.onClick.RemoveListener(onChatClicked);
            Forge_btn.onClick.RemoveListener(Forge_btn_click);
            union_btn.onClick.RemoveListener(union_btn_click);

			wing_btn.onClick.RemoveListener (wing_btn_click);
			horse_btn.onClick.RemoveListener (horse_btn_click);
            playinfo_btn.onClick.RemoveAllListeners();
            roleinfo_com.vip_btn.onClick.RemoveAllListeners();
            email_btn.onClick.RemoveAllListeners();
            zhuzhan_btn.onClick.RemoveAllListeners();
            shop_btn.onClick.RemoveListener(shop_btn_click);

            experience_btn.onClick.RemoveListener(experience_btn_click);
            other_btn.onClick.RemoveListener(other_btn_click);
            yewai_btn.onClick.RemoveListener(yewai_btn_click);

            shizhuang_btn.onClick.RemoveListener(shizhuang_btn_click);
            chenghao_btn.onClick.RemoveListener(chenghao_btn_click);
            peiyang_btn.onClick.RemoveListener(peiyang_btn_click);
            peiyang_btn.onClick.RemoveListener(touxiang_btn_click);  
            paihang_btn.onClick.RemoveListener(paihang_btn_click);
            Shezhi_btn.onClick.RemoveListener(Shezhi_btn_click);
            Vip_btn.onClick.RemoveListener(Vip_btn_click);
            Dianjin_btn.onClick.RemoveListener(Dianjin_btn_click);
            Tujian_btn.onClick.RemoveListener(Tujian_btn_click);
            Bianqiang_btn.onClick.RemoveListener(Bianqiang_btn_click);
            haoyou_btn.onClick.RemoveListener(haoyou_btn_click);
            AICostomer_btn.onClick.RemoveListener(AICostomer_btn_click);
            tree_btn.onClick.RemoveListener(tree_btn_click);

            roleinfo_com.vip_btn.onClick.RemoveListener(Vip_btn_click);

            UnRegisterGuide();
            UnRegisterModuleUnlock();

            SingletonFactory<EventManager>.Instance.RemoveEventListener("EvtShowUnlockModuleEffect", ShowEffect);
            SingletonFactory<EventManager>.Instance.RemoveEventListener("complete", EndUnlockModuleEffect);

			this.RemoveEventManagerListener ("EvtFirstChargeUpdateToMainUI", RefreshFirstChargePanel);

			SingletonFactory<EventManager>.Instance.RemoveEventListener ("QI_RI_ZUN_XIANG_UPDATE", onQiRiZunXiangUpdated);
            //SingletonFactory<EventManager>.Instance.RemoveEventListener("Main_Mission_Need_Show_First_Recharge", checkMainMissionShowFirstRecharge);
            this.RemoveEventManagerListener("EvtCloseMailPanelAndCloseOtherPanel",qitakuozhan_panel_close);
            Role.Instance.hero.QualityUpComplete -= this.onRoleInfoUpdate;

            base.Dispose();
        }

        private void onRoleInfoUpdate()
        {
            roleinfo_com.rolelv_txt.text = Role.Instance.baseInfo.level+"";
            roleinfo_com.role_icon.LoadIcon_ImgSprite(Role.Instance.hero.RoleHero.IconUrl);
            roleinfo_com.role_icon.SetColor(Role.Instance.hero.RoleHero.ShowColor);
            roleinfo_com.role_icon.SetBGColor(Role.Instance.hero.RoleHero.ShowColor);
            roleinfo_com.name_txt.text = WaterGameConst.GetColorTextStrByColorInt(Role.Instance.baseInfo.name, Role.Instance.hero.RoleHero.ShowColor); 
            roleinfo_com.vip_num_txt.text = Role.Instance.baseInfo.vip + "";
            onRoleTeamGSUpdate();
        }

        private void onRoleTeamGSUpdate()
        {
            Role.Instance.partner.PartnerTeam.TeamGSSetDirty(true);
            roleinfo_com.zhanli_txt.text = Role.Instance.partner.PartnerTeam.GetTeamGSStr();
        }

        private void onShowSceneStart(object sender, WaterEventArgs e){
            if(sender is NiudanScene){
                if(this.gameObject.activeSelf){
                    this.Exit(UIHandleType.DefaultHandle);
                }
            }
        }

        void paihang_btn_click()
        {
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Rank, true))
            {
                LuaScriptMgr.Instance.CallLuaFunction_Void_Int("water.showRankUIView", 101);
            }
        }

        void Shezhi_btn_click()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.showSystemSetUIView");
        }
        void Vip_btn_click()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.showVipUI");
        }
        void Dianjin_btn_click()
        {
             Water.Role.Instance.baseInfo.OnResAskAdd((int)SpecialItemID.GOLD);
        }

        void Tujian_btn_click()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.showPhotoHandBookUIView");
        }
       

        void haoyou_btn_click()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.ShowFriendsMainUIView");
        }

        void AICostomer_btn_click()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void("channel.OpenAI_HELP");
        }

        void tree_btn_click()
        {
            Guide.FinishClickOperation(21000);
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.ShowWorldTreeMainView");
        }

        void peiyang_btn_click()
        {
            Guide.FinishClickOperation(1050);
            if (AnimationUnDefaultpeiyang)
            {
                MainUIAnimationReset();
                BugReport.LogReport.SendFuncEnumId(43);
                if (peiyang_btn.gameObject.activeInHierarchy)
                    peiyang_panel.SetTrigger("show");
                AnimationUnDefaultpeiyang = false;
            }
            else if(!Guide.IsInSeries(150)) 
            {
                if (peiyang_btn.gameObject.activeInHierarchy)
                    peiyang_panel.SetTrigger("hide");
                AnimationUnDefaultpeiyang = true;
            }
             
        }
         
        void chenghao_btn_click()
        {
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Title, true))
            {
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.showTitleSystemUIView");
            }
            
        }
        
        void touxiang_btn_click()
        {
            if(ModuleUnlock.IsModuleUnlocked(ModuleId.Portrait,true))
            {
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.showPortraitUIView");
            }
        }

        void shizhuang_btn_click()
        {
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.FashionCloth, true))
            {
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.showFashionDressUIView");
            }
        }

        void playinfo_btn_click()
        {
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Challenge, true))
            {
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.showPlayEntranceUIView");
                Guide.FinishClickOperation(1008);
            }
        } 

        void experience_btn_click()
        { 
            NpcID npcID = GameSceneSwitcher.IfInNewMainCity() ? NpcID.eCity2Travel : NpcID.eTravel;
    		Water.ScriptUIManager.Instance.MoveOrOpenNpcUI(npcID);
        }

        void yewai_btn_click()
        {
            Guide.FinishClickOperation(1041);
            LuaScriptMgr.Instance.CallLuaFunction_Void_Int("water.jumpToPanel",Config.UnlockfunctionData.GetData((int)ModuleId.Field).Ui_id);        
        }

        void other_btn_click()
        {

            if (AnimationUnDefault_qitakuozhan)
            {
                MainUIAnimationReset();
                BugReport.LogReport.SendFuncEnumId(42);
                qitakuozhan_panel.transform.SetParent(other_btn.transform, false);                
                // qitakuozhan_panel.transform.localPosition = new Vector3(0,0,0);
                if (qitakuozhan_panel.gameObject.activeInHierarchy)
                    qitakuozhan_panel.SetTrigger("show");
                AnimationUnDefault_qitakuozhan = false;
                if (!AnimationUnDefault_beibao_zishiyin_panel)
                {
                    bag_btn_click();
                }

            }
            else
            {
                if (qitakuozhan_panel.gameObject.activeInHierarchy)
                    qitakuozhan_panel.SetTrigger("hide");
                AnimationUnDefault_qitakuozhan = true;
            }
            Guide.FinishClickOperation(1009);
        }

        void qitakuozhan_panel_close(){
            if(!AnimationUnDefault_qitakuozhan){ 
                if (qitakuozhan_panel.gameObject.activeInHierarchy)
                    qitakuozhan_panel.SetTrigger("hide");
                AnimationUnDefault_qitakuozhan = true;
            }
        }

        void daoju_btn_click()
        {
           
            Guide.FinishClickOperation(1014);
            
            SingletonFactory<UIManager>.Instance.OpenUI(UIType.BagUI);
             
        }
            
        void shoucang_btn_click()
        {
            Guide.FinishClickOperation(22000);
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.ShowAdventureBookMainUIView");
        }

        void miracle_btn_click()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.ShowMiracleMainUIView");
            Guide.FinishClickOperation(1043);
        }

        void boss_btn_click() {
            Guide.FinishClickOperation(1048);
            LuaScriptMgr.Instance.CallLuaFunction_Void_Int("water.showBossHouseUIView",0);
        }

        void explode_btn_click()
        {
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.HeroDecompose, true))
            {
                DecomposeMainUIView.showDecomposePanel(DecomposeType.Hero);
            }
        }
        void create_btn_click()
        { 
        
        }
		void wing_btn_click()
		{
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Wings, true))
            {
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.showWingMainUIView");
            }
		}
		void horse_btn_click()
		{
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Wings, true))
            {
                Guide.FinishClickOperation(106001);                
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.showHorseMainUIView");
            }
		}

        void Forge_btn_click()
        {//Furnace
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Furnace, true))
            {
                Guide.FinishClickOperation(1015);
                DecomposeMainUIView.showDecomposePanel(DecomposeType.Hero);
            }
        }
        void bag_btn_click()
        {
            Guide.FinishClickOperation(1039);

            if (Guide.IsInForceGuiding())
            {
                if (!AnimationUnDefault_beibao_zishiyin_panel)
                    return;
            }

            if (AnimationUnDefault_beibao_zishiyin_panel)
            {
                MainUIAnimationReset();
                if (beibao_panel.gameObject.activeInHierarchy)
                    beibao_panel.SetTrigger("show");
                AnimationUnDefault_beibao_zishiyin_panel = false;
                if (!AnimationUnDefault_qitakuozhan)
                {
                    other_btn_click();
                }
            }
            else
            {
                if (beibao_panel.gameObject.activeInHierarchy)
                    beibao_panel.SetTrigger("hide");
                AnimationUnDefault_beibao_zishiyin_panel = true;
            }
        }

        void union_btn_click()
        {
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Union, true))
            {
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.unionGuide");
                Guide.FinishClickOperation(1016);
            }
        }

        UnityEngine.Events.UnityAction sevenday_onClick(int i)
        {
            return delegate{qi_ri_zun_xiang_click(i);};
        }
        void qi_ri_zun_xiang_click(int i){
            LuaScriptMgr.Instance.CallLuaFunction_Void_Int("water.showQiRiZunXiangUIView",i+1);
        }

        void hero_btn_click()
        {
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Hero, true))
            {
                SingletonFactory<UIManager>.Instance.OpenUI(UIType.HeroListUI);
                Guide.FinishClickOperation(1011);
            }
        }

        void equip_btn_click()
        {
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Equipments, true))
            {
                SingletonFactory<UIManager>.Instance.OpenUI(UIType.EquipmentsListUI);
                Guide.FinishClickOperation(1012);
            }
        }

        void talisman_btn_click()
        {
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Hallow, true))
            {
                SingletonFactory<UIManager>.Instance.OpenUI(UIType.HallowsListUI);
            }
        }

        void hero_compose_btn_click()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void_Int("water.showComposeMainUI",1);
        }

        void task_btn_click()
        {
//            if (ModuleUnlock.IsModuleUnlocked(ModuleId.DailyTask, true))
//            {
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.showTaskDailyUIView");
                Guide.FinishClickOperation(1018);
                //LuaScriptMgr.Instance.CallLuaFunction_Void("water.ShowGrowGoalListUIView");
//            }
        }
			
        void team_btn_click()
        {
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Team, true))
            {
                Guide.FinishClickOperation(1010);
                SingletonFactory<UIManager>.Instance.OpenUI(UIType.PartnerUI);
            }
        }

        void signin_btn_click()
        {
             if(ModuleUnlock.IsModuleUnlocked(ModuleId.SignIn,true))
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.showSignInUIView");            
        }

        //private void chengzhang_click()
        //{
        //    LuaScriptMgr.Instance.CallLuaFunction_Void("water.ShowGrowGoalListUIView");
        //}
	 
        private void email_btn_click()
        {
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Mails, true))
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.showMailsListUIView");
        }

        private void zhuzhan_btn_click()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.showFriendAssistSetUIView");
        }

        private void recommended_btn_click()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.ShowRecommendTeamView");
        }

        void sevenDay_click()
        {
            
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.OpenSevenDayUI");//("water.OpenRewardingSevenDayUI");
        }

        void sevenDay1_click()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.OpenRewardingSevenDayUI");
        }

        void box_btn_click()
        {
            //if (ModuleUnlock.IsModuleUnlocked(ModuleId.Box, true))
            {
                Guide.FinishClickOperation(1040);
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.OpenBaoXiangUIView");
            }
        }

        void activity_click()
        {
//            if (Role.isTempClosed())
//            {
//                GameShowNoticeUtil.ShowMsgByCodeID(10318);
//                return;
//            }
           // TextMeshProUGUI child_txt = newButton.GetComponentInChildren<TextMeshProUGUI>();
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.showActivityUIView");


        }

        void map_btn_click()
        {
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.MainUIOpenCopy");
            Guide.FinishClickOperation(1006);
        }

        // void recharge_btn_click()
        // {
        //
        //     LuaScriptMgr.Instance.CallLuaFunction_Void("water.showRechargeUI");
        // }

        void gm_btn_click()
        {
            SingletonFactory<UIManager>.Instance.OpenUI(UIType.GMUI); 
        }

        UnityEngine.Events.UnityAction firstCharge_onClick(int i)
        {
            return delegate{fristRecharge_btn_click(i);};
        }
        void fristRecharge_btn_click(int i)
        {
			LuaScriptMgr.Instance.CallLuaFunction_Void_Int("water.showFirstChargeUIView",i+1);
        }

        void shop_btn_click()
        {
            Guide.FinishClickOperation(1029);            
            if (ModuleUnlock.IsModuleUnlocked(ModuleId.Shop, true))
                LuaScriptMgr.Instance.CallLuaFunction_Void("water.showChangeShopUIView"); 
        }

        void huodong_btn_click()
        {
            Guide.FinishClickOperation(1053);
            if (AnimationUnDefault_activity)
            {
                MainUIAnimationReset();
                huodong_panel.transform.position = huodong_btn.transform.position;
                if (huodong_panel.gameObject.activeInHierarchy )
                    huodong_panel.SetTrigger("show");
                AnimationUnDefault_activity = false;
            }
            else
            {
                if (huodong_panel.gameObject.activeInHierarchy )
                    huodong_panel.SetTrigger("hide");
                AnimationUnDefault_activity = true;
            }
        }

        void molong_btn_click(){
            if(AnimationMoLong_activity)
            {
                MainUIAnimationReset();
                molong_panel.transform.position = molong_btn.transform.position;
                if (molong_panel.gameObject.activeInHierarchy)
                    molong_panel.SetTrigger("show");
                AnimationMoLong_activity = false;
            }
            else
            {
                if (molong_panel.gameObject.activeInHierarchy)
                    molong_panel.SetTrigger("hide");
                AnimationMoLong_activity = true;
            }
        }

        private void shenmoZhanchang_btn_click()
        {
            NpcID npcID = GameSceneSwitcher.IfInNewMainCity() ? NpcID.eCity2ShenMoZhanChang : NpcID.eShenMoZhanChang;
    		Water.ScriptUIManager.Instance.MoveOrOpenNpcUI(npcID);
            //LuaScriptMgr.Instance.CallLuaFunction_Void("water.ShowShenmoZhanchangMainUIView");
        }

        void StartShowBtnTip()
        {
            if(btnTipTimer != null) btnTipTimer.Stop();
            btnTipTimer = TimeUtil.Instance.CreateRealTimeCheck(tip_duration, () =>
                {
                    if(gameObject.activeInHierarchy)
                    {
                        ShowBtnTip();
                    }
                },true);  
            btnTipTimer.Start();    
        }


        void huodongshoufang_btn_onclick_fold(){
            if (huodongshoufang_btn_anim.gameObject.activeInHierarchy)
                huodongshoufang_btn_anim.SetTrigger("guan");
            huodongshoufang_btn.onClick.RemoveListener(huodongshoufang_btn_onclick_fold);
            huodongshoufang_btn.onClick.AddListener(huodongshoufang_btn_onClick_unfold);
            if(huodongshoufang_hot_panel)
                huodongshoufang_hot_panel.gameObject.SetActive(true);
                RedDotReminderHandle.RedDotHandle(true,huodongshoufang_hot_panel);
            Activity_Spring_On = false;

            HideBtnTip();
            if (btnTipTimer != null) btnTipTimer.Stop();
            if (!AnimationUnDefault_zhaohuan)
            {
                if (huodongshoufang_btn_anim.gameObject.activeInHierarchy)
                    zhaohuan_panel.SetTrigger("hide");
                AnimationUnDefault_zhaohuan = true;
            }
            if (!AnimationUnDefault_activity)
            {
                if (huodongshoufang_btn_anim.gameObject.activeInHierarchy)
                    huodong_panel.SetTrigger("hide");
                AnimationUnDefault_activity = true;
            }

            if (!AnimationMoLong_activity){
                molong_btn_click();
            }

        }

        void huodongshoufang_btn_onClick_unfold(){
            if (huodongshoufang_btn_anim.gameObject.activeInHierarchy)
                huodongshoufang_btn_anim.SetTrigger("kai");
            huodongshoufang_btn.onClick.RemoveListener(huodongshoufang_btn_onClick_unfold);
            huodongshoufang_btn.onClick.AddListener(huodongshoufang_btn_onclick_fold);
            if(huodongshoufang_hot_panel)
                huodongshoufang_hot_panel.gameObject.SetActive(false);
                RedDotReminderHandle.RedDotHandle(false,huodongshoufang_hot_panel);
            Activity_Spring_On = true;

            StartShowBtnTip();
        }

        Dictionary<Button,GameObject> btnTips = new Dictionary<Button,GameObject>();
        Dictionary<EMainBtnTiptype, Button> tipBtns = new Dictionary<EMainBtnTiptype, Button>(); 
        void registerTipBtns(EMainBtnTiptype key,Button btn)
        {
            if(!tipBtns.ContainsKey(key))
                tipBtns.Add(key,btn);
        }
        void unregisterTipBtns(Button btn)
        {
            if(tipBtns.ContainsValue(btn))
            {
                foreach (var item in tipBtns)
                {
                    if(item.Value == btn)
                    {
                        tipBtns.Remove(item.Key);
                        break;
                    }
                }
            }
        }

        string tipContent = string.Empty;
        string assetPath = "UI/UnitUI/MainCity/qipaoUI";

        void ShowBtnTip()
        {
            HideBtnTip();
            Button btn = GetRandomTipBtn();
            if (string.IsNullOrEmpty(tipContent)) return;
            if (btn == null || !btn.gameObject.activeInHierarchy) 
            {

                return;
            }
                
            if (btnTips.ContainsKey(btn))
            {
                btnTips[btn].SetActive(true);
                btnTips[btn].transform.SetParent(btn.transform, true);
                btnTips[btn].transform.localPosition = new Vector3(0, 0, 0);
                btnTips[btn].transform.SetParent(huodongshoufang_btn.transform, true);
                TextMeshProUGUI txt = btnTips[btn].GetComponentInChildren<TextMeshProUGUI>();
                Animator ani = btnTips[btn].GetComponentInChildren<Animator>();
                ani.SetTrigger("show");
                txt.text = tipContent;
            }
            else
            {
                XAsset.Assets.LoadWithOwner(assetPath, (GameObject obj_) =>
                {
                    if(!btn) return;
                    GameObject obj = null;
                    if(btnTips.ContainsKey(btn)){
                        obj = btnTips[btn];
                    }else{
                        if(!obj_) return;
                        obj = GameObject.Instantiate(obj_);
                        btnTips.Add(btn,obj);
                    }
                    obj.SetActive(true);
                    obj.transform.SetParent(btn.transform, false);
                    obj.transform.localPosition = new Vector3(0, 0, 0);
                    obj.transform.SetParent(huodongshoufang_btn.transform, true);
                    TextMeshProUGUI txt = obj.GetComponentInChildren<TextMeshProUGUI>();
                    Animator ani = obj.GetComponentInChildren<Animator>();
                    ani.SetTrigger("show");
                    txt.text = tipContent;
                }, btn.gameObject, true);
            }
        }
        void HideBtnTip()
        {
            foreach(var v in btnTips)
            {
                v.Value.SetActive(false);
            }
        }

        List<int> _randBubbleTypes = null;
        int getRandBubbleType(){
            if(_randBubbleTypes == null){
                Dictionary<uint,MainbubbleData> bubble = MainbubbleData.Load();
                _randBubbleTypes = new List<int>();
                foreach(var v in bubble)
                {
                    if(!_randBubbleTypes.Contains(v.Value.Type))
                    {
                        _randBubbleTypes.Add(v.Value.Type);
                    }
                }

            }
            return _randBubbleTypes[UnityEngine.Random.Range(0, _randBubbleTypes.Count)];
            // return 3;
        }

        Button GetRandomTipBtn()
        {
            Dictionary<uint,MainbubbleData> bubble = MainbubbleData.Load();
            int randomType = getRandBubbleType();
            tipContent = string.Empty;
            int index = 0;
            if (randomType == (int)EMainBtnTiptype.FirstCharge)
            {
                index = LuaScriptMgr.Instance.CallLuaFunction_Int_Int("water.role.activity.GetFirstRechargeNext",1);
            }
            foreach (var v in bubble)
            {
                if (v.Value.Type == randomType && v.Value.Id%10==index)
                {
                    if (v.Value.Id == 10300){
                        string txt = LuaScriptMgr.Instance.CallLuaFunction_String("water.role.niudan.GetDayDisTxt");
                        if(!string.IsNullOrEmpty(txt)){
                            string[] strs = txt.Split('|');
                            tipContent = string.Format(CodeTextData.AUTOSTR("<color=orange>{0}</color>{1}"),strs[0],strs[1]);
                        }else{
                            tipContent = v.Value.Name;
                        }
                    }else{
                        tipContent = v.Value.Name;
                    }
                    break;
                }
            }
            if (tipBtns.ContainsKey((EMainBtnTiptype)randomType))
                return tipBtns[(EMainBtnTiptype)randomType];
            return null;
        }

        void SpringUp()
        {
           // spring_btn.animator.enabled = true;
            if (RightMove_panel.gameObject.activeInHierarchy)
                RightMove_panel.SetTrigger("moveup");
            if (BottomMoveFunc_panel.gameObject.activeInHierarchy)
                BottomMoveFunc_panel.SetTrigger("moveup");
            if (spring_btn.gameObject.activeInHierarchy)
                spring_btn.animator.SetTrigger("show");
            
            spring_btn.onClick.RemoveListener(SpringUp);
            spring_btn.onClick.AddListener(SpringDown);
            Spring_On = true;
            //spring_btn.animator.enabled = false;
            Transform red_tran = TransformUtil.FindChildAccurately(spring_btn.transform, RedDotReminderHandle.RedDotPanelName);
            if (red_tran)
            {
                red_tran.gameObject.SetActive(false);
            }
			LuaScriptMgr.Instance.CallLuaFunction_Void("water.CloseChatUI");
        }

        void SpringDown()
        {
            //   spring_btn.animator.enabled = true;
            if (RightMove_panel.gameObject.activeInHierarchy)
                RightMove_panel.SetTrigger("movedown");
            if (BottomMoveFunc_panel.gameObject.activeInHierarchy)
                BottomMoveFunc_panel.SetTrigger("movedown");
            if (spring_btn.gameObject.activeInHierarchy)
                spring_btn.animator.SetTrigger("hide");
            
            spring_btn.onClick.RemoveListener(SpringDown);
            spring_btn.onClick.AddListener(SpringUp);
            Spring_On = false;
          //  spring_btn.animator.enabled = false;
            Transform red_tran = TransformUtil.FindChildAccurately(spring_btn.transform, RedDotReminderHandle.RedDotPanelName);
            if (red_tran)
            { 
               red_tran.gameObject.SetActive(true); 
            }


            if (!AnimationUnDefault_qitakuozhan) 
                other_btn_click();
            if (!AnimationUnDefault_beibao_zishiyin_panel)
                bag_btn_click();

        }

  

        private void MainUIAnimationReset()
        {
            if (!Spring_On){
                //SpringUp();
            }
            if(!Activity_Spring_On){
                //huodongshoufang_btn_onClick_unfold();
            }
            if(!AnimationUnDefaultpeiyang)
            {
                peiyang_btn_click();
            }
            if(!AnimationUnDefault_qitakuozhan)
            {
                other_btn_click();
            }
            // if(!AnimationUnDefault_zhaohuan)
            // {
            //     chouka_btn_click();
            // }
            if (!AnimationUnDefault_activity)
            {
                huodong_btn_click();
            }

            if(!AnimationUnDefault_beibao_zishiyin_panel)
            {
                bag_btn_click();
            }

            if (!AnimationMoLong_activity){
                molong_btn_click();
            }
        }

		bool beforeClickChatBtnSpringIsOn = false;

        void onChatClicked()
        {
       
			if (Spring_On == true) 
			{
				beforeClickChatBtnSpringIsOn = true;
				SpringDown();
			}
			else
				beforeClickChatBtnSpringIsOn = false;
            
            chat_panel.Visible(false);
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.showChatUIView");
        }

        private void OnJoyStickStart()
        {
            _chat_PlayMakerFSM.SendEvent("chat_hide");
        }

        private void OnJoyStickStop()
        {
            _chat_PlayMakerFSM.SendEvent("chat_show");
        }
			
        protected override void OnExit()
        {
            this.RemoveEventManagerListener(GameJoystick.JoyStickStart, OnJoyStickStart);
            this.RemoveEventManagerListener(GameJoystick.JoyStickStop, OnJoyStickStop);


            //SingletonFactory<EventManager>.Instance.DispatchEvent(MainUI_Visable_Change, 0);

            _main_PlayMakerFSM.SendEvent("GoOn");
            AnimationUnDefaultpeiyang = true; 
            AnimationUnDefault_qitakuozhan = true;
            AnimationUnDefault_beibao_zishiyin_panel = true;
            Role.Instance.heartBeat.IsCanSend_AreaHeartBeat = false;

            SingletonFactory<EventManager>.Instance.RemoveEventListener(EquipsMediator.SHOW_QUICK_EQUIP, TryShowQuickEquip);
            SingletonFactory<EventManager>.Instance.RemoveEventListener(MainCity.MainCityOpenEvt, PlayMainCityAudio);

            base.OnExit();
        }


        private void PlayMainCityAudio()
        {
            SoundManager.Instance.BGMComponent.PlayBgm (SoundManager.BgmType.MainCity);
        }

        protected override void OnEnter()
        {
            huodongshoufang_animtion_callback.hideShade();
            this.AddEventManagerListener(GameJoystick.JoyStickStart, OnJoyStickStart);
            this.AddEventManagerListener(GameJoystick.JoyStickStop, OnJoyStickStop);
            this.AddEventManagerListener(play_btn_lucky,PlayerButtonLuckyChanage);
            //MainUIAnimationReset();

            //SingletonFactory<EventManager>.Instance.DispatchEvent(MainUI_Visable_Change, 1);

            // SoundManager.Instance.BGMComponent.PlayBgm (SoundManager.BgmType.MainCity);
            Role.Instance.heartBeat.IsCanSend_AreaHeartBeat = true;

            if(_chat_PlayMakerFSM)
                _chat_PlayMakerFSM.SendEvent("chat_show");

            SingletonFactory<EventManager>.Instance.AddEventListener(EquipsMediator.SHOW_QUICK_EQUIP, TryShowQuickEquip);
            SingletonFactory<EventManager>.Instance.AddEventListener(MainCity.MainCityOpenEvt, PlayMainCityAudio);

            //TryShowQuickEquip();
            Role.Instance.equip.quickEquipModule.OnResponseToUIDispose();
            base.OnEnter();
            SingletonFactory<UIManager>.Instance.HoverLayerViewUI.UITypeBeChange(this);
            SingletonFactory<UIManager>.Instance.HoverLayerViewUI.ResShowListControl(new List<int> { (int)SpecialItemID.STAMINA,(int)SpecialItemID.ENERGY, (int)SpecialItemID.GOLD, (int)SpecialItemID.DIAMOND });
            Guide.CheckIsAnyGuideActive();
            
            LuaScriptMgr.Instance.CallLuaFunction_Void("water.role.eveluate.checkShowEvaluate");
        }

        private void TryShowQuickEquip(object qe, WaterEventArgs args)
        {
            if (Role.Instance != null)
            {
                if(!Guide.IsInForceGuiding()){
                    AddSubUI(LuaScriptMgr.Instance.CallLuaFunction_Object("water.GetQuickEquipUIType") as UIType, null, (QuickEquip)qe);
                    //Role.Instance.equip.quickEquipModule.OnResponseToUIDispose();
                }
            }
        }

        public void SetHarveset(bool show)
        {
            if(show)
                harvest_txt.text = CodeTextData.AUTOSTR("1052051");
            else
                harvest_txt.text = "";
        }
   

        private void RegisterRedDotHandle()
        {
            
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Arena_Challenge_ID, 0, playinfo_btn.transform);

            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Season_SaiJiReward, 0, playinfo_btn.transform, spring_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Season_UpGrade, 0, playinfo_btn.transform, spring_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Season_WeekTask, 0, playinfo_btn.transform, spring_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Season_MonthTask, 0, playinfo_btn.transform, spring_btn.transform);
            //// RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Arena_Records_ID, 0, playinfo_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.ShenmoPower_CanChallenge_ID, 0, playinfo_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.ShenmoPower_CanReset_ID, 0, playinfo_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.GoldCopy_CanChallenge_ID, 0, playinfo_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.ExpCopy_CanChallenge_ID, 0, playinfo_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.KingWayMobai_ID, 0, playinfo_btn.transform);

            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Map_Chest_ID, 0, copy_btn.transform, spring_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.DailyMissionForDepthBoss, 0, copy_btn.transform, spring_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_DailyMissionDepthBossID, 0, copy_btn.transform, spring_btn.transform);


            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Travel_BoxReward_ID, 0, experience_btn.transform, spring_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.AutoTravel_ID, 0, experience_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Travel_QiYu_ID,0,experience_btn.transform, spring_btn.transform);


            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Role_Equip_Combine_ID, 0, equip_btn.transform, spring_btn.transform, bag_hongdian_panel.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Partner_Equip_Combine_ID, 0, equip_btn.transform, spring_btn.transform, bag_hongdian_panel.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Slot_Equip_CanEquipNew_ID, 0, equip_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Slot_Equip_Enhance_ID, 0, equip_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Slot_Equip_StarUp_ID, 0, equip_btn.transform);


            
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Slot_GemAdd_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Slot_RuneAdd_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Slot_RuneCanStrength_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Slot_RuneCanAdvance_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Slot_Equip_AdvanceUp_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Slot_Equip_CanEquipNew_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Slot_Equip_Enhance_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Slot_Equip_StarUp_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Hallow_Equip_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Hallow_Advance_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Hallow_Upgrade_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Hallow_LianJin_ID, 0 , team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Hallow_Awake_ID, 0 , team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Hallow_ChangBetterHallow_ID, 0, team_btn.transform, spring_btn.transform);

            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.EquipSuit_RoleCanCombineSuit, 0, team_btn.transform, spring_btn.transform);



            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Hero_Combine_ID, 0, hero_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Hero_Combine_ID, 0, hero_btn.transform, spring_btn.transform, bag_hongdian_panel.transform);


            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Partner_StarUp_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Partner_AdvanceUp_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Partner_SkillUp_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Partner_LevelUp_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Partner_NewHeroCanAdd_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Partner_YuanjunAdd_ID, 0, team_btn.transform, spring_btn.transform);
            // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Partner_GetNewSkin_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Partner_Quality_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Partner_StarFate_ID, 0, team_btn.transform, spring_btn.transform);

            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Talisman_All_ID, 0, team_btn.transform, spring_btn.transform); 


            {

			    RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Task_Main_ID, 0, task_btn.transform);

                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Wing_Combine_ID, 0, wing_btn.transform,peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Wing_StarUP_ID, 0, wing_btn.transform, peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Wing_LevelUP_ID, 0, wing_btn.transform, peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Wing_Advance_ID, 0, wing_btn.transform, peiyang_btn.transform);

                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Horse_Combine_ID, 0, horse_btn.transform,peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Horse_LevelUP_ID, 0, horse_btn.transform, peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Horse_StarUP_ID, 0, horse_btn.transform, peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Wing_Advance_ID, 0, horse_btn.transform, peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Fashion_Combine_ID, 0, shizhuang_btn.transform, peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Fashion_Combine_ID, 1, shizhuang_btn.transform, peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Fashion_Combine_ID, 2, shizhuang_btn.transform, peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Fashion_Combine_ID, 3, shizhuang_btn.transform, peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Fashion_LevelUP_ID, 0, shizhuang_btn.transform, peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Fashion_StarUP_ID, 0, shizhuang_btn.transform, peiyang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Fashion_Advance_ID, 0, shizhuang_btn.transform, peiyang_btn.transform);

                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Title_NewGet_ID, 0, chenghao_btn.transform, peiyang_btn.transform);
                // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Title_CanActive_ID, 0, chenghao_btn.transform, peiyang_btn.transform);

                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Portrait_CanActive_ID, 0, touxiang_btn.transform);
                // 
            }
            
            {
                // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.MoLong_ID,0,maolongbaozang_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Sign_Main_ID, 0, signin_btn.transform,huodongshoufang_btn.transform, huodong_btn.transform);
                // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.LuckDialActivity_ID,0, xinyunlunpan_btn.transform,huodongshoufang_btn.transform,huodong_btn.transform);

                //boss
                //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.BOSS_PERSONAL_BOSS, 0, boss_btn.transform,huodongshoufang_btn.transform);
                //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.BOSS_VIP_BOSS, 0, boss_btn.transform,huodongshoufang_btn.transform);
                // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.BOSS_WILD_BOSS, 0, boss_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.BOSS_World_BOSS, 0, boss_btn.transform, huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldBossMobai_ID, 0, boss_btn.transform, huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.DailyMissionForWorldBoss, 0, boss_btn.transform, huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_DailyMissionWorldBoss_ID, 0, boss_btn.transform, huodongshoufang_btn.transform);
                
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_Hero_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_Honor_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_Courage_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_Hallow_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_Union_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_EquipBlue_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_EquipPurple_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_EquipOranges1_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_EquipOranges2_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_AccessoryPurple_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_AccessoryOranges1_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_AccessoryOranges2_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_Theater_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);

                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Niudan_Diamon_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Niudan_Glod_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.NiuDan_Glod_STONE_DEBRIS, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.NiuDan_Diamon_STONE_DEBRIS, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Niudan_RenZu_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Niudan_ShenZu_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Niudan_MoZu_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Niudan_Talisman_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Niudan_Hallow_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Niudan_Equip_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Niudan_HeroEquip_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Niudan_Inscription_Gold_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Niudan_Inscription_DIAMOND_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);

                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.NiuDan_Activity_RedId, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);

                
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Activity_SevenDay_ID, 0, sevenDay_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Activity_SevenDay_ID, 1, sevenDay1_btn.transform,huodongshoufang_btn.transform);

                for(int i = 0 ; i < sevenday_btns.Count; i++) {
                    int reddotId = LuaScriptMgr.Instance.CallLuaFunction_Int_Int("water.role.qiRiZunXiang.getRedDotID",i+1);        
                    RedDotReminderHandle.AddRedDotReminder(reddotId, 0, sevenday_btns[i].btn.transform,huodongshoufang_btn.transform);
                }

                
                for(int i = 0 ; i < firstcharge_btns.Count; i++) {
                    int _redId = LuaScriptMgr.Instance.CallLuaFunction_Int_Int("water.role.activity.GetRedDotId",i+1);     
                    if(_redId != 0)
                        RedDotReminderHandle.AddRedDotReminder(_redId, 0, firstcharge_btns[i].btn.transform);
                }
            }
             

            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Union_CanDonate_ID, 0, union_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Union_NewPlayerApply_ID, 0, union_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Union_Office_Task, 0, union_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Union_Office_RewardBox, 0, union_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.UnionMobai_ID, 0, union_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Union_Office_Friend, 0, union_btn.transform);

            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Mail_Main_ID, 0, email_btn.transform, other_btn.transform.GetChild(3));
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Furnace_CanDecompose_ID,0,Forge_btn.transform,peiyang_btn.transform);


			//RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Chat_Main_ID, 0, chat_btn.transform);


            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Hallow_Combine_ID, 0, hallows_btn.transform, spring_btn.transform, bag_hongdian_panel.transform);
           

            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Vip_BuyGift_ID, 0, roleinfo_com.vip_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Vip_BuyGift_ID, 1, roleinfo_com.vip_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Vip_BuyGift_ID, 2, roleinfo_com.vip_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Vip_BuyGift_ID, 0, recharge_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Vip_BuyGift_ID, 1, recharge_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Vip_BuyGift_ID, 2, recharge_btn.transform);
            
  
            // for(int i = 1; i < 7 ;i++)
            // {
            //     RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Bag_BoxItem_ID, i, daoju_btn.transform, spring_btn.transform, bag_hongdian_panel.transform);
            // }
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Bag_MainUIRed_ID, 0, daoju_btn.transform, spring_btn.transform, bag_hongdian_panel.transform);


            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Friends_Energy_ID, 0, haoyou_btn.transform, other_btn.transform.GetChild(3));
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Friends_Invitation_ID, 0, haoyou_btn.transform, other_btn.transform.GetChild(3));
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Friends_LikeInfoLog_ID, 0, haoyou_btn.transform, other_btn.transform.GetChild(3));

            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.AdventureBook_Story_ID, 0, shoucang_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.AdventureBook_Monster_ID, 0, shoucang_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.AdventureBook_Npc_ID, 0, shoucang_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.AdventureBook_Collection_ID, 0, shoucang_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.AdventureBook_Fashion_ID, 0, shoucang_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.AdventureBook_Wing_ID, 0, shoucang_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.AdventureBook_Horse_ID, 0, shoucang_btn.transform,peiyang_btn.transform);


            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Miracle_BlessTabCanUpgrade_ID, 0, miracle_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Paragon_Level_RedDot, 0, miracle_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Miracle_NewBlessItemUnlocked_ID, 0, miracle_btn.transform,peiyang_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Miracle_NewBlessItemUnlocked_ID, 1, miracle_btn.transform);

     
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Wild_Main_ID, 0, yewai_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Wild_All_MapQuest_ID,0, yewai_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_DailyMissionWild_ID, 0, yewai_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.DailyMissionForWild, 0, yewai_btn.transform);
			// RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldBossMobai_ID, 0, yewai_btn.transform);
            // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.DailyMissionForWorldBoss, 0, yewai_btn.transform);
            // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_DailyMissionWorldBoss_ID, 0, yewai_btn.transform);



            // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldTree_CanAdv, 0, tree_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldTree_GatherCanGetReward, 0, tree_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldTree_GatherCanUp, 0, tree_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldTree_HightBoxReward, 0, tree_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldTree_OutPutGoldReachedTheLimit, 0, tree_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldTree_BossCanHit, 3, tree_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldTree_BossCanHit, 4, tree_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldTree_BossCanHit, 5, tree_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldTree_GatherCanUnLock, 0, tree_btn.transform);
            // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldTree_CanGrowUp, 0, tree_btn.transform);
            // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.WorldTree_CanHarvest, 0, tree_btn.transform);
            RedDotReminderHandle.AddTransReminder(RedDotHandleID.WorldTree_CanHarvest, 0, tree_btn_harvest);


            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_DailyMissionArena_ID, 0, playinfo_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.DailyMissionForArena, 0, playinfo_btn.transform);
            //RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_DailyMissionShenMo_ID, 0, playinfo_btn.transform);

            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Shop_DailyMissionTravel_ID, 0, experience_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.DailyMissionForTravel, 0, experience_btn.transform);

            // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.AdventureBook_Monster_ID, 0, shoucang_btn.transform);
            // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.AdventureBook_Npc_ID, 0, shoucang_btn.transform);
            // RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.AdventureBook_Collection_ID, 0, shoucang_btn.transform);


            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Friend_Assist, 0, zhuzhan_btn.transform);


            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.ShenmoField_MilitaryRank_ID, 0, shenmoZhanchang_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.ShenmoField_MilitaryRankReward_ID, 0, shenmoZhanchang_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.ShenmoField_BossShop_ID, 0, shenmoZhanchang_btn.transform);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.ShenmoField_AlignBossAppear_ID, 0, shenmoZhanchang_btn.transform);            
        }

        private void RegisterGuide()
        {
            // Guide.BindRectransform(1030, lottery_btn.transform as RectTransform);
            Guide.BindRectransform(1010, team_btn.transform as RectTransform);
            Guide.BindRectransform(1009, other_btn.transform as RectTransform);
            Guide.BindRectransform(1011, hero_btn.transform as RectTransform);
            Guide.BindRectransform(1007, experience_btn.transform as RectTransform);
            Guide.BindRectransform(1012, equip_btn.transform as RectTransform);
            Guide.BindRectransform(1014, daoju_btn.transform as RectTransform);
            Guide.BindRectransform(1015, Forge_btn.transform as RectTransform);
            Guide.BindRectransform(1016, union_btn.transform as RectTransform);
            Guide.BindRectransform(1018, task_btn.transform as RectTransform); 
            Guide.BindRectransform(1050, peiyang_btn.transform as RectTransform); 
            Guide.BindRectransform(1006, copy_btn.transform as RectTransform);
            Guide.BindRectransform(1008, playinfo_btn.transform as RectTransform);
            //Guide.BindRectransform(1008, playinfo_btn.transform as RectTransform);
            Guide.BindRectransform(1016, union_btn.transform as RectTransform);
            //Guide.BindRectransform(1028, ZhuXian_btn.transform as RectTransform); 
            Guide.BindRectransform(1029, shop_btn.transform as RectTransform);
            Guide.BindRectransform(1039, bag_btn.transform as RectTransform);
            // Guide.BindRectransform(1040, box_btn.transform as RectTransform);
            Guide.BindRectransform(1049, chouka_2_btn.transform as RectTransform);

            Guide.BindRectransform(1041, yewai_btn.transform as RectTransform);
            // Guide.BindRectransform(1047, equipbox_btn.transform as RectTransform);
            Guide.BindRectransform(1048, boss_btn.transform as RectTransform);
            Guide.BindRectransform(1043, miracle_btn.transform as RectTransform);
            Guide.BindRectransform(22000, shoucang_btn.transform as RectTransform);
            Guide.BindRectransform(21000, tree_btn.transform as RectTransform);
            Guide.BindRectransform(106001, horse_btn.transform as RectTransform);
            Guide.BindRectransform(1053, huodong_btn.transform as RectTransform);
            
        }

        private void RegisterModuleUnlock()
        {
            ModuleUnlock.RegisterRectTrans(ModuleId.Union, union_btn.transform as RectTransform, 29);
            ModuleUnlock.RegisterRectTrans(ModuleId.QianQiFuFei, vip_panel.transform as RectTransform, 11000);
            ModuleUnlock.RegisterRectTrans(ModuleId.QianQiFuFei, huodongshoufang_btn.transform as RectTransform, 11001);
            ModuleUnlock.RegisterRectTrans(ModuleId.Furnace, Forge_btn.transform as RectTransform, 1015);
            ModuleUnlock.RegisterRectTrans(ModuleId.Hallow, hallows_btn.transform as RectTransform, 1013);
            ModuleUnlock.RegisterRectTrans(ModuleId.HeroCompose, heroComposeBtn.transform as RectTransform, 6010);
            // ModuleUnlock.RegisterRectTrans(ModuleId.)
            ModuleUnlock.RegisterRectTrans(ModuleId.Equipments, equip_btn.transform as RectTransform, 1012);
            ModuleUnlock.RegisterRectTrans(ModuleId.Team, team_btn.transform as RectTransform, 1010);
            ModuleUnlock.RegisterRectTrans(ModuleId.Shop, shop_btn.transform as RectTransform, 8002);
            //ModuleUnlock.RegisterRectTrans(ModuleId.Call, shop_btn.transform as RectTransform, 9);
            ModuleUnlock.RegisterRectTrans(ModuleId.Other, other_btn.transform as RectTransform, 1009);
            ModuleUnlock.RegisterRectTrans(ModuleId.Wings, chibang_panel.transform as RectTransform, 1021);
         //   ModuleUnlock.RegisterRectTrans(ModuleId.FashionCloth, shizhuang_btn.transform as RectTransform, 1020);
            ModuleUnlock.RegisterRectTrans(ModuleId.Hero, hero_btn.transform as RectTransform, 1011);
         //   ModuleUnlock.RegisterRectTrans(ModuleId.DailyTask, task_btn.transform as RectTransform, 1018);
            ModuleUnlock.RegisterRectTrans(ModuleId.Rank, paihang_btn.transform as RectTransform, 1024);
         //   ModuleUnlock.RegisterRectTrans(ModuleId.Title, chenghao_panel.transform as RectTransform, 1038);
            ModuleUnlock.RegisterRectTrans(ModuleId.Mails, email_btn.transform as RectTransform, 1033);
            ModuleUnlock.RegisterRectTrans(ModuleId.SignIn, signin_btn.transform as RectTransform, 1032);
            ModuleUnlock.RegisterRectTrans(ModuleId.Decorate, peiyang_btn.transform as RectTransform, 1050);
            ModuleUnlock.RegisterRectTrans(ModuleId.Portrait, touxiang_panel.transform as RectTransform,1025);  

            // ModuleUnlock.RegisterRectTrans(ModuleId.Call, lottery_btn.rectTransform(), 1047);
            // ModuleUnlock.RegisterRectTrans(ModuleId.Box, box_btn.transform as RectTransform, 1040);
            // ModuleUnlock.RegisterRectTrans(ModuleId.Equip, equipbox_btn.rectTransform(), 1047);
            // ModuleUnlock.RegisterRectTrans(ModuleId.Inscription, inscription_btn.rectTransform(), 1047);

            ModuleUnlock.RegisterRectTrans(ModuleId.ChouKa, chouka_2_btn.rectTransform(), 1047);
            ModuleUnlock.RegisterRectTrans(ModuleId.MapStage, copy_btn.transform as RectTransform, 1006);
            ModuleUnlock.RegisterRectTrans(ModuleId.DailyTask, task_btn.rectTransform(), 1018);
            ModuleUnlock.RegisterRectTrans(ModuleId.Trial, experience_btn.rectTransform(), 1041);

            ModuleUnlock.RegisterRectTrans(ModuleId.Field, yewai_btn.rectTransform(), 1041);
            ModuleUnlock.RegisterRectTrans(ModuleId.Challenge, playinfo_btn.rectTransform(), 1041);
            ModuleUnlock.RegisterRectTrans(ModuleId.GettingStronger, Bianqiang_btn.transform as RectTransform, 1027);
            ModuleUnlock.RegisterRectTrans(ModuleId.Friend, haoyou_btn.transform as RectTransform, 1042);

            ModuleUnlock.RegisterRectTrans(ModuleId.RiskBook, shoucang_btn.transform as RectTransform, 1043);
            ModuleUnlock.RegisterRectTrans(ModuleId.WorldTree, tree_btn.transform as RectTransform, 1044);
            ModuleUnlock.RegisterRectTrans(ModuleId.Miracle, miracle_btn.transform as RectTransform, 65);
            ModuleUnlock.RegisterRectTrans(ModuleId.FriendAssist, zhuzhan_btn.transform as RectTransform, 71);

            ModuleUnlock.RegisterRectTrans(ModuleId.RECOMMENDED, recommended_btn.transform as RectTransform, 2000);

            ModuleUnlock.RegisterRectTrans(ModuleId.Horse, horse_panel.transform as RectTransform, 58100);
            ModuleUnlock.RegisterRectTrans(ModuleId.BOSSCollect, boss_btn.transform as RectTransform, 70);
            ModuleUnlock.RegisterRectTrans(ModuleId.ShenmoZhanchang, shenmoZhanchang_btn.transform as RectTransform, 72);
            for (int i = 0; i < sevenday_btns.Count; i++)
            {
                ModuleUnlock.RegisterRectTrans(ModuleId.QiRiZunXiang, sevenday_btns[i].btn.transform as RectTransform, 1002 + i);
            }

        }

        private void UnRegisterModuleUnlock()
        {
            ModuleUnlock.UnRegisterRectTrans(ModuleId.RECOMMENDED, 2000);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Union, 29);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.QianQiFuFei, 11000);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.QianQiFuFei, 11001);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Furnace, 1015);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Hallow, 1013);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.HeroCompose, 6010);

            ModuleUnlock.UnRegisterRectTrans(ModuleId.Equipments, 1012);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Team, 1010);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Shop, 8002);

            ModuleUnlock.UnRegisterRectTrans(ModuleId.Other, 1009);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Wings, 1021);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.FashionCloth, 1020);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Hero, 1011);

            ModuleUnlock.UnRegisterRectTrans(ModuleId.Rank, 1024);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Title, 1038);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Portrait,1025);  
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Mails, 1033);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.SignIn, 1032);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Decorate, 1050);

            ModuleUnlock.UnRegisterRectTrans(ModuleId.Call, 1047);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Box, 1040);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Equip, 1047);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Inscription, 1047);

            ModuleUnlock.UnRegisterRectTrans(ModuleId.ChouKa, 1047);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.MapStage, 1006);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.DailyTask, 1018);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Trial, 1041);

            ModuleUnlock.UnRegisterRectTrans(ModuleId.Field, 1041);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Challenge, 1041);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.GettingStronger, 1027);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Friend, 1042);

            ModuleUnlock.UnRegisterRectTrans(ModuleId.RiskBook, 1043);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.WorldTree, 1044);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.Miracle, 65);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.FriendAssist, 71);

            ModuleUnlock.UnRegisterRectTrans(ModuleId.Horse, 58100);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.BOSSCollect, 70);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.ShenmoZhanchang, 72);
            for (int i = 0; i < sevenday_btns.Count; i++)
            {
                ModuleUnlock.UnRegisterRectTrans(ModuleId.QiRiZunXiang, 1002 + i);
            }
        }

        private void RemoveRedDotHandle()
        {
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Arena_Challenge_ID, 0, playinfo_btn.transform);

            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Season_SaiJiReward, 0, playinfo_btn.transform, spring_btn.transform);
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Season_UpGrade, 0, playinfo_btn.transform, spring_btn.transform);
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Season_WeekTask, 0, playinfo_btn.transform, spring_btn.transform);
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Season_MonthTask, 0, playinfo_btn.transform, spring_btn.transform);

            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.ShenmoPower_CanChallenge_ID, 0, playinfo_btn.transform);
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.ShenmoPower_CanReset_ID, 0, playinfo_btn.transform);
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.GoldCopy_CanChallenge_ID, 0, playinfo_btn.transform);
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.ExpCopy_CanChallenge_ID, 0, playinfo_btn.transform);
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.KingWayMobai_ID, 0, playinfo_btn.transform);
            

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Map_Chest_ID, 0, copy_btn.transform, spring_btn.transform);
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.DailyMissionForDepthBoss, 0, copy_btn.transform, spring_btn.transform);
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_DailyMissionDepthBossID, 0, copy_btn.transform, spring_btn.transform);


            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Travel_BoxReward_ID, 0, experience_btn.transform, spring_btn.transform);
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.AutoTravel_ID, 0, experience_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Travel_QiYu_ID,0,experience_btn.transform, spring_btn.transform);

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Hero_Combine_ID, 0, hero_btn.transform, spring_btn.transform);


            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Role_Equip_Combine_ID, 0, equip_btn.transform, spring_btn.transform, bag_hongdian_panel.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Partner_Equip_Combine_ID, 0, equip_btn.transform, spring_btn.transform, bag_hongdian_panel.transform);


            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Slot_GemAdd_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Slot_RuneAdd_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Slot_RuneCanStrength_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Slot_RuneCanAdvance_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Slot_Equip_AdvanceUp_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Slot_Equip_CanEquipNew_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Slot_Equip_Enhance_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Slot_Equip_StarUp_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Hallow_Equip_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Hallow_Advance_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Hallow_Upgrade_ID, 0, team_btn.transform, spring_btn.transform); 
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Hallow_LianJin_ID, 0 , team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Hallow_Awake_ID, 0 , team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Hallow_ChangBetterHallow_ID, 0, team_btn.transform, spring_btn.transform);

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.EquipSuit_RoleCanCombineSuit, 0, team_btn.transform, spring_btn.transform);



            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Hero_Combine_ID, 0, hero_btn.transform, spring_btn.transform, bag_hongdian_panel.transform);

  
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Partner_StarUp_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Partner_AdvanceUp_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Partner_SkillUp_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Partner_LevelUp_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Partner_NewHeroCanAdd_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Partner_YuanjunAdd_ID, 0, team_btn.transform, spring_btn.transform);
            // RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Partner_GetNewSkin_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Partner_Quality_ID, 0, team_btn.transform, spring_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Partner_StarFate_ID, 0, team_btn.transform, spring_btn.transform);

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Talisman_All_ID, 0, team_btn.transform, spring_btn.transform); 


            {   
                // RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.MoLong_ID,0,maolongbaozang_btn.transform,huodongshoufang_btn.transform);

                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Sign_Main_ID, 0, signin_btn.transform,huodongshoufang_btn.transform, huodong_btn.transform);

                // RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.LuckDialActivity_ID, 0, xinyunlunpan_btn.transform, huodongshoufang_btn.transform, huodong_btn.transform);

                //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.BOSS_PERSONAL_BOSS, 0, boss_btn.transform,huodongshoufang_btn.transform);
                //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.BOSS_VIP_BOSS, 0, boss_btn.transform,huodongshoufang_btn.transform);
                //// RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.BOSS_WILD_BOSS, 0, boss_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.BOSS_World_BOSS, 0, boss_btn.transform, huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.WorldBossMobai_ID, 0, boss_btn.transform, huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.DailyMissionForWorldBoss, 0, boss_btn.transform, huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_DailyMissionWorldBoss_ID, 0, boss_btn.transform, huodongshoufang_btn.transform);


                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_Hero_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_Honor_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_Courage_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_Hallow_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_Union_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_EquipBlue_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_EquipPurple_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_EquipOranges1_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_EquipOranges2_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_AccessoryPurple_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_AccessoryOranges1_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_AccessoryOranges2_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_Theater_ID, 0, shop_btn.transform,huodongshoufang_btn.transform);

                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Niudan_Diamon_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Niudan_Glod_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.NiuDan_Glod_STONE_DEBRIS, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.NiuDan_Diamon_STONE_DEBRIS, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Niudan_RenZu_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Niudan_ShenZu_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Niudan_MoZu_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                

                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Niudan_Talisman_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Niudan_Hallow_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Niudan_Equip_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Niudan_HeroEquip_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Niudan_Inscription_Gold_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Niudan_Inscription_DIAMOND_ID, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);

                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.NiuDan_Activity_RedId, 0, chouka_2_btn.transform,huodongshoufang_btn.transform);


                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Activity_SevenDay_ID, 0, sevenDay_btn.transform,huodongshoufang_btn.transform);
                RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Activity_SevenDay_ID, 1, sevenDay1_btn.transform,huodongshoufang_btn.transform);

                for(int i = 0 ; i < sevenday_btns.Count; i++) {
                    int reddotId = LuaScriptMgr.Instance.CallLuaFunction_Int_Int("water.role.qiRiZunXiang.getRedDotID",i+1);        
                    RedDotReminderHandle.RemoveRedDotReminder(reddotId, 0, sevenday_btns[i].btn.transform,huodongshoufang_btn.transform);
                }


                for(int i = 0 ; i < firstcharge_btns.Count; i++) {
                    int _redId = LuaScriptMgr.Instance.CallLuaFunction_Int_Int("water.role.activity.GetRedDotId",i+1);     
                    if(_redId != 0)
                        RedDotReminderHandle.RemoveRedDotReminder(_redId, 0, firstcharge_btns[i].btn.transform);

                }


            }


			RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Task_Main_ID, 0, task_btn.transform);

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Union_CanDonate_ID, 0, union_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Union_NewPlayerApply_ID, 0, union_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Union_Office_Task, 0, union_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Union_Office_RewardBox, 0, union_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.UnionMobai_ID, 0, union_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Union_Office_Friend, 0, union_btn.transform);

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Mail_Main_ID, 0, email_btn.transform, other_btn.transform.GetChild(3));
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Furnace_CanDecompose_ID,0,Forge_btn.transform,peiyang_btn.transform);

			//RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Chat_Main_ID, 0, chat_btn.transform);
 

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Hallow_Combine_ID, 0, hallows_btn.transform, spring_btn.transform, bag_hongdian_panel.transform);

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Vip_BuyGift_ID, 0, roleinfo_com.vip_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Vip_BuyGift_ID, 1, roleinfo_com.vip_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Vip_BuyGift_ID, 2, roleinfo_com.vip_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Vip_BuyGift_ID, 0, recharge_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Vip_BuyGift_ID, 1, recharge_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Vip_BuyGift_ID, 2, recharge_btn.transform);


            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Wing_Combine_ID, 0, wing_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Wing_StarUP_ID, 0, wing_btn.transform, peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Wing_LevelUP_ID, 0, wing_btn.transform, peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Wing_Advance_ID, 0, wing_btn.transform, peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Fashion_Combine_ID, 0, shizhuang_btn.transform, peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Fashion_Combine_ID, 1, shizhuang_btn.transform, peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Fashion_Combine_ID, 2, shizhuang_btn.transform, peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Fashion_Combine_ID, 3, shizhuang_btn.transform, peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Fashion_LevelUP_ID, 0, shizhuang_btn.transform, peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Fashion_StarUP_ID, 0, shizhuang_btn.transform, peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Fashion_Advance_ID, 0, shizhuang_btn.transform, peiyang_btn.transform);

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Title_NewGet_ID, 0, chenghao_btn.transform, peiyang_btn.transform);
            // RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Title_CanActive_ID, 0, chenghao_btn.transform, peiyang_btn.transform);

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Portrait_CanActive_ID, 0, touxiang_btn.transform);

            // for(int i = 1; i < 7 ;i++)
            // {
            //     RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Bag_BoxItem_ID, i, daoju_btn.transform, spring_btn.transform, bag_hongdian_panel.transform);
            // }
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Bag_MainUIRed_ID, 0, daoju_btn.transform, spring_btn.transform, bag_hongdian_panel.transform);


            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Friends_Invitation_ID, 0, haoyou_btn.transform, other_btn.transform.GetChild(3));
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Friends_Energy_ID, 0, haoyou_btn.transform, other_btn.transform.GetChild(3));
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Friends_LikeInfoLog_ID, 0, haoyou_btn.transform, other_btn.transform.GetChild(3));

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.AdventureBook_Story_ID, 0, shoucang_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.AdventureBook_Monster_ID, 0, shoucang_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.AdventureBook_Npc_ID, 0, shoucang_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.AdventureBook_Collection_ID, 0, shoucang_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.AdventureBook_Fashion_ID, 0, shoucang_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.AdventureBook_Wing_ID, 0, shoucang_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.AdventureBook_Horse_ID, 0, shoucang_btn.transform,peiyang_btn.transform);

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Miracle_BlessTabCanUpgrade_ID, 0, miracle_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Paragon_Level_RedDot, 0, miracle_btn.transform,peiyang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Miracle_NewBlessItemUnlocked_ID, 0, miracle_btn.transform,peiyang_btn.transform);

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Wild_Main_ID, 0, yewai_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Wild_All_MapQuest_ID,0, yewai_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_DailyMissionWild_ID, 0, yewai_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.DailyMissionForWild, 0, yewai_btn.transform);
			// RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.WorldBossMobai_ID, 0, yewai_btn.transform);
            // RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.DailyMissionForWorldBoss, 0, yewai_btn.transform);
            // RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_DailyMissionWorldBoss_ID, 0, yewai_btn.transform);



            // RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.WorldTree_CanAdv, 0, tree_btn.transform);s
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.WorldTree_GatherCanGetReward, 0, tree_btn.transform);
            //     RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.WorldTree_GatherCanUnLock, 0, tree_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.WorldTree_HightBoxReward, 0, tree_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.WorldTree_OutPutGoldReachedTheLimit, 0, tree_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.WorldTree_BossCanHit, 3, tree_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.WorldTree_BossCanHit, 4, tree_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.WorldTree_BossCanHit, 5, tree_btn.transform);
            // RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.WorldTree_CanGrowUp, 0, tree_btn.transform);
            // RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.WorldTree_CanHarvest, 0, tree_btn.transform);
            RedDotReminderHandle.RemoveTransReminder(RedDotHandleID.WorldTree_CanHarvest, 0, tree_btn_harvest);

            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_DailyMissionArena_ID, 0, playinfo_btn.transform);
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.DailyMissionForArena, 0, playinfo_btn.transform);
            //RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_DailyMissionShenMo_ID, 0, playinfo_btn.transform);

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Shop_DailyMissionTravel_ID, 0, experience_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.DailyMissionForTravel, 0, experience_btn.transform);


            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Friend_Assist, 0, zhuzhan_btn.transform);

            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.ShenmoField_MilitaryRank_ID, 0, shenmoZhanchang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.ShenmoField_MilitaryRankReward_ID, 0, shenmoZhanchang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.ShenmoField_BossShop_ID, 0, shenmoZhanchang_btn.transform);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.ShenmoField_AlignBossAppear_ID, 0, shenmoZhanchang_btn.transform);
        }

        private void UnRegisterGuide()
        {
            Guide.UnBindRectransform(1030);
            Guide.UnBindRectransform(1010);
            Guide.UnBindRectransform(1009);
            Guide.UnBindRectransform(1011);
            Guide.UnBindRectransform(1007);
            Guide.UnBindRectransform(1012);
            Guide.UnBindRectransform(1014);
            Guide.UnBindRectransform(1015);
            Guide.UnBindRectransform(1018);
            Guide.UnBindRectransform(1016);
            Guide.UnBindRectransform(1006);
            Guide.UnBindRectransform(1008);
            //Guide.UnBindRectransform(1008);
            Guide.UnBindRectransform(1016);
            Guide.UnBindRectransform(1050);
            //Guide.UnBindRectransform(1028);
            Guide.UnBindRectransform(1029);
            Guide.UnBindRectransform(1039);
            Guide.UnBindRectransform(1040);
            Guide.UnBindRectransform(1049);
            Guide.UnBindRectransform(1041);
            Guide.UnBindRectransform(1043);
            Guide.UnBindRectransform(1048);
            Guide.UnBindRectransform(22000);
            Guide.UnBindRectransform(21000);
            Guide.UnBindRectransform(106001);
            Guide.UnBindRectransform(1053);
            
        }

     

		List<GameObject> _qiRiZunXiangSFX = new List<GameObject>();
		void showQiRiZunXiangSFX(bool show_,int id){
			if (show_) {
				if (id >= _qiRiZunXiangSFX.Count) {
					Transform sfxTrans = TransformUtil.FindChildAccurately (this.transform, "EFF_UI_MainUIXuanZhuan");
                    GameObject obj = Instantiate (sfxTrans.gameObject);
					obj.transform.SetParent (sevenday_btns[id].btn.transform, false);
					obj.transform.SetSiblingIndex (1);
					obj.transform.localScale = new Vector3 (1f, 1f, 1f);
					obj.SetActive (true);
                    _qiRiZunXiangSFX.Add(obj);
				} else {
					_qiRiZunXiangSFX[id].SetActive (true);
				}
			} else {
                if(id < _qiRiZunXiangSFX.Count)
                {
                    if (_qiRiZunXiangSFX[id]) {
                        _qiRiZunXiangSFX[id].SetActive (false);
                    }
                }
			}
		}

		void checkQiRiZunXiangSFX(int id){
			bool hasActivity = LuaScriptMgr.Instance.CallLuaFunction_Bool_Int("water.role.qiRiZunXiang.hasValidActivity",id+1);
			if (hasActivity) {
				bool isActive = LuaScriptMgr.Instance.CallLuaFunction_Bool_Int("water.role.qiRiZunXiang.isActivityActive",id+1);
				if (!isActive)
					showQiRiZunXiangSFX (true,id);
				else
					showQiRiZunXiangSFX (false,id);
			} else {
				showQiRiZunXiangSFX (false,id);
			}
		}

		// List<RedDotDisplay> _qiRiZunXiangRedDot = new List<RedDotDisplay>();
		// void showQiRiZunXiangRedDot(bool show_,int id){
        //     if (_qiRiZunXiangRedDot.Count <= id)
        //         _qiRiZunXiangRedDot.Add(sevenday_btns[id].btn.gameObject.AddComponent<RedDotDisplay> ());
		// 	if (show_) {
		// 		_qiRiZunXiangRedDot[id].forceShowDot = true;
		// 	} else {
		// 		if (_qiRiZunXiangRedDot[id]) {
		// 			_qiRiZunXiangRedDot[id].forceShowDot = false;
		// 		}
		// 	}
		// }

		// void checkQiRiZunXiangRedDot(int id){
		// 	bool hasActivity = LuaScriptMgr.Instance.CallLuaFunction_Bool_Int("water.role.qiRiZunXiang.hasValidActivity",id+1);
		// 	if (hasActivity) {
		// 		bool hasReward = LuaScriptMgr.Instance.CallLuaFunction_Bool_Int("water.role.qiRiZunXiang.hasClaimableRewards",id+1);
		// 		if (hasReward) {
		// 			showQiRiZunXiangRedDot (true,id);
		// 		} else {
		// 			showQiRiZunXiangRedDot (false,id);
		// 		}
		// 	} else {
		// 		showQiRiZunXiangRedDot (false,id);
		// 	}
		// }

		void onQiRiZunXiangUpdated(){
            for( int i = 0 ; i < sevenday_btns.Count ; i++)
            {
                checkQiRiZunXiangSFX (i);
                // checkQiRiZunXiangRedDot (i); 
                refreshQiRiZunXiangBtn(i);
            }
		}
    }
}
 