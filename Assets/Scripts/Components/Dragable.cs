using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dragable : MonoBehaviour
{
	protected Vector3 _dragOffset;
	protected float _speedDrag = 15f;
	protected bool _isDragable = false;

	protected void Start()
	{
		LevelManager.Instance.OnLevelStarted += OnLevelStartedHandler;
		LevelManager.Instance.OnPlayButtonClicked += OnPlayButtonClickedHandler;
	}

	protected void OnMouseDown()
	{
		_dragOffset = transform.position - GetMousePosition();
	}

	protected virtual void OnMouseDrag()
	{
		transform.position = GetMousePosition() + _dragOffset;
		transform.position = Vector3
			.MoveTowards(transform.position, GetMousePosition() + _dragOffset, _speedDrag * Time.deltaTime);
	}

	protected Vector3 GetMousePosition()
	{
		var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		mousePosition.z = 1;

		return mousePosition;
	}

	protected void OnLevelStartedHandler()
	{
		_isDragable = true;
	}

	protected void OnPlayButtonClickedHandler()
	{
		_isDragable = false;
	}

	protected void OnDestroy()
	{
		LevelManager.Instance.OnLevelStarted -= OnLevelStartedHandler;
		LevelManager.Instance.OnPlayButtonClicked -= OnPlayButtonClickedHandler;
	}
}