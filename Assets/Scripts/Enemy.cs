using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {
    public CharacterController characterController;
    public Animator animator;
    public Sprite sprite;
    public int hp;
    public float moveSpeed;
    public ArrayList patrolPoints;
    public Vector2 goalPoint;
    public int score;
    public bool alive;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /*
     * setHealth
     * Purpose: Set current integer health of enemy
     * Input: bool hit
     * Output: void
     */
    void setHealth (bool hit) {
        if (hit)
        {
            hp = hp - 1;
        }
    }

    /*
     * getHealth
     * Purpose: Return current integer health of enemy
     * Input: void
     * Output: int hp
     */
    int getHealth() {
        return hp;
    }

    /*
     * setMovespeed
     * Purpose: create float speed at which enemy travels
     * Input: int speed
     * Output: void
     */
    void setMovespeed (float speed) {
        moveSpeed = speed;
    }

    /*
     * getMovespeed
     * Purpose: return float speed at which enemy travels
     * Input: void
     * Output: int speed
     */
    float getMovespeed() {
        return moveSpeed;
    }

    /*
     * setPatrolPoints
     * Purpose: Store point locations for enemy to navigate to, append vector2 directions into a list to navigate through
     * Input: vector2
     * Output: void
     */

    void setPatrolPoints(Vector2 newPoint){
        patrolPoints.Add(newPoint);
    }

    /*
     * goalPoint
     * Purpose: Go to next node, store goal node as an integer
     * Input: void
     * Output: vector2
     */
    Vector2 setPatrolPoints() {
        Vector2 temp = (Vector2) (patrolPoints[0]);
        patrolPoints.RemoveAt(0);
        return temp;
    }

    /*
     * setScore
     * Purpose: Integer value of enemy upon death
     * Input: int value
     * Output: void
     */
    void setScore(int value) {
        score = value;
     }

    /*
     * incrementScore
     * Purpose: Integer value of enemy upon death
     * Input: int value
     * Output: void
     */
    void incrementScore(int value)
    {
        score += value;
    }

    /*
     * decrimentScore
     * Purpose: Integer value of enemy upon death
     * Input: int value
     * Output: void
     */
    void decrimentScore(int value)
    {
        score -= value;
    }

    /*
     * getScore
     * Purpose: Integer value of enemy upon death
     * Input: void
     * Output: int value
     */
    int getScore() {
        return score;
     }

    /*
     * setAlive
     * Purpose: Sets boolean of enemy is alive or dead
     * Input: int hp
     * Output: bool alive
     */
     bool isAlive (int hp) {
        if (hp <= 0) {
            alive = false;
        }
        else {
            alive = true;
        }
        return alive;
     }
}
