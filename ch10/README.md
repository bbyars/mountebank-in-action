# Chapter 10: Performance testing with mountebank

## Running the example services

First we need to install all dependencies:

````
(cd adoptionService && npm install)
````

Then we can run it. Without any environment variables, the service connects to the
real RescueOrgs service:

````
(cd adoptionService && npm start &)
````

I use a blunt instrument to kill it:

````
killall node
````
