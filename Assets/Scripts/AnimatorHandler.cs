using UnityEngine;

namespace LS {
	public class AnimatorHandler : MonoBehaviour {
		public Animator animator;
		public bool canRotate;

		private int vertical;
		private int horizontal;

		public void Initialize() {
			animator = GetComponent<Animator>();
			vertical = Animator.StringToHash("Vertical");
			horizontal = Animator.StringToHash("Horizontal");
		}

		public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement) {
			float v = 0;

			if (verticalMovement > 0 && verticalMovement < 0.55f) {
				v = 0.5f;
			} else if (verticalMovement > 0.55f) {
				v = 1;
			} else if (verticalMovement < 0 && verticalMovement > -0.55f) {
				v = -0.5f;
			} else if (verticalMovement < -0.55f) {
				v = -1;
			} else {
				v = 0;
			}

			float h = 0;

			if (horizontalMovement > 0 && horizontalMovement < 0.55f) {
				h = 0.5f;
			} else if (horizontalMovement > 0.55f) {
				h = 1;
			} else if (horizontalMovement < 0 && horizontalMovement > -0.55f) {
				h = -0.5f;
			} else if (horizontalMovement < -0.55f) {
				h = -1;
			} else {
				h = 0;
			}

			animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);
			animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
		}

		public void CanRotate() {
			canRotate = true;
		}

		public void StopRotation() {
			canRotate = false;
		}
	}
}