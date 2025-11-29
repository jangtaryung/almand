using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Water.Config;
using Game.Util;

namespace Water
{

    public class EquipmentsInfoBaseUIView:BaseView
    {
        protected TextMeshProUGUI name_txt;
        protected StarBar starlist_panel;
        protected EquipIcon icon_panel;
        protected Image huawei_img;

        protected TextMeshProUGUI adv_info1_lbl;
        protected TextMeshProUGUI adv_info1_txt;
        protected TextMeshProUGUI adv_info3_lbl;
        protected TextMeshProUGUI adv_info3_txt;
        protected TextMeshProUGUI adv_info2_lbl;
        protected TextMeshProUGUI adv_info2_txt;
        protected TextMeshProUGUI adv_info4_txt;

        protected Transform info_panel;

        protected TextMeshProUGUI talent_txt;
        protected Transform equiptalent_panel;

        protected TextMeshProUGUI desc_txt;


        protected Transform bottom_panel;

        protected Button change_btn;

        protected Button unload_btn;

        protected Button strength_btn;

        protected Button class_btn;

        protected Button starup_btn;

        protected Button close_btn;


        protected EquipsMediator.EquipServerData m_equip_data;

        public EquipmentsInfoBaseUIView(UIType uitype_)
            : base(uitype_)
        {

        }

        public override void InitUIView(object param_)
        {
            base.InitUIView(param_);

            name_txt = getUIComponent<TextMeshProUGUI>("name_txt");
            starlist_panel = addVirtualUIComponent<StarBar>("starlist_panel");
            icon_panel = getUIComponent<EquipIcon>("icon_panel");
            huawei_img = getUIComponent<Image>("huawei_img");

            adv_info1_lbl = getUIComponent<TextMeshProUGUI>("adv_info1_lbl");
            adv_info1_txt = getUIComponent<TextMeshProUGUI>("adv_info1_txt");
            adv_info3_lbl = getUIComponent<TextMeshProUGUI>("adv_info3_lbl");
            adv_info3_txt = getUIComponent<TextMeshProUGUI>("adv_info3_txt");
            adv_info2_lbl = getUIComponent<TextMeshProUGUI>("adv_info2_lbl");
            adv_info2_txt = getUIComponent<TextMeshProUGUI>("adv_info2_txt");
            adv_info4_txt = getUIComponent<TextMeshProUGUI>("adv_info4_txt");

            info_panel = TransformUtil.FindChildAccurately(this.transform, "info_panel");

            equiptalent_panel = TransformUtil.FindChildAccurately(this.transform, "equiptalent_panel");
            talent_txt = getUIComponent<TextMeshProUGUI>("talent_txt");


            desc_txt = getUIComponent<TextMeshProUGUI>("desc_txt");

            bottom_panel = getUIComponent<Transform>("bottom_panel");
            change_btn = getUIComponent<Button>("change_btn");
            unload_btn = getUIComponent<Button>("unload_btn");
            strength_btn = getUIComponent<Button>("strength_btn");
            class_btn = getUIComponent<Button>("class_btn");
            close_btn = getUIComponent<Button>("close_btn");
            starup_btn = getUIComponent<Button>("starup_btn");
             

            change_btn.onClick.AddListener(change_btn_OnClick);
            unload_btn.onClick.AddListener(unload_btn_OnClick);
            strength_btn.onClick.AddListener(strength_btn_OnClick);
            class_btn.onClick.AddListener(class_btn_OnClick);
            starup_btn.onClick.AddListener(starup_btn_OnClick);
            close_btn.onClick.AddListener(close_btn_OnClick);
            
            SetShowEquip();
            Guide.BindRectransform(3008, class_btn.transform as RectTransform);
            Guide.BindRectransform(3013, starup_btn.transform as RectTransform);

            
            //starup_btn.Visible(ModuleUnlock.IsModuleUnlocked(ModuleId.EquipUpstar));
        }

        public override void Dispose()
        {
            Guide.UnBindRectransform(3008);
            Guide.UnBindRectransform(3013);
            base.Dispose();
        }

        protected void CloseWhenAble()
        {
            if (UIEffectUtil.isUIClickable(close_btn.transform as UnityEngine.RectTransform))
                close_btn.onClick.Invoke();
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            Main.Instance.phoneDevice.registerBackHandler(CloseWhenAble);
        }

        protected override void OnExit()
        {
            Main.Instance.phoneDevice.unRegisterBackHandler(CloseWhenAble);
            base.OnExit();
        }


        protected  void SetShowEquip()
        {
  
            name_txt.text = WaterGameConst.GetColorTextStrByColorInt(m_equip_data.NameWithAdv, m_equip_data.Color);
            starlist_panel.ShowStar(m_equip_data.star);
            showIcon(m_equip_data);
            
            loadProp(m_equip_data);
            showTalent(m_equip_data);
            //showSuitPanel(m_equip_data);
            desc_txt.text = m_equip_data.ExcelData.Desc;
            IconItemBase.SetColorByColorValue(huawei_img, m_equip_data.Color);

            if (!m_equip_data.CanAdvance || !ModuleUnlock.IsModuleUnlocked(ModuleId.EquipAdvance))
                class_btn.Visible(false);
            if (!m_equip_data.CanStarUp || !ModuleUnlock.IsModuleUnlocked(ModuleId.EquipUpstar))
            {
                starup_btn.Visible(false);
            }
            strength_btn.Visible(m_equip_data.CanStrengthen && ModuleUnlock.IsModuleUnlocked(ModuleId.EquipmentsEnhance));
        }

        private void showIcon(EquipsMediator.EquipServerData esd_)
        {
            icon_panel.showIconWithoutStar(esd_);
        }

        private void loadProp(EquipsMediator.EquipServerData esd_)
        {
            if (esd_.IsExpEquip)
            {
                info_panel.Visible(false);
                return;
            }
            adv_info1_lbl.text = string.Format("{0}：",esd_.ExcelMainPropType.View);
            adv_info1_txt.text = esd_.mainProp.ToString();
            if (esd_.getExcelMainPropType(1) != null)
            {
                adv_info3_lbl.Visible(true);
                adv_info3_lbl.text = string.Format("{0}：", esd_.getExcelMainPropType(1).View);
                adv_info3_txt.text = esd_.getMainProp(1).ToString();
            }
            adv_info2_lbl.text = string.Format("{0}：", esd_.AdvProp.View);
            adv_info2_txt.text = esd_.getPropByType((EPropertyType)esd_.AdvProp.Id).ToString();

            adv_info2_txt.Visible(adv_info2_txt.text != "0");
            adv_info2_lbl.Visible(adv_info2_txt.text != "0");

            adv_info4_txt.text = esd_.quality.ToString();
        }

        /// <param name="esd_"></param>
        public void showTalent(EquipsMediator.EquipServerData esd_)
        {
            List<EquipStarTalent> talents = esd_.starModule.getAllTalents();
            if (talents.Count == 0)//.starModule.getMaxStar <= 0)
            {
                equiptalent_panel.Visible(false);
                return;
            }
            talent_txt.text = "";
            int i = 0;
            
            for (; i < talents.Count; i++)
            {
                if (talents[i].isActive)
                {
                    talent_txt.text += WaterGameConst.ChangeTextColor(TextColorType.green, string.Format("{0}\n", talents[i].Desc));
                }
                else
                {
					talent_txt.text += WaterGameConst.ChangeTextColor(TextColorType.gray, string.Format(Water.Config.CodeTextData.AUTOSTR("{0}({1})\n"), talents[i].Desc, EquipStarModule.getColorText(talents[i].reqStar)));
                }
            }
        }


        protected void ActiveSuit(Transform tran, EquipsMediator.EquipServerData esd)
        {

        }

        private void change_btn_OnClick()
        {
            if (Role.Instance.equip.IsAnyEquipBetterThan(m_equip_data, false))//  Role.Instance.equip.GetChangeEquipCount(m_equip_data.pos, m_equip_data.IsRoleEquip, false, m_equip_data.ExcelData.Potential) > 0)
            {
                SingletonFactory<UIManager>.Instance.CloseUI(UIType.EquipmentsInfoUI);
                SingletonFactory<UIManager>.Instance.OpenUI(UIType.ChangeEquipmentsUI);
            }
            else
            {
                GameShowNoticeUtil.ShowMsgByCodeID(33003);
            }
        }

        private void unload_btn_OnClick()
        {
            Role.Instance.partner.EquipHero(EquipConst.UnEquipUid);
            Role.Instance.partner.WFS_EquipHero.Start(this.UnLoadComplete);
        }

        private void UnLoadComplete()
        {
            this.close_btn_OnClick();
        }

        protected virtual void strength_btn_OnClick()
        {
            SingletonFactory<UIManager>.Instance.OpenUI(UIType.EquipmentsUI, EEquipAttributesTab.Enhance);
            //this.close_btn_OnClick();
        }

        private void class_btn_OnClick()
        {
            SingletonFactory<UIManager>.Instance.OpenUI(UIType.EquipmentsUI, EEquipAttributesTab.Advance);
            //this.close_btn_OnClick();
            Guide.FinishClickOperation(3008);
        }

        private void starup_btn_OnClick()
        {
            SingletonFactory<UIManager>.Instance.OpenUI(UIType.EquipmentsUI, EEquipAttributesTab.Upstar);
            //this.close_btn_OnClick();
            Guide.FinishClickOperation(3013);
        }

        protected void close_btn_OnClick()
        {
            SingletonFactory<UIManager>.Instance.CloseUI(this.ViewType);
        }

    }

}