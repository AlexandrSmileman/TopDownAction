using UnityEngine;

public class HomeBaseDirection : MonoBehaviour
{
    [SerializeField]
    private Transform homeBase;
    [SerializeField]
    private RectTransform directionImage;

    private Camera _camera;
    private float offset;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        offset = directionImage.rect.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 basePos = _camera.WorldToScreenPoint(homeBase.position);
        int borderWidth = 5;
        if (basePos.x < -borderWidth
            || basePos.x > Screen.width + borderWidth
            || basePos.y < -borderWidth
            || basePos.y > Screen.height + borderWidth)
        {            
            Vector3 pos = new Vector3(
                Mathf.Clamp(basePos.x, offset, Screen.width - offset),
                Mathf.Clamp(basePos.y, offset * 2f, Screen.height),
                0);
            Vector2 direction = new Vector2(basePos.x, basePos.y) - directionImage.anchoredPosition;
            directionImage.anchoredPosition = pos;
            //float angle = Vector2.Angle(Vector2.up, direction);
            //angle = basePos.x > directionImage.anchoredPosition.x ? -angle : angle;
            //directionImage.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            directionImage.gameObject.SetActive(true);
        }
        else
            directionImage.gameObject.SetActive(false);
    }
}
