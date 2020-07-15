# shift-tech-credit-card-validation

## Intro
> We want to write a system that allows our admins to submit a bunch of credit card numbers for validation. (Please read the guidelines below.)

## Requirements
> Build an ASP.Net MVC application that can do the following:
Allow the user to submit a valid credit card number. 
Make a configurable list of credit card providers(VISA, AMEX, MasterCard, Discover). 
Check which credit card provider(VISA, AMEX, MasterCard, Discover) has issued the card and only allow cards to be captured that has the credit card provider in the configurable list.
If the card is valid and allowed – store it somewhere.
Do not capture the same valid card twice.
Display all the credit cards that have been successfully captured so far. 
A job should update 5 of the captured credit cards as “Processed” every 30 seconds. 
Processed cards should not be reprocessed. 

## How to validate a Credit Card Number?
> Most credit card number can be validated using the Luhn algorithm, which is more or a less a glorified Modulo 10 formula!

## The Luhn Formula:
> - Drop the last digit from the number. 
> - The last digit is what we want to check against
> - Reverse the numbers
> - Multiply the digits in odd positions (1, 3, 5, etc.) by 2 and subtract 9 to all any result higher than 9
> - Add all the numbers together
> - The check digit (the last number of the card) is the amount that you would need to add to get a multiple of 10 (Modulo 10)

![Selection_086](https://user-images.githubusercontent.com/17546093/87582286-7cf7b480-c6da-11ea-8982-aab6fa9872d3.png)

![Selection_085](https://user-images.githubusercontent.com/17546093/87581918-fba02200-c6d9-11ea-8ee4-84fccf07ad89.png)
