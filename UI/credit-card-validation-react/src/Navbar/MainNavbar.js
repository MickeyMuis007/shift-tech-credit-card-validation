import React from "react";
import { Navbar, Nav, NavDropdown } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";


export function MainNavBar() {
  return (
    <Navbar bg="dark" expand="lg" variant="dark">
      <Navbar.Brand href="#home">Credit Card Validator</Navbar.Brand>
      <Navbar.Toggle aria-controls="basic-navbar-nav" />
      <Navbar.Collapse id="basic-navbar-nav">
        <Nav className="mr-auto">
          <Nav.Link href="#home">Home</Nav.Link>
          <NavDropdown title="Admin" id="basic-nav-dropdown">
            <LinkContainer to={"/admin/credit-card"}><NavDropdown.Item>Credit Cards</NavDropdown.Item></LinkContainer>
            <LinkContainer to={"/admin/credit-card-provider"}><NavDropdown.Item>Credit Card Providers</NavDropdown.Item></LinkContainer>
            <LinkContainer to={"/admin/credit-card-status"}><NavDropdown.Item>Credit Card Statuses</NavDropdown.Item></LinkContainer>
          </NavDropdown>
          <NavDropdown title="RND">
            <LinkContainer to={"/rnd/test-form-schema"}><NavDropdown.Item>Test Form Schema</NavDropdown.Item></LinkContainer>
            <LinkContainer to={"/rnd/speed-dailer"}><NavDropdown.Item>Speed Dailer</NavDropdown.Item></LinkContainer>
            <LinkContainer to={"/test"}><NavDropdown.Item>Test</NavDropdown.Item></LinkContainer>
          </NavDropdown>
        </Nav>
        <Nav>
          <LinkContainer to={"/login"}>
            <Nav.Link href="#link">Login</Nav.Link>
          </LinkContainer>
        </Nav>
      </Navbar.Collapse>
    </Navbar>
  )
}