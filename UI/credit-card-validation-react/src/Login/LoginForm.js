import React from "react";
import { MDBContainer, MDBInput } from "mdbreact";

function LoginForm ({formState, onFieldChange, onLogin}) {
  return (
    <MDBContainer className="main-container">
        <form className="grey lighten-4" onSubmit={onLogin}>
          <p className="h5 text-center mb-4">Login</p>
          <div className="grey-text">
            <MDBInput label="Type your email" icon="envelope" group type="email" name="email" value={formState.email} onChange={onFieldChange} validate error="wrong"
              success="right" />
            <MDBInput label="Type your password" icon="lock" group type="password" name="password" value={formState.password} onChange={onFieldChange} validate />
          </div>
          <div className="text-center">
            <input type="submit" value="Login" className="btn btn-success"/>
          </div>
        </form>
      </MDBContainer>
  )
}

export default LoginForm;
