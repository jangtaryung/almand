using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using LitJson;
using Network.Particle.Scripts.Core;
using Network.Particle.Scripts.Model;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
// ReSharper disable All

// ReSharper disable UseObjectOrCollectionInitializer

// ReSharper disable PossibleLossOfFraction

public class IndexMain : BaseMonoBehaviour
{
    public GameObject parent;
    public GuideManager gameGuide;
    public UIPanel TipPanel;
    public UIPanel resPanel;
    public UIPanel expPanel;
    public UIPanel bottomPanel;
    public IndexPanel indexPanel;
    public UIPanel EquipPanel;
    public UIPanel BagPanel;
    public UIPanel NftBagPanel;
    //    public UIPanel HerosPanel;
    public GameObject loadingPannel;

    public HeroShowListPanel heroListPanel;
    public HerosPanel herosPanel;
//    public UIPanel mergePanel;

    public GameObject chatBtnPanel;
    public PushMsgPanel pushPanel;
    
    public ChatPanel chatPanel;
    public BBSPanel bbsPanel;
    public EmailPanel emailPanel;
    public FirendPanel firendPanel;
    private MallPanel mallPanel;
    
    public FightNotenoughPanel fightNotenoughPanel;
    public QuestAndHeroPanel questAndHeroPanel;
    
    public LeaguePanel leaguePanel;
    public LeagueSelectPanel leagueSelectPanel;
    
    public ActivityPanel activityPanel;
    public CollectPanel collectPanel;
    public CollectRewardPanel collectRewardPanel;

    public UIPanel BattlePanel;

    public UIPanel TopPanel;
    
    public AwardPanel awardPanel;
    public PlayerInfoPanel playerInfoPanel;
    
    public UIPanel cardPanel;
    public EquipInfoPanel equipInfoPanel;
    public AwardGivePanel awardGivePanel;
    public LevelUpPanel levelUpPanel;
    public FightUp fightUp;
    public Diamond diamond;

    public UISpriteAnimation taplight;

    public RunePanel runePanel;
    private OpenHeroMaxPanel _openHeroPanel;
    private NewHeroPanel _newHeroPanel;
    private NewMajorPanel _newChangeMajorPanel;
    private NewCollectHeroPanel _newCollectPanel;
    private BalancePanel _balancePanel;
    private SignInPanel _signInPanel;
    private MysteryPanel _mysteryPanel;
    private LeagueWar _leagueWar;
    
    private MagicAndProfilePanel _magicAndProfilePanel;
    private HeroMajorPanel1 _majorPanel;
    private FirstMoviePanel _firstMoviePanel;
    private NewFirstMoviePanel _newFirstMoviePanel;
    private OpenMoviePanel _openMoviePanel;
    private MagicDetailPanel _magicDetail;
    private FriendFightPanel _friendFightPanel;

    private ArenaPanel _arenaPanel;
    private Arena2Panel _arena2Panel;

    private TavernPanel _tavernPanel;
    private ChangeMajorPanel _changeMajorPanel;

    private EquipExchangePanel _equipExchangePanel;
    private MazePanel _mazePanel;
    private SettingPanel _settingPanel;

    private ExchangePanel _exchangePanel;

    private TalkPanel _talkPanel;
    private MonsterAttackPanel _monsterPanel;
    private GivePanel _givePanel;
    private ChestPanel _chestPanel;
    private CallPanel callPanel;
    private GiftAward1Panel _giftAward1Panel;

    private RechargePanel _rechargePanel;
    public DailyPanel _dailyPanel;
    private SevenDaysPanel _sevenDaysPanel;
    private ColorDiamonWelfarePanel _colordiamondPanel;
    private LimitWelfarePanel _limitwelfarePanel;
    private FirstWelfarePanel _firstwelfarePanel;
    private RechargeReturnPanel _rechargeReturnPanel;
    private AdventureGuidePanel _adventureGuidePanel;
    private AllStrategyPanel _allStrategyPanel;
    private VipWarningPanel _vipWarningPanel;
    private QuickRisePanel _quickRisePanel;
    private KnightPanel _knightPanel;
    private KnightApplyPanel _knightApplyPanel;
    private MagicGirlPanel _magicGirlPanel;
    private ActForeverPanel _actForeverPanel;
    private ActGoldheroPanel _actGoldheroPanel;
    private ActRedpacketPanel _actRedpacketPanel;
    private ActFourPanel _actFourPanel;
    private ActFestivalPanel _actFestivalPanel;

    private KnightDuelPanel _knightDuelPanel;
    private LandWarPanel _landWarPanel;
    private LandWarFirstPanel _landWarFirstPanel;
    private LandWarList _landWarListPanel;
    private LandWarEndPanel _landWarEndPanel;
    private MergyPanel _mergePanel1;

    private ActSpecialPanel _actSpecialPanel;

    private MergyGuidePanel _getResPanel;
    private LuckCardPanel _luckCardPanel;
    private CardCollectPanel _cardCollectPanel;
    private UpdateWelfarePanel _updateWelfarePanel;
    public WorldWarPanel _worldWarPanel;
    private Wow3MainPanel _wow3MainPanel;
    private Wow3BattlePanel _wow3battlePanel;
    private Wow3FortPanel _wow3fortPanel;
    private Wow3ReportPanel _wow3reportPanel;
    private Wow3UnionPanel _wow3unionPanel;
    private Wow3UnionApplyPanel _wow3unionApplyPanel;
    private Wow3EmailPanel _wow3EmailPanel;
    
    private Wow3WorkshopPanel _workshopPanel;
    private Wow3RolePanel _wow3RolePanel;
    private Wow3InformationPanel _Wow3InformationPanel;
    private Wow3EmailWritePanel _Wow3EmailWritePanel;
    private Wow3FirendPanel _Wow3FirendPanel;
    private Wow3RankingPanel _wow3RankingPanel;
    private Wow3TargetPanel _wow3TargetPanel;
    public KnightWarPanel _knightWarPanel;
    private QuestionnairePanel _questionnairePanel;
    private ActGoldPanel _actGoldPanel;
    private ActHeroLimitPanel _actHeroLimitPanel;
    private Wow3SiegeAnimationPanel _wow3SiegeAnimationPanel;
    private Wow3ChatPanel _wow3ChatPanel;
    private AnniversaryPanel _anniversaryPanel;
    private YouyiAdPanel _youyiadPanel;

    private ArrayList Panels;

    public LevelGift1 levelGift;

    private UIPanel lastPanel;
    private bool lastShowTop;
    private bool lastShowRes;
    private bool lastShowBot;

    public UILabel nameText;
    public UILabel testText;
    public UILabel levelText;
    public UILabel vipText;
    public UILabel vipTxt;
    public UILabel coinTxt;
    public UILabel goldTxt;
    public UILabel ticketTxt;
    public UILabel cpiTxt;
    public UILabel cpi2Txt;
    public UILabel expTxt;
    public UILabel timeTxt;

    public UITexture heroBG;
    public UITexture heroHead;
    public UISprite expLine;
    
    public UISpriteAnimation expEffect;
    public UISpriteAnimation cpiEffect;
    
    public UISprite netState;
    public UISprite powerLine;

//    public UISpriteAnimation moneyEffect;

//    public HeroList heroList;
    public AdventrueMovie adventrue;
//    public CityPanel cityPanel;
    public HeroLevelUp heroLevel;

    public UIPanel guidePanel;

    public bool inBattle;

    private int chatCount = 1;
    private ArrayList showMsgList;
    private float _updateTime;
    public GameObject nowPanel;
    private ArrayList notShowAwardList;

    private int[] cdArr;
    private int cdHour;

    //
    private static ArrayList sendList;

    private ArrayList showList;

    private int gcCount;

    private ArrayList majorItemList;

    private bool isLevelGiftReward = true;
    public bool isLevelGiftLast = true;

    public UILabel BottomBtns_bottomMask_KingWarBtn_Label;
    public UILabel BottomBtns_bottomMask_friendFightBtn_Label;
    public UILabel BottomBtns_bottomMask_arena2Btn_Label;
    public UILabel BottomBtns_bottomMask_arenaBtn_Label;
    public GameObject leagueWar
    {
        get
        {
          return _leagueWar.gameObject;
        }
    }

    public void Awake()
    {
        if (!GlobalTools.GotoInitDate())
        {
            return;
        }

        
        main = this;
        GameSocket.main = this;
//        RemoteLog.Instance.Start(GlobalTools.LogIP, GlobalTools.LogPort);
        Cache.InitHeroInfo();

        Hashtable majorItemhp = Cache.WeaponTypeBonusList;
        majorItemList = new ArrayList();
        foreach (DictionaryEntry entry in majorItemhp)
        {
            weapon_type_bonus wInfo = (weapon_type_bonus) entry.Value;
            majorItemList.Add(wInfo.req_id);
        }
        
        notShowAwardList = new ArrayList();
        notShowAwardList.Add("CallPanel");
        notShowAwardList.Add(collectPanel.name);
        
        //notShowAwardList.Add(mergePanel.name);

        Panels = new ArrayList();
        Panels.Add(indexPanel.gameObject);
        Panels.Add(EquipPanel.gameObject);
        Panels.Add(NftBagPanel.gameObject);
        Panels.Add(BagPanel.gameObject);
//        Panels.Add(cardPanel.gameObject);
//        Panels.Add(HerosPanel.gameObject);
//        Panels.Add(herosPanel.gameObject);
        Panels.Add(heroListPanel.gameObject);
//        Panels.Add(callPanel);
//        Panels.Add(mergePanel.gameObject);
//        Panels.Add(tavernPanel.gameObject);
//        Panels.Add(equipExchangePanel.gameObject);
//        Panels.Add(WorldPanel.gameObject);
//        Panels.Add(FirendFightPanel.gameObject);
//        Panels.Add(MysteryPanel.gameObject);
        Panels.Add(BattlePanel.gameObject);
        Panels.Add(emailPanel.gameObject);
        Panels.Add(firendPanel.gameObject);
        Panels.Add(questAndHeroPanel.gameObject);
        
        Panels.Add(leaguePanel.gameObject);
        Panels.Add(leagueSelectPanel.gameObject);
//        Panels.Add(_leagueWar.gameObject);
        Panels.Add(activityPanel.gameObject);
        Panels.Add(collectPanel.gameObject);
        Panels.Add(collectRewardPanel.gameObject);
//        Panels.Add(mazePanel.gameObject);

        TipPanel.gameObject.SetActive(true);

        if (guidePanel != null) guidePanel.gameObject.SetActive(true);
        if (guidePanel != null && guidePanel.gameObject.activeSelf)
        {
            guidePanel.gameObject.SetActive(false);
        }

        showIndexPanel();

        //BottomBtns_bottomMask_KingWarBtn_Label  = this.gameObject.transform.Find("UI Root/BottomBtns/bottomMask/kingwarBtn/Label").gameObject.GetComponent<UILabel>();
        //BottomBtns_bottomMask_friendFightBtn_Label = this.gameObject.transform.Find("UI Root/BottomBtns/bottomMask/friendFightBtn/Label").gameObject.GetComponent<UILabel>();
        //BottomBtns_bottomMask_arena2Btn_Label = this.gameObject.transform.Find("UI Root/BottomBtns/bottomMask/arena2Btn/Label").gameObject.GetComponent<UILabel>();
        //BottomBtns_bottomMask_arenaBtn_Label = this.gameObject.transform.Find("UI Root/BottomBtns/bottomMask/arenaBtn/Label").gameObject.GetComponent<UILabel>();

        BottomBtns_bottomMask_KingWarBtn_Label.text = Manager.Language.GetString("IndexMain.BottomBtns.bottomMask.kingwarBtn.Label");
        BottomBtns_bottomMask_friendFightBtn_Label.text = Manager.Language.GetString("IndexMain.BottomBtns.bottomMask.friendFightBtn.Label");
        BottomBtns_bottomMask_arena2Btn_Label.text = Manager.Language.GetString("IndexMain.BottomBtns.bottomMask.arena2Btn.Label");
        BottomBtns_bottomMask_arenaBtn_Label.text = Manager.Language.GetString("IndexMain.BottomBtns.bottomMask.arenaBtn.Label");


        if (Socket&&cdArr == null)
        {
            cdArr = new int[3];
            setting_info goblin_shop_reset_time = (setting_info)Cache.SettingInfo["goblin_shop_reset_time"];
            string[] timeArr = StringUtil.Split(goblin_shop_reset_time.value, ",");
            for (int i = 0; i < timeArr.Length; i++)
            {
                int count = Convert.ToInt32(timeArr[i]);
                if (count == 0)
                {
                    count = 24;
                }
                cdArr[i] = count;
            }
            cdHour = MathUtil.nearest(DateTime.Now.Hour, cdArr);
        }

        CancelInvoke();
        InvokeRepeating("updateLocalTime", 1, 1);

        //UnityEngine.Debug.LogError(" 111111111111 walletmanager instance init ");
        //지갑 초기화
        //WalletManager.Instance.Init();
    }

    public void Start()
    {
        if (Data.role.level == 1)
        {
            showNewFirstMoviePanel();
        }
    }

    public void checkFirstGuide()
    {
        guide info = (guide)CacheData.Instance.GuideList[10001];
        gameGuide.showGuide(info);
    }

    public void OnEnable()
    {
        if (Socket != null)
        {
            ToolKit.init();

            //StartCoroutine("UpdataBattery");

            ToolKit.delayCall(0.2f, () =>
            {
                playIndexMusic();
            });
//            SoundManager.Mute();
            Socket.addCallBack(CMD.Change, onChange);
            Socket.addCallBack(CMD.Test_Echo, onEcho);
            Socket.addCallBack(CMD.RoleItem_list, onItemList);
            Socket.addCallBack(CMD.RoleHero_getHeros, onHeroBack);
            Socket.addCallBack(CMD.RoleExplore_getinfo, onExploreInfoBack);

            Socket.addCallBack(CMD.Combat_complete, onCombatCompleteBack);
            Socket.addCallBack(CMD.RoleExplore_event, onEventBack);
            Socket.addCallBack(CMD.RoleTeam_getinfo, onEquipBack);
            Socket.addCallBack(CMD.RoleEmail_new, onEmailNew);
            Socket.addCallBack(CMD.Friend_blessnew, onFirendNew);
            Socket.addCallBack(CMD.Friend_notify, onFirendNew);
            Socket.addCallBack(CMD.Friend_agreex, onAgreexNew);
            Socket.addCallBack(CMD.Sign_getinfo, onSignInfo);
            Socket.addCallBack(CMD.RoleScout_notify, onStoryNotify);
            Socket.addCallBack(CMD.Collect_getinfo, infoCollectBack);

            Socket.addCallBack(CMD.Quest_getinfo, onQuestMsg);

            Socket.addCallBack(CMD.ActSplendid_getinfo, onWonderInfo);
            Socket.addCallBack(CMD.Tavern_getInfo, infoCallBack);
            Socket.addCallBack(CMD.RoleWake_getinfo, infoWakeback);
            Socket.addCallBack(CMD.Chat_logs, onChatLogs);
            Socket.addCallBack(CMD.Chat_msg, onChatMsg);

            Socket.addCallBack(CMD.Chat_msg, chatPanel.onMsg);
            Socket.addCallBack(CMD.Chat_redpacket, onChatRedPacket);
            Socket.addCallBack(CMD.MsgBox_message, onPushMessage);
            Socket.addCallBack(CMD.Role_loginr, onlogin);
            Socket.addCallBack(CMD.Role_loginsg, loginCallBack);
            Socket.removeCallBack(CMD.Role_getdata, logingetdata);
            Socket.addCallBack(CMD.RoleExplore_balance, onBalance);
            Socket.addCallBack(CMD.Role_getInfo, onGetInfo);
            Socket.addCallBack(CMD.Daily_init, onDailyInfo);
            Socket.addCallBack(CMD.Resource_new, onNew);

            Socket.addCallBack(CMD.ActLevelGift_getinfo, onGiftInfo);
            Socket.addCallBack(CMD.ActLevelGift_reward, onGiftReward);
            Socket.addCallBack(CMD.Union_notify, onUnionNotifyInfo);
            Socket.addCallBack(CMD.Role_kick, onKickInfo);
            Socket.addCallBack(CMD.League_warClose, onWarClose);

            Socket.addCallBack(CMD.Role_warn, onWarn);
            Socket.addCallBack(CMD.ActShare_notify, onShare);
            Socket.addCallBack(CMD.Quest_done, onQuestDone);

            Socket.addCallBack(CMD.WorldWar_chatmsg,onWow3ChatMsg);

            Socket.addCallBack(CMD.WorldWar_mark, onWow3MarkInfo);

            Data.role.bind("level", updateLevel, true);
            Data.role.bind("level", showLevel);
            Data.role.bind("quality", updateQuality, true);
            Data.role.bind("vip", updateVip, true);
//            Data.role.bind("role_name", updateNameTxt, true);
            Data.role.bind("coin", updateCoin, true);
            Data.role.bind("gold", updateGold, true);
            Data.role.bind("ticket", updateTicket, true);
            Data.role.bind("cpi", showCpi);
            Data.role.bind("server_time", updateTime);
            Data.role.bind("fight_max", showFightMax);
            Data.role.bind("exp", updateExp, true);
            Data.role.bind("icon", updateIcon);

            cpiTxt.text = Data.role.cpi + "";
            //cpi2Txt.text = Data.role.cpi + "";

            Res.showLoginTexture(heroHead, "" + Data.role.icon);
//          heroHead.spriteName = "" + Data.role.icon;
            cpiEffect.gameObject.SetActive(false);

            indexPanel.emailTipSign.show(Data.role.email, indexPanel.emailBtn.isEnabled);
            indexPanel.friendTipSign.show(Data.role.fnotify + Data.role.fbless, indexPanel.firendsBtn.isEnabled);

            Data.battle.removeAllBattleCallBack();
            Data.battle.addBattleCallback(1, delegate
            {
                gameGuide.checkShow("fight:" + Data.adventure.currentInfo.id);
                showIndexPanel();
            });

            Data.battle.addBattleCallback(2, delegate
            {
                showFriendFightPanel();
            });
            Data.battle.addBattleCallback(3, delegate
            {
                showFriendFightPanel();
            });
            Data.battle.addBattleCallback(4, delegate { showMysteryPanel(); });
            Data.battle.addBattleCallback(5, delegate { showMysteryPanel(); });
            Data.battle.addBattleCallback(6, delegate
            {
                showLastPanel();
            });
            Data.battle.addBattleCallback(7, delegate
            {
                showArena2Panel();
            });
            Data.battle.addBattleCallback(9, delegate
            {
                showArenaPanel();
            });
            Data.battle.addBattleCallback(10, delegate
            {
                showArenaPanel();
            });
            Data.battle.addBattleCallback(11, delegate
            {
                showLeagueWar();
            });
            Data.battle.addBattleCallback(12, delegate { showMysteryPanel(); });
            Data.battle.addBattleCallback(13, delegate { showMazePanel(); });
            Data.battle.addBattleCallback(14, delegate
            {
                _newFirstMoviePanel.step++;
                showNewFirstMoviePanel();
            });

            Data.battle.addBattleCallback(16, delegate
            {
                showLeagueWar();
            });

            Data.battle.addBattleCallback(999, delegate
            {
                showLastPanel();
            });

            if (Data.role.level > 1)
            {
                showBanlance();

                if (Data.role.signin == 0)
                {
                    Socket.Send(CMD.Sign_getinfo);
                }
            }

            if (!Data.hero.Init)
            {
//                sendList = new ArrayList();
//                sendList.Add(CMD.RoleItem_list);
//                sendList.Add(CMD.RoleTeam_getinfo);
//                sendList.Add(CMD.RoleExplore_getinfo);
//                sendList.Add(CMD.Quest_getinfo);
//                sendList.Add(CMD.ActSplendid_getinfo);
//                sendList.Add(CMD.RoleWake_getinfo);
//                sendList.Add(CMD.Chat_logs);
//
//                sendMsg();
//                
//                
//                Socket.Send(CMD.RoleHero_getHeros);
//                Socket.Send(CMD.RoleTeam_getinfo);
//                Socket.Send(CMD.RoleExplore_getinfo);
//                Socket.Send(CMD.Quest_getinfo);
//                Socket.Send(CMD.ActSplendid_getinfo);
//                Socket.Send(CMD.RoleWake_getinfo);
//                Socket.Send(CMD.RoleItem_list);
                
                getRoleInfo();
            }

            Socket.Send(CMD.ActLevelGift_getinfo);
        }

        
    }

    bool gotInfo;
    int gotInfoCount;
    private void getRoleInfo()
    {
        if (!gotInfo && gotInfoCount <= 3)
        {
            Socket.Send(CMD.Role_getInfo);
            Invoke("getRoleInfo", 2);
            gotInfoCount++;
        }
        else
        {
            CancelInvoke("getRoleInfo");
        }
        
    }

    private void sendMsg()
    {
        if (sendList.Count > 0)
        {
            string msfStr = (string) sendList[0];
            sendList.RemoveAt(0);
            Socket.Send(msfStr);
            Invoke("sendMsg", 0.02f);
        }
        else
        {
            CancelInvoke("sendMsg");
        }

    }

    public void OnDisable()
    {
        if (Socket != null)
        {
            Socket.removeCallBack(CMD.Change, onChange);
            Socket.removeCallBack(CMD.Test_Echo, onEcho);
            Socket.removeCallBack(CMD.RoleItem_list, onItemList);
            Socket.removeCallBack(CMD.RoleHero_getHeros, onHeroBack);
            Socket.removeCallBack(CMD.RoleExplore_getinfo, onExploreInfoBack);
            Socket.removeCallBack(CMD.Combat_complete, onCombatCompleteBack);
            Socket.removeCallBack(CMD.RoleExplore_event, onEventBack);
            Socket.removeCallBack(CMD.RoleTeam_getinfo, onEquipBack);
            Socket.removeCallBack(CMD.RoleEmail_new, onEmailNew);
            Socket.removeCallBack(CMD.Friend_blessnew, onFirendNew);
            Socket.removeCallBack(CMD.Friend_notify, onFirendNew);
            Socket.removeCallBack(CMD.Friend_agreex, onAgreexNew);

            Socket.removeCallBack(CMD.Sign_getinfo, onSignInfo);
            Socket.removeCallBack(CMD.RoleScout_notify, onStoryNotify);

            Socket.removeCallBack(CMD.Quest_getinfo, onQuestMsg);

            Socket.removeCallBack(CMD.ActSplendid_getinfo, onWonderInfo);
            Socket.removeCallBack(CMD.Tavern_getInfo, infoCallBack);
            Socket.removeCallBack(CMD.RoleWake_getinfo, infoWakeback);
            Socket.removeCallBack(CMD.Collect_getinfo, infoCollectBack);
            Socket.removeCallBack(CMD.Chat_logs, onChatLogs);
            Socket.removeCallBack(CMD.Chat_msg, onChatMsg);
            Socket.removeCallBack(CMD.Chat_msg, chatPanel.onMsg);
            Socket.removeCallBack(CMD.Chat_redpacket, onChatRedPacket);
            Socket.removeCallBack(CMD.MsgBox_message, onPushMessage);
            Socket.removeCallBack(CMD.Role_loginr, onlogin);
            Socket.removeCallBack(CMD.Role_loginsg, loginCallBack);
            Socket.removeCallBack(CMD.Role_getdata, logingetdata);
            Socket.removeCallBack(CMD.RoleExplore_balance, onBalance);
            Socket.removeCallBack(CMD.Role_getInfo, onGetInfo);
            Socket.removeCallBack(CMD.Daily_init, onDailyInfo);

            Socket.removeCallBack(CMD.ActLevelGift_getinfo, onGiftInfo);
            Socket.removeCallBack(CMD.ActLevelGift_reward, onGiftReward);
            Socket.removeCallBack(CMD.Resource_new, onNew);
            Socket.removeCallBack(CMD.Union_notify, onUnionNotifyInfo);
            Socket.removeCallBack(CMD.Role_kick, onKickInfo);
            Socket.removeCallBack(CMD.League_warClose, onWarClose);

            Socket.removeCallBack(CMD.Role_warn, onWarn);
            Socket.removeCallBack(CMD.ActShare_notify, onShare);
            Socket.removeCallBack(CMD.Quest_done, onQuestDone);

            Socket.removeCallBack(CMD.WorldWar_chatmsg, onWow3ChatMsg);
            Socket.removeCallBack(CMD.WorldWar_mark, onWow3MarkInfo);

            Data.role.unbind("level", updateLevel);
            Data.role.unbind("level", showLevel);
            Data.role.unbind("quality", updateQuality);
            Data.role.unbind("vip", updateVip);
//            Data.role.unbind("role_name", updateNameTxt);
            Data.role.unbind("coin", updateCoin);
            Data.role.unbind("gold", updateGold);
            Data.role.unbind("ticket", updateTicket);
            Data.role.unbind("cpi", showCpi);
            Data.role.unbind("server_time", updateTime);
            Data.role.unbind("fight_max", showFightMax);
            Data.role.unbind("exp", updateExp);
            Data.role.unbind("icon", updateIcon);

        }
    }

    private void onWow3MarkInfo(JsonData msg)
    {
        int x = (int)msg["data"]["x"];
        int y = (int)msg["data"]["y"];
        int type = (int)msg["data"]["type"];
        string name = msg["data"]["name"].ToString();

        int op = (int)msg["data"]["op"];
        if (op == 1 && _worldWarPanel == null && Data.worldWar.markList != null)
        {
            Data.worldWar.markList[x + "|" + y] = new WorldWarData.MarkInfo { x = x, y = y, type = type, name = name };
        }

    }

    private void onQuestDone(JsonData msg)
    {
        Data.quest.updateQuest(msg["data"]);
        questAndHeroPanel.quest.GetComponent<QuestPanel>().addQuestItems();
    }

    private void onShare(JsonData msg)
    {
        if (GlobalTools.HasShare)
        {
            Data.role.update("act_share", 1);
        }
    }

    private void onWarn(JsonData msg) 
    {
        string str = (msg["data"]["msg"]).ToString();
        IDictionary dic = msg["data"];
        int quit = 0;
        int logout = 0;
        if (dic.Contains("quit"))
        {
            quit = (int)msg["data"]["quit"];
        }
        else if (dic.Contains("logout"))
        {
            logout = (int)msg["data"]["logout"];
        }

        Dialog.show(Manager.Language.GetString("DialogDefaultTitle"), str, yes => 
        {
            if (yes)
            {
                if (quit == 1)
                {
                    Manager.exit();
                }
                else if (logout == 1)
                {
                    //if (HoolaiSDK.isLogin == false)
                    //{
                    //    SceneManager.LoadScene(GlobalTools.LoginStage);
                    //    return;
                    //}
#if UNITY_EDITOR

                    SceneManager.LoadScene(GlobalTools.LoginStage);
                    return;
#else
                    Manager.logout();
                    return;
#endif
                }
            }
        },1);
    }

    private void onWarClose(JsonData msg)
    {
        Data.role.update("city_get", 1);
    }

    private void onKickInfo(JsonData msg)
    {
        Dialog.show(Manager.Language.GetString("SystemDialogTitle"), Manager.Language.GetString("IndexMain.onKickInfo.tip"), yes =>
        {
            //if (HoolaiSDK.isLogin == false)
            //{
            //    SceneManager.LoadScene(GlobalTools.LoginStage);
            //    return;
            //}
#if UNITY_EDITOR
            SceneManager.LoadScene(GlobalTools.LoginStage);
            return;
#else
            Manager.logout();
            return;
#endif
        }, 1);
    }

    private void onGiftInfo(JsonData msg)
    {
        Data.levelGift.initData(msg["data"]);

        if (isLevelGiftReward)
        {
            //adventrue.fightTxt.transform.parent.gameObject.SetActive(Data.levelGift.list.Length >= Cache.ActLevelGiftList.Count);

            if (Data.levelGift.list.Length >= Cache.ActLevelGiftList.Count)
            {
                levelGift.gameObject.SetActive(false);
                main.hideGiftAward1();
                return;
            }
            //levelGift.gameObject.SetActive(true);
            act_level_gift lg_info = (act_level_gift)Cache.ActLevelGiftList[Data.levelGift.list.Length + 1];
            if (isLevelGiftLast)
            {
                if (Data.role.level >= lg_info.level)
                {
                    main.showGiftAward1Panel(lg_info, true, false);
                }
                else 
                {
                    hideGiftAward1();
                    isLevelGiftLast = false;
                }
            }

            levelGift.show(lg_info.reward);
            GameObject _go = levelGift.transform.Find("firstSp").gameObject;
            if (lg_info == (act_level_gift)Cache.ActLevelGiftList[1])
            {
                levelGift.levelTxt.gameObject.SetActive(false);
                _go.SetActive(true);
            }
            else 
            {
                levelGift.levelTxt.gameObject.SetActive(true);
                levelGift.levelTxt.text = lg_info.name;
                _go.SetActive(false);
            }
        }
        isLevelGiftReward = false;


    }

    private void onUnionNotifyInfo(JsonData msg)
    {
        Data.role.union_apply = 1;
        main.indexPanel.checkKnightTip();
        Socket.Send(CMD.UnionRace_getInfo);
    }

    private void onGiftReward(JsonData msg)
    {
        isLevelGiftReward = true;
        Socket.Send(CMD.ActLevelGift_getinfo);
    }

    private void onNew(JsonData msg)
    {
        int pay = (int) msg["data"]["pay"];
        int id = (int) msg["data"]["id"];
        if (pay == 1)
        {
            Socket.Send(CMD.Vip_getInfo);
        }

        if (id == 1)
        {
            Data.role.update("act_timelimit", 1);
        }
        else if (id == 2)
        {
            main.indexPanel.giftBtn1.transform.Find("tipNum").gameObject.SetActive(true);
        }

        if (id >= 1 && id <= 3 && Data.role.vip < id)
        {
            checkBalance("vip", id, true);
        }

        if (pay == 1 && id == 3)
        {
            Data.role.act_forever = 2;
            main.indexPanel.giftBtn12.transform.Find("tipNum").GetComponent<TipSign>().show(1, Data.role.act_forever == 2);
        }

        Socket.Send(CMD.Resource_fetch);

        pay_info pinfo = (pay_info)Cache.PayInfo[id];
        if (pinfo != null)
        {
            Manager.Game_SetPayment(pinfo.id + "", "rmb", "gold", pinfo.price, pinfo.gold_now, pinfo.name, 1, Data.role.level);
        }
        
        
    }
    private void logingetdata(JsonData msg)
    {
        string username = (string)msg["data"]["username"];
        string guid = (string)msg["data"]["guid"];
    }

    private void loginCallBack(JsonData msg)
    {
        JsonData tMsg = new JsonData();
        tMsg["count"] = UnityEngine.Random.Range(0, 100000000);
        Socket.Send(CMD.Role_getInfo, tMsg);
    }

    private void onDailyInfo(JsonData msg)
    {
        int openDay = (int) msg["data"]["open_day"];
        Data.role.server_day = openDay;
        Cache.flashHeroList();
//        Cache.initHeroNew();
        Socket.Send(CMD.Role_getInfo);
    }

    private void onGetInfo(JsonData msg)
    {
        gotInfo = true;
        GC.Collect();
        onQuestMsg2(msg["data"]["quest"]);
        GC.Collect();
        onChatLogs2(msg["data"]["chat"]);
        infoWakeback2(msg["data"]["wake"]);
        onItemList2(msg["data"]["item"]);
        onEquipBack2(msg["data"]["team"]);
        GC.Collect();
        onExploreInfoBack2(msg["data"]["explore"]);
        GC.Collect();
        onWonderInfo2(msg["data"]["act_splendid"]);
        onHeroBack2(msg["data"]["hero"]);
        onMajorInfo(msg["data"]["weapon_type"]);
        GC.Collect();
        //onAllChatLogs(msg["data"]["chat_all"]);
        Invoke("initData", 10);

//        LoginMsg lmsg = new LoginMsg
//        {
//            userName = Data.role.user_name
//        
//        };
//        Socket.Send(CMD.Role_logins, lmsg);
    }

    private void initData()
    {
        if (indexPanel.listBtn.isEnabled)
        {
            var t = Cache.HeroCollectInfoList;
        }
    }

    private void onAllChatLogs(JsonData msg) 
    {
        Data.chat.initAllChatList(msg);

        adventrue.showTalkMsgHistory();
    }

    private void onMajorInfo(JsonData msg)
    {
        Data.equip.initMajorInfo(msg);
    }

    private void onBalance(JsonData msg)
    {
        IDictionary dic = msg["data"];
        if (!dic.Contains("balance"))
        {
            return;
        }
        Data.role.balance = msg["data"]["balance"];
        Data.role.initBalance();
        showBanlance();
    }

    private void onlogin(JsonData msg)
    {
        Socket.Send(CMD.RoleExplore_balance);
    }

    private void onPushMessage(JsonData msg)
    {
//        Debuger.Log(msg["data"].ToJson());
        if (pushPanel)
        {
            pushPanel.showMsg(msg["data"]);
        }
    }

    private void onChatLogs2(JsonData msg)
    {
        if (!Data.chat.Init)
        {
            Data.chat.initData(msg);
            Data.chat.init();
            if (Data.chat.msgList != null)
            {
                showMsgList = (ArrayList)Data.chat.msgList.Clone();
                showChatScreen();
            }
        }
    }

    private void onChatLogs(JsonData msg)
    {
        if (!Data.chat.Init)
        {
            Data.chat.initData(msg["data"]);
            Data.chat.init();
            if (Data.chat.msgList != null)
            {
                showMsgList = (ArrayList)Data.chat.msgList.Clone();
                showChatScreen();
            }
        }
    }

    private void onChatMsg(JsonData msg)
    {

        int chat = PlayerPrefsUtility.HasKey(GlobalTools.ChatMsg) ? PlayerPrefsUtility.GetInt(GlobalTools.ChatMsg) : 1;
        if (chat == 1)
        {
            int type = (int)(msg["data"]["type"]);
            string uid = msg["data"]["uid"].ToString();
            //전국 채널 연설 팁
            if (type == 2 && Data.role.uid != uid)
            {
                indexPanel.chatTipSign.show(1);
            }
        }

        string msgStr = msg["data"]["msg"].ToString();
        adventrue.showTalkMsg(msgStr);

        string historyMsg = PlayerPrefsUtility.HasKey(GlobalTools.ChatHistoryMsg) ? PlayerPrefsUtility.GetString(GlobalTools.ChatHistoryMsg) : "1,2,3,4";
        string[] msgArr = historyMsg.Split(',');
        int _type = (int)(msg["data"]["type"]);
        for (int i = 0; i < msgArr.Length; i++)
        {
            if (_type.ToString() == msgArr[i])
            {
                if (Data.chat.allChatList.Count >= 8)
                {
                    Data.chat.allChatList.RemoveAt(0);
                }
                ChatData.Msg msgInfo = Data.chat.initChatMsgRec(msg["data"]);
                Data.chat.allChatList.Add(msgInfo);
                adventrue.showTalkMsgHistory();
            }
        }
    }

    private void onChatRedPacket(JsonData msg) 
    {
        int type = (int)msg["data"]["type"];
        //indexPanel.redPacketTipSign.show(type, indexPanel.chatBtn.isEnabled);
        chatPanel.isMsg = true;
        Socket.Send(CMD.Chat_getRedPacket);
    }

    private void onWow3ChatMsg(JsonData msg)
    {
        WorldWarChatData.WorldWarChatMsg msgInfo = Data.chat.json2info<WorldWarChatData.WorldWarChatMsg>(msg["data"]);

        Data.worldWarChat.AddMsg(msgInfo);
    }

    public void showTargetDone(int id)
    {
 
        indexPanel.showWow3TargetDone(id);
    }

    public void infoCollectBack(JsonData msg)
    {
        if (!Data.collect.Init)
        {
            Data.collect.initData(msg["data"]);
            Data.collect.init();
        }
    }

    public void infoWakeback2(JsonData msg)
    {
        if (Data.hero.stoneList == null)
        {
            Data.hero.initStone(msg);
            checkHeroStone();
        }
    }

    public void infoWakeback(JsonData msg)
    {
        if (Data.hero.stoneList == null)
        {
            Data.hero.initStone(msg["data"]);
            checkHeroStone();
        }
    }
    public void infoCallBack(JsonData msg)
    {
        Data.call.initData(msg["data"]);
        Data.call.init();
        
    }

    private void onWonderInfo2(JsonData msg)
    {
        Data.activity.initWonder(msg);
    }

    private void onWonderInfo(JsonData msg)
    {
        Data.activity.initWonder(msg["data"]);
    }

    private void onWonderRankInfo(JsonData msg) 
    {
        Data.activity.initRankInfo(msg);
    }

    private void onQuestMsg2(JsonData msg)
    {
        Data.quest.initData(msg);
        Data.quest.init();
    }

    private void onQuestMsg(JsonData msg)
    {
        Data.quest.initData(msg["data"]);
        Data.quest.init();
    }

    private void onStoryNotify(JsonData msg)
    {
//        Data.hero.storyTip = 1;
//        indexPanel.storyTipSign.show(1);
        Data.hero.initNewGet(msg["data"]["first"]);
        Data.hero.update("storyTip", 1);
        Invoke("checkNewHeroShow", 3.2f);
    }

    public void checkNewHeroShow()
    {
        return;
        if (Data.hero.newList != null && Data.hero.newList.Count > 0)
        {
            setting_info hero_popup_level = (setting_info)Cache.SettingInfo["hero_popup_level"];
            if (Data.role.level < Convert.ToInt32(hero_popup_level.value))
            {
                return;
            }
            //            Data.hero.newList.Add(50051);
            var hero = (hero_info)Cache.heroList[Data.hero.newList[0]];
            showCardPanel(hero);
            Data.hero.newList.RemoveAt(0);
        }
    }

    private void onSignInfo(JsonData msg)
    {
        Data.signIn.initData(msg["data"]);
        Data.signIn.init();

        showSignIn();
    }

    private void onAgreexNew(JsonData msg)
    {
        indexPanel.friendTipSign.show(1);
    }

    private void onFirendNew(JsonData msg)
    {
        indexPanel.friendTipSign.show(1);
    }

    private void onEmailNew(JsonData msg)
    {
        indexPanel.emailTipSign.show(1);
    }

    private void onEquipBack2(JsonData msg)
    {
        Data.equip.initData(msg);
        Data.equip.init();
    }

    private void onEquipBack(JsonData msg)
    {
        Data.equip.initData(msg["data"]);
        Data.equip.init();
    }

    private void onEventBack(JsonData msg)
    {
        Data.adventure.currentInfo.openid = (int) msg["data"]["openid"];
        Data.adventure.currentInfo.events = msg["data"]["events"];
        Data.adventure.currentInfo.initEvent();
//        print("openid->" + (int) msg["data"]["openid"]);
        if (Data.adventure.currentInfo.openid > 0)
        {
//            print("Data.adventure.openid->" + Data.adventure.openid);
            var nextMain = (main) CacheData.Instance.mainList[Data.adventure.currentInfo.openid];
            Data.adventure.openid = Data.adventure.currentInfo.openid;
//            if (Data.role.stamina > 1)
//            {
//                adventrue.enterNew.gameObject.SetActive(true);
//            }
            adventrue.enterNew.gameObject.SetActive(true);
        }
        int count = Data.adventure.getCrossCount() - 1;
        gameGuide.checkShow("main:" + Data.adventure.currentInfo.id + ":" + count);
//                        gameGuide.checkShow("main:3:1");
    }

    private void onCombatCompleteBack(JsonData msg)
    {
//        Debuger.Log(msg["data"].ToJson());

        Data.battle.initData(msg["data"]);
        Data.battle.initBattle();
        showBattlePanel();

        hideMystery();
    }

    private void onExploreInfoBack2(JsonData msg)
    {
        Data.adventure.initData(msg);
        Data.adventure.init();

        if (adventrue.gameObject.activeSelf)
        {
            adventrue.show();
        }
    }

    private void onExploreInfoBack(JsonData msg)
    {
        Data.adventure.initData(msg["data"]);
        Data.adventure.init();

        if (adventrue.gameObject.activeSelf)
        {
            adventrue.show();
        }
    }

    private void onHeroInfoBack(JsonData msg)
    {
        IDictionary heroDic = msg;
        var update = heroDic.Contains("update");
        var delete = heroDic.Contains("delete");
        var create = heroDic.Contains("create");
        if (create)
        {
            Data.hero.heroUpdate(msg["create"]);
            IDictionary dcreate = msg["create"];
            foreach (DictionaryEntry entry in dcreate)
            {
                var info = Data.hero.json2info<HeroInfo>(entry.Value as JsonData);
                Tip.show(string.Format(Manager.Language.GetString("IndexMain.onHeroInfoBack.tip"), ItemConstants.getColorNameByQuality(info.baseInfo.name, info.baseInfo.appraise)));

                if (herosPanel != null && herosPanel.gameObject.activeSelf)
                {
                    Data.hero.update("showList", 1);
                }
            }
        }
        if (update)
        {
            ArrayList list = Data.hero.heroUpdate(msg["update"]);

            heroLevel.show(list);
        }
        if (delete)
        {
            Data.hero.heroDelete(msg["delete"]);
        }
    }

    public void onHeroBack2(JsonData msg)
    {
        Data.hero.initData(msg);
        Data.hero.init();

        Data.hero.update("formationTip", Data.role.fight_max - Data.hero.fightList.Count);
        showHeroList();


    }

    public void onHeroBack(JsonData msg)
    {
        Data.hero.initData(msg["data"]);
        Data.hero.init();

        Data.hero.update("formationTip", Data.role.fight_max -  Data.hero.fightList.Count);
        showHeroList();
    }

    public void showHeroBG()
    {
        Res.showLoginTexture(heroBG, "" + "ascending" + Data.role.quality);
        nameText.text = HeroConstants.getHeroName(Data.role.role_name, Data.role.quality);
    }

    public void onItemList2(JsonData msg)
    {
        Data.bag.initData(msg);
        //        print(msg.ToJson());
    }

    public void onItemList(JsonData msg)
    {
        Data.bag.initData(msg["data"]);
//        print(msg.ToJson());
    }

    public void onEcho(JsonData msg)
    {
        print(msg.ToJson());
    }

    // ReSharper disable once FunctionComplexityOverflow
    public void onChange(JsonData msg)
    {
        var change = msg["change"];

//        print(change.ToJson());

        IDictionary dic = change;
//        var balance = dic.Contains("balance");
        var balance = true;
        if (dic.Contains("quest"))
        {
            IDictionary dquest = change["quest"];
            foreach (DictionaryEntry qMsg in dquest)
            {
                var qqMsg = (JsonData) qMsg.Value;
                qqMsg["nextid"] = Convert.ToInt32(qMsg.Key.ToString());
                QuestData.QuestInfo info = Data.quest.updateQuest(qqMsg);

                if (info.state == 1)
                {
                    gameGuide.checkShow("quest:" + info.id);
                    indexPanel.showQuestDone();
                }
            }
            
        }

        if (dic.Contains("act_splendid"))
        {
            IDictionary idic = change["act_splendid"];
            foreach (DictionaryEntry qMsg in idic)
            {
                var qqMsg = (JsonData) qMsg.Value;
                var aid = qMsg.Key.ToString();
                var id = qqMsg["id"].ToString();
                var num = Convert.ToInt32(qqMsg["num"].ToString());
                var max = Convert.ToInt32(qqMsg["max"].ToString());
                Data.activity.updateWonder(aid, id, num, max);
            }
        }
        
        if (dic.Contains("act_splendidx"))
        {
            IDictionary idic = change["act_splendidx"];
            foreach (DictionaryEntry qMsg in idic)
            {
                var qqMsg = (JsonData) qMsg.Value;
                var aid = qMsg.Key.ToString();

                for (int i = 0; i < qqMsg.Count; i++)
                {
                    var id = qqMsg[i]["id"].ToString();
                    var num = Convert.ToInt32(qqMsg[i]["num"].ToString());
                    var max = Convert.ToInt32(qqMsg[i]["max"].ToString());
                    Data.activity.updateWonder(aid, id, num, max);
                }
            }
        }

        
        if (dic.Contains("act_target"))
        {
            IDictionary idic = change["act_target"];
            foreach (DictionaryEntry qMsg in idic)
            {
                var qqMsg = (JsonData) qMsg.Value;
                var aid = Convert.ToInt32(qMsg.Key.ToString());
                var num = Convert.ToInt32(qqMsg["num"].ToString());
                var max = Convert.ToInt32(qqMsg["max"].ToString());
                Data.role.update("act_target", num >= max ? 1 : 0);
            }
        }
        

        if (dic.Contains("act_active"))
        {
            IDictionary idic = change["act_active"];
            foreach (DictionaryEntry qMsg in idic)
            {
                var qqMsg = (JsonData) qMsg.Value;
                var aid = Convert.ToInt32(qMsg.Key.ToString());
                var num = Convert.ToInt32(qqMsg["num"].ToString());
                var max = Convert.ToInt32(qqMsg["max"].ToString());
                Data.role.update("act_active", num >= max ? 1 : 0);
            }
        }
        
        if (dic.Contains("act_crazy7"))
        {
            IDictionary idic = change["act_crazy7"];
            foreach (DictionaryEntry qMsg in idic)
            {
                var qqMsg = (JsonData)qMsg.Value;
                var aid = Convert.ToInt32(qMsg.Key.ToString());
                var num = Convert.ToInt32(qqMsg["num"].ToString());
                var max = Convert.ToInt32(qqMsg["max"].ToString());
                Data.role.update("act_crazy7", num >= max ? 1 : 0);
            }
        }
        if (dic.Contains("magic"))
        {
//            print(change["magic"].ToJson() + "!!!!!!!!!!!!!");
            indexPanel.roleTipSign.show(1);
        }
        if (dic.Contains("magic_add"))
        {
            showMagicDetailPanel(change["magic_add"]);
        }
        if (dic.Contains("heros"))
        {
            onHeroInfoBack(change["heros"]);
        }
        if (dic.Contains("server_time"))
        {
            Data.role.update("server_time", (int) change["server_time"]);
        }
        if (dic.Contains("fight_max"))
        {
            Data.role.update("fight_max", Convert.ToInt32(change["fight_max"].ToString()));
        }
        if (dic.Contains("cpi"))
        {
            Data.role.update("cpi", Data.role.cpi + (int) change["cpi"]);
        }
        if (dic.Contains("exp"))
        {
            Data.role.lastExp = (int)change["exp"];
            checkBalance("exp", (int) change["exp"], balance);
        }
        if (dic.Contains("level"))
        {
            checkBalance("level", (int) change["level"], balance);
        }
        if (dic.Contains("quality"))
        {
            checkBalance("quality", (int) change["quality"], true);
        }
        if (dic.Contains("vip"))
        {
            checkBalance("vip", (int) change["vip"], balance);
        }
        if (dic.Contains("icon"))
        {
            Data.role.update("icon", change["icon"].ToString());
        }
        if (dic.Contains("mod"))
        {
            Data.role.update("mod", change["mod"].ToString());
        }
        if (dic.Contains("coin"))
        {
            string value = change["coin"].ToString();
            Data.role.lastCoin = Convert.ToInt64(value);
            Data.role.update("coin", Convert.ToInt64(value));
//            int value = (int) change["coin"];
//            if (balance == false && value > 0)
//            {
//                Tip.show("金币 + " + value);
//            }
        }
        if (dic.Contains("gold"))
        {
            int getNum = (int)change["gold"];
            bool balanceGold = getNum <= Data.role.gold;

//            if (balance == false)
//            {
//                diamond.show(getNum - Data.role.gold);
//            }
            Data.role.update("gold", getNum);
        }
        if (dic.Contains("ticket"))
        {
            int getNum = (int)change["ticket"];
            bool balanceTicket = getNum <= Data.role.ticket;

            if (balanceTicket == false)
            {
                diamond.show(getNum - Data.role.ticket);
            }
            Data.role.update("ticket", getNum);
        }
        if (dic.Contains("crystal"))
        {
            Data.role.update("crystal", (int)change["crystal"]);
        }
        if (dic.Contains("summon_soul"))
        {
            Data.role.update("summon_soul", (int)change["summon_soul"]);
        }
        if (dic.Contains("glory_level"))
        {
            Data.role.update("glory_level", (int)change["glory_level"]);
        }
        if (dic.Contains("glory"))
        {
            Data.role.update("glory", (int)change["glory"]);
        }
        if (dic.Contains("stamina"))
        {
            checkBalance("stamina", (int) change["stamina"], balance);
        }
        if (dic.Contains("buy_stamina"))
        {
            Data.role.update("buy_stamina", (int) change["buy_stamina"]);
        }
        if (dic.Contains("merge_point"))
        {
            Data.role.update("merge_point", (int) change["merge_point"]);
        }
        if (dic.Contains("hero_soul"))
        {
            Data.role.update("hero_soul", (int)change["hero_soul"]);
        }
        if (dic.Contains("arena_point"))
        {
            Data.role.update("arena_point", (int)change["arena_point"]);
        }
        if (dic.Contains("arena_glory"))
        {
            Data.role.update("arena_glory", (int)change["arena_glory"]);
        }
        if (dic.Contains("friendship"))
        {
            var value = (int) change["friendship"];
            checkBalance("friendship", value, balance);
//            if (balance == false && value > 0)
//            {
//                Tip.show(value);
//            }
        }
        if (dic.Contains("courage_medal"))
        {
            var value = (int) change["courage_medal"];
            checkBalance("courage_medal", value, balance);
//            if (balance == false && value > 0)
//            {
//                Tip.show(value);
//            }
        }
        if (dic.Contains("champion_medal"))
        {
            var value = (int) change["champion_medal"];
            checkBalance("champion_medal", value, balance);
//            if (balance == false && value > 0)
//            {
//                Tip.show(value);
//            }
        }
        if (dic.Contains("league_point"))
        {
            var value = (int) change["league_point"];
            checkBalance("league_point", value, balance);
//            if (balance == false && value > 0)
//            {
//                Tip.show(value);
//            }
        }
        if (dic.Contains("league_medal"))
        {
            var value = (int) change["league_medal"];
            checkBalance("league_medal", value, true);
//            if (balance == false && value > 0)
//            {
//                Tip.show(value);
//            }
        }
        if (dic.Contains("union_pos"))
        {
            var value = (int)change["union_pos"];
            checkBalance("union_pos", value, true);
        }
        if (dic.Contains("union_point"))
        {
            var value = (int)change["union_point"];
            checkBalance("union_point", value, true);
        }
        if (dic.Contains("union_war_double"))
        {
            var value = (int)change["union_war_double"];
            checkBalance("union_war_double", value, true);
        }
        if (dic.Contains("union_medal"))
        {
            var value = (int)change["union_medal"];
            checkBalance("union_medal", value, true);
        }
        if (dic.Contains("give"))
        {
            if (nowPanel == null || (nowPanel != null && notShowAwardList.IndexOf(nowPanel.name) == -1))
            {
                if ((_signInPanel == null || !_signInPanel.gameObject.activeSelf))
                {
                    var give = change["give"];
                    var giveStr = give.ToString();
                    awardGivePanel.show(giveStr);
                }
            }
        }
        
        if (dic.Contains("wood"))
        {
            Data.role.update("wood", (int)change["wood"]);
        }
        if (dic.Contains("food"))
        {
            Data.role.update("food", (int)change["food"]);
        }
        if (dic.Contains("iron"))
        {
            Data.role.update("iron", (int)change["iron"]);
        }
        if (dic.Contains("rock"))
        {
            Data.role.update("rock", (int)change["rock"]);
        }
        if (dic.Contains("hinotama"))
        {
            Data.role.update("hinotama", (int)change["hinotama"]);
        }
        if (dic.Contains("mizunotama"))
        {
            Data.role.update("mizunotama", (int)change["mizunotama"]);
        }
        if (dic.Contains("kinotama"))
        {
            Data.role.update("kinotama", (int)change["kinotama"]);
        }
        if (dic.Contains("wood") || dic.Contains("food") || dic.Contains("iron") || dic.Contains("rock") || dic.Contains("hinotama") || dic.Contains("mizunotama") || dic.Contains("kinotama"))
        {
            if (_worldWarPanel != null)
            {
                _worldWarPanel.showRes();
                //Socket.Send(CMD.WorldWar_income);
            }
        }
        if (dic.Contains("prestige"))
        {
            if (_worldWarPanel != null && Data.worldWar.upres != null)
            {
                int prestige = (int)msg["change"]["prestige"];
                Data.worldWar.upres.prestige = prestige;
                _worldWarPanel.showRes();
            }
        }
        if (dic.Contains("rune"))
        {
            IDictionary runeDic = change["rune"];
            if (Data.worldwarrune.runeList!=null && runeDic.Contains("update"))
            {
                var update = change["rune"]["update"];
                IDictionary dupdate = update;
                var index = dupdate.GetEnumerator();
                while (index.MoveNext())
                {
                    int id = Convert.ToInt32(index.Key.ToString());
                    if (id == 0) continue;
                    JsonData data = (JsonData)index.Value;
                    Data.worldwarrune.UpdateRuneList(id,data);
                }
            }
            if (Data.worldwarrune.runeList != null && runeDic.Contains("delete"))
            {
                var delete = change["rune"]["delete"];
                for (var i = 0; i < delete.Count; i++)
                {
                    int id = Convert.ToInt32(delete[i].ToJson());
                    Data.worldwarrune.DeleteRuneById(id);
                }
            }
            if(_worldWarPanel) _worldWarPanel.checkRuneTips();
        }
        if (dic.Contains("license_num"))
        {
            Data.worldWar.update("license_num", (int)change["license_num"]);
        }
        if (dic.Contains("license_expire"))
        {
            Data.worldWar.update("license_expire", (int)change["license_expire"]);
        }
        if (dic.Contains("items"))
        {
            IDictionary itemDic = change["items"];
            bool checkStone = false;
            bool foodStone = false;
//            bool checkFood = false;
            if (Data.bag.itemList != null && itemDic.Contains("update"))
            {
                var update = change["items"]["update"];

                ItemInfo newHero = null;
                ItemInfo evolutionHero = null;
                hero_info heroInfo = null;
                hero_info _newheroInfo = null;
                hero_info _evolutionheroInfo = null;
                IDictionary dupdate = update;
                foreach (DictionaryEntry entry in dupdate)
                {
                    var info = Data.bag.addItem((JsonData) entry.Value);

                    if (info.baseInfo.type == ItemInfo.TypeEquip)
                    {
                        for (int i = 1; i <= 5; i++)
                        {                                                      
                            if (Data.equip.compare(info, i))
                            {
                                Data.equip.update("hasMergyNew", Data.equip.hasMergyNew + 1);
                                break;
                            }
                        }
                    }
                    if (info.addnum > 0)
                    {
                        bool isNew = false;
                        if (info.baseInfo.type == ItemInfo.TypeHero)
                        {
                            var heroId = Convert.ToInt32(StringUtil.Split(info.baseInfo.loot_id, ":")[1]);
                            heroInfo = (hero_info)Cache.heroList[heroId];
                            if (heroInfo.appraise >= 4)
                            {
                                if (Data.role.fight_max > Data.hero.fightList.Count || checkHeroComprehensive(heroInfo))
                                {
                                    if (!collectPanel.gameObject.activeSelf)
                                    {
                                        isNew = true;
                                        if (newHero == null)
                                        {
                                            newHero = info;
                                            _newheroInfo = heroInfo;
                                        }
                                        else if (newHero.baseInfo.appraise < info.baseInfo.appraise)
                                        {
                                            newHero = info;
                                            _newheroInfo = heroInfo;
                                        }
                                    }
                                }
                            }

                            menus m_Info = (menus)Cache.MenusList["HerosPanel_EvolutionPanel"];
                            foreach (DictionaryEntry entry1 in Data.hero.fightList) 
                            {
                                HeroInfo hInfo = (HeroInfo)entry1.Value;
                                if (hInfo.baseInfo.uid == heroId && m_Info.level <= Data.role.level)
                                {
                                    long selfCount = Data.bag.getItemCount(hInfo.baseInfo.hero_card);
                                    hero_upgrade upInfo = Cache.getUpgradeByQuality(hInfo.quality, hInfo.upgrade_level + 1);
                                    if (upInfo == null)
                                    {
                                        continue;
                                    }
                                    int cardNum = upInfo.card_num;
                                    ResInfo gold = ResInfo.str2info(upInfo.extra_req);
                                    if (Convert.ToInt32(selfCount) >= cardNum && evolutionHero == null && Data.role.coin > gold.num)
                                    {
                                        evolutionHero = info;
                                        _evolutionheroInfo = heroInfo;
                                        Data.hero.update("evolutionTip", 1);
                                        break;
                                    }
                                }
                            }

                            setting_info alert = (setting_info)Cache.SettingInfo["hero_collect_alert_level"];
                            int alertLevel = Convert.ToInt32(alert.value);
                            //                            Data.collect.checkHas()
                            if (Data.role.level >= alertLevel)
                            {
                                string[] getCArr;
                                Hashtable cList = Cache.HeroCollectInfoIndexsList;
                                string cStr = (string)cList[info.sysid];
                                if (!string.IsNullOrEmpty(cStr))
                                {
                                    string[] sArr = StringUtil.Split(cStr, ",");
                                    for (int j = 0; j < sArr.Length; j++)
                                    {
                                        int cid = Convert.ToInt32(sArr[j]);
                                        hero_collect_info cInfo = (hero_collect_info)Cache.HeroCollectInfoList[cid];

                                        if (!Data.collect.checkHas(cid, info.sysid))
                                        {
                                            Data.collect.update("collectTip", 1);
                                            if (!isNew)
                                            {
                                                showNewCollect(info, heroInfo.comprehensive, ItemConstants.getColorNameByQuality("【" + cInfo.name + "】", cInfo.appraise));
                                            }
                                            break;
                                        }
                                    }
                                }
                            }

                            menus menuInfo = (menus)Cache.MenusList["ActGoldheroPanel"];
                            if (Data.role.level >= menuInfo.level && Data.role.act_goldenhero != 0)
                            {
                                for (int i = 0; i < Data.activity.actGoldHero.actGoldHeroInfoList.Count; i++)
                                {
                                    ActivityData.ActGoldHeroInfo ac_info = Data.activity.actGoldHero.actGoldHeroInfoList[i];
                                    if (ac_info.heroid == heroId && ac_info.state == 0)
                                    {
                                        main.indexPanel.giftBtn14.transform.Find("tipNum").GetComponent<TipSign>().show(1);
                                        ac_info.state = 1;
                                    }
                                }
                            }

//                            if (_newHeroPanel == null)
//                            {
//                                
//                            }
                            
//                            newCollectPanel(info, hero.comprehensive);
                        }
                        else if (info.baseInfo.kind == ItemInfo.KindStone)
                        {
                            checkStone = true;
                        }
                        else if (info.baseInfo.kind == ItemInfo.KindFood)
                        {
                            foodStone = true;
                        }
                        else if (info.baseInfo.id == "hero_upgrade_card")
                        {
                            hero_upgrade upInfo = Cache.getUpgradeByQuality(Data.hero.master.quality, Data.hero.master.upgrade_level + 1);
                            if (upInfo == null) continue;
                            int cardNum = upInfo.card_num_special;
                            long selfCount = Data.bag.getItemCount("hero_upgrade_card");
                            if (selfCount >= cardNum)
                            {
                                Data.hero.update("evolutionTip", 1);
                            }
                        }
//                        else if (info.baseInfo.kind == ItemInfo.KindFood)
//                        {
//                            checkFood = true;
//                        }

//                        if (info.quality != 0)
//                        {
//                            Tip.show("获得了物品：" + ItemConstants.getColorNameByQuality(info.baseInfo.name, info.quality) +
//                                     " *" +
//                                     info.addnum);
//                        }
//                        else
//                        {
//                            Tip.show("获得了物品：" +
//                                     ItemConstants.getColorNameByQuality(info.baseInfo.name, info.baseInfo.appraise) +
//                                     " *" +
//                                     info.addnum);
//                        }
//                        Debuger.Log("add Item" + info.baseInfo.name + " :" + info.num);
                    }
                }
                if (newHero != null)
                {
                    showNewHeroOpen(newHero, _newheroInfo.comprehensive);
                }
                if (evolutionHero != null)
                {
                    showNewHeroOpen(evolutionHero, _evolutionheroInfo.comprehensive, 2);
                }
                
            }

            if (Data.bag.itemList != null && itemDic.Contains("delete"))
            {
                var delete = change["items"]["delete"];
                for (var i = 0; i < delete.Count; i++)
                {
                    int itemId = Convert.ToInt32(delete[i].ToJson());
                    Data.bag.delItem(itemId);
//                    Data.bag.itemList.Remove(Convert.ToInt32(delete[i].ToJson()));
                }
            }

            if (Data.bag.itemList != null)
            {
                Hashtable itemList = Data.bag.itemList;

                int green = 0;
                int food = 0;
                int piece = 0;
                int major = 0;
                Dictionary<string,int> equipTypeList = new Dictionary<string,int> ();
                
                foreach (DictionaryEntry entry in itemList)
                {
                    ItemInfo info = (ItemInfo)entry.Value;
                    if (info.flag == 0)
                    {
                        if (info.baseInfo.type == ItemInfo.TypeHero && info.baseInfo.appraise == 2)
                        {
                            green++;
                        }
                        else if (info.baseInfo.type == ItemInfo.TypeEquip)
                        {
                            int eCount;
                            if (equipTypeList.ContainsKey(info.baseInfo.equip_type))
                            {
                                eCount = equipTypeList[info.baseInfo.equip_type];
                            }
                            else
                            {
                                eCount = 0;
                            }
                            eCount ++;
                            equipTypeList[info.baseInfo.equip_type] = eCount;

                        }
                        else if (info.baseInfo.kind == 2)
                        {
                            food += Convert.ToInt32(info.num);
                        }
                        else if (majorItemList.Contains(info.sysid))
                        {
                            if (Data.equip.majorInfo == null)
                            {
                                return;
                            }
                            string typeStr = info.sysid.Substring(info.sysid.Length - 1, 1);
                            int type = Convert.ToInt32(typeStr);
                            EquipData.MajorList mInfo = Data.equip.majorInfo.getMajorByType(type);
                            weapon_type_level lInfo = (weapon_type_level)Cache.WeaponTypeLevelListList[mInfo.level + 1];

                            if (lInfo != null && Data.bag.getItemCount(info.sysid) >= lInfo.req_num)
                            {
                                major++;
                            }
                        }
                        else if (!string.IsNullOrEmpty(info.baseInfo.compose))
                        {
                            int count = Convert.ToInt32(StringUtil.Split(info.baseInfo.compose, ":")[0]);
                            if (info.num >= count)
                            {
                                piece++;
                            }
                        }
                        
                    }
                }

                if (green >= 5)
                {
                    if (indexPanel.getHeroMergyOpen())
                    {
                        Data.mergy.update("heroTip", 1);
                    }
                }
                else
                {
                    Data.mergy.heroTip = 0;
                }

//                int greenE = 0;
//                var eEntry = equipTypeList.GetEnumerator();
//                while (eEntry.MoveNext())
//                {
//                    if (eEntry.Current.Value >= 10)
//                    {
//                        greenE++;
//                    }
//                }
//                eEntry.Dispose();

//                if (greenE > 0)
//                {
//                    Data.mergy.update("equipTip", 1);
//                }
//                else
//                {
//                    Data.mergy.equipTip = greenE;
//                }
                setting_info food_alert_num = (setting_info)Cache.SettingInfo["food_alert_num"];
                int tipCount = Convert.ToInt32(food_alert_num.value);
                if (food > tipCount && foodStone)
                {
                    if (Data.hero.fightList != null)
                    {
                        foreach (DictionaryEntry entry in Data.hero.fightList)
                        {
                            var hero = (HeroInfo)entry.Value;
                            if (hero.level < hero.level_max)
                            {
                                Data.bag.update("foodTip", 1);
                                break;
                            }
                        }
                    }
                }
                Data.bag.update("pieceTip", piece);

                if (major > 0)
                {
                    Data.equip.update("majorTip", major);
                }
            }

            if (checkStone)
            {
                checkHeroStone();
            }
        }
    }

    public void checkHeroStone()
    {
        if (Data.hero.fightList == null)
            return;

        Data.hero.stoneTipHero.Clear();
        foreach (DictionaryEntry entry in Data.hero.fightList)
        {
            var hero = (HeroInfo)entry.Value;

            HeroData.HeroStones stoneInfo = Data.hero.getHeroStone(hero.pid);
            if (stoneInfo == null)
            {
                continue;
            }
            hero_awake wakeInfo = Cache.getWakeInfo(hero.quality, hero.wake_level + 1, hero.baseInfo.weapon_type);
            int done = 0;
            if (wakeInfo == null)
            {
                done = 1;
                if (hero.master == 1)
                {
                    continue;
                }
            }
            hero_awake_level wakeLevel = Cache.getWakeLevelInfo(hero.quality, hero.baseInfo.weapon_type);
            HeroData.Stones stone = stoneInfo.stones;

            if (stoneInfo.skill_level >= wakeLevel.level_up_num)
            {
                done = 2;
                continue;
            }
            if (done == 1)
            {
                wakeInfo = Cache.getWakeInfo(hero.quality, hero.baseInfo.weapon_type);
                stone = stoneInfo.skill_slot;
            }
            
            int tipCount = 0;
            bool hasItem = false;
            for (int i = 1; i <= 5; i++)
            {
                
                PropertyInfo pinfo = stone.GetType().GetProperty("s" + i);
                string value = (string)pinfo.GetValue(stone, null);
                if (wakeInfo != null)
                {
                    PropertyInfo winfo = wakeInfo.GetType().GetProperty("stone_" + i);
                    string itemStr = (string)winfo.GetValue(wakeInfo, null);
                    ItemInfo itemInfo = ResInfo.str2item(itemStr);
                    if (done == 1)
                    {
                        hero_awake_skill_level skillLevel = (hero_awake_skill_level)Cache.HeroAwakeSkillLevelList[hero.skill_level];
                        itemInfo.num = itemInfo.num * skillLevel.multi;
                    }
                    if (string.IsNullOrEmpty(value))
                    {
                        hasItem = true;
                        if (Data.bag.getItemHas(itemInfo.sysid, itemInfo.num))
                        {
                            tipCount++;
                        }
                        else if (Data.bag.getItemMergy(itemInfo.sysid, itemInfo.num*3))
                        {
                            tipCount++;
                        }
                    }
                    else
                    {
                        tipCount++;
                    }
                }
            }

            if (tipCount == 5 && hasItem)
            {
                Data.hero.stoneTipHero.Add(hero.pid);
                Data.hero.update("stoneTip", 1);
            }
            long selfCount = Data.bag.getItemCount(hero.baseInfo.hero_card);
            hero_upgrade upInfo = Cache.getUpgradeByQuality(hero.quality, hero.upgrade_level + 1);

            if (upInfo != null && selfCount >= upInfo.card_num)
            {
                ResInfo gold = ResInfo.str2info(upInfo.extra_req);
                if (Data.role.coin > gold.num)
                {
                    Data.hero.update("evolutionTip", 1);
                }
            }
        }
    }

    private bool checkHeroComprehensive(hero_info chero)
    {
        
        foreach (DictionaryEntry entry in Data.hero.fightList)
        {
            var hero = (HeroInfo) entry.Value;

            if (chero.hero_compose_index > hero.baseInfo.hero_compose_index && hero.master != 1)
            {
                return true;
            }
        }
        return false;
    }

    private void checkBalance(string propName, int value, bool balance)
    {
        var count = (int) Data.role.GetType().GetProperty(propName).GetValue(Data.role, null);
        if (balance)
        {
            Data.role.update(propName, value);
        }
        else
        {
            Data.role.update(propName, count + value);
        }
    }

    private void updateQuality(object value)
    {
        showHeroBG();
    }

    private void showLevel(object value)
    {
        levelUpPanel.show();

        gameGuide.checkShow("level:" + Data.role.level);

        Dictionary<string, string> userExtData = new Dictionary<string, string>();

        userExtData.Add(HoolaiSDK.ROLE_ID, Data.role.uid);
        userExtData.Add(HoolaiSDK.ROLE_NAME, Data.role.role_name);
        userExtData.Add(HoolaiSDK.ROLE_LEVEL, Data.role.level + "");
        userExtData.Add(HoolaiSDK.ZONE_ID, Data.role.zone_id);
        userExtData.Add(HoolaiSDK.ZONE_NAME, Data.role.zone_name);
        userExtData.Add(HoolaiSDK.BALANCE, Data.role.gold + "");
        userExtData.Add(HoolaiSDK.VIP, "1");
        userExtData.Add(HoolaiSDK.PARTYNAME,  Manager.Language.GetString("ServerSelectPanel.userExtData"));
        userExtData.Add(HoolaiSDK.APP_VERSION, HoolaiSDK.APP_VERSION);
        userExtData.Add(HoolaiSDK.APP_RES_VERSION, Manager.GameSetting.GetString("Client"));

        userExtData.Add(HoolaiSDK.ACTION, HoolaiSDK.ACTION_LEVEL_UP);
        Manager.setExtData(JsonMapper.ToJson(userExtData));

    }

    public void addShowList(Action act)
    {
        if (showList == null)
        {
            showList = new ArrayList();
        }
        showList.Add(act);
    }

    public void checkShowList()
    {
        if (showList != null && showList.Count > 0)
        {
            Action act = (Action) showList[0];
            act();
            showList.RemoveAt(0);
        }
    }

    private int lastLevel;
    private void updateLevel(object value)
    {
        
//        role_levelup_exp roleExp = (role_levelup_exp) Cache.RoleLevelupExpList[Data.role.level + 1];
//        print("==========service===========");
//        print("level->" + Data.role.level);
//        print("exp->" + Data.role.exp);
//        print("coin->" + Data.role.coin);
//        print("stamina->" + Data.role.stamina);
//        print("=====================");
        if (lastLevel == 0)
        {
            lastLevel = Data.role.level;
        }
        if (GlobalTools.isBanshu)
        {
            levelText.GetComponentInParent<UISprite>().spriteName = "";
            levelText.text = string.Format(Manager.Language.GetString("IndexMain.levelText"), Data.role.level);
        }
        else
        {
            levelText.text = Data.role.level + "";
        }
        

        if (Data.role.level == 1)
        {
//            Invoke("showChatScreen", 10);
        }

        for (int i = lastLevel; i <= Data.role.level; i++)
        {
            indexPanel.openLevelMenu(i);
        }
        
        lastLevel = Data.role.level;

        if (!Data.collect.Init)
        {
            setting_info alert = (setting_info)Cache.SettingInfo["hero_collect_alert_level"];
            int alertLevel = Convert.ToInt32(alert.value);
            if (Data.role.level >= alertLevel)
            {
                Socket.Send(CMD.Collect_getinfo);
            }
        }
    }

    private void updateNameTxt(object value)
    {
        nameText.text = HeroConstants.getHeroName(Data.role.role_name, Data.role.quality);
    }

    private void updateVip(object value)
    {
        vipText.text = "VIP " + value + "";
        vipTxt.text = Data.role.vip + "";
    }

    private void showChatScreen()
    {
        if (!indexPanel.gameObject.activeSelf)
        {
            Invoke("showChatScreen", 5);
            return;
        }
        if (showMsgList != null && showMsgList.Count > 0)
        {
            ChatData.Msg msgInfo = (ChatData.Msg)showMsgList[0];
            showMsgList.RemoveAt(0);
            adventrue.addScreenTxt(msgInfo.msg);
            Invoke("showChatScreen", 5);
        }
//        adventrue.addScreenTxt(Manager.Language.GetString("Chat.says." + chatCount));
//        chatCount++;
//        if (chatCount < 10)
//        {
//            Invoke("showChatScreen", 8);
//        }
    }

    private void updateCoin(object value)
    {
        coinTxt.text = "" + StringUtil.formatCount((long) value);
    }

    private void updateGold(object value)
    {
        goldTxt.text = "" + StringUtil.formatCount((int)value);
    }

    private void updateTicket(object value)
    {
        ticketTxt.text = "" + StringUtil.formatCount((int)value);
    }

    public void TestVoid()
    {
//     SoundManager.PlayConnection("Battle");
    }

    public void SendTestMessage()
    {
        var player = new Player {name = "xx", age = 12};

        Socket.Send(CMD.Test_Echo, player);
//        Socket.Send("test", "combat");
    }

    public void SendTest2Message()
    {
        var info = new JsonData();
        info["id"] = 11307;
        info["type"] = 1;
        Socket.Send(CMD.Test_Combat, info);
    }

    public void showIndexPanel()
    {
        hideTopPanels();
        hideAllPanel(indexPanel.gameObject, true, true, true);
        expPanel.gameObject.SetActive(true);
        chatBtnPanel.SetActive(true);
        showHeroList();
    }

    public void hideAll()
    {
        hideTopPanels();
        foreach (GameObject go in Panels)
        {
            if (go != null)
            {
                var pan = go.GetComponent<UIPanel>();
                if (pan.gameObject.activeSelf)
                {
                    pan.gameObject.SetActive(false);
                }
            }
        }
        expPanel.gameObject.SetActive(false);
        TopPanel.gameObject.SetActive(false);
        resPanel.gameObject.SetActive(false);
        bottomPanel.gameObject.SetActive(false);
        chatBtnPanel.SetActive(false);
    }

    public void showBattlePanel()
    {
        inBattle = true;
        hideAllPanel(BattlePanel, false, false, false);
    }

    public void showEquipPanel()
    {
        hideAllPanel(EquipPanel, true, false, false);
    }

    public void showBagPanel()
    {
        hideAllPanel(BagPanel, true, false, false);
    }

    public void showNFTBagPanel()
    {
        hideAllPanel(NftBagPanel, true, false, false);
    }

    

    public void showCardPanel(hero_info info)
    {
        if (info == null)
        {
            return;
        }
        cardPanel.gameObject.GetComponent<CardPanel>().show(info);
    }

    public void showCardPanel(HeroInfo info)
    {
        if (info == null)
        {
            return;
        }
        cardPanel.gameObject.GetComponent<CardPanel>().show(info);
    }

    public void showMajorPanel()
    {
        if (_majorPanel == null)
        {
            _majorPanel = ToolKit.loadPrefab<HeroMajorPanel1>("panles/HeroMajorPanel1", parent.transform, "HeroMajorPanel1");
        }
        _majorPanel.gameObject.SetActive(true);
        hideAllPanel(_majorPanel.gameObject, false, true, false);
    }

    public void hideMajor()
    {
        Destroy(_majorPanel.gameObject);
        _majorPanel = null;
    }

    public void showFirstMoviePanel(int index)
    {
        if (_firstMoviePanel == null)
        {
            _firstMoviePanel = ToolKit.loadPrefab<FirstMoviePanel>("panles/FirstMoviePanel", parent.transform, "FirstMoviePanel");
        }
        _firstMoviePanel.gameObject.SetActive(true);
        _firstMoviePanel.show(index);
        hideAllPanel(_firstMoviePanel.gameObject, false, false, false);
    }

    public void hideFirstMovie() 
    {
        Destroy(_firstMoviePanel.gameObject);
        _firstMoviePanel = null;
        main.showIndexPanel();
    }

    public void showNewFirstMoviePanel()
    {
        if (_newFirstMoviePanel == null)
        {
            _newFirstMoviePanel = ToolKit.loadPrefab<NewFirstMoviePanel>("panles/NewFirstMoviePanel", parent.transform, "NewFirstMoviePanel");
        }
        hideAllPanel(_newFirstMoviePanel.gameObject, false, false, false);
        //_newFirstMoviePanel.show();
        _newFirstMoviePanel.step = 10; // 건너뛰고 //////바로 튜토리얼로
        _newFirstMoviePanel.showBG2toIndex();
    }

    public void hideNewFirstMovie()
    {
        Destroy(_newFirstMoviePanel.gameObject);
        _newFirstMoviePanel = null;
        showIndexPanel();
    }

    public void showOpenMoviePanel()
    {
        if (_openMoviePanel == null)
        {
            _openMoviePanel = ToolKit.loadPrefab<OpenMoviePanel>("panles/OpenMoviePanel", parent.transform, "OpenMoviePanel");
        }
        _openMoviePanel.gameObject.SetActive(true);
    }

    public void hideOpenMovie()
    {
        Destroy(_openMoviePanel.gameObject);
        _openMoviePanel = null;
        checkFirstGuide();
    }

    public void showMagicDetailPanel(JsonData info)
    {
        if (_magicDetail == null)
        {
            _magicDetail = ToolKit.loadPrefab<MagicDetailPanel>("panles/MagicDetailPanel", parent.transform, "MagicDetailPanel");
        }
        _magicDetail.gameObject.SetActive(true);
        _magicDetail.show(info);
    }

    public void showMagicDetailPanel(string info)
    {
        if (_magicDetail == null)
        {
            _magicDetail = ToolKit.loadPrefab<MagicDetailPanel>("panles/MagicDetailPanel", parent.transform, "MagicDetailPanel");
        }
        _magicDetail.gameObject.SetActive(true);
        _magicDetail.show(info);
    }
    public void showMagicDetailPanel(string info,int maginid)
    {
        if (_magicDetail == null)
        {
            _magicDetail = ToolKit.loadPrefab<MagicDetailPanel>("panles/MagicDetailPanel", parent.transform, "MagicDetailPanel");
        }
        _magicDetail.gameObject.SetActive(true);
        _magicDetail.show(info,maginid);
    }

    public void hideMagicDetail()
    {
        Destroy(_magicDetail.gameObject);
        _magicDetail = null;
    }

    public void showExchangePanel()
    {
        if (_exchangePanel == null)
        {
            _exchangePanel = ToolKit.loadPrefab<ExchangePanel>("panles/ExchangePanel", parent.transform, "ExchangePanel");
        }
        _exchangePanel.gameObject.SetActive(true);
    }

    public void hideExchangePanel()
    {
        Destroy(_exchangePanel.gameObject);
        _exchangePanel = null;
    }

    public void showEquipInfoPanel(ItemInfo info)
    {
        equipInfoPanel.show(info);
    }

    public void showHerosPanel(int count = 0)
    {
//        hideAllPanel(HerosPanel);
        if (herosPanel == null)
        {
            herosPanel = ToolKit.loadPrefab<HerosPanel>("panles/HerosPanel", parent.transform, "HerosPanel");
        }
        if (!Panels.Contains(herosPanel.gameObject))
        {
            Panels.Add(herosPanel.gameObject);
        }
        hideAllPanel(herosPanel.gameObject, false, true, false);
        herosPanel.show(count);
    }

    public void hideHerosPanel()
    {
        herosPanel.gameObject.SetActive(false);
        return;
        if (herosPanel != null)
        {
            Panels.Remove(herosPanel.gameObject);
            Destroy(herosPanel.gameObject);
            herosPanel = null;
        }
    }


    public void showCallPanel()
    {
        if (callPanel == null)
        {
            callPanel = ToolKit.loadPrefab<CallPanel>("panles/CallPanel", parent.transform, "CallPanel");
        }
        
        hideAllPanel(callPanel.gameObject, true, false, false);
        Panels.Add(callPanel.gameObject);
    }

    public void hideCallPanel()
    {
        callPanel.gameObject.SetActive(false);

//        return;
//        if (callPanel != null)
//        {
//            Panels.Remove(callPanel.gameObject);
//            Destroy(callPanel.gameObject);
//            callPanel = null;
//        }
    }

    public void showHeroListPanel()
    {
        hideAllPanel(heroListPanel.gameObject, true, false, false);
    }

    public void showTavernPanel()
    {
        hideTopPanels();
        if (_tavernPanel == null)
        {
            _tavernPanel = ToolKit.loadPrefab<TavernPanel>("panles/TavernPanel", parent.transform, "TavernPanel");
        }
        hideAllPanel(_tavernPanel.gameObject, true, false, false);
    }

    public void hideTavernPanel()
    {
        if (_tavernPanel != null)
        {
            Destroy(_tavernPanel.gameObject);
            _tavernPanel = null;
        }
        

    }

    public void showGiftAward1Panel(act_level_gift info, bool isShowNow, bool isShow1)
    {
        if (_giftAward1Panel == null)
        {
            _giftAward1Panel = ToolKit.loadPrefab<GiftAward1Panel>("panles/GiftAward1Panel", parent.transform, "GiftAward1Panel");
        }
        _giftAward1Panel.gameObject.SetActive(false);
        _giftAward1Panel.show(info.reward, isShowNow, isShow1);

        _giftAward1Panel.title1Txt.text = string.Format(Manager.Language.GetString("GiftAward1Panel.title1Txt"), info.level);
        if (info.id == 1)
        {
            _giftAward1Panel.title2Txt.text = Manager.Language.GetString("GiftAward1Panel.title2Txt.state1");
        }
        else 
        {
            _giftAward1Panel.title2Txt.text = string.Format(Manager.Language.GetString("GiftAward1Panel.title2Txt.state2"), info.name);
        }
    }

    public void hideGiftAward1()
    {
        if (_giftAward1Panel != null)
        {
            Destroy(_giftAward1Panel.gameObject);
        }
    }

    public void showEquipExchangePanel()
    {
        hideTopPanels();
        if (_equipExchangePanel == null)
        {
            _equipExchangePanel = ToolKit.loadPrefab<EquipExchangePanel>("panles/EquipExchangePanel", parent.transform, "EquipExchangePanel");
        }
        hideAllPanel(_equipExchangePanel.gameObject, true, false, false);
    }

    public void hideEquipExchangePanel()
    {
        if (_equipExchangePanel != null)
        {
            Destroy(_equipExchangePanel.gameObject);
            _equipExchangePanel = null;
        }
        
    }

    public void showFriendFightPanel()
    {
        if (_friendFightPanel == null)
        {
            _friendFightPanel = ToolKit.loadPrefab<FriendFightPanel>("panles/FriendFightPanel", parent.transform, "FriendFightPanel");
        }
        hideAllPanel(_friendFightPanel.gameObject, true, false, false);
    }

    public void hideFriendFightPanel()
    {
        if (_friendFightPanel != null)
        {
            Destroy(_friendFightPanel.gameObject);
            _friendFightPanel = null;
        }
    }

    public void showMysteryPanel(int id = 0)
    {
        if (_mysteryPanel == null)
        {
            _mysteryPanel = ToolKit.loadPrefab<MysteryPanel>("panles/MysteryPanel", parent.transform, "MysteryPanel");
        }
        hideAllPanel(_mysteryPanel.gameObject, false, false, false);
        _mysteryPanel.enter(id);
        chatBtnPanel.SetActive(true);
        chatBtnPanel.transform.localPosition = new Vector3(0, 23);
    }

    public void hideMystery()
    {
        if (_mysteryPanel)
        {
            _mysteryPanel.gameObject.SetActive(false);
//            lastPanel = _mysteryPanel.GetComponent<UIPanel>();
//            lastShowTop = false;
//            lastShowRes = false;
//            lastShowBot = false;
//            Destroy(_mysteryPanel.gameObject);
//            _mysteryPanel = null;
        }
    }

    public void showLeagueWar()
    {
//        Stopwatch watch = new Stopwatch();
//        watch.Start();
        if (_leagueWar == null)
        {
            _leagueWar = ToolKit.loadPrefab<LeagueWar>("panles/LeagueWar", parent.transform, "LeagueWar");
        }
//        if (!notShowAwardList.Contains(_leagueWar.battleWar.name))
//        {
//            notShowAwardList.Add(_leagueWar.battleWar.name);
//        }
        hideAllPanel(_leagueWar.gameObject, false, true, false);


        chatBtnPanel.SetActive(true);
        chatBtnPanel.transform.localPosition = new Vector3(0, -42);


//        watch.Stop();
//        print("watch time main->" + watch.ElapsedMilliseconds);
    }

    public void hideLeagueWar()
    {
        if (_leagueWar)
        {
            Destroy(_leagueWar.gameObject);
            _leagueWar = null;
        }
    }

    public void showArenaPanel()
    {
        if (_arenaPanel == null)
        {
            _arenaPanel = ToolKit.loadPrefab<ArenaPanel>("panles/ArenaPanel", parent.transform, "ArenaPanel");
            Panels.Add(_arenaPanel.gameObject);
        }
        hideAllPanel(_arenaPanel.gameObject, false, false, false);
//        chatBtnPanel.SetActive(true);
//        chatBtnPanel.transform.localPosition = new Vector3(0, -296);
    }

    public void hideArenaPanel()
    {
        Panels.Remove(_arenaPanel.gameObject);
        Destroy(_arenaPanel.gameObject);
        _arenaPanel = null;

    }

    public void showArena2Panel()
    {
        if (_arena2Panel == null)
        {
            _arena2Panel = ToolKit.loadPrefab<Arena2Panel>("panles/Arena2Panel", parent.transform, "Arena2Panel");
            Panels.Add(_arena2Panel.gameObject);
        }
        _arena2Panel.gameObject.SetActive(false);
        hideAllPanel(_arena2Panel.gameObject, false, false, false);
        chatBtnPanel.SetActive(true);
        chatBtnPanel.transform.localPosition = new Vector3(0, -296);
    }

    public void hideArena2Panel()
    {
        Panels.Remove(_arena2Panel.gameObject);
        Destroy(_arena2Panel.gameObject);
        _arena2Panel = null;

    }

    public void showTalkPanel(string str, string modelLeft, string model, Action callback = null)
    {
        if (_talkPanel == null)
        {
            _talkPanel = ToolKit.loadPrefab<TalkPanel>("panles/TalkPanel", parent.transform, "TalkPanel");
        }
        _talkPanel.show(str, modelLeft, model, callback);
    }
    public void hideTalkPanel()
    {
        Destroy(_talkPanel.gameObject);
        _talkPanel = null;
    }

    public void showMonsterPanel(main_event info)
    {
        if (_monsterPanel == null)
        {
            _monsterPanel = ToolKit.loadPrefab<MonsterAttackPanel>("panles/MonsterAttackPanel", parent.transform, "MonsterAttackPanel");
        }
        _monsterPanel.show(info);
    }
    public void hideMonsterPanel()
    {
        Destroy(_monsterPanel.gameObject);
        _monsterPanel = null;
    }

    public void showChestPanel(main_event info)
    {
        if (_chestPanel == null)
        {
            _chestPanel = ToolKit.loadPrefab<ChestPanel>("panles/ChestPanel", parent.transform, "ChestPanel");
        }
        _chestPanel.show(info);
    }

    public void hideChestPanel()
    {
        Destroy(_chestPanel.gameObject);
        _chestPanel = null;
    }

    public void showGivePanel(main_event info)
    {
        if (_givePanel == null)
        {
            _givePanel = ToolKit.loadPrefab<GivePanel>("panles/GivePanel", parent.transform, "GivePanel");
        }
        _givePanel.show(info);
    }

    public void hideGivePanel()
    {
        Destroy(_givePanel.gameObject);
        _givePanel = null;
    }

    public void showChangeMajorPanel(weapon_type_change info = null)
    {
        hideTopPanels();
        
        if (_changeMajorPanel == null)
        {
            _changeMajorPanel = ToolKit.loadPrefab<ChangeMajorPanel>("panles/ChangeMajorPanel", parent.transform, "ChangeMajorPanel");
        }
        hideAllPanel(_changeMajorPanel.gameObject, false, true, false);
        _changeMajorPanel.showMajor(info);
    }

    public void hideChangeMajorPanel()
    {
        if (_changeMajorPanel)
        {
            Destroy(_changeMajorPanel.gameObject);
            _changeMajorPanel = null;
        }
    }

    public void showRechargePanel()
    {
        hideTopPanels();

        if (_rechargePanel == null)
        {
            _rechargePanel = ToolKit.loadPrefab<RechargePanel>("panles/RechargePanel", parent.transform, "RechargePanel");
        }
        hideAllPanel(_rechargePanel.gameObject, false, true, false);
        
    }

    /// <summary>
    ///  지갑보기
    /// </summary>
    public void ShowWalletInfo()
    {
        if(string.IsNullOrEmpty(WalletManager.Instance.GetWalletPublicAddress()))
        {
            string[] str = { Manager.Language.GetString("BtnSetting"), Manager.Language.GetString("BtnCancel") };
            Dialog.show(Manager.Language.GetString("DialogDefaultTitle"), Manager.Language.GetString("NoLinkedWallet.defualtTxt1"), yes =>
            {
                if (yes)
                {
                    main.showSettingPanel();
                    Dialog.title.color = new Color(1, 0.83f, 0.32f);
                }
                else
                {
                    Dialog.title.color = new Color(1, 0.83f, 0.32f);
                }
            }, 0, str);
        }
        else
        {
            ParticleWalletGUI.NavigatorWallet();
        }
    }

    //public async void SwitchWallet()
    //{
    //    var walletType = WalletType.MetaMask;
    //    var publicAddress = WalletManager.Instance.GetUserInfo();
    //    var nativeResultData = await ParticleWalletGUI.Instance.SwitchWallet(walletType, publicAddress);

    //    UnityEngine.Debug.Log(nativeResultData.data);

    //    if (nativeResultData.isSuccess)
    //    {
    //        UnityEngine.Debug.Log(nativeResultData.data);
    //        ParticleWalletGUI.SetShowManageWallet(true);
    //        ParticleWalletGUI.NavigatorWallet(0);
    //    }
    //    else
    //    {
    //        var errorData = JsonConvert.DeserializeObject<NativeErrorData>(nativeResultData.data);
    //        UnityEngine.Debug.Log(errorData);
    //    }
    //}

    public void hideRechargePanel()
    {
        if (_rechargePanel)
        {
            Destroy(_rechargePanel.gameObject);
            _rechargePanel = null;
        }
    }

    public void showDailyPanel()
    {
        if (_dailyPanel == null)
        {
            _dailyPanel = ToolKit.loadPrefab<DailyPanel>("panles/DailyPanel", parent.transform, "DailyPanel");
        }
        hideAllPanel(_dailyPanel.gameObject, false, true, false);
        Panels.Add(_dailyPanel.gameObject);
    }

    public void hideDailyPanel()
    {
        if (_dailyPanel)
        {
            Panels.Remove(_dailyPanel.gameObject);
            Destroy(_dailyPanel.gameObject);
            _dailyPanel = null;
        }
    }

    public void showSevenDaysPanel()
    {
        if (_sevenDaysPanel == null)
        {
            _sevenDaysPanel = ToolKit.loadPrefab<SevenDaysPanel>("panles/SevenDaysPanel", parent.transform, "SevenDaysPanel");
        }
        hideAllPanel(_sevenDaysPanel.gameObject, false, true, false);
    }

    public void hideSevenDaysPanel()
    {
        if (_sevenDaysPanel)
        {
            Destroy(_sevenDaysPanel.gameObject);
            _sevenDaysPanel = null;
        }
    }

    public void showColorDiamondPanel() 
    {
        if (_colordiamondPanel == null)
        {
            _colordiamondPanel = ToolKit.loadPrefab<ColorDiamonWelfarePanel>("panles/ColorDiamonWelfarePanel", parent.transform, "ColorDiamonWelfarePanel");
        }
        hideAllPanel(_colordiamondPanel.gameObject, false, true, false);
    }

    public void hideColorDiamond() 
    {
        if (_colordiamondPanel)
        {
            Destroy(_colordiamondPanel.gameObject);
            _colordiamondPanel = null;
        }
    }

    public void showLimitwelfarePanel()
    {
        if (_limitwelfarePanel == null)
        {
            _limitwelfarePanel = ToolKit.loadPrefab<LimitWelfarePanel>("panles/LimitWelfarePanel", parent.transform, "LimitWelfarePanel");
        }
        _limitwelfarePanel.gameObject.SetActive(true);
    }

    public void hideLimitwelfare()
    {
        if (_limitwelfarePanel)
        {
            Destroy(_limitwelfarePanel.gameObject);
            _limitwelfarePanel = null;
        }
    }

    public void showFirstwelfarePanel() 
    {
        if (_firstwelfarePanel == null)
        {
            _firstwelfarePanel = ToolKit.loadPrefab<FirstWelfarePanel>("panles/FirstWelfarePanel", parent.transform, "FirstWelfarePanel");
        }
        _firstwelfarePanel.gameObject.SetActive(true);
    }

    public void hideFirstwelfare()
    {
        if (_firstwelfarePanel)
        {
            Destroy(_firstwelfarePanel.gameObject);
            _firstwelfarePanel = null;
        }
    }

    public void showRechargeReturnPanel()
    {
        if (_rechargeReturnPanel == null)
        {
            _rechargeReturnPanel = ToolKit.loadPrefab<RechargeReturnPanel>("panles/RechargeReturnPanel", parent.transform, "RechargeReturnPanel");
        }
        hideAllPanel(_rechargeReturnPanel.gameObject, false, true, false);
    }

    public void hideRechargeReturn()
    {
        if (_rechargeReturnPanel)
        {
            Destroy(_rechargeReturnPanel.gameObject);
            _rechargeReturnPanel = null;
        }
    }

    public void showAdventureGuidePanel()
    {
        if (_adventureGuidePanel == null)
        {
            _adventureGuidePanel = ToolKit.loadPrefab<AdventureGuidePanel>("panles/AdventureGuidePanel", parent.transform, "AdventureGuidePanel");
        }
//        main.showLastPanel();
        _adventureGuidePanel.gameObject.SetActive(true);
    }

    public void hideAdventureGuidePanel() 
    {
        if (_adventureGuidePanel)
        {
            Destroy(_adventureGuidePanel.gameObject);
            _adventureGuidePanel = null;
        }
    }

    public void showAllStrategyPanel() 
    {
        if (_allStrategyPanel == null)
        {
            _allStrategyPanel = ToolKit.loadPrefab<AllStrategyPanel>("panles/AllStrategyPanel", parent.transform, "AllStrategyPanel");
        }
        _allStrategyPanel.gameObject.SetActive(true);
    }

    public void hideAllStrategyPanel() 
    {
        if (_allStrategyPanel)
        {
            Destroy(_allStrategyPanel.gameObject);
            _allStrategyPanel = null;
        }
    }

    public void showVipWarningPanel(int vip) 
    {
        if (_vipWarningPanel == null)
        {
            _vipWarningPanel = ToolKit.loadPrefab<VipWarningPanel>("panles/VipWarningPanel", parent.transform, "VipWarningPanel");
        }
        _vipWarningPanel.gameObject.SetActive(true);
        _vipWarningPanel.show(vip);
    }

    public void hideVipWarningPanel() 
    {
        if (_vipWarningPanel)
        {
            Destroy(_vipWarningPanel.gameObject);
            _vipWarningPanel = null;
        }
    }

    public void showQuickRisePanel() 
    {
        if (_quickRisePanel == null)
        {
            _quickRisePanel = ToolKit.loadPrefab<QuickRisePanel>("panles/QuickRisePanel", parent.transform, "QuickRisePanel");
        }
        _quickRisePanel.gameObject.SetActive(true);
    }

    public void hideQuickRisePanel()
    {
        if (_quickRisePanel)
        {
            Destroy(_quickRisePanel.gameObject);
            _quickRisePanel = null;
        }
    }

    public void showMagicGirlPanel() 
    {
        if (_magicGirlPanel == null)
        {
            _magicGirlPanel = ToolKit.loadPrefab<MagicGirlPanel>("panles/MagicGirlPanel", parent.transform, "MagicGirlPanel");
        }
        hideAllPanel(_magicGirlPanel.gameObject, false, true, false);
        Panels.Add(_magicGirlPanel.gameObject);
    }

    public void hideMagicGirlPanel() 
    {
        if (_magicGirlPanel)
        {
            Panels.Remove(_magicGirlPanel.gameObject);
            Destroy(_magicGirlPanel.gameObject);
            _magicGirlPanel = null;
        }
    }

    public void showActForeverPanel() 
    {
        if (_actForeverPanel == null)
        {
            _actForeverPanel = ToolKit.loadPrefab<ActForeverPanel>("panles/ActForeverPanel", parent.transform, "ActForeverPanel");
        }
        hideAllPanel(_actForeverPanel.gameObject, false, true, false);
    }

    public void hideActForeverPanel() 
    {
        if (_actForeverPanel)
        {
            Destroy(_actForeverPanel.gameObject);
            _actForeverPanel = null;
        }
    }

    public void showActGoldheroPanel() 
    {
        if (_actGoldheroPanel == null)
        {
            _actGoldheroPanel = ToolKit.loadPrefab<ActGoldheroPanel>("panles/ActGoldheroPanel", parent.transform, "ActGoldheroPanel");
        }
        hideAllPanel(_actGoldheroPanel.gameObject, false, true, false);
        Panels.Add(_actGoldheroPanel.gameObject);
    }

    public void hideActGoldheroPanel() 
    {
        if (_actGoldheroPanel)
        {
            Panels.Remove(_actGoldheroPanel.gameObject);
            Destroy(_actGoldheroPanel.gameObject);
            _actGoldheroPanel = null;
        }
    }

    public void showActRedpacketPanel()
    {
        if (_actRedpacketPanel == null)
        {
            _actRedpacketPanel = ToolKit.loadPrefab<ActRedpacketPanel>("panles/ActRedpacketPanel", parent.transform, "ActRedpacketPanel");
        }
        _actRedpacketPanel.gameObject.SetActive(true);
    }

    public void hideActRedpacketPanel()
    {
        if (_actRedpacketPanel)
        {
            Destroy(_actRedpacketPanel.gameObject);
            _actRedpacketPanel = null;
        }
    }

    public void showActFourPanel(int type) 
    {
        if (_actFourPanel == null)
        {
            _actFourPanel = ToolKit.loadPrefab<ActFourPanel>("panles/ActFourPanel", parent.transform, "ActFourPanel");
        }
        _actFourPanel._type = type;
        hideAllPanel(_actFourPanel.gameObject, false, true, false);
        Panels.Add(_actFourPanel.gameObject);
        _actFourPanel.show(type);
    }

    public void hideActFourPanel() 
    {
        if (_actFourPanel)
        {
            Panels.Remove(_actFourPanel.gameObject);
            Destroy(_actFourPanel.gameObject);
            _actFourPanel = null;
        }
    }

    public void showActFestivalPanel() 
    {
        if (_actFestivalPanel == null)
        {
            _actFestivalPanel = ToolKit.loadPrefab<ActFestivalPanel>("panles/ActFestivalPanel", parent.transform, "ActFestivalPanel");
        }
        hideAllPanel(_actFestivalPanel.gameObject, false, true, false);
    }

    public void hideActFestivalPanel()
    {
        if (_actFestivalPanel)
        {
            Destroy(_actFestivalPanel.gameObject);
            _actFestivalPanel = null;
        }
    }

    public void showChatPanel()
    {
        chatPanel.gameObject.SetActive(true);
    }

    public void showBBSPanel(hero_info info)
    {
        bbsPanel.show(info);
    }

    public void showAwardPanel(int type)
    {
        awardPanel.gameObject.SetActive(true);
        awardPanel.show(1);
    }

    public void showEmailPanel()
    {
        hideTopPanels();
//        emailPanel.gameObject.SetActive(true);
        hideAllPanel(emailPanel.gameObject, true, false, false);
    }

    public void showMallPanel()
    {
//        hideAllPanel(mallPanel.gameObject, true, false, false);
    }

    public void showFirendPanel()
    {
        hideTopPanels();
        hideAllPanel(firendPanel.gameObject, false, false,false);
    }

    public void showQuestAndHeroPanel()
    {
        hideAllPanel(questAndHeroPanel.gameObject, true, false, false);
        questAndHeroPanel.show();
    }

    public void showMagicAndProfilePanel()
    {
        hideTopPanels();
        
        if (_magicAndProfilePanel == null)
        {
            _magicAndProfilePanel = ToolKit.loadPrefab<MagicAndProfilePanel>("panles/MagicAndProfilePanel", parent.transform, "MagicAndProfilePanel");
        }
        hideAllPanel(_magicAndProfilePanel.gameObject, true, false, false);
    }

    public void hideMagicAndProfile()
    {
        if (_magicAndProfilePanel != null)
        {
            Destroy(_magicAndProfilePanel.gameObject);
            _magicAndProfilePanel = null;
        }
        
    }
    
    public void showSettingPanel()
    {
        if (_settingPanel == null)
        {
            _settingPanel = ToolKit.loadPrefab<SettingPanel>("panles/SettingPanel", parent.transform, "SettingPanel");
        }
        _settingPanel.gameObject.SetActive(true);
    }

    public void hideSettingPanel()
    {
        Destroy(_settingPanel.gameObject);
        _settingPanel = null;
    }

    public void showKnightPanel()
    {
        if (_knightPanel == null)
        {
            _knightPanel = ToolKit.loadPrefab<KnightPanel>("panles/KnightPanel", parent.transform, "KnightPanel");
        }
        hideAllPanel(_knightPanel.gameObject, false, true, false);
        _knightPanel.gameObject.SetActive(true);
    }

    public void hideKnightPanel()
    {
        Destroy(_knightPanel.gameObject);
        _knightPanel = null;
    }


    public void showKnightApplyPanel()
    {
        if (_knightApplyPanel == null)
        {
            _knightApplyPanel = ToolKit.loadPrefab<KnightApplyPanel>("panles/KnightApplyPanel", parent.transform, "KnightApplyPanel");
        }
        hideAllPanel(_knightApplyPanel.gameObject, false, true, false);
        _knightApplyPanel.gameObject.SetActive(true);
    }

    public void hideKnightApplyPanel()
    {
        Destroy(_knightApplyPanel.gameObject);
        _knightApplyPanel = null;
    }

    public void showKnightDuelPanel()
    {
        if (_knightDuelPanel == null)
        {
            _knightDuelPanel = ToolKit.loadPrefab<KnightDuelPanel>("panles/KnightDuelPanel", parent.transform, "KnightDuelPanel");
        }
        hideAllPanel(_knightDuelPanel.gameObject, false, true, false);
        _knightDuelPanel.gameObject.SetActive(true);
    }

    public void hideKnightDuelPanel()
    {
        Destroy(_knightDuelPanel.gameObject);
        _knightDuelPanel = null;
    }

    public void showLandWarPanel()
    {
        if (_landWarPanel == null)
        {
            _landWarPanel = ToolKit.loadPrefab<LandWarPanel>("panles/LandWarPanel", parent.transform, "LandWarPanel");
        }
        hideAllPanel(_landWarPanel.gameObject, false, true, false);
        _landWarPanel.gameObject.SetActive(true);
        chatBtnPanel.SetActive(true);
        chatBtnPanel.transform.localPosition = new Vector3(0, -428);
        pushPanel.gameObject.SetActive(false);
    }

    public void hideLandWarPanel()
    {
        Destroy(_landWarPanel.gameObject);
        _landWarPanel = null;
    }

    public void showLandWarFirstPanel()
    {
        if (_landWarFirstPanel == null)
        {
            _landWarFirstPanel = ToolKit.loadPrefab<LandWarFirstPanel>("panles/LandWarFirstPanel", parent.transform, "LandWarFirstPanel");
        }
        hideAllPanel(_landWarFirstPanel.gameObject, false, true, false);
        _landWarFirstPanel.gameObject.SetActive(true);
    }

    public void hideLandWarFirstPanel()
    {
        Destroy(_landWarFirstPanel.gameObject);
        _landWarFirstPanel = null;
    }

    public void showLandListPanel(bool send = false)
    {
        if (_landWarListPanel == null)
        {
            _landWarListPanel = ToolKit.loadPrefab<LandWarList>("panles/LandWarListPanel", parent.transform, "LandWarListPanel");
        }
        hideAllPanel(_landWarListPanel.gameObject, false, true, false);
        _landWarListPanel.show(send);
        
        chatBtnPanel.SetActive(true);
        chatBtnPanel.transform.localPosition = new Vector3(0, -428);

    }

    public void hideLandWarListPanel()
    {
        Destroy(_landWarListPanel.gameObject);
        _landWarListPanel = null;
    }

    public void showLandEndPanel()
    {
        if (_landWarEndPanel == null)
        {
            _landWarEndPanel = ToolKit.loadPrefab<LandWarEndPanel>("panles/LandWarEndPanel", parent.transform, "LandWarEndPanel");
        }
        _landWarEndPanel.show(Data.landWar.reportInfo);
    }

    public void hideLandWarEndPanel()
    {
        Destroy(_landWarEndPanel.gameObject);
        _landWarEndPanel = null;
    }

    public void showLuckySharePanel(ActivityData.ShareInfo value)
    {
      
    }

    public void hideLuckySharePanel()
    {
    
    }

    public void showMergyGuidePanel(string value)
    {
        if (_getResPanel == null)
        {
            _getResPanel = ToolKit.loadPrefab<MergyGuidePanel>("panles/MergyGuidePanel", parent.transform, "MergyGuidePanel");
        }
        _getResPanel.show(value);
    }

    public void hideMergyGuidePanel()
    {
        if (_getResPanel != null)
        {
            Destroy(_getResPanel.gameObject);
            _getResPanel = null;
        }
    }

    
    public void showActSpecialPanel()
    {
        if (_actSpecialPanel == null)
        {
            _actSpecialPanel = ToolKit.loadPrefab<ActSpecialPanel>("panles/ActSpecialPanel", parent.transform, "ActSpecialPanel");
        }
        _actSpecialPanel.gameObject.SetActive(true);
    }

    public void hideActSpecialPanel()
    {
        Destroy(_actSpecialPanel.gameObject);
        _actSpecialPanel = null;
    }

    public void showWorldWarPanel()
    {
        if (_worldWarPanel == null)
        {
            _worldWarPanel = ToolKit.loadPrefab<WorldWarPanel>("panles/WorldWarPanel", parent.transform, "WorldWarPanel");
        }
        hideAllPanel(_worldWarPanel.gameObject, false, true, false);
        _worldWarPanel.upPanel.gameObject.SetActive(true);
        //_worldWarPanel.show();
    }

    public void hideWorldWarPanel()
    {
        Destroy(_worldWarPanel.gameObject);
        _worldWarPanel = null;
    }

    public void showWorldWarPos(int tx, int ty)
    {
        if (_worldWarPanel == null)
        {
            showWorldWarPanel();
        }
        _worldWarPanel.map.showPos(tx, ty);
    }

    public void skip2Wow3SciencePanel(int toggleValue, int id) 
    {
        if (_wow3MainPanel == null)
        {
            _wow3MainPanel = ToolKit.loadPrefab<Wow3MainPanel>("panles/Wow3MainPanel", parent.transform, "Wow3MainPanel");
        }
        _wow3MainPanel.sciencePanel.skipId = id;
        hideAllPanel(_wow3MainPanel.gameObject, false, true, false);
        _wow3MainPanel.show();

        List<UIToggle> tabList = new List<UIToggle>{
            _wow3MainPanel.sciencePanel.tabBtn1,
            _wow3MainPanel.sciencePanel.tabBtn2,
            _wow3MainPanel.sciencePanel.tabBtn3};
        tabList[toggleValue - 1].value = true;
    }

    public void showWow3MainPanel()
    {
        if (_wow3MainPanel == null)
        {
            _wow3MainPanel = ToolKit.loadPrefab<Wow3MainPanel>("panles/Wow3MainPanel", parent.transform, "Wow3MainPanel");
        }
        hideAllPanel(_wow3MainPanel.gameObject, false, true, false);
        _wow3MainPanel.show();
    }

    public void hideWow3MainPanel()
    {
        Destroy(_wow3MainPanel.gameObject);
        _wow3MainPanel = null;
    }

    public void showWow3ArmyPanel() 
    {
        if (_wow3MainPanel == null)
        {
            _wow3MainPanel = ToolKit.loadPrefab<Wow3MainPanel>("panles/Wow3MainPanel", parent.transform, "Wow3MainPanel");
        }
        _wow3MainPanel.showArmy = true;
        hideAllPanel(_wow3MainPanel.gameObject, false, true, false);
        _wow3MainPanel.show();
    }

    public void showWow3EmailPanel(int msg1, int msg2, int msg3)
    {
        if (_wow3EmailPanel == null)
        {
            _wow3EmailPanel = ToolKit.loadPrefab<Wow3EmailPanel>("panles/Wow3EmailPanel", _worldWarPanel.transform, "Wow3EmailPanel");
        }
        hideAllPanel(_wow3EmailPanel.gameObject, false, true, false);
        _wow3EmailPanel.show(msg1, msg2 ,msg3);
    }
    public void show2Wow3EmailPanel()
    {
        if (_wow3EmailPanel == null)
        {
            _wow3EmailPanel = ToolKit.loadPrefab<Wow3EmailPanel>("panles/Wow3EmailPanel", _worldWarPanel.transform, "Wow3EmailPanel");
        }
        hideAllPanel(_wow3EmailPanel.gameObject, false, true, false);
        _wow3EmailPanel.onshow();
    }
    public void hideWow3EmailPanel()
    {
        Destroy(_wow3EmailPanel.gameObject);
        _wow3EmailPanel = null;
    }
    public void showWow3InformationPanel(string uid, int _type)
    {
        if (_Wow3InformationPanel == null)
        {
            _Wow3InformationPanel = ToolKit.loadPrefab<Wow3InformationPanel>("panles/Wow3InformationPanel", _worldWarPanel.transform, "Wow3InformationPanel");
        }
        hideAllPanel(_Wow3InformationPanel.gameObject, false, true, false);
        _Wow3InformationPanel.show(uid,_type);
    }
    public void hideWow3InformationPanel()
    {
        Destroy(_Wow3InformationPanel.gameObject);
        _Wow3InformationPanel = null;
    }
    public void showWow3EmailWritePanel(string name, string uid)
    {
        gameObject.SetActive(false);
        if (_Wow3EmailWritePanel == null)
        {
            _Wow3EmailWritePanel = ToolKit.loadPrefab<Wow3EmailWritePanel>("panles/Wow3EmailWritePanel", _worldWarPanel.transform, "Wow3EmailWritePanel");
        }
        hideAllPanel(_Wow3EmailWritePanel.gameObject, false, true, false);
        _Wow3EmailWritePanel.showServerInfo(name, uid, 1);
    }
    public void hideWow3EmailWritePanel()
    {
        Destroy(_Wow3EmailWritePanel.gameObject);
        _Wow3EmailWritePanel = null;
    }
    public void showWow3FirendPanel()
    {
        if (_Wow3FirendPanel == null)
        {
            _Wow3FirendPanel = ToolKit.loadPrefab<Wow3FirendPanel>("panles/Wow3FirendPanel", _worldWarPanel.transform, "Wow3FirendPanel");
        }
        hideAllPanel(_Wow3FirendPanel.gameObject, false, true, false);
        _Wow3FirendPanel.show();
    }
    public void hideWow3FirendPanel()
    {
        Destroy(_Wow3FirendPanel.gameObject);
        _Wow3FirendPanel = null;
    }

    public void showWow3BattlePanel(WorldWarData.TileInfo tileInfo,wow_personal_building_info resInfo, int type)
    {
        if (_wow3battlePanel == null)
        {
            _wow3battlePanel = ToolKit.loadPrefab<Wow3BattlePanel>("panles/Wow3BattlePanel", _worldWarPanel.transform, "Wow3BattlePanel");
        }
        _wow3battlePanel.type = type;
        _wow3battlePanel.tileInfo = tileInfo;
        _wow3battlePanel.resInfo = resInfo;
        hideAllPanel(_wow3battlePanel.gameObject, false, true, false);
    }

    public void hideWow3BattlePanel()
    {
        Destroy(_wow3battlePanel.gameObject);
        _wow3battlePanel = null;
    }

    public void showWow3FortPanel(Wow3FortressPanel fortresspanel)
    {
        if (_wow3fortPanel == null)
        {
            _wow3fortPanel = ToolKit.loadPrefab<Wow3FortPanel>("panles/Wow3FortPanel", _worldWarPanel.transform, "Wow3FortPanel");
        }
        _wow3fortPanel.map = _worldWarPanel.map;
        hideAllPanel(_wow3fortPanel.gameObject, false, true, false);
        _wow3fortPanel.showMovie(fortresspanel);
    }

    public void hideWow3FortPanel()
    {
        Destroy(_wow3fortPanel.gameObject);
        _wow3fortPanel = null;
    }

    public void showWow3ReportPanel() 
    {
        if (_wow3reportPanel == null)
        {
            _wow3reportPanel = ToolKit.loadPrefab<Wow3ReportPanel>("panles/Wow3ReportPanel", _worldWarPanel.transform, "Wow3ReportPanel");
        }
        hideAllPanel(_wow3reportPanel.gameObject, false, true, false);
    }

    public void hideWow3ReportPanel() 
    {
        Destroy(_wow3reportPanel.gameObject);
        _wow3reportPanel = null;
    }

    public void showWow3UnionPanel(string id)
    {
        if (_wow3unionPanel == null)
        {
            _wow3unionPanel = ToolKit.loadPrefab<Wow3UnionPanel>("panles/Wow3UnionPanel", _worldWarPanel.transform, "Wow3UnionPanel");
        }
        if (id != "")
        {
            _wow3unionPanel.startshow(id);
        }
        else
        {
            _wow3unionPanel.startshow2();
        }
    }

    public void hideWow3UnionPanel()
    {
        Destroy(_wow3unionPanel.gameObject);
        _wow3unionPanel = null;
    }

    public void showWow3UnionApplyPanel() 
    {
        if (_wow3unionApplyPanel == null)
        {
            _wow3unionApplyPanel = ToolKit.loadPrefab<Wow3UnionApplyPanel>("panles/Wow3UnionApplyPanel", _worldWarPanel.transform, "Wow3UnionApplyPanel");
        }
        _wow3unionApplyPanel.worldWarPanel = _worldWarPanel;
        hideAllPanel(_wow3unionApplyPanel.gameObject, false, true, false);        
    }

    public void hideWow3UnionApplyPanel() 
    {
        Destroy(_wow3unionApplyPanel.gameObject);
        _wow3unionApplyPanel = null;
    }

    public void showWow3TargetPanel(WorldWarPanel panel)
    {
        if (_wow3TargetPanel==null)
        {
            _wow3TargetPanel = ToolKit.loadPrefab<Wow3TargetPanel>("panles/Wow3TargetPanel", _worldWarPanel.transform, "Wow3TargetPanel");
        }
        hideAllPanel(_wow3TargetPanel.gameObject, false, true, false);
        _wow3TargetPanel.show(panel);
    }

    public void hideWow3TargetPanel()
    {
        _wow3TargetPanel.gameObject.SetActive(false);
    }

    public void showRunePanel(int id=0)
    {
        if (runePanel == null)
        {
            runePanel = ToolKit.loadPrefab<RunePanel>("panles/RunePanel", parent.transform, "RunePanel");
        }
        hideAllPanel(runePanel.gameObject, false, true, false);
        _worldWarPanel.upPanel.gameObject.SetActive(false);
        runePanel.show(_worldWarPanel,id);
    }

    public void hideRunePanel()
    {
        runePanel.gameObject.SetActive(false);
    }

    public void showWorkShopPanel()
    {
        if (_workshopPanel == null)
        {
            _workshopPanel = ToolKit.loadPrefab<Wow3WorkshopPanel>("panles/Wow3WorkshopPanel", _worldWarPanel.transform, "Wow3WorkshopPanel");
        }
        hideAllPanel(_workshopPanel.gameObject, false, true, false);
        _worldWarPanel.upPanel.gameObject.SetActive(false);
        _workshopPanel.show();
    }

    public void hideWorkShopPanel()
    {
        _workshopPanel.gameObject.SetActive(false);
    }

    public void showWow3ChatPanel(Wow3MainUpPanel uppanel = null,string showStr = "")
    {
        if (_wow3ChatPanel == null)
        {
            _wow3ChatPanel = ToolKit.loadPrefab<Wow3ChatPanel>("panles/Wow3ChatPanel", _worldWarPanel.transform,
                        "Wow3ChatPanel");
        }

        _wow3ChatPanel.show(uppanel);
        _wow3ChatPanel.inputTxt.value = showStr;
    }

    public void showWow3RolePanel()
    {
        if (_wow3RolePanel == null)
        {
            _wow3RolePanel = ToolKit.loadPrefab<Wow3RolePanel>("panles/Wow3RolePanel", _worldWarPanel.transform, "Wow3RolePanel");
        }
        hideAllPanel(_wow3RolePanel.gameObject, false, true, false);
        _wow3RolePanel.show();
    }
    public void showWow3RankingPanel()
    {
        if (_wow3RankingPanel == null)
        {
            _wow3RankingPanel = ToolKit.loadPrefab<Wow3RankingPanel>("panles/Wow3RankingPanel", _worldWarPanel.transform, "Wow3RankingPanel");
        }
        hideAllPanel(_wow3RankingPanel.gameObject, false, true, false);
        _wow3RankingPanel.show();
    }
    public void hideWow3RankingPanel()
    {
        Destroy(_wow3RankingPanel.gameObject);
        _wow3RankingPanel = null;
    }
    public void hideWow3RolePanel()
    {
        Destroy(_workshopPanel.gameObject);
        _wow3RolePanel = null;
    }


    public void showKnightWarPanel() 
    {
        if (_knightWarPanel == null)
        {
            _knightWarPanel = ToolKit.loadPrefab<KnightWarPanel>("panles/KnightWarPanel", parent.transform, "KnightWarPanel");
        }
        hideAllPanel(_knightWarPanel.gameObject, false, true, false);
    }

    public void hideKnightWarPanel() 
    {
        Destroy(_knightWarPanel.gameObject);
        _knightWarPanel = null;
    }

    public void showActGoldPanel() 
    {
        if (_actGoldPanel == null)
        {
            _actGoldPanel = ToolKit.loadPrefab<ActGoldPanel>("panles/ActGoldPanel", parent.transform, "ActGoldPanel");
        }
        hideAllPanel(_actGoldPanel.gameObject, false, true, false);
    }
    public void hideActGoldPanel()
    {
        Destroy(_actGoldPanel.gameObject);
        _actGoldPanel = null;
    }

    public void showActHeroLimitPanel() 
    {
        if (_actHeroLimitPanel == null)
        {
            _actHeroLimitPanel = ToolKit.loadPrefab<ActHeroLimitPanel>("panles/ActHeroLimitPanel", parent.transform, "ActHeroLimitPanel");
        }
        hideAllPanel(_actHeroLimitPanel.gameObject, false, true, false);
    }

    public void hideActHeroLimitPanel() 
    {
        if (_actHeroLimitPanel)
        {
            Destroy(_actHeroLimitPanel.gameObject);
            _actHeroLimitPanel = null;
        }
    }

    public void showLuckCardPanel() 
    {
        if (_luckCardPanel == null)
        {
            _luckCardPanel = ToolKit.loadPrefab<LuckCardPanel>("panles/LuckCardPanel", parent.transform, "LuckCardPanel");
        }
        hideAllPanel(_luckCardPanel.gameObject, false, true, false);
    }

    public void hideLuckCardPanel() 
    {
        if (_luckCardPanel)
        {
            Destroy(_luckCardPanel.gameObject);
            _luckCardPanel = null;
        }
    }

    public void showCardCollectPanel()
    {
        if (_cardCollectPanel == null)
        {
            _cardCollectPanel = ToolKit.loadPrefab<CardCollectPanel>("panles/CardCollectPanel", parent.transform, "CardCollectPanel");
        }
        hideAllPanel(_cardCollectPanel.gameObject, false, true, false);
    }

    public void hideCardCollectPanel() 
    {
        if (_cardCollectPanel)
        {
            Destroy(_cardCollectPanel.gameObject);
            _cardCollectPanel = null;
        }
    }

    public void showUpdateWelfarePanel() 
    {
        if (_updateWelfarePanel == null)
        {
            _updateWelfarePanel = ToolKit.loadPrefab<UpdateWelfarePanel>("panles/UpdateWelfarePanel", parent.transform, "UpdateWelfarePanel");
        }
        _updateWelfarePanel.gameObject.SetActive(true);
    }

    public void hideUpdateWelfarePanel() 
    {
        if (_updateWelfarePanel)
        {
            Destroy(_updateWelfarePanel.gameObject);
            _updateWelfarePanel = null;
        }
    }

    public void showMergePanel1(int count = 0) 
    {
        if (_mergePanel1 == null)
        {
            _mergePanel1 = ToolKit.loadPrefab<MergyPanel>("panles/MergePanel1", parent.transform, "MergePanel1");
        }
        hideAllPanel(_mergePanel1.gameObject, false, true, false);
        Panels.Add(_mergePanel1.gameObject);
        _mergePanel1.show(count);
        _mergePanel1.gameObject.SetActive(true);
    }

    public void hideMergePanel1() 
    {
        if (_mergePanel1)
        {
            Panels.Remove(_mergePanel1.gameObject);
            Destroy(_mergePanel1.gameObject);
            _mergePanel1 = null;
        }
    }

    public void showMazePanel()
    {
        hideTopPanels();
        if (_mazePanel == null)
        {
            _mazePanel = ToolKit.loadPrefab<MazePanel>("panles/MazePanel", parent.transform, "MazePanel");
        }
        Panels.Add(_mazePanel.gameObject);
        hideAllPanel(_mazePanel.gameObject, true, false, false);
        expPanel.gameObject.SetActive(true);
        chatBtnPanel.SetActive(true);
        chatBtnPanel.transform.localPosition = new Vector3(0, -460);
    }

    public void hideMazePanel()
    {
        if (_mazePanel)
        {
            Panels.Remove(_mazePanel.gameObject);
            _mazePanel.gameObject.SetActive(false);
            Destroy(_mazePanel.gameObject);
            _mazePanel = null;
        }
    }

    public void showQuestionnairePanel()
    {
        if (_questionnairePanel == null)
        {
            _questionnairePanel = ToolKit.loadPrefab<QuestionnairePanel>("panles/QuestionnairePanel", parent.transform, "QuestionnairePanel");
        }
        _questionnairePanel.gameObject.SetActive(true);
    }
    public void hideQuestionnairePanel()
    {
        if (_questionnairePanel)
        {
            Destroy(_questionnairePanel.gameObject);
            _questionnairePanel = null;
        }
    }
    public void showWow3SiegeAnimationPanel(int _siege)
    {
        if (_wow3SiegeAnimationPanel == null)
        {
            _wow3SiegeAnimationPanel = ToolKit.loadPrefab<Wow3SiegeAnimationPanel>("panles/Wow3SiegeAnimationPanel", _worldWarPanel.transform, "Wow3SiegeAnimationPanel");
        }
        _wow3SiegeAnimationPanel.show(_siege);
    }
    private void hideTopPanels()
    {
//        hideMagicAndProfile();
//        hideChangeMajorPanel();
////        hideMazePanel();
//        hideTavernPanel();
//        hideEquipExchangePanel();
//        hideFriendFightPanel();

        #region
        if (_magicAndProfilePanel != null)
        {
            hideMagicAndProfile();
        }
        else if (_friendFightPanel != null)
        {
            hideFriendFightPanel();
        }
        else if (_tavernPanel != null)
        {
            hideTavernPanel();
        }
        else if (_changeMajorPanel != null)
        {
            hideChangeMajorPanel();
        }
        else if (_equipExchangePanel != null)
        {
            hideEquipExchangePanel();
        }
        else if (_leagueWar != null)
        {
            hideLeagueWar();
        }
        else if (_majorPanel != null)
        {
            hideMajor();
        }
        else if (_mazePanel != null)
        {
            hideMazePanel();
        }
        else if (_exchangePanel != null)
        {
            hideExchangePanel();
        }
//        else if (callPanel != null)
//        {
//            Destroy(callPanel.gameObject);
//            callPanel = null;
//        }
        else if (_dailyPanel != null)
        {
            hideDailyPanel();
        }
        else if (_knightDuelPanel != null)
        {
            hideKnightDuelPanel();
        }
        else if (_landWarPanel != null)
        {
            hideLandWarPanel();
        }
        else if (_landWarFirstPanel != null)
        {
            hideLandWarFirstPanel();
        }
        else if (_landWarListPanel != null)
        {
            hideLandWarListPanel();
        }
        else if (_mergePanel1 != null)
        {
            hideMergePanel1();
        }
        else if (_knightPanel != null)
        {
            hideKnightPanel();
        }
        else if (_knightApplyPanel != null)
        {
            hideKnightApplyPanel();
        }
        else if (_sevenDaysPanel != null)
        {
            hideSevenDaysPanel();
        }
        else if (_colordiamondPanel != null)
        {
            hideColorDiamond();
        }
        else if (_rechargeReturnPanel != null)
        {
            hideRechargeReturn();
        }
        else if (_magicGirlPanel != null)
        {
            hideMagicGirlPanel();
        }
        else if (_actForeverPanel != null)
        {
            hideActForeverPanel();
        }
        else if (_actGoldheroPanel != null)
        {
            hideActGoldheroPanel();
        }
        else if (_actFourPanel != null)
        {
            hideActFourPanel();
        }
        else if (_actFestivalPanel != null)
        {
            hideActFestivalPanel();
        }
        else if (_luckCardPanel != null)
        {
            hideLuckCardPanel();
        }
        else if (_cardCollectPanel != null)
        {
            hideCardCollectPanel();
        }
        else if (_worldWarPanel != null)
        {
            if (_wow3MainPanel != null)
            {
                hideWow3MainPanel();
            }
            else if (runePanel != null)
            {
                hideRunePanel();
            }
            hideWorldWarPanel();
        }
        else if (_knightWarPanel != null)
        {
            hideKnightWarPanel();
        }
        else if (_actHeroLimitPanel != null)
        {
            hideActHeroLimitPanel();
        }
        else if (_actGoldPanel != null)
        {
            hideActGoldPanel();
        }

        #endregion
    }

    public void showLeaguePanel(int type = 0)
    {
        
        hideAllPanel(leaguePanel.gameObject, true, false, false);
        leaguePanel.show(type);
    }

    public void showLeagueSelectPanel()
    {
        hideAllPanel(leagueSelectPanel.gameObject, true, false, false);
    }

    

    public void showActivityPanel(bool showShop = false)
    {
        hideAllPanel(activityPanel.gameObject, false, true, true);
        activityPanel.show(showShop);
    }

    public void showCollectPanel()
    {
        hideAllPanel(collectPanel.gameObject, true, false, false);
    }

    public void showCollectRewardPanel(int chapter)
    {
        hideAllPanel(collectRewardPanel.gameObject, false, false, false);
        collectRewardPanel.show(chapter);
    }

    

    public void showPlayerInfoPanel(string uid)
    {
        playerInfoPanel.show(uid);
    }

    public void showAdventure()
    {
        adventrue.gameObject.SetActive(true);
        adventrue.show();
        showHeroList();
//        cityPanel.gameObject.SetActive(false);
    }

//    public void showCityPanel()
//    {
//        cityPanel.gameObject.SetActive(true);
//        adventrue.gameObject.SetActive(false);
//    }

    public void hideAllPanel()
    {
        foreach (GameObject panel in Panels)
        {
            if (panel == null)
            {
                Panels.Remove(panel);
                break;
            }
        }
        foreach (GameObject panel in Panels)
        {
            if (panel.activeSelf)
            {

                panel.SetActive(false);
            }
        }

        TopPanel.gameObject.SetActive(false);
        resPanel.gameObject.SetActive(false);
        bottomPanel.gameObject.SetActive(false);
    }

    private void hideAllPanel(GameObject panel, bool showTop, bool showRes, bool showBottom)
    {
        var sPanel = panel.GetComponent<UIPanel>();
        hideAllPanel(sPanel, showTop, showRes, showBottom);
        
    }

    private void hideAllPanel(UIPanel sPanel, bool showTop, bool showRes, bool showBottom)
    {
        expPanel.gameObject.SetActive(false);
        chatBtnPanel.SetActive(false);
        chatBtnPanel.transform.localPosition = new Vector3();

        if (nowPanel && nowPanel != BattlePanel.gameObject)
        {
            lastPanel = nowPanel.GetComponent<UIPanel>();
            lastShowTop = TopPanel.gameObject.activeSelf;
            lastShowRes = resPanel.gameObject.activeSelf;
            lastShowBot = bottomPanel.gameObject.activeSelf;
        }

        foreach (GameObject go in Panels)
        {
            if (go != null)
            {
                var pan = go.GetComponent<UIPanel>();
                if (pan != sPanel)
                {
                    if (pan.gameObject.activeSelf)
                    {
                        pan.gameObject.SetActive(false);
                    }
                }
            }
        }

        nowPanel = sPanel.gameObject;
        sPanel.gameObject.SetActive(true);

        if (showTop)
        {
            TopPanel.gameObject.SetActive(true);
            resPanel.gameObject.SetActive(true);
        }
        else
        {
            resPanel.gameObject.SetActive(showRes);
            TopPanel.gameObject.SetActive(false);
        }
        if (bottomPanel.gameObject.activeSelf != showBottom)
        {
            bottomPanel.gameObject.SetActive(showBottom);
        }
        inBattle = BattlePanel.gameObject.activeSelf;
        
    }

    public void showLastPanel()
    {
        if (lastPanel)
        {
            hideAllPanel(lastPanel, lastShowTop, lastShowRes, lastShowBot);
        }
        else
        {
            showIndexPanel();
        }
    }

//    public void showGuide(GameObject obj)
//    {
//        Guide.show(obj);
//    }

    public void showHeroList()
    {
//        if (Data.hero.fightList == null && !heroList.gameObject.activeSelf)
        if (!adventrue.gameObject.activeSelf || Data.hero == null || Data.hero.fightList == null)
        {
            return;
        }
        if (!Data.hero.heroChange)
        {
            return;
        }
        if (!indexPanel.gameObject.activeSelf)
        {
            return;
        }
//        heroList.removeItems();
        Data.hero.heroChange = false;
//        int count = 0;
//        foreach (DictionaryEntry entry in Data.hero.fightList)
//        {
//            heroList.addItem((HeroInfo) entry.Value);
//            count++;
//        }
//        if (count < 5)
//        {
//            for (int i = 0; heroList.Length() < 5; i++)
//            {
//                heroList.addItem(null);
//            }
//        }

//        heroList.Reposition();
        adventrue.initHeroList();
    }

    private void updateIcon(object value)
    {
        Data.role.mod = (string) value;
        Data.role.icon = (string) value;
        Res.showLoginTexture(heroHead, "" + Data.role.icon);
    }

    private void updateExp(object value)
    {
        role_levelup_exp roleExp = (role_levelup_exp) Cache.RoleLevelupExpList[Data.role.level + 1];
        if (roleExp == null)
        {
            expLine.fillAmount = 1;
            expTxt.text = Manager.Language.GetString("IndexMain.expTxt.state1");
        }
        else
        {
            expLine.fillAmount = (float) Data.role.exp/roleExp.exp;
            //expTxt.text = string.Format(Manager.Language.GetString("IndexMain.expTxt.state2"), StringUtil.formatCount(roleExp.exp - Data.role.exp));
            expEffect.gameObject.SetActive(true);
            expEffect.ResetToBeginning();
            expEffect.Play();
            EventDelegate.Add(expEffect.onFinished, delegate
            {
                expEffect.gameObject.SetActive(false);
            }, true);
        }

    }

    private void showFightMax(object value)
    {
        Data.hero.update("formationTip", Data.role.fight_max-Data.hero.fightList.Count);
//        print("FightMax->" + value);
        setting_info hero_num_alert_level = (setting_info) Cache.SettingInfo["hero_num_alert_level"];

        if (Data.role.level > Convert.ToInt32(hero_num_alert_level.value))
        {
            showHeroOpen();
        }
    }

    public void showHeroOpen()
    {
        if (_openHeroPanel == null)
        {
            _openHeroPanel = ToolKit.loadPrefab<OpenHeroMaxPanel>("panles/OpenHeroMaxPanel", parent.transform, "OpenHeroMaxPanel");
        }
        _openHeroPanel.show();
    }

    public void hideHero()
    {
        if (_openHeroPanel)
        {
            Destroy(_openHeroPanel.gameObject);
            _openHeroPanel = null;
        }
        
    }

    public void showNewChangeMajorPanel(weapon_type_change cInfo)
    {
        if (_newChangeMajorPanel == null)
        {
            _newChangeMajorPanel = ToolKit.loadPrefab<NewMajorPanel>("panles/NewMajorPanel", parent.transform, "NewMajorPanel");
        }
//        main.gameGuide.bind(_newHeroPanel.changeBtn.gameObject, new ArrayList { 50001, 80004, 80008, 106001 });
        _newChangeMajorPanel.show(cInfo);
    }

    public void hideNewChangeMajorPanel()
    {
//        main.gameGuide.unbind(_newChangeMajorPanel);
        Destroy(_newChangeMajorPanel.gameObject);
        _newChangeMajorPanel = null;
    }
    public void showNewHeroOpen(ItemInfo info, int cpi, int type = 1)
    {
        if (_newHeroPanel == null)
        {
            _newHeroPanel = ToolKit.loadPrefab<NewHeroPanel>("panles/NewHeroPanel", parent.transform, "NewHeroPanel");
        }
        main.gameGuide.bind(_newHeroPanel.changeBtn.gameObject, new ArrayList { 50001, 80004, 80008, 106001, 140001 });
        _newHeroPanel.show(info,cpi,type);
    }

    public void hideNewHeroOpen()
    {
        if (_newHeroPanel != null)
        {
            main.gameGuide.unbind(_newHeroPanel.changeBtn);
            Destroy(_newHeroPanel.gameObject);
            _newHeroPanel = null;
        }
    }

    public NewHeroPanel getNewHeroPanel()
    {
        return _newHeroPanel;
    }

    public void showNewCollect(ItemInfo info, int cpi, string nameStr)
    {
        if (_newCollectPanel == null)
        {
            _newCollectPanel = ToolKit.loadPrefab<NewCollectHeroPanel>("panles/NewCollectHeroPanel", parent.transform, "NewCollectHeroPanel");
        }
        _newCollectPanel.show(info, cpi, nameStr);
    }

    public void hideNewCollect()
    {
        Destroy(_newCollectPanel.gameObject);
        _newCollectPanel = null;
    }

    public void showBanlance()
    {
        if (_balancePanel == null)
        {
            _balancePanel = ToolKit.loadPrefab<BalancePanel>("panles/BalancePanel", parent.transform, "BalancePanel");
        }
        _balancePanel.show();
    }

    public void hideBanlance()
    {
        Destroy(_balancePanel.gameObject);
        _balancePanel = null;
    }

    public void showSignIn()
    {
        if (_signInPanel == null)
        {
            _signInPanel = ToolKit.loadPrefab<SignInPanel>("panles/SignInPanel", parent.transform, "SignInPanel");
        }
        _signInPanel.show();
    }

    public void hideSignIn()
    {
        Destroy(_signInPanel.gameObject);
        _signInPanel = null;
    }

    public void showAnniversaryPanel()
    {
        if (_anniversaryPanel == null)
        {
            _anniversaryPanel = ToolKit.loadPrefab<AnniversaryPanel>("panles/AnniversaryPanel", parent.transform, "AnniversaryPanel");
        }
        hideAllPanel(_anniversaryPanel.gameObject, false, true, false);
       // _anniversaryPanel.gameObject.SetActive(true);
    }

    public void hidAnniversaryPanel()
    {
        if (_anniversaryPanel)
        {
            Destroy(_anniversaryPanel.gameObject);
            _anniversaryPanel = null;
        }
    }


    public void showYouyiAdPanel()
    {
        if (_youyiadPanel == null)
        {
            _youyiadPanel = ToolKit.loadPrefab<YouyiAdPanel>("panles/YouyiAdPanel", parent.transform, "YouyiAdPanel");
        }
        _youyiadPanel.show();
    }

    public void hideYouyiAdPanel()
    {
        if (_youyiadPanel)
        {
            Destroy(_youyiadPanel.gameObject);
            _youyiadPanel = null;
        }
    }

    private void showCpi(object value)
    {
        int old = Convert.ToInt32(cpiTxt.text);
        int cpi = (int) value;
        if (old == cpi)
        {
            return;
        }
        fightUp.show(cpi, cpi - old);

        var label1 = cpiTxt.gameObject.GetComponent<DynamicCountLabel>();
        //var label2 = cpi2Txt.gameObject.GetComponent<DynamicCountLabel>();

        label1.showCount(cpi);
        //label2.showCount(cpi);

        cpiEffect.gameObject.SetActive(true);
        cpiEffect.ResetToBeginning();
        cpiEffect.Play();
    }

    public void showTapLight()
    {
        taplight.gameObject.SetActive(true);
        taplight.ResetToBeginning();
        taplight.Play();
        Vector2 pos = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
        
        taplight.transform.position = pos;

        if (taplight.onFinished.Count == 0)
        {
            EventDelegate.Add(taplight.onFinished, () =>
            {
                taplight.gameObject.SetActive(false);
            }, true);
        }
    }

    public bool canShowGuide()
    {
        bool canShowSign = _signInPanel == null;
        bool canShow =  !levelUpPanel.gameObject.activeSelf && !awardGivePanel.gameObject.activeSelf;
        return canShow && canShowSign;
    }

    string lastMsg;
    private void checkAutoChat(int secound, int type)
    {
        if (Data.role.server_day > 2)
        {
            return;
        }
//        Data.role.server_day = 1;
        string idStr = Data.role.server_id % 2 + "_" + Data.role.server_day + "_" + secound + "_" + type;
//        print(idStr);
        chat_robot robot = (chat_robot)Cache.ChatRobotList[idStr];
//        if (gcCount%3 == 1)
//        {
//            robot = (chat_robot)Cache.ChatRobotList["0_1_32402_2"];
//        }
        if (robot != null)
        {
//            print(robot.message);
            if (lastMsg == idStr)
            {
                return;
            }
            lastMsg = idStr;
            if (showMsgList != null)
            {
                ChatData.Msg msgInfo = new ChatData.Msg();

                msgInfo.type = robot.kind;
                msgInfo.uid = "npc";
                msgInfo.name = robot.name;
                msgInfo.msg = robot.message;
                msgInfo.mod = robot.model;
                msgInfo.time = DateTime.Now.ToString("MM-dd HH:mm");
                msgInfo.vip = 0;
                msgInfo.quality = robot.appraise;
                msgInfo.position = robot.position;

                msgInfo.short_name = getCountryShort(robot.country_id);
                showMsgList.Add(msgInfo);

                adventrue.showScreenMsg(robot.message);
                if (msgInfo.type == 1)
                {
                    Data.chat.msgList.Add(msgInfo);
                }
                else if (msgInfo.type == 2)
                {
                    Data.chat.familyList.Add(msgInfo);
                }

                chatPanel.showAddLogs(msgInfo);
            }
        }
    }

    private string getCountryShort(int id)
    {
        league_info lInfo = (league_info)Cache.LeagueInfoList[Data.league.id];
        if (lInfo != null)
        {
            return lInfo.short_name;
        }
        return "";
    }

    private void updateTime(object value)
    {
        Data.role.record_server_time = StringUtil.GetTimeStamp();
        Data.role.local_time = (int) value;

    }

    public void checkGemTip()
    {
        if (Data.hero.fightList == null)
            return;

        Data.hero.gemTipHero.Clear();

        int _controlGemTip = 0;

        var eEntry = Data.hero.fightList.GetEnumerator();
        while (eEntry.MoveNext())
        {
            HeroInfo hInfo = (HeroInfo)eEntry.Value;

            if (hInfo.baseInfo.sp_weapon_type == 0 || hInfo.baseInfo.appraise < 5)
            {
                continue;
            }

            setting_info hero_upgrade_max = (setting_info)Cache.SettingInfo["sp_weapon_awake_max"];
            int wakeMax = Convert.ToInt32(hero_upgrade_max.value);

            bool awakeMax = false;
            bool levelMax = false;
            int hideCount = 0;
            if (hInfo.master == 1)
            {
                setting_info sp_weapon_role_limit = (setting_info)Cache.SettingInfo["sp_weapon_role_limit"];
                Dictionary<int, int> mList = new Dictionary<int, int>();
                string[] mArr = StringUtil.Split(sp_weapon_role_limit.value, ";");
                for (int i = 0; i < mArr.Length; i++)
                {
                    string[] gArr = StringUtil.Split(mArr[i], ":");
                    int q = Convert.ToInt32(gArr[0]);
                    int m = Convert.ToInt32(gArr[1]);
                    mList.Add(q, m);
                }

                int max = mList[hInfo.quality];
                awakeMax = !(hInfo.gem_wake < wakeMax && hInfo.gem_wake < max);
            }
            else
            {
                awakeMax = !(hInfo.gem_wake < wakeMax);
            }

            if (!awakeMax)
            {
                hero_sp_weapon_type typeInfo = (hero_sp_weapon_type) Cache.HeroSpWeaponTypeList[hInfo.baseInfo.sp_weapon_type];
                int nextLevel = hInfo.gem_wake + 1 > wakeMax ? wakeMax : hInfo.gem_wake + 1;
                PropertyInfo reqInfo = typeInfo.GetType().GetProperty("req_" + nextLevel);
                string reqStr = (string)reqInfo.GetValue(typeInfo, null);
                List<ItemInfo> reqList = ResInfo.str2itemList(reqStr);

                if (Data.bag.getItemCount(reqList[0].sysid) >= reqList[0].num && Data.bag.getItemCount(reqList[1].sysid) >= reqList[1].num)
                {
                    Data.hero.update("gemTip", 1);
                    _controlGemTip = 1;
                    Data.hero.gemTipHero.Add(hInfo.pid);
                }
            }

            hero_sp_weapon_level nextInfo = (hero_sp_weapon_level) Cache.HeroSpWeaponLevelList[hInfo.gem_level + 1];
            levelMax = !(nextInfo != null && hInfo.gem_wake >= nextInfo.awake_level);
            if (!levelMax)
            {
                setting_info sp_weapon_material_item = (setting_info)Cache.SettingInfo["sp_weapon_material_item"];
                string[] weaponArr = StringUtil.Split(sp_weapon_material_item.value, ",");
                ItemInfo weaponInfo = ItemInfo.createItemInfoByBaseId(weaponArr[hInfo.baseInfo.weapon_type - 1]);

                PropertyInfo wwInfo = nextInfo.GetType().GetProperty("num_" + hInfo.baseInfo.appraise);
                int countStr = (int)wwInfo.GetValue(nextInfo, null);
                weaponInfo.num = countStr;

                wwInfo = nextInfo.GetType().GetProperty("coin_num_" + hInfo.baseInfo.appraise);
                int cc = (int)wwInfo.GetValue(nextInfo, null);
                ItemInfo coinInfo = ItemInfo.createItemInfoByBaseId("coin");
                coinInfo.num = cc;

                if (Data.bag.getItemCount(weaponInfo.sysid) >= weaponInfo.num && Data.role.coin >= coinInfo.num)
                {
                    Data.hero.update("gemTip", 1);
                    _controlGemTip = 1;
                    Data.hero.gemTipHero.Add(hInfo.pid);
                }
            }
        }

        if (_controlGemTip == 0)
        {
            Data.hero.update("gemTip", 0);
        }
    }

    private void updateLocalTime()
    {
        gcCount++;
        
        DateTime now = DateTime.Now;
        int totalSec = now.Hour * 3600 + now.Minute * 60 + now.Second;
//        totalSec = 36789;
        checkAutoChat(totalSec, 1);
        checkAutoChat(totalSec, 2);

        timeTxt.text = DateTime.Now.ToString("hh:mmt\\M");  
//        if (gcCount%10 == 0)
//        {
////            Socket.Closed();
//            Socket.Send(CMD.Test_msg);
//        }

//        if (gcCount % 300 == 0)
//        {
//            Cache.ChatRobotList[]
//            Resources.UnloadUnusedAssets();
//            GC.Collect();

//            print("==================== GC ====================");
//        }

        if (gcCount % 5 == 0)
        {
            if (Socket.cacheBytes != null)
            {
                testText.text = Socket.cacheBytes.Length + "";
            }
            else
            {
                testText.text = "";
            }
            
            Tip.removeAllTips();
        }

        if (gcCount % 120 == 0)
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
            Debuger.Log("==================== GC ====================");

            checkGemTip();
        }

        if (StringUtil.IsExpriredByHour(DateTime.Today, cdHour))
        {
            cdHour = MathUtil.nearest(DateTime.Now.Hour, cdArr);
            Data.activity.update("mealTip", 1);
        }

        
        long defTime = StringUtil.GetTimeStamp() - Data.role.record_server_time;
        if (Data.role.local_time > 0)
        {
            Data.role.local_time = Data.role.server_time + Mathf.RoundToInt(defTime);
//            print(defTime);
        }

        

////        if (Data.role.stamina > 1)
////        {
////            Data.role.stamina--;
//
//        var roleExp = (role_levelup_exp)Cache.RoleLevelupExpList[Data.role.level + 1];
//        if (Data.adventure.nowMain != null)
//        {
//            Data.role.update("exp", Data.role.lastExp + Data.adventure.nowMain.team_exp_sec * Mathf.RoundToInt(defTime));
//            Data.role.update("coin", Data.role.lastCoin + Data.adventure.nowMain.coin_sec * Mathf.RoundToInt(defTime));
//        }
////            if (!expGet.gameObject.activeSelf)
////            {
////                expGet.gameObject.SetActive(true);
////                moneyEffect.gameObject.SetActive(true);
////            }
//
//        vipText.text = Data.role.exp + "";
////        vipTxt.text = Data.role.local_time+"";
//        vipTxt.text = DateTime.UtcNow.ToString();
////        moneyEffect.gameObject.SetActive(true);
//
//        if (roleExp != null && Data.role.exp >= roleExp.exp && !inBattle)
//        {
////                print("==========before===========");
////                print("level->" + Data.role.level);
////                print("exp->" + Data.role.exp);
////                print("coin->" + Data.role.coin);
////                print("stamina->" + Data.role.stamina);
////                print("=====================");
//            Socket.Send(CMD.RoleExplore_update);
//            return;
//        }
//
//        if (Data.hero.fightList != null)
//        {
//            
//            foreach (DictionaryEntry entry in Data.hero.fightList)
//            {
//                var hero = (HeroInfo) entry.Value;
//
//                hero.exp = (int) (hero.expLast + Data.adventure.nowMain.hero_exp_sec * defTime);
//                var expMax = (hero_exp) Cache.HeroExpList[hero.level + 1];
//
//                if (hero.exp >= expMax.getExp && hero.level< hero.level_max && !inBattle)
//                {
//                    if (Time.realtimeSinceStartup - _updateTime > 10)
//                    {
//                        adventrue.showHeroLevelUp(hero.pid);
//                        Socket.Send(CMD.RoleExplore_update);
//                        _updateTime = Time.realtimeSinceStartup;
//                    }
//                    break;
//                }
//            }
//        }
        
    }

    public void Update()
    {
        gameGuide.checkGuide();
    }

    public void playIndexMusic()
    {
        if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 6)
        {
            SoundManager.PlayConnection("bgm1");
        }
        else if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 12)
        {
            SoundManager.PlayConnection("bgm2");
        }
        else if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 21)
        {
            SoundManager.PlayConnection("bgm3");
        }
        else
        {
            SoundManager.PlayConnection("bgm4");
        }
    }

    private int lastTime;
    private void OnApplicationPause(bool paused)
    {
        
        if (paused)
        {
            lastTime = Data.role.local_time;
        }
        else
        {
            if (Socket)
            {
                if (Data.role.local_time - lastTime > 60)
                {
                    Socket.Send(CMD.Role_loginr, Data.Manager.loginMsg);
                }
            }

            //showPower();
            CleanNotification();
        }
    }

    IEnumerator UpdataBattery()
    {
        while (true)
        {
            //showPower();
            yield return new WaitForSeconds(60f);
        }
    }

    private void showPower()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            netState.spriteName = "";
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            netState.spriteName = "mobilesignal";
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            netState.spriteName = "wifi1";
        }
#if UNITY_IOS && !UNITY_EDITOR
        powerLine.fillAmount = Manager.getBatteryLevel()*100 / 100f;
//        string battery = Manager.getBatteryLevel().ToString();
#else
        powerLine.fillAmount = Manager.GetBatteryLevel() / 100f;
//        string battery = Manager.GetBatteryLevel().ToString();
#endif
//        Debug.Log("Power->" + battery + " float->" + powerLine.fillAmount);
    }

    public void ShowLoadingPage(bool val)
    {
        if(loadingPannel != null)
            loadingPannel.SetActive(val);
    }
}

public class Player
{
    public int age;
    public string name;
    public Gou t = new Gou {name = "22a", age = 22};
}

public class Gou
{
    public string name { get; set; }
    public int age { get; set; }
}