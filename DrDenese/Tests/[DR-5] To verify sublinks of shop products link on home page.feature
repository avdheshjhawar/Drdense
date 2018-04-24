Feature: [DR-5] To verify sublinks of shop products link on home page

@mytag
Scenario: [DR-5] To verify sublinks of shop products link on home page

	Given user is present on home page 	
	When user mouse hover on shop products link
	Then all sublinks of shop products should display


	When user click on Regiments and Kit sub-link of shop products link
	Then user should redirected to desired page
