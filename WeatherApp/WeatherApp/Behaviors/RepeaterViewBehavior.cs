using System;
using System.Collections.Generic;
using System.Text;
using WeatherApp.Controls;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp.Behaviors
{
    class RepeaterViewBehavior : Behavior<RepeaterView>
    {
        private RepeaterView view;

        private void UpdateState()
        {
            MainThread.BeginInvokeOnMainThread(() => {
                var page = Application.Current.MainPage;

                if (page.Width > page.Height) {
                    view.VisualState = "Landscape";
                    return;
                }
                view.VisualState = "Portrait";
            });
        }

        protected override void OnAttachedTo(RepeaterView bindable)
        {
            this.view = bindable;
            base.OnAttachedTo(bindable);

            UpdateState();
            Application.Current.MainPage.SizeChanged += MainPage_SizeChanged;
        }

        protected override void OnDetachingFrom(RepeaterView bindable)
        {
            base.OnDetachingFrom(bindable);

            Application.Current.MainPage.SizeChanged -= MainPage_SizeChanged;
            this.view = null;
        }

        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
            UpdateState();
        }
    }
}
