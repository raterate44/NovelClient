using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using PearlsPeril.HOScene;
//using Status;
//using PearlsPeril.SceneBrowser;
//using PearlsPeril.Road;

public class ADRoot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public GameObject speakPanel;
    [SerializeField]
    public Text speakCharaNameText;
    //[SerializeField]
    //public Text speakText;
    [SerializeField]
    public GameObject chara1;
    [SerializeField]
    public Image charaImage1;
    string charaName1;
    //[SerializeField]
    //Text endCharaName1;
    //[SerializeField]
    //public Image endCharaImage1;
    [SerializeField]
    public GameObject chara2;
    [SerializeField]
    public Image charaImage2;
    string charaName2;
    //[SerializeField]
    //Text endCharaName2;
    //[SerializeField]
    //public Image endCharaImage2;
    [SerializeField]
    public GameObject hidePanel;
    [SerializeField]
    public GameObject storyTelopPanel;
    [SerializeField]
    public GameObject backLogPanel;
    //[SerializeField]
    //public GameObject endPanel;
    [SerializeField]
    public GameObject backLogBtn;
    [SerializeField]
    public GameObject hideBtn;
    [SerializeField]
    public GameObject skipBtn;
    //[SerializeField]
    //public GameObject poseBtn;
    //[SerializeField]
    //public GameObject bounusBar;

    [SerializeField]
    public GameObject backLogContent;
    [SerializeField]
    public Text[] speakCharaNameBackLog = new Text[1];
    [SerializeField]
    public Text[] speakBackLog = new Text[1];
    [SerializeField]
    public Text StoryTelopText;

    //public BootstrapRoot _boot = new BootstrapRoot();
    //HOSceneGameRoot _hOSceneGameRoot = new HOSceneGameRoot();
    //public HOSceneController _hOSceneController = new HOSceneController();
    //public TapToStartPopupRoot _tapToStartPopupRoot = new TapToStartPopupRoot();
    StorySpeakJson _storySpeakJson = new StorySpeakJson();

   // IList<UnitState> ownUnitList;

    //IList<UnitState> ownDeckUnitList;
    IList<Sprite> ownDeckUnitSpriteList;

    public GameObject tapToStartPopupRoot;

    string[] storySpeaks1 = { "Main Story Sentence１" ,
    "Main Story Sentence２" ,
    "Main Story Sentence３" ,
    "Main Story Sentence４" ,
    "Main Story Sentence５" ,
    "Main Story Sentence End"};

    int SpeakCnt = 0;
    string sinarioId;
    bool isEnd = false;

    string[] speaks;
    string[] endSpeaks;

    int[] charaIdLocal = new int[10];
    int[] charaId;
    string[] charaName;

    bool isSpeakPositionChange;
    bool isSpeakCharaLeft = true;

    [SerializeField]
    public Image fadeImage;
    float fadeTime = 1.5f;

    void FadeOutStart(int sceneNo)
    {
        fadeImage.gameObject.SetActive(true);
        FadeOut(sceneNo);
    }
    void FadeOut(int sceneNo)
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", 0f,
            "to", 1f,
            "time", fadeTime,
            "oncomplete", "SceneEnd",
            "oncompleteparams", sceneNo,
            "oncompletetarget", gameObject,
            "onupdate", "SetValue"));
    }

    void SetValue(float alpha)
    {
        fadeImage.color = new Color(0, 0, 0, alpha);
    }

    //フェードパネルの初期化
    public void FadePanelCler()
    {
        fadeImage.color = new Color(0, 0, 0, 0f);
        fadeImage.gameObject.SetActive(false);
    }

    
    void SceneEnd(int sceneNo)
    {
        /*
        _boot.isBackHo = true;
        switch (sceneNo)
        {
            case 1:
                SceneManager.LoadScene<SceneBrowserRoot>(false);
                break;
            case 3:
                SceneManager.LoadScene<RoadRoot>(false);
                break;
        }
        */
        Invoke("FirstLoad",1.0f);
        
    }

    void FirstLoad()
    {
        SceneManager.LoadScene(0);
    }

        void InstanceRoots()
    {
        if (_storySpeakJson == null)
        {
            _storySpeakJson = GetComponent<StorySpeakJson>();
        }
        /*
        if (_boot == null)
        {
            _boot = SceneManager.Get<BootstrapRoot>();
        }
        if (_hOSceneGameRoot == null)
        {
            _hOSceneGameRoot = SceneManager.Get<HOSceneGameRoot>();
        }
        */
    }

    public void SpeakBuild()
    {
        InstanceRoots();

        //if (_boot.GetSphereIsStory() || _boot.GetSphereIsSubStory())
        //{
            StorySentenceSetting();
        //}
        //else {
            //HoSceneSentenceSetting();
            //return;
        //}

        //transform.parent.FindChild("ObjectsToFind Phone").gameObject.SetActive(false);
       // poseBtn.SetActive(false);
    }

    void StorySentenceSetting()
    {
        storyTelopPanel.SetActive(true);
        //if (_boot.GetSphereIsStory())
        //{
            StoryTelopText.text = string.Format("MainStory");
            //speaks = storySpeaks1;
            speaks = _storySpeakJson.StoryTextGet();
        //}
        //else if (_boot.GetSphereIsSubStory())
        //{
        //StoryTelopText.text = string.Format("SubStory:{0}", _boot.GetSphereScenarioNo());
        //speaks = subStorySpeaks1;
        //speaks = _storySpeakJson.StoryTextGet();
        //}

        ADUISetting();

        for (int i = 0;i < 10;i++)
        {
            charaIdLocal[i] = i % 2;
        }

        //charaId = charaIdLocal;

        
        //ownUnitList = _boot.GetOwnUnits();

        charaId = _storySpeakJson.CharaIdGet();
        charaName = _storySpeakJson.CharaNameGet();
        //charaName1 = ownUnitList[charaId[0]].name;
        //charaName2 = ownUnitList[charaId[1]].name;
        /*
        charaImage1.sprite = ownUnitList[charaId[0]].unitSprite;

        int i = 1;
        while (charaImage1.sprite == ownUnitList[charaId[i]].unitSprite)
        {
            i++;
        }
        charaImage2.sprite = ownUnitList[charaId[i]].unitSprite;

        speakPanel.SetActive(false);
        */

        fadeImage.color = new Color(1,1,1,0);
        fadeImage.gameObject.SetActive(false);
    }

    void HoSceneSentenceSetting()
    {
        //ownDeckUnitList = _boot.GetOwnDeckUnits(_boot.GetSphereSelectDeck());
        //ownDeckUnitSpriteList = _boot.GetOwnDeckUnitsSprite(_boot.GetSphereSelectDeck());

        //charaName1 = "1";
        //charaName2 = "2";

        /*
        if (_boot.GetSphereIsSkip())
        {
            StartAndEndPop();
            return;
        }
        */


        //charaImage1.sprite = ownDeckUnitSpriteList[0];
        //charaImage2.sprite = ownDeckUnitSpriteList[1];


        //isEnd = false;

        //StartAndEndPop();
        /*
        //デッキに二人設定していないとき
        if (ownDeckUnitList.Count < 2 || _boot.GetSphereIsSkip())
        {
            StartAndEndPop();
            return;
        }
        else
        {
            ADUISetting();
        }

        speaks = _storySpeakJson.StoryTextGet();

        //初回会話画面構成
        isEnd = false;
        charaName1 = ownDeckUnitList[0].name;
        charaName2 = ownDeckUnitList[1].name;
        charaImage1.sprite = ownDeckUnitSpriteList[0];
        charaImage2.sprite = ownDeckUnitSpriteList[1];

        FirstSpeak();
        */
    }

    void ADUISetting()
    {
        chara1.SetActive(true);
        chara2.SetActive(true);
        speakPanel.SetActive(true);
        backLogBtn.SetActive(true);
        hideBtn.SetActive(true);
    }

    //会話ウィンドウなどを非表示、タップで戻すパネルで覆う
    public void HidePanelActive()
    {
        hidePanel.SetActive(true);
        speakPanel.SetActive(false);
        skipBtn.SetActive(false);
        backLogBtn.SetActive(false);
        hideBtn.SetActive(false);
    }

    public void HidePanelNonActive()
    {
        hidePanel.SetActive(false);
        speakPanel.SetActive(true);
        skipBtn.SetActive(true);
        backLogBtn.SetActive(true);
        hideBtn.SetActive(true);
    }
    //==========================================//

    public void StoryTelopPanelNonActive()
    {
        storyTelopPanel.SetActive(false);
        speakPanel.SetActive(true);
        skipBtn.SetActive(true);
        backLogBtn.SetActive(true);
        hideBtn.SetActive(true);

        FirstSpeak();
    }

    //バックログ
    public void BackLogPanelActive()
    {
        backLogPanel.SetActive(true);
        speakPanel.SetActive(false);
        //backBtn.SetActive(false);
        //hideBtn.SetActive(false);
    }

    public void BackLogPanelNonActive()
    {
        backLogPanel.SetActive(false);
        speakPanel.SetActive(true);
        //backBtn.SetActive(false);
        //hideBtn.SetActive(false);
    }

    //会話スキップ
    public void SkipBtn()
    {
        StartAndEndPop();
    }

    
    //モノ探し開始・終了(モノ探しが無いストーリー・サブストーリーはここで終了)
    void StartAndEndPop()
    {
        FadeOutStart(1);
        /*
        if (_boot.GetSphereIsStory())
        {
            FadeOutStart(1);
            return;
        }
        if (_boot.GetSphereIsSubStory())
        {
            FadeOutStart(3);
            return;
        }
        */
        /*
        if (isEnd)
        {
            IList<UnitState> ownDeckUnitList = _boot.GetOwnDeckUnits(_boot.GetSphereSelectDeck());
            _boot.CharaVoice(6, ownDeckUnitList[UnityEngine.Random.Range(0, 2)].id % 5);

            //this.gameObject.SetActive(false);
            transform.parent.FindChild("ObjectsToFind Phone").gameObject.SetActive(true);
            poseBtn.SetActive(true);
            //_hOSceneController.gameOverSignal.Dispatch(_hOSceneController.model);
            endPanel.SetActive(true);
            endCharaName1.text = charaName1;
            endCharaName2.text = charaName2;
            endCharaImage1.sprite = this.charaImage1.sprite;
            endCharaImage2.sprite = this.charaImage2.sprite;
            chara1.SetActive(false);
            chara2.SetActive(false);
            speakPanel.SetActive(false);

            ownUnitList = _boot.GetOwnUnits();
            if (_boot.GetSphereScenarioNo() < 30)
            {
                _boot.SetisScenario(0, _boot.GetSphereScenarioNo(), true);
            }
            InitializationEndComent();
            //ownUnitList[0].releasePoint++;
            //ownUnitList[0].subReleasePoint++;
            //_boot.SetReleasePoint(ownUnitList[0].id, ownUnitList[0].releasePoint);
            //_boot.SetSubReleasePoint(ownUnitList[0].id, ownUnitList[0].subReleasePoint);
        }
        else
        {
            if (ownDeckUnitList.Count == 2 && !_boot.GetSphereIsSkip())
            {
                tapToStartPopupRoot.SetActive(true);
                _tapToStartPopupRoot.charaImage1.sprite = this.charaImage1.sprite;
                _tapToStartPopupRoot.charaImage2.sprite = this.charaImage2.sprite;
            }

            transform.parent.FindChild("ObjectsToFind Phone").gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            poseBtn.SetActive(true);

            _boot.SetSphereIsSkip(true);
            _tapToStartPopupRoot.SetNextLine();
        //}
                */
    }


    public void StartScoreResult()
    {
        this.gameObject.SetActive(false);
        //_hOSceneController.gameOverSignal.Dispatch(_hOSceneController.Model);
    }

    //モノ探し終了時台詞開始
    /*
    public void EndSpeakStart()
    {
        this.gameObject.SetActive(true);
        transform.parent.FindChild("ObjectsToFind Phone").gameObject.SetActive(false);
        poseBtn.SetActive(false);
        bounusBar.SetActive(false);
        skipBtn.SetActive(false);

        isEnd = true;
        
        if (_boot.GetSphereIsSkip())
        {
            //StartAndEndPop();
            return;
        //}
        SpeakCnt = 0;

        switch (_hOSceneGameRoot.sceneIdToLoad)
        {
            case "01-01-01":
                endSpeaks = endSpeaks1;
                break;
            case "01-01-02":
                endSpeaks = endSpeaks2;
                break;
            case "01-01-03":
                endSpeaks = endSpeaks3;
                break;
            default:
                endSpeaks = endSpeaks1;
                break;
        }

        speaks = _storySpeakJson.StoryEndTextGet();
        FirstSpeak();
    }
*/

        /*
    public void EndSpeakStartReplay()
    {
        this.gameObject.SetActive(true);
        isEnd = true;
        StartAndEndPop();
    }
    */

    void SameSpeakCheck()
    {
        if (0 < SpeakCnt && charaId[SpeakCnt - 1] == charaId[SpeakCnt])
        {
            isSpeakPositionChange = false;
        }
        else
        {
            isSpeakPositionChange = true;
        }
    }

    void ChangeSpeakChara()
    {
        SameSpeakCheck();

        if (!isSpeakPositionChange)
        {
            return;
        }

        if (isSpeakCharaLeft)
        {
            /*
            if (_boot.GetSphereIsStory() || _boot.GetSphereIsSubStory())
            {
                charaName1 = ownUnitList[charaId[SpeakCnt]].name;
                charaImage1.sprite = ownUnitList[charaId[SpeakCnt]].unitSprite;
            }
            */
            charaName1 = charaName[SpeakCnt];

            //charaName1 = "Kumappa";
            charaImage2.color = new Color(0.8f, 0.8f, 0.8f, 1);
            charaImage1.color = new Color(1, 1, 1, 1);
            speakCharaNameText.text = charaName1;
            isSpeakCharaLeft = !isSpeakCharaLeft;
        }
        else
        {
            /*
            if (_boot.GetSphereIsStory() || _boot.GetSphereIsSubStory())
            {
                charaName2 = ownUnitList[charaId[SpeakCnt]].name;
                charaImage2.sprite = ownUnitList[charaId[SpeakCnt]].unitSprite;
            }
            */
            charaName2 = charaName[SpeakCnt];

            //charaName2 = "Kumappa2";
            charaImage1.color = new Color(0.8f, 0.8f, 0.8f, 1);
            charaImage2.color = new Color(1, 1, 1, 1);
            speakCharaNameText.text = charaName2;
            isSpeakCharaLeft = !isSpeakCharaLeft;
        }
    }

    void FirstSpeak()
    {
        ChangeSpeakChara();
        //speakText.text = string.Format("「{0}」", speaks[0]);
        SetNextLine();
        BackLogChange();
    }

    void BackLogChange()
    {
        //backLogContent.transform.localScale = new Vector3(1f, (1f + SpeakCnt), 1f);
        speakBackLog[0].text += speakCharaNameText.text;
        speakBackLog[0].text += "\n";
        //speakBackLog[0].text += speakText.text;
        speakBackLog[0].text += storySpeakText;
        speakBackLog[0].text += "\n\n";
    }


    public void OnPointerClick(PointerEventData eventData)
    {

        //speakText.text = string.Format("「{0}」", speaks[SpeakCnt]);

        // 文字の表示が完了してるならクリック時に次の行を表示する
        if (IsCompleteDisplayText)
        {
            if (isEnd)
            {
                if (endComentLine < endComents.Length)
                {
                    SetEndNextLine();
                }
            }
            else
            {
                SpeakCnt++;

                //文字列配列が空の時会話終了
                if (speaks.Length <= SpeakCnt)
                {
                    StartAndEndPop();
                    return;
                }
                if (SpeakCnt < speaks.Length)
                {
                    ChangeSpeakChara();
                    SetNextLine();
                    BackLogChange();
                }
            }
        }
        else {
            // 完了してないなら文字をすべて表示する
            timeUntilDisplay = 0;
        }
    }

    //public string[] scenarios;
    [SerializeField]
    Text uiText;

    //コメント関連
    [SerializeField]
    Text endUiText1;
    [SerializeField]
    Text endUiText2;

    string[] endComents = { "That’s a piece of cake!",
            "And it’s because of your help!"};

    [SerializeField]
    [Range(0.001f, 0.3f)]
    float intervalForCharacterDisplay = 0.05f;

    private string storySpeakText = string.Empty;
    private string endCommentText = string.Empty;
    private float timeUntilDisplay = 0;
    private float timeElapsed = 1;
    private int endComentLine = 0;
    private int lastUpdateCharacter = -1;

    void Start()
    {
        //SetNextLine();
        /*
        if (_storySpeakJson == null)
        {
            _storySpeakJson = GetComponent<StorySpeakJson>();
            //_jsonStatus = GetComponent<JsonStatus>();
        }
        */
        //StartCoroutine(_storySpeakJson.GetJSON());
        //OFF LINE SPEAK
        //SpeakBuild();
    }

    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    void Update()
    {
        if (isEnd)
        {
            int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * endCommentText.Length);
            if (displayCharacterCount != lastUpdateCharacter)
            {
                if (endComentLine % 2 == 0)
                {
                    endUiText2.text = endCommentText.Substring(0, displayCharacterCount);
                }
                else
                {
                    endUiText1.text = endCommentText.Substring(0, displayCharacterCount);
                }
            }
        }
        else {
            int displayCharacterCount = (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * storySpeakText.Length);
            if (displayCharacterCount != lastUpdateCharacter)
            {
                uiText.text = storySpeakText.Substring(0, displayCharacterCount);
                lastUpdateCharacter = displayCharacterCount;
            }
        }
    }

    void SetNextLine()
    {
        storySpeakText = string.Format("「{0}」", speaks[SpeakCnt]);
        timeUntilDisplay = storySpeakText.Length * intervalForCharacterDisplay;
        TextTimeSetting();
    }

    void InitializationEndComent()
    {
        endComentLine = 0;
        endUiText1.text = "";
        endUiText2.text = "";

        SetEndNextLine();
    }

    void SetEndNextLine()
    {
        endCommentText = string.Format("{0}", endComents[endComentLine]);
        timeUntilDisplay = endCommentText.Length * intervalForCharacterDisplay;
        endComentLine++;
        TextTimeSetting();
    }

    void TextTimeSetting()
    {
        timeElapsed = Time.time;
        lastUpdateCharacter = -1;
    }
}
