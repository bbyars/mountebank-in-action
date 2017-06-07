# Chapter 6: Programming mountebank

The injection examples require starting mountebank with the `--allowInjection` flag.
To keep it secure, we'll also test with the `--localOnly` flag.

In order to maintain readable JavaScript functions, the example JSON config files use
the `stringify` function mountebank adds to EJS to grab an external JavaScript file and
JSON-escape it.

## Listing 6.x: Testing the Roadie service

````
mb --allowInjection --localOnly --configfile examples/roadie-predicate.json &

# More than 10 degrees over (2nd row)
# Should respond with "Humidity levels dangerous, action required"
curl -i http://localhost:3000/ --data 'Day,Description,High,Low,Precip,Wind,Humidity
4-Jun,PM Thunderstorms,83,71,50%,E 5 mph,65%
5-Jun,PM Thunderstorms,82,69,60%,NNE 8mph,73%
6-Jun,Sunny,90,67,10%,NNE 11mph,55%
7-Jun,Mostly Sunny,86,65,10%,NE 7 mph,53%
8-Jun,Partly Cloudy,84,68,10%,ESE 4 mph,53%
9-Jun,Partly Cloudy,88,68,0%,SSE 11mph,56%
10-Jun,Partly Cloudy,89,70,10%,S 15 mph,57%
11-Jun,Sunny,90,73,10%,S 16 mph,61%
12-Jun,Partly Cloudy,91,74,10%,S 13 mph,63%
13-Jun,Partly Cloudy,90,74,10%,S 17 mph,59%'

# More than 3 days over (last 3 rows)
# Should respond with "Humidity levels dangerous, action required"
curl -i http://localhost:3000/ --data 'Day,Description,High,Low,Precip,Wind,Humidity
4-Jun,PM Thunderstorms,83,71,50%,E 5 mph,65%
5-Jun,PM Thunderstorms,82,69,60%,NNE 8mph,69%
6-Jun,Sunny,90,67,10%,NNE 11mph,55%
7-Jun,Mostly Sunny,86,65,10%,NE 7 mph,53%
8-Jun,Partly Cloudy,84,68,10%,ESE 4 mph,53%
9-Jun,Partly Cloudy,88,68,0%,SSE 11mph,56%
10-Jun,Partly Cloudy,89,70,10%,S 15 mph,57%
11-Jun,Sunny,90,73,10%,S 16 mph,61%
12-Jun,Partly Cloudy,91,74,10%,S 13 mph,63%
13-Jun,Partly Cloudy,90,74,10%,S 17 mph,64%'

# Should respond with "Humidity levels OK for the next 10 days"
curl -i http://localhost:3000/ --data 'Day,Description,High,Low,Precip,Wind,Humidity
4-Jun,PM Thunderstorms,83,71,50%,E 5 mph,65%
5-Jun,PM Thunderstorms,82,69,60%,NNE 8mph,63%
6-Jun,Sunny,90,67,10%,NNE 11mph,55%
7-Jun,Mostly Sunny,86,65,10%,NE 7 mph,53%
8-Jun,Partly Cloudy,84,68,10%,ESE 4 mph,53%
9-Jun,Partly Cloudy,88,68,0%,SSE 11mph,56%
10-Jun,Partly Cloudy,89,70,10%,S 15 mph,57%
11-Jun,Sunny,90,73,10%,S 16 mph,61%
12-Jun,Partly Cloudy,91,74,10%,S 13 mph,63%
13-Jun,Partly Cloudy,90,74,10%,S 17 mph,59%'

mb stop
````


