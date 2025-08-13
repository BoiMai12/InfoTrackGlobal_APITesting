Feature: Users

    Background:
        Given I set HTTP base URL from appSettings "Staging:HttpBaseUrl"
		And I set the request headers:
		| Name		| Key			 |
		| x-api-key | reqres-free-v1 |
@tag1
Scenario: User create User with invalid data
	Given I add Http request content from the file: 'CreateUserMorpheus.json'
	When I send a POST request to '/api/users'
	Then I should receive the HTTP response status code '201'
	And I should receive the HTTP response has 'name' is 'morpheus'
	And I should receive the HTTP response has 'job' equal 'leader'

@tag1
	Scenario: User create User successfully
        Given I add Http request content from the file: ''
        When I send a POST request to '/api/users'
        Then I should receive the HTTP response status code '200'
        And I should receive the HTTP response has 'Error' is NULL
        And I should receive the HTTP response has 'PayLoad' equal 'True'
