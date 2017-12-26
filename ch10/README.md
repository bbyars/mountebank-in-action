# Chapter 10: Performance testing with mountebank

## Running the Adoption Service

First we need to install all dependencies:

````
(cd adoptionService && npm install)
````

Then we can run it. Without any environment variables, the service connects to the
real RescueOrgs service:

````
(cd adoptionService && npm start)
````

You can run the API calls with `curl` in another console window:

````
curl http://localhost:5000/nearbyAnimals?postalCode=75228&maxDistance=50
curl http://localhost:5000/animals/10677691
````

## Listing 10.x: Capturing basic responses from RescueGroups.org

We have to add an environment variable to tell the Adoption service to use the URL of our proxy,
which we'll run on port 3000. Start it like this:

````
(cd adoptionService && RESCUE_URL=http://localhost:3000/ npm start)
````

Then, in a separate terminal window:

````
mb restart --configfile examples/basicProxy.json &

# Capture the two searches
curl http://localhost:5000/nearbyAnimals?postalCode=75228&maxDistance=20
curl http://localhost:5000/nearbyAnimals?postalCode=75228&maxDistance=50

# Capture the detailed results. Change the ids to ones returned in the last search
curl http://localhost:5000/animals/10677691
curl http://localhost:5000/animals/10837552
curl http://localhost:5000/animals/11618347

mb stop
````
