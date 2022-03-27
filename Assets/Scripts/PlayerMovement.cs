using UnityEngine;

namespace LS {
	public class PlayerMovement : MonoBehaviour {
		
		[HideInInspector]
		public Transform myTransform;
		[HideInInspector]
		public AnimatorHandler animatorHandler;
		public new Rigidbody rigidbody;
		public GameObject normalCamera;

		[Header("Stats")]
		[SerializeField]
		private float movementSpeed = 5;
		[SerializeField]
		private float rotationSpeed = 10;
		private Transform cameraObject;
		private InputHandler inputHandler;
		private Vector3 moveDirection;

		
		void Start() {
			rigidbody = GetComponent<Rigidbody>();
			inputHandler = GetComponent<InputHandler>();
			animatorHandler = GetComponentInChildren<AnimatorHandler>();
			
			animatorHandler.Initialize();

			cameraObject = Camera.main.transform;
			myTransform = transform;
		}

		#region Movement

		private Vector3 normalVector;
		private Vector3 targetPosition;

		private void HandleRotation(float delta) {
			Vector3 targetDir = Vector3.zero;
			float moveOverride = inputHandler.moveAmount;

			targetDir = cameraObject.forward * inputHandler.vertical;
			targetDir += cameraObject.right * inputHandler.horizontal;

			targetDir.Normalize();
			targetDir.y = 0;

			if (targetDir == Vector3.zero) {
				targetDir = myTransform.forward;
			}

			float rs = rotationSpeed;

			Quaternion tr = Quaternion.LookRotation(targetDir);
			Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

			myTransform.rotation = targetRotation;
		}

		#endregion
	
		private void Update() {
			float delta = Time.deltaTime;

			inputHandler.TickInput(delta);

			moveDirection = cameraObject.forward * inputHandler.vertical;
			moveDirection += cameraObject.right * inputHandler.horizontal;
			moveDirection.Normalize();

			float speed = movementSpeed;
			moveDirection *= speed;

			Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
			rigidbody.velocity = projectedVelocity;

			animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

			if (animatorHandler.canRotate) {
				HandleRotation(delta);
			}
		}
	}
}