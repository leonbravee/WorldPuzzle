
using UnityEngine;

namespace LeonBrave
{


	public class DragDrop : MonoBehaviour
	{
		public static DragDrop Instance;

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


		[SerializeField] public Vector3 _offset = new Vector3(0, 1, 0);

		[SerializeField] 
		private LayerMask _dragLayer;
		[SerializeField]
		private LayerMask _groundLayer; 
		
		[SerializeField]
		private bool _canDrag = false;

		public bool CanDrag
		{
			set
			{
				_canDrag = value;
				if (!value)
				{
					_dragGameObject = null;
					_isDragging = false;
				}
			}
		}
		private bool _isDragging = false;


		private GameObject _dragGameObject;
		
		void Update()
		{
			
			if(!_canDrag) return;
			
			if (Application.isEditor)
			{

				if (Input.GetMouseButton(0))
				{
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;

					RaycastHit hitGround;

					Physics.Raycast(ray, out hit, Mathf.Infinity, _dragLayer);
					Physics.Raycast(ray, out hitGround, Mathf.Infinity, _groundLayer);

					if (hit.collider != null  && Input.GetMouseButtonDown(0)  && hit.transform.TryGetComponent(out TileObject tileObject) && tileObject!=null && tileObject.TileObjectState==TileObjectState.Selectable)
					{
						_isDragging = true;
						_dragGameObject = hit.collider.gameObject;
						tileObject.TileObjectState = TileObjectState.Selected;
					}

					if (_dragGameObject == null || hitGround.collider == null)
					{
						return;
					}
					
					if (_isDragging && Input.GetMouseButton(0))
					{
						_dragGameObject.transform.position = hitGround.point + _offset;
					}
				}
				else if (Input.GetMouseButtonUp(0))
				{
					_dragGameObject = null;
					_isDragging = false;
				}
				
			}
			else
			{
				if (Input.touchCount > 0)
				{
					Touch touch = Input.GetTouch(0);
				
					if (touch.phase == TouchPhase.Began)
					{
						Ray ray = Camera.main.ScreenPointToRay(touch.position);
						
						RaycastHit hit;

						Physics.Raycast(ray, out hit, Mathf.Infinity, _dragLayer);
						if(hit.collider!=null &&  hit.transform.TryGetComponent(out TileObject tileObject) && tileObject!=null && tileObject.TileObjectState==TileObjectState.Selectable) 
						{
							_dragGameObject = hit.collider.gameObject;
							_isDragging = true;
							tileObject.TileObjectState = TileObjectState.Selected;
						}
						
						
					} 
					else if (touch.phase == TouchPhase.Moved && _isDragging)
					{
						
						
						Ray ray = Camera.main.ScreenPointToRay(touch.position);
						RaycastHit hitGround;
						Physics.Raycast(ray, out hitGround, Mathf.Infinity, _groundLayer);
						
						if (_dragGameObject == null || hitGround.collider == null)
						{
							return;
						}
						
						_dragGameObject.transform.position= hitGround.point + _offset;
						
					}
					else if (touch.phase == TouchPhase.Ended)
					{
						_dragGameObject = null;
						_isDragging = false;
					}
				}
			}

		}


		
		
		
		
	}
}

