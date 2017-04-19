using System;
using System.Linq;

using UIKit;

using info.yunnxx.Hello;
using AVFoundation;
using MapKit;

namespace Hello
{
    public partial class DetailViewController : UIViewController, IMKMapViewDelegate
    {
        public object DetailItem { get; set; }

        protected DetailViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetDetailItem(object newDetailItem)
        {
            if (DetailItem != newDetailItem)
            {
                DetailItem = newDetailItem;

                // Update the view
                ConfigureView();
            }
        }

        void ConfigureView()
        {
            // Update the user interface for the detail item
            if (!IsViewLoaded || DetailItem == null) return;

            //detailDescriptionLabel.Text = DetailItem.ToString();

            if (DetailItem is IMKAnnotation annotation)
            {
                Map.AddAnnotations(annotation);
                //Map.reg
            }

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Map.Delegate = Map.Delegate ?? this;
            // Perform any additional setup after loading the view, typically from a nib.
            ConfigureView();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            if (DetailItem is MasterItem item)
            {
                Speak(item.Hello, item.Language);
                Map.SelectAnnotation(item, true);
            }
        }

        float volume = 1.0f;//⑩
        float pitch = 1.0f;//⑪
        void Speak(string text, string language)//⑫
        {//
            if (!string.IsNullOrWhiteSpace(text))
            {//⑬
                var speechSynthesizer = new AVSpeechSynthesizer();//⑭
                var speechUtterance = new AVSpeechUtterance(text)
                {//⑮
                    Rate = AVSpeechUtterance.MaximumSpeechRate / 4,//⑯
                    Voice = AVSpeechSynthesisVoice.FromLanguage(language),//⑰
                    Volume = volume,//⑱
                    PitchMultiplier = pitch//⑲
                };//
                speechSynthesizer.SpeakUtterance(speechUtterance);//⑳
            }//
        }
    }
}