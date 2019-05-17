import React, { Component } from "react";
import { FormGroup, FormControl, ControlLabel, Button } from "react-bootstrap";
import "./Login.css";

export default class Login extends Component {
  constructor(props) {
    super(props);

    this.state = {
      username: '',
      password: '',
      errors: {
        username: '',
        password: ''
      }
    };
  }

  validateForm = (state) => {
    let { username, password } = state;

    let errors = {};

    if (!username){
      errors.username = 'Please enter the username.';
    }
    else{
      errors.username = '';
    }

    if (!password){
      errors.password = 'Please enter the password.';
    }
    else{
      errors.password = '';
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
  
      const URL = `http://localhost:62973/api/Authentication/login?username=${state.username}&password=${state.password}`;
  
      fetch(URL)
      .then(response => {
        console.log(response.status);
      });
    });
  }

  render() {
    let {errors} = this.state;

    return (
      <div className="Login">
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
          <Button
            block
            bsSize="large"
            type="submit"
          >
            Login
          </Button>
        </form>
      </div>
    );
  }
}
