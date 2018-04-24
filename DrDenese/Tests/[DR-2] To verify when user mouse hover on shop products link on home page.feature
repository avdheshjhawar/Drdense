Feature: [DR-2] To verify when user mouse hover on shop products link on home page
	
@mytag
Scenario: [DR-2] To verify when user mouse hover on shop products link on home page
	Given user is present on home page 	
	When user mouse hover on shop products link
	Then all sublinks of shop products should display
