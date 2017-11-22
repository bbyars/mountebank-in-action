# Chapter 9: Mountebank and Continuous Delivery

## Running the example services

First we need to install all dependencies:

````
(cd productService && npm install)
(cd contentService && npm install)
(cd webFacadeService && npm install)
````

Then to run them all at once:

````
export PRODUCT_SERVICE_URL='http://localhost:3000'
export CONTENT_SERVICE_URL='http://localhost:4000'
(cd productService && npm start &)
(cd contentService && npm start &)
(cd webFacadeService && npm start &)
````

I use a blunt instrument to kill them all:

````
killall node
````
