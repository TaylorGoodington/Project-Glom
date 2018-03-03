using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] backgrounds;
    public static Parallax parallax;

    private Collider2D levelCollider;
    private float levelSizeX;
    private float levelSizeY;
    private float cameraWidth;

    void Start()
    {
        parallax = GetComponent<Parallax>();
        cameraWidth = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().orthographicSize * 2 * 2;
        levelCollider = this.GetComponent<Collider2D>();
        levelSizeX = levelCollider.bounds.size.x;
        levelSizeY = levelCollider.bounds.size.y;
    }

    public void Scrolling(Vector2 cameraPosition)
    {
        for (var i = 0; i < backgrounds.Length; i++)
        {
            float backgroundSizeX = backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.x * 1;
            float backgroundSizeY = backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.y * 1;

            float maxBackgroundPositionY = levelSizeY - backgroundSizeY;
            float distanceToMoveX = levelSizeX - cameraWidth;
            float distanceToMoveY = levelSizeY - (cameraWidth / 2);

            float rateOfMovementX = (backgroundSizeX - cameraWidth) / distanceToMoveX;
            float rateOfMovementY = (backgroundSizeY - (cameraWidth / 2)) / distanceToMoveY;

            float backgroundTargetPositionX = Mathf.Round(((cameraPosition.x - cameraWidth / 2) + (rateOfMovementX * (cameraWidth / 2))) - (cameraPosition.x * (rateOfMovementX)));
            float backgroundTargetPositionY = Mathf.Round(((cameraPosition.y - cameraWidth / 4) + (rateOfMovementY * (cameraWidth / 4))) - (cameraPosition.y * (rateOfMovementY)));

            backgrounds[i].position = new Vector3(backgroundTargetPositionX, backgroundTargetPositionY, 1);

            //backgrounds[i].position = Vector3.Lerp(
            //    //From:
            //    backgrounds[i].position,
            //    //To:
            //    new Vector3(backgroundTargetPositionX, Mathf.Clamp(backgroundTargetPositionY, 0, maxBackgroundPositionY), backgrounds[i].position.z), 1);
        }
    }
}