using UnityEngine;

public class SkillShow : BaseMonoBehaviour
{
    public UILabel heroNameTxt;
    public UILabel skillNameTxt;

    public GameObject bg;


    public void show(BattleData.Action act, System.Action callback = null)
    {
        gameObject.SetActive(true);
        TweenPosition tp = heroNameTxt.gameObject.GetComponent<TweenPosition>();
        Skill_info skill = (Skill_info)Cache.SkillInfo[act.data.skillid];
        skillNameTxt.text = skill.name;

        heroNameTxt.text = Data.battle.getHeroInfo(act.from).name;

        const float delayTime = 2.0f;
        Vector3 init = heroNameTxt.gameObject.transform.localPosition;
        tp.duration = 0.2f;
        tp.from = new Vector2(-700, init.y);
        tp.delay = 0f;
        tp.to = new Vector2(0, init.y);
        tp.ResetToBeginning();
        tp.PlayForward();

        EventDelegate.Add(tp.onFinished, delegate
        {
            tp.duration = 0.2f;
            tp.from = tp.to;
            tp.delay = delayTime;
            tp.to = new Vector2(700, init.y);
            tp.ResetToBeginning();
            tp.PlayForward();

            if (callback != null)
            {
                callback();
            }

            EventDelegate.Add(tp.onFinished, delegate
            {
                heroNameTxt.gameObject.transform.localPosition = new Vector2(-700, init.y);
            }, true);
        }, true);

        bg.transform.localScale = new Vector2(1, 0);
        TweenScale ts = bg.GetComponent<TweenScale>();
        ts.duration = 0.2f;
        ts.from = new Vector2(1, 0);
        ts.delay = 0f;
        ts.to = new Vector2(1, 1);
        ts.ResetToBeginning();
        ts.PlayForward();

        EventDelegate.Add(ts.onFinished, delegate
        {
            ts.duration = 0.2f;
            ts.from = new Vector2(1, 1);
            ts.delay = delayTime;
            ts.to = new Vector2(1, 0);
            ts.ResetToBeginning();
            ts.PlayForward();

            EventDelegate.Add(ts.onFinished, delegate
            {
//                gameObject.SetActive(false);
            }, true);
        }, true);
    }
}