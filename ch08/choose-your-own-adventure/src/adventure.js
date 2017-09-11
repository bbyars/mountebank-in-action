'use strict';

function create () {
  var currentState = '__INIT',
    stateMachine = {
      __INIT: {
        CREATE: function () {
          currentState = 'CREATE';
          return 'You find yourself in a tangled mess of enterprise IT. What do you do?\n' +
                 'QUIT: '
        }
      }
    };

  function do (action) {

  }

  return {
    do: do
  };
}

module.exports = {
  create: create
};
