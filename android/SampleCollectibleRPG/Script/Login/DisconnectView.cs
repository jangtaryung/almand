using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;
using UnityEngine;

namespace Water
{
    public enum DisconnectType
    {
        None,
        Retry,
        BackCity,
        BackLoginAndRetry,
    }

    public class DisconnectParam
    {
        public DisconnectType Type;
        public DisconnectReason Reason;
        public int errorCode;
        public string errInfo;
        public Action OnRetry;
        public Action OnBackCity;
        public Action OnBackLogin;
    }

    public class DisconnectView : BaseView
    {
        Button btn;
        public override HoverViewType ViewHoverType { get { return HoverViewType.UnShow; } }

        public DisconnectView() : base(UIType.DisconnectViewUI)
        {
        }

        protected override void AwakeInit(object param = null)
        {
            base.AwakeInit();
            var value = param as DisconnectParam;

            // hide all buttons
            WaterButton[] btns = this.gameObject.GetComponentsInChildren<WaterButton>(false);
            if (btns != null)
            {
                foreach (var btn in btns) btn.gameObject.SetActive(false);
            }

            switch (value.Type)
            {
                case DisconnectType.Retry:
                    ShowText(Config.CodeTextData.AUTOSTR("10010108"), value);
                    ShowRetryBtn(value.OnRetry);
                    break;
                case DisconnectType.BackLoginAndRetry:
                    ShowText(Config.CodeTextData.AUTOSTR("10010107"), value);
                    ShowBackLoginBtn(value.OnBackLogin);
                    ShowRetryBtn(value.OnRetry);
                    break;
                case DisconnectType.BackCity:
                    ShowText(Config.CodeTextData.AUTOSTR("10010106"), value);
                    ShowBackCityBtn(value.OnBackCity);
                    break;
            }
        }

        protected void CloseWhenAble()
        {
            if (UIEffectUtil.isUIClickable(btn.transform.GetChild(0) as UnityEngine.RectTransform))
                btn.onClick.Invoke();
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


        private void ShowBackCityBtn(Action onBackCity)
        {
            ShowButton("fanhuizhucheng_btn", () =>
            {
                if(onBackCity != null) { onBackCity(); }
                SingletonFactory<EventManager>.Instance.DispatchEvent(Battle.BattleScene.BATTLE_EVT_CLOSE, Water.Game.EBattleEndType.eToCity);

                //SingletonFactory<UIManager>.Instance.CloseUI(isPop: false);
                //SingletonFactory<UIManager>.Instance.CloseUI(this.ViewType);
                //GameSceneSwitcher.changeScene(GameScene.E_MAINCITY);
            });
        }

        private void ShowRetryBtn(Action onRetry)
        {
            ShowButton("chongshi_btn", () =>
            {
                if (onRetry != null) { onRetry(); }
                SingletonFactory<UIManager>.Instance.CloseUI(this.ViewType);
            });
        }

        private void ShowBackLoginBtn(Action onBackLogin)
        {
            ShowButton("fanhuidenglu_btn", () =>
            {
                if(onBackLogin != null) { onBackLogin(); }
                Role.Instance.logoutGameServer();
                SingletonFactory<UIManager>.Instance.CloseUI(this.ViewType);
            });
        }

        private void ShowText(string msg, DisconnectParam param)
        {
            var txt = getUIComponent<TextMeshProUGUI>("tishi1_txt");
            txt.text = string.Format("{0}\n[{1}_{2}]",msg, param.Reason, param.errInfo);
            txt.gameObject.SetActive(true);
        }

        private void ShowButton(string name, UnityAction onClick)
        {
            btn = getUIComponent<Button>(name);
            btn.gameObject.SetActive(true);
            btn.onClick.AddListener(onClick);
        }
    }
}