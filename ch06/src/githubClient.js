'use strict';

// Adapted from https://developer.github.com/v3/guides/basics-of-authentication/

var path = require('path'),
  app = require('express')(),
  clientId = process.env.GH_BASIC_CLIENT_ID,
  clientSecret = process.env.GH_BASIC_CLIENT_SECRET;

app.set('views', path.join(__dirname, 'views'));
app.set('view engine', 'ejs');

app.get('/', function (request, response) {
  response.render('index', { clientId: clientId });
});

require('request-promise-any/node_modules/any-promise/register/q');
var httpRequest = require('request-promise-any');

app.get('/callback', function (request, response) {
  var url = require('url'),
    query = url.parse(request.url, true).query,
    util = require('util'),
    postBody = util.format('client_id=%s&client_secret=%s&code=%s',
      clientId, clientSecret, query.code);

  console.log(postBody);
  httpRequest({
    method: 'POST',
    uri: 'https://github.com/login/oauth/access_token',
    body: postBody
  }).then(function (body) {
    console.log(body);
    response.send('OK');
  }).done();
});

app.listen(3000, function () {
  console.log('github client listening on port 3000');
});
