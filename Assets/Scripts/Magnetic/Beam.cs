using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class Beam : MonoBehaviour
{
	public Charge CurrentCharge = Charge.Zero;
	public Transform ForcePoint => _forcePoint;

	[SerializeField] private Transform _forcePoint;
	[SerializeField] private float strength = 10f;
	[SerializeField] private float radius = 5f;
	[SerializeField] private LayerMask _layerMask;
	[SerializeField] private Charge _charge;

	private HingeJoint2D _joint;
	private Rigidbody2D _rb;

	private void Start()
	{
		_joint = GetComponent<HingeJoint2D>();
		_rb = GetComponent<Rigidbody2D>();
		_joint.enabled = false;
		LevelManager.Instance.OnPlayButtonClicked += OnPlayButtonClickedHandler;
		LevelManager.Instance.OnLevelEnded += OnLevelEndedHandler;
	}

	private void FixedUpdate()
	{
		if (CurrentCharge == Charge.Zero)
			return;
		var colliders = Physics2D.OverlapCircleAll(_forcePoint.position, radius, _layerMask)
			.Where(c => c.gameObject != gameObject);

		foreach (var collider in colliders)
		{
			if (collider.GetComponent<Beam>() && !collider.GetComponent<MotorBeam>())
			{
				var charge = collider.GetComponent<Beam>().CurrentCharge;
				AddMagneticForce(collider, charge);
			}

			if (collider.GetComponent<MagneticBall>() && !GetComponent<MotorBeam>())
			{
				var charge = collider.GetComponent<MagneticBall>().Charge;
				if (charge == Charge.Zero)
					continue;
				
				AddMagneticForce(collider, charge);
			}
		}
	}

	private void AddMagneticForce(Collider2D otherCollider, Charge otherCharge)
	{
		var direction = otherCollider.transform.position - transform.position;
		var distance = direction.magnitude;

		if (distance > 0)
		{
			var forceMagnitude = strength / distance;
			var force = otherCharge != CurrentCharge
				? direction.normalized * forceMagnitude
				: -direction.normalized * forceMagnitude;
			_rb.AddForceAtPosition(force, _forcePoint.position, ForceMode2D.Force);
		}
	}

	private void OnPlayButtonClickedHandler()
	{
		CurrentCharge = _charge;
		_joint.enabled = true;
		_rb.bodyType = RigidbodyType2D.Dynamic;
	}

	private void OnLevelEndedHandler()
	{
		CurrentCharge = Charge.Zero;
	}

	private void OnDestroy()
	{
		LevelManager.Instance.OnPlayButtonClicked -= OnPlayButtonClickedHandler;
		LevelManager.Instance.OnLevelEnded -= OnLevelEndedHandler;
	}
}