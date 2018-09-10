using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UITweenColor : UITweener
{
    [SerializeField]
    public Color src = Color.white;
    [SerializeField]
    public Color dst = Color.white;

    public override void ResetAtBeginning()
    {
        base.ResetAtBeginning();
        Graphic.color = src;
    }

    public override void ResetAtTheEnd()
    {
        base.ResetAtTheEnd();
        Graphic.color = dst;
    }

    protected override void Animate()
    {
        base.Animate();

        Graphic.color = Color.Lerp(src, dst, curve.Evaluate(factor));
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
