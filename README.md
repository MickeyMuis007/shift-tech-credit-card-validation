# shift-tech-credit-card-validation

## Intro
We want to write a system that allows our admins to submit a bunch of credit card numbers for validation. (Please read the guidelines below.)

## Requirements
Build an ASP.Net MVC application that can do the following:
Allow the user to submit a valid credit card number. 
Make a configurable list of credit card providers(VISA, AMEX, MasterCard, Discover). 
Check which credit card provider(VISA, AMEX, MasterCard, Discover) has issued the card and only allow cards to be captured that has the credit card provider in the configurable list.
If the card is valid and allowed – store it somewhere.
Do not capture the same valid card twice.
Display all the credit cards that have been successfully captured so far. 
A job should update 5 of the captured credit cards as “Processed” every 30 seconds. 
Processed cards should not be reprocessed. 
