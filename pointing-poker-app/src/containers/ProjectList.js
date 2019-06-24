import React, { Component } from "react";
import  { Redirect } from 'react-router-dom';
import { UserContext } from "../contexts/UserContext";
import { Card, CardColumns, Button } from "react-bootstrap";
import "./ProjectList.css";

export default class ProjectList extends Component {
    constructor(props) {
      super(props);

      this.state = {
          projects: [],
          selectedProjectId: null
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

        if (this.state.selectedProjectId == -1){
            return (<Redirect to={{pathname: '/project'}}></Redirect>);
        }

        if (this.state.selectedProjectId){
            return (<Redirect to={{pathname: '/project', id: this.state.selectedProjectId}}></Redirect>);
        }

        let projects = this.state.projects.map(p => (
            <Card key={p.id} style={{ width: '18rem' }} bg="light">
                <Card.Body>
                    <Card.Title>{p.code}</Card.Title>
                    <Card.Link href={p.youTrackUrl}>YouTrack URL</Card.Link>
                    <Card.Text>{p.youTrackQuery}</Card.Text>
                    {this.context.authenticatedUser.isAdmin && <Button variant="primary" size="sm" block onClick={() => this.setState({ selectedProjectId: p.id})}>Open</Button>}
                </Card.Body>
            </Card>
        ));

        return (
            <div>
                <h1>Projects</h1>
                <CardColumns>
                    {projects}
                </CardColumns>
                {this.context.authenticatedUser.isAdmin && <Button variant="primary" size="lg" className="float-right" onClick={() => this.setState({ selectedProjectId: -1})}>New project</Button>}
            </div>
        );
    }
}

ProjectList.contextType = UserContext;