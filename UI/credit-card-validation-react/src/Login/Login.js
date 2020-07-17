import React from "react";
import { MDBContainer, MDBInput, MDBBtn } from 'mdbreact';
import "./Login.css";

export class Login extends React.Component {
  constructor() {
    super();
    this.state = {

    }
  }

  render() {
    return (
      <MDBContainer className="main-container">
        <form className="grey lighten-4">
          <p className="h5 text-center mb-4">Login</p>
          <div className="grey-text">
            <MDBInput label="Type your email" icon="envelope" group type="email" validate error="wrong"
              success="right" />
            <MDBInput label="Type your password" icon="lock" group type="password" validate />
          </div>
          <div className="text-center">
            <MDBBtn>Login</MDBBtn>
          </div>
        </form>
      </MDBContainer>
    )
  }
}