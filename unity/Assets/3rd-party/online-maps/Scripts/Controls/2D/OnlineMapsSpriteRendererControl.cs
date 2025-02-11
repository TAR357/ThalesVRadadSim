﻿/*     INFINITY CODE 2013-2019      */
/*   http://www.infinity-code.com   */

using UnityEngine;

/// <summary>
/// Class control the map for the SpriteRenderer.
/// </summary>
[AddComponentMenu("Infinity Code/Online Maps/Controls/SpriteRenderer")]
[RequireComponent(typeof(SpriteRenderer))]
public class OnlineMapsSpriteRendererControl:OnlineMapsControlBase2D
{
    private Collider _cl;
    private Collider2D _cl2D;

    private SpriteRenderer spriteRenderer;

    /// <summary>
    /// Singleton instance of OnlineMapsSpriteRendererControl control.
    /// </summary>
    public new static OnlineMapsSpriteRendererControl instance
    {
        get { return _instance as OnlineMapsSpriteRendererControl; }
    }

    /// <summary>
    /// Collider
    /// </summary>
    public Collider cl
    {
        get
        {
            if (_cl == null) _cl = GetComponent<Collider>();
            return _cl;
        }
    }

    /// <summary>
    /// Collider2D
    /// </summary>
    public Collider2D cl2D
    {
        get
        {
            if (_cl2D == null) _cl2D = GetComponent<Collider2D>();
            return _cl2D;
        }
    }

    private bool GetCoords2D(Vector2 position, out double lng, out double lat)
    {
        lng = lat = 0;
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(position), Mathf.Infinity);
        if (hit.collider == null || hit.collider.gameObject != gameObject) return false;
        if (cl2D == null) return false;

        HitPointToCoords(hit.point, out lng, out lat);
        return true;
    }

    private bool GetCoords3D(Vector3 position, out double lng, out double lat)
    {
        lng = lat = 0;
        RaycastHit hit;
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(position), out hit)) return false;

        if (hit.collider.gameObject != gameObject) return false;

        HitPointToCoords(hit.point, out lng, out lat);
        return true;
    }

    public override bool GetCoords(Vector2 position, out double lng, out double lat)
    {
        if (GetCoords2D(position, out lng, out lat)) return true;
        return GetCoords3D(position, out lng, out lat);
    }

    public override Rect GetRect()
    {
        Vector2 p1 = Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min);
        Vector2 p2 = Camera.main.WorldToScreenPoint(spriteRenderer.bounds.max);
        Vector2 s = p2 - p1;
        return new Rect(p1.x, p1.y, s.x, s.y);
    }

    public override Vector2 GetScreenPosition(double lng, double lat)
    {
        double tlx, tly, brx, bry;
        map.GetTileCorners(out tlx, out tly, out brx, out bry);
        int max = 1 << map.zoom;
        if (tlx > brx) brx += max;

        double px, py;
        map.projection.CoordinatesToTile(lng, lat, map.zoom, out px, out py);

        if (px + max / 2 < tlx) px += max;

        double rx = (px - tlx) / (brx - tlx) - 0.5;
        double ry = 0.5 - (py - tly) / (bry - tly);

        Bounds bounds = spriteRenderer.sprite.bounds;
        Vector3 size = bounds.size;

        rx *= size.x;
        ry *= size.y;

        Vector3 worldPoint = transform.localToWorldMatrix.MultiplyPoint(new Vector3((float)rx, (float)ry, bounds.center.z));
        return Camera.main.WorldToScreenPoint(worldPoint);
    }

    private void HitPointToCoords(Vector3 point, out double lng, out double lat)
    {
        if (spriteRenderer.sprite == null)
        {
            lng = lat = 0;
            return;
        }

        double tlx, tly, brx, bry;
        map.GetTileCorners(out tlx, out tly, out brx, out bry);

        if (tlx > brx) brx += 1 << map.zoom;

        Bounds bounds = spriteRenderer.sprite.bounds;
        Vector3 size = bounds.size;
        Vector3 localPoint = transform.worldToLocalMatrix.MultiplyPoint(point);
        double px = localPoint.x / size.x + 0.5;
        double py = localPoint.y / size.y + 0.5;
        px = (brx - tlx) * px + tlx;
        py = bry - (bry - tly) * py;

        map.projection.TileToCoordinates(px, py, map.zoom, out lng, out lat);
    }

    protected override void OnEnableLate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("Can not find SpriteRenderer.");
            OnlineMapsUtils.Destroy(this);
        }
    }

    public override void SetTexture(Texture2D texture)
    {
        base.SetTexture(texture);

        if (spriteRenderer.sprite == null)
        {
            if (texture != null)
            {
                spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                GetComponent<BoxCollider>().size = new Vector3(texture.width / 100f, texture.height / 100f, 0.2f);
            }
        }

        MaterialPropertyBlock props = new MaterialPropertyBlock();
        props.SetTexture("_MainTex", texture);
        spriteRenderer.SetPropertyBlock(props);
    }
}