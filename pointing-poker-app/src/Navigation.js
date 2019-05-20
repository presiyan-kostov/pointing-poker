import React from "react";
import { Nav, Navbar } from "react-bootstrap";
import { Link } from 'react-router-dom';
import { UserContext } from "./contexts/UserContext";

export default function Navigation() {
    return (
        <UserContext.Consumer>
            {
                ({authenticatedUserId, updateAuthenticatedUserId, pushNewMessage}) => (
                    <Navbar>
                        {authenticatedUserId &&
                        <Navbar.Brand>
                            <Link to="/">Home</Link>
                        </Navbar.Brand>}
                        <Navbar.Toggle />
                        <Navbar.Collapse id="basic-navbar-nav">
                            <Nav className="justify-content-end">
                                {!authenticatedUserId &&
                                <>
                                    <Nav.Item>
                                        <Link className="nav-link" to="/signup">Sign up</Link>
                                    </Nav.Item>
                                    <Nav.Item>
                                        <Link className="nav-link" to="/login">Login</Link>
                                    </Nav.Item>
                                </>}
                                {authenticatedUserId &&
                                <>
                                    <Nav.Item>
                                        <Link className="nav-link" to="/login"
                                            onClick={() => {
                                                updateAuthenticatedUserId(null);
                                                pushNewMessage({text: 'You have been successfully logged out.', variant: 'success'}, true);
                                                }}>
                                            Log out
                                        </Link>
                                    </Nav.Item>
                                </>
                                }
                            </Nav>
                        </Navbar.Collapse>
                    </Navbar>
                )
            }
          </UserContext.Consumer>
    );
}