using UnityEngine.UI;
using TMPro;

namespace Water{
	public class LoginCDKUIView: BaseView {
		public override bool IsShowBG
        {
            get
            {
                return false;
            }
        }

		Button closeBtn;
		WaterButton sureBtn;
		TMP_InputField acountInputField; 

		public LoginCDKUIView(): base(UIType.LoginCDKUI){}

		public override void InitUIView(object param_){
			base.InitUIView(param_);

			acountInputField = getUIComponent<TMP_InputField> ("acount_InputField");
			sureBtn  = getUIComponent<WaterButton> ("sure_btn");
			sureBtn.onClick.AddListener (OnClickSureBtn);
			closeBtn = getUIComponent<WaterButton>("close_btn");
			closeBtn.onClick.AddListener(onCloseClicked);
		}

		public override void Dispose(){
			base.Dispose();

			closeBtn = null;
		}

		void OnClickSureBtn()
		{
            string strTemp = acountInputField.text;//.Trim();
          //  UnityEngine.Debug.LogError("Before Trim : " + strTemp);
            strTemp = strTemp.Replace("\r", "");
            strTemp = strTemp.Replace("\n", "");
            strTemp = strTemp.Replace("\t", "");
            strTemp = strTemp.Replace(" ", "");
            //UnityEngine.Debug.LogError("After Trim :" + strTemp);
            string cdk = strTemp;

			// LoginSystem.Instance.SendLoginCDK (cdk);
		}

		void onCloseClicked(){
			Exit();
		}
	}
}
