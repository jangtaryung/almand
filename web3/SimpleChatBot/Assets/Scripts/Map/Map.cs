using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Map
{
    /// <summary>
    /// VARIABLES......
    /// </summary>
    
    #region VARIABLES......

    private int m_idx;
    private GameObject m_gObj;

    private const string m_strBaseMaterialPath = "Assets/Cartoon_Texture_Pack/";
    private int m_mType;        //materialType;

    public Vector3 POS
    {
        get
        {
            if (null == m_gObj)
                return Vector3.zero;

            return this.m_gObj.transform.localPosition;
        }
    }

    public int INDEX
    {
        get
        {
            return m_idx;
        }
    }

    #endregion


    /// <summary>
    /// BASE FUNCTIONS......
    /// </summary>
    
    #region BASE FUNCTIONS......

    public Map(int idx, GameObject go)
    {
        this.m_idx = idx;
        this.m_gObj = go;

    }

    public void SetMaterial(int materialType)
    {
        this.m_mType = materialType;

        //refresh();

        //if (materialType < 0)
        //{
        //    testSelectMap();
        //}
        //else
        //{
        //    this.m_mType = materialType;
        //    refresh();
        //}
    }

    #endregion


    /// <summary>
    /// PUBLIC FUNCTIONS......
    /// </summary>
    
    #region PUBLIC FUNCTIONS......

    #endregion


    /// <summary>
    /// PRIVATE FUNCTIONS......
    /// </summary>

    #region PRIVATE FUNCTIONS......

    private void testSelectMap() 
    {
        //string newMaterialName = m_strBaseMaterialPath + "DIRT/Dirt_Path/Materials/Dirt_Path.mat";
        string newMaterialName = "Materials/Dirt/Dirt_Path";
        if (null != m_gObj)
        {
            MeshRenderer renderer = m_gObj.GetComponent<MeshRenderer>();
            if (null != renderer)
            {
                //renderer.material = (Material)AssetDatabase.LoadAssetAtPath(newMaterialName, typeof(Material));
                renderer.material = Resources.Load(newMaterialName) as Material;
            }  
        }
    }

    private void refresh() 
    {
        //string newMaterialName = m_strBaseMaterialPath + "GRASS/GRASS_Dense/GRASS_Dense_Tint_02/Materials/Grass_Dense_Tint_02_Base_C.mat"; // string.Format(m_strBaseMaterialPath, "DIRTDirt_Path/Dirt_Path");
        string newMaterialName = "Materials/Dirt/Dirt_Path";

        //"GRASS/GRASS_Dense/GRASS_Dense_Tint_01/Materials/"
        //"GRASS/GRASS_Dense/GRASS_Dense_Tint_02/Materials/"

        //Assets/Cartoon_Texture_Pack/GRASS/GRASS_Dense/GRASS_Dense_Tint_02/Materials/
        //                         A/{0}/{1}/{2}/Materials/{3}.mat
        /*
        switch (m_mType) 
        {
            case 0:
                {
                    newMaterialName = "Material/Grass/Grass_Tint_01_1";
                    break;
                }

            case 1: 
                {
                    break;
                }

            case 2: 
                { 
                    break; 
                }

            case 3:
                { 
                    break;
                }

            case 4:
                {
                    break;
                }

            default: 
                {
                    break;
                }
        }
        */

        if (null !=  m_gObj) 
        {
            MeshRenderer renderer = m_gObj.GetComponent<MeshRenderer>();
            if (null != renderer)
            {
                //renderer.material = (Material)AssetDatabase.LoadAssetAtPath(newMaterialName, typeof(Material));
                Material m = Resources.Load(newMaterialName) as Material;

                if (null != m)
                {
                    //Texture mainTex = (Texture)Resources.Load("Materials/Dirt/Textures/Dirt_Path_Basecolor");

                    //if (null == mainTex)
                    //    Debug.LogError(" !!!!! MainTex is null ");
                    //else
                    //    Debug.LogError(" !!! MainTex not null ");

                    //m.mainTexture = mainTex;
                    renderer.material = m;
                }
                else
                {
                    Debug.LogError(" !!!!! Map SetMaterial m is NULL " );
                }
            }

        }
        
    }

    #endregion


}
