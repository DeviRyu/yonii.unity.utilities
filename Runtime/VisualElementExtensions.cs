using System;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace Yonii.Unity.Utilities
{
    public static class VisualElementExtensions
    {
        public static UniTask FadeAsync(this VisualElement visualElement, 
                                              int startOpacityValue = 0,
                                              int endOpacityValue = 1,
                                              int durationMs = 300)
        {
            var tsk = new UniTaskCompletionSource();

            visualElement.Fade(startOpacityValue,
                               endOpacityValue,
                               durationMs,
                               () => tsk.TrySetResult()
                               );

            return tsk.Task;
        }
        
        public static void Fade(this VisualElement visualElement,
                                int startOpacityValue = 0,
                                int endOpacityValue = 1,
                                int durationMs = 300,
                                Action onFinish = null
            )
        {
            visualElement.experimental.animation.Start(
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
    }

    public enum FadeType
    {
        In,
        Out
    }
}
