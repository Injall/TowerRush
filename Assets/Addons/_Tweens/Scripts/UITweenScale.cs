using UnityEngine;
using System.Collections;

public class TweenScale : UITweener
{
    [SerializeField]
    public Vector3 src = Vector3.one;
    [SerializeField]
    public Vector3 dst = Vector3.one;

    public override void ResetAtBeginning()
    {
        base.ResetAtBeginning();
        Target.localScale = src;
    }

    public override void ResetAtTheEnd()
    {
        base.ResetAtTheEnd();
        Target.localScale = dst;
    }

    protected override void Animate()
    {
        base.Animate();

        Target.localScale = Vector3.Lerp(src, dst, curve.Evaluate(factor));
 
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
