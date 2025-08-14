Feature: Users

    Background:
        Given I set HTTP base URL from appSettings "Staging:HttpBaseUrl"
		And I set api key "Staging:x-api-key"

	Scenario: User create User successfully
		Given I add Http request content from the file: 'Users/CreateUserMorpheus.json'
		When I send a POST request to '/api/users'
		Then I should receive the HTTP response status code '201'
		And I should receive the HTTP response has 'id' is not NULL
		And I should receive the HTTP response has 'name' is 'morpheus'
		And I should receive the HTTP response has 'job' is 'leader'


