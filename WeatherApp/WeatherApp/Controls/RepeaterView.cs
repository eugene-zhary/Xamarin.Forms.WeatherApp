using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp.Controls
{
    public class RepeaterView : FlexLayout
    {
        private string visualState;
        public string VisualState {
            get => visualState;
            set {
                visualState = value;

                foreach (var child in Children) {
                    VisualStateManager.GoToState(child, visualState);
                }
                VisualStateManager.GoToState(this, visualState);
            }
        }

        private DataTemplate itemsTemplate;
        public DataTemplate ItemsTemplate {
            get => itemsTemplate;
            set {
                itemsTemplate = value;
                MainThread.BeginInvokeOnMainThread(() => Generate());
            }
        }

        public static BindableProperty ItemsSourceProperty
            = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable<object>), typeof(RepeaterView),
                propertyChanged: (bindable, oldValue, newValue) => {

                    var repeater = (RepeaterView)bindable;
                    if (repeater.ItemsTemplate == null) {
                        return;
                    }
                    MainThread.BeginInvokeOnMainThread(() => repeater.Generate());
                });

        public IEnumerable<object> ItemsSource {
            get => GetValue(ItemsSourceProperty) as IEnumerable<object>;
            set => SetValue(ItemsSourceProperty, value);
        }

        public void Generate()
        {
            Children.Clear();
            if (ItemsSource == null) {
                return;
            }

            foreach (var item in ItemsSource) {

                if (itemsTemplate.CreateContent() is View view) {
                    view.BindingContext = item;
                    Children.Add(view);
                }
                else {
                    return;
                }
            }
        }

    }
}
