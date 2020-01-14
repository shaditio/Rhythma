using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int health;

    public void reduceHealth(int amount){
        health -= amount;
    }
}
