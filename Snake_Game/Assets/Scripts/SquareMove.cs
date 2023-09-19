/*using UnityEngine;

public class Circle : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}*/

using UnityEngine;

public class SquareMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVertical = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVertical = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = 1;
        }

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}

