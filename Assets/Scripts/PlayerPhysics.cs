using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {

	public LayerMask collisionMask;

	private BoxCollider collider;
	private Vector3 size;
	private Vector3 center;

	private float skin = 0.005f;

	[HideInInspector]
	public bool grounded;

	Ray ray;
	RaycastHit hit;

	void Start() {
		collider = GetComponent<BoxCollider>();
		size = collider.size;
		center = collider.center;
	}

	public void Move(Vector2 amount) {
		float deltaY = amount.y;
		float deltaX = amount.x;
		Vector2 p = transform.position;

		for(int i = 0; i < 3; i++) {
			float dir = Mathf.Sign (deltaY);
			float x = (p.x + center.x - size.x/2) + size.x / 2 * i;
			float y = p.y + center.y + size.y/2 * dir;

			ray = new Ray(new Vector2(x,y), new Vector2(0, dir));
			Debug.DrawRay(ray.origin, ray.direction);

			if(Physics.Raycast(ray, out hit, Mathf.Abs (deltaY), collisionMask)) {
				float dst = Vector3.Distance(ray.origin, hit.point);

				if(dst > skin) {
					deltaY = -dst + skin;
				} else {
					deltaY = 0;
				}
				grounded = true;
				break;
			}
		}

		Vector2 finalTransform = new Vector2(deltaX, deltaY);
		transform.Translate(finalTransform);
	}

}
