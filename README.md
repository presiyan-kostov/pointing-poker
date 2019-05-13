# Pointing poker app

React app for pointing poker

## API endpoints

### ~/api/authentication/login

* Type: get
* Request parameters
  * string username
  * string password
* Return
  * Ok: UserModel user
  * BadRequest
  * InternalServerError: string message

### ~/api/user/{username}

* Type: get
* Return
  * Ok: UserModel user
  * InternalServerError: string message

### ~/api/user/register

* Type: post
* Request parameters
  * SaveModel model
* Return
  * Ok: UserModel user
  * BadRequest: IList < ValidationError >

### ~api/user/{username}/projects

* Type: get
  * Ok: IList< ProjectModel> projects
  * InternalServerError: string message