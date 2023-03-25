
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using LeonBrave;
using UnityEditor.Timeline.Actions;
using UnityEngine;


public class StackController : MonoBehaviour
{
	public static StackController Instance;

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		UserInput.Instance.TouchEvent += Touch;
		UserInput.Instance.MovementEvent += Movement;
	}
	
	 [SerializeField]
	private List<Transform> _stackObjects;

	[SerializeField]
	private float _movementSpeed;
	
	
	private bool _isTouch;
	private void Movement(Vector3 movement)
	{
		MovementX((movement.x*_movementSpeed*Time.deltaTime));
	}
	private void MovementX(float x)
	{
		if (_stackObjects.Count > 0)
		{
			Transform firstObject = _stackObjects[0];
			firstObject.position = new Vector3((firstObject.position.x + x), firstObject.position.y, firstObject.position.z);

			for (int i = 1; i < _stackObjects.Count; i++)
			{
				Transform currentObject = _stackObjects[i];
				Vector3 previousObjectPosition = _stackObjects[i - 1].position;
				currentObject.position = new Vector3(
					Mathf.Lerp(currentObject.position.x, previousObjectPosition.x, Time.deltaTime *_movementSpeed*2),
					currentObject.position.y,
					currentObject.position.z
				);
			}
		}

	}

	private void Update()
	{
		if(_isTouch) return;
		MovementX(0);
	}

	private void Touch(UserInput.TouchType touchType)
	{
		if (touchType == UserInput.TouchType.Down)
		{
			_isTouch = true;
		}
		else if (touchType == UserInput.TouchType.Up)
		{
			_isTouch = false;
		}
	}


	private IEnumerator ScaleUpDown()
	{

		for (int i = _stackObjects.Count-1; i >= 0; i--)
		{
			SetStackObjectScale(_stackObjects[i]);
			yield return new WaitForSeconds(0.05f);
		}
	
	}

	private void SetStackObjectScale(Transform stackObject)
	{
		stackObject.DOScale(Vector3.one * 1.5f, .15f).OnComplete((() =>
		{
			stackObject.DOScale(Vector3.one, .15f);
		}));
	}

	private IEnumerator MoveToZero()
	{
		for (int i = 0; i <_stackObjects.Count; i++)
		{
			_stackObjects[i].transform.DOLocalMoveX(0, .25f);
			yield return new WaitForSeconds(0.05f);
		}
	}




}




	
