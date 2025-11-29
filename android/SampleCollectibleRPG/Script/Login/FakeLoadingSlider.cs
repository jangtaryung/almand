using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
using TMPro;

namespace Water{
	public class FakeLoadingSlider : UIBaseComponent {
		Slider sld;
		TextMeshProUGUI	txt;
		
		public override void InitUIView(object param_){
			base.InitUIView(param_);
        
			txt = getUIComponent<TextMeshProUGUI>("tips_txt");
            sld = getUIComponent<Slider>("laoding_di_img");
            sld.value = 0;
            sld.maxValue = 1f;
		}

		public void startLoading(float secs_){
			sld.value = 0;
			sld.DOValue(1, secs_).SetEase(Ease.InOutSine);
		}

		public IEnumerator finishLoading(){
            if (sld.value >= 0.99f)
				yield break;

			float fastForwardSecs = 0.1f;
			DOTween.Kill(sld);
            sld.DOValue(1, fastForwardSecs);

			yield return new WaitForSeconds(fastForwardSecs);
		}

		public void loadingTo(float amount_, float secs_){
			DOTween.Kill(sld);
            sld.DOValue(amount_, secs_);
		}

		public void loadingTo(float amount_){
            sld.value = amount_;
		}

		public void showTips(string info_){
			if(txt)
				txt.text = info_;
		}
	}

}
