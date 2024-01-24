using UnityEngine;
using TMPro; // TextMeshPro i�in gereklidir
using UnityEngine.Networking;
using System.Collections; // Coroutine i�in gereklidir

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
    public TMP_Text textMeshProOutput;  // ��kt� i�in TextMeshPro text nesnesi
    public TMP_InputField tmpInputField; // Input i�in TMP_InputField

    void Update()
    {
        // Enter tu�una bas�ld���nda kontrol� yap
        if (tmpInputField != null &&  Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(CheckFlag(tmpInputField.text));
        }
    }

    // Flag kontrol� i�in bir coroutine
    IEnumerator CheckFlag(string flagName)
    {
        string url = $"https://xexcjda7ei.execute-api.us-west-1.amazonaws.com/prod/getflagbyname?name={flagName}";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError($"[TMPInputFieldSubmitChecker] Hata: {request.error}");
            textMeshProOutput.text = "<color=#FF0000>WRONG</color>";  // K�rm�z� renk
        }
        else
        {
            string response = request.downloadHandler.text;
            Debug.Log($"[TMPInputFieldSubmitChecker] Yan�t: {response}");

            // JSON cevab�n� ayr��t�r
            FlagResponse flagResponse = JsonUtility.FromJson<FlagResponse>(response);

            // Alanlar�n varl���n� kontrol et
            if (!string.IsNullOrEmpty(flagResponse.room) &&
                !string.IsNullOrEmpty(flagResponse.difficulty) &&
                !string.IsNullOrEmpty(flagResponse.id) &&
                !string.IsNullOrEmpty(flagResponse.flag_name))
            {
                textMeshProOutput.text = "<color=#008000>TRUE</color>";  // Ye�il renk
            }
            else
            {
                textMeshProOutput.text = "<color=#FF0000>WRONG</color>";  // K�rm�z� renk
            }
        }
    }
}
