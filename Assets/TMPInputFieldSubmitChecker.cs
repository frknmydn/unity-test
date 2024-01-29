using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

[System.Serializable]
public class FlagResponse
{
    public string room;
    public string difficulty;
    public string id;
    public string[] FlagCategories;
    public string FlagName;
}

public class TMPInputFieldSubmitChecker : MonoBehaviour
{
    public TMP_Text textMeshProOutput;
    public TMP_InputField inputFlagName;
    public TMP_InputField inputEmail;
    public TMP_InputField inputTeam;

    private string flagRoom;
    private string flagDifficulty;
    private string flagId;
    private string[] flagCategories;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(GetFlagByName(inputFlagName.text));
        }
    }

    IEnumerator GetFlagByName(string flagName)
    {
        string url = $"https://xexcjda7ei.execute-api.us-west-1.amazonaws.com/prod/getflagbyname?name={flagName}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                FlagResponse flagResponse = JsonUtility.FromJson<FlagResponse>(request.downloadHandler.text);
                flagRoom = flagResponse.room;
                flagDifficulty = flagResponse.difficulty;
                flagId = flagResponse.id;
                flagCategories = flagResponse.FlagCategories;

                StartCoroutine(SubmitFlag());
            }
            else
            {
                textMeshProOutput.text = "<color=#FF0000>Flag retrieval failed</color>";
            }
        }
    }

    IEnumerator SubmitFlag()
    {
        string jsonData = $"{{\"flag\":\"FLAG{{{flagId}:{inputFlagName.text}}}\"}}";
        string url = $"https://xexcjda7ei.execute-api.us-west-1.amazonaws.com/prod/submitflag?team_name={inputTeam.text}&challenge_id={flagId}&user_email={inputEmail.text}";

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.responseCode == 200)
            {
                textMeshProOutput.text = "<color=#008000>Flag submitted successfully</color>";
            }
            else
            {
                textMeshProOutput.text = "<color=#FF0000>WRONG</color>";
            }
        }
    }
}
