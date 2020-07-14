using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera camera;

    private bool check;
    private DistanceJoint2D joint;

    public GameObject pixel;
    SpriteRenderer renderrer;

    public LayerMask swingLayer;

    private List<GameObject> pixels = new List<GameObject>();

    private void Awake()
    {
        renderrer = pixel.GetComponent<SpriteRenderer>();
        joint = GetComponent<DistanceJoint2D>();
        camera = Camera.main;
    }
    void Start()
    {
        joint.enabled = false;
        check = true;
    }

    // Update is called once per frame
    void Update()
    {
        ClearPixels();
        GetMousePos();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (mousePos-transform.position), Mathf.Infinity, swingLayer);

        
        if(Input.GetMouseButtonDown(0) && check && hit)
        {
            joint.enabled = true;
            joint.connectedAnchor = hit.point;
            check = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            joint.enabled = false;
            check = true;
        }

        if (!check)
        {
            DrawLine(0, 0, Mathf.RoundToInt((joint.connectedAnchor.x - transform.position.x) / renderrer.bounds.size.x + 0.5f), Mathf.RoundToInt((joint.connectedAnchor.y - transform.position.y) / renderrer.bounds.size.y + 0.5f));
        }
    }

    private void GetMousePos()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void DrawLine(int x, int y, int x2, int y2)
    {
        int w = x2 - x;
        int h = y2 - y;
        int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;

        if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
        if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
        if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;

        int longest = Mathf.Abs(w);
        int shortest = Mathf.Abs(h);

        if (longest <= shortest)
        {
            longest = Mathf.Abs(h);
            shortest = Mathf.Abs(w);
            if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
            dx2 = 0;
        }

        int D = 2*shortest-longest;
        for (int i = 0; i <= longest; i++)
        {
            DrawPixel(x, y);
            D += 2*shortest;
            if (D >= longest)
            {
                D -= 2*longest;
                x += dx1;
                y += dy1;
            }
            else
            {
                x += dx2;
                y += dy2;
            }
        }
    }

    void DrawPixel(float x, float y)
    {
        pixels.Add(Instantiate(pixel, new Vector3(transform.position.x + x * renderrer.bounds.size.x, transform.position.y + y * renderrer.bounds.size.y, transform.position.z), Quaternion.identity));
    }

    void ClearPixels()
    {
        foreach(GameObject pixel in pixels)
        {
            Destroy(pixel);
        }
        pixels.Clear();
    }

}
