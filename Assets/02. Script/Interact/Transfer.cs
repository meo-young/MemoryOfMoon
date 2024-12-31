using UnityEngine;

public class Transfer : MonoBehaviour
{
    [SerializeField] Direction direction;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 pos = collision.transform.position;
            switch (direction)
            {
                case Direction.UP:
                    pos.y += 6f;
                    collision.transform.position = pos;
                    break;
                case Direction.DOWN:
                    pos.y -= 6f;
                    collision.transform.position = pos;
                    break;
                case Direction.LEFT:
                    pos.x -= 6f;
                    collision.transform.position = pos;
                    break;
                case Direction.RIGHT:
                    pos.x += 6f;
                    collision.transform.position = pos;
                    break;
            }
        }
    }

    [System.Serializable]
    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}
