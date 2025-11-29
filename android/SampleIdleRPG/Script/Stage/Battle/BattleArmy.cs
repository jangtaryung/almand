using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BattleArmy : BaseMonoBehaviour
{
    private static string[] buffFoot = {"Storm"};

    public Battle battle;
//    public GameObject heroPrefab;

    public Hashtable addedMonsters;
    public Hashtable allHeros;

    public delegate void CallBack();


    public bool left;

    public UILabel hpTxt;

    public UIFont whiteFont;
    public UIFont greenFont;
    public UIFont orangeFont;
    public UIFont redFont;
    public UIFont silverFont;


    public UISprite effectSign;
    public UILabel effectTxt;

    private ArrayList hpList;
    private Hashtable initList;

    public void Awake()
    {
        if (hpTxt)
        {
            hpList = new ArrayList();
            hpTxt.gameObject.SetActive(false);
            effectSign.gameObject.SetActive(false);

            addedMonsters = new Hashtable();
            initList = new Hashtable();
            for (int i = 1; i <= 15; i++)
            {
                GameObject go = transform.Find("BattleAvatar" + i).gameObject;
                Vector3 pos = go.transform.localPosition;

                initList.Add(i, pos);
                addedMonsters.Add(i, go);
            }
        }
    }

    public void OnEnable()
    {
//        show();
    }

    public void show(Hashtable heroArr, bool isLeft)
    {
        if (hpList != null && hpList.Count > 0)
        {
            for (int i = 0; i < hpList.Count; i++)
            {
                UILabel showHp = (UILabel) hpList[i];
                Destroy(showHp.gameObject);
            }
            hpList.Clear();
        }

        left = isLeft;
        TweenAlpha.Begin(gameObject, 0.1f, 1);
        
        gameObject.SetActive(true);
        hpTxt.gameObject.SetActive(false);

//        TransformUtils.removeAllChilds(gameObject);
        if (addedMonsters != null)
        {
            foreach (DictionaryEntry entry in addedMonsters)
            {
                GameObject ahero = (GameObject) entry.Value;
                ahero.SetActive(false);
            }
        }
//        heroArr = new Hashtable();
//        for (int i = 0; i < Random.Range(15, 20); i++)
//        {
//            BattleData.Hero ttt = new BattleData.Hero();
//            ttt.index = i;
//            ttt.mod = "saber";
//            heroArr.Add(i, ttt);
//        }
        
        int nowCount = 1;

        allHeros = new Hashtable();
        foreach (DictionaryEntry entry in heroArr)
        {
            BattleData.Hero heroInfo = (BattleData.Hero) entry.Value;
            BattleAvatar hero = creatHeros(heroInfo.pos, heroInfo.mod);
            allHeros.Add(heroInfo.index,hero);
            hero.info = heroInfo;
            if (hero)
            {
                Vector3 pos = (Vector3) initList[heroInfo.pos];
                hero.initPosition(pos);

                UISprite u2d = hero.model;
                int def = pos.y > 0 ? -5 : 5;
                def = nowCount == 1 ? def - 2 : def;
                u2d.depth = 50 - nowCount + def;
                u2d = hero.face.gameObject.GetComponent<UISprite>();
                u2d.depth = 50 - nowCount + def + 1;
                u2d = hero.shadow;
                u2d.depth = 40 - nowCount;
                u2d = hero.skillLight.gameObject.GetComponent<UISprite>();
                u2d.depth = 60 - nowCount;
                nowCount ++;
            }
            else
            {
                Debuger.Log(hero);
            }
        }
    }

    private BattleAvatar creatHeros(int id, string monName)
    {
        GameObject hero = (GameObject) addedMonsters[id];
        if (hero != null)
        {
            hero.SetActive(true);
            BattleAvatar tempS = hero.GetComponent<BattleAvatar>();
            if (tempS)
            {
                tempS.name = "BattleAvatar" + id;
                tempS.show(monName);
                tempS.changeState = BaseAvatar.Action.stand;
                tempS.changeDirect = left ? BaseAvatar.Direct.Right : BaseAvatar.Direct.Left;
                tempS.left = left;
                tempS.army = this;
                tempS.showInit();

                return tempS;
            }
        }
        return null;
    }

    public void showAct(BattleData.Action act, ArrayList tarigger, CallBack callback)
    {
        switch (act.type)
        {
            case BattleData.ACT_NORMAL_ATTACK:
                showNormalAttack(act, tarigger, callback);
                break;
            case BattleData.ACT_SKILL_ATTACK:
                showSkill(act, tarigger, callback);
                break;
            case BattleData.ACT_ADD_BUFF:
//                print("용감한 버프 추가");
                addBuff(act, callback);
                break;
            case BattleData.ACT_DEL_BUFF:
                //                print("용감한 버프 제거");
                removeBuff(act, callback);
                break;
            case BattleData.ACT_TRIGGER_SKILL:
                //                print("패시브 스킬 발동");
                showArrow(act, callback);
                break;
        }
    }

    public void showSkill(BattleData.Action act, ArrayList tarigger, CallBack callback)
    {
        if (act.side != 0)
        {
//            battle.skillShow1.show(act);
            
            showNormalAttack(act, tarigger, callback);
        }
        else
        {
//            battle.skillShow2.show(act);
            showNormalAttack(act, tarigger, callback);
        }
    }


    public void showNormalAttack(BattleData.Action act, ArrayList tarigger, CallBack callback)
    {
        GameObject hero = getHeroFromAll(act.from);
        BattleAvatar avatar = hero.GetComponent<BattleAvatar>();

        BattleData.To[] toInfo = act.toInfo;
        int len = toInfo.Length;
        bool isCrit = false;
        float moveY = 500;
        BattleAvatar[] toA = new BattleAvatar[len];
        for (int i = 0; i < len; i++)
        {
            BattleData.To gTo = toInfo[i];
            GameObject heroTo = getHeroFromAll(gTo.id);
            toA[i] = heroTo.GetComponent<BattleAvatar>();

            if (gTo.crit == 1)
            {
                isCrit = true;
            }
            if (heroTo.transform.localPosition.y < moveY)
            {
                moveY = heroTo.transform.localPosition.y;
            }
        }
        battle.skillTalkShow1.show(hero, act);

        showUnderEffect(isCrit, act);

        //검게 변하다
        battle.army1.showBlack(avatar, toA);
        battle.army2.showBlack(avatar, toA);

        //빛을 외쳐라
        avatar.showSkillLight(act, () =>
        {
            //1=암살자, 2=궁수, 3=마법사, 4=전사, 5=치유사
            if (avatar.info.weapon_type == 1)
            {
                avatar.rush(act,moveY, avatar.info.weapon_type);
            }
            else if (avatar.info.weapon_type == 2)
            {
                avatar.throwBall(act, avatar.info.weapon_type);
            }
            else if (avatar.info.weapon_type == 3)
            {
                avatar.throwBall(act, avatar.info.weapon_type);
            }
            else if (avatar.info.weapon_type == 4)
            {
                avatar.rush(act, moveY, avatar.info.weapon_type);
            }
            else if (avatar.info.weapon_type == 5)
            {
                avatar.showAddHP(act);
            }
        },
            () =>
            {
                if (tarigger.Count > 0)
                {
                    //                        print("패시브 트리거");
                    for (int i = 0; i < tarigger.Count; i++)
                    {
                        showArrow((BattleData.Action)tarigger[i], null);
                    }
                }
                battle.hideEffect();
                showEffect(act);
                //                    showBack(avatar);
                if (callback != null) callback();
            });
    }

    

    private void showBack(BattleAvatar except)
    {
        foreach (DictionaryEntry entry in addedMonsters)
        {
            GameObject ahero = (GameObject) entry.Value;
            BattleAvatar aa = ahero.GetComponent<BattleAvatar>();
            if (except != aa)
            {
                aa.back();
            }
        }
    }


    public void showState(BaseAvatar.Action action)
    {
        foreach (DictionaryEntry entry in addedMonsters)
        {
            GameObject ahero = (GameObject) entry.Value;
            BattleAvatar aa = ahero.GetComponent<BattleAvatar>();
            aa.changeState = action;
        }
        //        showUnderEffect(act);
    }

    private void addBuff(BattleData.Action act, CallBack callback)
    {
        buff_info buff = (buff_info) Cache.BuffInfo[act.data.buffid];

        GameObject hero = getHeroFromAll(act.from);
        BattleAvatar avatar = hero.GetComponent<BattleAvatar>();
        if (string.IsNullOrEmpty(buff.movie))
        {
            Debug.Log("buff movie " + act.data.buffid + "오류를 입력하세요");
            buff.movie = "Storm";
        }
        avatar.addBuff(buff, buff.movie, getIsBefore(buff.movie), delegate { if (callback != null) callback(); });
    }

    private void removeBuff(BattleData.Action act, CallBack callback)
    {
        GameObject hero = getHeroFromAll(act.from);
        BattleAvatar avatar = hero.GetComponent<BattleAvatar>();
        avatar.removeBuff(act.data.buffid, delegate { if (callback != null) callback(); });
    }

    private void addTeamBuff(BattleData.Action act, CallBack callback)
    {
        buff_info buff = (buff_info) Cache.BuffInfo[act.data.buffid];

        int count = 0;
        foreach (DictionaryEntry entry in addedMonsters)
        {
            count++;
            GameObject hero = (GameObject) entry.Value;
            BattleAvatar avatar = hero.GetComponent<BattleAvatar>();

            if (string.IsNullOrEmpty(buff.movie))
            {
                Debug.Log("buff movie " + act.data.buffid + "오류를 입력하세요");
                buff.movie = "Storm";
            }

            if (count == addedMonsters.Count)
            {
                avatar.addBuff(buff, buff.movie, getIsBefore(buff.movie), delegate { if (callback != null) callback(); });
            }
            else
            {
                avatar.addBuff(buff, buff.movie, getIsBefore(buff.movie));
            }
        }
    }

    private bool getIsBefore(string str)
    {
        int len = buffFoot.Length;
        for (int i = 0; i < len; i++)
        {
            if (str == buffFoot[i])
            {
                return false;
            }
        }
        return true;
    }

    private void removeTeamBuff(BattleData.Action act, CallBack callback)
    {
        int count = 0;
        foreach (DictionaryEntry entry in addedMonsters)
        {
            count++;
            GameObject hero = (GameObject) entry.Value;
            BattleAvatar avatar = hero.GetComponent<BattleAvatar>();

            if (count == addedMonsters.Count)
            {
                avatar.removeBuff(act.data.buffid, delegate { if (callback != null) callback(); });
            }
            else
            {
                avatar.removeBuff(act.data.buffid);
            }
        }
    }

    public void showBlack(BattleAvatar[] toA)
    {
        foreach (DictionaryEntry entry in addedMonsters)
        {
            GameObject hero = (GameObject)entry.Value;
            BattleAvatar avatar = hero.GetComponent<BattleAvatar>();
            int count = 0;
            for (int i = 0; i < toA.Length; i++)
            {
                if (avatar != toA[i])
                {
                    count++;
                }
            }
            if (count == toA.Length)
            {
                avatar.showBlack();
            }
        }
    }

    private void showBlack(BattleAvatar showAvatar, BattleAvatar[] toA)
    {
        foreach (DictionaryEntry entry in addedMonsters)
        {
            GameObject hero = (GameObject)entry.Value;
            BattleAvatar avatar = hero.GetComponent<BattleAvatar>();
            int count = 0;
            for (int i = 0; i < toA.Length; i++)
            {
                if (avatar != toA[i])
                {
                    count++;
                }
            }

            if (count == toA.Length && avatar != showAvatar)
            {
                avatar.showBlack();
            }
        }
    }

    public void showArrow(BattleData.Action act, CallBack callback)
    {
        Skill_info skill = (Skill_info)Cache.SkillInfo[act.data.skillid];

        GameObject hero = getHeroFromAll(act.from);
        BattleAvatar avatar = hero.GetComponent<BattleAvatar>();
        avatar.showArrow(skill.name, true, delegate { if (callback != null) callback(); });
    }

    private void showUnderEffect(bool isCrit, Skill_info skill)
    {
        if (skill == null)
        {
            return;
        }
        if (!string.IsNullOrEmpty(skill.effect_icon))
        {
            battle.showEffect(isCrit);
        }
    }

    private void showUnderEffect(bool isCrit, BattleData.Action act)
    {
        Skill_info skill = (Skill_info)Cache.SkillInfo[act.data.skillid];
        showUnderEffect(isCrit,skill);
    }

    public GameObject getHeroFromAll(int index)
    {
        BattleAvatar avatar = (BattleAvatar)battle.army1.allHeros[index];

        if (avatar)
        {
            var hero = avatar.gameObject;
            return hero;
        }
        avatar = (BattleAvatar)battle.army2.allHeros[index];
        if (avatar)
        {
            var hero = avatar.gameObject;
            return hero;
        }
        Debug.Log("영웅을 찾을 수 없습니다 index->" + index);
        return null;
    }

    public Hashtable getOppsiteMonster()
    {
        return left ? battle.army2.addedMonsters : battle.army1.addedMonsters;
    }

    public BattleArmy getOppsiteArmy()
    {
        return left ? battle.army2 : battle.army1;
    }

    public void showDamage(BattleAvatar avatar,int hpValue, UIFont font = null)
    {
        if (hpValue == 0)
        {
            return;
        }

        UILabel showHp = NGUITools.AddChild(gameObject, hpTxt.gameObject).GetComponent<UILabel>();
        hpList.Add(showHp);
        if (font == null)
        {
            font = whiteFont;
        }
        showHp.bitmapFont = font;
        if (hpValue > 0)
        {
            showHp.text = "+ " + hpValue;
            showHp.bitmapFont = greenFont;
        }
        else
        {
            showHp.text = "" + hpValue;
        }
        showHp.gameObject.SetActive(true);

        const float sTime = 0.15f;
        const float sdelayTime = 1f;

        const float sShow = 0.75f;

        showHp.transform.localPosition = avatar.transform.localPosition;
        showHp.transform.DOLocalMoveY(showHp.transform.localPosition.y + 80, sTime + sdelayTime);

        showHp.transform.localScale = new Vector3(sShow, sShow);
        showHp.transform.DOScale(Vector2.one, sTime).OnComplete(() =>
        {
//            showHp.material.DOFade(0.2f, sTime).SetDelay(sdelayTime);
            showHp.transform.DOScale(new Vector2(sShow + 1, sShow + 1), sTime).SetDelay(sdelayTime).OnComplete(() =>
            {
                showHp.gameObject.SetActive(false);
                hpList.Remove(showHp);
                Destroy(showHp.gameObject);
            });
        });
    }

    private void showEffect(BattleData.Action act)
    {
//        if (act.data.crit > 0)
//        {
//            showEffectOnce("critword");
//        }
//        if (act.data.miss > 0)
//        {
//            showEffectOnce("missword");
//        }
        if (act.data.suck > 0)
        {
            GameObject go = getHeroFromAll(act.from);
            BattleAvatar avatar = go.GetComponent<BattleAvatar>();
            avatar.showEffectOnce("suckbloodword", act.data.suck);
            avatar.showHP(avatar.info.hp + act.data.suck);
            showDamage(avatar, act.data.suck);
        }
//        if (act.data.back > 0)
//        {
//            showEffectOnce("Counterattackword");
//        }
    }

    public void showDamage(BattleAvatar avatar, BattleData.Action act)
    {
        int hpValue = act.data.hp;
        if (hpValue == 0)
        {
            return;
        }
        showDamage(avatar, hpValue);
    }
}