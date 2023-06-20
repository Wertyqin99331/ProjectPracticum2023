using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private float _smoothing = 5f;

    private Vector3 _offset;
    private bool _isFixed = false;
    private CameraFollower _instance;
    private Transform _target;
    private Transform _cameraPoint;
    
    private void Start()
    {
        _target = FindObjectOfType<Player>().transform;
        _cameraPoint = FindObjectOfType<CameraPoint>().transform;
        
        _offset = transform.position - _target.position;
        LevelManager.Instance.OnLevelStarted += OnLevelStartedHandler;
        LevelManager.Instance.OnLevelRestarted += OnLevelRestartedHandler;
        LevelManager.Instance.OnLevelEnded += OnLevelEndedHandler;
    }

    private void FixedUpdate()
    {
        if (!_isFixed)
        {
            var targetCamPos = _target.position + _offset + Vector3.up * 3;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, _smoothing * Time.deltaTime);
        }
    }

    private void FixCamera(Vector3 position)
    {
        gameObject.transform.position = new Vector3(position.x, position.y, -10);
        _isFixed = true;
    }

    private void UnfixCamera() => _isFixed = false;

    private void OnLevelStartedHandler()
    {
        FixCamera(_cameraPoint.position);
    }

    private void OnLevelEndedHandler() => UnfixCamera();

    private void OnLevelRestartedHandler()
    {
        _target = FindObjectOfType<Player>().transform;
        _cameraPoint = FindObjectOfType<CameraPoint>().transform;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnLevelStarted -= OnLevelStartedHandler;
        LevelManager.Instance.OnLevelRestarted -= OnLevelRestartedHandler;
        LevelManager.Instance.OnLevelEnded -= OnLevelEndedHandler;
    }
}