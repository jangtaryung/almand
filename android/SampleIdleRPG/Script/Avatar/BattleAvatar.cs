
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BattleAvatar : BaseAvatar
{
    public UISpriteAnimation skillLight;
    public UISpriteAnimation skillEffect;
    public UISpriteAnimation hitTail;
    public UISpriteAnimation rushEffect;

    public GameObject skillCon;
    public GameObject arrowCon;
    public GameObject buffCon;
    public UIAtlas buffAtlas;
    public UILabel bufTxt;
    public UISprite effectSign;
    
    public UILabel effectTxt;
    public UISprite hpLine;

    public System.Action callback;
    public System.Action lightCallback;

    public bool left { get; set; }

    public BattleArmy army;

    private Vector3 standInit;
    private Vector3 scaleInit;

    private Hashtable buffList;

    private ArrayList effectList;
    private Vector3 effectInit;
    private Vector3 toPos ;

    public BattleData.Hero info;

    public void showInit()
    {
        hitTail.gameObject.SetActive(false);
        
        buffList = new Hashtable();
        hpLine.spriteName = left ? "lifebarG" : "lifebarR";
        hpLine.invert = !left;
        showHpMax();

        model.transform.localRotation = Quaternion.identity;
        
        shadow.gameObject.SetActive(true);
        skillEffect.gameObject.SetActive(false);
        skillCon.transform.parent = transform;
        skillCon.transform.localPosition = new Vector3();
        TransformUtils.removeAllChilds(buffCon);

        
        effectInit = effectSign.transform.localPosition;
    }

    public void initPosition(Vector3 pos)
    {
        scaleInit = new Vector3(1, 1, 1);
        initHero();
        standInit = pos;
        
        transform.localScale = scaleInit;
        transform.localPosition = pos;
    }

    public void initHero()
    {
        CancelInvoke();

        DOTween.Kill(model.transform);
        DOTween.Kill(model.material);

        model.color = new Color(1, 1, 1, 1);
        hpLine.color = model.color;

        DOTween.Kill(transform);

        transform.localScale = new Vector3(1, 1);
        transform.localPosition = standInit;
        model.transform.localRotation = Quaternion.identity;
        effectSign.gameObject.SetActive(false);
        if (effectList != null && effectList.Count > 0)
        {
            for (int i = 0; i < effectList.Count; i++)
            {
                GameObject obj = (GameObject)effectList[i];
                Destroy(obj);
            }
        }
        effectList = new ArrayList();
    }

    public void LateUpdate()
    {
    }

    public void showMoveOneStep(System.Action atkBack = null)
    {
        SoundUtils.PlaySFX("move");
        State = Action.walk;
        
        float mlen1 = -30;
        if (left)
        {
            mlen1 *= -1;
        }

        Vector3 init = transform.localPosition;
        transform.DOLocalMove(new Vector2(init.x + mlen1, init.y), 0.1f).OnComplete(() =>
        {
            State = Action.stand;
            if (atkBack != null)
            {
                atkBack();
            }
        });
    }

    public void showMoveToFront(float posY, System.Action atkBack = null)
    {
        SoundUtils.PlaySFX("move");
        State = Action.walk;

        transform.DOLocalMove(new Vector3(0, posY), 0.2f).OnComplete(() =>
        {
            State = Action.stand;
            if (atkBack != null)
            {
                atkBack();
            }
        });
    }

    public void showArrow(string txt, bool up, System.Action action)
    {
        SoundUtils.PlaySFX("buff");
        arrowCon.SetActive(true);

        UISprite arrow = arrowCon.transform.Find("arrow").GetComponent<UISprite>();
        arrow.spriteName = up ? "ArrowUP" : "ArrowDown";

        UILabel label = arrowCon.transform.Find("txt").GetComponent<UILabel>();
        label.text = txt;

        Vector3 init = new Vector2(0, 10);
        
        if (up)
        {
            arrow.transform.localPosition = init;
            arrow.transform.DOLocalMove(new Vector2(init.x, init.y + 25), 0.5f).OnComplete(() =>
            {
                arrowCon.SetActive(false);
                action();
            });
        }
        else
        {
            arrow.transform.localPosition = new Vector2(init.x, init.y + 25);
            arrow.transform.DOLocalMove(init, 0.5f).OnComplete(() =>
            {
                arrowCon.SetActive(false);
                action();
            });
        }
        label.transform.localScale = new Vector2(0.1f, 0.1f);
        label.transform.DOScale(Vector2.one, 0.1f);
        
        if (up)
        {
            
            label.transform.localPosition = init;
            label.transform.DOLocalMove(new Vector2(init.x, init.y + 10), 0.8f).SetDelay(0.1f);
        }
        else
        {
            label.transform.localPosition = new Vector2(init.x, init.y + 10);
            label.transform.DOLocalMove(init, 0.8f).SetDelay(0.1f);
        }
    }

    public void showEffect(buff_info binfo)
    {
        if (binfo.type == 1)
        {
            effectSign.spriteName = "paralysisword";
        }
        else if (binfo.type == 2)
        {
            effectSign.spriteName = "shockword";
        }
        else if (binfo.type == 3)
        {
            effectSign.spriteName = "vertigoword";
        }
        else if (binfo.type == 4)
        {
            effectSign.spriteName = "burningword";
        }
        else if (binfo.type == 5)
        {
            effectSign.spriteName = "burningword";
        }
        else if (binfo.type == 5)
        {
            effectSign.spriteName = "frozenword";
        }
        //2025.02.04
        //레드마인 #1988 관련수정.
        effectSign.MakePixelPerfect();
        effectTxt.gameObject.SetActive(false);
        Vector3 init = new Vector2(0, 10);
        effectSign.transform.localPosition = init;
        effectSign.transform.DOLocalMoveY(init.y + 25, 1).OnComplete(() =>
        {
            effectSign.gameObject.SetActive(false);
        });
//        effectSign.material.DOFade(0, 0.6f).SetDelay(0.6f);

    }

    public void showSkillEffect(BattleData.Effect_hero eInfo)
    {
        if (eInfo.hp != 0)
        {
            if (eInfo.hp > 0)
            {
//                effectTxt.bitmapFont = army.greenFont;
//                army.showDamage(this, eInfo.hp, effectTxt.bitmapFont);
                effectTxt.bitmapFont = army.greenFont;
                showEffectOnce("recoveryword", eInfo.hp);
            }
            else
            {
                effectTxt.bitmapFont = army.whiteFont;
                army.showDamage(this, eInfo.hp, effectTxt.bitmapFont);

                UnderAttack(null, info.hp + eInfo.hp <= 0);
            }
        }

        if (eInfo.def != 0)
        {
            if (eInfo.def > 0)
            {
                effectTxt.bitmapFont = army.silverFont;
                showEffectOnce("statedefenserise", eInfo.def);
            }
            else
            {
                effectTxt.bitmapFont = army.redFont;
                showEffectOnce("statedefensedecline", eInfo.def);
            }

        }
        if (eInfo.dodge != 0)
        {
            if (eInfo.dodge > 0)
            {
                effectTxt.bitmapFont = army.silverFont;
                showEffectOnce("statedodgerise", eInfo.dodge);
            }
            else
            {
                effectTxt.bitmapFont = army.redFont;
                showEffectOnce("statedodgedecline", eInfo.dodge);
            }
        }
        if (eInfo.tenacity != 0)
        {
            if (eInfo.tenacity > 0)
            {
                effectTxt.bitmapFont = army.silverFont;
                showEffectOnce("statetoughnessrise", eInfo.tenacity);
            }
            else
            {
                effectTxt.bitmapFont = army.redFont;
                showEffectOnce("statetoughnessdecline", eInfo.tenacity);
            }
        }
        if (eInfo.hit != 0)
        {
            if (eInfo.hit > 0)
            {
                effectTxt.bitmapFont = army.orangeFont;
                showEffectOnce("stateshot", eInfo.hit);
            }
            else
            {
                effectTxt.bitmapFont = army.redFont;
                showEffectOnce("stateshotdecline", eInfo.hit);
            }
        }

        if (eInfo.atk != 0)
        {
            if (eInfo.atk > 0)
            {
                effectTxt.bitmapFont = army.orangeFont;
                showEffectOnce("stateattackrise", eInfo.atk);
            }
            else
            {
                effectTxt.bitmapFont = army.redFont;
                showEffectOnce("stateattackdecline", eInfo.atk);
            }
        }

        if (eInfo.crit != 0)
        {
            if (eInfo.crit > 0)
            {
                effectTxt.bitmapFont = army.orangeFont;
                showEffectOnce("statecritrise", eInfo.crit);
            }
            else
            {
                effectTxt.bitmapFont = army.redFont;
                showEffectOnce("statecritdecline", eInfo.crit);
            }
        }
        
    }

    public void showAtkEffect(BattleData.To eInfo)
    {
        effectTxt.bitmapFont = army.whiteFont;
        if (eInfo.crit > 0)
        {
            effectTxt.bitmapFont = army.orangeFont;
            showEffectOnce("statecritrise");
        }
//        if (eInfo.recover > 0)
//        {
//            effectTxt.bitmapFont = army.greenFont;
//            showEffectOnce("statecritrise", eInfo.recover);
//        }
        if (eInfo.back > 0)
        {
            effectTxt.bitmapFont = army.orangeFont;
            showEffectOnce("Counterattackword", eInfo.crit);
        }
        if (eInfo.miss > 0)
        {
            effectTxt.bitmapFont = army.silverFont;
            showEffectOnce("statedodgerise");
        }
        if (eInfo.revive > 0)
        {
            initHero();
            effectTxt.bitmapFont = army.silverFont;
            showEffectOnce("resurrectionword", eInfo.hp);
            
        }
        army.showDamage(this,eInfo.damage, effectTxt.bitmapFont);
    }

    public void showEffectOnce(string filename, int count = 0)
    {
        effectSign.spriteName = filename;

        effectTxt.gameObject.SetActive(true);
        GameObject obj = NGUITools.AddChild(gameObject, effectSign.gameObject);

        obj.transform.parent = army.transform;
        int eCount = effectList.Count;
        eCount = eCount == 0 ? 1 : eCount;
        //2025.02.04
        //레드마인 #1988 관련수정.
        obj.GetComponent<UISprite>().MakePixelPerfect();
        obj.transform.localPosition = new Vector3(standInit.x, standInit.y * eCount);
        obj.gameObject.SetActive(true);
        UILabel countTxt = obj.GetComponentInChildren<UILabel>();
        countTxt.gameObject.SetActive(count != 0);
        countTxt.text = count > 0 ? "+" + count : "" + count;

        effectList.Add(obj);
        const float sTime = 0.3f;
        const float sdelayTime = 0.4f;

        const float sShow = 0.35f;
//        const float sTime = 0.15f;
//        const float sdelayTime = 0.8f;
//
//        const float sShow = 0.75f;

        obj.transform.localPosition = obj.transform.localPosition;
        obj.transform.DOLocalMoveY(obj.transform.localPosition.y + 120, sTime + sdelayTime);

        obj.transform.localScale = new Vector3(sShow, sShow);
        obj.transform.DOScale(Vector2.one, sTime).OnComplete(() =>
        {
            TweenAlpha ta = MovieUtils.getComponent<TweenAlpha>(obj);
            ta.duration = sTime;
            ta.from = 1.0f;
            ta.delay = sdelayTime;
            ta.to = 0.2f;
            ta.ResetToBeginning();
            ta.PlayForward();

            obj.transform.DOScale(new Vector2(sShow + 1, sShow + 1), sTime).OnComplete(() =>
            {
                Destroy(obj);
                effectList.Remove(obj);
            }).SetDelay(sdelayTime);
        });
    }

    public void showEffectCount(BattleData.Effect_hero binfo)
    {
        int count = 0;
        if (binfo.atk != 0)
        {
            count = binfo.atk;
            if (binfo.atk > 0)
            {
                effectTxt.bitmapFont = army.orangeFont;
                effectSign.spriteName = "stateattackrise";
            }
            else
            {
                effectTxt.bitmapFont = army.redFont;
                effectSign.spriteName = "stateattackdecline";
            }
            
        }
        else if (binfo.crit != 0)
        {
            count = binfo.crit;
            if (binfo.crit > 0)
            {
                effectTxt.bitmapFont = army.orangeFont;
                effectSign.spriteName = "statedodgerise";
            }
            else
            {
                effectTxt.bitmapFont = army.redFont;
                effectSign.spriteName = "statedodgedecline";
            }
        }
        else if (binfo.hit != 0)
        {
            count = binfo.hit;
            if (binfo.hit > 0)
            {
                effectTxt.bitmapFont = army.orangeFont;
                effectSign.spriteName = "stateshot";
            }
            else
            {
                effectTxt.bitmapFont = army.redFont;
                effectSign.spriteName = "stateshotdecline";
            }
        }

        effectTxt.gameObject.SetActive(count != 0);
        effectTxt.text = count > 0 ? "+" + count : "-" + count;

        Vector3 init = new Vector2(0, 10);

        effectSign.transform.DOLocalMoveY(init.y + 25, 1).OnComplete(() =>
        {
            effectSign.gameObject.SetActive(false);
        });

    }

    public void showAddHp(Skill_info skill)
    {
        rushEffect.transform.localScale = left ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        rushEffect.gameObject.SetActive(true);
        rushEffect.namePrefix = "hp";
        rushEffect.ResetToBeginning();
        rushEffect.Play();
        rushEffect.onFinished.Clear();
        EventDelegate.Add(rushEffect.onFinished, delegate
        {
            rushEffect.gameObject.SetActive(false);
        },true);
    }

    public void UnderAttack(Skill_info skill, bool isDead)
    {
        if (skill != null)
        {
            skillCon.transform.parent = army.transform;
            skillCon.transform.localScale = new Vector3(2f, 2f);
            skillEffect.gameObject.SetActive(true);
//            Res.showSkillEffect(skillEffect, skill != null ? skill.movie : "boow");
            
//            skill.movie = Random.RandomRange(1,100)>50?"wind":"mimi";
//            skill.movie = "wind";
//            Debuger.Log(skill.movie);
            Res.showSkillEffect(skillEffect, skill.movie);
//            skillEffect.transform.localScale = new Vector3(1.5f, 1.5f);
//            skillEffect.namePrefix = skill.movie;
            EventDelegate.Set(skillEffect.onFinished, () => { skillEffect.gameObject.SetActive(false); });
        }
        
        model.color = new Color(1, 0, 0, 1);
        TweenColor tc = TweenColor.Begin(model.gameObject, 0.7f, new Color(1, 0, 0, 1));
        tc.delay = 0;
        tc.ResetToBeginning();
        tc.PlayForward();
        EventDelegate.Add(tc.onFinished, delegate { model.color = new Color(1, 1, 1, 1); },true);

        Invoke("playHurtSound", Random.Range(0.0f, 0.4f));
        if (!isDead)
        {
            Vector2 init = transform.localPosition;
            shake(3, init);
        }
        else
        {
            showDead();
        }
    }

    private void playHurtSound()
    {
//        SoundUtils.PlaySFX(info.sound_hurt + "_" + Random.Range(1, 2));
    }

    private void shake(int count, Vector2 init, int gap = 20, float duration = 0.06f, bool backInit = true,
        System.Action shakeBack = null)
    {
        transform.DOShakePosition(duration * count).OnComplete(() =>
        {
            if (backInit)
            {
                transform.DOLocalMove(init, duration);
            }
            if (shakeBack != null)
            {
                shakeBack();
            }
        });

    }

    public void showDead()
    {
        Invoke("delayDead", 0.1f);
    }

    private void delayDead()
    {

        playHurtSound();
        removeAllBuff();
        shadow.gameObject.SetActive(false);
        model.color = new Color(1, 1, 1, 1);

        float delay = Random.Range(0.2f, 0.01f);
        
        const int difMin = 200;
        const int difMax = 300;
        float mlen1 = Random.Range(difMin, difMax) * 1.5f;
        Vector3 toVec;
        Vector3 roVec;
        if (left)
        {
//            tr.from = new Vector3(0, 0, 0);
            roVec = new Vector3(0, 0, 720);
            toVec = new Vector2(transform.localPosition.x - mlen1, transform.localPosition.y + Random.Range(difMin, difMax));
        }
        else
        {
//            tr.to = new Vector3(0, 0, 0);
            roVec = new Vector3(0, 0, -720);
            toVec = new Vector2(transform.localPosition.x + mlen1, transform.localPosition.y + Random.Range(difMin, difMax));
        }

        model.transform.localRotation = Quaternion.identity;
        model.transform.DORotate(roVec, 0.3f, RotateMode.FastBeyond360).SetDelay(delay);
        transform.DOLocalMove(toVec, Random.Range(0.6f, 0.8f)).SetDelay(delay).OnComplete(() =>
        {
            
        });
    }

    public void showSkillLight(BattleData.Action act, System.Action lightback, System.Action action)
    {
        Invoke("delayShowSkill", 0.2f);
        callback = action;
        lightCallback = lightback;
    }

    private void delayShowSkill()
    {
        skillLight.gameObject.SetActive(true);
        skillLight.ResetToBeginning();
        skillLight.Play();

        skillLight.onFinished.Clear();
        EventDelegate.Add(skillLight.onFinished, () =>
        {
            lightCallback();
        }, true);

        const float scaleMul = 2f;

        gameObject.transform.localScale = scaleInit;
        gameObject.transform.DOScale(new Vector2(scaleMul * scaleInit.x, scaleMul * scaleInit.y), 0.2f).OnComplete(
            () =>
            {
                gameObject.transform.DOScale(scaleInit, 0.2f).SetDelay(1);
            });


//        SoundUtils.PlaySFX(info.sound_attack + "_2");
        Invoke("playBaoqiSound", 0.1f);
    }

    private void playBaoqiSound()
    {
//        SoundUtils.PlaySFX("baoqi");
    }

    public void showAddHP(BattleData.Action act)
    {
        Skill_info skill = (Skill_info)Cache.SkillInfo[act.data.skillid];
        SoundUtils.PlaySFX("yiAtk");
        showTail();
        showAttack(() =>
        {
            if (callback != null)
            {
                callback();
                callback = null;
            }
            if (skill != null)
            {
                skillCon.transform.parent = army.transform;
                skillCon.transform.localScale = new Vector3(2f, 2f);
                skillEffect.gameObject.SetActive(true);
                Res.showSkillEffect(skillEffect, skill.movie);

                EventDelegate.Set(skillEffect.onFinished, () =>
                {
                    skillEffect.gameObject.SetActive(false);
                    
                });
            }
        });
    }

    public void throwBall(BattleData.Action act, int weapon_type)
    {
        Skill_info skill = (Skill_info)Cache.SkillInfo[act.data.skillid];
        SoundUtils.PlaySFX(weapon_type == 2 ? "bombAtk" : "magicAtk");

        showTail();
        showAttack(() =>
        {
            BattleData.To[] toInfo = act.toInfo;
            int len = toInfo.Length;
            for (int i = 0; i < len; i++)
            {
                BattleData.To gTo = toInfo[i];
                GameObject heroTo = army.getHeroFromAll(gTo.id);
                BattleAvatar toA = heroTo.GetComponent<BattleAvatar>();
                System.Action call = null;
                if (i == 0)
                {
                    call = callback;
                }
                string showName;
                if (weapon_type == 2)
                {
                    showName = "boweffect";
                }
                else
                {
                    if (skill != null)
                    {
                        showName = skill.start_movie;
                    }
                    else
                    {
                        showName = "magicball3";
                    }
                }
                army.battle.showBallAttack(this, toA, showName, call);
            }
        });
    }

    public void rush(BattleData.Action act,float moveY, int weapon_type)
    {
        SoundUtils.PlaySFX("move");
        State = Action.walk;
//        if (tp == null)
//        {
//            tp = GetComponent<TweenPosition>();
//        }
//        Vector2 init = transform.localPosition;
//        float mlen1 = -500;
//        float mlen2 = 840;
//        float mlen3 = 485;
        float mlen4 = -48;

//        toPos = army.transform.InverseTransformPoint(oppHero.transform.position);
//        float mlen5 = 200;

        if (weapon_type == 4)
        {
            rushEffect.gameObject.transform.localScale = left ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
            rushEffect.gameObject.SetActive(true);
            rushEffect.namePrefix = "Arush";
            rushEffect.ResetToBeginning();
            rushEffect.Play();
        }

        if (left)
        {
            mlen4 *= -1;
        }
        transform.DOLocalMove(new Vector2(mlen4 * 1f, moveY), 0.2f).OnComplete(() =>
        {
            rushEffect.gameObject.SetActive(false);
            showTail();
            SoundUtils.PlaySFX("phyAtk");
            showAttack(() =>
            {
                if (callback != null)
                {
                    callback();
                    callback = null;
                }

                transform.DOLocalMove(standInit, 0.2f).OnComplete(() =>
                {
                    State = Action.stand;
                    transform.localPosition = standInit;
                });
            });
        });

//        tp.duration = 0.2f;
//        tp.from = transform.localPosition;
//        tp.delay = 0f;
//        tp.to = new Vector2(mlen4 * 1f, moveY);
//        tp.ResetToBeginning();
//        tp.PlayForward();
//
//        EventDelegate.Add(tp.onFinished, delegate
//        {
//            rushEffect.gameObject.SetActive(false);
//            showTail();
//            showAttack(() =>
//            {
//                if (callback != null)
//                {
//                    callback();
//                    callback = null;
//                }
//
//                tp.from = transform.localPosition;
//                tp.duration = 0.2f;
//                tp.delay = 0f;
//                tp.to = standInit;
//                tp.ResetToBeginning();
//                tp.PlayForward();
//                EventDelegate.Add(tp.onFinished, delegate
//                {
//                    State = Action.stand;
//                    transform.localPosition = standInit;
//                }, true);
//            });
//        }, true);
    }

    public void back()
    {
        transform.DOLocalMove(standInit, 0.2f);
//        if (tp == null)
//        {
//            tp = GetComponent<TweenPosition>();
//        }
//        tp.duration = 0.2f;
//
//        tp.from = transform.localPosition;
//        tp.delay = 0f;
//        tp.to = standInit;
//        tp.ResetToBeginning();
//        tp.PlayForward();
    }

    private void showTail()
    {
        hitTail.gameObject.transform.localScale = left ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        hitTail.gameObject.SetActive(true);
        if (left)
        {
            hitTail.gameObject.transform.localPosition = new Vector2(0, 12);
        }
        else
        {
            hitTail.gameObject.transform.localPosition = new Vector2(0, 12);
        }
        hitTail.ResetToBeginning();
        hitTail.Play();

//        SoundUtils.PlaySFX(info.sound_attack + "_1");
        
    }

    public void addBuff(buff_info buffInfo, string buffStr, bool before = true, System.Action buffCallback = null)
    {
        showEffect(buffInfo);

        SoundUtils.PlaySFX("buff");
        int buffId = buffInfo.id;
        if (buffList.ContainsKey(buffId))
        {
            if (buffCallback != null)
            {
                buffCallback();
            }

            return;
        }

        UISprite buff = NGUITools.AddSprite(buffCon, buffAtlas, buffStr + "0");
        buff.MakePixelPerfect();
        buff.depth = before ? model.depth + 1 : model.depth - 1;
        UISpriteAnimation buffAni = buff.gameObject.AddComponent<UISpriteAnimation>();

        buffAni.loop = true;
        buffAni.namePrefix = buffStr;
        buffAni.framesPerSecond = 12;

        buffList.Add(buffId, buff.gameObject);
        UILabel label = NGUITools.AddChild(buff.gameObject, bufTxt.gameObject).GetComponent<UILabel>();
        string color = buffInfo.buff == 1 ? "ff0000" : "5E07E1";
        label.text = "[" + color + "]" + buffInfo.name + "[-]";

        label.transform.localPosition = Vector2.zero;
        label.transform.DOLocalMoveY(50,0.5f).OnComplete(() =>
        {
            if (buffCallback != null)
            {
                buffCallback();
            }
            Destroy(label.gameObject);
        });
        label.depth = buff.depth + 1;

        
    }

    public void removeBuff(int buffId, System.Action buffCallback = null)
    {
        if (buffList.ContainsKey(buffId))
        {
            GameObject buff = (GameObject) buffList[buffId];
            if (buffCallback != null)
            {
                buffCallback();
            }
            buffList.Remove(buffId);
            if (buff == null)
            {
                return;
            }

            TweenAlpha ta = buff.AddMissingComponent<TweenAlpha>();
            ta.duration = 0.1f;
            ta.from = 1;
            ta.to = 0;
            ta.ResetToBeginning();
            ta.PlayForward();
            EventDelegate.Add(ta.onFinished, () =>
            {
                Destroy(buff);
            }, true);
        }
        else
        {
            if (buffCallback != null)
            {
                buffCallback();
            }
        }
    }

    private void removeAllBuff()
    {
        if (buffList == null)
        {
            return;
        }
        foreach (DictionaryEntry entry in buffList)
        {
            GameObject buff = (GameObject) entry.Value;
            Destroy(buff);
        }
    }

    public void OnDestroy()
    {
        Destroy(skillEffect.gameObject);
    }

    public void showHP(int hp)
    {
        info.hp = hp;
        if (info.hp > info.hp_max)
        {
            info.hp_max = info.hp;
        }
        float hpPer = (float)info.hp / info.hp_max;
        hpPer = hpPer > 1 ? 1 : hpPer;
        hpLine.fillAmount = hpPer;
    }

    public void showHpMax()
    {
        hpLine.fillAmount = 1;
    }

    public void showBlack()
    {
        TweenColor tc = TweenColor.Begin(model.gameObject, 0.2f, new Color(0.25f, 0.25f, 0.25f, 1f));
        tc.delay = 0;
        tc.ResetToBeginning();
        tc.PlayForward();
        tc.onFinished.Clear();
        hpLine.color = model.color;
        EventDelegate.Add(tc.onFinished, delegate
        {
            hpLine.color = model.color;
            tc = TweenColor.Begin(model.gameObject, 0.2f, new Color(1, 1, 1, 1));
            tc.delay = 1.2f;
            tc.ResetToBeginning();
            tc.PlayForward();
            tc.onFinished.Clear();
            EventDelegate.Add(tc.onFinished, delegate
            {
                model.color = new Color(1, 1, 1, 1);
                hpLine.color = model.color;
            },true);
        }, true);
    }
}