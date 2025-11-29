
using UnityEngine;

// ReSharper disable CompareOfFloatsByEqualityOperator
public class CityAvatar : BaseAvatar
{
    private const float downTime = 0.5f;
    private const float walkTime = 1.5f;
    private const float initX = 400;
    private const float jumpX = 60;

    public UILabel talkTxt;
    public UILabel nameTxt;
    public UISprite talkBg;

    public void showFallDown(Vector2 from, Vector2 to, bool left, System.Action callback)
    {
        jumpHeight = 60;
        jumpUpTime = 0.2f;
        jumpDownTime = 0.15f;

        int dir = left ? -1 : 1;
        const float fallY = -15;
        TweenRotation tr = model.gameObject.GetComponent<TweenRotation>() ??
                           model.gameObject.AddComponent<TweenRotation>();
        State = Action.stand;
        tr.delay = jumpUpTime - 0.1f;
        tr.duration = jumpDownTime;
        tr.style = UITweener.Style.Once;
        tr.from = new Vector3(0, 0, 0);
        tr.to = new Vector3(0, 0, 90*dir);
        tr.enabled = true;
        tr.ResetToBeginning();
        tr.PlayForward();

        TweenPosition tp = gameObject.GetComponent<TweenPosition>() ??
                           gameObject.AddComponent<TweenPosition>();
        tp.duration = jumpUpTime;
        tp.from = from;
        tp.delay = 0;
        tp.to = to;
        tp.ResetToBeginning();
        tp.PlayForward();

        showJump(new Vector2(), new Vector2(0, fallY), () =>
        {
            float moveLen = -50;
            if (left)
            {
                moveLen *= -1;
            }

            Vector3 init = transform.localPosition;
            tp.duration = 0.4f;
            tp.from = init;
            tp.delay = 0;
            tp.to = new Vector2(transform.localPosition.x + moveLen, transform.localPosition.y);
            tp.enabled = true;
            tp.ResetToBeginning();
            tp.PlayForward();

            showSmallJump(() =>
            {
                tr.delay = downTime;
                tr.duration = 0.3f;
                tr.style = UITweener.Style.Once;
                tr.from = new Vector3(0, 0, 90*dir);
                tr.to = new Vector3(0, 0, 90*dir);
                tr.ResetToBeginning();
                tr.PlayForward();
                EventDelegate.Add(tr.onFinished, () =>
                {
                    jumpHeight = 90;
                    jumpUpTime = 0.15f;
                    jumpDownTime = 0.1f;
                    showJump(new Vector2(0, fallY), new Vector2(), callback);
                    tr.delay = 0;
                    tr.duration = 0.2f;
                    tr.style = UITweener.Style.Once;
                    tr.from = tr.to;
                    tr.to = new Vector3(0, 0, 0);
                    tr.ResetToBeginning();
                    tr.PlayForward();
                }, true);
            });
        });
    }

    private void showSmallJump(System.Action callback)
    {
        jumpHeight = 8;
        jumpUpTime = 0.15f;
        jumpDownTime = 0.05f;

        const float fallY = -15;
        showJump(new Vector2(0, fallY), new Vector2(0, fallY), () =>
        {
            jumpHeight = jumpHeight/2;
            showJump(new Vector2(0, fallY), new Vector2(0, fallY), callback);
        });
    }

    public void showMoveFallDown(float delay, bool left, float fromY, System.Action callback, float runTime = 0,
        float fromX = 0, float toX = 0,float fallWidth = 0)
    {
        TweenPosition tp = gameObject.GetComponent<TweenPosition>() ??
                           gameObject.AddComponent<TweenPosition>();

        tp.ignoreTimeScale = false;
        float initNow = fromX == 0 ? initX : fromX;
        float jumpXNow = toX == 0 ? jumpX - 5 : toX;
        if (left)
        {
            initNow *= -1;
            jumpXNow *= -1;
            changeDirect = Direct.Right;
        }
        else
        {
            changeDirect = Direct.Left;
        }

        Vector3 init = new Vector2(initNow, fromY);
        tp.duration = runTime == 0 ? walkTime : runTime;
        tp.from = init;
        tp.delay = delay;
        tp.to = new Vector2(jumpXNow, init.y);
        tp.enabled = true;
        tp.ResetToBeginning();
        tp.PlayForward();

        State = Action.walk;
        EventDelegate.Add(tp.onFinished, () =>
        {
            //            hero.State = BaseAvatar.Action.stand;
            float fallOut = fallWidth == 0 ? -jumpXNow : jumpXNow + fallWidth;
            showFallDown(tp.to, new Vector2(fallOut, init.y), left, () =>
            {
                if (callback != null)
                {
                    callback();
                }
            });
        }, true);
    }

    public void showOutScreen(bool left, float fromY, System.Action callback)
    {
        TweenPosition tp = MovieUtils.getComponent<TweenPosition>(gameObject);
        float initNow = initX;
        if (left)
        {
            initNow *= -1;
            changeDirect = Direct.Right;
        }
        else
        {
            changeDirect = Direct.Left;
        }
        Vector3 init = new Vector2(initNow, fromY);
        State = Action.walk;
        tp.duration = walkTime;
        tp.from = tp.to;
        tp.delay = 0;
        tp.to = new Vector2(-initNow, init.y);
        tp.ResetToBeginning();
        tp.PlayForward();
        EventDelegate.Add(tp.onFinished, () =>
        {
            if (callback != null)
            {
                callback();
            }
        }, true);
    }


    public void jumpStone(float delay, bool left, float fromY, System.Action callback)
    {
        TweenPosition tp = gameObject.GetComponent<TweenPosition>() ??
                           gameObject.AddComponent<TweenPosition>();
        tp.ignoreTimeScale = false;

        float initNow = initX;
        float jumpXNow = jumpX;
        if (left)
        {
            initNow *= -1;
            jumpXNow *= -1;
            changeDirect = Direct.Right;
        }
        else
        {
            changeDirect = Direct.Left;
        }

        Vector3 init = new Vector2(initNow, fromY);
        tp.duration = walkTime;
        tp.from = init;
        tp.delay = delay;
        tp.to = new Vector2(jumpXNow, init.y);
        tp.enabled = true;
        tp.ResetToBeginning();
        tp.PlayForward();

        State = Action.walk;
        EventDelegate.Add(tp.onFinished, () =>
        {
            //            hero.State = BaseAvatar.Action.stand;
            jumpHeight = 65;
            jumpUpTime = 0.3f;
            jumpDownTime = 0.2f;
            showJump();

            tp.duration = jumpUpTime;
            tp.from = tp.to;
            tp.delay = 0;
            tp.to = new Vector2(-jumpXNow, init.y);
            tp.ResetToBeginning();
            tp.PlayForward();

            EventDelegate.Add(tp.onFinished, () =>
            {
                tp.duration = walkTime;
                tp.from = tp.to;
                tp.delay = 0;
                tp.to = new Vector2(-initNow, init.y);
                tp.ResetToBeginning();
                tp.PlayForward();
                EventDelegate.Add(tp.onFinished, () =>
                {
                    if (callback != null)
                    {
                        callback();
                    }
                }, true);
            }, true);
        }, true);
    }

    private int pCount;
    private string msgStr;
    public void showTalkMsg(string msg)
    {
        
//        talkTxt.transform.localScale = changeDirect == Direct.Right ? new Vector2(1, 1) : new Vector2(-1, 1);
        msgStr = msg;
        talkTxt.text = msg+"...";
        nameTxt.gameObject.SetActive(false);
        
        talkTxt.gameObject.SetActive(true);
        talkBg.gameObject.SetActive(true);
        hideEmotion();
        InvokeRepeating("showTalkPoint", 1, 1);
    }

    private void showTalkPoint()
    {
        pCount = pCount % 3;
        pCount++;
        string addStr = "";
        for (int i = 0; i < pCount; i++)
        {
            addStr += ".";
        }
        talkTxt.text = msgStr + addStr;
    }

    public void lookLeftAndRight(System.Action callback, int count = 2, float delayTime = 0.7f)
    {
        changeDirect = cDirect == Direct.Right ? Direct.Left : Direct.Right;
        changeState = Action.stand;
        MovieUtils.delayCall(gameObject, delayTime, () =>
        {
            if (count > 0)
            {
                count--;
                lookLeftAndRight(callback, count, delayTime);
            }
            else
            {
                if (callback != null)
                {
                    callback();
                }
            }
        });
    }
}