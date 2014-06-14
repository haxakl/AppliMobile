using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour
{

		// Player Handling
		public float gravity = 20;
		public float speed = 8;
		public float acceleration = 12;
		private PlayerPhysics playerPhysics;
		private float currentSpeed;
		private float targetSpeed;
		private Vector2 amountToMove;

		// Use this for initialization
		void Start ()
		{
				playerPhysics = GetComponent<PlayerPhysics> ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				targetSpeed = Input.GetAxisRaw ("Horizontal") * speed;
				currentSpeed = this.incrementTowards (currentSpeed, targetSpeed, acceleration);

				amountToMove.x = currentSpeed;
				amountToMove.y -= gravity * Time.deltaTime;
				playerPhysics.Move (amountToMove * Time.deltaTime);
		}

		// Increase n towards target by speed
		private float incrementTowards (float n, float target, float speed)
		{
				if (n == target) {
						return n;
				} else {
						float dir = Mathf.Sign (target - n);
						n += speed * Time.deltaTime * dir;
						return (dir == Mathf.Sign (target - n)) ? n : target;
				}
		}
}
