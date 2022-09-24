using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class MultiImageButton : Button
    {
        private Graphic[] _graphics;

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            var targetColor =
                state switch
                {
                    SelectionState.Disabled => colors.disabledColor,
                    SelectionState.Highlighted => colors.highlightedColor,
                    SelectionState.Normal => colors.normalColor,
                    SelectionState.Pressed => colors.pressedColor,
                    SelectionState.Selected => colors.selectedColor,
                    _ => Color.white
                };

            if (_graphics == null)
            {
                _graphics = GetComponentsInChildren<Graphic>();
            }

            foreach (var graphic in _graphics)
            {
                graphic.CrossFadeColor(targetColor, instant ? 0f : colors.fadeDuration, true, true);
            }
        }
    }
}