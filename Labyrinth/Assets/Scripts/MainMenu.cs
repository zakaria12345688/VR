using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class VRMenuManager : MonoBehaviour
{
    public Canvas canvas;
    public Button startButton;
    public TextMeshProUGUI infoText;
    public float infoDisplayTime = 5f;

    void Start()
    {
        SetupCanvas();

        infoText.gameObject.SetActive(false); // Verberg uitleg bij start
        startButton.onClick.AddListener(OnStartClicked); // Koppel klik
    }

    void SetupCanvas()
    {
        // Zet Canvas op World Space
        canvas.renderMode = RenderMode.WorldSpace;

        // Positioneer Canvas vlak voor de speler (bijv. 2 meter voor de camera)
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            canvas.transform.position = mainCam.transform.position + mainCam.transform.forward * 2f;
            canvas.transform.rotation = Quaternion.LookRotation(mainCam.transform.forward); // Draai richting speler
        }

        // Maak de canvas klein (VR-geschikt)
        canvas.transform.localScale = new Vector3(0.002f, 0.002f, 0.002f);
    }

    public void OnStartClicked()
    {
        startButton.gameObject.SetActive(false);         // Verberg knop
        infoText.gameObject.SetActive(true);             // Toon uitleg
        infoText.text = "Welkom bij het spel! Gebruik je VR-controller om te bewegen. Probeer de blauwe sleutel te vinden en navigeer naar de blauwe deur.";

        Invoke("LoadGameScene", infoDisplayTime);        // Na paar seconden scene laden
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("mohamedB"); // Zorg dat deze scene in Build Settings staat
    }
}
