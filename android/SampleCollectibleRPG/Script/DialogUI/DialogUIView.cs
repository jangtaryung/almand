using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Water.Config;
using Game.Util;
using System;
using LuaInterface;
using Water.Battle;
using System.Collections;

namespace Water
{
    public class DialogUIViewParam
    {
        public List<DialogData> Dialogs;
        public Action OnFinished;
        public GameObject owner;
        public bool hasOwner = false;
        public int id;
        public static DialogUIViewParam Create(int id, Action onFinish, GameObject owner = null)
        {
            DialogUIViewParam dialogParam = new DialogUIViewParam();
            var dialogs = new List<DialogData>();
			dialogParam.id = id;
            var itor = DialogueData.Load().GetEnumerator();
            while (itor.MoveNext())
            {
                var item = itor.Current.Value;
                if (item.Series == id)
                {

                    if (1 <= item.Role && item.Role <= 3)
                    {
                        var role = Role.Instance;
                        var index = item.Role - 1;
                        var teamMate = role.partner.PartnerTeam.getTeamMemberBySequenceIndex(index);
                        var heroID = teamMate.Id == 0 ? role.hero.RoleHero.Id : teamMate.Id;
                        index = teamMate.Id == 0 ? 0 : index;
						var config = HeroData.GetData((uint)heroID);
                        var configModel = UIAssetPath.GetFullModelUrl(config.UIModelId);

                        dialogs.Add(new DialogData
                        {
                            ModelURL = index == 0 ? role.hero.RoleHero.UIModelUrl : configModel,
                            Name = index == 0 ? role.baseInfo.name : config.Name,
							IsMainRole = index == 0 ? true:false,
                            Pos = item.pos,
                            Order = item.Order,
                            Text = item.Dialogue,
                            Scale = config.Scaling,
                            OffsetAngle = config.OffsetAngle,
                            dialogId =(int)item.Series,
                        });
                    }
                    else
                    {
                        var config = NpcData.GetData((uint)item.Role);
                        dialogs.Add(new DialogData
                        {
                            ModelURL = UIAssetPath.GetFullModelUrl(config.UIModelId),
                            Name = config.Name,
                            Pos = item.pos,
                            Order = item.Order,
                            Text = string.Format(item.Dialogue, Role.Instance.baseInfo.OldName),
                            Scale = config.Scaling,
                            OffsetAngle = config.OffsetAngle,
                            dialogId =(int)item.Series,
                        });
                    }
                }
            }

            dialogs.Sort((DialogData x, DialogData y) => { return x.Order - y.Order; });
            dialogParam.OnFinished = onFinish;
            dialogParam.Dialogs = dialogs;
            if(owner){
                dialogParam.hasOwner = true;
                dialogParam.owner = owner;
            }
            return dialogParam;
        }
    }

    public class DialogData
    {
        public string Name;
        public string Text;
        public string ModelURL;
        public int Order;
        public int Pos;
        public int Scale;
        public int OffsetAngle;
		public bool IsMainRole = false; 
        public int dialogId;
    }

    public class DialogTextPanel
    {
        private float _textSpeed;
        private GameObject root;
        private TextMeshProUGUI _text;
        private TextMeshProUGUI _name;
        private GameObject _modelRoot;
        private Dictionary<string, GameObject> _models;
        private HeroShowCom _heroShowCom;
        private Animator _npc_Ani;

        public bool isFinish = true;

        public DialogTextPanel(GameObject root, TextMeshProUGUI text,
            GameObject modelRoot, TextMeshProUGUI name, HeroShowCom herocom, Animator npc_Ani)
        {
            this.root = root;
            _heroShowCom = herocom;
            _text = text;
            _modelRoot = modelRoot;
            _name = name;
            _npc_Ani = npc_Ani;
            _textSpeed = float.Parse(WaterGameConst.GetPropertyValueByPropertyId(TablePropertyId.DialogTextSpeed));
            _models = new Dictionary<string, GameObject>();
        }

        public void SetText(string text, bool ifAuto)
        {
            DOTween.Kill(_text);
            _text.text = string.Empty; 
            isFinish = false;
            if (ifAuto)
            {
                _text.text = text;
            }
            else
            {
                _text.DoText(text, text.Length * _textSpeed,()=>{
                    isFinish= true;
                });
            }
        }

        public void breakAnim(){
            isFinish = true;
            DOTween.Kill(_text,true);
        }

        public void SetModel(string modelURL, int modelScale, int offsetAngle, bool isMainRole, int modelYPosition = 0)
        {
            var scale = _heroShowCom.GetScale(modelScale);
            var rotation = _heroShowCom.GetRotation(offsetAngle);
            if (isMainRole == false)
            {
                Vector3 pos = new Vector3(0, modelYPosition, 0);
                _heroShowCom.showModelByUrl(modelURL, scale, pos, rotation);
            }
            else
            {
				if (BattleScene.Inst != null && GameSceneSwitcher.curScene == GameScene.E_BATTLE )
                {
                    var team = BattleProxy.instance.FindTeam(c => c.TeamID == BattleScene.Inst.teamID);
                    _heroShowCom.ShowRoleByAvatarShowInfo(team.TeamData.PlayerData.AvatarShowInfo);
                }
                else
                {
                    _heroShowCom.ShowRoleHero(null);
                }
            }
        }

        public void PlayNpcAni()
        {
            _npc_Ani.SetTrigger("jin");
        }

        public void SetAcitve(bool active) { root.SetActive(active); }
        public void SetName(string name) { _name.text = name; }
    }

    public class DialogUIView : BaseView
    {
        public static bool hasDialogView(){
            var layer = UIManager.Instance.GetParentLayer(EUILayer.Dialog);
            if(layer == null || !layer.transform) return false;

            return layer.transform.childCount > 0;
        }

        private float WaitTime = float.Parse(PropertyData.GetData(8067).Value);
        public override HoverViewType ViewHoverType { get { return HoverViewType.UnShow; } }
        private DialogTextPanel _rightPanel;
        private DialogTextPanel _leftPanel;
        private int _lastSetPos = -1;
        private string _lastSetModelURL = "";
        private DialogTextPanel _curShowPanel;
        private Action _onFinished;
        private bool hasOwner;
        private GameObject owner;
        private List<DialogData> _dialogs;
        private int _curIndex;
        private float _curStepTime;
        private int dialogId;

        Button skipBtn;

        // Button nextBtn;

        // TextMeshProUGUI next_btn_text;

        public DialogUIView()
            : base(UIType.DialogUI)
        {
        }

        public void sendTalkingDataEventById(int id, bool isSkip = false)
        {
            if (id != 5000001
                && id != 5000002
                //&& id != 5000003
                //&& id != 5000004
                //&& id != 5000005
                //&& id != 5000006
                //&& id != 5000007
                )
                return;

            if (!isSkip)
            {
                //Water.ChannelManager.SendChannelEventGuide("dian_ji_ren_wu_" + id + "_dui_hua_" + _curIndex, id);
                Water.ChannelManager.NewAFDotGuide("click_task_" + id + "_dialog_" + _curIndex);
            }
            else
            {
                for(int i = _curIndex; i < _dialogs.Count; i++)
                {
                    //Water.ChannelManager.SendChannelEventGuide("dian_ji_ren_wu_" + id + "_dui_hua_" + i, id);
                    Water.ChannelManager.NewAFDotGuide("click_task_" + id + "_dialog_" + i);
                }
            }
        }

        public override void InitUIView(object param)
        {
            var tempParam = param as DialogUIViewParam;
            _onFinished = tempParam.OnFinished;
            hasOwner = tempParam.hasOwner;
            owner = tempParam.owner;
            dialogId = tempParam.id;

            base.InitUIView(param);

            _dialogs = tempParam.Dialogs;

            var trans = transform;
            var rightRoot = TransformUtil.FindChildAccurately(trans, "Right_panel").gameObject;
            var rightNpcModelRoot = TransformUtil.FindChildAccurately(trans, "npc_1_panel").gameObject;
            var rightText = TransformUtil.FindChildAccurately(trans, "message_1_txt").GetComponent<TextMeshProUGUI>();
            var rightNpcName = TransformUtil.FindChildAccurately(trans, "npc_name_1_txt").GetComponent<TextMeshProUGUI>();
            var rightherocom = this.getUIComponent<HeroShowCom>("npc_1_panel");
            var _rightPanel_npc_Ani = this.getUIComponent<Animator>("npc_1_panel");
            _rightPanel = new DialogTextPanel(rightRoot, rightText, rightNpcModelRoot, rightNpcName, rightherocom, _rightPanel_npc_Ani);
            
            

            var leftRoot = TransformUtil.FindChildAccurately(trans, "Lift_panel").gameObject;
            var leftNpcModelRoot = TransformUtil.FindChildAccurately(trans, "npc_2_panel").gameObject;
            var leftText = TransformUtil.FindChildAccurately(trans, "message_2_txt").GetComponent<TextMeshProUGUI>();
            var leftNpcName = TransformUtil.FindChildAccurately(trans, "npc_name_2_txt").GetComponent<TextMeshProUGUI>();
            var leftherocom = this.getUIComponent<HeroShowCom>("npc_2_panel");
            var _lefttPanel_npc_Ani = this.getUIComponent<Animator>("npc_2_panel");
            _leftPanel = new DialogTextPanel(leftRoot, leftText, leftNpcModelRoot, leftNpcName, leftherocom, _lefttPanel_npc_Ani);
           
            TransformUtil.FindChildAccurately(trans, "exp_img").gameObject.SetActive(false);
            TransformUtil.FindChildAccurately(trans, "gold_img").gameObject.SetActive(false);
            TransformUtil.FindChildAccurately(trans, "operation_1_btn").gameObject.SetActive(false);
            TransformUtil.FindChildAccurately(trans, "operation_2_btn").gameObject.SetActive(false);

            // next_btn_text = TransformUtil.FindChildAccurately(trans,"next_btn");
            // next_btn_text = this.getUIComponent<TextMeshProUGUI>("next_btn");

            skipBtn = TransformUtil.FindChildAccurately(trans, "skip_btn").GetComponent<Button>();
            skipBtn.onClick.AddListener(onSkipBtnClick);
            var nextBtn = GetComponent<Button>();
            nextBtn.onClick.AddListener(()=> { MoveNext(); });

            _rightPanel.SetAcitve(false);
            _leftPanel.SetAcitve(false);

            if (_dialogs != null && _dialogs.Count > 0)
            {
                MoveNext();
            }
            //if (Role.Instance != null && LuaScriptMgr.Instance.CallLuaFunction_Bool("water.role.mainTask.IsAutoMission"))
            //{
                StartCoroutine(NextCountDown());
            //}

            SingletonFactory<UIManager>.Instance.HideLayerByEUILayer(EUILayer.AlertTips);
        }

        public override void Dispose()
        {
            if (GameSceneSwitcher.curScene != GameScene.E_BATTLE)
                SingletonFactory<UIManager>.Instance.ShowLayerByEUILayer(EUILayer.AlertTips);

            callFinishCB();

            base.Dispose();
        }

        protected void CloseWhenAble()
        {
            if (UIEffectUtil.isUIClickable(skipBtn.transform as RectTransform))
                skipBtn.onClick.Invoke();
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

        private IEnumerator NextCountDown()
        {
            while (true) {
                _curStepTime += Time.deltaTime;
                if (_curStepTime > WaitTime) {
                    MoveNext(true);
                }
                yield return null;    
            }
        }

        private void MoveNext(bool ifAuto = false)
        {
            if(_curShowPanel != null && !_curShowPanel.isFinish){
                _curShowPanel.breakAnim();
                return;
            }

            if (_dialogs != null && _curIndex < _dialogs.Count)
            {
                sendTalkingDataEventById(dialogId);
                //sendTalkingDataEventById(200456, dialogId);
                //sendTalkingDataEventById(5000001, dialogId);
                //sendTalkingDataEventById(5000002, dialogId);
                //sendTalkingDataEventById(5000003, dialogId);
                //sendTalkingDataEventById(5000004, dialogId);
                //sendTalkingDataEventById(5000005, dialogId);
                //sendTalkingDataEventById(5000006, dialogId);
                //sendTalkingDataEventById(5000007, dialogId);

				var curDialog = _dialogs[_curIndex];

				if (_lastSetPos != curDialog.Pos) {
					if (_curShowPanel != null) {
						_curShowPanel.SetAcitve (false);
					}
				}
				else{
					if( _lastSetModelURL != curDialog.ModelURL && _curShowPanel != null){
						_curShowPanel.SetAcitve (false);
					}
				}

				_curShowPanel = curDialog.Pos == 1 ? _rightPanel : _leftPanel;											              
                _curShowPanel.SetAcitve(true);
                _curShowPanel.SetName(curDialog.Name);
                // next_btn_text.gameObject.SetActive(false);
                // nextBtn.enabled = false;
                _curShowPanel.SetText(curDialog.Text, ifAuto);

                if (!string.IsNullOrEmpty(curDialog.ModelURL))
                {
                    if (_curIndex > 0)
                    {
						if (_lastSetPos != curDialog.Pos)
                        {
							_curShowPanel.SetModel(curDialog.ModelURL, curDialog.Scale, curDialog.OffsetAngle,curDialog.IsMainRole);
                            _curShowPanel.PlayNpcAni();
                            _lastSetPos = curDialog.Pos;
                        }
						else if(_lastSetPos == curDialog.Pos && _lastSetModelURL != curDialog.ModelURL)
						{
							_curShowPanel.SetModel(curDialog.ModelURL, curDialog.Scale, curDialog.OffsetAngle,curDialog.IsMainRole);
							_curShowPanel.PlayNpcAni();

						}
						_lastSetModelURL = curDialog.ModelURL;
                    }
                    else
                    {
						_lastSetModelURL = curDialog.ModelURL;
                        _lastSetPos = curDialog.Pos;
                        if (curDialog.dialogId == 5000007)
                        {
						    _curShowPanel.SetModel(curDialog.ModelURL, curDialog.Scale, curDialog.OffsetAngle,curDialog.IsMainRole, 140);
                        }
                        else
                        {
						    _curShowPanel.SetModel(curDialog.ModelURL, curDialog.Scale, curDialog.OffsetAngle,curDialog.IsMainRole);
                        }
                    }
                }

                _curStepTime = 0;
                ++_curIndex;
            }
            else
            {
                DialogFinished();
            }
        }

        void callFinishCB(){
            if(!hasOwner || (hasOwner && owner)){
                if (_onFinished != null) { 
                    _onFinished();
                    _onFinished = null; 
                }
            }
        }

        private void DialogFinished()
        {
            StopCoroutine(NextCountDown());
            if (_dialogs != null) { _dialogs.Clear(); }
            _curIndex = 0;
            SingletonFactory<UIManager>.Instance.CloseUI(UIType.DialogUI, UIHandleType.ExitDestory, true);
            callFinishCB();
        }

        private void onSkipBtnClick()
        {
            sendTalkingDataEventById(dialogId, true);
            //sendTalkingDataEventById(200456, dialogId, true);
            //sendTalkingDataEventById(5000001, dialogId, true);
            //sendTalkingDataEventById(5000002, dialogId, true);
            //sendTalkingDataEventById(5000003, dialogId, true);
            //sendTalkingDataEventById(5000004, dialogId, true);
            //sendTalkingDataEventById(5000005, dialogId, true);
            //sendTalkingDataEventById(5000006, dialogId, true);
            //sendTalkingDataEventById(5000007, dialogId, true);
            DialogFinished();
        }
    }
}

