using UnityEngine;
using UnityEngine.UI;
using System;

public class Projectile : MonoBehaviour
{
    private Image fill;
    private TileStyle style;
    private CentralPivot parent;

    private string targetPos;
    private float xTrans;
    private float yTrans;
    private float speedMultiplier;
    private bool allowInput = true;

    private readonly int centerScreenX = Screen.width / 2;
    private readonly int centerScreenY = Screen.height / 2;

    public void Setup(float xTrans, float yTrans, float speedMultiplier, TileStyle style, CentralPivot parent)
    {
        this.xTrans = xTrans;
        this.yTrans = yTrans;
        this.speedMultiplier = speedMultiplier;
        Style = style;
        this.parent = parent;
    }

    public string TargetPos { get => targetPos; set => targetPos = value; }
    public float XTrans { get => xTrans; set => xTrans = value; }
    public float YTrans { get => yTrans; set => yTrans = value; }
    public float SpeedMultiplier { get => speedMultiplier; set => speedMultiplier = value; }

    public TileStyle Style
    {
        get => style;
        set
        {
            style = value;
            fill.color = value.FillColor;
        }
    }

    private void Awake()
    {
        fill = GetComponent<Image>();
    }

    private void Update()
    {
        Vector3 movementVec = new Vector3(xTrans * SpeedMultiplier, yTrans * SpeedMultiplier, 0f) * Time.deltaTime;

        if (transform.position.x + movementVec.x > centerScreenX && allowInput == true)
        {
            transform.position = new Vector3(centerScreenX, transform.position.y, 0f);
            allowInput = false;
            targetPos = parent.TargetPos;
            xTrans = parent.XTrans;
            yTrans = parent.YTrans;
        }
        else
        {
            transform.position += movementVec;
        }

        if (transform.position.x < 0 || transform.position.x > Screen.width
            || transform.position.y < 0 || transform.position.y > Screen.height)
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        GameObject[] edgeTiles = GameObject.FindGameObjectsWithTag("EdgeTile");
        GameObject closestEdgeTile = edgeTiles[0];
        float minDist = float.PositiveInfinity;
        bool shouldHaveHit = false;

        foreach (GameObject obj in edgeTiles)
        {
            float dist = (transform.position - obj.transform.position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                closestEdgeTile = obj;
            }

            EdgeTile edgeTileTemp = obj.GetComponent<EdgeTile>();
            if (edgeTileTemp.Style.FillColor == style.FillColor)
            {
                shouldHaveHit = true;
            }
        }

        EdgeTile edgeTile = closestEdgeTile.GetComponent<EdgeTile>();
        bool didHit = minDist < 10f;
        bool stylesMatched = edgeTile.Style.FillColor == style.FillColor;

        parent.ProjDestroy(didHit, stylesMatched, shouldHaveHit);

        Destroy(gameObject);
    }
}