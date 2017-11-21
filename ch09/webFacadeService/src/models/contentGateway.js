'use strict';

require('any-promise/register/q');

var request = require('request-promise-any'),
  Q = require('q');

function create (url) {
  function getContent(productIds) {
    var query = 'ids=' + productIds.join(',');
    return request(url + '/content?' + query).then(function (body) {
      return Q(JSON.parse(body));
    });
  }

  return {
    getContent: getContent
  };
}

module.exports = {
  create: create
};

