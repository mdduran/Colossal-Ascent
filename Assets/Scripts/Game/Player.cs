using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public CharacterController characterController;
    public Animator animator;
    public Sprite sprite;
    public SpriteRenderer sr;
    public int hp;
    public int playerNum;
    public bool hit;
    public float moveSpeed;
    public float fallSpeed;
    public Vector2 newPoint;
    public Vector2 goalPoint;
    public int score;
    public bool alive;
    public Rigidbody2D armRigidBody;
    public GameObject weapon;
    public GameObject bullet;
    public bool grounded;
    public float timePressed;
    // Use this for initialization
    void Start () {

        this.hp = 1;
        this.playerNum = 0;
        this.hit = false;
        this.moveSpeed = 20;
        this.fallSpeed = 8;
        this.newPoint.x = 0;
        this.newPoint.y = 0;
        this.goalPoint.x = 0;
        this.goalPoint.y = 0;
        this.score = 0;
        this.alive = true;
        this.armRigidBody = GetComponent<Rigidbody2D>();
        this.characterController = (CharacterController)GetComponent(typeof(CharacterController));
        this.weapon = GetComponent<GameObject>();
        grounded = true;
        timePressed = 0;
        if (this.sprite == null)
        {
            Debug.LogError("Sprite not found!");
        }
        if (this.animator != null)
        {
            this.animator = GetComponent<Animator>();
        }
        if (this.characterController != null)
        {
            this.characterController = (CharacterController)GetComponent(typeof(CharacterController));
        }
        if (this.weapon != null)
        {
            this.weapon = GetComponent<GameObject>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        SetDestination(newPoint);
        SetMovespeed(moveSpeed);
        SetPlayerNum(playerNum);
        SetJump(timePressed);
        Aim(0);
        Shoot();
        SetScore(score);
        IsAlive(hp);
        IsGrounded();
	}

    /*
     * SetDestination
     * Purpose: Store Vector2 which has XY-coordinate of destination position
     * Input: Vector2 dest
     * Output: void
     */
    void SetDestination(Vector2 dest) {
        goalPoint = dest;
    }

    /*
     * GetDestination
     * Purpose: gets target point for player's character to track to.
     * Input: void
     * Output: Vector2 dest
     */
    Vector2 GetDestination() {
        return goalPoint;
    }

    /*
     * setMovespeed
     * Purpose: create float speed at which player travels
     * Input: float speed
     * Output: void
     */
    void SetMovespeed(float speed) {
        moveSpeed = speed;
    }

    /*
     * GetMovespeed
     * Purpose: return float speed at which player travels
     * Input: void
     * Output: int speed
     */
    float GetMovespeed() {
        return moveSpeed;
    }

    /*
    * SetPlayerNum
    * Purpose: give players an integer value 1-4 to determine who is P1, P2, etc
    * Input: int value
    * Output: void
    */
    void SetPlayerNum(int value)
    {
        playerNum = value;
    }

    /*
     * GetPlayerNum
     * Purpose: return integer value for P1, P2 etc.
     * Input: void
     * Output: int speed
     */
    float GetPlayerNum()
    {
        return playerNum;
    }

    /*
     * SetJump
     * Purpose: sets float value of velocity for rigidBody
     * Input: float value
     * Output: void
     */

        //Incorporate time of button press
    void SetJump(float value) {
        if (moveSpeed > 0f) {
            characterController.SimpleMove(new Vector3(moveSpeed, characterController.velocity.y, 0));
        }
        else if (moveSpeed < 0f) {
            characterController.SimpleMove(new Vector3(moveSpeed, characterController.velocity.y, 0));
        }
        else {
            characterController.SimpleMove(new Vector3(0, characterController.velocity.y, 0));
        }
    }
    
    /*
     * Aim
     * Purpose: Aim firearm towards direction
     * Input: float potVal
     * Output: void
     */
     void Aim (float potVal) {
        armRigidBody.MoveRotation(potVal/255*180);
     }

    /*
     * Shoot
     * Purpose: Shoot projectile in direction
     */
     void Shoot() {
        //Check for button press
        GameObject projectile = Instantiate<GameObject>(bullet, transform.position, transform.rotation);
        Vector2 temp;
        temp.x = 0;
        temp.y = 1000;
        projectile.GetComponent<Rigidbody2D>().AddForce(temp);
         
     }
    
    /*
     * SetScore
     * Purpose: Integer value of enemy upon death
     * Input: int value
     * Output: void
     */
    void SetScore(int value)
    {
        score = value;
    }

    /*
     * incrementScore
     * Purpose: Integer value of enemy upon death
     * Input: int value
     * Output: void
     */
    void IncrementScore(int value)
    {
        score += value;
    }

    /*
     * decrimentScore
     * Purpose: Integer value of enemy upon death
     * Input: int value
     * Output: void
     */
    void DecrimentScore(int value)
    {
        score -= value;
    }

    /*
     * getScore
     * Purpose: Integer value of player upon death or end of game
     * Input: void
     * Output: int value
     */
    int GetScore()
    {
        return score;
    }

    /*
     * isAlive
     * Purpose: Sets boolean of player is alive or dead
     * Input: int hp
     * Output: bool alive
     */
    bool IsAlive(int hp) {
        if (hp <= 0) {
            alive = false;
        }
        else {
            alive = true;
        }
        return alive;
    }

    /*
     * isGrounded
     * Purpose: Sets boolean if character is on ground or not
     * Input: void
     * Output: bool grounded
     */
    bool IsGrounded() {
        if (characterController.isGrounded) {
            grounded = true;
        }
        else {
            grounded = false;
        }
        return grounded;
    }
}
