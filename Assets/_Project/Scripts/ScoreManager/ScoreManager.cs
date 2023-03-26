using System.Collections;
using System.Collections.Generic;
using UnityEngine;
     
public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance;

	[SerializeField]
	private ScoreManagerData _properties;
	
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
}
