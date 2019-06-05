import React, { Component } from "react";
import { Form } from "react-bootstrap";
import { Button } from "react-bootstrap";
import { UserContext } from "../contexts/UserContext";
import  { Redirect } from 'react-router-dom';
import "./SignUp.css";

export default class SignUp extends Component {
  constructor(props) {
    super(props);

    this.state = {
      username: '',
      password: '',
      firstname: '',
      lastname: '',
      email: '',
      errors: {
        username: '',
        password: '',
        firstname: '',
        lastname: '',
        email: ''
      },
      isSuccess: false
    };

    this.validationErrorTypes ={
      UsernameMissing : 0,
      UsernameExists : 1,
      UsernameToLong : 2,
      PasswordMissing : 3,
      PasswordToLong : 4,
      FirstnameMissing : 5,
      FirstnameToLong : 6,
      LastnameMissing : 7,
      LastnameToLong : 8,
      EmailMissing : 9,
      EmailToLong : 10,
      EmailWrongFormat : 11
    };

    Object.freeze(this.validationErrorTypes);
  }

  validateForm() {
    let errors = {};

    let { username, password, firstname, lastname, email } = this.state;

    if (!username){
      errors.username = 'Please enter the username.';
    }
    else{
      errors.username = '';
    }

    if (!password){
      errors.password = 'Please enter the password.';
    }
    else if (password.length < 8){
      errors.password = 'Minimal password length is 8 characters.';
    }
    else{
      let hasBigLetter = false;
      let hasSmallLetter = false;
      let hasDigit = false;
      let hasSpecial = false;

      for (let c of password){
        if (c >='A' && c <= 'Z'){
          hasBigLetter = true;
        }
        else if (c >= 'a' && c <= 'z'){
          hasSmallLetter = true;
        }
        else if (c >= '0' && c <= '9'){
          hasDigit = true;
        }
        else{
          hasSpecial = true;
          break;
        }
      }

      if (hasSpecial){
        errors.password = 'The password mustn\'t contain special chars.';
      }
      else if (hasBigLetter && hasSmallLetter && hasDigit){
        errors.password = '';
      }
      else{
        errors.password = 'The password must contain a big letter, a small letter and a digit.';
      }
    }

    if (!firstname){
      errors.firstname = 'Please enter the first name.';
    }
    else{
      errors.firstname = '';
    }

    if (!lastname){
      errors.lastname = 'Please enter the last name.';
    }
    else{
      errors.lastname = '';
    }

    if (!email){
      errors.email = 'Please enter the email.';
    }
    else{
      const validEmailRegex = RegExp(/^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i);

      if (!validEmailRegex.test(email)){
        errors.email = 'Please enter valid email.';
      }else{
        errors.email = '';
      }
    }

    return {errors: errors};
  }

  handleChange = event => {
    this.setState({
      [event.target.id]: event.target.value,
      errors: Object.assign(this.state.errors, {[event.target.id]: ''})
    });
  }

  handleSubmit = event => {
    event.preventDefault();

    // Client-side validation
    this.setState(this.validateForm);

    this.setState((state) => {
      for (let e in state.errors){
        if (state.errors[e].length > 0){
          return;
        }
      }
  
      const URL = 'http://localhost:62973/api/User/register';

      let data = {
        username: this.state.username,
        password: this.state.password,
        firstname: this.state.firstname,
        lastname: this.state.lastname,
        email: this.state.email
      };

      fetch(URL, {
          method: 'POST',
          body: JSON.stringify(data),
          headers: {
            'Accept' : 'application/json',
            'Content-type' : 'application/json'
          },
        })
        .then(response => {
          if (response.status == 200){
            response.json().then(response =>{
              this.context.pushNewMessage({text: `Mr/Mrs ${response.firstname} ${response.lastname}, you have been successfully signed up. You can log in with your credentials below.`, variant: 'success'}, true);
              this.setState({isSuccess: true});
            });
          }else{
            response.json().then(response => {
              let errors = {};

              if (response.some(x => x == this.validationErrorTypes.UsernameMissing)){
                errors.username = 'Please enter the username.';
              }else if (response.some(x => x == this.validationErrorTypes.UsernameExists)){
                errors.username = 'This username is already taken. Please choose another one.';
              }else if (response.some(x => x == this.validationErrorTypes.UsernameToLong)){
                errors.username = 'The entered username is too long.';
              }else{
                errors.username = '';
              }

              if (response.some(x => x == this.validationErrorTypes.PasswordMissing)){
                errors.password = 'Please enter the password.';
              }else if (response.some(x => x == this.validationErrorTypes.PasswordToLong)){
                errors.password = 'The entered password is too long.';
              }else{
                errors.password = '';
              }

              if (response.some(x => x == this.validationErrorTypes.FirstnameMissing)){
                errors.firstname = 'Please enter the first name.';
              }else if (response.some(x => x == this.validationErrorTypes.FirstnameToLong)){
                errors.firstname = 'The first name is too long.';
              }else{
                errors.firstname = '';
              }

              if (response.some(x => x == this.validationErrorTypes.LastnameMissing)){
                errors.lastname = 'Please enter the last name.';
              }else if (response.some(x => x == this.validationErrorTypes.LastnameToLong)){
                errors.lastname = 'The entered last name is too long.';
              }else{
                errors.lastname = '';
              }

              if (response.some(x => x == this.validationErrorTypes.EmailMissing)){
                errors.email = 'Please enter the email.';
              }else if (response.some(x => x == this.validationErrorTypes.EmailToLong)){
                errors.email = 'The entered email is too long.';
              }else if (response.some(x => x == this.validationErrorTypes.EmailWrongFormat)){
                errors.email = 'Please enter valid email.';
              }else{
                errors.email = '';
              }

              this.setState({errors: errors});
            });
          }
        });
    });
  }

  render() {
    if (this.state.isSuccess){
      return (<Redirect to="/login"></Redirect>); 
    }

    let {errors} = this.state;

    return (
      <div>
        <h1>Sign up</h1>
        <form onSubmit={this.handleSubmit}>

          <Form.Group controlId="username">
            <Form.Label>Username</Form.Label>
            <Form.Control
              autoFocus
              value={this.state.username}
              onChange={this.handleChange}
              isInvalid={errors.username.length > 0}
            />
            <Form.Control.Feedback type="invalid">{errors.username}</Form.Control.Feedback>
          </Form.Group>

          <Form.Group controlId="password">
            <Form.Label>Password</Form.Label>
            <Form.Control
              value={this.state.password}
              onChange={this.handleChange}
              type="password"
              isInvalid={errors.password.length > 0}
            />
            <Form.Control.Feedback type="invalid">{errors.password}</Form.Control.Feedback>
          </Form.Group>

          <Form.Group controlId="firstname">
            <Form.Label>First name</Form.Label>
            <Form.Control
              value={this.state.firstname}
              onChange={this.handleChange}
              isInvalid={errors.firstname.length > 0}
            />
            <Form.Control.Feedback type="invalid">{errors.firstname}</Form.Control.Feedback>
          </Form.Group>

          <Form.Group controlId="lastname">
            <Form.Label>Last name</Form.Label>
            <Form.Control
              value={this.state.lastname}
              onChange={this.handleChange}
              isInvalid={errors.lastname.length > 0}
            />
            <Form.Control.Feedback type="invalid">{errors.lastname}</Form.Control.Feedback>
          </Form.Group>

          <Form.Group controlId="email">
            <Form.Label>Email</Form.Label>
            <Form.Control
              value={this.state.email}
              onChange={this.handleChange}
              type='email'
              isInvalid={errors.email.length > 0}
            />
            <Form.Control.Feedback type="invalid">{errors.email}</Form.Control.Feedback>
          </Form.Group>

          <Button
            block
            type="submit"
          >
            Sign up
          </Button>
        </form>
      </div>
    );
  }
}

SignUp.contextType = UserContext;