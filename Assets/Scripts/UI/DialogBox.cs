using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI txt_message;
    [SerializeField] protected Button btn_Close;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        btn_Close?.onClick.AddListener(CloseWindow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected void OnDestroy()
	{
        btn_Close?.onClick.RemoveListener(CloseWindow);
	}

	protected void CloseWindow()
    {
		UIManager uiManager = FindFirstObjectByType<UIManager>();
		uiManager?.popOutUI();
		Destroy(gameObject);
    }

    public void setMessageText(string messageText)
    {
        if (txt_message != null)
        {
            txt_message.text = messageText;
        }
    }
}
