using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BaseAvatar : BaseMonoBehaviour
{
    public UISprite model;
    public UISprite shadow;
    public UISpriteAnimation face;

    private UIAtlas atlas;
    private UISpriteAnimation ani;
    private Action cState;

    public Action changeState;
    public string avatarName = "saberAvatar";
    public HeroInfo heroInfo = null;

    private bool _isEmotion;

    public bool isEmotion
    {
        set
        {
            _isEmotion = value;
            if (_isEmotion)
            {
                InvokeRepeating("showEmotion", 12, 12);
            }
            else
            {
                CancelInvoke("showEmotion");
            }
        }
        get { return _isEmotion; }
    }

    public enum Face
    {
        none,
        zzz,
        Love,
        Bulb,
        dian,
        Exclamationmark,
        Music,
        Chaos,
        Sweat,
        Angry,
        Questionmark
    }

    public Face faceState;

    private Face cFace;

    private string cName;

    [HideInInspector] public float bounce = -0.8f;
    [HideInInspector] public float vx = -100f;
    [HideInInspector] public float vy = -100f;

    public Action State
    {
        get { return cState; }
        set
        {
            changeState = value;
            Play();
        }
    }

    public enum Action
    {
        walk,
        stand,
        attack,
    }

    public enum Direct
    {
        Left,
        Right
    }

    public Direct changeDirect;
    protected Direct cDirect;

    public Direct nowDirect
    {
        get { return cDirect; }
        set
        {
            cDirect = value;
            showDirect();
        }
    }

    public float speed { get; set; }

    public float jumpHeight = 50;
    public float jumpUpTime = 0.25f;
    public float jumpDownTime = 0.18f;
    protected bool jumping;

    protected TweenPosition jumpUp;

    private System.Action attackBack;

    // Start is called just before any of the
    // Update methods is called the first time.


    public void Awake()
    {
        cName = avatarName;
        jumpUp = model.GetComponent<TweenPosition>();

        if (face)
        {
            face.gameObject.SetActive(false);
        }
    }

    private void showModel()
    {
        cName = avatarName;
        atlas = Res.getAvatar(cName);

        model.atlas = atlas;
        model.spriteName = cState + "0";
//        model.gameObject.AddComponent<TweenPosition>(jumpUp);
        model.name = cName;
        model.MakePixelPerfect();

        if (ani == null) ani = model.gameObject.AddComponent<UISpriteAnimation>();
        ani.framesPerSecond = 8;
        Play();
        showDirect();
    }

    private void Play()
    {
        cState = changeState;
        if (ani != null)
        {
            ani.framesPerSecond = cState == Action.attack ? 12 : 8;
            ani.loop = cState != Action.attack;
            ani.ResetToBeginning();
            ani.namePrefix = cState.ToString();
        }
    }

    public void showAttack(System.Action callback = null)
    {
        changeState = Action.attack;

        attackBack = callback;
    }

    private void showDirect()
    {
        cDirect = changeDirect;
        model.transform.localScale = cDirect == Direct.Right ? new Vector2(-1, 1) : new Vector2(1, 1);
        if (face)
        {
            face.transform.localScale = cDirect == Direct.Right ? new Vector2(-1, 1) : new Vector2(1, 1);
        }
    }

    public void Update()
    {
        if (ani == null)
        {
            return;
        }
        if (changeState != cState)
        {
            Play();
        }

        if (cName != avatarName)
        {
            showModel();
        }

        if (cDirect != changeDirect)
        {
            showDirect();
        }

        if (face != null)
        {
            if (cFace != faceState && faceState != Face.none)
            {
                cFace = faceState;
                face.namePrefix = faceState.ToString();
                face.gameObject.SetActive(true);
                face.ResetToBeginning();
                face.Play();
            }
            else if (face.gameObject.activeSelf && face.isPlaying == false)
            {
                face.gameObject.SetActive(false);
                cFace = faceState = Face.none;
            }
        }

        if (!ani.isPlaying && Action.attack == cState)
        {
            changeState = Action.stand;
            if (attackBack != null)
            {
                attackBack();
                attackBack = null;
            }
        }
    }

    public void showEmotion()
    {
        if (!_isEmotion)
        {
            InvokeRepeating("showEmotion", 5, 5);
            return;
        }

        if (face != null)
        {
            Face[] facees = Enum.GetValues(typeof (Face)) as Face[];
            if (facees != null && Random.Range(0, 100) > 50)
            {
                faceState = facees[Random.Range(0, facees.Length)];
            }
        }
    }

    public void showEmotion(Face showFace)
    {
        if (face != null)
        {
            face.gameObject.SetActive(true);
            face.ResetToBeginning();
            faceState = showFace;
        }
    }

    public void hideEmotion()
    {
        if (face != null) face.gameObject.SetActive(false);
    }

    public void showRandomEmotion()
    {
        Face[] facees = Enum.GetValues(typeof (Face)) as Face[];
        if (facees != null && Random.Range(0, 100) > 50)
        {
            faceState = facees[Random.Range(0, facees.Length)];
        }
    }

    public void show(string showAvatar)
    {
        if (showAvatar.IndexOf("Avatar", 0, StringComparison.Ordinal) == -1)
        {
            showAvatar += "Avatar";
        }
        avatarName = showAvatar;

        cState = changeState;
        cDirect = changeDirect;
        cFace = faceState = Face.none;
        showModel();
    }

    public void playEffect(UISpriteAnimation pani)
    {
        if (pani != null)
        {
            pani.gameObject.SetActive(true);
            pani.ResetToBeginning();
            pani.Play();
        }
    }

    public void showJump(Vector2 init = new Vector2(), Vector2 jumpTo = new Vector2(),System.Action callback = null)
    {
        if (jumping)
        {
            return;
        }
        if (jumpUp == null)
        {
//            return;
            jumpUp = model.GetComponent<TweenPosition>();
        }
        
        jumpUp.duration = jumpUpTime;
        jumpUp.from = init;
        jumpUp.delay = 0f;
        jumpUp.to = new Vector2(init.x, init.y + jumpHeight);
        jumpUp.ResetToBeginning();
        jumpUp.PlayForward();
        jumping = true;
        EventDelegate.Add(jumpUp.onFinished, delegate
        {
            jumpUp.duration = jumpDownTime;
            jumpUp.from = new Vector2(init.x, init.y + jumpHeight);
            jumpUp.delay = 0f;
            jumpUp.to = jumpTo;
            jumpUp.ResetToBeginning();
            jumpUp.PlayForward();
            EventDelegate.Add(jumpUp.onFinished, delegate
            {
                jumping = false;
                model.transform.localPosition = jumpTo;
                if (callback != null)
                {
                    callback();
                }
            }, true);
        }, true);

        if (shadow != null)
        {
            TweenScale sts = shadow.GetComponent<TweenScale>();
            sts.ResetToBeginning();
            sts.enabled = true;
            sts.PlayForward();
        }
    }

    public void showJumpTo(Vector2 jumpTo = new Vector2(), System.Action callback = null)
    {
        if (jumping)
        {
            return;
        }
        if (jumpUp == null)
        {
            jumpUp = model.GetComponent<TweenPosition>();
        }
        transform.DOLocalMove(jumpTo, jumpUpTime);

        model.transform.DOLocalMoveY(jumpHeight, jumpUpTime).OnComplete(() =>
        {
            model.transform.DOLocalMoveY(0, jumpDownTime).OnComplete(() =>
            {
                jumping = false;
                if (callback != null)
                {
                    callback();
                }
                model.transform.localPosition = Vector2.zero;
            });

        });
        jumping = true;
        if (shadow != null)
        {
            TweenScale sts = shadow.GetComponent<TweenScale>();
            sts.ResetToBeginning();
            sts.enabled = true;
            sts.PlayForward();
        }

//        if (jumping)
//        {
//            return;
//        }
//        if (jumpUp == null)
//        {
//            jumpUp = model.GetComponent<TweenPosition>();
//        }
//
//        Vector2 init = new Vector2();
//        jumpUp.duration = jumpUpTime;
//        jumpUp.from = init;
//        jumpUp.delay = 0f;
//        jumpUp.to = new Vector2(init.x, init.y + jumpHeight);
//        jumpUp.ResetToBeginning();
//        jumpUp.PlayForward();
//        jumping = true;
//        EventDelegate.Add(jumpUp.onFinished, delegate
//        {
//            jumpUp.duration = jumpDownTime;
//            jumpUp.from = new Vector2(init.x, init.y + jumpHeight);
//            jumpUp.delay = 0f;
//            jumpUp.to = init;
//            jumpUp.ResetToBeginning();
//            jumpUp.PlayForward();
//            EventDelegate.Add(jumpUp.onFinished, delegate
//            {
//                jumping = false;
//                if (callback != null)
//                {
//                    callback();
//                }
//                model.transform.localPosition = init;
//            }, true);
//        }, true);
//
//        if (shadow != null)
//        {
//            TweenScale sts = shadow.GetComponent<TweenScale>();
//            sts.ResetToBeginning();
//            sts.enabled = true;
//            sts.PlayForward();
//        }
//
//        TweenPosition tp = MovieUtils.getComponent<TweenPosition>(gameObject);
//        tp.duration = jumpUpTime;
//        tp.from = transform.localPosition;
//        tp.delay = 0f;
//        tp.to = jumpTo;
//        tp.ResetToBeginning();
//        tp.PlayForward();
    }


    public void showMoveTo(Vector2 moveTo = new Vector2(), System.Action callback = null,float moveTime = 0.5f)
    {
        TweenPosition tp = MovieUtils.getComponent<TweenPosition>(gameObject);
        tp.duration = moveTime;
        tp.from = transform.localPosition;
        tp.delay = 0f;
        tp.to = moveTo;
        tp.ResetToBeginning();
        tp.PlayForward();

        changeState = Action.walk;

        EventDelegate.Add(tp.onFinished, delegate
        {
            changeState = Action.stand;
            if (callback != null)
            {
                callback();
            }
        }, true);
    }

    public void showRound(float angle = 360)
    {
        TweenRotation tr = model.gameObject.GetComponent<TweenRotation>() ??
                               model.gameObject.AddComponent<TweenRotation>();
        tr.delay = 0;
        tr.duration = 0.3f * angle/360;
        tr.style = UITweener.Style.Once;
        tr.from = new Vector3(0, 0, 0);
        tr.to = new Vector3(0, 0, angle);
        tr.ResetToBeginning();
        tr.enabled = true;
        tr.onFinished.Clear();
        hideEmotion();
//        EventDelegate.Add(tr.onFinished, delegate
//        {
//            
//        }, true);
    }
}