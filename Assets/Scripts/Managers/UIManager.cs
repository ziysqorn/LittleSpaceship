using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	protected Stack<GameObject> UIPriorities = new Stack<GameObject>();	

	void Awake()
	{

	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void addUI(GameObject inUI)
	{
		if(UIPriorities.Count > 0)
		{
			GameObject topUI = UIPriorities.Peek();
			CanvasGroup topCanvasGroup = topUI.GetComponent<CanvasGroup>();
			topCanvasGroup.interactable = false;
			topCanvasGroup.blocksRaycasts = false;
		}
		UIPriorities.Push(inUI);
	}

	public void popOutUI()
	{
		if (UIPriorities.Count > 0)
			UIPriorities.Pop();
		if (UIPriorities.Count > 0)
		{
			GameObject topUI = UIPriorities.Peek();
			CanvasGroup topCanvasGroup = topUI.GetComponent<CanvasGroup>();
			topCanvasGroup.interactable = true;
			topCanvasGroup.blocksRaycasts = true;
		}
	}
}
