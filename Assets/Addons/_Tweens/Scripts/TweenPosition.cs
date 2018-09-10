using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TweenPosition : UITweener 
{
    public bool isLocal = true;
    public Vector3 src;
    public Vector3 dst;

    public override void ResetAtBeginning()
    {
        base.ResetAtBeginning();
        if (isLocal)
            Target.localPosition = src;
        else
            Target.position = src;
    }

    public override void ResetAtTheEnd()
    {
        base.ResetAtTheEnd();
        if (isLocal)
            Target.localPosition = dst;
        else
            Target.position = dst;
    }

    protected override void Animate()
    {
        base.Animate();

        if (isLocal)
            Target.localPosition = Vector3.Lerp(src, dst, curve.Evaluate(factor));
        else
            Target.position = Vector3.Lerp(src, dst, curve.Evaluate(factor));
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
}