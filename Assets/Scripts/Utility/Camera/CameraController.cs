using UnityEngine;

public class CameraController : MonoBehaviour
{
    public LayerMask levelBounds;
    [HideInInspector] public Controller2D target;
    public float verticalOffset;
    public float lookAheadDstX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    public Vector2 focusAreaSize;

    private float cameraHeight;
    private float cameraWidth;
    private FocusArea focusArea;
    private float maxYCameraClamp;
    private float minYCameraClamp;
    private float maxXCameraClamp;
    private float minXCameraClamp;

    private float targetAspectRatio;

    void Start()
    {
        targetAspectRatio = 160 / (float)144;
        cameraHeight = GetComponent<Camera>().orthographicSize;
        cameraWidth = cameraHeight * targetAspectRatio;
    }
    
    void LateUpdate()
    {
        if (IsPlayerInScene())
        {
            float currentLookAheadX = 0;
            float targetLookAheadX = 0;
            float lookAheadDirX = 0;
            float smoothLookVelocityX = 0;
            bool lookAheadStopped = true;

            focusArea.Update(target.boxCollider.bounds);

            Vector2 focusPosition = focusArea.center + Vector2.up * verticalOffset;

            if (focusArea.velocity.x != 0)
            {
                lookAheadDirX = Mathf.Sign(focusArea.velocity.x);
                if (Mathf.Sign(target.playerInput.x) == Mathf.Sign(focusArea.velocity.x) && target.playerInput.x != 0)
                {
                    lookAheadStopped = false;
                    targetLookAheadX = lookAheadDirX * lookAheadDstX;
                }
                else
                {
                    if (!lookAheadStopped)
                    {
                        lookAheadStopped = true;
                        targetLookAheadX = currentLookAheadX + (lookAheadDirX * lookAheadDstX - currentLookAheadX) / 4f;
                    }
                }
            }

            currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

            focusPosition += Vector2.right * currentLookAheadX;
            FindBounds();
            float cameraPositionX = Mathf.Clamp(focusPosition.x, (minXCameraClamp + cameraWidth), maxXCameraClamp - cameraWidth);
            float cameraPositionY = Mathf.Clamp(focusPosition.y, (minYCameraClamp + cameraHeight), (maxYCameraClamp - cameraHeight));
            transform.position = new Vector3(Mathf.Round(cameraPositionX), Mathf.Round(cameraPositionY), -10);
        }
    }

    private bool IsPlayerInScene ()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller2D>();
            focusArea = new FocusArea(target.boxCollider.bounds, focusAreaSize);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void FindBounds()
    {
        Vector2 topRight = new Vector2((transform.position.x - 1 + cameraWidth), (transform.position.y - 1 + cameraHeight));
        Vector2 topRightHitPoint = new Vector2();
        Vector2 topLeft = new Vector2((transform.position.x + 1 - cameraWidth), (transform.position.y - 1 + cameraHeight));
        Vector2 topLeftHitPoint = new Vector2();
        Vector2 bottomRight = new Vector2((transform.position.x - 1 + cameraWidth), (transform.position.y + 1 - cameraHeight));
        Vector2 bottomRightHitPoint = new Vector2();
        Vector2 bottomLeft = new Vector2((transform.position.x + 1 - cameraWidth), (transform.position.y + 1 - cameraHeight));
        Vector2 bottomLeftHitPoint = new Vector2();
        Vector2 rightTop = new Vector2((transform.position.x - 1 + cameraWidth), (transform.position.y - 1 + cameraHeight));
        Vector2 rightTopHitPoint = new Vector2();
        Vector2 leftTop = new Vector2((transform.position.x + 1 - cameraWidth), (transform.position.y - 1 + cameraHeight));
        Vector2 leftTopHitPoint = new Vector2();
        Vector2 rightBottom = new Vector2((transform.position.x - 1 + cameraWidth), (transform.position.y + 1 - cameraHeight));
        Vector2 rightBottomHitPoint = new Vector2();
        Vector2 leftBottom = new Vector2((transform.position.x + 1 - cameraWidth), (transform.position.y + 1 - cameraHeight));
        Vector2 leftBottomHitPoint = new Vector2();

        RaycastHit2D topRightHit = Physics2D.Raycast(topRight, Vector2.up, 5000, levelBounds);
        if (topRightHit)
        {
            topRightHitPoint = topRightHit.point;
        }
        RaycastHit2D topLeftHit = Physics2D.Raycast(topLeft, Vector2.up, 5000, levelBounds);
        if (topLeftHit)
        {
            topLeftHitPoint = topLeftHit.point;
        }
        RaycastHit2D bottomRightHit = Physics2D.Raycast(bottomRight, Vector2.down, 5000, levelBounds);
        if (bottomRightHit)
        {
            bottomRightHitPoint = bottomRightHit.point;
        }
        RaycastHit2D bottomLeftHit = Physics2D.Raycast(bottomLeft, Vector2.down, 5000, levelBounds);
        if (bottomLeftHit)
        {
            bottomLeftHitPoint = bottomLeftHit.point;
        }
        RaycastHit2D rightTopHit = Physics2D.Raycast(rightTop, Vector2.right, 5000, levelBounds);
        if (rightTopHit)
        {
            rightTopHitPoint = rightTopHit.point;
        }
        RaycastHit2D leftTopHit = Physics2D.Raycast(leftTop, Vector2.left, 5000, levelBounds);
        if (leftTopHit)
        {
            leftTopHitPoint = leftTopHit.point;
        }
        RaycastHit2D rightBottomHit = Physics2D.Raycast(rightBottom, Vector2.right, 5000, levelBounds);
        if (rightBottomHit)
        {
            rightBottomHitPoint = rightBottomHit.point;
        }
        RaycastHit2D leftBottomHit = Physics2D.Raycast(leftBottom, Vector2.left, 5000, levelBounds);
        if (leftBottomHit)
        {
            leftBottomHitPoint = leftBottomHit.point;
        }

        //MaxY Clamp
        if (topRightHitPoint.y >= topLeftHitPoint.y)
        {
            maxYCameraClamp = topLeftHitPoint.y;
        }
        else
        {
            maxYCameraClamp = topRightHitPoint.y;
        }
        
        //MinY Clamp
        if (bottomRightHitPoint.y >= bottomLeftHitPoint.y)
        {
            minYCameraClamp = bottomRightHitPoint.y;
        }
        else
        {
            minYCameraClamp = bottomLeftHitPoint.y;
        }

        //MaxX Clamp
        if (rightTopHitPoint.x >= rightBottomHitPoint.x)
        {
            maxXCameraClamp = rightBottomHitPoint.x;
        }
        else
        {
            maxXCameraClamp = rightTopHitPoint.x;
        }

        //MinX Clamp
        if (leftTopHitPoint.x >= leftBottomHitPoint.x)
        {
            minXCameraClamp = leftTopHitPoint.x;
        }
        else
        {
            minXCameraClamp = leftBottomHitPoint.x;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.center, focusAreaSize);
    }

    struct FocusArea
    {
        public Vector2 center;
        public Vector2 velocity;
        float left, right;
        float top, bottom;


        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds)
        {
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;
            center = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }
}