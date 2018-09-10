using UnityEngine;
using System.Collections;

public class UITweenRotation : UITweener
{
    [SerializeField]
    private bool isLocal = true;
    [SerializeField]
    private bool useQuaternion = true;
    [SerializeField]
    private Vector3 src;
    [SerializeField]
    private Vector3 dst;


    public override void ResetAtBeginning()
    {
        base.ResetAtBeginning();

        if (useQuaternion)
        {
            if (isLocal)
                RectTransform.localRotation = Quaternion.Euler(src);
            else
                RectTransform.rotation = Quaternion.Euler(src);
        }
        else
        {
            if (isLocal)
                RectTransform.localEulerAngles = src;
            else
                RectTransform.eulerAngles = src;
        }
    }

    public override void ResetAtTheEnd()
    {
        base.ResetAtTheEnd();

        if (useQuaternion)
        {
            if (isLocal)
                RectTransform.localRotation = Quaternion.Euler(dst);
            else
                RectTransform.rotation = Quaternion.Euler(dst);
        }
        else
        {
            if (isLocal)
                RectTransform.localEulerAngles = dst;
            else
                RectTransform.eulerAngles = dst;
        }
    }

    protected override void Animate()
    {
        base.Animate();

        if (useQuaternion)
        {
            if (isLocal)
                RectTransform.localRotation = Quaternion.Lerp(Quaternion.Euler(src), Quaternion.Euler(dst), curve.Evaluate(factor));
            else
                RectTransform.rotation = Quaternion.Lerp(Quaternion.Euler(src), Quaternion.Euler(dst), curve.Evaluate(factor));
        }
        else
        {
            if (isLocal)
                RectTransform.localEulerAngles = Vector3.Lerp(src, dst, curve.Evaluate(factor));
            else
                RectTransform.eulerAngles = Vector3.Lerp(src, dst, curve.Evaluate(factor));
        }
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