import React, { Component } from "react";
import "./App.css";
import Navigation from "./Navigation";
import Messages from "./Messages";
import Routes from "./Routes";
import { UserContext } from "./contexts/UserContext";

class App extends Component {
  constructor(props){
    super(props);

    this.state = {
      authenticatedUser: '',
      updateAuthenticatedUser: this.updateAuthenticatedUser,

      messages: [],
      pushNewMessage: this.pushNewMessage,
      removeMessageAt: this.removeMessageAt,
      clearMessages: this.clearMessages
    }
  }

  updateAuthenticatedUser = newAuthenticatedUser => {
    this.setState({ authenticatedUser: newAuthenticatedUser});
  }

  pushNewMessage = (message, clear) => {
    let messages = this.state.messages;

    if (clear){
      messages = [];
    }

    this.setState({messages: [...messages, message]})
  }

  removeMessageAt = (index) => {
    let messages = this.state.messages.filter((_m, i) => i != index);
    this.setState({messages: messages});
  }

  clearMessages = () => {
    this.setState({messages: []});
  }

  render() {
    return (
      <UserContext.Provider value={this.state}>
        <div className="App container">
          <Navigation />
          <Messages />
          <Routes />
        </div>
      </UserContext.Provider>
    );
  }
}

export default App;
