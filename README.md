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

## Run Project
# UI
> Folder

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

## Test Cases
Card Type | Card Number | Exp. Date | CVV Code | Country/Currency | Results
--- | --- | --- | --- |--- |---
Amex | 374245455400126 | 	05/2023 |  |  | Success
Amex | 374245455400126 | 	05/2003 |  |  | Failure
Amex | 378282246310005 | 	05/2023 |  |  | Success
Amex | 343434736000692 | 	05/2023 |  |  | Failure
Amex | 340813837964364 | 	05/2023 |  |  | Failure
Amex | 345853567934887 | 	05/2023 |  |  | Failure
Discover | 60115564485789458 | 	05/2023 |  | US/USD  | Success
Discover | 6011000991300009 | 	05/2023 |  | US/USD | Success
Discover | 6011293173161551 | 	05/2023 |  | | Failure
Discover | 6011332856732405 | 	05/2023 |  | | Failure
Discover | 6011238238211959912 | 	05/2023 |  | | Success
Mastercard | 5425233430109903 | 	05/2023 |  |  | Success
Mastercard | 5425233430109903 | 	05/2023 |  |  | Success
Mastercard | 2222420000001113 | 	05/2023 |  |  | Success
Mastercard | 2223000048410010 | 	05/2023 |  |  | Success
Mastercard | 5488093786004654 | 	05/2023 |  |  | Failure
Mastercard | 2221001762754131 | 	05/2023 |  |  | Failure
Mastercard | 5199732226695143 | 	05/2023 |  |  | Failure
Visa | 4263982640269299 | 	05/2023 | 837 |  | Success
Visa | 4263982640269299 | 	05/2023 | 738 |  | Success
Visa | 4993593233294632 | 	05/2023 | 738 |  | Failure
Visa | 4485443740136246 | 	05/2023 | 738 |  | Failure
Visa | 4916291818012675588 | 	05/2023 | 738 |  | Failure


## DB Models
![Selection_087](https://user-images.githubusercontent.com/17546093/87583957-090adb80-c6dd-11ea-95e5-c70036fc1cbf.png)

## Process
![Selection_088](https://user-images.githubusercontent.com/17546093/87585608-a23af180-c6df-11ea-9539-e757f8ea8313.png)

## References
> https://www.freeformatter.com/credit-card-number-generator-validator.html
> https://developers.bluesnap.com/docs/test-credit-cards
