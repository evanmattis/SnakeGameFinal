using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Imports and class declaration
public class SpawnFood : MonoBehaviour {  
    //Declares the foodPrefab, which is just a pixel that is duplicated for the food spawning process
    public GameObject foodPrefab;
    //Begins calling the spawnFood method 1 second after the game begins, and does so every 2 seconds thereafter
    void Start () {
        InvokeRepeating("spawnFood", 1, 2);
	}
    //Method that outlines the food spawning
    void spawnFood()
    {
        //The x border is 69 units long, and goes from -34.5 to 34.5. However, since spawning of food inside the border is to be avoided,
        //the range is 1.5 unit less than it. Creates a random int and rounds to the nearest whole number, so that the food won't spawn in 
        //on half a pixel
        int xCoordinate = (int)Random.Range(-33,33);
        //Same for the y axis
        int yCoordinate = (int)Random.Range(-23,23);
        //Creates a new food unit at the coordinates randomly generated above, and uses the foodPrefab, which outlines what a food unit
        //looks like
        Instantiate(foodPrefab,
                    new Vector2(xCoordinate, yCoordinate),
                    Quaternion.identity);

    }
}
