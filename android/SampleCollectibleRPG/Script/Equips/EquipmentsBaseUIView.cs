using TMPro;
using System;
using UnityEngine;
using Water.Config;
using UnityEngine.UI;
using Water.Game.Util;

using System.Collections;
using UnityEngine.Events;
using HutongGames.PlayMaker;
using System.Collections.Generic;
using Game.Util;
using LuaInterface;

namespace Water
{
    public class EquipmentsBaseUIView : BaseView
    {
        /// <summary>
        /// tabpage component
        /// </summary>
        protected UITabPageCom tabpage_panel;
        protected UITabCom tabs_panel;
        /// <summary>
        /// </summary>
        protected TextMeshProUGUI name_txt;
        protected EquipPropChange PropChange;
        protected Image equip_img;
        protected StopWatchCom zhanli_stopWatchCom;
        protected Button getinfo_btn;
        protected Dictionary<int, EquipAttributesModuleUIBase> attributesModules = new Dictionary<int,EquipAttributesModuleUIBase>();

        /// <summary>
        /// </summary>
        protected StarBar starlist_panel;
        protected Transform konglist_panel;
        /// <summary>
        /// </summary>
        protected EquipDisplayBase eff_dizuo_panel;

        protected WaterToggle skipJinJieAnim;


        protected EquipsMediator.EquipServerData _esd;

        public EquipmentsBaseUIView(UIType uitype_)
            : base(uitype_) { }
        
        private void OnGetInfo()
        {
            if (LuaInterface.LuaScriptMgr.Instance.CallLuaFunction_Object("water.GetSelectItemUIType") != null)
			{
            	SingletonFactory<UIManager>.Instance.GetParentLayer(EUILayer.NoticePop).ClearLayer();
			}
            ReqItem req = new ReqItem(_esd.ExcelData.PieceId, _esd.ExcelData.PieceNum); 
            AddSubUI(UIType.GetItemInfoUI, null, req, (int)-1000);
        }

        public override void InitUIView(object param_)
        {
            base.InitUIView(param_);

            getinfo_btn = getUIComponent<Button>("getinfo_btn");
            getinfo_btn.onClick.AddListener(OnGetInfo);
            tabpage_panel = getUIComponent<UITabPageCom>("tabpage_panel");
            name_txt = getUIComponent<TextMeshProUGUI>("name_txt");
            tabs_panel = getUIComponent<UITabCom>("tabs_panel");
            equip_img = getUIComponent<Image>("equip_img");
            eff_dizuo_panel = getUIComponent<EquipDisplayBase>("eff_dizuo_panel");
            zhanli_stopWatchCom = getUIComponent<StopWatchCom>("zhanli_txt");
            // zhanli_stopWatchCom.text.text = "0";
            skipJinJieAnim = getUIComponent<WaterToggle>("show_tgl");

            starlist_panel = addVirtualUIComponent<StarBar>("starlist_panel");
            konglist_panel = getUIComponent<Transform>("konglist_panel");

            skipJinJieAnim.onValueChanged.AddListener((bool arg0) => {
                GameConfig.SaveEquipJinJieSkipAnim(arg0 ? 1 : 0);
            });
            skipJinJieAnim.isOn = GameConfig.GetEquipJinJieSkipAnim() == 1;

        }

        public override void Dispose()
        {
            getinfo_btn.onClick.RemoveListener(OnGetInfo);
            skipJinJieAnim.onValueChanged.RemoveAllListeners();
            var itor = attributesModules.GetEnumerator();
            while (itor.MoveNext())
                itor.Current.Value.Dispose();
            base.Dispose();
        }

        protected void initBaseInfoView()
        {
            if (!attributesModules.ContainsKey((int)EEquipAttributesTab.BaseInfo))
            {
                UIPrefabWeakLink prefab = getUIComponent<UIPrefabWeakLink>("panel1_JD");
                prefab.loadWithUIComponent<EquipBaseInfoModuleUI>(null, (com_) =>
                {
                    attributesModules.Add((int)EEquipAttributesTab.BaseInfo, com_);
                    com_.RefreshChangeEquipAndLoad(this._esd);
                });
            }
        }

        protected virtual void refreshLeftSideUIWithEquipserverData(EquipsMediator.EquipServerData _esd)
        {
            equip_img.Visible(false);
            XAsset.Assets.LoadWithOwner<Sprite>(_esd.BigIconUrl, (sprite) => { equip_img.sprite = sprite; equip_img.Visible(true); }, this.gameObject);

            if (_esd.CanStarUp)
            {
                starlist_panel.ShowStar(_esd.star);
                konglist_panel.Visible(true);
            }
            else
                konglist_panel.Visible(false);

            name_txt.text = WaterGameConst.GetColorTextStrByColorInt(_esd.NameWithAdv, _esd.Color);
            zhanli_stopWatchCom.SetValue(_esd.GetGS());
            eff_dizuo_panel.ShowByColor(_esd.Color);
        }

        /// <summary>
        /// </summary>
        protected virtual void refreshChangeEquipAndLoad(int uid)
        {
            _esd = Role.Instance.equip.GetEquipServerDataByUid(uid);
            PropChange = new EquipPropChange();
            PropChange.GetCurPropList(_esd);
            PropChange.GetPrePropList(_esd);
            
            refreshLeftSideUIWithEquipserverData(_esd);
        }
    }

    public class EquipDisplayBase : UIBaseComponent
    {
        public void ShowByColor(int color)
        {
            XAsset.Assets.LoadWithOwner<GameObject>(WaterGameConst.GetEquipBaseByColorInt(color), (obj) =>
            {
                if (obj != null)
                {
                    GameObject go = GameObject.Instantiate(obj) as GameObject;
                    if (this.transform.childCount > 0) this.transform.DestroyChildren();
                    go.transform.SetParent(this.transform, false);

                    UIDepth depthCom = go.AddComponent<UIDepth>();
                    depthCom.order = 11;
                    depthCom.isUI = false;
                    depthCom.RefreshSortOrder();
                }
            }, this.gameObject);
        }
        public void ShowMiniEffByColor(int color)
        {
            XAsset.Assets.LoadWithOwner<GameObject>(WaterGameConst.GetMiniEffByColorInt(color), (obj) =>
            {
                if (obj != null)
                {
                    GameObject go = GameObject.Instantiate(obj) as GameObject;
                    if (this.transform.childCount > 0) this.transform.DestroyChildren();
                    go.transform.SetParent(this.transform, false);

                    UIDepth depthCom = go.AddComponent<UIDepth>();
                    depthCom.order = 11;
                    depthCom.isUI = false;
                    depthCom.RefreshSortOrder();
                }
            }, this.gameObject);
        }
    }
}
