import React, { Component } from "react";
import  { Redirect } from 'react-router-dom';
import { UserContext } from "../contexts/UserContext";
import { Card, CardColumns, Button } from "react-bootstrap";
import "./ProjectList.css";

export default class ProjectList extends Component {
    constructor(props) {
      super(props);

      this.state = {
          projects: []
      }
    }

    componentDidMount(){
        if (!this.context.authenticatedUser){
            return;
        }

        const URL = `http://localhost:62973/api/User/${this.context.authenticatedUser.id}/projects`;
  
        fetch(URL)
        .then(response => {
            if (response.status == 200){
                response.json().then(response => {
                        this.setState({ projects: response});
                    });
            }else{
                this.context.pushNewMessage({text: 'Something has gone wrong!', variant: 'danger'}, true);
            }
        })
        .catch(err => this.context.pushNewMessage({text: err, variant: 'danger'}, true));
    }

    render(){
        if (!this.context.authenticatedUser){
            return (<Redirect to="/login"></Redirect>);
        }

        let projects = this.state.projects.map(p => (
            <Card key={p.id} style={{ width: '18rem' }} bg="light">
                <Card.Body>
                    <Card.Title>{p.code}</Card.Title>
                    <Card.Link href={p.youTrackUrl}>YouTrack URL</Card.Link>
                    <Card.Text>{p.youTrackQuery}</Card.Text>
                    <Button variant="primary" size="sm" block>Open</Button>
                </Card.Body>
            </Card>
        ));

        return (
            <div>
                <h1>Projects</h1>
                <CardColumns>
                    {projects}
                </CardColumns>
                {this.context.authenticatedUser.isAdmin && <Button variant="primary" size="lg" className="float-right">New project</Button>}
            </div>
        );
    }
}

ProjectList.contextType = UserContext;