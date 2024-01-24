using UnityEngine;
using TMPro; // TextMeshPro için gereklidir
using UnityEngine.Networking;
using System.Collections; // Coroutine için gereklidir

[System.Serializable]
public class FlagResponse
{
    public string room;
    public string difficulty;
    public string id;
    public string flag_name;
}

public class TMPInputFieldSubmitChecker : MonoBehaviour
{
    public TMP_Text textMeshProOutput;  // Çýktý için TextMeshPro text nesnesi
    public TMP_InputField tmpInputField; // Input için TMP_InputField

    void Update()
    {
        // Enter tuþuna basýldýðýnda kontrolü yap
        if (tmpInputField != null &&  Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(CheckFlag(tmpInputField.text));
        }
    }

    // Flag kontrolü için bir coroutine
    IEnumerator CheckFlag(string flagName)
    {
        string url = $"https://xexcjda7ei.execute-api.us-west-1.amazonaws.com/prod/getflagbyname?name={flagName}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError($"[TMPInputFieldSubmitChecker] Hata: {request.error}");
            textMeshProOutput.text = "<color=#FF0000>WRONG</color>";  // Kýrmýzý renk
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log($"[TMPInputFieldSubmitChecker] Yanýt: {response}");

            // JSON cevabýný ayrýþtýr
            FlagResponse flagResponse = JsonUtility.FromJson<FlagResponse>(response);

            // Alanlarýn varlýðýný kontrol et
            if (!string.IsNullOrEmpty(flagResponse.room) &&
                !string.IsNullOrEmpty(flagResponse.difficulty) &&
                !string.IsNullOrEmpty(flagResponse.id) &&
                !string.IsNullOrEmpty(flagResponse.flag_name))
            {
                textMeshProOutput.text = "<color=#008000>TRUE</color>";  // Yeþil renk
            }
            else
            {
                textMeshProOutput.text = "<color=#FF0000>WRONG</color>";  // Kýrmýzý renk
            }
        }
    }
}
