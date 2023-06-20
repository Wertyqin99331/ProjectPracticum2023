using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class MagneticBall : MonoBehaviour
{
    [SerializeField] private float strength = 10f;
    [SerializeField] private float radius = 5f;
    [SerializeField] private LayerMask _layerMask;

    public bool IsRight = false;
    public Charge Charge = Charge.Minus;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        LevelManager.Instance.OnPlayButtonClicked += OnPlayButtonClickedHandler;
        LevelManager.Instance.OnLevelEnded += OnLevelEndedHandler;
    }

    private void FixedUpdate()
    {
        if (Charge == Charge.Zero)
            return;
        
        var colliders = Physics2D.OverlapCircleAll(transform.position, radius, _layerMask)
            .Where(c => c.gameObject != gameObject);

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<MagneticBall>())
            {
                var charge = collider.GetComponent<MagneticBall>().Charge;
                AddMagneticForce(charge, collider.transform);
            }

            if (collider.GetComponent<Beam>())
            {
                var charge = collider.GetComponent<Beam>().CurrentCharge;
                AddMagneticForce(charge, collider.GetComponent<Beam>().ForcePoint);
            }
        }
    }

    private void AddMagneticForce(Charge otherCharge, Transform magneticPosition)
    {
        var direction = magneticPosition.position - transform.position;
        var distance = direction.magnitude;

        if (distance > 0)
        {
            if (otherCharge == Charge.Zero)
                return;
            
            var forceMagnitude = strength / distance;
            var force = otherCharge != Charge
                ? direction.normalized * forceMagnitude
                : -direction.normalized * forceMagnitude;/*direction.normalized * forceMagnitude;*/
            _rb.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void OnPlayButtonClickedHandler()
    {
        
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnLevelEndedHandler()
    {
        _rb.bodyType = RigidbodyType2D.Static;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnPlayButtonClicked -= OnPlayButtonClickedHandler;
        LevelManager.Instance.OnLevelEnded -= OnLevelEndedHandler;
    }
}
