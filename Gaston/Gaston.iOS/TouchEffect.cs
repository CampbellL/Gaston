using System.Linq;
using Gaston.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("XamarinDocs")]
[assembly: ExportEffect(typeof(TouchEffect), "TouchEffect")]

namespace Gaston.iOS
{
    public class TouchEffect : PlatformEffect
    {
        private UIView _view;
        private TouchRecognizer _touchRecognizer;

        protected override void OnAttached()
        {
            // Get the iOS UIView corresponding to the Element that the effect is attached to
            _view = Control == null ? Container : Control;

            // Get access to the TouchEffect class in the .NET Standard library
            global::Gaston.TouchTracking.TouchEffect effect = (global::Gaston.TouchTracking.TouchEffect)Element.Effects.FirstOrDefault(e => e is global::Gaston.TouchTracking.TouchEffect);

            if (effect != null && _view != null)
            {
                // Create a TouchRecognizer for this UIView
                _touchRecognizer = new TouchRecognizer(Element, _view, effect); 
                _view.AddGestureRecognizer(_touchRecognizer);
            }
        }

        protected override void OnDetached()
        {
            if (_touchRecognizer != null)
            {
                // Clean up the TouchRecognizer object
                _touchRecognizer.Detach();

                // Remove the TouchRecognizer from the UIView
                _view.RemoveGestureRecognizer(_touchRecognizer);
            }
        }
    }
}