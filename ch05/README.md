# Mountebank in Action

These examples are in support of chapter 5: Using proxies

Example: Setting up a basic proxy to query inventory

````
mb --configfile examples/basic-inventory.json &

# Should respond with 54
curl -i http://localhost:3000/inventory/2599b7f4

# Should still respond 54, even though the downstream service would return 21
curl -i http://localhost:3000/inventory/2599b7f4

# Shows the changed state of the imposter
curl -i http://localhost:2525/imposters/3000

# Returns 21, showing difference between proxy and downstream service
curl -i http://localhost:8080/inventory/2599b7f4

mb stop
````
