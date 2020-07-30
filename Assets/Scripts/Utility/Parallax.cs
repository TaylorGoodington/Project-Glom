using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform[] backgrounds;
    public static Parallax parallax;

    private Collider2D levelCollider;
    private float levelSizeX;
    private float levelSizeY;
    private float cameraWidth;
    private float cameraHeight;
    private float cameraMaxPositionX;
    private float cameraMaxPositionY;

    void Start()
    {
        parallax = GetComponent<Parallax>();
        cameraWidth = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().targetTexture.width;
        cameraHeight = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().targetTexture.height;
        levelCollider = this.GetComponent<Collider2D>();
        levelSizeX = levelCollider.bounds.size.x;
        cameraMaxPositionX = levelSizeX - cameraWidth;
        levelSizeY = levelCollider.bounds.size.y;
        cameraMaxPositionY = levelSizeY - cameraHeight;
    }

    public void Scrolling(Vector2 cameraPosition)
    {
        for (var i = 0; i < backgrounds.Length; i++)
        {
            float backgroundSizeX = backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.x * 1;
            float backgroundSizeY = backgrounds[i].GetComponent<SpriteRenderer>().bounds.size.y * 1;
            float maxPositionX = levelSizeX - backgroundSizeX;
            float maxPositionY = levelSizeY - backgroundSizeY;
            float distanceToMoveX = cameraMaxPositionX - maxPositionX;
            float distanceToMoveY = cameraMaxPositionY - maxPositionY;

            float rateOfMovementX;

            if (distanceToMoveX == 0)
            {
                rateOfMovementX = 0;
            }
            else
            {
                rateOfMovementX = distanceToMoveX / cameraMaxPositionX;
            }

            float rateOfMovementY;

            if (distanceToMoveY == 0)
            {
                rateOfMovementY = 0;
            }
            else
            {
                rateOfMovementY = distanceToMoveY / cameraMaxPositionY;
            }

            float adjustedCameraPositionX = cameraPosition.x - (cameraWidth / 2);
            float adjustedCameraPositionY = cameraPosition.y - (cameraHeight / 2);

            float backgroundTargetPositionX = Mathf.Round(adjustedCameraPositionX - adjustedCameraPositionX * rateOfMovementX);
            float backgroundTargetPositionY = Mathf.Round(adjustedCameraPositionY - adjustedCameraPositionY * rateOfMovementY);

            backgrounds[i].position = new Vector3(backgroundTargetPositionX, backgroundTargetPositionY, backgrounds[i].transform.position.z);
        }
    }
}