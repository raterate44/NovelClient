using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//using PearlsPeril.HOScene;

[System.Serializable]
public class JsonStatus
{
    public string[] charaName;
    public int[] charaId;
    //public string[] charaEndName;
    public string[] storyText;
    public string[] storyEndText;
}

public class StorySpeakJson : MonoBehaviour
{
    JsonStatus _jsonStatus;
    //BootstrapRoot _boot;
    int sinarioNo;
    int sinarioSort;

    string speakPanelURL = "http://133.130.96.139/db/files/speakPanel.png";
    string bgPanelURL = "http://133.130.96.139/db/files/bg00.png";
    string CharaURL = "http://133.130.96.139/db/files/Chara";

    [SerializeField]
    NovelManager _NovelManager;

    void Start()
    {
        /*
        if (_boot == null)
        {
            _boot = GameObject.Find("Bootstrap").GetComponent<BootstrapRoot>();
        }
        */
        StartCoroutine(GetCoroutineStart());
    }

    IEnumerator GetCoroutineStart()
    {
        yield return StartCoroutine(GetBGImages());
        yield return StartCoroutine(GetSpeakPanelImages());

        for (int i = 0; i < 2; i++)
        {
            var charaCoroutine = StartCoroutine(GetChara(i));
            yield return charaCoroutine;
        }

        StartCoroutine(GetJSON());
    }

    public IEnumerator GetSpeakPanelImages()
    {
        WWW www;
        // webサーバへアクセス
        www = new WWW(speakPanelURL);
        // webサーバから何らかの返答があるまで停止
        yield return www;
        // もし、何らかのエラーがあったら
        if (!string.IsNullOrEmpty(www.error))
        {
            // エラー内容を表示
            Debug.LogError(string.Format("Fail Whale!\n{0}", www.error));
            yield break; // コルーチンを終了
        }
        Renderer renderer = GetComponent<Renderer>();
        Texture2D texture = www.texture;

        _NovelManager.speakPanelImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        yield break;
    }

    public IEnumerator GetBGImages()
    {
        WWW www;
        // webサーバへアクセス
        www = new WWW(bgPanelURL);
        // webサーバから何らかの返答があるまで停止
        yield return www;
        // もし、何らかのエラーがあったら
        if (!string.IsNullOrEmpty(www.error))
        {
            // エラー内容を表示
            Debug.LogError(string.Format("Fail Whale!\n{0}", www.error));
            yield break; // コルーチンを終了
        }
        Renderer renderer = GetComponent<Renderer>();
        Texture2D texture = www.texture;

        _NovelManager.bgPanelImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

        yield break;
    }

    public IEnumerator GetChara(int i)
    {
        WWW www;
        // webサーバへアクセス
        www = new WWW(CharaURL + i + ".png");
        // webサーバから何らかの返答があるまで停止
        yield return www;
        // もし、何らかのエラーがあったら
        if (!string.IsNullOrEmpty(www.error))
        {
            // エラー内容を表示
            Debug.LogError(string.Format("Fail Whale!\n{0}", www.error));
            yield break; // コルーチンを終了
        }
        Renderer renderer = GetComponent<Renderer>();
        Texture2D texture = www.texture;

        Sprite chara = Sprite.Create(texture, new Rect(0, 0, 512, 512), Vector2.zero);
        _NovelManager.charaSpriteImage[i].sprite = chara;

        yield break;
    }

    public IEnumerator GetJSON()
    {
        //phpへ送るデータの格納
        WWWForm form = new WWWForm();

        sinarioNo = 1;
        sinarioSort = 1;

        /*
        if (_boot.GetSphereScenarioNo() == 0)
        {
            sinarioNo = 1;
            sinarioSort = 2;
        }
        else
        {
            sinarioNo = _boot.GetSphereScenarioNo();
            sinarioSort = _boot.GetSphereScenarioSort();
        }
        */

        form.AddField("storyNo", sinarioNo);
        form.AddField("storySort", sinarioSort);

        WWW www;
        // webサーバへアクセス
        www = new WWW("http://133.130.96.139/db/StorySpeakData.php", form.data);
        // webサーバから何らかの返答があるまで停止
        yield return www;
        // もし、何らかのエラーがあったら
        if (!string.IsNullOrEmpty(www.error))
        {
            // エラー内容を表示
            Debug.LogError(string.Format("Fail Whale!\n{0}", www.error));
            yield break; // コルーチンを終了
        }
        // webサーバからの内容を文字列変数に格納
        string json = www.text;
        _jsonStatus = new JsonStatus();

        _jsonStatus = JsonUtility.FromJson<JsonStatus>(json);

        //SceneManager.Get<HOSceneLoadingRoot>().CloseScreen();
        GetComponent<ADRoot>().SpeakBuild();
    }

    public string[] CharaNameGet()
    {
        return _jsonStatus.charaName;
    }
    public int[] CharaIdGet()
    {
        return _jsonStatus.charaId;
    }
    public string[] StoryTextGet()
    {
        return _jsonStatus.storyText;
    }
    public string[] StoryEndTextGet()
    {
        return _jsonStatus.storyEndText;
    }
}