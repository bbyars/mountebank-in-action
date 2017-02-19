#!/usr/bin/env bash

FILE=$1

## Start mountebank
~/src/mountebank/bin/mb &
while [ ! -f mb.pid ]; do
  sleep 1
done

# Create imposter
curl -d@examples/$FILE.json http://localhost:2525/imposters
echo

# Call the imposter
curl -i -k http://localhost:3000/
echo

~/src/mountebank/bin/mb stop
