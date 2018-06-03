using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Imports and class declaration
public class Snake : MonoBehaviour
{
    // Setting if the snake ate something to false
    public bool ateFood = false;
    //Initializing the text that displays the score
    public Text scoreText;
    // Prefab of snake, which allows for the manipulation and cloning necessary to make the snake move
    public GameObject tailPrefab;
    //List that keeps track of all the individual parts of the snake; necessary for cloning and other manipulation
    public List<Transform> snakeBody = new List<Transform>();
    // Declaring the 2 dimensional vector necessary to keep track of the snake's movement direction,
    // by default the snake is moving to the left
    public Vector2 direction = Vector2.left;
    // Count to keep track of the score (how many food pellets the snake has eaten)
    public int count;
    // Use this for initialization
    void Start()
    {
        // Calls the Move method every 50 milliseconds throughout the game
        InvokeRepeating("moveSnake", 0.05f, 0.05f);
        // Sets the score to 0 
        count = 0;
        // Converts the integer version of count into a text version, which then is used to display the score
        scoreText.text = "Score: " + count.ToString();   
    }

    // Method that updates the movement vector of the snake depending on which arrow key the user presses
    void Update()
    {
        // If the user presses the down arrow, the new direction is the negative of the 
        // up vector (down), if they do not press it, then the code scans for the other possible key inputs
        // If-else configuration is needed to avoid a double input
        if (Input.GetKey(KeyCode.DownArrow))
            direction = -Vector2.up;    
        else if (Input.GetKey(KeyCode.UpArrow))
            direction = Vector2.up;
        else if (Input.GetKey(KeyCode.RightArrow))
            direction = Vector2.right;
        //As with the down arrow, the left arrow utilizes the negative of the opposite vector (in this case,
        // the negative of the right vector, as that is left)
        else if (Input.GetKey(KeyCode.LeftArrow))
            direction = -Vector2.right; 
        
    }
    //Method that uses the vectors to actually move the snake
    void moveSnake()
    {
       
        //Sets the 2D position in space of the head of the snake to a variable (transform.position gets the postion of the snake)
        Vector2 lastPosition = transform.position;

        //Moves the head of the snake in the direction assigned by the Update method
        transform.Translate(direction);
        //If the snake ate something, the code adds another unit to the snake (the tailPrefab is just a white pixel, which gets duplicated whenever
        //the snake eats)
        if (ateFood)
        {
            //Declares ate to false so the snake doesn't double eat
            ateFood = false;
   
            //Intializes the parameters of the new snake unit, using the tailPrefab (what to create) 
            //and the vector lastPosition (where to place it)  
            GameObject newUnit = (GameObject)Instantiate(tailPrefab,
                                                  lastPosition,
                                                  Quaternion.identity);
            
            //Inserts the snake unit into the snake as defined previously
            snakeBody.Insert(0, newUnit.transform);

        }
        //If the snake doesn't eat anything, the snake still needs to move
        //also checks if the snake body is equal to or greater than 1 (to see if the snake isn't just a head)
        else if (snakeBody.Count >= 1)
        {
            //Moves the last part of the snake to the location of the last location of the head
            snakeBody.Last().position = lastPosition;

            //Adds a new snake unit to the front and deletes the last unit from the snake, which is how the snake moves
            snakeBody.Insert(0, snakeBody.Last());
            snakeBody.RemoveAt(snakeBody.Count - 1);
        }
    }
    //If one of the objects collides with another, this method is ran
    void OnTriggerEnter2D(Collider2D coll)
    {
        // If the collision is due to a collision between the snake and regular food, increment score by 1
        if (coll.name.StartsWith("Food"))
        {
            //Eat and increment the score by 1
            eatAndIncrement(1);
            //Destroy the food
            Destroy(coll.gameObject);
        }
        //If the collision is due to a border collision, quit the game (game over)
        else if (coll.name.StartsWith("Border"))
        {
            Application.Quit();
        }
        //If the collision is due to the special red food, increment by 3
        else if (coll.name.StartsWith("Red"))
        {
            eatAndIncrement(3);
            Destroy(coll.gameObject);
        }
    }
    //Method that is called when the snake collides with a food object
    void eatAndIncrement(int increment)
    {
        //Increments score by set increment
        count = count + increment;
        //Updates score text to reflect the change
        scoreText.text = "Score: " + count.ToString();
        //Sets ateFood to true so that the moveSnake method works as intended
        ateFood = true;
    }
}