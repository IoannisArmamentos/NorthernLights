# NorthernLights
test

A weather forecast application made in Visual Studio using Angular 8 and ASP.NET Core 5 frameworks. It may take up to 10 seconds to load the weather data.
Production is hosted on Azure here: https://northernlights.azurewebsites.net/ <br>
Currently there is only a master branch. Every new commit, passes through the CI/CD.<br>

**Openstreetmap** <br>
You can select a location on the map with your mouse. Latitude & longitude are saved on click. <br>
Longitude=X and latitude=Y are appearing below the map like the pointer of your mouse. In real life Latitude always goes first. <br>
By default, the map is zoomed into my region.

**OpenWeather** <br>
Using the One Call API from https://openweathermap.org/api/one-call-api , we can get all the essential weather data for a specific location in json format. The json response can be translated to typescript with a tool like http://json2ts.com/ . It looks like this: https://api.openweathermap.org/data/2.5/onecall?lat=39.3666&lon=22.9507&appid=5fb1dc554c8f1fda3541e7f155802c28&units=metric

<br>

**OpenWeatherOptions.cs** <br>
Apikey and BaesUrl that are used in logger. <br> 

**WeatherForecast.cs** <br>
Declaring classes and variables. Date comes in *double dt* Unix timestamp that needs proper converting. <br>
For example, *public class Daily* has to do with the incoming values for the next 10 days. <br>

**WeatherForecastController.cs** <br>
GetAsync sends a GET request with apikey, langitude and longitude, etc. <br>
Initially I had a HttpClient returning a big result but for performance costs I didn't use it. <br>
Also I have commented a *foreach* that runs the list of *daily* and does the proper backend conversions for the Timestamps(dt, sunrise,sunset).


**Extensions.cs** <br>
This backend method is currently not used.
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

**CI/CD**
The YAML file is located in .github/workflows/master_northernlights.yml
The environment is setup up with the following tutorials: <br>
https://github.com/Azure/webapps-deploy
https://docs.microsoft.com/en-us/azure/app-service/deploy-github-actions?tabs=applevel#generate-deployment-credentials%2010%20# <br>

<br>

**Useful links that helped me:** <br>
https://www.youtube.com/watch?v=nGVoHEZojiQ <br>
https://github.com/ultrasonicsoft/ng-openstreetmap-demo <br>
https://github.com/ultrasonicsoft/ng-openstreetmap-demo/blob/master/src/app/app.component.ts
https://github.com/CodeExplainedRepo/Weather-App-JavaScript <br>
https://github.com/atufkas/angular-openweather-app <br>
Mouse: https://stackoverflow.com/questions/26880487/formatting-the-mouseposition-control-output-in-openlayers-3 <br>
Timestamp to Date: https://stackoverflow.com/questions/38569832/convert-timestamp-to-date-using-angular-2-pipes <br>
https://openlayers.org/ <br>
