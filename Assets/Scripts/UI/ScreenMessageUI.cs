using System;
using System.Collections;
using TMPro;
using TreeEditor;
using UnityEngine;

public class ScreenMessageUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI screenMessage;
    protected float remainingTime = 2.0f;
    protected MainCharacter character;
    protected bool result = false;
    public event Action OnMessageEnd;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(EndMessage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected IEnumerator EndMessage()
    {
        while (remainingTime > 0) { 
            yield return null;
            remainingTime -= Time.unscaledDeltaTime;
		}
        OnMessageEnd();
		Destroy(gameObject);
	}

	protected void OnDestroy()
	{
		
	}

	public void setScreenMessage(in string inMessage)
    {
        screenMessage.text = inMessage; 
    }

    public void setCharacter(in MainCharacter inCharacter)
    {
        character = inCharacter;    
    }

    public void setResult(in bool inResult)
    {
        result = inResult;
    }

    public void setMessageTextColor(in Color inColor)
    {
        screenMessage.color = inColor;
    }
}
