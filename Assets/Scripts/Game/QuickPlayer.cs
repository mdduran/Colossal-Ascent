using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickPlayer : MonoBehaviour
{
    private float Stamina;
    public float MaxStamina;
    public float StaminaRechargeRate;
    public float MoveSpeed;
    public float JumpHeight;
    public float Gravity;

    public float Goal;
    public float negativeBound;
    public float positiveBound;

    private Vector3 Velocity;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        // Calculate Goal
        float x = Mathf.Lerp(negativeBound, positiveBound, Goal);

        // Move Character
        float v = ((x - transform.position.x) * 2) / (positiveBound - negativeBound) * (MoveSpeed * (Stamina / MaxStamina));
        Velocity.x = v;
        transform.position = transform.position + Velocity * Time.deltaTime;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(v, 0));

        // Increase Stamina
        Stamina -= v * Time.deltaTime;
        Stamina += StaminaRechargeRate * Time.deltaTime;
        Stamina = Mathf.Min(MaxStamina, Stamina);
        Stamina = Mathf.Max(0, Stamina);


	}

    public void Jump()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, JumpHeight));
        Debug.Log("Jumped");
    }


}
