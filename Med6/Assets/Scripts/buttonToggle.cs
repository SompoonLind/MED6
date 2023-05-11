using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class buttonToggle : MonoBehaviour
{
    GameObject cameraMover;
    CameraMover camMove;
    Button button;
    TMP_Text buttonText;
    void Start()
    {
        cameraMover = GameObject.Find("Camera Movement");
        camMove = cameraMover.GetComponent<CameraMover>();
        button = this.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        buttonText = this.gameObject.GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void OnClick()
    {
        if (camMove.pause == true)
        {
            buttonText.text = "||";
            camMove.pause = false;
        }
        else if(camMove.pause == false)
        {
            buttonText.text = "▶";
            camMove.pause = true;
        }
        
    }
}
