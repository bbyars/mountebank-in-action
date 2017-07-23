# Chapter 7: Understanding behaviors

Any behavior that allows script execution (JavaScript or shell) requires starting mountebank
with the `--allowInjection` flag. To keep it secure, we'll also test with the `--localOnly` flag.

In order to maintain readable JavaScript functions, most example JSON config files use
the `stringify` function mountebank adds to EJS to grab an external JavaScript file and
JSON-escape it.

Since it's not the focus of the chapter, we'll ignore predicates for the examples.

## Listing 7.x: Using an `inject` response to add a dynamic timestamp

````
mb --allowInjection --localOnly --configfile examples/inject-response-timestamp.json &

# Should respond with current timestamp in response body
curl -i http://localhost:3000/

mb stop
````

## Listing 7.x: Combining an `is` response with a `decorate` behavior

````
mb --allowInjection --localOnly --configfile examples/decorate-timestamp.json &

# Should respond with current timestamp in response body
curl -i http://localhost:3000/

mb stop
````

## Listing 7.x: Adding a `decorate` behavior to recorded responses

````
mb --allowInjection --localOnly --configfile examples/decorate-proxy.json &

# Should respond with current timestamp in response body
curl -i http://localhost:3000/

# Should respond with current timestamp in response body
curl -i http://localhost:3000/

mb stop
````

