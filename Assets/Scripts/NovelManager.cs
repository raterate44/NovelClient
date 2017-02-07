using UnityEngine;
using UnityEngine.UI;
//using PearlsPeril.Localization;
using System.Collections;
using System.Collections.Generic;
//using Wooga.Signals;

//namespace PearlsPeril.HOScene
//{
public class NovelManager : MonoBehaviour
{

    public event System.Action FindClueMessagePanelPressed = delegate {};
    public event System.Action HintPressed = delegate {};

    //public readonly Signal onStartLookingForClue = new Signal();

    [SerializeField]
    public Image speakPanelImage;
    [SerializeField]
    public Image bgPanelImage;
    [SerializeField]
    public Image[] charaSpriteImage = new Image[2];

    [SerializeField] GameObject findClueMessagePanel;
    [SerializeField] Button findClueMessageButton;
    //[SerializeField] TMPro.TextMeshProUGUI scoreText;
    [SerializeField] GameObject penaltyPanel;
    //[SerializeField] TMPro.TextMeshProUGUI penaltyTextLabel;

    //[SerializeField] HiddenObjectsToFindPanel objectsToFindPanelPhone;
    //[SerializeField] HiddenObjectsToFindPanel objectsToFindPanelTablet;

    [SerializeField] GameObject bonusBar;
    [SerializeField] Image bonusBarFill;
    //[SerializeField] TMPro.TextMeshProUGUI bonusBarLabel;

    [SerializeField] GameObject extraRewardChest;

    [SerializeField]
    public ADRoot _aDRoot;

    [SerializeField] GameObject eventsChest;
    public Image eventsIcon;

    public GameObject objectsPanel;

    [SerializeField]
    Image[] imageMiniUnit = new Image[5];
    [SerializeField]
    GameObject[] speakObject = new GameObject[5];
    [SerializeField]
    //TMPro.TextMeshProUGUI[] SpeakLabel = new TMPro.TextMeshProUGUI[5];
    Text[] SpeakLabel = new Text[5];


    //public HOSceneController _hOSceneController = new HOSceneController();
    //public BootstrapRoot _boot = new BootstrapRoot();
    //public IList<Status.UnitState> ownDeckUnits = new List<Status.UnitState>();

    //public float speakActiveTime = 0f;
    //public int speakSortNo = 0;
    //public int speakCharaIndex = 0;
    //public bool isSpeaking = false;
    //public bool isFrendSpeakAfter = false;

    //Dictionary<GameObject,Coroutine> flashRoutines = new Dictionary<GameObject, Coroutine>();
    /*
		HiddenObjectsToFindPanel _objectsToFindPanel;
		public HiddenObjectsToFindPanel objectsToFindPanel {
			get {
				if (_objectsToFindPanel == null) {					
					if (PearlsPeril.Shared.DeviceUtil.IsLargeDevice()) {
						GameObject.Destroy (objectsToFindPanelPhone);
						_objectsToFindPanel = objectsToFindPanelTablet;
					} else {
						GameObject.Destroy (objectsToFindPanelTablet);
						_objectsToFindPanel = objectsToFindPanelPhone;
					}
					_objectsToFindPanel.gameObject.SetActive (true);
					_objectsToFindPanel.hintButton.onClick.AddListener (OnHintPressed);
				}
				return _objectsToFindPanel;
			}
		}

        public void Speak(int speakNo, int UnitNo)
        {
            //if (ownDeckUnits.Count > 0 && ownDeckUnits.Count > discoveryUnitNo)
            //{
                speakSortNo = speakNo;
                speakCharaIndex = UnitNo;

                switch (speakSortNo)
                {
                    case 0:
                        //SpeakLabel[speakCharaIndex].text = string.Format("{0}", ownDeckUnits[speakCharaIndex].speakDiscovery);
                        break;
                    case 1:
                        //SpeakLabel[speakCharaIndex].text = string.Format("{0}", ownDeckUnits[speakCharaIndex].speakConboEnd);
                        break;
                    case 2:
                        //SpeakLabel[speakCharaIndex].text = string.Format("{0}", ownDeckUnits[speakCharaIndex].speakHint);
                        break;
                    case 3:
                        //SpeakLabel[speakCharaIndex].text = string.Format("{0}", ownDeckUnits[speakCharaIndex].speakLastOne);
                        break;
                }

                
                speakObject[speakCharaIndex].SetActive(true);

                if (!isFrendSpeakAfter && speakSortNo == 0)
                {
                    int speakUnitGroupId = ownDeckUnits[speakCharaIndex].groupId;
                    int frendUnitGroupId = 0;
                    for (int i = 0; i < ownDeckUnits.Count; i++)
                    {
                        if (ownDeckUnits[speakCharaIndex].id == ownDeckUnits[i].id)
                        {
                            continue;
                        }
                        frendUnitGroupId = ownDeckUnits[i].groupId;
                        if (speakUnitGroupId == frendUnitGroupId)
                        {
                            int random = Random.Range(0, 100);
                            if (random < 30)
                            {
                                int speakSort = ownDeckUnits[speakCharaIndex].id % 5;
                                if (ownDeckUnits[speakCharaIndex].id > ownDeckUnits[i].id)
                                {
                                    speakSort--;
                                }
                                StartCoroutine(FrendSpeak((speakSort + 10), i, speakCharaIndex));
                                return;
                            }
                        }
                    }
                }
                else
                {
                    isFrendSpeakAfter = false;
                }
                speakActiveTime = 6f;
                
            //}
        }

        Vector3 _eventsChestScreenPosition;
		public Vector3 EventsChestScreenPosition {
			get {
				if (_eventsChestScreenPosition == Vector3.zero) {
					var canvas = GetComponentInChildren<Canvas>();
					_eventsChestScreenPosition = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, eventsChest.transform.position);
				}
				return _eventsChestScreenPosition;
			}
		}

		LocalizationService LocalizationService { get { return ServiceLocator.Get<LocalizationService>(); } }

		void Awake()
		{
            speakSortNo = 6;

            findClueMessageButton.onClick.AddListener (OnFindClueMessageButtonPressed);
			ResetUI();

            _hOSceneController = GameObject.Find("HOSceneGame").GetComponent<HOSceneController>();
            _boot = SceneManager.Get<BootstrapRoot>();

            ownDeckUnits = _boot.GetOwnDeckUnits(0);
        }

		public void ResetUI ()
		{
			SetClueText ("");
			ResetChestIcons();
			bonusBar.SetActive (false);
			countupRoutine = null;
			scoreUpdates.Clear();
			UpdateHintCooldown(0);
		}

		void ResetChestIcons ()
		{
			extraRewardChest.transform.SetScale(0);
			eventsChest.transform.SetScale(0);
		}
			
		void OnFindClueMessageButtonPressed()
		{
			FindClueMessagePanelPressed();
		}

		void OnHintPressed ()
		{
			HintPressed();
		}

		Coroutine countupRoutine;

		Queue<int[]> scoreUpdates = new Queue<int[]>(); //int[0] newScore, int[1] oldScore

		public void DisplayScore(int newScore, int oldScore)
		{
			if (newScore > 0) {
				scoreUpdates.Enqueue(new int[]{newScore,oldScore});
				if (countupRoutine == null) {
					countupRoutine = StartCoroutine(CountUpScoreRoutine());
				}
			}
			else {
				scoreText.text = "";
			}
		}

		IEnumerator CountUpScoreRoutine()
		{
			const float SCORE_COUNTUP_TIME = 1f;

			var numberFormat = LocalizationService.GetCultureInfo().NumberFormat;

			while (!scoreUpdates.IsNullOrEmptyCollection()) {
				var scoreUpdate = scoreUpdates.Dequeue();
				var newScore = scoreUpdate[0];
				var oldScore = scoreUpdate[1];
				var deltaTime = 0f;
				while (deltaTime < SCORE_COUNTUP_TIME)
				{
					deltaTime += Time.deltaTime;
					
					var score = Mathf.RoundToInt(Mathf.Lerp(oldScore, newScore, deltaTime / SCORE_COUNTUP_TIME));

					scoreText.text = score.ToString("N0", numberFormat);
					
					yield return null;
				}				
			}
			countupRoutine = null;
		}

		#region - clue
		public void ShowFindClueMessagePanel(string clueName)
		{
			StartCoroutine(ShowFindClueMessagePanelRoutine(clueName));
		}

		public void HideFindClueMessagePanel()
		{
			findClueMessagePanel.SetActive (false);
			onStartLookingForClue.Dispatch();
		}

		public void SetClueText(string clueItemName)
		{
			objectsToFindPanel.ShowClueText (clueItemName);
		}

		IEnumerator ShowFindClueMessagePanelRoutine (string clueName)
		{
			bonusBar.SetActive(false);

			findClueMessagePanel.SetActive (true);
			var button = findClueMessagePanel.GetComponent<Button>();
			var panel = findClueMessagePanel.transform.GetChild(0);

			button.interactable = false;
			panel.gameObject.SetActive(false);

			//TODO change to show after last item pickup is done instead of arbitrary waiting time
			yield return new WaitForSeconds(1.2f);

			panel.gameObject.SetActive(true);
			button.interactable = true;

			while (findClueMessagePanel.activeInHierarchy) {
				yield return null;
			}

			SetClueText(clueName);
		}
		#endregion

		#region - penalty panel

		public void UpdatePenaltyPanel(int numSeconds)
		{
			if (numSeconds <= 0)
			{
				HidePenaltyPanel();
			} else {
				ShowPenaltyPanel();
				var penaltyText = LocalizationService.GetText(LocaKeys.ho_penalty_text, new LocaParam("time", numSeconds));
				SetPenaltyText(penaltyText);
			}
		}

		void ShowPenaltyPanel()
		{
			penaltyPanel.SetActive (true);
		}

		void HidePenaltyPanel()
		{
			penaltyPanel.SetActive (false);
		}

		void SetPenaltyText(string text)
		{
			penaltyTextLabel.text = text;
		}
		#endregion

		#region - hints

		public void UpdateHintCooldown(int numSeconds)
		{
			var hintButton = objectsToFindPanel.hintButton;
			var hintTimerLabel = objectsToFindPanel.hintTimerText;

			if (numSeconds <= 0)
			{
				hintButton.interactable = true;
				hintTimerLabel.text = "";
			} else {
				hintButton.interactable = false;
				hintTimerLabel.text = string.Format("{0}", numSeconds);
			}
		}

		#endregion

		#region - bonus bar

		public void UpdateBonusBarStatus(int multiplier, float fill)
		{
			if (fill <= 0 || multiplier < 2)
			{
				bonusBar.SetActive(false);
			} else {
				bonusBar.SetActive(true);
				bonusBarFill.fillAmount = fill;
				bonusBarLabel.text = string.Format("{0}: {1}x", LocalizationService.GetText(LocaKeys.score_ho_bonus_headline), multiplier);
			}
		}

		#endregion

		#region - hint arrow

		public void ShowHintArrow()
		{
			objectsToFindPanel.arrowDown.gameObject.SetActive (true);
		}

		public void HideHintArrow()
		{
			objectsToFindPanel.arrowDown.gameObject.SetActive (false);
		}

		#endregion

		#region - extra reward chest

		public void FlashExtraRewardChest ()
		{
			FlashRewardIcon(extraRewardChest);
		}

		public void FlashEventChest ()
		{
			FlashRewardIcon(eventsChest);
		}

		void FlashRewardIcon (GameObject icon)
		{
			if (flashRoutines.ContainsKey(icon))
			{
				StopCoroutine(flashRoutines[icon]);
				flashRoutines.Remove(icon);
			}
			flashRoutines[icon] = StartCoroutine(FlashRoutine(icon));
		}

		IEnumerator FlashRoutine (GameObject go)
		{
			go.transform.SetScale(0);

			var t = 0f;
			while (t < 0.1f)
			{
				t+= Time.unscaledDeltaTime;
				go.transform.SetScale(10f * t);
				yield return null;
			}

			t = 0f;
			while (t < 1f)
			{
				t+= Time.unscaledDeltaTime;
				go.transform.SetScale(Mathf.Sin(t * Mathf.PI * 6f) * 0.25f + 1f);
				yield return null;
			}

			t = 0f;
			while (t < 0.1f)
			{
				t+= Time.unscaledDeltaTime;
				go.transform.SetScale(1f - 10f * t);
				yield return null;
			}
		}

		#endregion

		void OnDisable ()
		{
			StopAllCoroutines();
		}
        */
}

/*
	public static partial class TransormExtensions
	{
		public static void SetScale (this Transform t, float scale)
		{
			t.localScale = Vector3.one * scale;
		}
	}
    */
//}
