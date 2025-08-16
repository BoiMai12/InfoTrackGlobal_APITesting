Feature: Users

    Background:
       Given I set HTTP base URL from appSettings "Staging:HttpBaseUrl"

#Precondition to pass token to other endpoints
	Scenario: 00_Login app successfully
	   Given I add Http request content from the file: 'Users/LoginRequest_Template.json' with data from appSettings
	   When I send a POST request to 'api/login'
	   Then I should receive the HTTP response status code '200'
	   Given I save HTTP response 'token' to variable 'BearerToken'

	Scenario: Create user successfully
	   Given I add Http request content from the file: 'Users/CreateUserMorpheus.json'
	   When I send a POST request to '/api/users'
	   Then I should receive the HTTP response status code '201'
	   And I should receive the HTTP response has 'name' is 'morpheus'
       And I should receive the HTTP response has 'job' is 'leader'


    Scenario Outline: Can not create user due to authentication failure
	   Given I add Http request content from the file: 'Users/CreateUserMorpheus.json' with invalid '<authentication>'
	   When I send a POST request to '/api/users'
	   Then I should receive the HTTP response status code <statusCode>
	   And I should receive the HTTP response has 'error' is <errorMessage>

	   Examples:  
	    | authentication | statusCode | errorMessage				 |
	    |     reqres     | 403        | Invalid or inactive API key |
		|                | 401        | Missing API key			 |

   	Scenario: Get user by Id
	   Given I send a GET request to '/api/users/<UserId>'
	   Then I should receive the HTTP response status code <statusCode>
	   And I should receive the HTTP response has 'data.Id'' is <expectedId>
	   #And I should receive the HTTP response has 'data.first_name' is <expectedFirstName>

	   
	   Examples:  
	    | UserId | statusCode | expectedId |
        | 2      | 200        | 2		    |
        | 467    | 404        |            |


    Scenario Outline: Can not Get user due to authentication failure
	   Given I add Http request content from the file: 'Users/CreateUserMorpheus.json' with invalid '<authentication>'
	   When I send a GET request to '/api/users/<UserId>'
	   Then I should receive the HTTP response status code <statusCode>
	   And I should receive the HTTP response has 'PayLoad.Id' is <errorMessage>

	   Examples:  
        | authentication | statusCode | errorMessage                | UserId |
        | reqres         | 403        | Invalid or inactive API key | 2      |
        |                | 401        | Missing API key             | 2      |