using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnchorTool 
{

    public enum AnchorHorizontal
    {
        Left,
        Center,
        Right,
        Stretched,
        Custom,
    }

    public enum AnchorVertical
    {
        Top,
        Center,
        Bottom,
        Stretched,
        Custom,
    }

    #region Stretch Tools

    public static bool IsStretchedHorizontally(RectTransform rectTransform)
    {
        return (rectTransform.anchorMin.x == 0 && rectTransform.anchorMax.x == 1);
    }

    public static bool IsStretchedVertically(RectTransform rectTransform)
    {
        return (rectTransform.anchorMin.y == 0 && rectTransform.anchorMax.y == 1);
    }

    public static void Set(RectTransform rectTransform, float left, float right, float top, float bottom)
    {
        Vector2 offsetMin = rectTransform.offsetMin;
        Vector2 offsetMax = rectTransform.offsetMax;
        Vector2 anchoredPosition = rectTransform.anchoredPosition;

        if (AnchorTool.IsStretchedHorizontally(rectTransform))
        {
            offsetMin.x = left;
            offsetMax.x = -right;
            anchoredPosition.x = offsetMin.x * 0.5f + offsetMax.x * 0.5f;
        }
        else
            Debug.LogWarning(rectTransform.gameObject + " is not horizontally stretched");

        if (AnchorTool.IsStretchedVertically(rectTransform))
        {
            offsetMin.y = bottom;
            offsetMax.y = -top;
            anchoredPosition.y = offsetMin.y * 0.5f + offsetMax.y * 0.5f;
        }
        else
            Debug.LogWarning(rectTransform.gameObject + " is not vertically stretched");

        rectTransform.offsetMin = offsetMin;
        rectTransform.offsetMax = offsetMax;
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public static void SetHorizontal(RectTransform rectTransform, float left, float right)
    {
        Vector2 offsetMin = rectTransform.offsetMin;
        Vector2 offsetMax = rectTransform.offsetMax;
        Vector2 anchoredPosition = rectTransform.anchoredPosition;

        if (AnchorTool.IsStretchedHorizontally(rectTransform))
        {
            offsetMin.x = left;
            offsetMax.x = -right;
            anchoredPosition.x = offsetMin.x * 0.5f + offsetMax.x * 0.5f;
        }
        else
            Debug.LogWarning(rectTransform.gameObject + " is not horizontally stretched");


        rectTransform.offsetMin = offsetMin;
        rectTransform.offsetMax = offsetMax;
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public static void SetVertical(RectTransform rectTransform, float top, float bottom)
    {
        Vector2 offsetMin = rectTransform.offsetMin;
        Vector2 offsetMax = rectTransform.offsetMax;
        Vector2 anchoredPosition = rectTransform.anchoredPosition;

        if (AnchorTool.IsStretchedVertically(rectTransform))
        {
            offsetMin.y = bottom;
            offsetMax.y = -top;
            anchoredPosition.y = offsetMin.y * 0.5f + offsetMax.y * 0.5f;
        }
        else
            Debug.LogWarning(rectTransform.gameObject + " is not vertically stretched");

        rectTransform.offsetMin = offsetMin;
        rectTransform.offsetMax = offsetMax;
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public static void SetLeft(RectTransform rectTransform, float left)
    {
        Vector2 offsetMin = rectTransform.offsetMin;
        Vector2 offsetMax = rectTransform.offsetMax;
        Vector2 anchoredPosition = rectTransform.anchoredPosition;

        if (AnchorTool.IsStretchedHorizontally(rectTransform))
        {
            offsetMin.x = left;
            anchoredPosition.x = offsetMin.x * 0.5f + offsetMax.x * 0.5f;
        }
        else
            Debug.LogWarning(rectTransform.gameObject + " is not horizontally stretched");

        rectTransform.offsetMin = offsetMin;
        rectTransform.offsetMax = offsetMax;
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public static void SetRight(RectTransform rectTransform, float right)
    {
        Vector2 offsetMin = rectTransform.offsetMin;
        Vector2 offsetMax = rectTransform.offsetMax;
        Vector2 anchoredPosition = rectTransform.anchoredPosition;

        if (AnchorTool.IsStretchedHorizontally(rectTransform))
        {
            offsetMax.x = -right;
            anchoredPosition.x = offsetMin.x * 0.5f + offsetMax.x * 0.5f;
        }
        else
            Debug.LogWarning(rectTransform.gameObject + " is not horizontally stretched");


        rectTransform.offsetMin = offsetMin;
        rectTransform.offsetMax = offsetMax;
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public static void SetTop(RectTransform rectTransform, float top)
    {
        Vector2 offsetMin = rectTransform.offsetMin;
        Vector2 offsetMax = rectTransform.offsetMax;
        Vector2 anchoredPosition = rectTransform.anchoredPosition;

        if (AnchorTool.IsStretchedVertically(rectTransform))
        {
            offsetMax.y = -top;
            anchoredPosition.y = offsetMin.y * 0.5f + offsetMax.y * 0.5f;
        }
        else
            Debug.LogWarning(rectTransform.gameObject + " is not vertically stretched");

        rectTransform.offsetMin = offsetMin;
        rectTransform.offsetMax = offsetMax;
        rectTransform.anchoredPosition = anchoredPosition;
    }

    public static void SetBottom(RectTransform rectTransform, float bottom)
    {
        Vector2 offsetMin = rectTransform.offsetMin;
        Vector2 offsetMax = rectTransform.offsetMax;
        Vector2 anchoredPosition = rectTransform.anchoredPosition;

        if (AnchorTool.IsStretchedVertically(rectTransform))
        {
            offsetMin.y = bottom;
            anchoredPosition.y = offsetMin.y * 0.5f + offsetMax.y * 0.5f;
        }
        else
            Debug.LogWarning(rectTransform.gameObject + " is not vertically stretched");

        rectTransform.offsetMin = offsetMin;
        rectTransform.offsetMax = offsetMax;
        rectTransform.anchoredPosition = anchoredPosition;
    }

#endregion

    #region Anchor Tools

    public static AnchorHorizontal GetAnchorHorizontal(RectTransform rectTransform)
    {
        float minX = rectTransform.anchorMin.x;
        float maxX = rectTransform.anchorMax.x;

        if (minX == 0 && maxX == 0)
            return AnchorHorizontal.Left;
        else if (minX == 0.5f && maxX == 0.5f)
            return AnchorHorizontal.Center;
        else if (minX == 1 && maxX == 1)
            return AnchorHorizontal.Right;
        else if (minX == 0 && maxX == 1)
            return AnchorHorizontal.Stretched;

        return AnchorHorizontal.Custom;
    }

    public static AnchorVertical GetAnchorVertical(RectTransform rectTransform)
    {
        float minY = rectTransform.anchorMin.y;
        float maxY = rectTransform.anchorMax.y;

        if (minY == 0 && maxY == 0)
            return AnchorVertical.Bottom;
        else if (minY == 0.5f && maxY == 0.5f)
            return AnchorVertical.Center;
        else if (minY == 1 && maxY == 1)
            return AnchorVertical.Top;
        else if (minY == 0 && maxY == 1)
            return AnchorVertical.Stretched;

        return AnchorVertical.Custom;
    }

    public static void SetAnchor(RectTransform rectTransform, AnchorHorizontal anchorH, AnchorVertical anchorV, float customMinX = 0, float customMinY = 0, float customMaxX = 0, float customMaxY = 0)
    {
        float minX = 0;
        float minY = 0;
        float maxX = 0;
        float maxY = 0;

        switch (anchorH)
        {
            case AnchorHorizontal.Left:

                minX = 0;
                maxX = 0;

                break;

            case AnchorHorizontal.Center:

                minX = 0.5f;
                maxX = 0.5f;

                break;

            case AnchorHorizontal.Right:

                minX = 1;
                maxX = 1;

                break;


            case AnchorHorizontal.Stretched:

                minX = 0;
                maxX = 1;

                break;

            case AnchorHorizontal.Custom:
                
                minX = customMinX;
                maxX = customMaxX;

                break;
        }


        switch (anchorV)
        {
            case AnchorVertical.Bottom:
                
                minY = 0;
                maxY = 0;

                break;

            case AnchorVertical.Center:

                minY = 0.5f;
                maxY = 0.5f;

                break;

            case AnchorVertical.Top:

                minY = 1;
                maxY = 1;

                break;


            case AnchorVertical.Stretched:

                minY = 0;
                maxY = 1;

                break;


            case AnchorVertical.Custom:

                minY = customMinY;
                maxY = customMaxY;

                break;
        }

        rectTransform.anchorMin = new Vector2(minX, minY);
        rectTransform.anchorMax = new Vector2(maxX, maxY);
    }

    #endregion
}
