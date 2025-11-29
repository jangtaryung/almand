using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapManager
{
    /// <summary>
    /// VARIABLES......
    /// </summary>
    
    #region VARIABLES......

    public static MapManager Instance;

    private const int MAX_H = 10;
    private const int MAX_W = 10;
    private const float m_fMAXScale = 1.0f;

    private Dictionary<int, Map> m_cMapList;
    private Transform m_tRootTrans;

    #endregion


    /// <summary>
    /// BASE FUNCTIONS......
    /// </summary>
    
    #region BASE FUNCTIONS......

    #endregion


    /// <summary>
    /// PUBLIC FUNCTIONS......
    /// </summary>

    #region PUBLIC FUNCTIONS......

    public void Initialize(Transform rootT)
    {
        Instance = this;

        m_cMapList = new Dictionary<int, Map>();
        
        m_tRootTrans = rootT;

        reset();

        init();

    }


    public Vector4 GetBounds()
    {
        return new Vector4(0, 0, (MAX_W - 1), (MAX_H - 1));
    }


    public Vector2 GetMapPos(int mapIdx)
    {
        if (mapIdx < 0)
            return Vector2.zero;

        if (mapIdx >= m_cMapList.Count)
            return Vector2.zero;

        Map m = m_cMapList[mapIdx];
        if(null != m)
            return new Vector2(m.POS.x, m.POS.z);

        return Vector2.zero;

    }


    public Vector2[] GetBoundsPos()
    {
        Vector2[] boundPos = new Vector2[4];
        boundPos[0] = GetMapPos(0);                         
        boundPos[1] = GetMapPos((MAX_W - 1));
        boundPos[2] = GetMapPos((MAX_W * (MAX_H - 1)));
        boundPos[3] = GetMapPos(((MAX_W * MAX_H) - 1));

        //2 3
        //0 1

        return boundPos;
    }


    public Map GetMap(int mapIdx)
    {
        if (mapIdx < 0)
            return null;

        if (mapIdx >= m_cMapList.Count)
            return null;

        Map m = m_cMapList[mapIdx];
        if (null != m)
            return m;

        return null;

    }


    #endregion


    /// <summary>
    /// PRIVATE FUNCTIONS......
    /// </summary>
    
    #region PRIVATE FUNCTIONS......

    private void init()
    {
        createMap();

        loadMap();
    }

    private void createMap()
    {
        if (null == m_cMapList)
            return;

        cleanMap();

        int cnt = 0;
        GameObject tmpObj = (GameObject)Resources.Load("Prefab/Map") as GameObject;
        
        for (int i = 0; i < MAX_H; i++) 
        {
            for(int j = 0; j < MAX_W; j++)
            {
                GameObject obj = GameObject.Instantiate(tmpObj);
                if (null != obj)
                {
                    obj.transform.localPosition = new Vector3(((j - (MAX_W / 2)) * m_fMAXScale), 0, ((i - (MAX_H / 2)) * m_fMAXScale));
                    obj.transform.localRotation = Quaternion.identity;
                    obj.transform.localScale = new Vector3(m_fMAXScale, m_fMAXScale, m_fMAXScale);
                    obj.gameObject.name = string.Format("{0}_M_{1}:{2}", cnt, j, i);
                    obj.transform.SetParent(m_tRootTrans);
                }

                m_cMapList.Add(cnt, new Map(cnt, obj));
                cnt++;
                obj = null;
            }
        }

        tmpObj = null;
    }

    private void loadMap()
    {
        if (null == m_cMapList)
            return;
        
        foreach (KeyValuePair<int, Map> m in m_cMapList)
        {
            m.Value.SetMaterial(m.Key);
        }
    }

    private void cleanMap()
    {
        GameObject obj = null;

        while (true)
        {
            if(m_tRootTrans.transform.childCount <= 0)
            {
                break;
            }

            obj = m_tRootTrans.transform.GetChild(0).gameObject;
            
            if(null != obj)
                GameObject.DestroyImmediate(obj);
        }


        if (null != m_cMapList)
            m_cMapList.Clear();

    }

    private void reset()
    {
        
    }

    #endregion


}
