﻿using System.Collections.Generic;
using Android.Views;
using App.Droid;
using App;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Maps;
using Android.Gms.Maps.Model;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace App.Droid
{
    public class CustomMapRenderer : MapRenderer
    {
        List<Position> shapeCoordinates;
        bool isDrawn;

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                shapeCoordinates = formsMap.ShapeCoordinates;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName.Equals("VisibleRegion") && !isDrawn)
            {
                var polygonOptions = new PolygonOptions();
                polygonOptions.InvokeFillColor(0x66FF0000);
                polygonOptions.InvokeStrokeColor(0x660000FF);
                polygonOptions.InvokeStrokeWidth(30.0f);

                foreach (var position in shapeCoordinates)
                {
                    polygonOptions.Add(new LatLng(position.Latitude, position.Longitude));
                }
                NativeMap.AddPolygon(polygonOptions);
                isDrawn = true;
            }
        }
    }
}