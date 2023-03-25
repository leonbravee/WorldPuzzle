using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
     
public class CanvasController : MonoBehaviour
{
	public static CanvasController Instance;

	[SerializeField]
	private TextMeshProUGUI _levelTitleText;
	
	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else if(Instance != this)
		{
			Destroy(gameObject);
		}
	}

	public void SetLevelTitle(string title)
	{
		_levelTitleText.text = title;
	}
}
