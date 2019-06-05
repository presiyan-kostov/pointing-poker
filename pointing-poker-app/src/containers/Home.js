import React, { Component } from "react";
import { UserContext } from "../contexts/UserContext";
import  { Redirect } from 'react-router-dom';
import "./Home.css";

export default class Home extends Component {
  render() {
    if (!this.context.authenticatedUser){
      return (<Redirect to="/login"></Redirect>);
    }

    return (
      <div>
        <h1>Home</h1>
        <p>Application for estimation of IT issues</p>
      </div>
    );
  }
}

Home.contextType = UserContext;
