﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour {
    public CharacterController characterController;
    //public Animator animator;
    public Sprite sprite;
    public SpriteRenderer sr;
    public int hp;
    public bool hit;
    public float moveSpeed;
    public int numPatrolPoints;
    public Vector2[] patrolPoints;
    public Vector2 newPoint;
    public Vector2 goalPoint;
    public Vector2 currPoint;
    public int currentPatrolIndex;
    public int score;
    public bool alive;
    public bool isPaused;
    public float distModifier;
    // Use this for initialization
    void Start () {
        this.hp = 1;
        this.moveSpeed = 20;
        if(this.patrolPoints.Length <= 0)
            this.patrolPoints = new Vector2[numPatrolPoints];
        this.currentPatrolIndex = 0;
        this.score = 0;
        this.alive = true;
        this.characterController = (CharacterController)GetComponent(typeof(CharacterController));
        if (this.sprite == null) {
            Debug.LogError("Sprite not found!");
        }
        /*if (this.animator != null) {
            this.animator = GetComponent<Animator>();
        }*/
        if (this.characterController != null)
        {
            this.characterController = (CharacterController)GetComponent(typeof(CharacterController));
        }

        distModifier = 0.5f;
    }
	
	// Update is called once per frame
	void Update () {
        if(hp > 0)
        {
            if(patrolPoints.Length > 1)
            {
                //set the goal to be 1 more than the current patrol point
                goalPoint = patrolPoints[currentPatrolIndex++];
                
            }
            
        }
        
	}

    public int nextGoal()
    {
        int nextIndex;
        if (Vector2.Distance(transform.position, goalPoint) <= distModifier) //Reached goal node
        {
            nextIndex = 0;

        }
        else //Have not reached goal node
        {
            
            nextIndex = currentPatrolIndex++;
        }
        return nextIndex;
    }


    /*
     * setHealth
     * Purpose: Set current integer health of enemy
     * Input: bool hit
     * Output: void
     */
    void SetHealth (bool hit) {
        if (hit)
        {
            hp = hp - 1;
        }
    }

    /*
    * isHit
    * Purpose: Set current integer health of enemy
    * Input: bool hit
    * Output: void
    */
    void IsHit (bool hit)
    {
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
    int GetHealth() {
        return hp;
    }

    /*
     * setMovespeed
     * Purpose: create float speed at which enemy travels
     * Input: int speed
     * Output: void
     */
    void SetMovespeed (float speed) {
        moveSpeed = speed;
    }

    /*
     * GetMovespeed
     * Purpose: return float speed at which enemy travels
     * Input: void
     * Output: int speed
     */
    float GetMovespeed() {
        return moveSpeed;
    }

    /*
     * AddPatrolPoints
     * Purpose: Adds 1 new patrol point to the list of patrol points
     * Input: vector2
     * Output: void
     */

    void AddPatrolPoint(Vector2 newPoint){
        Vector2[] clone = patrolPoints;
        patrolPoints = new Vector2[clone.Length + 1];
        clone.CopyTo(patrolPoints, 0);
        patrolPoints[patrolPoints.Length + 1] = newPoint;
    }

    /*
     * goalPoint
     * Purpose: Go to next node, store goal node as an integer
     * Input: void
     * Output: vector2
     */
    Vector2 GetGoalPoint() {
        Vector2 temp = (Vector2) (patrolPoints[0]);
        //patrolPoints.RemoveAt(0);
        return temp;
    }

    /*
     * SetScore
     * Purpose: Integer value of enemy upon death
     * Input: int value
     * Output: void
     */
    void SetScore(int value) {
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
     * Purpose: Integer value of enemy upon death
     * Input: void
     * Output: int value
     */
    int GetScore() {
        return score;
     }

    /*
     * isAlive
     * Purpose: Sets boolean of enemy is alive or dead
     * Input: int hp
     * Output: bool alive
     */
     bool IsAlive (int hp) {
        if (hp <= 0) {
            alive = false;
        }
        else {
            alive = true;
        }
        return alive;
     }
}
