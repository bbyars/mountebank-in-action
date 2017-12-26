# Chapter 10: Performance testing with mountebank

## Running the example services

First we need to install all dependencies:

````
(cd webFacadeService && npm install)
````

Then we can run it. Without any environment variables, the service connects to the
real RescueOrgs service:

````
(cd webFacadeService && npm start &)
````

I use a blunt instrument to kill it:

````
killall node
````
