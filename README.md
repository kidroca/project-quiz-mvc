# Project Quiz 

## Description

A web application that allows users to create, rate and take quizzes/tests 

A user is able to register a local account or use an existing google account. 
There is a profile page where additional information can be reviewed and editted

Anyone can view and try to solve the public quizzes no need to register or log-in, 
registered users can make private quizzes that they can decide with whom to share.

An option is available to create a topic of many questions and then each time a
user request a test from this topic a random test is generated


## Demo:
Live demo at azure: [https://projectquiz.azurewebsites.net/](https://projectquiz.azurewebsites.net/)

## Features:

* Registration / Login (external login: google)
* Reviewing existing quizzes
* Creating a new Quiz
* Taking a Quiz
* Quiz Result - Wrong / Correct Answers
* User Profile Page - with edittable information
* Creating a quiz topic - for each test of this topic a group of questions is 
gathered and the test is formed randomly
* Answers shuffling - if desired the answers can be shuffled
* Daily|Weekly|Mothly|AllTime ranking
* User avatars from gravatar - if the user have an avatar image at [gravatar.com](https://en.gravatar.com/)
it will be associated with his account

## Todos:

* Picture upload and review window - quiz avatar image
* My Quizzes Page
* Customized notifications
* Activity indicators
* Facebook external login
* Created pdf/printable from a given test

## Technologies used

### Languages
* C#
* JavaScript

### Frameworks
* ASP NET MVC 5 (@Razor)
* Entity Framework 6
* AngularJs 1.5
* Bootstrap

### Utilities
* Autofac - Dependency Inversion
* AutoMapper - Model mappings
