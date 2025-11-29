using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Water.Config;
using Game.Util;
using LuaInterface;

namespace Water
{
    class EquipmentsListUIViewParam{
        public int openTab = 0;  
    }

    /// <summary>
    ///  (responsing ui view for "equipments" button on main city ui)
    /// </summary>
    public class EquipmentsListUIView : BaseView
    {
        public const string EvtAddSubUI = "EquipmentsListUIView_AddSubUI";

        ScrollRect role_equipment_panel, partner_equipment_panel;
        ScrollList role_equip_panel;
        ScrollList partner_equip_panel;

        GameObject role_title_panel;
        GameObject partner_title_panel;

        ScrollRect role_debrisgrid_panel, partner_debrisgrid_panel;
        ScrollList role_combinable_panel, partner_combinable_panel;
        ScrollList role_debris_panel;
        ScrollList partner_debris_panel;

        GameObject role_combinable_title_panel, partner_combinable_title_panel;
        GameObject role_debris_title_panel;
        GameObject partner_debris_title_panel;

        UITabCom tabs_panel;
        UITabPageCom tabpage_panel;

        Button rebuild_btn;
     
        Button decomposition_btn;

        Transform role_special1_img, partner_special1_img;
        Transform role_panel1_panel, partner_panel1_panel;
        Transform role_special2_img, partner_special2_img;
        Transform role_panel2_panel, partner_panel2_panel;

        Transform role_Debris_Btn, partner_Debris_Btn;

        TextMeshProUGUI limit_txt;
        Transform role_equip_panel_root, partner_equip_panel_root, role_equip_debris_panel, partner_equip_debris_panel;

        private int roleEquipCount, partnerEquipCount = 0;

        public EquipmentsListUIView()
            : base(UIType.EquipmentsListUI)
        {
        }

        protected override void AwakeInit(object param_ = null)
        {
            base.AwakeInit();

            EquipmentsListUIViewParam ep = null;
            if(param_ != null) ep = param_ as EquipmentsListUIViewParam;

     
            role_equip_panel_root = getUIComponent<Transform>("pc_panel1_panel");
            partner_equip_panel_root = getUIComponent<Transform>("gc_panel1_panel");
            role_equip_debris_panel = getUIComponent<Transform>("pc_panel2_panel");
            partner_equip_debris_panel = getUIComponent<Transform>("gc_panel2_panel");

            role_equipment_panel = getUIComponentByParent<ScrollRect>("equipment_panel", role_equip_panel_root);
            partner_equipment_panel = getUIComponentByParent<ScrollRect>("equipment_panel", partner_equip_panel_root);
            role_equip_panel = getUIComponent<ScrollList>("role_equip_panel");
            partner_equip_panel = getUIComponent<ScrollList>("partner_equip_panel");
            role_title_panel = TransformUtil.FindChildAccurately(transform, "role_title_panel").gameObject;
            partner_title_panel = TransformUtil.FindChildAccurately(transform, "partner_title_panel").gameObject;
            limit_txt = getUIComponent<TextMeshProUGUI>("limit_lbl");


            
            role_debrisgrid_panel = getUIComponentByParent<ScrollRect>("debrisgrid_panel", role_equip_debris_panel);
            partner_debrisgrid_panel = getUIComponentByParent<ScrollRect>("debrisgrid_panel", partner_equip_debris_panel);

            role_combinable_panel = getUIComponentByParent<ScrollList>("combinable_panel", role_equip_debris_panel);
            partner_combinable_panel = getUIComponentByParent<ScrollList>("combinable_panel", partner_equip_debris_panel);

            role_debris_panel = getUIComponent<ScrollList>("role_debris_panel");
            partner_debris_panel = getUIComponent<ScrollList>("partner_debris_panel");
            role_combinable_title_panel = getUIComponentByParent<Transform>("combinable_title_panel", role_equip_debris_panel).gameObject;
            partner_combinable_title_panel = getUIComponentByParent<Transform>("combinable_title_panel", partner_equip_debris_panel).gameObject;

            role_debris_title_panel = TransformUtil.FindChildAccurately(transform, "role_debris_title_panel").gameObject;
            partner_debris_title_panel = TransformUtil.FindChildAccurately(transform, "partner_debris_title_panel").gameObject;
            
            tabs_panel = getUIComponent<UITabCom>("tabs_panel");
            tabpage_panel = getUIComponent<UITabPageCom>("tabpage_panel");
            rebuild_btn = getUIComponent<Button>("rebuild_btn");
            decomposition_btn = getUIComponent<Button>("decomposition_btn");

            role_special1_img = getUIComponentByParent<Transform>("special1_img", role_equip_panel_root);
            partner_special1_img = getUIComponentByParent<Transform>("special1_img", partner_equip_panel_root);
            role_panel1_panel = getUIComponentByParent<Transform>("equipment_panel", role_equip_panel_root);
            partner_panel1_panel = getUIComponentByParent<Transform>("equipment_panel", partner_equip_panel_root);
            role_special2_img = getUIComponentByParent<Transform>("special2_img", role_equip_debris_panel);
            partner_special2_img = getUIComponentByParent<Transform>("special2_img", partner_equip_debris_panel);
            role_panel2_panel = getUIComponentByParent<Transform>("debrisgrid_panel", role_equip_debris_panel);
            partner_panel2_panel = getUIComponentByParent<Transform>("debrisgrid_panel", partner_equip_debris_panel);

            role_equip_panel.SetListInit(typeof(EquipListItem), role_equipment_panel.transform);
            partner_equip_panel.SetListInit(typeof(EquipListItem), partner_equipment_panel.transform);
            role_combinable_panel.SetListInit(typeof(EquipDebrisListItem), role_debrisgrid_panel.transform);
            partner_combinable_panel.SetListInit(typeof(EquipDebrisListItem), partner_debrisgrid_panel.transform);
            role_debris_panel.SetListInit(typeof(EquipDebrisListItem), role_debrisgrid_panel.transform);
            partner_debris_panel.SetListInit(typeof(EquipDebrisListItem), partner_debrisgrid_panel.transform);

            refreshEquipList();
           // refreshEquipDebrisList();
   
            rebuild_btn.onClick.AddListener(rebuild_btn_OnClick);
            decomposition_btn.onClick.AddListener(decomposition_btn_OnClick);

            Role.Instance.equip.EquipEnhanceCompelete += refreshEquipList;
            Role.Instance.equip.EquipAdvanceCompelete += refreshEquipList;
            Role.Instance.equip.EquipUpStarComplete += refreshEquipList;
            Role.Instance.equip.EquipCombineCompelete += refreshEquipList;
            Role.Instance.equip.EquipCombineCompelete += refreshEquipDebrisList;
            Role.Instance.equip.EquipCombineCompleteWithUid += this.EquipCombineComplete;
            Role.Instance.equip.EquipCombineCompleteWithId += this.ExpEquipCombineComplete;

            Role.Instance.equip.EquipInfoUpdate += refreshEquipList;


            //tabs_panel.selectCallBack += showList;

            role_Debris_Btn = tabs_panel.transform.GetChild(2);
            partner_Debris_Btn = tabs_panel.transform.GetChild(3);

            Guide.BindRectransform(7003, role_Debris_Btn.transform as RectTransform);
            ClickEventTrigger.Get(role_Debris_Btn.gameObject).onCClick = new UnityEngine.Events.UnityAction<GameObject>((obj) => { Guide.FinishClickOperation(7003); });

            SingletonFactory<EventManager>.Instance.AddEventListener(EvtAddSubUI, AddGetItemUI);


            tabpage_panel.selectCallBack = (int index_) =>
            {
                TextMeshProUGUI text = decomposition_btn.GetComponentInChildren<TextMeshProUGUI>();

                 if (index_ == 0 || index_ == 1)
                 {
                    text.text = CodeTextData.AUTOSTR("05571364");
                    limit_txt.Visible(true);
                    ViplvData config = ViplvData.GetData((uint)Role.Instance.baseInfo.vip);
                    limit_txt.text = string.Format(CodeTextData.AUTOSTR("<color={0}>{1}/{2}</color>"), "#A58C73", (roleEquipCount + partnerEquipCount), config.EquipNum);

                 }
                else
                {
                    text.text = CodeTextData.AUTOSTR("05578364");
                    limit_txt.Visible(false);    
                }

            };
            
            if(ep != null){
                if(ep.openTab == 1)
                    tabpage_panel.SelectTabIndex = 2;
            }
        }

        private void EquipCombineComplete(int uid)
        {
            Role.Instance.equip.ShowUid = uid;
            SingletonFactory<UIManager>.Instance.OpenUI(UIType.EquipmentsInfoUI, new object[] { 0, uid });
        }

        private void ExpEquipCombineComplete(int itemId)
        {
            SingletonFactory<UIManager>.Instance.OpenUI(UIType.EquipmentsInfoUI, new object[] { 1, itemId });
        }

        private void AddGetItemUI(object sender_, WaterEventArgs args_)
        {
            //SingletonFactory<UIManager>.Instance.SetHoverViewUIParent(this.transform);
            AddSubUI(UIType.GetItemInfoUI, (gameObject) => {
               // gameObject.GetComponent<GetItemInfoUIView>().onClose += RecoverResourceBar;
            }, sender_);
        }

        //private void RecoverResourceBar()
        //{
        //    SingletonFactory<UIManager>.Instance.RecoverHoverViewUIParent();
        //}

        //private void showList(int index_)
        //{
        //    if (index_ == 1)
        //    {
        //        if (all_panel.DataProvider == null)
        //            all_panel.DataProvider = GetDebris();
        //    }
        //}
        protected override void OnEnter()
        { 
            RegisterRedDotHandle();
            refreshEquipDebrisList();
            base.OnEnter();
        }

        protected override void OnExit()
        {
            RemoveRedDotHandle();
            base.OnExit();
        }

        private void RegisterRedDotHandle()
        {
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Role_Equip_Combine_ID, 0, role_Debris_Btn);
            RedDotReminderHandle.AddRedDotReminder(RedDotHandleID.Partner_Equip_Combine_ID, 0, partner_Debris_Btn);
        }

        private void RemoveRedDotHandle()
        {
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Role_Equip_Combine_ID, 0, role_Debris_Btn);
            RedDotReminderHandle.RemoveRedDotReminder(RedDotHandleID.Partner_Equip_Combine_ID, 0, partner_Debris_Btn);
        }

        private void refreshEquipList()
        {
            List<EquipsMediator.EquipServerData> roleList;
            List<EquipsMediator.EquipServerData> partnerList;
            GetEquips(out roleList, out partnerList);
            role_equip_panel.DataProvider = new DataProvider<EquipsMediator.EquipServerData>(roleList);// GetEquips(true) as List<EquipsMediator.EquipServerData>);
            partner_equip_panel.DataProvider = new DataProvider<EquipsMediator.EquipServerData>(partnerList);// GetEquips(false) as List<EquipsMediator.EquipServerData>);

            roleEquipCount = role_equip_panel.DataProvider.GetList().Count;
            partnerEquipCount = partner_equip_panel.DataProvider.GetList().Count;

            role_title_panel.SetActive(roleEquipCount > 0);
            role_equip_panel.gameObject.SetActive(roleEquipCount > 0);
            partner_title_panel.SetActive(partnerEquipCount > 0);
            partner_equip_panel.gameObject.SetActive(partnerEquipCount > 0);

            bool role_hasEquip = roleEquipCount > 0;
            bool partner_hasEquip = partnerEquipCount > 0;

            role_special1_img.Visible(!role_hasEquip);
            partner_special1_img.Visible(!partner_hasEquip);

            role_panel1_panel.Visible(role_hasEquip);
            partner_panel1_panel.Visible(partner_hasEquip);

            ViplvData config = ViplvData.GetData((uint)Role.Instance.baseInfo.vip);
            limit_txt.text = string.Format(CodeTextData.AUTOSTR("<color={0}>{1}/{2}</color>"), "#A58C73", (roleEquipCount + partnerEquipCount), config.EquipNum);
        }

        private void refreshEquipDebrisList()
        {
            List<EquipsDebrisItem> role_combinableList;
            List<EquipsDebrisItem> partner_combinableList;

            List<EquipsDebrisItem> roleList;
            List<EquipsDebrisItem> partnerList;
            GetDebris(out role_combinableList, out partner_combinableList, out roleList, out partnerList);

            role_combinable_panel.DataProvider = new DataProvider<EquipsDebrisItem>(role_combinableList);
            partner_combinable_panel.DataProvider = new DataProvider<EquipsDebrisItem>(partner_combinableList);

            role_debris_panel.DataProvider = new DataProvider<EquipsDebrisItem>(roleList);
            partner_debris_panel.DataProvider = new DataProvider<EquipsDebrisItem>(partnerList);

            int role_combinableDebrisCount = role_combinable_panel.DataProvider.GetList().Count;
            int partner_combinableDebrisCount = partner_combinable_panel.DataProvider.GetList().Count;

            int roleDebrisCount = role_debris_panel.DataProvider.GetList().Count;
            int partnerDebrisCount = partner_debris_panel.DataProvider.GetList().Count;

            role_combinable_title_panel.SetActive(role_combinableDebrisCount > 0);
            partner_combinable_title_panel.SetActive(partner_combinableDebrisCount > 0);

            role_combinable_panel.gameObject.SetActive(role_combinableDebrisCount > 0);
            partner_combinable_panel.gameObject.SetActive(partner_combinableDebrisCount > 0);

            role_debris_title_panel.SetActive(roleDebrisCount > 0);
            role_debris_panel.gameObject.SetActive(roleDebrisCount > 0);
            partner_debris_title_panel.SetActive(partnerDebrisCount > 0);
            partner_debris_panel.gameObject.SetActive(partnerDebrisCount > 0);

            bool role_hasDebris = role_combinableDebrisCount > 0 || roleDebrisCount > 0;
            bool partner_hasDebris = partner_combinableDebrisCount > 0 || partnerDebrisCount > 0;

            role_special2_img.Visible(!role_hasDebris);
            partner_special2_img.Visible(!partner_hasDebris);
            
            role_panel2_panel.Visible(role_hasDebris);
            partner_panel2_panel.Visible(partner_hasDebris);
        }
        
        protected void rebuild_btn_OnClick()
        {
            //DecomposeMainUIView.showDecomposePanel(DecomposeType.RebornEquip);
        }

        protected void decomposition_btn_OnClick()
        {
            if (tabpage_panel.SelectTabIndex == 0 || tabpage_panel.SelectTabIndex == 1)
                DecomposeMainUIView.showDecomposePanel(DecomposeType.Equip);
            else
            {
                if(role_equip_panel.DataProvider.GetList().Count > 0 || partner_equip_panel.DataProvider.GetList().Count > 0)
                    UIManager.Instance.OpenUI(UIType.SellDebris_UI, new List<ItemUseEffectType>{ ItemUseEffectType.UseGet_DUST });
                else
                    GameShowNoticeUtil.ShowMsgByCodeID(10335);
            }
        }

        public override void Dispose()
        {
            Role.Instance.equip.EquipEnhanceCompelete -= refreshEquipList;
            Role.Instance.equip.EquipAdvanceCompelete -= refreshEquipList;
            Role.Instance.equip.EquipUpStarComplete -= refreshEquipList;
            Role.Instance.equip.EquipCombineCompelete -= refreshEquipList;
            Role.Instance.equip.EquipCombineCompelete -= refreshEquipDebrisList;
            Role.Instance.equip.EquipCombineCompleteWithUid -= this.EquipCombineComplete;
            Role.Instance.equip.EquipCombineCompleteWithId -= this.ExpEquipCombineComplete;

            Role.Instance.equip.EquipInfoUpdate -= refreshEquipList;

            rebuild_btn.onClick.RemoveListener(rebuild_btn_OnClick);
            decomposition_btn.onClick.RemoveListener(decomposition_btn_OnClick);

            SingletonFactory<EventManager>.Instance.RemoveEventListener(EvtAddSubUI, AddGetItemUI);
            Guide.UnBindRectransform(7003);
            ClickEventTrigger.Get(role_Debris_Btn.gameObject).onCClick = new UnityEngine.Events.UnityAction<GameObject>((obj) => { Guide.FinishClickOperation(7003); });
            
            base.Dispose();
        }

        private int equipedListSort(EquipsMediator.EquipServerData esd1, EquipsMediator.EquipServerData esd2)
        {
            if (esd1.SlotIndex == esd2.SlotIndex)
            {
                if (esd1.pos < esd2.pos) return -1;
                else if (esd1.pos > esd2.pos) return 1;
                else return 0;
            }
            else return esd1.SlotIndex < esd2.SlotIndex ? -1 : 1;
        }

        private int unEquipedListSort(EquipsMediator.EquipServerData esd1, EquipsMediator.EquipServerData esd2)
        {
            if (esd1.ExcelData.UseType == esd2.ExcelData.UseType)
            {
                if (esd1.Color == esd2.Color)
                {
                    if (esd1.pos == esd2.pos)
                    {
                        if (esd1.upgrade == esd2.upgrade)
                        {
                            if (esd1.adv == esd2.adv)
                            {
                                if (esd1.star == esd2.star)
                                {
                                    if (esd1.id < esd2.id) return -1;
                                    else if (esd1.id > esd2.id) return 1;
                                    else return 0;
                                }
                                else return esd1.star > esd2.star ? -1 : 1;
                            }
                            else return esd1.adv > esd2.adv ? -1 : 1;
                        }
                        else return esd1.upgrade > esd2.upgrade ? -1 : 1;
                    }
                    else return esd1.pos < esd2.pos ? -1 : 1;
                }
                else return esd1.Color > esd2.Color ? -1 : 1;
            }
            else return esd1.ExcelData.UseType < esd2.ExcelData.UseType ? -1 : 1; 
        }
        private void GetEquips(out List<EquipsMediator.EquipServerData> roleList, out List<EquipsMediator.EquipServerData> partnerList)
        {
            roleList = new List<EquipsMediator.EquipServerData>();
            List<EquipsMediator.EquipServerData> roleNonEquipResult = new List<EquipsMediator.EquipServerData>();
            partnerList = new List<EquipsMediator.EquipServerData>();
            List<EquipsMediator.EquipServerData> partnerNonEquipResult = new List<EquipsMediator.EquipServerData>();

            EquipsMediator.EquipServerData _equip;
            for (int i = 0; i < Role.Instance.equip.EquipServerDatas.Count; i++)
            {
                _equip = Role.Instance.equip.EquipServerDatas[i];
                if (_equip.IsRoleEquip)
                {
                    if (_equip.IsEquiped)
                        roleList.Add(_equip);
                    else
                        roleNonEquipResult.Add(_equip);
                }
                else
                {
                    if (_equip.IsEquiped)
                        partnerList.Add(_equip);
                    else
                        partnerNonEquipResult.Add(_equip);
                }

            }

            roleList.Sort(equipedListSort);

            roleNonEquipResult.Sort(unEquipedListSort);

            partnerList.Sort(equipedListSort);
            partnerNonEquipResult.Sort(unEquipedListSort);

            roleList.AddRange(roleNonEquipResult);
            partnerList.AddRange(partnerNonEquipResult);

            for (int i = 0, j = 0; i < roleList.Count || j < partnerList.Count; i++, j++)
            {
                if (roleList.Count > i) roleList[i].displayIdx = i;
                if (partnerList.Count > j) partnerList[j].displayIdx = j;
            }
        }

        private int EquipDebrisSort(EquipsDebrisItem i1, EquipsDebrisItem i2)
        {
            bool i1isMeetTheNeed = i1.isMeetTheNeed;
            if (i1isMeetTheNeed == i2.isMeetTheNeed)
            {
                int i1Color = i1.Color;
                int i2Color = i2.Color;
                if (i1Color == i2Color)
                {
                    long i1OwnNum = i1.OwnNum;
                    long i2OwnNum = i2.OwnNum;
                    if (i1OwnNum == i2OwnNum)
                    {
                        if (i1.Id < i2.Id) return -1;
                        else if (i1.Id > i2.Id) return 1;
                        else return 0;
                    }
                    else return i1OwnNum > i2OwnNum ? -1 : 1;
                }
                else return i1Color > i2Color ? -1 : 1;
            }
            else return i1isMeetTheNeed ? -1 : 1;
        }
        private void GetDebris(out List<EquipsDebrisItem> role_combinableList, out List<EquipsDebrisItem> partner_combinableList, out List<EquipsDebrisItem> roleList, out List<EquipsDebrisItem> partnerList)
        {
            role_combinableList = new List<EquipsDebrisItem>();
            partner_combinableList = new List<EquipsDebrisItem>();
            roleList = new List<EquipsDebrisItem>();
            partnerList = new List<EquipsDebrisItem>();
            List<EquipsDebrisItem> temp = Role.Instance.equip.debrisModule.alldebrisList.GetList() as List<EquipsDebrisItem>;

            foreach (EquipsDebrisItem item in temp)
            {
                if (item.isMeetTheNeed)
                {
                    if ( item.equipIsRole)
                        role_combinableList.Add(item);
                    else
                        partner_combinableList.Add(item);
                }
                else if (item.equipIsRole)
                {
                    roleList.Add(item);
                }
                else
                {
                    partnerList.Add(item);
                }
            }


            role_combinableList.Sort(EquipDebrisSort);
            partner_combinableList.Sort(EquipDebrisSort);

            roleList.Sort(EquipDebrisSort);
            partnerList.Sort(EquipDebrisSort);

            for (int i = 0,j = 0,k = 0, l = 0; i < role_combinableList.Count || j < partner_combinableList.Count || k < roleList.Count || l < partnerList.Count; i++, j++, k++, l++)
            {
                if (role_combinableList.Count > i) role_combinableList[i].displayIdx = i;
                if (role_combinableList.Count > j) role_combinableList[j].displayIdx = j;
                if (roleList.Count > k) roleList[k].displayIdx = k;
                if (partnerList.Count > l) partnerList[l].displayIdx = l;
            }
        }
        
    }

    public class EquipListItem : BaseListItem
    {
        EquipIcon bg_icon_img;
        TextMeshProUGUI level_txt;
        TextMeshProUGUI name_txt;
        TextMeshProUGUI aptitude_txt;
        TextMeshProUGUI aptitude_lbl;
        TextMeshProUGUI crit_txt;
        TextMeshProUGUI ownedheroname_txt;
        WaterText ownedheroname_txt_;
        TextMeshProUGUI crit_lbl;
        TextMeshProUGUI qualificationNum_txt;



        protected override void AwakeInit(object param_ = null)
        {
            base.AwakeInit();

            bg_icon_img = getUIComponent<EquipIcon>("bg_icon_img");

            //level_txt = getUIComponent<Text>("level_txt");
            name_txt = getUIComponent<TextMeshProUGUI>("name_txt");
            aptitude_txt = getUIComponent<TextMeshProUGUI>("aptitude_txt");
            aptitude_lbl = getUIComponent<TextMeshProUGUI>("aptitude_lbl");
            crit_txt = getUIComponent<TextMeshProUGUI>("crit_txt");
            ownedheroname_txt = getUIComponent<TextMeshProUGUI>("ownedheroname_txt");
            ownedheroname_txt_ = getUIComponent<WaterText>("ownedheroname_txt");
            crit_lbl = getUIComponent<TextMeshProUGUI>("crit_lbl");
            qualificationNum_txt = getUIComponent<TextMeshProUGUI>("qualificationNum_txt");

            ClickEventTrigger.Get(gameObject).onCClick = ItemOnClick;
        }

        protected override void FillData(object data = null)
        {
            base.FillData(data);

            EquipsMediator.EquipServerData vo = data as EquipsMediator.EquipServerData;

            bg_icon_img.showIcon(vo);

            name_txt.text = vo.NameWithAdvColor;
            //level_txt.text = vo.level.ToString();

            if (vo.ExcelMainPropType != null)
            {
                aptitude_txt.text = vo.getPropByType((EPropertyType)vo.ExcelMainPropType.Id).ToString();
                aptitude_lbl.text = vo.ExcelMainPropType.View + "：";
            }
            else
            {
                aptitude_txt.text = aptitude_lbl.text = string.Empty;
            }
            
            //if (vo.IsAccessEquip == true)
            //{
                if (vo.getExcelMainPropType(1) != null)
                {
                    this.crit_lbl.text = vo.getExcelMainPropType(1).View + "：";
                    this.crit_txt.text = vo.getPropByType((EPropertyType)vo.getExcelMainPropType(1).Id).ToString();
                }
            //    else
            //    {
            //        this.crit_lbl.text = "";
            //        this.crit_txt.text = "";
            //    }
            //}
            else
            {
                if (vo.AdvProp != null)
                {
                    this.crit_txt.text = vo.getPropByType((EPropertyType)vo.AdvProp.Id).ToString();
                    this.crit_lbl.text = vo.AdvProp.View + "：";
                }
                else
                {
                    this.crit_lbl.text = "";
                    this.crit_txt.text = "";
                }
            }
            if (crit_txt.text == "0")
            {
                this.crit_lbl.text = "";
                this.crit_txt.text = "";
            }

            if (ownedheroname_txt != null)
                ownedheroname_txt.text = vo.IsEquiped ? String.Format(Config.CodeTextData.AUTOSTR("{0}"), Role.Instance.partner.PartnerTeam.getTeamMemberBySlotIndex(vo.SlotIndex).heroData.HeroName) : "";

            if (ownedheroname_txt_ != null)
                ownedheroname_txt_.text = vo.IsEquiped ? String.Format(Config.CodeTextData.AUTOSTR("{0}"), Role.Instance.partner.PartnerTeam.getTeamMemberBySlotIndex(vo.SlotIndex).heroData.HeroName) : "";

            if (qualificationNum_txt!=null)
                qualificationNum_txt.text = vo.quality.ToString();

        }

        public override void Dispose()
        {
            ClickEventTrigger.Get(gameObject).onCClick = null;
            base.Dispose();
        }

        private void ItemOnClick(GameObject obj)
        {
            if (!(this.ui_data as EquipsMediator.EquipServerData).IsExpEquip)
            { 
                Role.Instance.equip.ShowUid = (this.ui_data as EquipsMediator.EquipServerData).uid;
                SingletonFactory<UIManager>.Instance.OpenUI(UIType.EquipmentsUI);
            }
        }
    }

    public class EquipDebrisListItem : BaseListItem
    {
        TextMeshProUGUI name_txt;
        TextMeshProUGUI reqnum_txt;
        Button Summon_btn;
        Button Gain_btn;
        UIPrefabCom icon_img;
        Icon80 icon;

        EquipsDebrisItem _evo;

        //string iconUrl = string.Empty;

        private void GuideBindRecttransform(bool isBind = true)
        { 
            if(_evo.displayIdx == 0)
            {
                if (isBind)
                    Guide.BindRectransform(7004, Summon_btn.transform as RectTransform);
                else
                    Guide.UnBindRectransform(7004);
            }   
        }

        public override void InitUIView(object obj)
        {
            base.InitUIView(obj);
        }

        protected override void AwakeInit(object param_ = null)
        {
            base.AwakeInit();
            name_txt = getUIComponent<TextMeshProUGUI>("name_txt");
            reqnum_txt = getUIComponent<TextMeshProUGUI>("reqnum_txt");
            Summon_btn = getUIComponent<Button>("Summon_btn");
            Gain_btn = getUIComponent<Button>("Gain_btn");
            icon_img = getUIComponent<UIPrefabCom>("icon_img");

            UIEventTriggerListener.Get(Summon_btn.gameObject).onClick = Summon_btn_OnClick;
            UIEventTriggerListener.Get(Gain_btn.gameObject).onClick = Gain_btn_OnClick;
            icon = icon_img.CreatPreFabComponetInstanceToThis(typeof(Icon80)) as Icon80;
            icon.piece_img.Visible(true);
            //icon.gameObject.AddComponent<ClickEventTrigger>().onClick += this.icon_OnClick;
            ClickEventTrigger.Get(icon.gameObject).onCClick += this.icon_OnClick;

            SingletonFactory<EventManager>.Instance.AddEventListener(BagMediator.BagMediatorItemAddUpdate, RefreshWhenItemUpdateAndLoad);
        }

        public override void Dispose()
        {
            UIEventTriggerListener.Get(Summon_btn.gameObject).onClick -= Summon_btn_OnClick;
            UIEventTriggerListener.Get(Gain_btn.gameObject).onClick -= Gain_btn_OnClick;
            ClickEventTrigger.Get(icon.gameObject).onCClick -= this.icon_OnClick;
            GuideBindRecttransform(false);
            SingletonFactory<EventManager>.Instance.RemoveEventListener(BagMediator.BagMediatorItemAddUpdate, RefreshWhenItemUpdateAndLoad);

            base.Dispose();
        }

        private void RefreshWhenItemUpdateAndLoad()
        {
            if (_evo.isMeetTheNeed)
            {
                reqnum_txt.text = WaterGameConst.ChangeTextColor(TextColorType.green, string.Format("{0}/{1}", _evo.OwnNum, _evo.ReqNum));
                Summon_btn.Visible(true);
                Gain_btn.Visible(false);
            }
            else
            {
                reqnum_txt.text = WaterGameConst.ChangeTextColor(TextColorType.red, string.Format("{0}/{1}", _evo.OwnNum, _evo.ReqNum));
                Summon_btn.Visible(false);
                Gain_btn.Visible(true);
            }
        }

        protected override void FillData(object vo = null)
        {
            base.FillData(vo);
            
            EquipsDebrisItem data = (EquipsDebrisItem)vo;
            _evo = data;
            name_txt.text = data.NameWithColor;

            //if (data.IconPath != iconUrl)
            //{
            //    iconUrl = data.IconPath;
            //    icon.icon_img.Visible(false);
            //    XAsset.Assets.LoadWithOwner<Sprite>(data.IconPath, (sprite) =>
            //    //SingletonFactory<AssetManager>.Instance.LoadAsync<Sprite>(data.IconPath, (sprite) =>
            //    {
            //        icon.icon_img.sprite = sprite;
            //        icon.icon_img.Visible(true);
            //    }, this.gameObject, true);
            //    //IconItemBase.SetColor(icon.quality_panel.GetChild(0), data.Color);
            //    icon.SetColor(data.Color);
            //}
            icon.LoadIcon_ImgSprite(data.IconPath);
            icon.SetColor(data.Color);

            RefreshWhenItemUpdateAndLoad();
            GuideBindRecttransform();
        }

        
        private bool isBatchCompose(){
            return _evo.OwnNum/_evo.ReqNum > 1;
        }

        private void Summon_btn_OnClick(GameObject obj)
        {
            if(_evo.isMeetTheNeed){
                if(isBatchCompose()){
                    LuaScriptMgr.Instance.CallLuaFunction_Void_Int ("water.ShowBatchComposeItem", (int)_evo.Id);  
                }else{
                    Role.Instance.equip.summonEquip((int)((EquipsDebrisItem)data)._config.Effect_Num1);
                }
            }
           
        }

        private void Gain_btn_OnClick(GameObject obj)
        {
            icon_OnClick(obj);
        }

        private void icon_OnClick(GameObject obj)
        {
            ReqItem ri = _evo as ReqItem;
            SingletonFactory<EventManager>.Instance.DispatchEvent(EquipmentsListUIView.EvtAddSubUI, ri);
        }

    }
}
