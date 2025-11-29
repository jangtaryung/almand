using System;
using System.Collections;
using LitJson;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Battle : BaseMonoBehaviour
{
    public UIAtlas ballEffect;

    public SkillShow skillShow1;
    public SkillShow skillShow2;

    public SkillTalkShow skillTalkShow1;

    public GameObject screenTxt;
    public UIPanel screenTxts;
    private ArrayList screenTxtList;

    public GameObject startBlack;
    public GameObject startFight;
    public GameObject startSign;

    public UIButton skipBtn;
    public UIButton endBtn;
    public UIButton guideBtn;
    public UIButton replayBtn;
    public UIButton quickBtn;

    public GameObject quickBtn_x1;
    public GameObject quickBtn_x2;
    public UIButton pauseBtn;
    public BattleArmy army1;
    public BattleArmy army2;

    public UITexture bg;
    public UITexture effect;
    public GameObject black;
    public GameObject arrow;

    public GameObject resultPanel;
    public GameObject win;
    public GameObject lose;
    public UISpriteAnimation winStar;

    public UISprite roundSign;

    private int roundCount;

    private ArrayList roundList;
    private ArrayList tarigger;
    private ArrayList ballList;

    private const int endTime = 10;
    private int restTime = 10;
    private const float quick1 = 1.2f;
    private const float quick2 = 2.0f;
    private const float quick4 = 3f;

    private bool checkGuide;


    public UIGrid btGrid;
    //텍스트 추출
    public UILabel fightNum1;
    public UILabel fightNum2;
    public UILabel testTxt1;
    public UILabel testTxt2;
    public UILabel nameTxt1;
    public UILabel nameTxt2;
    public UILabel endTimeTxt;
    private UILabel replayBtnTxt;
    private UILabel guideBtnTxt;
    private UILabel endBtnTxt;
    private UILabel skipBtnTxt;

    void InitPanel() 
    {
        replayBtnTxt = replayBtn.transform.Find("Label").GetComponent<UILabel>();
        guideBtnTxt = guideBtn.transform.Find("Label").GetComponent<UILabel>();
        endBtnTxt = endBtn.transform.Find("Label").GetComponent<UILabel>();
        skipBtnTxt = skipBtn.transform.Find("Label").GetComponent<UILabel>();

        InitDefaultUIlabel();
    }

    void InitDefaultUIlabel() 
    {
        replayBtnTxt.text = Manager.Language.GetString("Battle.replayBtnTxt");
        guideBtnTxt.text = Manager.Language.GetString("Battle.guideBtnTxt");
        endBtnTxt.text = Manager.Language.GetString("Battle.endBtnTxt");
        skipBtnTxt.text = Manager.Language.GetString("Battle.skipBtnTxt");
    }

    public void Awake()
    {
        InitPanel();

        UIEventListener.Get(endBtn.gameObject).onClick = ButtonClick;
        UIEventListener.Get(guideBtn.gameObject).onClick = ButtonClick;
        UIEventListener.Get(skipBtn.gameObject).onClick = ButtonClick;
        UIEventListener.Get(quickBtn.gameObject).onClick = ButtonClick;
        UIEventListener.Get(replayBtn.gameObject).onClick = ButtonClick;
        UIEventListener.Get(pauseBtn.gameObject).onClick = ButtonClick;
        army1.gameObject.SetActive(false);
        army2.gameObject.SetActive(false);
        effect.gameObject.SetActive(false);
        black.SetActive(false);
        army1.battle = this;
        army2.battle = this;

        if (GlobalTools.isBanshu)
        {
            startFight.GetComponent<UISprite>().spriteName = "";
        }

        screenTxtList = new ArrayList();
    }

    private void ButtonClick(GameObject go)
    {
        SoundUtils.PlaySFX(SoundUtils.effect.click);
        if (go == skipBtn.gameObject)
        {
//            skillShow1.show(null,null);
//            army1.showUnderSkill();
//            army2.showUnderSkill();
//            army1.showAddBuff();
//            army2.showAddBuff();
//            return;
            showResult();
        }
        else if (go == endBtn.gameObject)
        {
//            army1.showArrow();
//            army2.showArrow();
//            show();
            //            skillShow1.show(null);
            //            skillShow2.show(null);
//            main.showIndexPanel();
            GC.Collect();
            Data.battle.showCallBack();
//            if (main.awardGivePanel.gameObject.activeSelf)
//            {
//                main.awardGivePanel.showMovie();
//            }
        }
        else if (go == guideBtn.gameObject)
        {
            guideBtn.gameObject.SetActive(false);
            Data.battle.showCallBack();
            main.showAdventureGuidePanel();
            btGrid.enabled = true;
            btGrid.Reposition();
            btGrid.repositionNow = true;
        }
        else if (go == replayBtn.gameObject)
        {
            show();
        }
        else if (go == quickBtn.gameObject)
        {
            arrow.SetActive(false);
            UILabel label = quickBtn.gameObject.transform.Find("Label").gameObject.GetComponent<UILabel>();
            if (label.text == "X1")
            {
                Time.timeScale = quick2;
                label.text = "X2";
                quickBtn_x1.SetActive(false);
                quickBtn_x2.SetActive(true);
                PlayerPrefsUtility.SetFloat(GlobalTools.UserInfoSpeed, quick2);
            }
            else if (label.text == "X2" && Data.role.vip >= 2)
            {
                Time.timeScale = quick4;
                label.text = "X4";
                PlayerPrefsUtility.SetFloat(GlobalTools.UserInfoSpeed, Time.timeScale);
            }
            else
            {
                Time.timeScale = quick1;
                label.text = "X1";
                PlayerPrefsUtility.SetFloat(GlobalTools.UserInfoSpeed, Time.timeScale);
                quickBtn_x1.SetActive(true);
                quickBtn_x2.SetActive(false);
            }
        }
        else if (pauseBtn.gameObject == go)
        {
            Time.timeScale = Time.timeScale < quick1 ? quick1 : 0;
            testTxt1.gameObject.SetActive(true);
            testTxt2.gameObject.SetActive(true);
        }
    }

    private void showResult()
    {
        SoundManager.StopMusic();
        CancelInvoke("showRound");
        win.SetActive(false);
        lose.SetActive(false);
        GameObject endGo;
        if (Data.battle.winner == 0)
        {
            endGo = win;
            guideBtn.gameObject.SetActive(false);
            winStar.ResetToBeginning();
            winStar.Play();
            SoundUtils.PlaySFX("win");
            btGrid.enabled = true;
            btGrid.Reposition();
            btGrid.repositionNow = true;
        }
        else
        {
            endGo = lose;
            menus info = (menus)Cache.MenusList[IndexPanel.Menu.AdventureGuidePanel.ToString()];
            if (Data.role.level < info.level || main._worldWarPanel != null)
            {
                guideBtn.gameObject.SetActive(false);
            }
            else 
            {
                guideBtn.gameObject.SetActive(true);
            }
            btGrid.enabled = true;
            btGrid.Reposition();
            btGrid.repositionNow = true;
            SoundUtils.PlaySFX("lose");
        }

        endGo.SetActive(true);
        resultPanel.SetActive(true);
        TweenScale ts = endGo.GetComponent<TweenScale>();
        ts.ResetToBeginning();
        ts.PlayForward();
    }


    public void OnEnable()
    {
        if (Socket != null)
        {
            
//            Socket.addCallBack(CMD.Test_Combat, callBack);
//            Socket.Send(CMD.Test_Combat);
        }
        show();
        showBattleBg();
    }

    public void OnDisable()
    {
        if (Socket != null)
        {
            Time.timeScale = 1;

//            Socket.removeCallBack(CMD.Test_Combat, callBack);
            main.playIndexMusic();
        }
    }

    public void showBattleBg()
    {
        if (Data.battle.type == 4 || Data.battle.type == 5)
        {
            bg.mainTexture = Res.getBattleBg("battleBG" + Data.mystery.now.battle_bg);
        }
        else if (Data.battle.type == 11)
        {
            bg.mainTexture = Res.getBattleBg("battleBG" + Data.leagueWar.cityInfo.city.battle_bg);
        }
        else if (Data.battle.type == 14)
        {
            bg.mainTexture = Res.getBattleBg("battleBG8");
        }
        else
        {
            bg.mainTexture = Res.getBattleBg("battleBG" + Data.adventure.nowMain.map);
        }
    }

    private void callBack(JsonData msg)
    {
        GC.Collect();
        Data.battle.initData(msg["data"]);
        Data.battle.initBattle();

        GC.Collect();
        Invoke("show", 0.2f);
    }

//    private string getTestStr(bool isLeft)
//    {
//        string showStr = "";
//        BattleData.Team info;
//        Hashtable heroArr;
//        if (isLeft)
//        {
//            info = Data.battle.team1;
//            heroArr = Data.battle.heroArr1;
//        }
//        else
//        {
//            info = Data.battle.team2;
//            heroArr = Data.battle.heroArr2;
//        }
//        showStr += "Def->"+info.def+" ";
//        showStr += "Dod->" + info.dodge + " ";
//        showStr += "Ten->" + info.tenacity + "\n";
//        foreach (DictionaryEntry entry in heroArr)
//        {
//            BattleData.Hero heroInfo = (BattleData.Hero) entry.Value;
//
//            showStr += heroInfo.name + " atk->" + heroInfo.atk + "";
//            showStr += " crit->" + heroInfo.crit + "";
//            showStr += " hit->" + heroInfo.hit + "\n";
//        }
//        return showStr;
//    }

    public void showInfo()
    {
        CancelInvoke();
        
        restTime = endTime;
        if (Data.role.isGm || Data.role.vip == 3)
        {
            restTime = 2;
        }
        Invoke("showSkipTime", 1);


        float delay = 0.2f;
        float delay2 = 1.2f;

        startFight.SetActive(true);
        startFight.transform.localPosition = new Vector3(410, 164);
        startFight.transform.DOLocalMoveX(0, 0.4f).SetEase(Ease.InSine).SetDelay(delay);
        startFight.transform.DOLocalMoveX(-410, 0.4f).SetEase(Ease.InSine).SetDelay(delay2);

        startSign.SetActive(true);
        startSign.transform.localPosition = new Vector3(-502, 239);
        startSign.transform.DOLocalMoveX(0, 0.4f).SetEase(Ease.InSine).SetDelay(delay);
        startSign.transform.DOLocalMoveX(552, 0.4f).SetEase(Ease.InSine).SetDelay(delay2).OnComplete(() =>
        {
            startBlack.SetActive(false);
            Invoke("showRound", 0.2f);
            showBattleScreen();
        });

        startBlack.SetActive(true);
        startBlack.transform.localScale = new Vector3(1, 0);
        startBlack.transform.DOScaleY(1, 0.4f);
        startBlack.transform.DOScaleY(0, 0.4f).SetDelay(delay2 + delay);

    }

    private void showRound()
    {
//        return;
        if (resultPanel.activeSelf || startBlack.activeSelf)
        {
            return;
        }
        if (roundList == null || roundList.Count == 0)
        {
            Debuger.Log("roundCount->" + roundCount);
            if (roundCount == 5)
            {
                checkGuideSpeed();
            }
            roundList = Data.battle.getRoundAction(roundCount);
            tarigger = checkTrigger(roundList);
            roundCount++;
            if (roundCount == 1 && roundList.Count == 0)
            {
                showRound();
                return;
            }
            if (roundList.Count == 0 && tarigger.Count != 0)
            {
                tarigger = mergyList(tarigger);
                roundList = tarigger;
            }
            if (roundList.Count == 0)
            {
                CancelInvoke();
                showResult();
                showSkipBtn();
//                print("Battle Over");
                return;
            }
        }

        BattleData.Action act = (BattleData.Action) roundList[0];
        roundList.RemoveAt(0);

        roundSign.spriteName = (act.round) + "";

        int side = Data.battle.getTeamByHero(act.from);
        act.side = side;

        ArrayList buffArr = null;
        if (act.type == BattleData.ACT_ADD_BUFF || act.type == BattleData.ACT_DEL_BUFF)
        {
            buffArr = checkBuff(act, roundList);
        }

        if (buffArr == null)
        {
            buffArr = new ArrayList {act};
        }
//        hideEffect();
        if (side == 0)
        {
            checkSide(side, buffArr, delegate
            {
                if (roundCount == 1)
                {
                    showRound();
                }
                else
                {
//                    StartCoroutine(checkHP(act));
                    checkHP(act, buffArr);
                }
            });
        }
        else
        {
            checkSide(side, buffArr, delegate
            {
                if (roundCount == 1)
                {
                    showRound();
                }
                else
                {
//                    StartCoroutine(checkHP(act));
                    checkHP(act, buffArr);
                }
            });
        }
    }

    private ArrayList mergyList(ArrayList arrayList)
    {
        ArrayList list = new ArrayList();
        Hashtable tempList = new Hashtable();
        for (int i = 0; i < arrayList.Count; i++)
        {
            BattleData.Action actc = (BattleData.Action)arrayList[i];

            foreach (DictionaryEntry entry in actc.data.heroList)
            {
                int index = (int) entry.Key;
                BattleData.Effect_hero eInfo = (BattleData.Effect_hero) entry.Value;
                BattleData.Effect_hero gInfo = (BattleData.Effect_hero) tempList[index];
                if (gInfo != null)
                {
                    gInfo.hp += eInfo.hp;
                }
                else
                {
                    tempList[index] = eInfo;
                }
            }
            
        }
        if (tempList.Count > 0)
        {
            BattleData.Action end = (BattleData.Action) arrayList[0];
            end.data.heroList = tempList;
            list.Add(end);
        }
        return list;
    }

    private ArrayList checkTrigger(ArrayList arrayList)
    {
        ArrayList list = new ArrayList();
        for (int i = 0; i < arrayList.Count; i++)
        {
            BattleData.Action actc = (BattleData.Action) arrayList[i];
            if (BattleData.ACT_TRIGGER_SKILL == actc.type)
            {
                list.Add(actc);
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            arrayList.Remove(list[i]);
        }
        return list;
    }

    private ArrayList checkBuff(BattleData.Action act, ArrayList arrayList)
    {
        ArrayList list = new ArrayList {act};
        for (int i = 0; i < arrayList.Count; i++)
        {
            BattleData.Action actc = (BattleData.Action) arrayList[i];
            if (act.type == actc.type)
            {
                list.Add(actc);
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            arrayList.Remove(list[i]);
        }

        return list;
    }

    private void checkSide(int side, ArrayList actArr, BattleArmy.CallBack callback)
    {
        for (int i = 0; i < actArr.Count; i++)
        {
            BattleData.Action act = (BattleData.Action) actArr[i];
            if (side == 0)
            {
                army1.showAct(act, tarigger, i == 0 ? callback : null);
            }
            else if (side == 1)
            {
                army2.showAct(act, tarigger, i == 0 ? callback : null);
            }
            else
            {
                print(side);
            }
        }
    }

    private void checkHP(BattleData.Action act, ArrayList buffArr)
    {
        changeHP(act, buffArr);

        Invoke("showRound", 0.5f + 0.2f * Time.timeScale);
    }

    private void changeHP(BattleData.Action act, ArrayList buffArr)
    {
        if (act.type == BattleData.ACT_NORMAL_ATTACK || act.type == BattleData.ACT_SKILL_ATTACK)
        {
            BattleData.To[] toInfo = act.toInfo;
            if (toInfo == null)
            {
                Tip.show(string.Format("Battle.changeHP.tip"), act.count);
                return;
            }
            int len = toInfo.Length;
            int playCount = 0;
            Skill_info skill = (Skill_info)Cache.SkillInfo[act.data.skillid];
            for (int i = 0; i < len; i++)
            {
                BattleData.To gTo = toInfo[i];
                GameObject hero = army1.getHeroFromAll(gTo.id);
                BattleAvatar avatar = hero.GetComponent<BattleAvatar>();
                avatar.showHP(gTo.hp);
                avatar.showAtkEffect(gTo);

                bool isDead = gTo.hp <= 0;

                if (act.type == BattleData.ACT_NORMAL_ATTACK || act.type == BattleData.ACT_SKILL_ATTACK)
                {
                    if (skill != null)
                    {
                        if (act.type == BattleData.ACT_SKILL_ATTACK)
                        {
                            //                        if (playCount == 0 || Random.Range(1, 100) > 40)
                            playCount++;
                            float delayTime = 0.02f * playCount;
                            MovieUtils.delayCall(avatar.gameObject, delayTime, () =>
                            {
                                //                            skill.movie = "wind";
                                if (gTo.damage < 0)
                                {
                                    avatar.UnderAttack(skill, isDead);
                                }
//                                else if (gTo.recover > 0)
//                                {
//                                    avatar.showAddHp(skill);
//                                }
                            });
                        }
                        else
                        {
                            avatar.UnderAttack(null, isDead);
                        }
                    }
                    else
                    {
                        avatar.UnderAttack(null, isDead);
                    }
                }
            }
            showShake();
        }

        for (int i = 0; i < buffArr.Count; i++)
        {
            BattleData.Action popAct = (BattleData.Action) buffArr[i];
            //속성 변경
            if (popAct.data.heroList != null)
            {
                foreach (DictionaryEntry entry in popAct.data.heroList)
                {
                    int index = (int)entry.Key;
                    BattleData.Effect_hero eInfo = (BattleData.Effect_hero)entry.Value;
                    GameObject hero = army1.getHeroFromAll(index);
                    BattleAvatar avatar = hero.GetComponent<BattleAvatar>();
                    avatar.showSkillEffect(eInfo);
                }
            }
        }
        
        
    }


    public void show()
    {
        SoundManager.PlayConnection("Battle");
        CancelInvoke();

        effect.gameObject.SetActive(false);
        black.SetActive(false);

        startSign.SetActive(false);
        startBlack.SetActive(false);
        startFight.SetActive(false);
        resultPanel.SetActive(false);
        skillShow1.gameObject.SetActive(false);
        skillShow2.gameObject.SetActive(false);
        skillTalkShow1.gameObject.SetActive(false);
        startSign.transform.localPosition = new Vector2(-500, 26);

        clearBallList();

        nameTxt1.text = Data.battle.team1.name;
        nameTxt2.text = Data.battle.team2.name;

        fightNum1.text = /*string.Format(Manager.Language.GetString("Battle.fightNum"), Data.battle.team1.cpi);*/Data.battle.team1.cpi.ToString();
        fightNum2.text = /*string.Format(Manager.Language.GetString("Battle.fightNum"), Data.battle.team2.cpi)*/Data.battle.team2.cpi.ToString();

        roundSign.spriteName = "1";

        if (PlayerPrefsUtility.HasKey(GlobalTools.UserInfoSpeed))
        {
            Time.timeScale = PlayerPrefsUtility.GetFloat(GlobalTools.UserInfoSpeed);
        }
        else
        {
            Time.timeScale = quick2;
        }

        if (PlayerPrefsUtility.HasKey(GlobalTools.SpeedGuide))
        {
            checkGuide = false;
        }
        else
        {
            checkGuide = true;
        }
        UILabel label = quickBtn.gameObject.transform.Find("Label").gameObject.GetComponent<UILabel>();
        if (Time.timeScale > quick2)
        {
            label.text = "X4";
            quickBtn_x1.SetActive(false);
            quickBtn_x2.SetActive(true);
        }
        else if (Time.timeScale > quick1)
        {
            label.text = "X2";
            quickBtn_x1.SetActive(false);
            quickBtn_x2.SetActive(true);
        }
        else
        {
            label.text = "X1";
            quickBtn_x1.SetActive(true);
            quickBtn_x2.SetActive(false);
        }

        army1.show(Data.battle.heroArr1, true);
        army2.show(Data.battle.heroArr2, false);

        roundCount = 0;
        roundList = new ArrayList();

        skipBtn.gameObject.SetActive(false);
        army1.gameObject.transform.localPosition = new Vector2(-300, 0);
        army2.gameObject.transform.localPosition = new Vector2(300, 0);

        if (Data.battle.skills != null)
        {
            for (int i = 0; i < Data.battle.skills.Length; i++)
            {
                int id = (int)Data.battle.skills[i];
                Skill_info skill = (Skill_info)Cache.SkillInfo[id];
                Res.getSkillEffect(skill.movie);
            }
        }
        
        Invoke("showEnter", 0.5f);
    }

    private void showEnter()
    {
        moveArmy(army1.gameObject, new Vector2(-300, 0), new Vector2(0, 0));
        moveArmy(army2.gameObject, new Vector2(300, 0), new Vector2(0, 0), () =>
        {
            army1.showState(BaseAvatar.Action.stand);
            army2.showState(BaseAvatar.Action.stand);
        });

        army1.showState(BaseAvatar.Action.walk);
        army2.showState(BaseAvatar.Action.walk);
        
//        testTxt1.text = getTestStr(true);
//        testTxt2.text = getTestStr(false);
        testTxt1.gameObject.SetActive(false);
        testTxt2.gameObject.SetActive(false);

        Invoke("showInfo", 0.2f);
    }


    private void moveArmy(GameObject go, Vector2 from, Vector2 to, Action callback = null)
    {
        go.transform.localPosition = from;
        go.transform.DOLocalMove(to, 0.4f).OnComplete(() =>
        {
            if (callback != null)
            {
                callback();
            }
        });
    }

    private void showSkipTime()
    {
        restTime--;
        endTimeTxt.text = "Time：" + restTime;
        if (restTime < 0)
        {
            showSkipBtn();
        }
        else
        {
            Invoke("showSkipTime", 1);
        }
    }

    private void showSkipBtn()
    {
        skipBtn.gameObject.SetActive(true);
        endTimeTxt.text = "";
    }

    private void checkGuideSpeed()
    {
        if (checkGuide)
        {
            UILabel label = quickBtn.gameObject.transform.Find("Label").gameObject.GetComponent<UILabel>();
            if (label.text == "X1")
            {
                arrow.SetActive(true);
            }
            PlayerPrefsUtility.SetInt(GlobalTools.SpeedGuide, 1);
        }
    }

    public void showEffect(bool isCrit)
    {
        GameObject showObj = isCrit ? effect.gameObject : black;
        
        showObj.SetActive(true);
        if (effect.gameObject.activeSelf)
        {
            effect.mainTexture = Res.getBattleEffect("effect" + Random.Range(1, 4));
        }
        TweenAlpha ta = MovieUtils.getComponent<TweenAlpha>(showObj);
        ta.onFinished.Clear();
        ta.duration = 0.2f;
        ta.from = 0;
        ta.delay = 0;
        ta.to = 1;
        ta.ResetToBeginning();
        ta.PlayForward();
//        EventDelegate.Set(ta.onFinished, () => {  });
    }

    public void hideEffect()
    {
        GameObject showObj = null;
        if (effect.gameObject.activeSelf)
        {
            showObj = effect.gameObject;
        }
        else if (black.activeSelf)
        {
            showObj = black;
        }
        if (showObj != null)
        {
            TweenAlpha ta = MovieUtils.getComponent<TweenAlpha>(showObj);
            ta.duration = 0.2f;
            ta.from = 1;
            ta.delay = 0;
            ta.to = 0;
            ta.ResetToBeginning();
            ta.PlayForward();
            EventDelegate.Set(ta.onFinished, () => { showObj.SetActive(false); });
        }
    }

    public void showBallAttack(BattleAvatar from, BattleAvatar to, string spriteName, Action callback)
    {
        UISprite ball = NGUITools.AddSprite(gameObject, ballEffect, spriteName + "0001");
        ball.gameObject.SetActive(true);
        ball.MakePixelPerfect();
        ball.gameObject.transform.localScale = from.left ? Vector3.one : new Vector3(-1, 1, 1);
        UISpriteAnimation ballAni = ball.gameObject.AddComponent<UISpriteAnimation>();
        ballList.Add(ball.gameObject);
        ballAni.loop = true;
        ballAni.namePrefix = spriteName;
        ballAni.framesPerSecond = 12;

        ball.transform.localPosition  = from.transform.localPosition;
        ball.transform.DOLocalMove(to.transform.localPosition, 0.3f).OnComplete(() =>
        {
            if (callback != null)
            {
                callback();
            }
            Destroy(ball.gameObject);
        });
    }

    private void clearBallList()
    {
        if (ballList == null)
        {
            ballList = new ArrayList();
        }
        int len = ballList.Count;
        for (int i = 0; i < len; i++)
        {
            GameObject go = (GameObject) ballList[i];
            Destroy(go);
        }
    }

    public void showShake()
    {
        shake(3, new Vector3());
    }

    private void shake(int count, Vector2 init, int gap = 20, float duration = 0.06f, bool backInit = true,
        Action shakeBack = null)
    {
        TweenPosition tp = MovieUtils.getComponent<TweenPosition>(gameObject);

        tp.ignoreTimeScale = false;
        tp.duration = duration;
        tp.from = backInit ? (Vector3)init : transform.localPosition;

        tp.delay = 0f;
        tp.to = new Vector3(init.x + Random.Range(-gap / 2, gap / 2), init.y + Random.Range(-gap, gap));
        tp.ResetToBeginning();
        tp.PlayForward();

        if (count <= 0)
        {
            EventDelegate.Add(tp.onFinished, delegate
            {
                if (backInit)
                {
                    tp.duration = duration;
                    tp.from = tp.to;
                    tp.delay = 0f;
                    tp.to = init;
                    tp.ResetToBeginning();
                    tp.PlayForward();
                }

                if (shakeBack != null)
                {
                    shakeBack();
                }
            }, true);
        }
        else
        {
            EventDelegate.Add(tp.onFinished, delegate
            {
                count--;
                this.shake(count, init, gap, duration, backInit, shakeBack);
            }, true);
        }
    }

    private void showBattleScreen()
    {
        for (int i = 0; i < screenTxtList.Count; i++)
        {
            Destroy((GameObject)screenTxtList[i]);
        }
        screenTxtList.Clear();

        addScreenTxt(Data.battle.team1.sign, 0);
        addScreenTxt(Data.battle.team2.sign, 1, 0.5f);
        
    }

    private void addScreenTxt(string msg,int type, float delay = 0)
    {
        if (!gameObject.activeSelf || string.IsNullOrEmpty(msg) || screenTxtList.Count > 30)
        {
            return;
        }

        GameObject addText = NGUITools.AddChild(screenTxts.gameObject, screenTxt);
        screenTxtList.Add(addText);

        UILabel label = addText.GetComponent<UILabel>();
        label.depth = NGUITools.CalculateNextDepth(gameObject);

        label.fontSize = Random.Range(20, 26);
        if (type == 0)
        {
            label.text = "[00ff00]" + msg + "[-]";
        }
        else
        {
            label.text = "[ff0000]" + msg + "[-]";
        }


        Vector3 init = new Vector3(350, Random.Range(0, 200));

        label.transform.localPosition = init;

        addText.transform.DOLocalMove(new Vector3(-350 - label.width, init.y), (5f + Random.Range(1f, 3f)) * Time.timeScale)
            .OnComplete(() =>
            {
                screenTxtList.Remove(addText);
                Destroy(addText);
            }).SetDelay(delay);
    }
}