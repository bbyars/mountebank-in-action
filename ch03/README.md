# Chapter 3

Most of the examples can be run in a bash shell with the `run.sh` script. The examples are listed 
under the examples directory.

`run.sh helloWorld` starts mountebank, creates the examples/helloWorld.json imposter, and sends a request
to it.

`run.sh inventory 4` sends 4 consecutive requests to the examples/inventory.json imposter. 

`runWithConfigh.sh inventory` will start mountebank with the config file at configfiles/inventory.json.
