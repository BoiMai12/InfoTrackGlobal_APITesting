Feature: Login

    Background:
       Given I set HTTP base URL from appSettings "Staging:HttpBaseUrl"

	Scenario: Login app successfully
	   Given I add Http request content from the file: 'Users/LoginRequest_Template.json' with data from appSettings
	   When I send a POST request to 'api/login'
	   Then I should receive the HTTP response status code '200'


   	Scenario: Login app Unsuccessfully
	   Given I add Http request content from the file: 'Users/LoginRequest_Template.json' with data from appSettings
	   When I send a POST request to 'api/login'
	   Then I should receive the HTTP response status code '200'

