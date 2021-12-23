import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { of, Subject, Subscription } from 'rxjs';
import { debounceTime, switchMap, distinctUntilChanged } from 'rxjs/operators';
declare var ol: any;

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent implements OnInit, OnDestroy{
  public weather: RootObject;
  map: any;
  latitude: any;
  longitude: any;
  http: HttpClient;

  subscription: Subscription;
  onRequest: Subject<{ longitude, latitude }> = new Subject<{ longitude, latitude }>();

  constructor(http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.http = http;
  }
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
    }


  /* A callback method where X=Longitude Y=Latitude
   EPSG:4326 is a geographic, non-project coordinate system. It is the lat and long the GPS displays.
   EPSG:3857 is a projected coordinate system. This is the coordinate system used by Google Maps and pretty much all other web mapping applications.
   Often, data is stored in EPSG:4326 and displayed in EPSG:3857 Also, a mapping API may take lat, longs (i.e. EPSG: 4326 ) as an input
   but when those coordinates are displayed on a map, they will be shown a map based on a Web Mercator (i.e. EPSG:3857) projection.*/

  // Coordinates are taken by the mouse position on the map. My region is chosen as default.
  ngOnInit() {
    var mousePositionControl = new ol.control.MousePosition({
      coordinateFormat: ol.coordinate.createStringXY(4),
      projection: 'EPSG:4326',
      className: 'custom-mouse-position',
      target: document.getElementById('mouse-position'),
      undefinedHTML: '&nbsp;'
    });

    // cancels previous requests and debounces every 800 ms
    this.subscription = this.onRequest.pipe(
      debounceTime(800),
      distinctUntilChanged(),
      switchMap(longlat => this.http.get<RootObject>(this.baseUrl + `weatherforecast?latitude=${longlat.latitude}&longitude=${longlat.longitude}`)
      )).subscribe((result: RootObject) => {
        this.weather = result;
        console.error(result);
      }, error => console.error(error));

    this.map = new ol.Map({
      target: 'map',
      controls: ol.control.defaults({
        attributionOptions: {
          collapsible: false
        }
      }).extend([mousePositionControl]),
      layers: [
        new ol.layer.Tile({
          source: new ol.source.OSM()
        })
      ],
      view: new ol.View({
        center: ol.proj.fromLonLat([22.9507, 39.3666]),
        zoom: 8
      })
    });

    this.map.on('click', (args: any) => {
      const lonlat = ol.proj.transform(args.coordinate, 'EPSG:3857', 'EPSG:4326');

      this.longitude = lonlat[0];
      this.latitude = lonlat[1];
      this.weather = null;
      this.onRequest.next({ latitude: lonlat[1], longitude: lonlat[0] })
      this.setCenter();
    });
  }

  setCenter() {
    var view = this.map.getView();
    view.setCenter(ol.proj.fromLonLat([this.longitude, this.latitude]));
    view.setZoom(12);
  }
}
export interface Weather {
  id: number;
  main: string;
  description: string;
  icon: string;
}

export interface Current {
  dt: number;
  sunrise: number;
  sunset: number;
  temp: number;
  feels_like: number;
  pressure: number;
  humidity: number;
  dew_point: number;
  uvi: number;
  clouds: number;
  visibility: number;
  wind_speed: number;
  wind_deg: number;
  wind_gust: number;
  weather: Weather[];
}

export interface Minutely {
  dt: number;
  precipitation: number;
}

export interface Weather2 {
  id: number;
  main: string;
  description: string;
  icon: string;
}

export interface Hourly {
  dt: number;
  temp: number;
  feels_like: number;
  pressure: number;
  humidity: number;
  dew_point: number;
  uvi: number;
  clouds: number;
  visibility: number;
  wind_speed: number;
  wind_deg: number;
  wind_gust: number;
  weather: Weather2[];
  pop: number;
  rain: any;
}

export interface Temp {
  day: number;
  min: number;
  max: number;
  night: number;
  eve: number;
  morn: number;
}

export interface FeelsLike {
  day: number;
  night: number;
  eve: number;
  morn: number;
}

export interface Weather3 {
  id: number;
  main: string;
  description: string;
  icon: string;
}

export interface Daily {
  dt: number;
  sunrise: number;
  sunset: number;
  moonrise: number;
  moonset: number;
  moon_phase: number;
  temp: Temp;
  feels_like: FeelsLike;
  pressure: number;
  humidity: number;
  dew_point: number;
  wind_speed: number;
  wind_deg: number;
  wind_gust: number;
  weather: Weather3[];
  clouds: number;
  pop: number;
  rain: number;
  uvi: number;
}

export interface Alert {
  sender_name: string;
  event: string;
  start: number;
  end: number;
  description: string;
  tags: string[];
}

export interface RootObject {
  lat: number;
  lon: number;
  timezone: string;
  timezone_offset: number;
  current: Current;
  minutely: Minutely[];
  hourly: Hourly[];
  daily: Daily[];
  alerts: Alert[];
}
