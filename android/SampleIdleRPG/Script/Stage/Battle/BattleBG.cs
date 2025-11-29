using UnityEngine;

public class BattleBG : BaseMonoBehaviour
{
    private string mapName = "";

    public TweenPosition tw;

    public delegate void Callback();

    public GameObject backGound;

    public BattleArmy army;

    private string nameFarfar = "";
    private string nameFar = "";
    private string nameMiddle = "";

    public void OnEnable()
    {
        if (!Data.adventure.Init)
        {
            return;
        }
        mapName = Data.adventure.nowMain.scene;
        if (string.IsNullOrEmpty(mapName))
        {
            mapName = "1_far,1_farfar,1_middle";
        }

        string[] mapStrArr = StringUtil.Split(mapName, ",");
        for (int i = 0; i < mapStrArr.Length; i++)
        {
            string[] tempStr = StringUtil.Split(mapStrArr[i], "_");

            switch (tempStr[1])
            {
                case AdventrueMovie.farfar:
                    nameFarfar = mapStrArr[i];
                    break;
                case AdventrueMovie.far:
                    nameFar = mapStrArr[i];
                    break;
                case AdventrueMovie.middle:
                    nameMiddle = mapStrArr[i];
                    break;
            }
        }

        TransformUtils.removeAllChilds(backGound);

        loadBackGround(nameFarfar, 1);
        loadBackGround(nameFar, 2);
        loadBackGround(nameMiddle, 3);
        
//        show();
    }

    public void show(EventDelegate callback = null)
    {
        tw = GetComponent<TweenPosition>();
        tw.ResetToBeginning();
        tw.PlayForward();
        if (callback != null)
        {
            EventDelegate.Add(tw.onFinished, callback);
        }
        
    }

    private void loadBackGround(string mapStr, int depth)
    {
//        Texture2D texture2d = Res.getBackGround(mapStr);
//
//        if (texture2d == null)
//        {
//            return ;
//        }
//
//        UITexture texture = NGUITools.AddWidget<UITexture>(backGound);
//        texture.name = mapStr + "1";
//        texture.pivot = UIWidget.Pivot.Left;
//        texture.depth = depth;
//
//        texture.mainTexture = texture2d;
//        texture.MakePixelPerfect();

    }
}
