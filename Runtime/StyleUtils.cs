using UnityEngine.UIElements;

namespace Yonii.Unity.Utilities
{
    public static class StyleUtils
    {
        public static void WithMargin(this IStyle style,
                                      float? allValue = null,
                                      float? top = null,
                                      float? bottom = null,
                                      float? right = null,
                                      float? left = null
            )
        {
            var sameValue = allValue.HasValue;
            if (sameValue || top.HasValue)
                style.marginTop = new StyleLength(allValue ?? top.Value);

            if (sameValue || bottom.HasValue)
                style.marginBottom = new StyleLength(allValue ?? bottom.Value);

            if (sameValue || right.HasValue)
                style.marginRight = new StyleLength(allValue ?? right.Value);

            if (sameValue || left.HasValue)
                style.marginLeft = new StyleLength(allValue ?? left.Value);
        }
    }
}