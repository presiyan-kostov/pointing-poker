import React from "react";
import { UserContext } from "./contexts/UserContext";
import { Alert } from "react-bootstrap";

export default class Messages extends React.Component{
    render(){
        return (
            <UserContext.Consumer>
                {
                    ({messages, removeMessageAt}) => (
                        <>
                            {messages.map((message, index) =>
                                (
                                    <Alert key={index} variant={message.variant} dismissible onClose={(index) => removeMessageAt(index)}>
                                        {message.text}
                                    </Alert>
                                ))
                            }
                        </>
                    )
                }
            </UserContext.Consumer>
        );
    }
}

Messages.contextType = UserContext;
