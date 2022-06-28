using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace BeeRun
{
    public class Scale : MonoBehaviour
    {
        public Vector3 BeginScale = Vector3.zero;
        public Vector3 TargetScale = Vector3.one;
        public bool local = true;
        public float Duration = 0.5f;
        public float Delay = 0f;
        public iTween.EaseType easeType = iTween.EaseType.linear;
        public bool DestroyOnComplete = true;
        public UnityEvent OnScaleComplete;

        private iTween.EasingFunction easingFunction;
        private Coroutine scaleRoutine;

        private bool isStarted = false;

        private void Start()
        {
            isStarted = true;
            StartScale(Delay);
        }

        private void OnEnable()
        {
            if (isStarted) StartScale(Delay);
        }

        public void StartScale(float delay = 0f)
        {
            // remove previous Scale components
            Cancel();

            gameObject.transform.localScale = BeginScale;
            easingFunction = iTween.GetEasingFunction(easeType);
            scaleRoutine = StartCoroutine(ScaleRoutine(delay));
        }

        private IEnumerator ScaleRoutine(float delay)
        {
            //Debug.Log("ScaleRoutine delay=" + delay);
            if (delay > 0f) yield return new WaitForSeconds(delay);
            float progress = 0f;
            //Debug.Log("ScaleRoutine!");
            while (progress < 1f)
            {
                progress += Time.deltaTime / Duration;
                float value = easingFunction.Invoke(0f, 1f, progress);
                gameObject.transform.localScale = BeginScale + (TargetScale - BeginScale) * value;
                yield return null;
            }
            if (OnScaleComplete != null)
                OnScaleComplete.Invoke();
            Cancel();
        }

        static public void Cancel(GameObject target)
        {
            if (target != null)
            {
                Scale scale = target.GetComponent<Scale>();
                if (scale != null) scale.Cancel();
            }
        }

        public void Cancel()
        {
            if (scaleRoutine != null)
            {
                StopCoroutine(scaleRoutine);
                gameObject.transform.localScale = TargetScale;
                if (DestroyOnComplete) Destroy(this);
            }
        }

        static public Scale ScaleTo(GameObject target, Vector3 scale, float duration = 0.3f, iTween.EaseType easeType = iTween.EaseType.linear, bool isLocal = true, float delay = 0f, bool destroyOnComplete = true)
        {
            if (target != null)
            {
                // create new Scale components with parameters
                Scale scaler = target.AddComponent<Scale>();
                scaler.BeginScale = target.transform.localScale;
                scaler.TargetScale = scale;
                scaler.Duration = duration;
                scaler.Delay = delay;
                scaler.easeType = easeType;
                scaler.local = isLocal;
                scaler.DestroyOnComplete = destroyOnComplete;
                return scaler;
            }
            return null;
        }
    }
}