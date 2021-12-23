# NorthernLights

## Github Actions
[![.NET](https://github.com/IoannisArmamentos/NorthernLights/actions/workflows/pipeline.yml/badge.svg)](https://github.com/IoannisArmamentos/NorthernLights/actions/workflows/pipeline.yml)

A weather forecast application made in Visual Studio using Angular 8 and ASP.NET Core 5 frameworks. It may take up to 10 seconds to load the weather data.
Production is hosted on Azure here: https://northernlights.azurewebsites.net/ <br>

![gen](https://user-images.githubusercontent.com/1202504/147295852-f27948f6-681f-45fb-9fff-e53e781f077b.png)


## APIs <br>
**Openstreetmap** <br>
You can select a location on the map with your mouse. Latitude & longitude are saved on click. <br>
Longitude=X and latitude=Y are appearing below the map depending on the pointer of your mouse. In real life, Latitude always goes first. By default, the map is zoomed into my region. I haven't set default coordinates.

**OpenWeather** <br>
Utilizing the One Call API from https://openweathermap.org/api/one-call-api , we can get all the essential weather data for a specific location in json format. The json response can be translated to typescript with a tool like http://json2ts.com/ . <br>
![json](https://user-images.githubusercontent.com/1202504/147295616-617d48db-8805-47c8-9537-9cc9f705b3c4.png)


I used Current & Daily Weather and Region Alerts. These alerts refer to the regional area around the choosen coordinates as well as the local language. These are setup by the API and cannot be changed or be more precise/detailed. <br>
Global weather alerts with live updates can be created by subscribing to the paid API, implementing Webhook in backend that will be called. To push that info into the frontend, websocket implementation can be used.![alert3](https://user-images.githubusercontent.com/1202504/147295431-364073eb-ab20-4f7e-949f-c6151edc2930.png)

## Source Files <br>
**WeatherForecast.cs** <br>
Declaring classes and variables. Date comes in *double dt* Unix timestamp that needs proper converting. <br>
For example, *public class Daily* has to do with the incoming values for the next 10 days. <br>

**WeatherForecastController.cs** <br>
GetAsync sends a GET request with apikey, langitude and longitude, etc. <br>
Initially I had a HttpClient returning a big result but for performance costs I didn't use it. <br>
Also I have commented a *foreach* that runs the list of *daily* and does the proper backend conversions for the Timestamps(dt, sunrise,sunset). <br>

**OpenWeatherOptions.cs** <br>
Apikey and BaseUrl that are used in *WeatherForecastController.cs*. <br> 

**Extensions.cs** <br>
This backend method is currently not used. <br>
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

## CI/CD <br>
The YAML file is located in *.github/workflows/pipelines.yml* <br>
Currently there are a master and a dev branch. Every new commit, passes through the CI/CD that is properly setup with specific factors.<br>

<br>

## Useful links that helped me <br>
https://www.youtube.com/watch?v=nGVoHEZojiQ <br>
https://github.com/CodeExplainedRepo/Weather-App-JavaScript <br>
OpenWeatherMap: <br>
https://github.com/ultrasonicsoft/ng-openstreetmap-demo <br>
https://github.com/ultrasonicsoft/ng-openstreetmap-demo/blob/master/src/app/app.component.ts <br>
https://github.com/atufkas/angular-openweather-app <br>
https://openlayers.org/ <br>
Mouse: <br>
https://stackoverflow.com/questions/26880487/formatting-the-mouseposition-control-output-in-openlayers-3 <br>
Timestamp to Date: <br>
https://stackoverflow.com/questions/38569832/convert-timestamp-to-date-using-angular-2-pipes <br>
CI/CD: <br>
https://github.com/Azure/webapps-deploy <br>
https://docs.microsoft.com/en-us/azure/app-service/deploy-github-actions?tabs=applevel#generate-deployment-credentials%2010%20#
