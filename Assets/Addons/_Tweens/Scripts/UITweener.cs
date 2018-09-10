using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class UITweener : MonoBehaviour 
{

    [System.Serializable]
    public class LerpVector
    {
        public float src;
        public float dst;
    }

    public enum Behavior
    {
        Once,
        Loop,
        PingPong,
        PingPongOnce,
    }

    public enum Disabling
    {
        None,
        AtBeginning,
        AtEnd,
        Both,
        DisableWhenNotTweening,
    }

    public enum Direction
    {
        Forward,
        Backward,
    }

    private enum PingPongStep
    {
        Forward = 0,
        Backward = 1,
        Stop = 2,
    }

    public enum ButtonDisabler
    {
        DoNothing,
        DisableAtBeginning,
        DisableAtEnd,
    }

    public delegate void TweenerEvent();

    public TweenerEvent onPlayForward;
    public TweenerEvent onPlayBackward;
    public TweenerEvent onStop;

    protected float factor = 0;

    public string tag;
    [SerializeField]
    protected bool resetOnStop = false;
    [SerializeField]
    protected float duration = 1;
    [SerializeField]
    protected AnimationCurve curve = new AnimationCurve();
    [SerializeField]
    protected Behavior behavior;
    [SerializeField]
    protected Direction direction = Direction.Forward;
    [SerializeField]
    protected Disabling gameObjectDisabling = Disabling.None;
    public ButtonDisabler buttonDisabler = ButtonDisabler.DoNothing;

    private MaskableGraphic graphic;
    private RectTransform rectTransform;
    private Transform target;
    private Button button;
    private PingPongStep pingPongStep = PingPongStep.Forward;

    private void OnEnable()
    {
        if (factor > 1)
        {
            factor = 1;
        }
        else if (factor < 0)
            factor = 0;
    }

    private void CheckDisabling()
    {
        switch(gameObjectDisabling)
        {
            case Disabling.Both:

                if (factor >= 1 || factor <= 0)
                    gameObject.SetActive(false);

                break;

            case Disabling.AtBeginning:
 
                if (factor <= 0)
                    gameObject.SetActive(false);

                break;

            case Disabling.AtEnd:

                if (factor >= 1)
                    gameObject.SetActive(false);

                break;

            case Disabling.DisableWhenNotTweening:

                if (!IsTweening)
                    gameObject.SetActive(false);

                break;
        }
    }

    public void PlayForward()
    {
        if(onPlayForward != null)
            onPlayForward();

        if (gameObjectDisabling != Disabling.None && !gameObject.activeSelf)
            gameObject.SetActive(true);

        pingPongStep = PingPongStep.Forward;
        direction = Direction.Forward;
        this.enabled = true;

        if (Button != null)
            Button.interactable = true;
    }

    public void PlayBackward()
    {
        if (onPlayBackward != null)
            onPlayBackward();

        if (gameObjectDisabling != Disabling.None && !gameObject.activeSelf)
            gameObject.SetActive(true);

        pingPongStep = PingPongStep.Forward;
        direction = Direction.Backward;
        this.enabled = true;

        if (Button != null)
            Button.interactable = true;
    }

    public void Toggle()
    {
        if (direction == Direction.Forward)
            PlayBackward();
        else if (direction == Direction.Backward)
            PlayForward();
    }

    public void Stop()
    {
        if (onStop != null)
            onStop();

        if (resetOnStop)
        {
            if (direction == Direction.Forward)
                ResetAtBeginning();
            else if (direction == Direction.Backward)
                ResetAtTheEnd();
        }

        if (Button != null)
        {
            if (buttonDisabler != ButtonDisabler.DoNothing)
            {
                if (buttonDisabler == ButtonDisabler.DisableAtBeginning && factor <= 0)
                    button.interactable = false;
                else if (buttonDisabler == ButtonDisabler.DisableAtEnd && factor >= 1)
                    button.interactable = false;

            }
        }

        CheckDisabling();

        this.enabled = false;
    }

    public virtual void ResetAtBeginning()
    {
        factor = 0;
        Stop();
    }

    public virtual void ResetAtTheEnd()
    {
        factor = 1;
        Stop();
    }

    protected void FactorUpdate()
    {
        if (direction == Direction.Forward)
            factor += Time.deltaTime * (1/duration);
        else
            factor -= Time.deltaTime * (1/duration);
    }

    protected virtual void Animate()
    {

    }

    protected void CheckEndTween()
    {
        if (factor < 0 || factor > 1)
        {
            switch (behavior)
            {
                case UITweener.Behavior.Once:   

                    Stop();

                    break;

                case UITweener.Behavior.Loop:

                    factor = (direction == Direction.Forward) ? 0 : 1;

                    break;

                case UITweener.Behavior.PingPong:

                    factor = (direction == Direction.Forward) ? 1 : 0;
                    direction = (direction == Direction.Forward) ? Direction.Backward : Direction.Forward;

                    break;

                case UITweener.Behavior.PingPongOnce:

                    if (pingPongStep == PingPongStep.Forward)
                    {
                        pingPongStep = PingPongStep.Backward;

                        factor = (direction == Direction.Forward) ? 1 : 0;
                        direction = (direction == Direction.Forward) ? Direction.Backward : Direction.Forward;
                    }
                    else if (pingPongStep == PingPongStep.Backward)
                    {
                        pingPongStep = PingPongStep.Stop;
                        Stop();
                    }

                    break;
            }
        }
    }
    
    /// <summary>
    /// Normalized progress of the tween (0-1)
    /// </summary>
    public float ProgressNormalized
    {
        get
        {
            return factor;
        }

        set
        {
            factor = value;
        }
    }

    /// <summary>
    /// The duration of the tween
    /// </summary>
    public float Duration
    {
        get
        {
            return duration;
        }

        set
        {
            duration = value;
        }
    }

    public AnimationCurve Curve
    {
        get
        {
            return curve;
        }
    }

    public float DurationLeft
    {
        get
        {
            if(direction == Direction.Forward)
                return Duration * (1-ProgressNormalized);
            else
                return Duration * ProgressNormalized;
        }
    }

    /// <summary>
    /// Progress of the tween (0-Duration)
    /// </summary>
    public float Progress
    {
        get
        {
            return factor * duration;
        }
    }

    /// <summary>
    /// Tweened UI element
    /// </summary>
    public RectTransform RectTransform
    {
        get
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();

            return rectTransform;
        }
    }

    public MaskableGraphic Graphic
    {
        get
        {
            if (graphic == null)
                graphic = GetComponent<MaskableGraphic>();

            return graphic;
        }
    }

    public Transform Target
    {
        get
        {
            return transform;
        }
    }

    /// <summary>
    /// Tweener behavior
    /// </summary>
    public Behavior BehaviorType
    {
        get
        {
            return behavior;
        }
    }

    /// <summary>
    /// Tweener Direction
    /// </summary>
    public Direction DirectionType
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    /// <summary>
    /// Is it tweening ?
    /// </summary>
    public bool IsTweening
    {
        get
        {
            return this.enabled;
        }
    }

    public Button Button
    {
        get
        {
            if (button == null)
                button = GetComponent<Button>();

            return button;
        }
    }
}
