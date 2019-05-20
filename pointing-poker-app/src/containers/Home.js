import React, { Component } from "react";
import { UserContext } from "../contexts/UserContext";
import  { Redirect } from 'react-router-dom';
import "./Home.css";

export default class Home extends Component {
  render() {
    if (!this.context.authenticatedUserId){
      return (<Redirect to="/login"></Redirect>);
    }

    return (
      <div className="Home">
        <div className="lander">
          <h1>Home</h1>
          <p>Application for estimation of IT issues</p>
        </div>
      </div>
    );
  }
}

Home.contextType = UserContext;
