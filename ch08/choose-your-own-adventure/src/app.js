'use strict';

var net = require('net'),
  Adventure = require('./adventure'),
  server = net.createServer(),
  adventures = {};

server.on('connection', function (socket) {
  socket.on('data', function (data) {
    var message = data.toString('utf8'),
      lines = message.split('\n'),
      who = lines[0].trim(),
      action = lines[1].trim();

    if (!adventures[who]) {
      adventures[who] = Adventure.create();
    }

    socket.end(adventures[who].do(action), 'utf8');
  });

  socket.on('end', function () {
    console.log('Socket ended');
  });
});

server.on('error', function (error) {
  console.log(error);
});

server.listen(3000, function () {
  console.log('Server now running...')
});
