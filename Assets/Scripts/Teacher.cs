using UnityEngine;
using TMPro;
using System;
using System.IO;

[Serializable]
public class TeacherData
{
    public string name;
    public string sprechstunde;
}

public class Teacher : MonoBehaviour
{
    [Header("Textfeld für die Tafel")]
    [SerializeField] private TextMeshProUGUI outputText;

    [Header("Lokale JSON-Datei nutzen?")]
    [SerializeField] private bool useLocalJson = true; // true = lokal, false = HTTP

    [Header("Lokaler Pfad zur JSON-Datei")]
    [SerializeField] private string localFilePath = @"C:\Users\winke\github\19-3DRaum-lwin\19-3DRaum-lwin\Assets\Scripts\teacher.json";

    [Header("HTTP URL (optional)")]
    [SerializeField] private string url = "https://htl-website.tld/api/lehrerdaten.json";

    private void Start()
    {
        if (outputText == null)
        {
            Debug.LogError("Output Text ist nicht zugewiesen! Bitte TextMeshPro-Objekt zuweisen.");
            return;
        }

        if (useLocalJson)
        {
            LoadLocalData();
        }
        else
        {
            Debug.LogError("HTTP-Loading ist aktuell nicht implementiert, da die HTL-Seite HTML liefert.");
            outputText.text = "HTTP-Daten nicht verfügbar!";
        }
    }

    // --- Lokale JSON laden ---
    private void LoadLocalData()
    {
        try
        {
            if (!File.Exists(localFilePath))
            {
                Debug.LogError($"Datei nicht gefunden: {localFilePath}");
                outputText.text = "Fehler beim Laden!";
                return;
            }

            // Datei lesen
            string json = File.ReadAllText(localFilePath);

            // JSON parsen
            TeacherData data = JsonUtility.FromJson<TeacherData>(json);

            // Text auf Tafel setzen
            outputText.text = $"Lehrer: {data.name}\nSprechstunde: {data.sprechstunde}";
            Debug.Log("Lokales JSON geladen: " + json);
        }
        catch (Exception e)
        {
            outputText.text = "Fehler beim Laden!";
            Debug.LogError(e.Message);
        }
    }
}

