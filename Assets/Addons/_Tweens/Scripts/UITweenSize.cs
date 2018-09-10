using UnityEngine;
using System.Collections;

public class UITweenSize : UITweener
{
    [Header("Act as an offset if stretched")]
    public Vector2 src;
    public Vector2 dst;

    public override void ResetAtBeginning()
    {
        base.ResetAtBeginning();
        RectTransform.sizeDelta = src;
    }

    public override void ResetAtTheEnd()
    {
        base.ResetAtTheEnd();
        RectTransform.sizeDelta = dst;
    }

    protected override void Animate()
    {
        base.Animate();

        RectTransform.sizeDelta = Vector3.Lerp(src, dst, curve.Evaluate(factor));
    }

    private void Update()
    {
        // Update factor over time
        FactorUpdate();

        //Do Tween
        Animate();

        // Check if factor is "< 0" or "> 1"
        CheckEndTween();
    }

    public Vector2 Src
    {
        get
        {
            return src;
        }

        set
        {
            src = value;
        }
    }

    public Vector2 Dst
    {
        get
        {
            return dst;
        }

        set
        {
            dst = value;
        }
    }
}
