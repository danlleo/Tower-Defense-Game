using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic.PathFinders
{
	public class PathFinder : MonoBehaviour
	{
		private const string Hittable = "Hittable";
		private const string MoveKeyPoint = "MoveKeyPoint";

		[SerializeField] private float _speed = 5;
		[SerializeField] private List<MoveKeyPoint> _movePoints;
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private float _rotationSpeed;

		private List<MoveKeyPoint> _passedMovePoints;
		private RaycastHit[] _hits = new RaycastHit[1];
		private int _layerMask;
		private Vector3 _direction;
		private bool _isReached;
		private Coroutine _moveCoroutine;

		private void Awake() =>
			_layerMask = 1 << LayerMask.NameToLayer(Hittable);

		private void Start() =>
			InitPoints();

		private void Update()
		{
			Debug.DrawRay(transform.position, _direction, Color.red, 2.0f);
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag(MoveKeyPoint))
			{
				other.gameObject.SetActive(false);
				StopAllCoroutines();
				_moveCoroutine = null;
				_movePoints.Remove(_movePoints.FirstOrDefault());

				if (_movePoints.Count < 1)
					return;

				InitPoints();
			}
		}

		private void InitPoints()
		{
			SortInDistance();
			ThrowRayToNearestPoint();
		}

		private void ChoosePoints()
		{
			ShiftKeyPoints();
			ThrowRayToNearestPoint();
		}

		private void ShiftKeyPoints()
		{
			if (_movePoints.Count <= 1)
			{
				return;
			}

			MoveKeyPoint firstElement = _movePoints[0];

			for (int i = 0; i < _movePoints.Count - 1; i++)
			{
				_movePoints[i] = _movePoints[i + 1];
			}

			_movePoints[^1] = firstElement;
		}

		private void ThrowRayToNearestPoint()
		{
			MoveKeyPoint nearestObject = _movePoints.FirstOrDefault();

			if (!nearestObject)
				return;

			_direction = nearestObject.transform.position - transform.position;
			int hitCount = Physics.RaycastNonAlloc(transform.position, _direction, _hits, 5000, _layerMask);

			if (hitCount > 0)
			{
				ChoosePoints();
			}
			else
			{
				_moveCoroutine ??= StartCoroutine(MoveAndRotateToTarget(nearestObject.transform));
			}
		}

		private IEnumerator MoveAndRotateToTarget(Transform targetPosition)
		{
			yield return StartCoroutine(RotateTowards(targetPosition));

			yield return StartCoroutine(MoveToPoint(targetPosition));
		}

		private IEnumerator RotateTowards(Transform targetPosition)
		{
			Quaternion startQuaternion = transform.rotation;

			Vector3 targetDirection = targetPosition.position - transform.position;
			targetDirection.y = 0;
			Quaternion targetQuaternion = Quaternion.LookRotation(targetDirection.normalized);

			float elapsedTime = 0f;

			while (elapsedTime < _rotationSpeed)
			{
				float t = elapsedTime / _rotationSpeed;
				transform.rotation = Quaternion.Slerp(startQuaternion, targetQuaternion, t);
				elapsedTime += Time.deltaTime;
				yield return new WaitForFixedUpdate();
			}

			transform.rotation = targetQuaternion;
		}

		private IEnumerator MoveToPoint(Transform nearestObject)
		{
			while (true)
			{
				Vector3 direction = ( nearestObject.position - transform.position ).normalized;
				direction.y = 0;
				_rigidbody.velocity = direction * _speed;
				yield return new WaitForFixedUpdate();
			}
		}

		private void SortInDistance()
		{
			_movePoints.Sort((a, b) =>
			{
				Vector3 referencePoint = transform.position;
				float distanceA = Vector3.Distance(a.transform.position, referencePoint);
				float distanceB = Vector3.Distance(b.transform.position, referencePoint);
				return distanceA.CompareTo(distanceB);
			});
		}
	}

	public class EnemyMove : MonoBehaviour
	{
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private float _speed;

		private void OnValidate()
		{
			_rigidbody ??= GetComponent<Rigidbody>();
		}

		public void StartMove(Transform target)
		{
			StartCoroutine(MoveToPoint(target));
		}	
		
		public void StopMove(Transform target)
		{
			StopCoroutine(MoveToPoint(target));
		}
		
		private IEnumerator MoveToPoint(Transform nearestObject)
		{
			while (true)
			{
				Vector3 direction = ( nearestObject.position - transform.position ).normalized;
				direction.y = 0;
				_rigidbody.velocity = direction * _speed;
				yield return new WaitForFixedUpdate();
			}
		}
	}	

}