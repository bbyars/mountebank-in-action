'use strict';

require('any-promise/register/q');

var request = require('request-promise-any'),
  Q = require('q');

function create (url) {
  function getProducts() {
    var products;

    return request(url + '/products').then(function (body) {
      return Q(JSON.parse(body));
    });
  }

  return {
    getProducts: getProducts
  };
}

module.exports = {
  create: create
};

