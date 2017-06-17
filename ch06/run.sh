#!/usr/bin/env bash

# Point app to mountebank instead of github
export MBSTAR_BASE_AUTH_URL=http://localhost:3001
export MBSTAR_BASE_API_URL=http://localhost:3001

# Configure secrets
export GH_BASIC_CLIENT_ID=79553235751bfcb87654
export GH_BASIC_CLIENT_SECRET=a1284814800451b97f7288d9c9cd762bf176eb87

mb restart --allowInjection --localOnly --configFile examples/auth.json &
npm start &

# Wait for mb to fully initialize by waiting for it to create the pidfile
while [ ! -f mb.pid ]; do
  sleep 1
done

npm test

# $! expands to the PID of the last process executed in the background
kill $!
mb stop
