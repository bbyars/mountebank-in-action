# Chapter 8: Protocols

The examples below assume you have netcat (`nc`) installed on your machine.
Netcat is like telnet, but easier to script.

## Listing 8.x: Virtualizing a TCP updateInventory call

````
mb --configfile examples/updateInventory.json &

# Should respond with "0\n1343"
echo "updateInventory\n5131\n-5" | nc localhost 3000

mb stop
````

## Listing 8.x: Using a TCP proxy

````
mb --configfile examples/textProxy.json &

# Should respond with "0\n1343"
# Note the -q 1 flag, which unfortunately doesn't work on my Mac but does on Linux
# It prevents netcat from prematurely closing the connection while mountebank negotiates the proxy
# You can leave it off and the first call will fail (by the time mb tries to respond, the
# nc client will have closed the connection), but the subsequent calls will work because
# mountebank saved the response
echo "updateInventory\n5131\n-5" | nc -q 1 localhost 3000

# Should respond with "0\n1343" (proxy serving saved response)
echo "updateInventory\n5131\n-5" | nc localhost 3000

# Should respond with "0\n0" if you go to the origin server
echo "updateInventory\n5131\n-5" | nc localhost 3333

mb stop
````

## Listing 8.x: Using a TCP proxy with predicate generators

````
mb --configfile examples/proxyWithPredicateGenerators.json &

# Should respond with "0\n1343"
echo "updateInventory\n5131\n-5" | nc -q 1 localhost 3000

# Should respond with "0\n1343" (saved response, no -q needed)
echo "updateInventory\n5131\n-5" | nc localhost 3000

# Should respond with "0\0"
echo "updateInventory\n5040\n-5" | nc -q 1 localhost 3000

# Should respond with "0\0" (saved response, no -q needed)
echo "updateInventory\n5040\n-5" | nc localhost 3000

mb stop
````
