using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalState
{
    idle,
    walk,
    hide, 
    wakeUp,
    stop

}

public class Animal : MonoBehaviour
{
    public AnimalState currentState;
    //public FloatValue maxHealth;
    //public float health;
    public string AnimalName;
    // public int baseAttack;
    public float moveSpeed;

    private void Awake() 
    {
        //health = maxHealth.initialValue;
    }
}
