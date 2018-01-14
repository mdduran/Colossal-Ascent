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

    private CharacterController CC;
    public float Goal;
    public float negativeBound;
    public float positiveBound;

    private Vector3 Velocity;

	// Use this for initialization
	void Start () {
        CC = GetComponent<CharacterController>();
        Goal = 0.2f;
	}
	
	// Update is called once per frame
	void Update () {

        // Calculate Goal
        float x = Mathf.Lerp(negativeBound, positiveBound, Goal);

        // Move Character
        float v = ((x - transform.position.x) * 2) / (positiveBound - negativeBound) * (MoveSpeed * (Stamina / MaxStamina));
        Velocity.x = v;
        CC.Move(Velocity * Time.deltaTime);

        // Increase Stamina
        Stamina -= v * Time.deltaTime;
        Stamina += StaminaRechargeRate * Time.deltaTime;
        Stamina = Mathf.Min(MaxStamina, Stamina);
        Stamina = Mathf.Max(0, Stamina);
	}

}
