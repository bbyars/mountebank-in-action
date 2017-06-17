function (request, state, logger, callback) {
  var http = require('http'),
    options = {
      method: 'POST',
      hostname: 'localhost',
      port: 3000,
      path: '/callback?code=TEST-CODE'
    },
    httpRequest = http.request(options, function (response) {
      var body = '';
      response.setEncoding('utf8');
      response.on('data', function (chunk) {
        body += chunk;
      });
      response.on('end', function () {
        logger.warn('The callback body is: ' + body);
        callback({ body: body });
      });
    });

  httpRequest.end();
}
