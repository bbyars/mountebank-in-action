'use strict';

module.exports = function (grunt) {
  grunt.loadNpmTasks('grunt-mocha-test');
  grunt.loadNpmTasks('grunt-mountebank');

  grunt.initConfig({
    mochaTest: {
      unit: {
        options: {
          reporter: 'spec'
        },
        src: ['unitTest/**/*.js']
      },
      functional: {
        options: {
          reporter: 'spec'
        },
        src: ['serviceTest/**/*.js']
      }
    },
    mb: {
      restart: [],
      stop: []
    }
  });

  grunt.registerTask('test:unit', 'Run the unit tests', ['mochaTest:unit']);

  grunt.registerTask('test:service', 'Run the service tests',
    ['mb:restart', 'try', 'mochaTest:service', 'finally', 'mb:stop', 'checkForErrors']);

  grunt.registerTask('default', ['test:unit']);
};
