/// Credit Alastair Aitchison
/// Sourced from - https://bitbucket.org/UnityUIExtensions/unity-ui-extensions/issues/123/uilinerenderer-issues-with-specifying

using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace UnityEngine.UI.Extensions
{
    [AddComponentMenu("UI/Extensions/UI Line Connector")]
    [RequireComponent(typeof(UILineRenderer))]
    [ExecuteInEditMode]
    public class UILineConnector : MonoBehaviour
    {
        [BoxGroup("Animation")] [SerializeField][Range(-1,10)]
        private int loop;
        [BoxGroup("Animation")] [SerializeField][Range(0,2)]
        private float duration;
        [BoxGroup("Animation")] [SerializeField]
        private Ease ease;
        [BoxGroup("Animation")] [SerializeField]
        private LoopType loopType;
        
        // The elements between which line segments should be drawn
        public List<RectTransform> transforms;
        private Vector2[] previousPositions;
        private RectTransform canvas;
        private RectTransform rt;
        private UILineRenderer lr;
        private Sequence _sequence;
        
        public void SetPayline(List<RectTransform> paylineSlots, Color color)
        {
            transforms = new List<RectTransform>(paylineSlots);
            lr.color = color;
            
            if(_sequence != null) _sequence.Kill();
            _sequence = DOTween.Sequence();
            _sequence.Append(lr.DOFade(0, duration)).SetEase(ease).SetLoops(loop, loopType).OnComplete(() =>
                {
                    lr.DOFade(0, duration);
                    transforms.Clear();
                    transforms = null;
                });
            _sequence.Play();
        }
        
        private void Awake()
        {
            canvas = GetComponentInParent<RectTransform>().GetParentCanvas().GetComponent<RectTransform>();
            rt = GetComponent<RectTransform>();
            lr = GetComponent<UILineRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (transforms == null || transforms.Count < 1)
            {
                return;
            }
            //Performance check to only redraw when the child transforms move
            if (previousPositions != null && previousPositions.Length == transforms.Count)
            {
                bool updateLine = false;
                for (int i = 0; i < transforms.Count; i++)
                {
                    if (!updateLine && previousPositions[i] != transforms[i].anchoredPosition)
                    {
                        updateLine = true;
                    }
                }
                if (!updateLine) return;
            }

            // Get the pivot points
            Vector2 thisPivot = rt.pivot;
            Vector2 canvasPivot = canvas.pivot;

            // Set up some arrays of coordinates in various reference systems
            Vector3[] worldSpaces = new Vector3[transforms.Count];
            Vector3[] canvasSpaces = new Vector3[transforms.Count];
            Vector2[] points = new Vector2[transforms.Count];

            // First, convert the pivot to worldspace
            for (int i = 0; i < transforms.Count; i++)
            {
                worldSpaces[i] = transforms[i].TransformPoint(thisPivot);
            }

            // Then, convert to canvas space
            for (int i = 0; i < transforms.Count; i++)
            {
                canvasSpaces[i] = canvas.InverseTransformPoint(worldSpaces[i]);
            }

            // Calculate delta from the canvas pivot point
            for (int i = 0; i < transforms.Count; i++)
            {
                points[i] = new Vector2(canvasSpaces[i].x, canvasSpaces[i].y);
            }

            // And assign the converted points to the line renderer
            lr.Points = points;
            lr.RelativeSize = false;
            lr.drivenExternally = true;

            previousPositions = new Vector2[transforms.Count];
            for (int i = 0; i < transforms.Count; i++)
            {
                previousPositions[i] = transforms[i].anchoredPosition;
            }
        }
    }
}