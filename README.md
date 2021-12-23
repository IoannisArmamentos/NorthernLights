# NorthernLights

A weather forecast application made in Visual Studio using Angular and Asp.net frameworks.

**Openstreetmap** <br>
You can select a location on the map with your mouse. Latitude & longitude are saved on click. <br>
Longitude=X and latitude=Y are appearing below the map like the pointer of your mouse. In real life Latitude always goes first. <br>
By default, the map is zoomed into my region. If there is a problem with the coordinates, then NaN NaN appears.

**OpenWeather** <br>
Using the One Call API from https://openweathermap.org/api/one-call-api , we can get all the essential weather data for a specific location in json format. The json response can be translated to typescript with a tool like http://json2ts.com/ . It looks like this: https://api.openweathermap.org/data/2.5/onecall?lat=39.3666&lon=22.9507&appid=5fb1dc554c8f1fda3541e7f155802c28&units=metric

<br>

**WeatherForecast.cs** <br>
Declaring classes and variables. Date comes in *double dt* Unix timestamp that needs proper converting. <br>
For example, *public class Daily* has to do with the incoming values for the next 10 days.

**WeatherForecastController.cs** <br>
Sends a GET request with api, langitude and longitude. <br>
Calls for translation for dt.

**Extensions.cs** <br>
Unix datetime starts at 01-01-1970 at 00:00 ΑΜ . <br>
*GetDateTimeFromUnixTimeStamp* converts a Unix timestamp to datetime. <br>
*GetTime* is used for sunrise and sunset times. <br>
*GetDate* is used for date.

**fetch-data.component.html** <br>
The frontend part of the weather forecast tables. <br>
For each new table you can use for example <tr *ngFor="let dForecast of weather.daily"> and change it to hForecast & weather.hourly .

**fetch-data.component.ts** <br>
The typescript part where the coordinates and the map are initialized and the declarations(weather interfaces) can be exported.

<br>

**Useful links that helped me:** <br>
https://www.youtube.com/watch?v=nGVoHEZojiQ <br>
https://github.com/ultrasonicsoft/ng-openstreetmap-demo <br>
https://github.com/CodeExplainedRepo/Weather-App-JavaScript <br>
https://github.com/atufkas/angular-openweather-app <br>
https://stackoverflow.com/questions/26880487/formatting-the-mouseposition-control-output-in-openlayers-3 <br>
https://openlayers.org/ <br>
