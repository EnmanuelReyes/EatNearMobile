using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using EatNearMobile;
using EatNearMobile.Droid;
using Android.Gms.Maps;
using Xamarin.Forms.Maps.Android;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Maps;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace EatNearMobile.Droid
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter, IOnMapReadyCallback
    {
        GoogleMap map;
        List<CustomPin> customPins;
        bool isDrawn;
        CustomMap formsMap;


        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                map.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                formsMap = (CustomMap)e.NewElement;
                customPins = formsMap.CustomPins;
                Control.GetMapAsync(this);
            }
        }

        public Android.Views.View GetInfoContents(Marker marker)
        {
            var inflater = Android.App.Application.Context.GetSystemService(Context.LayoutInflaterService) as Android.Views.LayoutInflater;
            if (inflater != null)
            {
                Android.Views.View view=null;

                var customPin = GetCustomPin(marker);
                if (customPin == null)
                {
                    throw new Exception("Custom pin not found");
                }

                    view = inflater.Inflate(Resource.Layout.MapInfoWindow, null);

                var infoTitle = view.FindViewById<TextView>(Resource.Id.InfoWindowTitle);
                var infoSubtitle = view.FindViewById<TextView>(Resource.Id.InfoWindowSubtitle);

                if (infoTitle != null)
                {
                    infoTitle.Text = marker.Title;
                }
                if (infoSubtitle != null)
                {
                    infoSubtitle.Text = marker.Snippet;
                }

                return view;
            }
            return null;
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            return null;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;
            map.InfoWindowClick += OnInfoWindowClick;
            map.SetInfoWindowAdapter(this);
        }

        public void OnInfoWindowClick(object sender, GoogleMap.InfoWindowClickEventArgs e)
        {
            var customPin = GetCustomPin(e.Marker);
            if (customPin == null)
            {
                throw new Exception("Custom pin not found");
            }
			var page = new EatNearMobile.RestaurantPage(customPin.Restaurant) as RestaurantPage;
				Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(page);
              
        }

        private CustomPin GetCustomPin(Marker annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in formsMap.CustomPins)
            {
                if (pin.Pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName.Equals("VisibleRegion") && !isDrawn && map != null)
            {
                map.UiSettings.ZoomControlsEnabled = false;
                map.Clear();

                foreach (var pin in formsMap.CustomPins)
                {
                    var marker = new MarkerOptions();
                    marker.SetPosition(new LatLng(pin.Pin.Position.Latitude, pin.Pin.Position.Longitude));
                    marker.SetTitle(pin.Pin.Label);
                    marker.SetSnippet(pin.Pin.Address);
                    //marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.pin));

                    map.AddMarker(marker);
                }
                isDrawn = true;
            }
        }
        
    }
}