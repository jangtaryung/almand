using UnityEngine;

namespace Water
{
    /// <summary>
    /// equipments information ui view
    /// </summary>
    public class EquipmentsInfoUIView : EquipmentsInfoBaseUIView
    {
        PlayMakerFSM fsm;

        public EquipmentsInfoUIView()
            :base(UIType.EquipmentsInfoUI)
        {
            
        }

        public override void InitUIView(object param_)
        {
            object[] values = param_ as object[];
            
            fsm = getUIComponent<PlayMakerFSM>("views_panel");
            if (values != null)
            {
                int idType = (int)values[0]; // 0 -uid 1-itemId
                int id = (int)values[1];

                if (idType == 0)
                {
                    Role.Instance.equip.ShowUid = (int)id;
                    fsm.SendEvent("go");

                    m_equip_data = Role.Instance.equip.GetEquipServerDataByUid(Role.Instance.equip.ShowUid);
                }
                else
                {
                    m_equip_data = Role.Instance.equip.GetExpEquipServerDataById(id);
                }
            }
            else 
            {
                PartnerMediator pm = Role.Instance.partner;
                Role.Instance.equip.ShowUid = pm.PartnerTeam.getTeamMemberBySlotIndex(pm.PartnerTeam.slotIndex).equipData[pm.PartnerTeam.equipSlotIndex].equipUid;

                m_equip_data = Role.Instance.equip.GetEquipServerDataByUid(Role.Instance.equip.ShowUid);
            }
            

            base.InitUIView(param_);

            if (param_ != null) 
            {
                change_btn.Visible(false);
                unload_btn.Visible(false);
            }
            else
            {
                RedDotReminderHandle.RedDotHandle(m_equip_data.IsCanRedDotReminder(RedDotHandleID.Slot_Equip_CanChangeBatterEquip_ID), change_btn.transform);
                RedDotReminderHandle.RedDotHandle(m_equip_data.IsCanRedDotReminder(RedDotHandleID.Slot_Equip_StarUp_ID), starup_btn.transform);
                RedDotReminderHandle.RedDotHandle(m_equip_data.IsCanRedDotReminder(RedDotHandleID.Slot_Equip_Enhance_ID), strength_btn.transform);
                RedDotReminderHandle.RedDotHandle(m_equip_data.IsCanRedDotReminder(RedDotHandleID.Slot_Equip_AdvanceUp_ID), class_btn.transform);
            }

            SingletonFactory<EventManager>.Instance.AddEventListener(EquipmentsUIView.EvtEquipmentsOpen, close_btn_OnClick);

            Guide.BindRectransform(3006, strength_btn.transform as RectTransform);
        }

        public override void Dispose()
        {
            SingletonFactory<EventManager>.Instance.RemoveEventListener(EquipmentsUIView.EvtEquipmentsOpen, close_btn_OnClick);

            Guide.UnBindRectransform(3006);

            base.Dispose();
        }

        protected override void strength_btn_OnClick(){
            base.strength_btn_OnClick();

            Guide.FinishClickOperation(3006);
        }
    }
}
