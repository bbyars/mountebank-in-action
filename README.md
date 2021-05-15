# Welcome, friend

[Mountebank](http://mbtest.org) is one of the most capable service virtualization
platforms in the world, and it's entirely free. You can learn more about
it by reading the Manning book
[Testing Microservices with Mountebank](https://www.manning.com/books/testing-microservices-with-mountebank):

![the book](https://images.manning.com/255/340/resize/book/d/b083e59-69bc-477f-b97f-33a701366637/Byars-Mountebank-MEAP-HI.png)

This repo captures the code examples used throughout the book. Enjoy!

## curl commands
1. Configure imposter
1.1 From file
`curl -d@helloWorld.json http://localhost:2525/imposters`

1.2 From command line directly
```
curl -X POST http://localhost:2525/imposters --data '{      
  "port": 3000,                                             
  "protocol": "http",                                       
  "stubs": [{
    "responses": [{
      "is": {                                               
        "statusCode": 200,                                  
        "headers": {"Content-Type": "application/json"},    
        "body": {                                           
          "products": [                                     
            {                                               
              "id": "2599b7f4",                             
              "name": "The Midas Dogbowl",                  
              "description": "Pure gold"                    
            },                                              
            {                                               
              "id": "e1977c9e",                             
              "name": "Fishtank Amore",                     
              "description": "Show your fish some love"     
            }                                               
          ],                                                
          "_links": {                                       
            "next": "/products?page=2&itemsPerPage=2"       
          }                                                 
        }                                                   
      }                                                     
    }]
  }]
}'
```

1.3 Change the default response
```
curl -X POST http://localhost:2525/imposters --data '{ 
  "protocol": "http",
  "port": 3000,
  "defaultResponse": {                 
    "statusCode": 400,                 
    "headers": {                       
      "Connection": "Keep-Alive",      
      "Content-Length": 0              
    }
  },
  "stubs": [{
    "responses": [{
      "is": { "body": "BOOM!!!" }      
    }]
  }]
}'
```
- new default response now will be
````
HTTP/1.1 400 Bad Request                1
Connection: Keep-Alive                  2
Content-Length: 7                       3
Date: Fri, 17 Feb 2017 16:29:00 GMT     4

BOOM!!! 
```
2. Remove all existing imposters
curl -X DELETE http://local host:2525/imposters
