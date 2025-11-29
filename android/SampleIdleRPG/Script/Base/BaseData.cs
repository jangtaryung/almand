using System;
using System.Collections;
using System.Reflection;
using LitJson;
using UnityEngine;

public class BaseData
{
    public delegate void UpdateCallback(object value);

    private Hashtable bindTb;

    public bool Init { get; set; }
    public static DataManager Data { get; set; }

    public static CacheData Cache
    {
        get { return CacheData.Instance; }
    }

    PropertyInfo[] propertys;
    IDictionary dic;
    string str;
    string arrStr;
    object[] jArr;
    JsonData jsonData;
    bool check;

    public void initData(JsonData msg)
    {
        Init = true;
        propertys = GetType().GetProperties();

        foreach (PropertyInfo pinfo in propertys)
        {
            //            Debuger.Log("==============================");
            try
            {
                dic = msg;
                check = dic.Contains(pinfo.Name);
            }
            catch (Exception ex)
            {
                Debuger.Log("JsonData msg has't " + pinfo.Name + " property." + ex);
                check = false;
            }
//            Debug.Log(msg[pinfo.Name].GetType() + " " + pinfo.PropertyType + " " + msg[pinfo.Name].IsArray);
            try
            {
                if (check)
                {
                    if (pinfo.PropertyType == typeof (string))
                    {
//                        pinfo.SetValue(this, Convert.ToString(msg[pinfo.Name]), null);
                        update(pinfo.Name, Convert.ToString(msg[pinfo.Name]));
                    }
                    else if (pinfo.PropertyType == typeof (int))
                    {
//                        pinfo.SetValue(this, (int)msg[pinfo.Name], null);
                        update(pinfo.Name, (int) msg[pinfo.Name]);
                    }
                    else if (pinfo.PropertyType == typeof(long))
                    {
                        //                        pinfo.SetValue(this, (int)msg[pinfo.Name], null);
                        str = Convert.ToString(msg[pinfo.Name]);
                        update(pinfo.Name, Convert.ToInt64(str));
                    }
                    else if (msg[pinfo.Name].IsArray && pinfo.PropertyType == typeof (object[]))
                    {
                        arrStr = msg[pinfo.Name].ToJson();

                        jArr = JsonMapper.ToObject<object[]>(arrStr);

                        pinfo.SetValue(this, jArr, null);
                    }
                    else if (pinfo.PropertyType == typeof (JsonData) && !msg[pinfo.Name].IsArray)
                    {
                        pinfo.SetValue(this, msg[pinfo.Name], null);
                    }
                    else if (msg[pinfo.Name].IsArray && pinfo.PropertyType == typeof (JsonData[]))
                    {
                        arrStr = msg[pinfo.Name].ToJson();

                        jArr = JsonMapper.ToObject<JsonData[]>(arrStr);

                        pinfo.SetValue(this, jArr, null);
                    }
                    else if (msg[pinfo.Name].IsArray && msg[pinfo.Name].Count == 0)
                    {
                        pinfo.SetValue(this, null, null);
                    }
                }
            }
            catch (Exception e)
            {
                jsonData = msg[pinfo.Name];
                if (jsonData != null)
                {
                    Debuger.LogError(pinfo.Name + " type is " + jsonData.GetType() + " value is " + jsonData +
                                   " can't convert to " + pinfo.PropertyType + "," + e.Message);
                }
            }

//            Debug.Log(pinfo.Name + "  " + pinfo.GetValue(this, null));
        }
    }

    public void update(string prop, object value)
    {
        PropertyInfo pinfo = GetType().GetProperty(prop);
        if (pinfo.PropertyType == typeof (string))
        {
            pinfo.SetValue(this, Convert.ToString(value), null);
        }
        else if (pinfo.PropertyType == typeof(long))
        {
            pinfo.SetValue(this, (long)value, null);
        }
        else if (pinfo.PropertyType == typeof (int))
        {
            pinfo.SetValue(this, (int) value, null);
        }

        if (bindTb != null && bindTb[prop] != null)
        {
            ArrayList callBackArr = (ArrayList) bindTb[prop];

            foreach (object fun in callBackArr)
            {
                UpdateCallback callback = fun as UpdateCallback;
                if (callback != null)
                {
                    UpdateCallback call = callback;
                    call(value);
                }
                else
                {
                    UILabel uiLabel = fun as UILabel;
                    if (uiLabel != null)
                    {
                        uiLabel.text = Convert.ToString(value);
                    }
                }
            }
        }
    }


    private ArrayList checkInit(string prop)
    {
        if (bindTb == null)
        {
            bindTb = new Hashtable();
        }

        ArrayList callBackArr = (ArrayList) bindTb[prop];
        if (callBackArr == null)
        {
            callBackArr = new ArrayList();
            bindTb.Add(prop, callBackArr);
        }
        return callBackArr;
    }

    // bind UpdateCallback
    public void bind(string prop, UpdateCallback callback, bool init = false)
    {
        ArrayList callBackArr = checkInit(prop);
        callBackArr.Add(callback);

        if (init)
        {
            update(prop, GetType().GetProperty(prop).GetValue(this, null));
        }
//        bindTb.Add(prop, callBackArr);
    }

    // bind UILabel
    public void bind(string prop, UILabel target, bool init = false)
    {
        if (target == null)
        {
            return;
        }
        ArrayList callBackArr = checkInit(prop);
        callBackArr.Add(target);
        if (init)
        {
            update(prop, GetType().GetProperty(prop).GetValue(this, null));
        }
    }

    public void unbind(string key, object callback)
    {
        if (bindTb == null)
        {
            return;
        }
        ArrayList callBackArr = (ArrayList) bindTb[key];
        if (callBackArr == null)
        {
            return;
        }
        callBackArr.Remove(callback);
    }

    public void unbind(string key, UpdateCallback callback)
    {
        if (bindTb == null)
        {
            return;
        }
        ArrayList callBackArr = (ArrayList) bindTb[key];
        if (callBackArr == null)
        {
            return;
        }
        callBackArr.Remove(callback);
    }

    public T json2info<T>(JsonData data)
    {
        T info = CacheData.json2info<T>(data);
        return info;
//        foreach (DictionaryEntry entry in data)
//        {
//            PropertyInfo pinfo = info.GetType().GetProperty((string)entry.Key);
//            if (pinfo != null)
//            {
//                if (pinfo.PropertyType == typeof(string))
//                {
//                    pinfo.SetValue(info, Convert.ToString(entry.Value), null);
//                }
//                else if (pinfo.PropertyType == typeof(int))
//                {
//                    pinfo.SetValue(info, Convert.ToInt32(entry.Value.ToString()), null);
//                }
//                else
//                {
//                    pinfo.SetValue(info, entry.Value, null);
//                }
//            }
//        }
//        return info;
    }
}