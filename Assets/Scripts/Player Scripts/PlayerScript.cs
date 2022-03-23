using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float moveForce = 20f;
	public float jumpForce = 700f;
	public float maxVelocity = 4f;

	private bool grounded;

	private Rigidbody2D myBody;
	private Animator anim;

	private bool moveLeft, moveRight;

	void Awake(){	
		myBody = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator> ();
		GameObject.Find ("Jump").GetComponent<Button> ().onClick.AddListener (() => Jump());
	}

	public void SetMoveLeft(bool moveLeft){
		this.moveLeft = moveLeft;
		this.moveRight = !moveLeft;
	}

	public void StopMoving(){
		this.moveLeft = false;
		this.moveRight = false;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//PlayerWalkJoyStick ();
		PlayerWalkKeyBoard ();
	}

	void PlayerWalkJoyStick(){
		float forceX = 0f;
		float vel = Mathf.Abs (myBody.velocity.x);

		if (moveRight) {

			if (vel < maxVelocity) {
				if (grounded) {
					forceX = moveForce;
				} else {
					forceX = moveForce * 1.1f;
				}
			}
			
			Vector3 scale = transform.localScale;
			scale.x = 1f;
			transform.localScale = scale;
			
			anim.SetBool ("Walk", true);

		} else if (moveLeft) {

			if (vel < maxVelocity) {
				if (grounded) {
					forceX = -moveForce;
				} else {
					forceX = -moveForce * 1.1f;
				}
			}
			
			Vector3 scale = transform.localScale;
			scale.x = -1f;
			transform.localScale = scale;
			
			anim.SetBool ("Walk", true);

		} else {
			anim.SetBool ("Walk", false);
		}

		myBody.AddForce (new Vector2 (forceX, 0));

	}

	void PlayerWalkKeyBoard(){
		float forceX = 0f;
		float forceY = 0f;

		float vel = Mathf.Abs (myBody.velocity.x);

		float h = Input.GetAxisRaw ("Horizontal"); //-1 0 1

		if (h > 0) {
			if (vel < maxVelocity) {
				if(grounded){
					forceX = moveForce;
				}else{
					forceX = moveForce * 1.1f;
				}
			}

			Vector3 scale = transform.localScale;
			scale.x = 1f;
			transform.localScale = scale;

			anim.SetBool ("Walk", true);

		} else if (h < 0) {
			if (vel < maxVelocity) {
				if(grounded){
					forceX = -moveForce;
				}else{
					forceX = -moveForce * 1.1f;
				}
			}
			
			Vector3 scale = transform.localScale;
			scale.x = -1f;
			transform.localScale = scale;
			
			anim.SetBool ("Walk", true);
		} else if (h == 0) {
			anim.SetBool("Walk",false);
		}

		if (Input.GetKey (KeyCode.Space)) {
			if(grounded){
				grounded = false;
				forceY = jumpForce;
			}
		}

		myBody.AddForce (new Vector2 (forceX, forceY));
	}

	public void BouncePlayerWithBouncy(float force){
		if (grounded) {
			grounded = false;
			myBody.AddForce(new Vector2(0,force));
		}
	}

	public void Jump(){
		if (grounded) {
			grounded = false;
			myBody.AddForce(new Vector2(0,jumpForce));
		}
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Ground") {
			grounded = true;
		}
	}
}





















