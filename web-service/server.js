'use strict';

const mongodb = require('mongodb');
const nconf = require('nconf');
const app = require('express')();
const bodyParser = require('body-parser');
const https = require('https');
var db;

nconf.argv().env().file('keys.json');

const user = nconf.get('mongoUser');
const pass = nconf.get('mongoPass');
const host = nconf.get('mongoHost');
const port = nconf.get('mongoPort');
const dbName = nconf.get('mongoDatabase');

app.use(bodyParser.json());

const uri = `mongodb://${user}:${pass}@${host}:${port}/${dbName}`;

mongodb.MongoClient.connect(uri, (err, db_) => {
  if (err) {
    console.log('MongoDB connection error', err);
    return;
  }

  db = db_;

  app.get('/', (request, response) => {
    response.sendStatus(200);
  });

  app.get('/users/:username', (request, response) => {
    db.collection('users', {strict : true}, (err, users) => {
      if (err) {
        response.set('Content-Type', 'text/plain');
        response.status(404).send(err);
        return;
      }

      users.find({user_name: request.params.username}).toArray((err, result) => {
        if (err || result.length === 0) {
          if (err) {
            response.set('Content-Type', 'text/plain'); 
            response.status(400).send(err);      
          }
          else {
            response.sendStatus(404);
          }
        }
        else {
          response.status(200).json(result[0]);
        }
      });
    });
  });

  // app.post('/', (request, response) => {
  //   db.collection('users', {strict : true}, (err, users) => {
  //     (users.find({user_name : request.body.username}).toArray((err, result) => {
  //       if (result.length == 1) {
  //       }
  //     })) 
  //     let newUser = request.body;
  //     users.insert()
  //   });
  // });
});

app.listen(8080, () => {
  console.log('Listening on port 8080');
});