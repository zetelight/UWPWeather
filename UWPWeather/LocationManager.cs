﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace UWPWeather
{
    public class LocationManager
    {
        public async static Task<Geoposition> GetPosition()
        {
            // request access to the user's location
            var accessStatus = await Geolocator.RequestAccessAsync();

            //determine the status whether users give perimissions
            if (accessStatus !=GeolocationAccessStatus.Allowed)
            {
                throw new Exception();
            }

            var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };
            var position = await geolocator.GetGeopositionAsync();
            return position;

        }
    }
}
