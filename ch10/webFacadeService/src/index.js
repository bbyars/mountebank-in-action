'use strict';

require('any-promise/register/q');

var express = require('express'),
  app = express(),
  url = process.env.RESCUE_URL || "https://api.rescuegroups.org/",
  gateway = require('./models/rescueGroupsGateway').create(url);

app.get('/nearbyAnimals', function (req, res) {
  gateway.getNearbyAnimals(req.query.postalCode, req.query.maxDistance).then(function (animals) {
    res.json({ animals: animals });
  }, function (err) {
    res.statusCode = 500;
    res.send(err);
  }).done();
});


app.get('/animals/:id', function (req, res) {
  gateway.getAnimalById(req.params.id).then(function (animal) {
    res.json(animal);
  }, function (err) {
    res.statusCode = 500;
    res.send(err);
  }).done();
});

app.listen(2000, function () {
  console.log('Content service started on port 2000');
});
