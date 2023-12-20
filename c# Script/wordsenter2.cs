using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class wordsenter2 : MonoBehaviour
{

    public InputField inputField;
    public Text replyText;
    public List<EmotionData> emotionDatas;
    public static float joyValue;
    public static float angryValue;


    // Start is called before the first frame update
    void Start()
    {
       
    }
    private string GenerateReply(string userInput)
    {
        // �b������Τ��A�o�Ө�ƥi��|�ھڥΤ��J�i���������B�z�M�ͦ��^��
        return "你說了" + userInput;
    }
    public void SendRequest()
    {
        string url = "https://api.droidtown.co/KeyMoji/API/";

        // �`�N�G�o�̪�URL�ݭn�A���z���ݨD�A�T�O���O���Ī� KeyMoji API ���I�C

        // �N content_type �]�m�� "application/json"
        UnityWebRequest www = UnityWebRequest.PostWwwForm(url, "");
        www.SetRequestHeader("Content-Type", "application/json");

        // �q InputField ��Ū����r
        string inputText = inputField.text;

        // �c�� JSON �ШD���e
        string jsonData = "{" +
            "\"username\": \"\"," +
            "\"keymoji_key\": \"\"," +
            "\"input_str\": \"" + inputText + "\"," +
            "\"sense\": \"sense8\"," +
            "\"model\": \"general\"," +
            "\"user_defined_positive\": []," +
            "\"user_defined_negative\": []," +
            "\"user_defined_cursing\": []," +
            "\"context_sensitivity\": true" +
            "}";

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();

        StartCoroutine(SendRequestCoroutine(www));
    }

    IEnumerator SendRequestCoroutine(UnityWebRequest www)
    {
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogError(www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            replyText.text = responseText;
            emotionDatas = JsonUtility.FromJson<ApiResponse>(responseText).results;
            string jsonResult = JsonUtility.ToJson(emotionDatas);
            joyValue = emotionDatas[0].Joy;
            angryValue = emotionDatas[0].Anger;

            //Debug.Log("Joy Value: " + joyValue);
            //Debug.Log(responseText);
            //Debug.Log("Results:" + jsonResult);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
    [Serializable]
    public class EmotionData
    {
        public string input_str;
        public float Joy;
        public float Trust;
        public float Surprise;
        public float Anticipation;
        public float Fear;
        public float Sadness;
        public float Anger;
        public float Disgust;
    }

    public class ApiResponse
    {
        public bool status;
        public string msg;
        public List<EmotionData> results;
        public string sense;
        public string model;
        public string version;
    }

public class Serialization<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }
    public Serialization(List<T> target)
    {
        this.target = target;
    }
}