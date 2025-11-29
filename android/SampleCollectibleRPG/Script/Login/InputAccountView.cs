using System.Collections.Generic;
using System.Collections;
using Game.Util;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Water
{

    public class InputAccountView : BaseView
    {
		TMP_InputField acount_InputField;
		Button Login_btn;
		Button signup_btn;
        Button fast_login_btn;

        public InputAccountView()
            : base(UIType.InputAccountUI){}

        public override void InitUIView(object param_ = null)
        {
            base.InitUIView(param_);

            acount_InputField = getUIComponent<TMP_InputField>("acount_InputField");

            Login_btn = getUIComponent<Button>("Login_btn");
            signup_btn = getUIComponent<Button>("signup_btn");
            fast_login_btn = getUIComponent<Button>("kuaisu_btn");
            fast_login_btn.Visible(false);
            getUIComponent<TMP_InputField>("password_InputField").Visible(false);
            getUIComponent<TextMeshProUGUI>("passwordLabel").Visible(false);

            Login_btn.onClick.AddListener(onLoginClicked);

            signup_btn.Visible(false);
            RestoreAcountAndPwd();
        }


        private void RestoreAcountAndPwd()
        {
            string prevAccount = GameConfig.Acount;
            if (!string.IsNullOrEmpty(prevAccount))
            {
                acount_InputField.text = prevAccount;
            }
        }

		void onLoginClicked()
		{
            LoginSystem.Instance.isInitNormalServer = true;
            LoginSystem.Instance.last_login_time = -1; 
            if(string.IsNullOrEmpty(acount_InputField.text)){
                MessageBoxUtils.ShowMessgeBox(Config.CodeTextData.AUTOSTR("10010105"));
                return ;
            }
            //Water.ChannelManager.sendChannelEvent("dian_ji_jin_ru_you_xi");
			ChannelManager.uid = acount_InputField.text;
            
            GameConfig.SaveAcount(ChannelManager.uid);
            PersistentEventManager.Instance.DispatchEvent(LoginSystem.DebugLoginSucessed,null);
            Exit();
		}
    }
}