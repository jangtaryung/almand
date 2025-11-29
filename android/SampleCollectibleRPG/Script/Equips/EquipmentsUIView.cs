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
	public class EquipmentsHandBookUIView:EquipmentsBaseUIView
	{
		protected List<Transform> konglist_paneltrans;

		public EquipmentsHandBookUIView()
			: base(UIType.EquipmentsHandBookUI)
		{

		}

		public override void InitUIView(object param_)
		{
			base.InitUIView(param_);
            getinfo_btn.gameObject.SetActive(true);
			tabpage_panel.tab_panel.gameObject.SetActive(false);
			starlist_panel.gameObject.SetActive (false);
			konglist_paneltrans = TransformUtil.FindChildrenAccurately(this.transform, "konglist_panel");
			foreach (var item in konglist_paneltrans) 
			{
				item.gameObject.SetActive (false);
			}
            this.skipJinJieAnim.Visible(false);
			_esd = param_ as EquipsMediator.EquipServerData;
            refreshLeftSideUIWithEquipserverData(_esd);
            initBaseInfoView();
		}

        protected override void refreshChangeEquipAndLoad(int uid)
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
            zhanli_stopWatchCom.SetWatchZero(_esd.GetGS());
            eff_dizuo_panel.ShowByColor(_esd.Color);
        }

        public override void Dispose()
        {
            SingletonFactory<UIManager>.Instance.ShowLayerByEUILayer(EUILayer.NoticePop);
        }
	}
		
    class EquipmentsUIView : EquipmentsBaseUIView
    {
        public const string EvtAddAutoStarUI = "EquipmentsUIView_EvtAddAutoStarUI";
        public const string EvtEquipmentsOpen = "EvtEquipmentsOpen";
        public const string EvtStopUpStarEquip = "EvtStopUpStarEquip";

        //Transform info_trans;
        Transform starup_trans;
        Transform enhance_trans;
        Transform advance_trans;

        Button left_btn, right_btn;
        TextMeshProUGUI inuse_txt;

        public EquipmentsUIView()
            : base(UIType.EquipmentsUI)
        {
        }

        protected void initUpStarView()
        {
            if (!attributesModules.ContainsKey((int)EEquipAttributesTab.Upstar))
            {
                UIPrefabWeakLink prefab = getUIComponent<UIPrefabWeakLink>("panel2_JD");
                prefab.loadWithUIComponent<EquipUpStarModuleUI>(null, (com_) =>
                {
                    attributesModules.Add((int)EEquipAttributesTab.Upstar, com_);
                    com_.RefreshChangeEquipAndLoad(this._esd);
                });
            }
        }

        protected void initEnhanceView()
        {
            if (!attributesModules.ContainsKey((int)EEquipAttributesTab.Enhance))
            {
                UIPrefabWeakLink prefab = getUIComponent<UIPrefabWeakLink>("panel5_JD");
                prefab.loadWithUIComponent<EquipEnhanceModuleUI>(null, (com_) =>
                {
                    attributesModules.Add((int)EEquipAttributesTab.Enhance, com_);
                    com_.RefreshChangeEquipAndLoad(_esd);
                });
            }
        }

        protected void initAdvanceView()
        {
            if (!attributesModules.ContainsKey((int)EEquipAttributesTab.Advance))
            {
                UIPrefabWeakLink prefab = getUIComponent<UIPrefabWeakLink>("panel3_JD");
                prefab.loadWithUIComponent<EquipAdvanceModuleUI>(null, (com_) =>
                {
                    attributesModules.Add((int)EEquipAttributesTab.Advance, com_);
                    com_.RefreshChangeEquipAndLoad(_esd);
                });
            }
        }

        void OpenComposeSub(object sender_, WaterEventArgs args_)
        {
            bool isGem = (sender_ as EquipGemItem) != null;
             
            AddSubUI(isGem ? UIType.GemComposeViewUI:UIType.ItemBeingComposeUI, (gameObject) => {
            
            }, sender_);
        }


        protected override void OnEnter()
        {
            base.OnEnter();
            RefreshHoverBar();
            SingletonFactory<EventManager>.Instance.AddEventListener(WaterGameConst.EvtShowItemGetView, showGetItemView);
            SingletonFactory<EventManager>.Instance.AddEventListener(HeroAttributesUIView.EvtOpenSubUI, OpenComposeSub);
        }

        private void RefreshHoverBar()
        {
            // if (_esd == null || _esd.IsRoleEquip)
            // {
                SingletonFactory<UIManager>.Instance.HoverLayerViewUI.ResShowListControl(new List<int> { (int)SpecialItemID.TEAM_GS, (int)SpecialItemID.GOLD, (int)SpecialItemID.DIAMOND });
            // }
            // else
            // {
            //     SingletonFactory<UIManager>.Instance.HoverLayerViewUI.ResShowListControl(new List<int> { (int)SpecialItemID.TEAM_GS, (int)SpecialItemID.EQUIP_EXP, (int)SpecialItemID.GOLD, (int)SpecialItemID.DIAMOND });
            // }
        }


        protected override void OnExit()
        {
            SingletonFactory<UIManager>.Instance.HoverLayerViewUI.ShowNormalResList();
            SingletonFactory<EventManager>.Instance.RemoveEventListener(WaterGameConst.EvtShowItemGetView, showGetItemView);
            SingletonFactory<EventManager>.Instance.RemoveEventListener(HeroAttributesUIView.EvtOpenSubUI, OpenComposeSub);

            //FloatingMessageUtil.ClearFloatingShow();
            base.OnExit();
        }

        private EquipsMediator.EquipServerData _esd;
        public override void InitUIView(object param_)
        {
            base.InitUIView(param_);

            //enhance_trans = tabs_panel.transform.GetChild(1);
            //starup_trans = tabs_panel.transform.GetChild(2);
            //advance_trans = tabs_panel.transform.GetChild(3);

            enhance_trans = getUIComponent<Transform>("t5_panel");


            advance_trans = getUIComponent<Transform>("t3_panel");
            starup_trans = getUIComponent<Transform>("t2_panel");
            //t6_panel = getUIComponent<Transform>("t6_panel");
            //t3_panel = getUIComponent<Transform>("t3_panel");
            //t2_panel = getUIComponent<Transform>("t2_panel");

            left_btn = getUIComponent<Button>("left_btn");
            right_btn = getUIComponent<Button>("right_btn");
            inuse_txt = getUIComponent<TextMeshProUGUI>("inuse_txt");

            _esd = Role.Instance.equip.GetEquipServerDataByUid(Role.Instance.equip.ShowUid);

            RefreshHoverBar();

            RegisterModuleUnlock();
            refreshChangeEquipAndLoad(_esd.uid);
            //refreshChangeEquipAndLoad(_esd.uid);


            Role.Instance.equip.EquipAdvanceCompelete += refreshAdvance;
            Role.Instance.equip.EquipEnhanceCompelete += refreshEnhance;
            Role.Instance.equip.EquipUpStarComplete += refreshUpStar;
            Role.Instance.equip.ActEquipShowUidChange += refreshChangeEquipAndLoad;
            SingletonFactory<EventManager>.Instance.AddEventListener(RoleBaseInfoMediator.RoleBaseInfo_Update, refreshGold);
            SingletonFactory<EventManager>.Instance.AddEventListener(EquipmentsUIView.EvtAddAutoStarUI, showAutoStarUI);
            SingletonFactory<EventManager>.Instance.AddEventListener(BagMediator.BagMediatorItemAddUpdate, refreshBagItems);

            RedDotCheck(_esd);

            int index = 0;
            if (param_ != null)
            {
                index = (int)param_;

                //if (index == (int)EEquipAttributesTab.Enhance && !_esd.IsRoleEquip)
                //{
                //    index = (int)EEquipAttributesTab.EnhancePartner;
                //}
            }
            tabs_panel.selectCallBack += tabSelectedCallback;
            tabpage_panel.SelectTabIndex = index;

            left_btn.onClick.AddListener(left_btn_OnClick);
            right_btn.onClick.AddListener(right_btn_OnClick);

            SingletonFactory<EventManager>.Instance.DispatchEvent(EvtEquipmentsOpen, null);
        }

        public override void Dispose()
        {
            Role.Instance.equip.ActEquipShowUidChange -= refreshChangeEquipAndLoad;
            Role.Instance.equip.EquipAdvanceCompelete -= refreshAdvance;
            Role.Instance.equip.EquipEnhanceCompelete -= refreshEnhance;
            Role.Instance.equip.EquipUpStarComplete -= refreshUpStar;
            SingletonFactory<EventManager>.Instance.RemoveEventListener(RoleBaseInfoMediator.RoleBaseInfo_Update, refreshGold);
            SingletonFactory<EventManager>.Instance.RemoveEventListener(EquipmentsUIView.EvtAddAutoStarUI, showAutoStarUI);
            SingletonFactory<EventManager>.Instance.RemoveEventListener(BagMediator.BagMediatorItemAddUpdate, refreshBagItems);
            tabs_panel.selectCallBack -= tabSelectedCallback;

            UnRegisterModuleUnlock();
            base.Dispose();
        }

        private void refreshBagItems()
        {
            if (_esd != null)
                refreshData(_esd.uid);
        }

        protected void RegisterModuleUnlock()
        {

            //if (_esd.IsRoleEquip)
            //{
                ModuleUnlock.RegisterRectTrans(ModuleId.EquipmentsEnhance, enhance_trans as RectTransform, 7000);
            //}
            //else
            //{
            //    ModuleUnlock.RegisterRectTrans(ModuleId.EquipmentsEnhance, enhance_partner_trans as RectTransform, 7000);
            //}
            ModuleUnlock.RegisterRectTrans(ModuleId.EquipAdvance, advance_trans as RectTransform, 7001);
            ModuleUnlock.RegisterRectTrans(ModuleId.EquipUpstar, starup_trans as RectTransform, 7002);
        }

        protected void UnRegisterModuleUnlock()
        {
            ModuleUnlock.UnRegisterRectTrans(ModuleId.EquipmentsEnhance, 7000);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.EquipAdvance, 7001);
            ModuleUnlock.UnRegisterRectTrans(ModuleId.EquipUpstar, 7002);
        }

        private void showGetItemView(object sender, WaterEventArgs args)
        {
            ReqContentBase reqItem = sender as ReqContentBase;
            AddSubUI(reqItem.GetWayUIType(), null, sender, -1000);
        }

        int _prevIdx = 0;
        protected void tabSelectedCallback(int idx_)
        {
            
            this.skipJinJieAnim.Visible(idx_ == 3);
            if (idx_ == 0)
            {
                initBaseInfoView();
            }
            else if (idx_ == 1)
            {
                initEnhanceView();
            }
            else if (idx_ == 2)
            {
                initUpStarView();
            }
            else if (idx_ == 3)
            {
                initAdvanceView();
            }

            //else if (idx_ == 2)
            //{
            //    initEnhancePartnerView();
            //}
            //else if (idx_ == 3)
            //{
            //    initUpStarView();
            //}
            //else if (idx_ == 4)
            //{
            //    initAdvanceView();
            //}
            _prevIdx = idx_;

            stopUpStar(idx_);
        }

        bool onTurningPage = false;

        void left_btn_OnClick()
        {
            onTurningPage = true;
            Role.Instance.equip.ShowUid = Role.Instance.partner.PartnerTeam.GetPreEquipInAllInlineHeroEquipByCurEquipUid(Role.Instance.equip.ShowUid).uid;  
        }

        void right_btn_OnClick()
        {
            onTurningPage = true;
            Role.Instance.equip.ShowUid = Role.Instance.partner.PartnerTeam.GetNextEquipInAllInlineHeroEquipByCurEquipUid(Role.Instance.equip.ShowUid).uid; 
        }

        private void stopUpStar(int index)
        {
            SingletonFactory<EventManager>.Instance.DispatchEvent(EvtStopUpStarEquip, null);
        }

        private void refreshGold()
        {
            EquipsMediator.EquipServerData esd = Role.Instance.equip.GetEquipServerDataByUid(Role.Instance.equip.ShowUid);
            var itor = attributesModules.GetEnumerator();
            while (itor.MoveNext())
                itor.Current.Value.RefreshResource(esd);
        }


        protected void refreshAnyway(EquipsMediator.EquipServerData esd_)
        {
            RedDotCheck(esd_);
        }

        private void RedDotCheck(EquipsMediator.EquipServerData esd_)
        {
            RedDotReminderHandle.RedDotHandle(esd_.IsCanRedDotReminder(RedDotHandleID.Slot_Equip_StarUp_ID), starup_trans);
            //if (esd_.IsRoleEquip)
            //{
                RedDotReminderHandle.RedDotHandle(esd_.IsCanRedDotReminder(RedDotHandleID.Slot_Equip_Enhance_ID), enhance_trans);
            //}
            //else
            //{
            //    RedDotReminderHandle.RedDotHandle(esd_.IsCanRedDotReminder(RedDotHandleID.Slot_Equip_Enhance_ID), enhance_partner_trans);
            //}
            RedDotReminderHandle.RedDotHandle(esd_.IsCanRedDotReminder(RedDotHandleID.Slot_Equip_AdvanceUp_ID), advance_trans);
        }

 
        private void refreshAdvance()
        {
            EquipsMediator.EquipServerData esd = Role.Instance.equip.GetEquipServerDataByUid(Role.Instance.equip.ShowUid);
            var itor = attributesModules.GetEnumerator();
            while (itor.MoveNext()) itor.Current.Value.RefreshAdvance(esd);
            refreshAnyway(esd);
            name_txt.text = WaterGameConst.GetColorTextStrByColorInt(esd.NameWithAdv, esd.Color);
            PropChange.GetCurPropList(esd);
            PropChange.ShowPropChange();
            
            zhanli_stopWatchCom.SetWatchZero(_esd.GetGS());
        }

        int preStar = -1;
 
        private void refreshUpStar()
        {
            EquipsMediator.EquipServerData esd = Role.Instance.equip.GetEquipServerDataByUid(Role.Instance.equip.ShowUid);
            refreshAnyway(esd);
           
            if (esd.star != preStar)
            {
                preStar = esd.star;
                PropChange.GetCurPropList(esd);
                PropChange.ShowPropChange();
            }

            var itor = attributesModules.GetEnumerator();
            while (itor.MoveNext()) itor.Current.Value.RefreshUpStar(esd);
            
            zhanli_stopWatchCom.SetWatchZero(_esd.GetGS());
        }

        private void refreshEnhance()
        {
            EquipsMediator.EquipServerData esd = Role.Instance.equip.GetEquipServerDataByUid(Role.Instance.equip.ShowUid);
            var itor = attributesModules.GetEnumerator();
            while (itor.MoveNext()) itor.Current.Value.RefreshEnhance(esd);
            refreshAnyway(esd);
            PropChange.GetCurPropList(esd);
            PropChange.ShowPropChange();
            zhanli_stopWatchCom.SetWatchZero(_esd.GetGS());
        }

        private void showAutoStarUI(object sender, WaterEventArgs args)
        {
            SingletonFactory<UIManager>.Instance.OpenUI(UIType.AutoStarUpUI);
        }

        protected void refreshData(int uid){
            _esd = Role.Instance.equip.GetEquipServerDataByUid(uid);

            if (onTurningPage)
            {
                onTurningPage = false;
                RefreshHoverBar();
            }

            TeamSlotSeverData tssd = Role.Instance.partner.PartnerTeam.GetTeamSlotServerDataByEquipUid(uid);
            if (!_esd.IsEquiped || !ModuleUnlock.IsModuleUnlocked(ModuleId.EquipmentsEnhance))
            {
                enhance_trans.Visible(false);
                //enhance_partner_trans.Visible(false);
                if (_prevIdx == (int)EEquipAttributesTab.Enhance)// || _prevIdx == (int)EEquipAttributesTab.EnhancePartner)
                {
                    tabs_panel.SelectTabIndex = (int)EEquipAttributesTab.BaseInfo;
                    //return;
                }
            }
            else {

                //if (_esd.IsRoleEquip)
                //{
                    enhance_trans.Visible(true);
                    //enhance_partner_trans.Visible(false);
                //    if (tabs_panel.SelectTabIndex == (int)EEquipAttributesTab.EnhancePartner)
                //    {
                //        tabs_panel.SelectTabIndex = (int)EEquipAttributesTab.Enhance;
                //        //return;
                //    }
                //}
                //else
                //{
                //    enhance_trans.Visible(false);
                //    //enhance_partner_trans.Visible(true);
                //    if (tabs_panel.SelectTabIndex == (int)EEquipAttributesTab.Enhance)
                //    {
                //        tabs_panel.SelectTabIndex = (int)EEquipAttributesTab.EnhancePartner;
                //        //return;
                //    }
                //}
            }
            if (_esd.advModule.advMax <= 0 || !_esd.IsEquiped || !ModuleUnlock.IsModuleUnlocked(ModuleId.EquipAdvance))
            {
                advance_trans.Visible(false);
                if (_prevIdx == (int)EEquipAttributesTab.Advance)
                {
                    tabs_panel.SelectTabIndex = (int)EEquipAttributesTab.BaseInfo;
                    //return;
                }
            }
            else
            {
                advance_trans.Visible(true);
            }
            if (_esd.starModule.getMaxStar <= 0 || !_esd.IsEquiped || !ModuleUnlock.IsModuleUnlocked(ModuleId.EquipUpstar))
            {
                starup_trans.Visible(false);
                if (_prevIdx == (int)EEquipAttributesTab.Upstar)
                {
                    tabs_panel.SelectTabIndex = (int)EEquipAttributesTab.BaseInfo;
                    //return;
                }
            }
            else
            {
                starup_trans.Visible(true);
            }

            var itor = attributesModules.GetEnumerator();
            while (itor.MoveNext()) itor.Current.Value.RefreshChangeEquipAndLoad(_esd);

            
            right_btn.Visible(_esd.IsEquiped && Role.Instance.partner.PartnerTeam.inlineEquipCount > 1);
            left_btn.Visible(_esd.IsEquiped && Role.Instance.partner.PartnerTeam.inlineEquipCount > 1);
            inuse_txt.Visible(_esd.IsEquiped && Role.Instance.partner.PartnerTeam.inlineEquipCount > 1);
            if (_esd.IsEquiped)
                inuse_txt.text = string.Format(CodeTextData.AUTOSTR("{0}"), tssd.heroData.NameWithColor);

            zhanli_stopWatchCom.SetValue(_esd.GetGS());
        }

        protected override void refreshChangeEquipAndLoad(int uid)
        {
            base.refreshChangeEquipAndLoad(uid);
            refreshData(uid);
        }
    }


    public class EquipIcon : UIBaseComponent
    {
        Icon80 icon;

        public override void InitUIView(object obj)
        {
            base.InitUIView(obj);

            UIPrefabCom com = gameObject.AddComponent<UIPrefabCom>();
            com.InitUIView(null);

            icon = com.CreatPreFabComponetInstanceToThis(typeof(Icon80)) as Icon80;

            icon.labelall_panel.Visible(true);

            icon.disableRayCast();
        }

        protected override void AwakeInit(object param_ = null)
        {
            //isReflect = false;
            base.AwakeInit();

            
            StarBar nextstar_panel;

            PropItemUI prop_panel;

            TextMeshProUGUI succrate_txt;

            TextMeshProUGUI skill_txt;

            BaseList reqItem_panel;

            TextMeshProUGUI upstargoldreq_txt;

            Button upstar_btn;
            
            Button upstar2_btn;

            Button set_btn;

            Transform btn2_panel;

            Transform max2_lbl;
         
            //GetComponent<Image>().raycastTarget = false;
        }

        public void showIconWithoutStar(EquipsMediator.EquipServerData data_)
        {
            //icon.icon_img.Visible(false);
            //XAsset.Assets.LoadWithOwner<Sprite>(data_.IconUrl, (sprite) =>
            ////SingletonFactory<AssetManager>.Instance.LoadAsync<Sprite>(data_.IconUrl, (sprite) =>
            //{
            //    if (this == null) return;
            //    icon.icon_img.sprite = sprite;
            //    icon.icon_img.Visible(true);
            //}, this.gameObject, true);
            icon.LoadIcon_ImgSprite(data_.IconUrl);

            //IconItemBase.SetColor(icon.quality_panel.GetChild(0), data_.Color);
            icon.SetColor(data_.Color);
            icon.showUpLabel(data_.upgrade, data_.Color);

            icon.ShowSuitEffect(LuaScriptMgr.Instance.CallLuaFunction_String_Int("water.role.equip.GetIconEffectPathById", (int)data_.ExcelData.Item_Id));
        }

       
        public void showIcon(EquipsMediator.EquipServerData data_)
        {
            showIconWithoutStar(data_);

            icon.showStar(data_.star);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }

    public class StarBar : VirtualUIBaseComponent
    {
        public void ShowStar(int level)
        {
            if (level < 0) level = 0;

            int count = level % 5;
            if (count == 0 && level != 0)
                count = 5;

            int layer = (level - 1) / 5;

            string StarImageUrl = "";
            StarImageUrl = UIAssetPath.GetEquipStarFullPath((EquipStarSeries)layer);

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).Visible(false);
            }

            GameSpriteCache.getSprite(StarImageUrl, (sprite) =>
            {
                if (this == null) return;
                int i;
                Transform tran;
                for (i = 0; i < transform.childCount; i++)
                {
                    tran = transform.GetChild(i);
                    if (i < count)
                    {
                        tran.GetComponent<Image>().sprite = sprite;
                        tran.Visible(true);
                    }
                    else
                        tran.Visible(false);
                }
            }, this.gameObject, true);
        }

        public void DisableRayCast()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Image>().raycastTarget = false;
            }
        }

        public void ShowStarWithColor(int count, ColorType color)
        {
            if (count == 0)
                ShowStar(count);
            else
                ShowStar(((int)color - 1) * 5 + count);
        }

        public void ShowStarWithColorInt(int count, int colorValue)
        {
            ShowStarWithColor(count, (ColorType)colorValue);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }

    public class ReqListItem : BaseListItem
    {
        UIPrefabCom item_img;

        Icon80 icon;

        TextMeshProUGUI reqnum_txt;

        ReqContentBase _item;

        protected override void AwakeInit(object param_ = null)
        {
            base.AwakeInit();
            item_img = getUIComponent<UIPrefabCom>("item_img");
            reqnum_txt = getUIComponent<TextMeshProUGUI>("reqnum_txt");

            icon = (item_img.CreatPreFabComponetInstanceToThis(typeof(Icon80)) as Icon80);
            ClickEventTrigger.Get(icon.click_panel.gameObject).onCClick += showGetItemView;
        }

        public override void Dispose()
        {
            ClickEventTrigger.Get(icon.click_panel.gameObject).onCClick -= showGetItemView;
            base.Dispose();
        }

        protected override void FillData(object vo = null)
        {
            base.FillData(vo);
            item_img.Visible(false);
            if (vo == null) return;
            _item = vo as ReqContentBase;
            if (_item.IconPath != null && _item.IconPath != "")
            {
                XAsset.Assets.LoadWithOwner<Sprite>(_item.IconPath, (sprite) =>
                //SingletonFactory<AssetManager>.Instance.LoadAsync<Sprite>(_item.IconPath, (sprite) =>
                    {
                        if (sprite == null) return;
                        item_img.transform.GetChild(0).Find("icon_img").GetComponent<Image>().sprite = sprite;
                        item_img.Visible(true);
                    },this.gameObject, true
                    );
            }

            reqnum_txt.text = string.Format("{0}/{1}", WaterGameConst.GetGoldNumOderBackStr(_item.OwnNum), WaterGameConst.GetGoldNumOderBackStr(_item.ReqNum));

            if (_item.type == ItemType.Hero)
            {
                icon.SetColor(_item.Quality);
                icon.SetBGColor(_item.Color);
            }else{
                icon.SetColor(_item.Color);
            }

            if (_item.isMeetTheNeed)
                reqnum_txt.text = WaterGameConst.ChangeTextColor(TextColorType.green, reqnum_txt.text);
            else
                reqnum_txt.text = WaterGameConst.ChangeTextColor(TextColorType.red, reqnum_txt.text);                
        }


        private void showGetItemView(GameObject obj)
        {
            //if (!_item.isMeetTheNeed) 
            //{
            //ReqItem hero = new ReqItem(this._config.PieceId, acd.Num2);

            //if (_item is ReqHero)
            //{
            //    ReqHero hero = _item as ReqHero;
            //    new ReqItem(hero._config.PieceId, 1).ShowGetWayView();
            //}
            //else
            //{
            //    (_item as ReqContentBase).ShowGetWayView();
            //}

            (_item as ReqContentBase).ShowGetWayView();
        }
    }
}