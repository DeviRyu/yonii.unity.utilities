using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace Yonii.Unity.Utilities
{
    public static class VisualElementExtensions
    {
        public static UniTask FadeAsync(this VisualElement visualElement, 
                                              int startOpacityValue = 0,
                                              int endOpacityValue = 1,
                                              int durationMs = 300,
                                              CancellationToken cancellationToken = default)
        {
            var tsk = new UniTaskCompletionSource();
            
            var completed = false;

            var animation = visualElement.Fade(startOpacityValue,
                                               endOpacityValue,
                                               durationMs,
                                               OnFinish
                );
            
            if (cancellationToken.CanBeCanceled)
            {
                cancellationToken.Register(() =>
                {
                    CompleteIfNotDone(() =>
                    {
                        animation.Stop();
                        // visualElement.style.opacity = visualElement.resolvedStyle.opacity;
                        tsk.TrySetCanceled();
                    });
                });
            }

            return tsk.Task;

            void CompleteIfNotDone(Action operation)
            {
                if (completed)
                {
                    return;
                }

                completed = true;
                operation();
            }

            void OnFinish() => CompleteIfNotDone(() => tsk.TrySetResult());
        }

        public static IValueAnimation Fade(this VisualElement visualElement,
                                           int startOpacityValue = 0,
                                           int endOpacityValue = 1,
                                           int durationMs = 300,
                                           Action onFinish = null
            )
        {
            return visualElement.experimental.animation.Start(
                    new StyleValues { opacity = startOpacityValue },
                    new StyleValues { opacity = endOpacityValue },
                    durationMs: durationMs
                )
                .Ease(Easing.OutQuad)
                .OnCompleted(onFinish);
        }

        public static void Fade(this VisualElement element,
                                FadeType type,
                                Action callback = null
            )
        {
            var startOpacityValue = type == FadeType.In ? 0 : 1;
            var stopOpacityValue = type == FadeType.In ? 1 : 0;

            element.Fade(startOpacityValue, stopOpacityValue, onFinish: callback);
        }

        public static void Hide(this VisualElement element)
        {
            element.style.display = DisplayStyle.None;
        }

        public static void Show(this VisualElement element)
        {
            element.style.display = DisplayStyle.Flex;
        }
        
        public static async UniTask ScaleAsync(this VisualElement element,
                                                float from = 1f,
                                                float to = 0f,
                                                float duration = .2f)
        {
            var elapsed = 0f;
            while (elapsed < duration)
            {
                var t = Mathf.Clamp01(elapsed / duration);
                var scale = Mathf.Lerp(from, to, Mathf.SmoothStep(0f, 1f, t));
                element.transform.scale = new Vector3(scale, scale, 1f);
                await UniTask.Yield();
                elapsed += Time.deltaTime;
            }
            
            element.transform.scale = new Vector3(to, to, 1f);
        }
    }

    public enum FadeType
    {
        In,
        Out
    }
}
