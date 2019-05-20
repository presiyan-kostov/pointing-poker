import React, { Component } from "react";
import { Form } from "react-bootstrap";
import { Button } from "react-bootstrap";
import { UserContext } from "../contexts/UserContext";
import  { Redirect } from 'react-router-dom';
import "./Login.css";

export default class Login extends Component {
  constructor(props) {
    super(props);

    this.state = {
      username: '',
      password: '',
      authenticatedUserId: null,
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
        if (response.status == 200){
          response.json().then(response => {
            this.context.updateAuthenticatedUserId(response.id);
            this.context.pushNewMessage({text: `Hello, Mr/Mrs ${response.firstname} ${response.lastname}! You have been successfully logged in.`, variant: 'success'}, true);
          });
        }else{
          this.context.pushNewMessage({text: 'Your username or password is incorrect!', variant: 'danger'}, true);
        }
      })
      .catch(err => this.context.pushNewMessage({text: err, variant: 'danger'}, true));
    });
  }

  render() {
    if (this.context.authenticatedUserId){
      return (<Redirect to="/home"></Redirect>);
    }

    let {errors} = this.state;

    return (
      <div className="Login">
        <Form onSubmit={this.handleSubmit}>

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

          <Button
            block
            type="submit"
          >
            Login
          </Button>
        </Form>
      </div>
    );
  }
}

Login.contextType = UserContext;
