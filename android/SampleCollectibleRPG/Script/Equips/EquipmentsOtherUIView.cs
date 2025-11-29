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

namespace Water
{
    public class EquipmentsOtherUIView : EquipmentsBaseUIView
    {

        public EquipmentsOtherUIView()
            : base(UIType.EquipmentsOtherUI)
        {

        }

        private EquipsMediator.EquipServerData _esd;
        public override void InitUIView(object param_)
        {
            base.InitUIView(param_);

            tabpage_panel.tab_panel.gameObject.SetActive(false);

            _esd = param_ as EquipsMediator.EquipServerData;
            //refreshOnce(_esd.uid);
            initBaseInfoView();

            this.skipJinJieAnim.Visible(false);
        }

        //protected override void onBaseInfoInited()
        //{
        //    base.onBaseInfoInited();

        //    refreshBaseView(_esd);
        //}
    }
}
