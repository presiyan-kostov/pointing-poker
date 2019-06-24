import React from "react";
import { Nav, Navbar, Badge } from "react-bootstrap";
import { Link } from 'react-router-dom';
import { UserContext } from "./contexts/UserContext";
import "./Navigation.css";

export default function Navigation() {
    return (
        <UserContext.Consumer>
            {
                ({authenticatedUser, updateAuthenticatedUser, pushNewMessage}) => (
                    <Navbar>
                        {authenticatedUser &&
                        <Navbar.Brand>
                            <Link to="/">Home</Link>
                        </Navbar.Brand>}
                        {authenticatedUser &&
                        <Nav>
                            <Nav.Item>
                                <Link to="/projectlist">Projects</Link>
                            </Nav.Item>
                        </Nav>}
                        <Navbar.Toggle />
                        <Navbar.Collapse id="basic-navbar-nav" className="justify-content-end">
                            <Nav>
                                {!authenticatedUser &&
                                <>
                                    <Nav.Item>
                                        <Link className="nav-link" to="/signup">Sign up</Link>
                                    </Nav.Item>
                                    <Nav.Item>
                                        <Link className="nav-link" to="/login">Login</Link>
                                    </Nav.Item>
                                </>}
                                {authenticatedUser &&
                                <>
                                    <Navbar.Text>
                                        Logged in as: {authenticatedUser.firstname} {authenticatedUser.lastname} (<b>{authenticatedUser.username}</b>{authenticatedUser.isAdmin && <Badge variant="primary">admin</Badge>})
                                    </Navbar.Text>
                                </>}
                                {authenticatedUser &&
                                <>
                                    <Nav.Item>
                                        <Link className="nav-link" to="/login"
                                            onClick={() => {
                                                pushNewMessage({text: `Goodbye, ${authenticatedUser.firstname} ${authenticatedUser.lastname}! You have been successfully logged out.`, variant: 'success'}, true);
                                                updateAuthenticatedUser(null);
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