import React, { Component } from "react";
import  { Redirect } from 'react-router-dom';
import { Form } from "react-bootstrap";
import { Button } from "react-bootstrap";
import { UserContext } from "../contexts/UserContext";
import "./Project.css";

export default class Project extends Component {
    constructor(props){
        super(props);

        this.state = {
            id: null,
            code: '',
            youTrackUrl: '',
            youTrackQuery: '',
            errors: {
                code: '',
                youTrackUrl: '',
                youTrackQuery: ''
            },
            isSuccess: false
        };

        this.validationErrorTypes ={
          CodeMissing: 0,
          CodeExists: 1,
          CodeToLong: 2,
          YouTrackUrlMissing: 3,
          YouTrackUrlToLong: 4,
          YouTrackQueryMissing: 5,
          YouTrackQueryToLong: 6
        };
    
        Object.freeze(this.validationErrorTypes);
    }

    componentDidMount(){
      if (!this.context.authenticatedUser || !this.props.location.id){
          return;
      }

      const URL = `http://localhost:62973/api/Project/${this.props.location.id}`;

      fetch(URL)
      .then(response => {
          if (response.status == 200){
              response.json().then(response => {
                    if (!response){
                      return;
                    }

                    this.setState({
                      id: response.id,
                      code: response.code,
                      youTrackUrl: response.youTrackUrl,
                      youTrackQuery: response.youTrackQuery
                    });
                  });
          }else{
              this.context.pushNewMessage({text: 'Something has gone wrong!', variant: 'danger'}, true);
          }
      })
      .catch(err => this.context.pushNewMessage({text: err, variant: 'danger'}, true));
    }

    validateForm() {
      let errors = {};
  
      let { code, youTrackUrl, youTrackQuery } = this.state;
  
      if (!code){
        errors.code = 'Please enter the code.';
      }
      else{
        errors.code = '';
      }

      if (!youTrackUrl){
        errors.youTrackUrl = 'Please enter the YouTrack URL.';
      }
      else{
        errors.youTrackUrl = '';
      }
  
      if (!youTrackQuery){
        errors.youTrackQuery = 'Please enter the YouTrack Query.';
      }
      else{
        errors.youTrackQuery = '';
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
    
        const URL = 'http://localhost:62973/api/Project/save';
  
        let data = {
          id: this.state.id,
          code: this.state.code,
          youTrackUrl: this.state.youTrackUrl,
          youTrackQuery: this.state.youTrackQuery
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
                this.context.pushNewMessage({text: `The project "${response.code}" has been saved successfully.`, variant: 'success'}, true);
                this.setState({isSuccess: true});
              });
            }else{
              response.json().then(response => {
                let errors = {};
  
                if (response.some(x => x == this.validationErrorTypes.CodeMissing)){
                  errors.code = 'Please enter the code.';
                }else if (response.some(x => x == this.validationErrorTypes.CodeExists)){
                  errors.code = 'This code is already taken. Please choose another one.';
                }else if (response.some(x => x == this.validationErrorTypes.CodeToLong)){
                  errors.code = 'The entered code is too long.';
                }else{
                  errors.code = '';
                }
  
                if (response.some(x => x == this.validationErrorTypes.YouTrackUrlMissing)){
                  errors.youTrackUrl = 'Please enter the YouTrack URL.';
                }else if (response.some(x => x == this.validationErrorTypes.YouTrackUrlToLong)){
                  errors.youTrackUrl = 'The YouTrack URL is too long.';
                }else{
                  errors.youTrackUrl = '';
                }

                if (response.some(x => x == this.validationErrorTypes.YouTrackQueryMissing)){
                  errors.youTrackQuery = 'Please enter the YouTrack Query.';
                }else if (response.some(x => x == this.validationErrorTypes.YouTrackQueryToLong)){
                  errors.youTrackQuery = 'The YouTrack Query is too long.';
                }else{
                  errors.youTrackQuery = '';
                }
  
                this.setState({errors: errors});
              });
            }
          });
      });
    }

    render(){
        if (!this.context.authenticatedUser){
            return (<Redirect to="/login"></Redirect>);
        }

        if (this.state.isSuccess){
          return (<Redirect to="/projectlist"></Redirect>); 
        }

        let {errors} = this.state;

        return (
            <div>
              <h1>{this.state.id ? (<span>Project <b>{this.state.code}</b></span>) : 'New project'}</h1>
              <form onSubmit={this.handleSubmit}>
      
                <Form.Group controlId="code">
                  <Form.Label>Code</Form.Label>
                  <Form.Control
                    autoFocus
                    value={this.state.code}
                    onChange={this.handleChange}
                    isInvalid={errors.code.length > 0}
                  />
                  <Form.Control.Feedback type="invalid">{errors.code}</Form.Control.Feedback>
                </Form.Group>
      
                <Form.Group controlId="youTrackUrl">
                  <Form.Label>YouTrack URL</Form.Label>
                  <Form.Control
                    value={this.state.youTrackUrl}
                    onChange={this.handleChange}
                    isInvalid={errors.youTrackUrl.length > 0}
                    disabled={!this.context.authenticatedUser.isAdmin}
                  />
                  <Form.Control.Feedback type="invalid">{errors.youTrackUrl}</Form.Control.Feedback>
                </Form.Group>
      
                <Form.Group controlId="youTrackQuery">
                  <Form.Label>YouTrack Query</Form.Label>
                  <Form.Control
                    value={this.state.youTrackQuery}
                    onChange={this.handleChange}
                    isInvalid={errors.youTrackQuery.length > 0}
                  />
                  <Form.Control.Feedback type="invalid">{errors.youTrackQuery}</Form.Control.Feedback>
                </Form.Group>
      
                <Button
                  block
                  type="submit"
                >
                  Save
                </Button>
              </form>
            </div>
          );
    }
}

Project.contextType = UserContext;