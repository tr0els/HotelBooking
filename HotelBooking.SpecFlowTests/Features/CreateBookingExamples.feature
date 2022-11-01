Feature: CreateBookingExamples

In order to be able to book a room
As a customer
I want to know if there are any unoccupied rooms between the start and end date I enter

@tag1
Scenario Outline: Enter start date and end date
	Given that the start date is <startOffset> days from now
	And and the end date is <endOffset> days from now
	When the method is called
	Then the result should be <validBooking>

	Examples: 
	| startOffset | endOffset | validBooking |
	| 1           | 9         | 'true'       |
	| 21          | 30        | 'true'       |
	| 9           | 21        | 'false'      |
	| 9           | 10        | 'false'      |
	| 9           | 20        | 'false'      |
	| 10          | 21        | 'false'      |
	| 20          | 21        | 'false'      |	
	| 10          | 20        | 'false'      |
	
