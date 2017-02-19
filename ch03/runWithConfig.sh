#!/usr/bin/env bash

FILE=$1
REPEAT=${2:-1}

## Start mountebank
~/src/mountebank/bin/mb --configfile configfiles/${FILE}.ejs &
while [ ! -f mb.pid ]; do
  sleep 1
done

# Call imposter $REPEAT times
for (( i = 0; i < $REPEAT; i++ )); do
  curl -i -k https://localhost:3000/
  echo
done

~/src/mountebank/bin/mb stop
