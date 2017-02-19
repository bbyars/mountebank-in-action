#!/usr/bin/env bash

FILE=$1
REPEAT=${2:-1}

## Start mountebank
~/src/mountebank/bin/mb &
while [ ! -f mb.pid ]; do
  sleep 1
done

# Create imposter
curl -d@examples/$FILE.json http://localhost:2525/imposters
echo

# Call imposter $REPEAT times
for (( i = 0; i < $REPEAT; i++ )); do
  curl -i -k http://localhost:3000/
  echo
done

~/src/mountebank/bin/mb stop
