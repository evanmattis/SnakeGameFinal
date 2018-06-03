using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Imports and class declaration
public class RedColor : MonoBehaviour {
    //Declares the RedColorPrefab 
    public GameObject RedColorPrefab;
    void Start () {
    //Randomizes the spawn time of the special food
    float randomTime = (float) Random.Range(10, 30);
    //Spawning starts 5 seconds after the game starts, and from there spawns according to randomTime
    InvokeRepeating("spawnRed", 5, randomTime);	
	}
	//Method for spawning the red food
	void spawnRed()
    {
        //Sets x and y coordinates to the bottom left corner, 
        //this food is designed to be hard to eat
        int xCoordinate = (int)Random.Range(-33, -30);
        int yCoordinate = (int)Random.Range(-23, -20);
        //Places the red food into the game
        Instantiate(RedColorPrefab,
                    new Vector2(xCoordinate, yCoordinate),
                    Quaternion.identity);
    }
}
