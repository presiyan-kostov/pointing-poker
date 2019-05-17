import React, { Component } from "react";
import { FormGroup, FormControl, ControlLabel, Button } from "react-bootstrap";
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
      }
    };
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

      let formData = new FormData();
      formData.append('username', this.state.username);
      formData.append('password', this.state.password);
      formData.append('firstname', this.state.firstname);
      formData.append('lastname', this.state.lastname);
      formData.append('email', this.state.email);

      fetch(URL, {
          method: 'POST',
          body: formData
        })
        .then(response =>
          console.log(response));
    });
  }

  render() {
    let {errors} = this.state;

    return (
      <div className="SignUp">
        <form onSubmit={this.handleSubmit}>
          <FormGroup controlId="username" bsSize="large">
            <ControlLabel>Username</ControlLabel>
            <FormControl
              autoFocus
              value={this.state.username}
              onChange={this.handleChange}
            />
            {errors.username.length > 0 && <span className="error">{errors.username}</span>}
          </FormGroup>
          <FormGroup controlId="password" bsSize="large">
            <ControlLabel>Password</ControlLabel>
            <FormControl
              value={this.state.password}
              onChange={this.handleChange}
              type="password"
            />
            {errors.password.length > 0 && <span className="error">{errors.password}</span>}
          </FormGroup>
          <FormGroup controlId="firstname" bsSize="large">
            <ControlLabel>First name</ControlLabel>
            <FormControl
              value={this.state.firstname}
              onChange={this.handleChange}
            />
            {errors.firstname.length > 0 && <span className="error">{errors.firstname}</span>}
          </FormGroup>
          <FormGroup controlId="lastname" bsSize="large">
            <ControlLabel>Last name</ControlLabel>
            <FormControl
              value={this.state.lastname}
              onChange={this.handleChange}
            />
            {errors.lastname.length > 0 && <span className="error">{errors.lastname}</span>}
          </FormGroup>
          <FormGroup controlId="email" bsSize="large">
            <ControlLabel>Email</ControlLabel>
            <FormControl
              value={this.state.email}
              onChange={this.handleChange}
              type='email'
            />
            {errors.email.length > 0 && <span className="error">{errors.email}</span>}
          </FormGroup>
          <Button
            block
            bsSize="large"
            type="submit"
          >
            Sign up
          </Button>
        </form>
      </div>
    );
  }
}
