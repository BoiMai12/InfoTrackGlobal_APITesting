Feature: Users

    Background:
       Given I set HTTP base URL from appSettings "Staging:HttpBaseUrl"

#Precondition to pass token to other endpoints
    @E2EFlow1 @E2EFlow2
	Scenario: 00Login app successfully
	   Given I add Http request content from the file: 'Users/LoginRequest_Template.json' with data from appSettings
	   When I send a POST request to 'api/login'
	   Then I should receive the HTTP response status code '200'
	   Given I save HTTP response 'token' to variable 'BearerToken'

    @E2EFlow1
	Scenario: Create user successfully
	   Given I add Http request content from the file: 'Users/CreateUserMorpheus.json'
	   When I send a POST request to '/api/users'
	   Then I should receive the HTTP response status code '201'
	   And I should receive the HTTP response has 'name' is 'morpheus'
       And I should receive the HTTP response has 'job' is 'leader'

    @E2EFlow2
	Scenario Outline: Can not create user due to authentication failure
	   Given I add Http request content from the file: 'Users/CreateUserMorpheus.json' with invalid '<authentication>'
	   When I send a POST request to '/api/users'
	   Then I should receive the HTTP response status code <statusCode>
	   And I should receive the HTTP response has 'error' is <errorMessage>

	   Examples:  
	    | authentication | statusCode | errorMessage				 |
	    |     reqres     | 403        | Invalid or inactive API key |
		|                | 401        | Missing API key			 |

    @E2EFlow1
   	Scenario: Get user by valid Id
	   Given I send a GET request to '/api/users/2'
	   Then I should receive the HTTP response status code '200'
	   And I should receive the HTTP response has 'data.id' is '2'
	   And I should receive the HTTP response has 'data.first_name' is 'Janet'

    @E2EFlow2
    Scenario: Get user by Id doesn't exist
	   Given I send a GET request to '/api/users/200'
	   Then I should receive the HTTP response status code '404'
   

    Scenario Outline: Can not Get user due to authentication failure
	   Given I pass invalid authentication '<auth>' into header
	   When I send Get to '/api/users/2900'
	   Then I should receive the HTTP response status code <statusCode>
	   And I should receive the HTTP response has 'error' is <errorMessage>

	   Examples:  
        | authentication		  | statusCode | errorMessage                | 
        | reqres-free-v199        | 403        | Invalid or inactive API key | 
        |						  | 401        | Missing API key             | 
    
	@E2EFlow1
	Scenario: Update user successfully
	   Given I add Http request content from the file: 'Users/CreateUserMorpheus.json'
	   When I send a PUT request to '/api/users'
	   Then I should receive the HTTP response status code '201'
	   And I should receive the HTTP response has 'name' is 'morpheus'
       And I should receive the HTTP response has 'job' is 'leader'

    @E2EFlow2
    Scenario Outline: Can not Update user due to authentication failure
	   Given I add Http request content from the file: 'Users/CreateUserMorpheus.json' with invalid '<authentication>'
	   When I send a PUT request to '/api/users'
	   Then I should receive the HTTP response status code <statusCode>
	   And I should receive the HTTP response has 'error' is <errorMessage>
	   Examples:  
	    | authentication | statusCode | errorMessage				|
	    |     reqres     | 403        | Invalid or inactive API key |
		|                | 403        | Missing API key			    |
    
	@E2EFlow1
   	Scenario: Delete user successfully
	   Given I send a Delete to '/api/users/4'
	   Then I should receive the HTTP response status code '200'

   @E2EFlow2
   Scenario Outline: Can not Delete user due to authentication failure
	   Given I pass invalid authentication '<auth>' into header
	   When I send Delete to '/api/users/4'
	   Then I should receive the HTTP response status code <statusCode>
	   Examples:  
	    | authentication		     | statusCode | errorMessage				| 
	    |     reqres-free-v19999     | 403        | Invalid or inactive API key |
		|                            | 403        | Missing API key			    |
