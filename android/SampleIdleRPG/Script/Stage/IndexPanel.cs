using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;


public class IndexPanel : BaseMonoBehaviour
{
    public enum Menu
    {
        LeaguePanel,
        WelfarePanel,
        HunterPanel,
        GodnessPanel,
        QuestAndHeroPanel,
        HerosPanel,
        HerosPanel_Formation,
        HerosPanel_ExpPanel,
        HerosPanel_StonePanel,
        HerosPanel_EvolutionPanel,
        HerosPanel_GemPanel,
        CollectPanel,
        BagPanel,
        EquipPanel,
        MagicAndProfilePanel,
        CallPanel,
        MysteryPanel,
        BottomMask,
        FirendFightPanel,
        ArenaPanel,
        LadderPanel,
        MergePanel,
        MergePanel_EquipMergy,
        EquipExchangePanel,
        MergePanel_HeroMergy,
        EmailPanel,
        FirendPanel,
        ChatPanel,
        SettingPanel,
        SignInPanel,
        MallPanel,
        ActivityPanel,
        NextDayPanel,
        MealPanel,
        SevenDayPanel,
        FortuneCatPanel,
        SharePanel,
        TargetPanel,
        ActivePanel,
        InputKeyPanel,
        VipPanel,
        VersionPanel,
        FirstDayPanel,
        MerchantPanel,
        LeagueWarPanel,
        MazePanel1,
        MazePanel2,
        MazePanel3,
        ChangeMajorPanel,
        HeroMajorPanel1,

        RechargePanel,
        RechargeReturnPanel,
        ColorDiamonWelfarePanel,
        LimitWelfarePanel,
        FirstWelfarePanel,
        DailyPanel,
        DailyActivePanel,
        SevenDaysPanel,
        TavernPanel,
        AdventureGuidePanel,
        KnightPanel,
        MagicGirlPanel,
        ActForeverPanel,
        ActSpecialPanel,
        ActGoldheroPanel,
        ActRedpacketPanel,
        LandWarFirstPanel,
        ActPayMorePanel,
        ActPayOncePanel,
        ActMallPanel,
        ActHeroBankPanel,
        LuckCardPanel,
        CardCollectPanel,
        QQVip,
        QQVPlus,
        knightMarriagePanel,
        UpdateWelfarePanel,
        QuestionnairePanel,
        KnightWarPanel,
        KnightWarShopPanel,
        ActMallSpecialPanel,
        ActGoldPanel,
        ActHeroLimitPanel,
        WorldWarPanel,
        Wow3SciencePanel,
        Wow3WorkshopPanel,
        RuneSetPanel,
        RuneEcmPanel,
        RuneDevPanel,
        RuneDecPanel,
        Wow3ChatPanel,
        ActFestivalPanel,
        AnniversaryPanel
    }
    
//    public UIButton wildBtn;
//    public UIButton cityBtn;
    public UIButton megyBtn;
    public UIButton firendsBtn;
    public UIButton leagueBtn;
    public UIButton packBtn;
    public UIButton listBtn;
    public UIButton equipBtn;
    public UIButton taskBtn;

    public UIButton activityBtn;

    public UISpriteAnimation secEffect;
    public UIButton secBtn;
    public UIButton leagueWarBtn;
    public UIButton heroBtn;
    public UIButton challengeBtn;
    public UIButton recuitBtn;
    public UIButton mazeBtn;
    public UIButton NftBtn;
    public GameObject worldWarBtn;

    public GameObject leagueWarTxtObj;
    public GameObject activityTxtObj;
    public GameObject heroTxtObj;
    public GameObject challengeTxtObj;
    public GameObject recuitTxtObj;

    public UIButton arenaBtn;
    public UIButton arena2Btn;
    public UIButton firendFightBtn;
    public UIButton kingwarBtn;

    public GameObject roleBtn;
    public GameObject changeBtn;
    public UIButton chatBtn;
    public UIButton emailBtn;
    public UIButton systemBtn;
    public UIButton shopBtn;
    public UIButton signBtn;
//    public UIButton screenCloseBtn;

    public UIButton addGoldBtn;
    public UIButton addCoinBtn;
    public UIButton WalletBtn;

    public UIButton ShowGoldTooltipBtn;
    public UIButton ShowCoinTooltipBtn;
    public UIButton ShowTicketTooltipBtn;

    public UIGrid giftGrid;
    public GameObject giftBtn;
    public GameObject giftBtn1;
    public GameObject giftBtn2;
    public GameObject giftBtn3;
    public GameObject giftBtn4;
    public GameObject giftBtn5;
    public GameObject giftBtn6;
    public GameObject giftBtn7;
    public GameObject giftBtn8;
    public GameObject giftBtn9;
    public GameObject giftBtn10;
    public GameObject giftBtn11;
    public GameObject giftBtn12;
    public GameObject giftBtn13;
    public GameObject giftBtn14;
    public GameObject giftBtn15;
    public GameObject giftBtn16;
    public GameObject giftBtn17;
    public GameObject giftBtn18;
    public GameObject giftBtn19;
    public GameObject giftBtn20;
    public GameObject giftBtn21;
    public GameObject giftBtn22;
    public GameObject giftBtn23;
    public GameObject giftBtn24;
    public GameObject giftBtn25;
    public GameObject giftBtn26;
    public GameObject giftBtn27;
    public GameObject giftBtn28;
    public GameObject giftBtn29;
    public GameObject giftBtn30;
    public UILabel limitTimeTxt;
    public UILabel firstTimeTxt;

    //JET
    public GameObject giftBtnYouyiAd;

    public TipSign roleTipSign;

    public TipSign activityTipSign;
    
    public TipSign packTipSign;
    public TipSign callTipSign;
    public TipSign heroTipSign;
    public TipSign challengeTipSign;
    public TipSign arenaTipSign;
    public TipSign arena2TipSign;
    public TipSign firendFightSign;
    public TipSign kingWarTipSign;
    public TipSign collectTipSign;

    public TipSign emailTipSign;
    public TipSign friendTipSign;
    public TipSign vipTipSign;

    public TipSign storyTipSign;
    public TipSign magicTipSign;

    public TipSign equipTipSign;
    public TipSign mergyTipSign;
    public TipSign leagueTipSign;
    public TipSign leagueWarTipSign;
    public TipSign secTipSign;
    public TipSign mazeTipSign;

    public TipSign dailyTipSign;

    public GameObject bottomMask;

    public GameObject questTip;
    public UILabel questTxt;

    public TipSign chatTipSign;
    public TipSign redPacketTipSign;

    public GameObject HerosPanel_Formation;
    public GameObject HerosPanel_ExpPanel;
    public GameObject HerosPanel_StonePanel;
    public GameObject HerosPanel_EvolutionPanel;
    public GameObject HerosPanel_GemPanel;

    public GameObject tipPanels;
    public GameObject tipPanel;
    public bool inHideMovie = false ;

    private Hashtable levelMenu;
    private Hashtable tipTimeList;

    private setting_info dungeon_no_hint_num;

    private List<GameObject> giftList;
    private List<GameObject> showList;
    private List<Vector3> oldPosList;

    private bool showMazeTip2;
    private bool showMazeTip3;
    private bool isMenuShow = false;
    private bool isInitGiftBtn = true;
    public bool isInActivity = false;

    private string Coin;
    private string gold;
    private string ticket;
    private string stamp;

    private UILabel equipBtnlabel;
    private UILabel packBtnlabel;
    private UILabel taskBtnlabel;
    private UILabel listBtnlabel;
    private UILabel leagueBtnlabel;
    private UILabel megyBtnlabel;
    private UILabel secBtnlabel;
    
    private UILabel recuitBtnlabel;
    
    private UILabel heroBtnlabel;
    private UILabel activityBtnlabel;
    private UILabel leagueWarBtnlabel;
    private UILabel challengeBtnlabel;



    // Awake is called when the script instance
    // is being loaded.
    public void Awake()
    {
        
        //        UIEventListener.Get(wildBtn.gameObject).onClick = onClick;
        //        UIEventListener.Get(cityBtn.gameObject).onClick = onClick;
        UIEventListener.Get(worldWarBtn).onClick = onClick;
        UIEventListener.Get(mazeBtn.gameObject).onClick = onClick;
        UIEventListener.Get(megyBtn.gameObject).onClick = onClick;
        UIEventListener.Get(firendsBtn.gameObject).onClick = onClick;
        UIEventListener.Get(leagueBtn.gameObject).onClick = onClick;
        UIEventListener.Get(packBtn.gameObject).onClick = onClick;
        UIEventListener.Get(NftBtn.gameObject).onClick = onClick;
        
        UIEventListener.Get(listBtn.gameObject).onClick = onClick;
        UIEventListener.Get(taskBtn.gameObject).onClick = onClick;
        

        UIEventListener.Get(activityBtn.gameObject).onClick = onClick;

        UIEventListener.Get(secBtn.gameObject).onClick = onBottomClick;
        UIEventListener.Get(equipBtn.gameObject).onClick = onBottomClick;
        UIEventListener.Get(heroBtn.gameObject).onClick = onBottomClick;
        UIEventListener.Get(challengeBtn.gameObject).onClick = onBottomClick;
        UIEventListener.Get(recuitBtn.gameObject).onClick = onBottomClick;
        UIEventListener.Get(arenaBtn.gameObject).onClick = onBottomClick;
        UIEventListener.Get(arena2Btn.gameObject).onClick = onBottomClick;
        UIEventListener.Get(firendFightBtn.gameObject).onClick = onBottomClick;
        UIEventListener.Get(kingwarBtn.gameObject).onClick = onBottomClick;
        UIEventListener.Get(leagueWarBtn.gameObject).onClick = onBottomClick;

        UIEventListener.Get(roleBtn).onClick = onTopClick;
        UIEventListener.Get(changeBtn).onClick = onTopClick;
        UIEventListener.Get(chatBtn.gameObject).onClick = onTopClick;
        UIEventListener.Get(emailBtn.gameObject).onClick = onTopClick;
        UIEventListener.Get(systemBtn.gameObject).onClick = onTopClick;
        UIEventListener.Get(shopBtn.gameObject).onClick = onTopClick;
        UIEventListener.Get(signBtn.gameObject).onClick = onTopClick;
//        UIEventListener.Get(screenCloseBtn.gameObject).onClick = onTopClick;

        UIEventListener.Get(addGoldBtn.gameObject).onClick = onTopClick;
        UIEventListener.Get(addCoinBtn.gameObject).onClick = onTopClick;
        UIEventListener.Get(WalletBtn.gameObject).onClick = onTopClick;

        UIEventListener.Get(ShowGoldTooltipBtn.gameObject).onPress = OnPress;
        UIEventListener.Get(ShowCoinTooltipBtn.gameObject).onPress = OnPress;
        UIEventListener.Get(ShowTicketTooltipBtn.gameObject).onPress = OnPress;

        UIEventListener.Get(giftBtn).onClick = onGiftClick;
        UIEventListener.Get(giftBtn1).onClick = onGiftClick;
        UIEventListener.Get(giftBtn2).onClick = onGiftClick;
        UIEventListener.Get(giftBtn3).onClick = onGiftClick;
        UIEventListener.Get(giftBtn4).onClick = onGiftClick;
        UIEventListener.Get(giftBtn5).onClick = onGiftClick;
        UIEventListener.Get(giftBtn6).onClick = onGiftClick;
        UIEventListener.Get(giftBtn7).onClick = onGiftClick;
        UIEventListener.Get(giftBtn8).onClick = onGiftClick;
        UIEventListener.Get(giftBtn9).onClick = onGiftClick;
        UIEventListener.Get(giftBtn10).onClick = onGiftClick;
        UIEventListener.Get(giftBtn11).onClick = onGiftClick;
        UIEventListener.Get(giftBtn12).onClick = onGiftClick;
        UIEventListener.Get(giftBtn13).onClick = onGiftClick;
        UIEventListener.Get(giftBtn14).onClick = onGiftClick;
        UIEventListener.Get(giftBtn15).onClick = onGiftClick;
        UIEventListener.Get(giftBtn16).onClick = onGiftClick;
        UIEventListener.Get(giftBtn17).onClick = onGiftClick;
        UIEventListener.Get(giftBtn18).onClick = onGiftClick;
        UIEventListener.Get(giftBtn19).onClick = onGiftClick;
        UIEventListener.Get(giftBtn20).onClick = onGiftClick;
        UIEventListener.Get(giftBtn21).onClick = onGiftClick;
        UIEventListener.Get(giftBtn22).onClick = onGiftClick;
        UIEventListener.Get(giftBtn23).onClick = onGiftClick;
        UIEventListener.Get(giftBtn24).onClick = onGiftClick;
        UIEventListener.Get(giftBtn25).onClick = onGiftClick;
        UIEventListener.Get(giftBtn26).onClick = onGiftClick;
        UIEventListener.Get(giftBtn27).onClick = onGiftClick;
        UIEventListener.Get(giftBtn28).onClick = onGiftClick;
        UIEventListener.Get(giftBtn29).onClick = onGiftClick;
        UIEventListener.Get(giftBtn30).onClick = onGiftClick;
        //JET
        UIEventListener.Get(giftBtnYouyiAd).onClick = onGiftClick;

        #region 버튼 등록
        giftList = new List<GameObject>();
        giftList.Add(giftBtn1);
        giftList.Add(giftBtn2);
        giftList.Add(giftBtn3);
        giftList.Add(giftBtn4);
        giftList.Add(giftBtn5);
        giftList.Add(giftBtn6);
        giftList.Add(giftBtn7);
        giftList.Add(giftBtn8);
        giftList.Add(giftBtn9);
        giftList.Add(giftBtn10);
        giftList.Add(giftBtn11);
        giftList.Add(giftBtn12);
        giftList.Add(giftBtn13);
        giftList.Add(giftBtn14);
        giftList.Add(giftBtn15);
        giftList.Add(giftBtn16);
        giftList.Add(giftBtn17);
        giftList.Add(giftBtn18);
        giftList.Add(giftBtn19);
        giftList.Add(giftBtn20);
        giftList.Add(giftBtn21);
        giftList.Add(giftBtn22);
        giftList.Add(giftBtn23);
        giftList.Add(giftBtn24);
        giftList.Add(giftBtn25);
        giftList.Add(giftBtn26);
        giftList.Add(giftBtn27);
        giftList.Add(giftBtn28);
        giftList.Add(giftBtn29);
        giftList.Add(giftBtn30);
        #endregion

        var open = !PlayerPrefsUtility.HasKey(GlobalTools.UserInfoScreen) || PlayerPrefsUtility.GetBool(GlobalTools.UserInfoScreen);
        chatBtn.gameObject.SetActive(open);

        bottomMask.SetActive(false);
        questTip.SetActive(false);

        #region 데이터 셋
        if (Socket)
        {
            Data.activity.bind("wonderTip", showActivityTip, true);
            Data.activity.bind("mealTip", showActivityTip, true);
            Data.role.bind("act2", showAct7Tip, true);
            Data.role.bind("act7", showAct7Tip, true);
            Data.role.bind("vip_gift", showAct7Tip, true);
            Data.role.bind("act_meal", showAct7Tip, true);
            Data.role.bind("act_target", showAct7Tip, true);
            Data.role.bind("act_active", showDailyTip, true);
            Data.role.bind("act_version", showAct7Tip, true);
            Data.role.bind("league_gift", showLeagueTip, true);
            Data.role.bind("league_pray", showLeagueTip, true);
            Data.role.bind("league_boss", showLeagueTip, true);
            Data.role.bind("league_donate", showLeagueTip, true);
            Data.role.bind("friendwar", showChallengeTip);
            Data.role.bind("rich", showRichTip, true);
            Data.role.bind("act_timelimit",showGiftBtn6, true);
            Data.role.bind("act_crazy7", showGiftBtn3, true);

            Data.mergy.bind("equipTip", showMergyTip, true);
            Data.mergy.bind("heroTip", showMergyTip, true);

            Data.hero.bind("stoneTip", showHeroTip, true);
            Data.hero.bind("evolutionTip", showHeroTip, true);
            Data.hero.bind("formationTip", showHeroTip, true);
            Data.hero.bind("gemTip", showHeroTip, true);
            Data.equip.bind("majorTip", showHeroTip, true);
            Data.bag.bind("foodTip", showHeroTip, true);
            Data.bag.bind("pieceTip", showBagTip, true);

            Data.quest.bind("doneCount", showQuestTip);
            Data.hero.bind("storyTip", showQuestTip);
            Data.equip.bind("hasMergyNew", showEquipTip);
            Data.collect.bind("collectTip", showCollectTip);

            Data.role.bind("maze1", showMazeTip, true);
            
            Socket.addCallBack(CMD.ActFirstPay_update, onFirstUpdate);
            Socket.addCallBack(CMD.ActCountdown_reward, onActCountdown);
            Socket.addCallBack(CMD.ActMeal_new, onMealUpdate);
            Socket.addCallBack(CMD.Union_beinvite, onBeInvite);
            Socket.addCallBack(CMD.Union_pass, onUnionPass);
            Socket.addCallBack(CMD.ActCatchup_info, onActCatchupInfo);
            Socket.addCallBack(CMD.ActGoldenHero_getinfo, onActGoldenHeroInfo);
            Socket.addCallBack(CMD.Chat_all, onHistoryMsg);
            Socket.addCallBack(CMD.ActPayMore_notify, onPayMoreNotify); 
            Socket.addCallBack(CMD.ActPayOnce_notify, onPayOnceNotify);
            Socket.addCallBack(CMD.ActHeroBank_notify, onHeroBankNotify);
            Socket.addCallBack(CMD.Union_getInfo, onUnionGetInfo);
            Socket.addCallBack(CMD.ActGoldSign_notify, onGoldSignNotify);

            main.gameGuide.bind(heroBtn.gameObject, new ArrayList { 60001, 70001, 80006, 13004, 22001, 106002, 120002, 121002, 150007 }, new Vector2(0, 50));
            main.gameGuide.bind(recuitBtn.gameObject, new ArrayList { 80001, 13001, 150002 }, new Vector2(0, 50));
            main.gameGuide.bind(taskBtn.gameObject, new ArrayList { 16001 }, new Vector2(0, 50));
            main.gameGuide.bind(challengeBtn.gameObject, new ArrayList { 15001, 21001, 116001 }, new Vector2(0, 50));
            main.gameGuide.bind(activityBtn.gameObject, new ArrayList { 14001 }, new Vector2(0, 50));

            main.gameGuide.bind(roleBtn.gameObject, new ArrayList { 900012 });
            main.gameGuide.bind(secBtn.gameObject, new ArrayList { 17001 });
            main.gameGuide.bind(leagueBtn.gameObject, new ArrayList { 12001 });
            
            main.gameGuide.bind(arenaBtn.gameObject, new ArrayList { 15002 });
            main.gameGuide.bind(megyBtn.gameObject, new ArrayList { 18001, 24001 });
            main.gameGuide.bind(listBtn.gameObject, new ArrayList { 19001 });
            main.gameGuide.bind(firendFightBtn.gameObject, new ArrayList { 21002 });
            main.gameGuide.bind(arena2Btn.gameObject, new ArrayList { 116002 });
            main.gameGuide.bind(leagueBtn.gameObject, new ArrayList { 119001 });

            main.gameGuide.bind(mazeBtn.gameObject, new ArrayList { 130001 });
        }
        #endregion

        //        cityBtn.gameObject.SetActive(!GlobalTools.isBanshu);
        //        cityBtn.gameObject.SetActive(false);
        mazeBtn.gameObject.SetActive(!GlobalTools.isBanshu);

        //2025.02.05
        //레드마인 #623
        //addCoinBtn.gameObject.SetActive(false);
#if UNITY_EDITOR
        addCoinBtn.gameObject.SetActive(true);
#endif 

        if (GlobalTools.isBanshu)
        {
            taskBtn.normalSprite = "examinebutton3";
            taskBtn.pressedSprite = "examinebutton3click";
            UISprite rSp = recuitTxtObj.GetComponent<UISprite>();
            rSp.spriteName = "examinebutton1";
            rSp.MakePixelPerfect();
            UISprite aSp = activityTxtObj.GetComponent<UISprite>();
            aSp.spriteName = "examinebutton2";
            aSp.MakePixelPerfect();
            changeBtn.gameObject.SetActive(false);
            shopBtn.gameObject.SetActive(true);
        }

        clipTipTime();


        #region ui 텍스트 테이블 연결
        equipBtnlabel = equipBtn.transform.Find("Label").GetComponent<UILabel>();        equipBtnlabel.text = Manager.Language.GetString("indexpanel.homeBtns.equipBtn.Label");
        packBtnlabel = packBtn.transform.Find("Label").GetComponent<UILabel>();        packBtnlabel.text = Manager.Language.GetString("indexpanel.homeBtns.packBtn.Label");
        taskBtnlabel = taskBtn.transform.Find("Label").GetComponent<UILabel>();        taskBtnlabel.text = Manager.Language.GetString("indexpanel.homeBtns.taskBtn.Label");
        listBtnlabel = listBtn.transform.Find("Label").GetComponent<UILabel>(); listBtnlabel.text = Manager.Language.GetString("indexpanel.homeBtns.listBtn.Label");
        leagueBtnlabel = leagueBtn.transform.Find("Label").GetComponent<UILabel>(); leagueBtnlabel.text = Manager.Language.GetString("indexpanel.homeBtns.leagueBtn.Label");
        megyBtnlabel = megyBtn.transform.Find("Label").GetComponent<UILabel>(); megyBtnlabel.text = Manager.Language.GetString("indexpanel.homeBtns.megyBtn.Label");
        secBtnlabel = secBtn.transform.Find("Label").GetComponent<UILabel>(); secBtnlabel.text = Manager.Language.GetString("indexpanel.homeBtns.secBtn.Label");

        recuitBtnlabel = recuitBtn.transform.Find("Label").GetComponent<UILabel>(); recuitBtnlabel.text = Manager.Language.GetString("indexpanel.homeBtns.recuitBtnlabel.Label");
        heroBtnlabel = heroBtn.transform.Find("Label").GetComponent<UILabel>(); heroBtnlabel.text = Manager.Language.GetString("indexpanel.homeBtns.heroBtn.Label");
        activityBtnlabel = activityBtn.transform.Find("Label").GetComponent<UILabel>(); activityBtnlabel.text = Manager.Language.GetString("indexpanel.homeBtns.activityBtn.Label");
        leagueWarBtnlabel = leagueWarBtn.transform.Find("Label").GetComponent<UILabel>(); leagueWarBtnlabel.text = Manager.Language.GetString("indexpanel.homeBtns.leagueWarBtn.Label");
        challengeBtnlabel = challengeBtn.transform.Find("Label").GetComponent<UILabel>(); challengeBtnlabel.text = Manager.Language.GetString("indexpanel.homeBtns.challengeBtn.Label");
        #endregion

    }

    public void OnDestroy()
    {
        if (Socket)
        {
            Data.activity.unbind("wonderTip", showActivityTip);
            Data.activity.unbind("mealTip", showActivityTip);
            Data.role.unbind("act2", showAct7Tip);
            Data.role.unbind("act7", showAct7Tip);
            Data.role.unbind("vip_gift", showAct7Tip);
            Data.role.unbind("act_meal", showAct7Tip);
            Data.role.unbind("act_target", showAct7Tip);
            Data.role.unbind("act_active", showDailyTip);
            Data.role.unbind("act_version", showAct7Tip);
            Data.role.unbind("league_gift", showLeagueTip);
            Data.role.unbind("league_pray", showLeagueTip);
            Data.role.unbind("league_boss", showLeagueTip);
            Data.role.unbind("league_donate", showLeagueTip);
            Data.role.unbind("friendwar", showChallengeTip);
            Data.role.unbind("ladder", showChallengeTip);
            Data.role.unbind("rich", showRichTip);
            Data.role.unbind("act_timelimit", showGiftBtn6);
            Data.role.unbind("act_crazy7", showGiftBtn3);

            Data.mergy.unbind("equipTip", showMergyTip);
            Data.mergy.unbind("heroTip", showMergyTip);

            Data.hero.unbind("stoneTip", showHeroTip);
            Data.hero.unbind("evolutionTip", showHeroTip);
            Data.hero.unbind("formationTip", showHeroTip);
            Data.hero.unbind("gemTip", showHeroTip);
            Data.equip.unbind("majorTip", showHeroTip);
            Data.bag.unbind("foodTip", showHeroTip);
            Data.bag.unbind("pieceTip", showBagTip);

            Data.quest.unbind("doneCount", showQuestTip);
            Data.hero.unbind("storyTip", showQuestTip);
            Data.equip.unbind("hasMergyNew", showEquipTip);
            Data.collect.unbind("collectTip", showCollectTip);

            Data.role.unbind("maze1", showMazeTip);

            Socket.removeCallBack(CMD.ActFirstPay_update, onFirstUpdate);
            Socket.removeCallBack(CMD.ActCatchup_info, onActCatchupInfo);
            Socket.removeCallBack(CMD.ActCountdown_reward, onActCountdown);
            Socket.removeCallBack(CMD.ActMeal_new, onMealUpdate);
            Socket.removeCallBack(CMD.Union_beinvite, onBeInvite);
            Socket.removeCallBack(CMD.Union_pass, onUnionPass);
            Socket.removeCallBack(CMD.ActGoldenHero_getinfo, onActGoldenHeroInfo);
            Socket.removeCallBack(CMD.Chat_all, onHistoryMsg);
            Socket.removeCallBack(CMD.ActPayMore_notify, onPayMoreNotify);
            Socket.removeCallBack(CMD.ActPayOnce_notify, onPayOnceNotify);
            Socket.removeCallBack(CMD.ActHeroBank_notify, onHeroBankNotify);
            Socket.removeCallBack(CMD.Union_getInfo, onUnionGetInfo);
            Socket.removeCallBack(CMD.ActGoldSign_notify, onGoldSignNotify);

            main.gameGuide.unbind(heroBtn.gameObject);
            main.gameGuide.unbind(recuitBtn.gameObject);
            main.gameGuide.unbind(taskBtn.gameObject);
            main.gameGuide.unbind(roleBtn.gameObject);
            main.gameGuide.unbind(secBtn.gameObject);
            main.gameGuide.unbind(leagueBtn.gameObject);
            main.gameGuide.unbind(challengeBtn.gameObject);
            main.gameGuide.unbind(arenaBtn.gameObject);
            main.gameGuide.unbind(megyBtn.gameObject);
            main.gameGuide.unbind(listBtn.gameObject);
            main.gameGuide.unbind(firendFightBtn.gameObject);
            main.gameGuide.unbind(arena2Btn.gameObject);
            main.gameGuide.unbind(leagueWarBtn.gameObject);
        }
        
    }

    private void updateCoin(object value)
    {
        Coin = value.ToString();// "" + StringUtil.formatCount((long)value);
    }
    private void updateGold(object value)
    {
        gold = value.ToString();//"" + StringUtil.formatCount((int)value);
    }
    private void updateTicket(object value)
    {
        ticket = value.ToString();// "" + StringUtil.formatCount((int)value);
    }
    private void updatestamp(object value)
    {
        stamp = value.ToString();// "" + StringUtil.formatCount((int)value);
    }

    private void onGoldSignNotify(JsonData msg) 
    {
        IDictionary dic = msg["data"];
        int pro = 0;
        int recharge = 0;
        int active = 0;
        if (dic.Contains("pro"))
        {
            pro = (int)msg["data"]["pro"];
        }
        if (dic.Contains("recharge"))
        {
            recharge = (int)msg["data"]["recharge"];
        }
        if (dic.Contains("active"))
        {
            active = (int)msg["data"]["active"];
        }

        if (pro == 1) 
        {
            Data.role.act_goldsign |= (1 << (4 - 1)); 
        }
        if (recharge == 1)
        {
            Data.role.act_goldsign |= (1 << (6 - 1)); 
        }
        if (active == 1) 
        {
            Data.role.act_goldsign |= (1 << (5 - 1)); 
        }
        int _tip27 = Convert.ToInt32(Data.role.act_goldsign >> (6 - 1) & 1) +
            Convert.ToInt32(Data.role.act_goldsign >> (4 - 1) & 1) +
            Convert.ToInt32(Data.role.act_goldsign >> (5 - 1) & 1);
        giftBtn27.transform.Find("tipNum").GetComponent<TipSign>().show(_tip27);
    }

    private void onUnionGetInfo(JsonData msg)
    {
        string id = msg["data"]["id"].ToString();
        leagueWarTipSign.hide();
        if (!string.IsNullOrEmpty(id))
        {
            main.showKnightPanel();
        }
        else
        {
            main.showKnightApplyPanel();
        }
    }

    private void showMazeTip(object value)
    {
        if (levelMenu == null)
        {
            return;
        }
        if (!showMazeTip2)
        {
            Data.role.maze2 = 0;
        }
        if (!showMazeTip3)
        {
            Data.role.maze3 = 0;
        }

        mazeTipSign.show(Data.role.maze1 + Data.role.maze2 + Data.role.maze3 , mazeBtn.isEnabled);

    }

    private void showCollectTip(object value)
    {
        collectTipSign.show((int)value, listBtn.isEnabled);
    }

    private void showEquipTip(object value)
    {
        equipTipSign.show((int)value, equipBtn.isEnabled);
    }

    private void showBagTip(object value)
    {
        packTipSign.show((int)value, packBtn.isEnabled);
    }

    private void showQuestTip(object value)
    {
        storyTipSign.show((int) value, taskBtn.isEnabled);
    }

    private void showRichTip(object value)
    {
        showRichEffect();
    }
    private void showRichEffect()
    {
        if (dungeon_no_hint_num == null)
        {
            dungeon_no_hint_num = (setting_info)Cache.SettingInfo["dungeon_no_hint_num"];
        }
        bool show = Data.role.rich >= Convert.ToInt32(dungeon_no_hint_num.value);
        secTipSign.show(show ? 1 : 0, secBtn.isEnabled);
        secEffect.gameObject.SetActive(secBtn.isEnabled && show);
        if (secBtn.isEnabled && show)
        {
            secBtn.normalSprite = "Mining Button2";
        }
        else
        {
            secBtn.normalSprite = "Mining Button";
        }
    }
    private void showGiftBtn6(object value) 
    {
        if (limitTimeTxt.text == "00:00:00" && Data.role.act_timelimit == 0)
        {
            giftBtn6.SetActive(false);
            main.indexPanel.giftGrid.Reposition();
        }
        else if (limitTimeTxt.text != "00:00:00" && Data.role.act_timelimit == 0)
        {
            giftBtn6.SetActive(true);
            giftBtn6.transform.Find("ring").gameObject.SetActive(false);
        }
        else
        {
            giftBtn6.SetActive(true);
            giftBtn6.transform.Find("ring").gameObject.SetActive(true);
        }
    }

    private void showGiftBtn3(object value) 
    {
        setting_info sInfo2 = (setting_info)Cache.SettingInfo["act_seven_real_deadline"];
        int time3 = ((Data.role.reg_time + (8 * 3600)) / 3600 / 24) * 3600 * 24;
        int time2 = Convert.ToInt32(sInfo2.value) * 3600 * 24 + time3 - (8 * 3600);
        if (Data.role.local_time >= time2)
        {
            giftBtn3.SetActive(false);
        }
        else 
        {
            //giftBtn3.SetActive(true);
            giftBtn3.transform.Find("tipNum").GetComponent<TipSign>().show(Data.role.act_crazy7);
        }
        giftGrid.Reposition();
    }

    private void showChallengeTip(object value)
    {
        challengeTipSign.show(Data.role.ladder + Data.role.friendwar + Data.role.kingwar, challengeBtn.isEnabled);
        arenaTipSign.show(Data.role.ladder, arenaBtn.isEnabled);
        arena2TipSign.show(Data.role.arena_tips, arena2Btn.isEnabled);
        kingWarTipSign.show(Data.role.kingwar, kingwarBtn.isEnabled);
        firendFightSign.show(Data.role.friendwar, firendFightBtn.isEnabled);
    }
    private void showLeagueTip(object value)
    {
        leagueTipSign.show((int)value, leagueBtn.isEnabled);
    }

    private void showHeroTip(object value)
    {
        int heroTipCount = 0;
        heroTipCount += Data.hero.formationTip + Data.equip.majorTip;
        menus menu = (menus)Cache.MenusList["HerosPanel_StonePanel"];
        if (Data.role.level >= menu.level)
        {
            heroTipCount += Data.hero.stoneTip;
        }
        menu = (menus)Cache.MenusList["HerosPanel_ExpPanel"];
        if (Data.role.level >= menu.level)
        {
            heroTipCount += Data.bag.foodTip;
        }
        menu = (menus)Cache.MenusList["HerosPanel_EvolutionPanel"];
        if (Data.role.level >= menu.level)
        {
            heroTipCount += Data.hero.evolutionTip;
        }
        menu = (menus)Cache.MenusList["HerosPanel_GemPanel"];
        if (Data.role.level >= menu.level)
        {
            heroTipCount += Data.hero.gemTip;
        }
        heroTipSign.show(heroTipCount, heroBtn.isEnabled);
    }
    private void showMergyTip(object value)
    {
        mergyTipSign.show(Data.mergy.equipTip + Data.mergy.heroTip, megyBtn.isEnabled);
    }

    private void onFirstUpdate(JsonData msg)
    {
        Data.activity.firstTip = 1;
        activityTipSign.show(1, activityBtn.isEnabled);
    }

    private void onPayMoreNotify(JsonData msg) 
    {
        Data.role.paymore = 2;
        giftBtn16.transform.Find("tipNum").GetComponent<TipSign>().show(1);
    }

    private void onPayOnceNotify(JsonData msg) 
    {
        Data.role.payonce = 2;
        giftBtn17.transform.Find("tipNum").GetComponent<TipSign>().show(1);
    }

    private void onHeroBankNotify(JsonData msg) 
    {
        Data.role.herobank = 2;
        giftBtn19.transform.Find("tipNum").GetComponent<TipSign>().show(1);
    }

    private void onHistoryMsg(JsonData msg)
    {
        Data.chat.initAllChatList(msg["data"]["list"]);
        main.adventrue.showTalkMsgHistory();
    }

    public void onActCatchupInfo(JsonData msg)
    {
        Data.activity.initActCatchupInfo(msg["data"]);
        if (Data.activity.actCatchupInfo.level == 0)
        {
            giftBtn10.SetActive(false);
            giftGrid.Reposition();
            Socket.removeCallBack(CMD.ActCatchup_info, onActCatchupInfo);
        }
        else 
        {
            giftBtn10.SetActive(true);
            giftGrid.Reposition();
        }
    }

    public void onActGoldenHeroInfo(JsonData msg) 
    {
        Data.activity.initActGoldHero(msg["data"]);
    }

    private void onActCountdown(JsonData msg)
    {
        Data.role.act_countdown = 2;
        
        initGiftBtn();
    }
    private void onMealUpdate(JsonData msg) 
    {
        Data.role.update("act_meal", 1);
    }

    private void showAct7Tip(object value)
    {
        if (isInActivity)
        {
            activityTipSign.hide();
            return;
        }
        int count = (int)value;
        if (main.indexPanel.activityTipSign.gameObject.activeSelf == false)
        {
            activityTipSign.show(count, activityBtn.isEnabled && main.activityPanel.gameObject.activeSelf == false);
        }
    }

    private void showDailyTip(object value)
    {
        int count = (int)value;
        dailyTipSign.show(count, giftBtn8.activeSelf);
    }


    private void showActivityTip(object value)
    {
        if (isInActivity)
        {
            activityTipSign.hide();
            return;
        }
        int count = (int)value;
        if ( main.indexPanel.activityTipSign.gameObject.activeSelf == false)
        {
            activityTipSign.show(count, activityBtn.isEnabled && main.activityPanel.gameObject.activeSelf == false);
        }
    }

    private void onBeInvite(JsonData msg)
    {
        Data.role.union_invite = 1;
        checkKnightTip();
    }

    

    private void onUnionPass(JsonData msg)
    {
        Data.role.union_id = msg["data"]["id"].ToString();
        leagueWarTipSign.show(1, leagueWarBtn.isEnabled && leagueWarBtn.gameObject.activeSelf);
    }

    public void checkKnightTip() 
    {
        if (!string.IsNullOrEmpty(Data.role.union_id))
        {
            if (Data.role.union_apply == 1 || Data.role.union_home == 1 || Data.role.union_race == 1)
            {
                leagueWarTipSign.show(1, leagueWarBtn.isEnabled && leagueWarBtn.gameObject.activeSelf);
            }
            else
            {
                leagueWarTipSign.hide();
            }
        }
        else 
        {
            if (Data.role.union_invite == 1)
            {
                leagueWarTipSign.show(1, leagueWarBtn.isEnabled && leagueWarBtn.gameObject.activeSelf);
            }
            else 
            {
                leagueWarTipSign.hide();
            }
        }
        
    }

    public void OnEnable()
    {
        if (Data != null && Data.role != null)
        {
//            unKnownBtn.isEnabled = false;
            checkMenuOpen();
            checkTip();

            //reduceLimitTime();
            //InvokeRepeating("reduceLimitTime", 1, 1);

            Data.role.bind("coin", updateCoin, true);
            Data.role.bind("gold", updateGold, true);
            Data.role.bind("ticket", updateTicket, true);
            //Data.role.bind("stamp", updatestamp, true); 
        }
    }
    public void OnDisable()
    {
        if (Data != null && Data.role != null)
        {
            Data.role.unbind("coin", updateCoin);
            Data.role.unbind("gold", updateGold);
            Data.role.unbind("ticket", updateTicket);
            Data.role.unbind("stamp", updatestamp);
        }
    }

    private void reduceLimitTime() 
    {
        setting_info act_week_card_bonus_time = (setting_info)Cache.SettingInfo["act_week_card_bonus_time"];
        int showTime = Data.role.reg_time + (Convert.ToInt32(act_week_card_bonus_time.value) * 3600);
        int countTime = showTime - Data.role.local_time;
        if (countTime > 0)
        {
            limitTimeTxt.text = StringUtil.timeFormat(countTime, true);
        }
        else
        {
            limitTimeTxt.text = "00:00:00";
            if (Data.role.act_timelimit == 0)
            {
                giftBtn6.SetActive(false);
                main.indexPanel.giftGrid.Reposition();
            }
            CancelInvoke("reduceLimitTime");
        }
    }
    private void reduceFirstTime() 
    {
        setting_info act_countdown_time = (setting_info)Cache.SettingInfo["act_countdown_time"];
        int showTime = Data.role.reg_time + (Convert.ToInt32(act_countdown_time.value) * 60);
        int countTime = showTime - Data.role.local_time;
        if (countTime > 0)
        {
            firstTimeTxt.text = StringUtil.timeFormat(countTime, true);
        }
        if (countTime <= 0)
        {
            firstTimeTxt.text = "00:00:00";
            Data.role.act_countdown = 1;
            giftBtn7.transform.Find("tipNum").GetComponent<TipSign>().show(Data.role.act_countdown);
            CancelInvoke("reduceFirstTime");
        }
    }

    private void checkTip()
    {
        
        leagueTipSign.show(Data.role.league_gift + Data.role.league_boss + Data.role.league_donate + Data.role.league_pray, leagueBtn.isEnabled);
        int actTipCount = 0;
//        actTipCount += Data.role.act7;
//        actTipCount += Data.role.act2;
        actTipCount += Data.activity.firstTip;
        actTipCount += Data.activity.mealTip;
        actTipCount += Data.activity.wonderTip;
        actTipCount += Data.role.act_rank;
//        actTipCount += Data.role.vip_gift;
        actTipCount += Data.role.act_meal;
        actTipCount += Data.role.act_target;
//        actTipCount += Data.role.act_active;
        actTipCount += Data.role.act_version;
        if (GlobalTools.HasShare)
        {
            actTipCount += Data.role.act_share;
        }
        activityTipSign.show(actTipCount, activityBtn.isEnabled);

        dailyTipSign.show(Data.role.act_active, giftBtn8.activeSelf);

        equipTipSign.show(Data.equip.hasMergyNew, equipBtn.isEnabled);
        collectTipSign.show(Data.collect.collectTip, listBtn.isEnabled);
        arenaTipSign.show(Data.role.ladder, arenaBtn.isEnabled);
        arena2TipSign.show(Data.role.arena_tips, arena2Btn.isEnabled);
        firendFightSign.show(Data.role.friendwar, firendFightBtn.isEnabled);
        kingWarTipSign.show(Data.role.kingwar, kingwarBtn.isEnabled);
        challengeBtn.isEnabled = firendFightBtn.isEnabled || arenaBtn.isEnabled || arena2Btn.isEnabled || kingwarBtn.isEnabled;

        showMazeTip(1);

        int total = 0;
        if (arenaBtn.isEnabled)
        {
            total += Data.role.ladder;
        }
        if (firendFightBtn.isEnabled)
        {
            total += Data.role.friendwar;
        }
        if (kingwarBtn.isEnabled)
        {
            total += Data.role.kingwar;
        }
        challengeTipSign.show(total, challengeBtn.isEnabled);

        mergyTipSign.show(Data.mergy.equipTip + Data.mergy.heroTip, megyBtn.isEnabled);

        storyTipSign.show(Data.hero.storyTip + Data.quest.doneCount, taskBtn.isEnabled);

        int heroTipCount = 0;
        heroTipCount += Data.hero.formationTip + Data.equip.majorTip;
        menus menu = (menus)Cache.MenusList["HerosPanel_StonePanel"];
        if (Data.role.level >= menu.level)
        {
            heroTipCount += Data.hero.stoneTip;
        }
        menu = (menus)Cache.MenusList["HerosPanel_ExpPanel"];
        if (Data.role.level >= menu.level)
        {
            heroTipCount += Data.bag.foodTip;
        }
        menu = (menus)Cache.MenusList["HerosPanel_EvolutionPanel"];
        if (Data.role.level >= menu.level)
        {
            heroTipCount += Data.hero.evolutionTip;
        }
        menu = (menus)Cache.MenusList["HerosPanel_GemPanel"];
        if (Data.role.level >= menu.level)
        {
            heroTipCount += Data.hero.gemTip;
        }
        heroTipSign.show(heroTipCount, heroBtn.isEnabled);

        callTipSign.show(Data.call.freenum_1 + Data.call.freenum_2, recuitBtn.isEnabled);

        packTipSign.show(Data.bag.pieceTip, packBtn.isEnabled);
        showRichEffect();

        vipTipSign.show(Data.role.vip_tips, shopBtn.isEnabled);

        if (Data.role.act_countdown == 2)
        {
            giftBtn7.SetActive(false);
        }

        checkKnightTip();

        //처음에 한 번만 실행
        if (isInitGiftBtn)
	    {
            //Data.role.bind("act_timelimit", showGiftBtn6, true);bind

            giftBtn1.transform.Find("tipNum").GetComponent<TipSign>().show(Data.role.act_manygold);

            giftBtn7.transform.Find("tipNum").GetComponent<TipSign>().show(Data.role.act_countdown);
            //카운트다운 계산
            reduceLimitTime();
            InvokeRepeating("reduceLimitTime", 1, 1);

            reduceFirstTime();
            InvokeRepeating("reduceFirstTime", 1, 1);

            //checkTipPanel();
            //InvokeRepeating("checkTipPanel", 1, 1);

            giftBtn11.SetActive(giftBtn11.activeSelf && Data.role.mgirl_type != 0);
            UISprite sprite = giftBtn11.GetComponent<UISprite>();
            sprite.spriteName = "activitytype1" + (Data.role.mgirl_type - 1);

            giftBtn4.SetActive(giftBtn4.activeSelf && Data.role.beta == 1);

            giftBtn11.transform.Find("tipNum").GetComponent<TipSign>().show(Data.role.mgirl_tips);

            giftBtn12.SetActive(giftBtn12.activeSelf && Data.role.act_forever != 0);
            giftBtn12.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_forever == 2);

            giftBtn13.SetActive(giftBtn13.activeSelf && Data.role.act_special != 0);
            giftBtn13.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_special == 1);

            giftBtn14.SetActive(giftBtn14.activeSelf && Data.role.act_goldenhero != 0 );
            giftBtn14.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_goldenhero == 2);
            if (giftBtn14.activeSelf && Data.role.act_goldenhero != 0)
            {
                Socket.Send(CMD.ActGoldenHero_getinfo);
            }
            giftBtn15.SetActive(giftBtn15.activeSelf && Data.role.act_redpacket != 0);
            giftBtn15.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_redpacket == 2);

            giftBtn16.SetActive(giftBtn16.activeSelf && Data.role.paymore != 0);
            giftBtn16.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.paymore == 2);

            giftBtn17.SetActive(giftBtn17.activeSelf && Data.role.payonce != 0);
            giftBtn17.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.payonce == 2);

            giftBtn18.SetActive(giftBtn18.activeSelf && Data.role.act_mall != 0);
            giftBtn18.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_mall == 2);

            giftBtn19.SetActive(giftBtn19.activeSelf && Data.role.herobank != 0);
            giftBtn19.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.herobank == 2);

            giftBtn20.transform.Find("tipNum").GetComponent<TipSign>().show(Data.role.luckcard);

            giftBtn21.SetActive(giftBtn21.activeSelf && Data.role.collect_card != 0);
            giftBtn21.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.collect_card == 2);

            giftBtn23.SetActive(giftBtn23.activeSelf && Data.role.act_update != 0);
            giftBtn23.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_update == 1);

            giftBtn25.SetActive(giftBtn25.activeSelf && Data.role.vote != 0);
            giftBtn25.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.vote == 2);

            giftBtn26.SetActive(giftBtn26.activeSelf && Data.role.act_mallsp != 0);
            giftBtn26.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_mallsp == 2);

            if (Data.role.act_goldsign == 0)
            {
                giftBtn27.SetActive(false);
            }
            else if(giftBtn27.activeSelf)
            {
                giftBtn27.SetActive(true);
                if (Convert.ToInt32(Data.role.act_goldsign >> (2 - 1) & 1) == 1) 
                {
                    giftBtn27.GetComponent<UISprite>().spriteName = "activitytype31";
                }
                else if (Convert.ToInt32(Data.role.act_goldsign >> (1 - 1) & 1) == 1)
                {
                    giftBtn27.GetComponent<UISprite>().spriteName = "activitytype30";                    
                }
            }
            int _tip27 = 0;
            for (int i = 3; i <= 7; i++)
            {
                _tip27 += Convert.ToInt32(Data.role.act_goldsign >> (i - 1) & 1);
            }
            giftBtn27.transform.Find("tipNum").GetComponent<TipSign>().show(_tip27);

            giftBtn28.SetActive(giftBtn28.activeSelf && Data.role.act_herolimit != 0);
            giftBtn28.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_herolimit == 1);
            giftBtn29.SetActive(giftBtn29.activeSelf && Data.role.act_festival != 0);
            giftBtn29.transform.Find("tipNum").GetComponent<TipSign>().show(1, (Data.role.act_festival >> 0 & 1) > 0 || (Data.role.act_festival >> 0 & 2) > 0);

            giftBtn30.SetActive(giftBtn30.activeSelf && Data.role.act_anniversary != 0);
            giftBtn30.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_anniversary == 2);

            //JET
            giftBtnYouyiAd.SetActive(false);

            //redPacketTipSign.show(Data.role.redpacket, chatBtn.isEnabled);

            string historyMsg = PlayerPrefsUtility.HasKey(GlobalTools.ChatHistoryMsg) ? PlayerPrefsUtility.GetString(GlobalTools.ChatHistoryMsg) : "1,2,3,4";
            JsonData data = new JsonData();
            data["types"] = historyMsg;
            Socket.Send(CMD.Chat_all, data);
            //ES2.Save("1,2,3,4", GlobalTools.ChatHistoryMsg);

            isInitGiftBtn = false;

//            Data.role.qqvip = 2;
            menus qqMenu = (menus)Cache.MenusList["QQVip"];
            giftBtn22.SetActive(giftBtn22.activeSelf && Data.role.qqvip != 0 &&  qqMenu != null && Data.role.level >= qqMenu.level);
            giftBtn22.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.qqvip == 2);

            menus vMenu = (menus)Cache.MenusList["QQVPlus"];
            giftBtn24.SetActive(giftBtn24.activeSelf && Data.role.qqvplus != 0 && vMenu != null && Data.role.level >= vMenu.level);
            giftBtn24.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.qqvplus == 2);

            //setting_info wow_open_time = (setting_info)Cache.SettingInfo["wow_open_time"];
            //worldWarBtn.gameObject.SetActive(worldWarBtn.isEnabled && Convert.ToInt32(wow_open_time) <= Data.role.server_day);
            worldWarBtn.SetActive(worldWarBtn.activeSelf && Data.role.wow_open == 1);
	    }
    }

    private void onGiftClick(GameObject go)
    {
        SoundUtils.PlaySFX(SoundUtils.effect.click);
        if (go == giftBtn.gameObject)
        {
            showOrHideGift();
        }
        else if (go == giftBtn1.gameObject)
        {
            main.showColorDiamondPanel();
        }
        else if (go == giftBtn2.gameObject)
        {

        }
        else if (go == giftBtn3.gameObject)
        {
            main.showSevenDaysPanel();
        }
        else if (go == giftBtn4.gameObject)
        {
            main.showRechargeReturnPanel();
        }
        else if (go == giftBtn5.gameObject)
        {

        }
        else if (go == giftBtn6.gameObject)
        {
            main.showLimitwelfarePanel();
        }
        else if (go == giftBtn7.gameObject)
        {
            main.showFirstwelfarePanel();
        }
        else if (go == giftBtn8.gameObject)
        {
            main.showDailyPanel();
        }
        else if (go == giftBtn9.gameObject)
        {
            main.showAdventureGuidePanel();
        }
        else if (go == giftBtn10)
        {
            main.showQuickRisePanel();
        }
        else if (go == giftBtn11)
        {
            Data.role.mgirl_tips = 0;
            giftBtn11.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showMagicGirlPanel();
        }
        else if (go == giftBtn12)
        {
            Data.role.act_forever = 1;
            giftBtn12.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showActForeverPanel();
        }
        else if (go == giftBtn13)
        {
            Data.role.act_special = 2;
            giftBtn13.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showActSpecialPanel();
        }
        else if (go == giftBtn14)
        {
            Data.role.act_goldenhero = 1;
            giftBtn14.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showActGoldheroPanel();
        }
        else if (go == giftBtn15)
        {
            Data.role.act_redpacket = 1;
            giftBtn15.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showActRedpacketPanel();
        }
        else if (go == giftBtn16)
        {
            Data.role.paymore = 1;
            giftBtn16.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showActFourPanel(1);
        }
        else if (go == giftBtn17)
        {
            Data.role.payonce = 1;
            giftBtn17.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showActFourPanel(2);
        }
        else if (go == giftBtn18)
        {
            Data.role.act_mall = 1;
            giftBtn18.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showActFourPanel(3);
        }
        else if (go == giftBtn19)
        {
            Data.role.herobank = 1;
            giftBtn19.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showActFourPanel(4);
        }
        else if (go == giftBtn20)
        {
            Data.role.luckcard = 0;
            giftBtn20.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showLuckCardPanel();
        }
        else if (go == giftBtn21)
        {
            Data.role.collect_card = 1;
            giftBtn21.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showCardCollectPanel();
        }
        else if (go == giftBtn22)
        {
//#if UNITY_EDITOR
//        GlobalTools.OPEN_ID = "oDYQ6wsbKzZZu-8jrZglBvNTQWrA";
//        GlobalTools.OPEN_KEY = "IEymvqJkUpTPHhGsD7TscjgiWgxLjYP5opH2Hwv1_GsSq_j28EegzOPzgX5ovOMlfB0x6owrMqSfcRfkwuM_Awv9rnHj4W5EWA4I2n44Bq0";
//#endif

            string url = "" + GlobalTools.OPEN_ID + "&openkey=" + GlobalTools.OPEN_KEY + "&roleid=" + Data.role.uid + "&partition=" + Data.role.server_id + "&signature=" + getWebSign();
//            Application.OpenURL(url);
            print(url);
            Data.role.qqvip = 1;
            giftBtn22.transform.Find("tipNum").GetComponent<TipSign>().hide();

//            return;
           /* UniWebView webView = gameObject.GetComponent<UniWebView>();
            if (webView == null)
            {
                webView = gameObject.AddComponent<UniWebView>();
                webView.OnLoadComplete += OnLoadComplete;
                webView.OnReceivedKeyCode += OnKeyCode;
                webView.InsetsForScreenOreitation += InsetsForScreenOreitation;
            }
            

            webView.toolBarShow = true;
            webView.openLinksInExternalBrowser = true;
            webView.url = url;
            webView.Load();*/

        }
        else if (go == giftBtn24)
        {
            giftBtn24.transform.Find("tipNum").GetComponent<TipSign>().hide();
            //HoolaiSDK.instance.invokeSpecialFunction(1);
            Debug.Log("giftBtn24");
        }
        else if (go == giftBtn23)
        {
            Data.role.act_update = 2;
            giftBtn23.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showUpdateWelfarePanel();
        }
        else if (go == giftBtn25)
        {
            Data.role.vote = 1;
            giftBtn25.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showQuestionnairePanel();
        }
        else if (go == giftBtn26)
        {
            Data.role.act_mallsp = 1;
            giftBtn26.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showActFourPanel(5);
        }
        else if (go == giftBtn27)
        {
            giftBtn27.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showActGoldPanel();
        }
        else if (go == giftBtn28)
        {
            giftBtn28.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showActHeroLimitPanel();
        }
        else if (go == giftBtn29)
        {
            giftBtn29.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showActFestivalPanel();
        }
        else if (go == giftBtn30)
        {
            giftBtn30.transform.Find("tipNum").GetComponent<TipSign>().hide();
            main.showAnniversaryPanel();
        }

        else if (go == giftBtnYouyiAd)
        {
            main.showYouyiAdPanel();
        }
    }

    private string getWebSign()
    {
        Dictionary<string, string> signDic = new Dictionary<string, string>();
        signDic.Add("appid", "1105847180");
        signDic.Add("openid", GlobalTools.OPEN_ID);
        signDic.Add("openkey", GlobalTools.OPEN_KEY);
        signDic.Add("partition", Data.role.server_id + "");
        signDic.Add("roleid", Data.role.uid + "");

        const string key = "";

        string paramStr = "";
        var iter = signDic.GetEnumerator();
        while (iter.MoveNext())
        {
            paramStr += iter.Current.Key + "=" + iter.Current.Value + "&";
        }
        iter.Dispose();
        paramStr += key;
        print("key->" + paramStr);
        return LoginMsg.GetMD5(paramStr);
    }

    private void onTopClick(GameObject go)
    {
        SoundUtils.PlaySFX(SoundUtils.effect.click);
        if (go == signBtn.gameObject)
        {
            Socket.Send(CMD.Sign_getinfo);
            //            main.showSignIn();
        }
        else if (go == roleBtn)
        {
            main.showMagicAndProfilePanel();
            roleTipSign.hide();
        }
        else if (go == changeBtn)
        {
            main.showChangeMajorPanel();
        }
        else if (go == emailBtn.gameObject)
        {
            main.showEmailPanel();
            emailTipSign.hide();
        }
        else if (go == systemBtn.gameObject)
        {
            main.showSettingPanel();
        }
        else if (go == shopBtn.gameObject)
        {
            //            main.showMallPanel();
            main.showRechargePanel();
            vipTipSign.hide();
            Data.role.vip_tips = 0;
        }
        else if (go == chatBtn.gameObject)
        {
            main.showChatPanel();
            chatTipSign.hide();
        }
        //        else if (go == screenCloseBtn.gameObject)
        //        {
        //            ES2.Save(true, GlobalTools.UserInfoScreen);
        //            main.adventrue.closeScreen();
        //            screenCloseBtn.gameObject.SetActive(false);
        //            screenOpenBtn.gameObject.SetActive(true);
        //        }
        else if (go == addGoldBtn.gameObject)
        {
            main.showRechargePanel();
        }
        else if (go == WalletBtn.gameObject)
        {
            //2025.05.02    particle x
            //main.ShowWalletInfo();
        }
        else if (go == addCoinBtn.gameObject)
        {
            //2025.02.05
            //레드마인 #623
            main.showRechargePanel();
#if UNITY_EDITOR

//            guide info = (guide)CacheData.Instance.GuideList[150002];
//            main.gameGuide.showGuide(info);

//            Socket.Closed();
//            main.showLandWarFirstPanel();

            //main.showWorldWarPanel();
//            int soundId = Random.Range(1,31);
//            NGUITools.PlaySound(SoundManager.Load(soundId + ""));
//            SoundUtils.PlaySFX(soundId + "");
#else
            Tip.show(Manager.Language.GetString("IndexPanel.Tip"));
#endif

        }
        bottomMask.SetActive(false);
    }

    private void onClick(GameObject go)
    {
        SoundUtils.PlaySFX(SoundUtils.effect.click);

        if (go == activityBtn.gameObject)
        {
            if (GlobalTools.isBanshu)
            {
                main.showArena2Panel();
            }
            else
            {
                isInActivity = true;
                main.showActivityPanel();
            }
        }
//        else if (go == wildBtn.gameObject)
//        {
//            main.showAdventure();
//            
//        }
        else if (go == mazeBtn.gameObject)
        {
            main.showMazePanel();
        }
        else if (go == worldWarBtn.gameObject)
        {
            main.showWorldWarPanel();
        }
//        else if (go == cityBtn.gameObject)
//        {
//#if UNITY_EDITOR
////            Socket.showConnect();
//            main.showCityPanel();
////            main.gameGuide.checkShow("level:" + 25);
////            guide info = (guide)CacheData.Instance.GuideList[18001];
////            main.gameGuide.showGuide(info);
////            main.showOpenMoviePanel();
//#else
//            main.showCityPanel();
//#endif
////                        guide info = (guide)CacheData.Instance.GuideList[130001];
////            main.gameGuide.showGuide(info);
//           
//            
////            main.heroLevel.show((ArrayList)Data.hero.heroList.Clone());
////            main.SendTest2Message();
//        }
        else if (go == megyBtn.gameObject)
        {
            main.showMergePanel1();
            mergyTipSign.hide();
        }
        else if (go == firendsBtn.gameObject)
        {
            main.showFirendPanel();
            friendTipSign.hide();
        }
        else if (go == leagueBtn.gameObject)
        {
            if (Data.role.league_id == 0)
            {
                main.showLeagueSelectPanel();
            }
            else
            {
                main.showLeaguePanel();
            }
        }
        else if (go == packBtn.gameObject)
        {
            main.showBagPanel();
        }
        else if (go == NftBtn.gameObject)
        {
            main.showNFTBagPanel();
        }
        
        else if (go == listBtn.gameObject)
        {
            main.showCollectPanel();
            
        }
        else if (go == taskBtn.gameObject)
        {
            main.showQuestAndHeroPanel();
        }
        
        bottomMask.SetActive(false);
    }

    private void OnPress(GameObject go, bool state)
    {
        if(state)
        {
            if (go == ShowGoldTooltipBtn.gameObject)
            {
                string str = string.Format(GameManager.Instance.Language.GetString("Resource.Gold"), gold) ;
                UITooltip.Show(str);
            }
            else if (go == ShowCoinTooltipBtn.gameObject)
            {
                string str = string.Format(GameManager.Instance.Language.GetString("Resource.Coin"), Coin);
                UITooltip.Show(str);
            }
            else if (go == ShowTicketTooltipBtn.gameObject)
            {
                string str = string.Format(GameManager.Instance.Language.GetString("Resource.Ticket"), ticket);
                UITooltip.Show(str);
            }
        }
        else
        {
            UITooltip.Hide();
        }
        
    }

    private void onBottomClick(GameObject go)
    {
        SoundUtils.PlaySFX(SoundUtils.effect.clickStone);
        if (go == secBtn.gameObject)
        {
//            showQuestDone();
            main.showMysteryPanel();
//            main.awardGivePanel.show();
        }
        else if (go == equipBtn.gameObject)
        {
            main.showEquipPanel();
        }
        else if (go == heroBtn.gameObject)
        {
            main.showHerosPanel();
        }
        else if (go == challengeBtn.gameObject)
        {
            if (GlobalTools.isBanshu)
            {
                main.showArenaPanel();
            }
            else
            {
                showOrHideBottomMask();
                return;
            }
        }
        else if (go == recuitBtn.gameObject)
        {
//            main.showRecuitPanel();
            if (GlobalTools.isBanshu)
            {
                main.showFriendFightPanel();
            }
            else
            {
                main.showCallPanel();
            }
        }
        else if (go == arenaBtn.gameObject)
        {
            main.showArenaPanel();
        }
        else if (go == kingwarBtn.gameObject)
        {
            main.showKnightWarPanel();
        }
        else if (go == arena2Btn.gameObject)
        {
            main.showArena2Panel();
        }
        else if (go == firendFightBtn.gameObject)
        {
            main.showFriendFightPanel();
        }
        else if (go == leagueWarBtn.gameObject)
        {
            Socket.Send(CMD.Union_getInfo);
            
//            main.showLeagueWar();
        }
        bottomMask.SetActive(false);
    }

    private void showOrHideBottomMask()
    {
        bottomMask.SetActive(!bottomMask.activeSelf);
    }

    public void showQuestDone()
    {
        questTip.SetActive(true);
        var info = Data.quest.getDoneQuest();
        if (info != null)
        {
            questTxt.text = string.Format(Manager.Language.GetString("IndexPanel.questTxt"), info.info.title, Manager.Language.GetString("Quest.type." + info.info.type));
        }
        questTip.transform.localPosition = new Vector2(-640, 526);

        questTip.transform.DOLocalMoveX(0, 0.1f).OnComplete(() =>
        {
            questTip.transform.DOLocalMoveX(640, 0.1f).SetDelay(3).OnComplete(() =>
            {
                questTip.SetActive(false);
            });
        });

    }

    public void showWow3TargetDone(int id)
    {
        questTip.SetActive(true);
        wow_mission_info info = (wow_mission_info)Cache.WowMissonList[id];

        if (info != null)
        {
            questTxt.text = string.Format(Manager.Language.GetString("IndexPanel.targetTxt"), info.title);
        }
        questTip.transform.localPosition = new Vector2(-640, 526);

        questTip.transform.DOLocalMoveX(0, 0.1f).OnComplete(() =>
        {
            questTip.transform.DOLocalMoveX(640, 0.1f).SetDelay(3).OnComplete(() =>
            {
                questTip.SetActive(false);
            });
        });

    }

    public void checkMenuOpen()
    {
        if (Socket != null && levelMenu == null)
        {
            levelMenu = new Hashtable();
            levelMenu[Menu.ChangeMajorPanel.ToString()] = changeBtn.gameObject;
            levelMenu[Menu.LeaguePanel.ToString()] = leagueBtn.gameObject;
            levelMenu[Menu.KnightPanel.ToString()] = leagueWarBtn.gameObject;
            levelMenu[Menu.QuestAndHeroPanel.ToString()] = taskBtn.gameObject;
            levelMenu[Menu.HerosPanel.ToString()] = heroBtn.gameObject;
            levelMenu[Menu.BagPanel.ToString()] = packBtn.gameObject;
            levelMenu[Menu.EquipPanel.ToString()] = equipBtn.gameObject;
            levelMenu[Menu.MagicAndProfilePanel.ToString()] = roleBtn.gameObject;
            levelMenu[Menu.CallPanel.ToString()] = recuitBtn.gameObject;
            levelMenu[Menu.MysteryPanel.ToString()] = secBtn.gameObject;
            levelMenu[Menu.FirendFightPanel.ToString()] = firendFightBtn.gameObject;
            levelMenu[Menu.LadderPanel.ToString()] = arenaBtn.gameObject;
            levelMenu[Menu.ArenaPanel.ToString()] = arena2Btn.gameObject;
            levelMenu[Menu.KnightWarPanel.ToString()] = kingwarBtn.gameObject;
            levelMenu[Menu.MergePanel.ToString()] = megyBtn.gameObject;
            levelMenu[Menu.EmailPanel.ToString()] = emailBtn.gameObject;
            levelMenu[Menu.FirendPanel.ToString()] = firendsBtn.gameObject;
            levelMenu[Menu.ChatPanel.ToString()] = chatBtn.gameObject;
            levelMenu[Menu.SettingPanel.ToString()] = systemBtn.gameObject;
            levelMenu[Menu.MallPanel.ToString()] = shopBtn.gameObject;
            levelMenu[Menu.ActivityPanel.ToString()] = activityBtn.gameObject;
            levelMenu[Menu.CollectPanel.ToString()] = listBtn.gameObject;
            levelMenu[Menu.MazePanel1.ToString()] = mazeBtn.gameObject;
            levelMenu[Menu.WorldWarPanel.ToString()] = worldWarBtn;

//            levelMenu[Menu.HerosPanel_Formation.ToString()] = HerosPanel_Formation;
//            levelMenu[Menu.HerosPanel_ExpPanel.ToString()] = HerosPanel_ExpPanel;
//            levelMenu[Menu.HerosPanel_StonePanel.ToString()] = HerosPanel_StonePanel;
//            levelMenu[Menu.HerosPanel_EvolutionPanel.ToString()] = HerosPanel_EvolutionPanel;
//            levelMenu[Menu.HerosPanel_GemPanel.ToString()] = HerosPanel_GemPanel;

            levelMenu[Menu.RechargeReturnPanel.ToString()] = giftBtn4;
            levelMenu[Menu.ColorDiamonWelfarePanel.ToString()] = giftBtn1;
            levelMenu[Menu.LimitWelfarePanel.ToString()] = giftBtn6;
            levelMenu[Menu.FirstWelfarePanel.ToString()] = giftBtn7;
            levelMenu[Menu.DailyPanel.ToString()] = giftBtn8;
            levelMenu[Menu.SevenDaysPanel.ToString()] = giftBtn3;
            levelMenu[Menu.AdventureGuidePanel.ToString()] = giftBtn9;
            levelMenu[Menu.MagicGirlPanel.ToString()] = giftBtn11;
            levelMenu[Menu.ActForeverPanel.ToString()] = giftBtn12;
            levelMenu[Menu.ActSpecialPanel.ToString()] = giftBtn13;
            levelMenu[Menu.ActGoldheroPanel.ToString()] = giftBtn14;
            levelMenu[Menu.ActRedpacketPanel.ToString()] = giftBtn15;
            levelMenu[Menu.ActPayMorePanel.ToString()] = giftBtn16;
            levelMenu[Menu.ActPayOncePanel.ToString()] = giftBtn17;
            levelMenu[Menu.ActMallPanel.ToString()] = giftBtn18;
            levelMenu[Menu.ActHeroBankPanel.ToString()] = giftBtn19;
            levelMenu[Menu.LuckCardPanel.ToString()] = giftBtn20;
            levelMenu[Menu.CardCollectPanel.ToString()] = giftBtn21;
            levelMenu[Menu.QQVip.ToString()] = giftBtn22;
            levelMenu[Menu.UpdateWelfarePanel.ToString()] = giftBtn23;
            levelMenu[Menu.QQVPlus.ToString()] = giftBtn24;
            levelMenu[Menu.QuestionnairePanel.ToString()] = giftBtn25;
            levelMenu[Menu.ActMallSpecialPanel.ToString()] = giftBtn26;
            levelMenu[Menu.ActGoldPanel.ToString()] = giftBtn27;
            levelMenu[Menu.ActHeroLimitPanel.ToString()] = giftBtn28;
            levelMenu[Menu.ActFestivalPanel.ToString()] = giftBtn29;
            levelMenu[Menu.AnniversaryPanel.ToString()] = giftBtn30;

            Hashtable list = Cache.MenusList;

            foreach (DictionaryEntry entry in list)
            {
                menus info = (menus) entry.Value;
                if (Data.role.level < info.level)
                {
                    GameObject go = (GameObject) levelMenu[info.id];
                    if (go != null)
                    {
                        UIButton btn = go.GetComponent<UIButton>();
                        
                        if (btn !=null)
                        {
                            btn.isEnabled = false;
                        }
                        else
                        {
                            go.SetActive(false);
                        }
                    }
                    
                }

                if (info.id == Menu.MazePanel2.ToString() && Data.role.level >= info.level)
                {
                    showMazeTip2 = true;
                }
                else if (info.id == Menu.MazePanel3.ToString() && Data.role.level >= info.level)
                {
                    showMazeTip3 = true;
                }
            }

            //leagueWarTxtObj.SetActive(leagueWarBtn.isEnabled);
            //activityTxtObj.SetActive(activityBtn.isEnabled);
            //heroTxtObj.SetActive(heroBtn.isEnabled);
            //challengeTxtObj.SetActive(challengeBtn.isEnabled);
            //recuitTxtObj.SetActive(recuitBtn.isEnabled);
            
            checkAllUnOpen(list);
            
        }
    }

    private void checkUnOpen(GameObject btn, bool show, menus info)
    {
        Transform unOpenTran = btn.transform.Find("unOpen");
        if (unOpenTran)
        {
            unOpenTran.gameObject.SetActive(!show);
            if (!show)
            {
                RegistUIButton(unOpenTran.gameObject, go =>
                {
                    Tip.show(string.Format(Manager.Language.GetString("IndexPanel.checkUnOpen.Tip"), info.name, info.level), 24);
                });
            }
        }
    }

    private void checkChallengeUpOpen(UIButton btn, menus info) 
    {
        UILabel _challengeTxt = btn.transform.Find("limitTxt").GetComponent<UILabel>();
        
        if (info == null)
        {
            info = new menus { level = 999 };
            btn.isEnabled = false;
        }
        _challengeTxt.gameObject.SetActive(!btn.isEnabled);
        _challengeTxt.text = string.Format(Manager.Language.GetString("IndexPanel._challengeTxt.1"), info.name, info.level);

        if (btn == kingwarBtn && Data.role.level >= info.level && Data.role.kingwar_open != 0)
        {
            btn.isEnabled = false;
            _challengeTxt.gameObject.SetActive(true);
            _challengeTxt.text = string.Format(Manager.Language.GetString("IndexPanel._challengeTxt.2"), Mathf.Ceil(((Data.role.kingwar_open - Data.role.local_time) + 0.0f) / (86400 + 0.0f)));
        }
    }

    private void checkAllUnOpen(Hashtable list)
    {
        checkUnOpen(leagueWarBtn.gameObject, leagueWarBtn.isEnabled, (menus)list[Menu.KnightPanel.ToString()]);
        checkUnOpen(activityBtn.gameObject, activityBtn.isEnabled, (menus)list[Menu.ActivityPanel.ToString()]);
        checkUnOpen(heroBtn.gameObject, heroBtn.isEnabled, (menus)list[Menu.HerosPanel.ToString()]);
        checkUnOpen(challengeBtn.gameObject, firendFightBtn.isEnabled || arenaBtn.isEnabled || arena2Btn.isEnabled || kingwarBtn.isEnabled, (menus)list[Menu.LadderPanel.ToString()]);
        checkUnOpen(recuitBtn.gameObject, recuitBtn.isEnabled, (menus)list[Menu.CallPanel.ToString()]);

        checkUnOpen(megyBtn.gameObject, megyBtn.isEnabled, (menus)list[Menu.MergePanel.ToString()]);
        checkUnOpen(leagueBtn.gameObject, leagueBtn.isEnabled, (menus)list[Menu.LeaguePanel.ToString()]);
        checkUnOpen(packBtn.gameObject, packBtn.isEnabled, (menus)list[Menu.BagPanel.ToString()]);
        checkUnOpen(listBtn.gameObject, listBtn.isEnabled, (menus)list[Menu.CollectPanel.ToString()]);
        checkUnOpen(taskBtn.gameObject, taskBtn.isEnabled, (menus)list[Menu.QuestAndHeroPanel.ToString()]);
        checkUnOpen(equipBtn.gameObject, equipBtn.isEnabled, (menus)list[Menu.EquipPanel.ToString()]);
        checkUnOpen(secBtn.gameObject, secBtn.isEnabled, (menus)list[Menu.MysteryPanel.ToString()]);

        //mazeBtn
        checkUnOpen(mazeBtn.gameObject, mazeBtn.isEnabled, (menus)list[Menu.MazePanel1.ToString()]);

        //changeBtn
        checkUnOpen(changeBtn.gameObject, changeBtn.GetComponent<UIButton>().isEnabled, (menus)list[Menu.ChangeMajorPanel.ToString()]);

        //roleBtn MagicAndProfilePanel
        checkUnOpen(roleBtn.gameObject, roleBtn.GetComponent<UIButton>().isEnabled, (menus)list[Menu.MagicAndProfilePanel.ToString()]);

        //chatBtn
        checkUnOpen(chatBtn.gameObject, chatBtn.isEnabled, (menus)list[Menu.ChatPanel.ToString()]);

        checkChallengeUpOpen(arenaBtn, (menus)list[Menu.LadderPanel.ToString()]);
        checkChallengeUpOpen(arena2Btn, (menus)list[Menu.ArenaPanel.ToString()]);
        checkChallengeUpOpen(firendFightBtn, (menus)list[Menu.FirendFightPanel.ToString()]);
        checkChallengeUpOpen(kingwarBtn, (menus)list[Menu.KnightWarPanel.ToString()]);
    }

    public void openLevelMenu(int level)
    {
//        Debug.Log("");
//        return;
        Hashtable list = Cache.MenusList;

        foreach (DictionaryEntry entry in list)
        {
            menus info = (menus)entry.Value;
            if (info.level == level && !string.IsNullOrEmpty(info.id))
            {
                GameObject go = (GameObject)levelMenu[info.id];

                if (go != null)
                {
                    UIButton btn = go.GetComponent<UIButton>();
                    if (btn)
                    {
                        btn.isEnabled = true;
                    }
                    if (info.id == "WorldWarPanel"&& Data.role.wow_open==0)
                    {
                        go.SetActive(false);
                    }
                    else
                    {
                        go.SetActive(true);
                    }
             
                    if (go.name.IndexOf("gbtn", StringComparison.Ordinal) != -1)
                    {
                        initGiftBtn();
                    }
                    else if (go == secBtn.gameObject)
                    {
                        showRichEffect();
                    }
                    
                }
                if (info.id == Menu.MazePanel2.ToString() && Data.role.level >= info.level)
                {
                    showMazeTip2 = true;
                }
                else if (info.id == Menu.MazePanel3.ToString() && Data.role.level >= info.level)
                {
                    showMazeTip3 = true;
                }
            }
        }
        challengeBtn.isEnabled = firendFightBtn.isEnabled || arenaBtn.isEnabled || arena2Btn.isEnabled || kingwarBtn.isEnabled;

        //leagueWarTxtObj.SetActive(leagueWarBtn.isEnabled);
        //activityTxtObj.SetActive(activityBtn.isEnabled);
        //heroTxtObj.SetActive(heroBtn.isEnabled);
        //challengeTxtObj.SetActive(challengeBtn.isEnabled);
        //recuitTxtObj.SetActive(recuitBtn.isEnabled);
        main.adventrue.showTalkMsgHistory();

        checkAllUnOpen(list);
    }


    public bool checkLevelOpen(string menu)
    {
        Hashtable list = Cache.MenusList;
        menus mInfo = (menus)list[menu];
        if (mInfo == null)
        {
            return false;
        }

        if (Data.role.level < mInfo.level)
        {
            return false;
        }
        return true;
    }

    public bool showPanelByMenu(string menu)
    {
        Hashtable list = Cache.MenusList;
        menus mInfo = (menus) list[menu];
        if (mInfo == null)
        {
            return false;
        }

        if (Data.role.level < mInfo.level)
        {
            Tip.show(string.Format(Manager.Language.GetString("IndexPanel.showPanelByMenu.LevelTip"), mInfo.level, mInfo.name));
            return false;
        }
        
        if (menu == Menu.LeaguePanel.ToString())
        {
            if (Data.role.league_id != 0)
            {
                main.showLeaguePanel();
            }
            else
            {
                Tip.show(Manager.Language.GetString("IndexPanel.showPanelByMenu.LeagueTip"));
                main.showLeagueSelectPanel();
            }
        }
        else if (menu == Menu.LeagueWarPanel.ToString())
        {
            main.showLeagueWar();
        }
        else if (menu == Menu.HerosPanel.ToString())
        {
            main.showHerosPanel();
        }
        else if (menu == Menu.HerosPanel_Formation.ToString())
        {
            main.showHerosPanel();
        }
        else if (menu == Menu.HerosPanel_ExpPanel.ToString())
        {
            main.showHerosPanel(1);
        }
        else if (menu == Menu.HerosPanel_StonePanel.ToString())
        {
            main.showHerosPanel(2);
        }
        else if (menu == Menu.HerosPanel_EvolutionPanel.ToString())
        {
            main.showHerosPanel(3);
        }
        else if (menu == Menu.BagPanel.ToString())
        {
            main.showBagPanel();
        }
        else if (menu == Menu.EquipPanel.ToString())
        {
            main.showEquipPanel();
        }
        else if (menu == Menu.MagicAndProfilePanel.ToString())
        {
            main.showMagicAndProfilePanel();
        }
        else if (menu == Menu.CallPanel.ToString())
        {
            main.showCallPanel();
        }
        else if (menu == Menu.MysteryPanel.ToString())
        {
            main.showMysteryPanel();
        }
        else if (menu == Menu.BottomMask.ToString())
        {
            showOrHideBottomMask();
        }
        else if (menu == Menu.FirendFightPanel.ToString())
        {
            main.showFriendFightPanel();
        }
        else if (menu == Menu.ArenaPanel.ToString())
        {
            main.showArena2Panel();
        }
        else if (menu == Menu.LadderPanel.ToString())
        {
            main.showArenaPanel();
        }
        else if (menu == Menu.CollectPanel.ToString())
        {
            main.showCollectPanel();
        }
        else if (menu == Menu.MergePanel.ToString())
        {
            main.showMergePanel1();
        }
        else if (menu == Menu.MergePanel_EquipMergy.ToString())
        {
            main.showMergePanel1();
        }
        else if (menu == Menu.EquipExchangePanel.ToString())
        {
            main.showEquipExchangePanel();
        }
        else if (menu == Menu.MergePanel_HeroMergy.ToString())
        {
            main.showMergePanel1(1);
        }
        else if (menu == Menu.EmailPanel.ToString())
        {
            main.showEmailPanel();
        }
        else if (menu == Menu.FirendPanel.ToString())
        {
            main.showFirendPanel();
        }
        else if (menu == Menu.ChatPanel.ToString())
        {
            main.showChatPanel();
        }
        else if (menu == Menu.SettingPanel.ToString())
        {
            main.showSettingPanel();
        }
        else if (menu == Menu.ActivityPanel.ToString())
        {
            main.showActivityPanel();
        }
        else if (menu == Menu.MazePanel1.ToString())
        {
            main.showMazePanel();
        }
        else if (menu == Menu.ChangeMajorPanel.ToString())
        {
            main.showChangeMajorPanel();
        }
        else if (menu == Menu.RechargePanel.ToString())
        {
            main.showRechargePanel();
        }
        else if (menu == Menu.RechargeReturnPanel.ToString())
        {
            main.showRechargeReturnPanel();
        }
        else if (menu == Menu.ColorDiamonWelfarePanel.ToString())
        {
            main.showColorDiamondPanel();
        }
        else if (menu == Menu.LimitWelfarePanel.ToString())
        {
            main.showLimitwelfarePanel();
        }
        else if (menu == Menu.FirstWelfarePanel.ToString())
        {
            main.showFirstwelfarePanel();
        }
        else if (menu == Menu.DailyPanel.ToString())
        {
            main.showDailyPanel();
        }
        else if (menu == Menu.DailyActivePanel.ToString())
        {
            main.showDailyPanel();
            main._dailyPanel.isShowActive = true;
        }
        else if (menu == Menu.QuestAndHeroPanel.ToString())
        {
            main.showQuestAndHeroPanel();
        }
        else if (menu == Menu.MerchantPanel.ToString())
        {
            main.showActivityPanel(true);
        }
        else if (menu == Menu.MealPanel.ToString())
        {
            main.showActivityPanel();
            main.activityPanel.btn3.GetComponent<UIToggle>().value = true;
            Data.activity.mealTip = 0;
        }
        else if (menu == Menu.TavernPanel.ToString())
        {
            main.showTavernPanel(); 
        }
        else if (menu == Menu.FortuneCatPanel.ToString())
        {
            main.showActivityPanel();
            main.activityPanel.btn13.GetComponent<UIToggle>().value = true;
        }
        else if (menu == Menu.GodnessPanel.ToString())
        {
            if (Data.role.league_id != 0)
            {
                main.showLeaguePanel();
                main.leaguePanel.info.show_godness = true;
            }
            else 
            {
                Tip.show(Manager.Language.GetString("IndexPanel.showPanelByMenu.LeagueTip"));
                main.showLeagueSelectPanel();
            }
        }
        else if (menu == Menu.HunterPanel.ToString())
        {
            if (Data.role.league_id != 0)
            {
                main.showLeaguePanel();
                main.leaguePanel.info.show_hunter = true;
            }
            else 
            {
                Tip.show(Manager.Language.GetString("IndexPanel.showPanelByMenu.LeagueTip"));
                main.showLeagueSelectPanel();
            }
        }
        else if (menu == Menu.WelfarePanel.ToString())
        {
            if (Data.role.league_id != 0)
            {
                main.showLeaguePanel();
                main.leaguePanel.info.show_welfare = true;
            }
            else 
            {
                Tip.show(Manager.Language.GetString("IndexPanel.showPanelByMenu.LeagueTip"));
                main.showLeagueSelectPanel();
            }
        }
        else if (menu == Menu.HeroMajorPanel1.ToString())
        {
            main.showHerosPanel();
            main.showMajorPanel();
        }
        else if (menu == Menu.KnightPanel.ToString())
        {
            if (!string.IsNullOrEmpty(Data.role.union_id))
            {
                main.showKnightPanel();
            }
            else
            {
                main.showKnightApplyPanel();
            }
        }
        else if (menu == Menu.LandWarFirstPanel.ToString())
        {
            main.showLandWarFirstPanel();
        }
        else if (menu == Menu.LuckCardPanel.ToString())
        {
            main.showLuckCardPanel();
        }
        else if (menu == Menu.KnightWarShopPanel.ToString())
        {
            //if (Data.role.level < mInfo.level)
            //{
            //    Tip.show(string.Format(Manager.Language.GetString("IndexPanel.KnightWarShopPanel.Tip"), mInfo.level, mInfo.name));
            //}
            if (Data.role.kingwar_open != 0)
            {
                main.showIndexPanel();
                Tip.show(Manager.Language.GetString("IndexPanel.KnightWarShopPanel.Tip.1"));
            }
            else 
            {
                main.showKnightWarPanel();
                if (main._knightWarPanel != null)
                {
                    main._knightWarPanel.bottomBtn1.value = false;
                    main._knightWarPanel.bottomBtn5.value = true;
                    main.showKnightWarPanel();
                }
            }
        }
        else
        {
            return false;
        }
        return true;
    }

    public void showWow3PanelByName(string name,int id=0)
    {
        if (name==Menu.WorldWarPanel.ToString())
        {
            main.showWorldWarPanel();
        }
        else if (name == Menu.Wow3WorkshopPanel.ToString())
        {
            main.showWorkShopPanel();
        }
        else if (name == Menu.RuneSetPanel.ToString())
        {
            main.showRunePanel(0);
        }
        else if (name == Menu.RuneEcmPanel.ToString())
        {
            main.showRunePanel(1);
        }
        else if (name == Menu.RuneDevPanel.ToString())
        {
            main.showRunePanel(2);
        }
        else if (name == Menu.RuneDecPanel.ToString())
        {
            main.showRunePanel(3);
        }
        else if (name == Menu.Wow3ChatPanel.ToString())
        {
            main.showWow3ChatPanel();
        }
        else if (name == Menu.Wow3SciencePanel.ToString())
        {
            int type = 1;
            if (id>=2 &&id<=17)
            {
                type = 1;
            }
            else if (id>=22 &&id<=28)
            {
                type = 2;
            }
            else if (id==1||(id>=18&&id<=21))
            {
                type = 3;
            }

            main.skip2Wow3SciencePanel(type,id);
        }
        else
        {
            Debug.Log("---------------------"+name);
        }
        

    }

    public bool getHeroMergyOpen()
    {
        menus menuInfo = (menus) Cache.MenusList[Menu.MergePanel_HeroMergy.ToString()];
        return Data.role.level >= menuInfo.level;
    }

    private void initGiftBtn()
    {
        if (isMenuShow == true)
        {
            showOrHideGift();
        }

        if (giftBtn7.activeSelf)
        {
            giftBtn7.SetActive(Data.role.act_countdown != 2);
        }

        if (giftBtn6.activeSelf)
        {
            if (limitTimeTxt.text == "00:00:00" && Data.role.act_timelimit == 0)
            {
                giftBtn6.SetActive(false);
                main.indexPanel.giftGrid.Reposition();
            }
            else if (limitTimeTxt.text != "00:00:00" && Data.role.act_timelimit == 0)
            {
                giftBtn6.SetActive(true);
                giftBtn6.transform.Find("ring").gameObject.SetActive(false);
            }
        }

        if (giftBtn3.activeSelf)
        {
            setting_info sInfo2 = (setting_info)Cache.SettingInfo["act_seven_real_deadline"];
            double time2 = Convert.ToDouble(sInfo2.value) * 3600 * 24 + Data.role.reg_time;
            if (Data.role.local_time >= time2)
            {
                giftBtn3.SetActive(false);
            }
            else
            {
                //giftBtn3.SetActive(true);
                giftBtn3.transform.Find("tipNum").GetComponent<TipSign>().show(Data.role.act_crazy7);
            }
        }

        giftBtn11.SetActive(Data.role.mgirl_type != 0 && giftBtn11.activeSelf);
        UISprite sprite = giftBtn11.GetComponent<UISprite>();
        sprite.spriteName = "activitytype1" + (Data.role.mgirl_type - 1);

        giftBtn4.SetActive(giftBtn4.activeSelf && Data.role.beta == 1);

        giftBtn11.transform.Find("tipNum").GetComponent<TipSign>().show(Data.role.mgirl_tips);

        giftBtn12.SetActive(Data.role.act_forever != 0 && giftBtn12.activeSelf);
        giftBtn12.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_forever == 2);
        giftBtn13.SetActive(Data.role.act_special != 0 && giftBtn13.activeSelf);
        giftBtn13.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_special == 1);
        giftBtn14.SetActive(Data.role.act_goldenhero != 0 && giftBtn14.activeSelf);
        giftBtn14.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_goldenhero == 2);
        if (giftBtn14.activeSelf && Data.role.act_goldenhero != 0)
        {
            Socket.Send(CMD.ActGoldenHero_getinfo);
        }
        giftBtn15.SetActive(Data.role.act_redpacket != 0 && giftBtn15.activeSelf);
        giftBtn15.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_redpacket == 2);
        giftBtn16.SetActive(giftBtn16.activeSelf && Data.role.paymore != 0);
        giftBtn16.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.paymore == 2);
        giftBtn17.SetActive(giftBtn17.activeSelf && Data.role.payonce != 0);
        giftBtn17.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.payonce == 2);
        giftBtn18.SetActive(giftBtn18.activeSelf && Data.role.act_mall != 0);
        giftBtn18.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_mall == 2);
        giftBtn19.SetActive(giftBtn19.activeSelf && Data.role.herobank != 0);
        giftBtn19.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.herobank == 2);
        giftBtn20.transform.Find("tipNum").GetComponent<TipSign>().show(Data.role.luckcard);
        giftBtn21.SetActive(giftBtn21.activeSelf && Data.role.collect_card != 0);
        giftBtn21.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.collect_card == 2);
        giftBtn22.SetActive(giftBtn22.activeSelf && Data.role.qqvip != 0);
        giftBtn22.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.qqvip == 2);
        giftBtn23.SetActive(giftBtn23.activeSelf && Data.role.act_update != 0);
        giftBtn23.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_update == 1);
        giftBtn24.SetActive(giftBtn24.activeSelf && Data.role.qqvplus != 0);
        giftBtn24.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.qqvplus == 2);
        giftBtn25.SetActive(giftBtn25.activeSelf && Data.role.vote != 0);
        giftBtn25.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.vote == 2);
        giftBtn26.SetActive(giftBtn26.activeSelf && Data.role.act_mallsp != 0);
        giftBtn26.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_mallsp == 2);
        giftBtn28.SetActive(giftBtn28.activeSelf && Data.role.act_herolimit != 0);
        giftBtn28.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_herolimit == 1);
        giftBtn29.SetActive(giftBtn29.activeSelf && Data.role.act_festival != 0);
        giftBtn29.transform.Find("tipNum").GetComponent<TipSign>().show(1, (Data.role.act_festival >> 0 & 1) > 0 || (Data.role.act_festival >> 0 & 2) > 0);
        giftBtn30.SetActive(giftBtn30.activeSelf && Data.role.act_anniversary != 0);
        giftBtn30.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_anniversary == 2);
        worldWarBtn.SetActive(worldWarBtn.activeSelf && Data.role.wow_open == 1);
        if (Data.role.act_goldsign == 0)
        {
            giftBtn27.SetActive(false);
        }
        else if(giftBtn27.activeSelf)
        {
            giftBtn27.SetActive(true);
            if (Convert.ToInt32(Data.role.act_goldsign >> (2 - 1) & 1) == 1)
            {
                giftBtn27.GetComponent<UISprite>().spriteName = "activitytype31";
            }
            else if (Convert.ToInt32(Data.role.act_goldsign >> (1 - 1) & 1) == 1)
            {
                giftBtn27.GetComponent<UISprite>().spriteName = "activitytype30";
            }
        }
        int _tip27 = 0;
        for (int i = 3; i <= 7; i++)
        {
            _tip27 += Convert.ToInt32(Data.role.act_goldsign >> (i - 1) & 1);
        }
        giftBtn27.transform.Find("tipNum").GetComponent<TipSign>().show(_tip27);

        giftGrid.Reposition();
        oldPosList = new List<Vector3>();
        showList = new List<GameObject>();
        for (int i = 0; i < giftList.Count; i++)
        {
            if (giftList[i].activeSelf)
            {
                showList.Add(giftList[i]);
                Vector3 vect = new Vector3(giftList[i].transform.localPosition.x, giftList[i].transform.localPosition.y);
                oldPosList.Add(vect);
            }
        }
    }

    private void showOrHideGift()
    {
        if (oldPosList == null)
        {
            initGiftBtn();
        }
        
        giftBtn.transform.DOLocalRotate(new Vector3(0, 0, isMenuShow ? -45 : 0), 0.2f);

        const float toPos = 100;
        for (int i = 0; i < showList.Count; i++)
        {
            Transform tran = showList[i].transform;
            if (!isMenuShow)
            {
                tran.DOLocalMoveX(toPos, 0.15f).OnComplete(() =>
                {
                    tran.gameObject.SetActive(false);
                    hideRing(tran, true);
                });
            }
            else
            {
                hideRing(tran, false);
                tran.DOScale(new Vector2(1, 1), 0.05f);
                tran.DOLocalMoveX(oldPosList[i].x, 0.15f);
                tran.gameObject.SetActive(true);
            }
        }
        isMenuShow = !isMenuShow;
    }

    private void hideRing(Transform tran,bool isHide)
    {
        if (!tran.gameObject.activeSelf)
        {
            return;
        }
        Transform ringTran = tran.Find("ring");
        if (ringTran)
            ringTran.gameObject.SetActive(!isHide);
    }

    public static void RegistUIButton(GameObject button, UIEventListener.VoidDelegate action)
    {
        UIEventListener listener = UIEventListener.Get(button);
        listener.onClick = (go) =>
        {
            if (UIEventListenerExtension.current != null)
                return;

            UIEventListenerExtension.current = listener;
            action(go);
            UIEventListenerExtension.current = null;
        };
    }

    private void checkTipPanel() 
    {
        //int todayTime = ((Data.role.server_time + (8 * 3600)) / 3600 / 24) * 3600 * 24 - (8 * 3600);
        int localTime = Data.role.local_time;
        //localTime = todayTime + 12 * 3600 + 10 * 60;
        bool canBreak = false;
        for (int j = 1; j <= tipTimeList.Count; j++)
        {
            tipInfo info = (tipInfo)tipTimeList[j];
            if (Data.role.level < info.level)
            {
                continue;
            }

            if (!string.IsNullOrEmpty(info.week))
            {
                string[] weeks = info.week.Split(',');
                bool needTipWeek = true;
                for (int i = 0; i < weeks.Length; i++)
                {
                    if (Convert.ToInt32(weeks[i]) == Convert.ToInt32(DateTime.Now.DayOfWeek))
                    {
                        needTipWeek = false;
                        break;
                    }
                }
                if (!needTipWeek)
                {
                    continue;
                }
            }

            for (int i = 0; i < info.startTimeList.Count; i++)
            {
                if (!info.isClickClose && info.startTimeList[i] <= localTime && localTime < info.endTimeList[i])
                {
                    info.indexTipPanel.showMovie();
                    canBreak = true;
                    break;
                }
                else if (i <= info.startTimeList.Count - 2 && info.endTimeList[i] <= localTime && localTime < info.startTimeList[i + 1])
                {
                    info.indexTipPanel.hideMovie();
                    info.isClickClose = false;
                    break;
                }
                else if (i == info.startTimeList.Count - 1 && info.endTimeList[i] <= localTime)
                {
                    info.indexTipPanel.hideMovie();
                    info.isClickClose = false;
                }
            }
            if (canBreak)
            {
                canBreak = false;
                break;
            }
        }
    }

    private void clipTipTime() 
    {
        tipTimeList = new Hashtable();
        int todayTime = ((Data.role.server_time + (8 * 3600)) / 3600 / 24) * 3600 * 24 - (8 * 3600);
        foreach (DictionaryEntry entry in Cache.ActBubbleUpList)
        {
            act_bubble_up info = (act_bubble_up)entry.Value;
            string[] _timeArr = info.time.Split(',');
            tipInfo tip_info = new tipInfo();
            tip_info.id = info.id;
            tip_info.level = info.level;
            tip_info.week = info.week;
            tip_info.startTimeList = new List<int>();
            tip_info.endTimeList = new List<int>();
            for (int i = 0; i < _timeArr.Length; i++)
            {
                tip_info.startTimeList.Add((Convert.ToInt32(_timeArr[i].Substring(0, 2)) * 3600) + (Convert.ToInt32(_timeArr[i].Substring(3, 2)) * 60) + todayTime);
                tip_info.endTimeList.Add((Convert.ToInt32(_timeArr[i].Substring(6, 2)) * 3600) + (Convert.ToInt32(_timeArr[i].Substring(9, 2)) * 60) + todayTime);
            }

            tip_info.indexTipPanel = (NGUITools.AddChild(tipPanels, tipPanel)).GetComponent<IndexTipPanel>();
            tip_info.indexTipPanel.show(tip_info, info);

            tipTimeList.Add(tip_info.id, tip_info);
        }
    }

    public class tipInfo 
    {
        public int id { get; set;}
        public int level { get; set; }
        public string week { get; set; }

        public List<int> startTimeList;

        public List<int> endTimeList;

        public bool isClickClose = false;

        public IndexTipPanel indexTipPanel;
    }

    /*private void OnLoadComplete(UniWebView webView, bool success, string errorMessage)
    {
        if (success)
        {
            webView.Show();
        }
        else
        {
//            Tip.show("" + errorMessage);
        }
    }

    private void OnWebViewClose(UniWebView webView)
    {
        Tip.show("msg->OnWebViewClose");
    }
    
    private void OnKeyCode(UniWebView webview, int keycode)
    {
        Debug.Log("keycode: " + keycode);
        if (keycode == 4)
        {
            if (!webview.CanGoBack())
            {
//                webview.Hide();
                webview = null;
            }
        }
        else
        {
            Debug.Log("keycode: " + keycode);
        }
    }

    private UniWebViewEdgeInsets InsetsForScreenOreitation(UniWebView webView, UniWebViewOrientation orientation)
    {
        return new UniWebViewEdgeInsets(5, 5, 5, 5);
    }
*/
}