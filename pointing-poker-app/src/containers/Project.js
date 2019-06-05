import React, { Component } from "react";
import  { Redirect } from 'react-router-dom';

export default class Project extends Component {
    constructor(props){
        super(props);

        this.state = {
            code: '',
            youTrackUrl: '',
            youTrackQuery: '',
            errors: {
                code: '',
                youTrackUrl: '',
                youTrackQuery: ''
            }
        };
    }

    render(){
        if (!this.context.authenticatedUser){
            return (<Redirect to="/login"></Redirect>);
        }

        return (
            <div>
              <h1>{this.props.id ? `Project ${this.state.code}` : 'New project'}</h1>
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