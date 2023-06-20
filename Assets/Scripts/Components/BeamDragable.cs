using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeamDragable : MonoBehaviour
{
	private Vector3 _dragOffset;
	private float _speedDrag = 15f;
	private bool _isDragable = false;
	private HingeJoint2D _joint;

	private void Start()
	{
		_joint = GetComponent<HingeJoint2D>();
		LevelManager.Instance.OnLevelStarted += OnLevelStartedHandler;
		LevelManager.Instance.OnPlayButtonClicked += OnPlayButtonClickedHandler;
	}

	private void OnMouseDown()
	{
		_dragOffset = transform.position - GetMousePosition();
	}

	private void OnMouseDrag()
	{
		if (_isDragable && !GetComponent<MotorBeam>())
		{
			transform.position = GetMousePosition() + _dragOffset;
			transform.position = Vector3
				.MoveTowards(transform.position, GetMousePosition() + _dragOffset, _speedDrag * Time.deltaTime);
			_joint.anchor.Set(transform.position.x, transform.position.y);
		}
	}

	private Vector3 GetMousePosition()
	{
		var mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		mousePosition.z = 0;

		return mousePosition;
	}

	private void OnLevelStartedHandler() => _isDragable = true;
	private void OnPlayButtonClickedHandler() => _isDragable = false;

	private void OnDestroy()
	{
		LevelManager.Instance.OnLevelStarted -= OnLevelStartedHandler;
		LevelManager.Instance.OnPlayButtonClicked -= OnPlayButtonClickedHandler;
	}
}