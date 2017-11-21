'use strict';

require('any-promise/register/q');

var express = require('express'),
  request = require('request-promise-any'),
  Q = require('q'),
  productsGateway = require('./models/productsGateway').create('http://localhost:3000'),
  contentGateway = require('./models/contentGateway').create('http://localhost:4000');

function getProductsWithContent () {
  var products;

  return productsGateway.getProducts().then(function (response) {
    products = response.products;

    var productIds = products.map(function (product) {
      return product.id;
    });
    return contentGateway.getContent(productIds);
  }).then(function (response) {
    var contentEntries = response.content;

    products.forEach(function (product) {
      var contentEntry = contentEntries.find(function (entry) {
        return entry.id === product.id;
      });
      product.copy = contentEntry.copy;
      product.image = contentEntry.image;
    });

    return Q(products);
  });
}

var app = express();

app.get('/products', function (req, res) {
  console.log('[Web Facade service] /products');
  getProductsWithContent().then(function (results) {
    res.send({ products: results });
  }, function (err) {
    console.log(err);
    res.statusCode = 500;
    res.send(err);
  });
});

app.listen(2000, function () {
  console.log('Web facade service started on port 2000');
});
