import React from "react";
import "./Login.css";
import LoginForm from "./LoginForm";

export class Login extends React.Component {
  constructor() {
    super();
    this.state = {
      email: "",
      password: ""
    }
    this.onFieldChange = this.onFieldChange.bind(this);
    this.onLogin = this.onLogin.bind(this);
  }

  onFieldChange(event) {
    event.preventDefault();
    this.setState({
      [event.target.name] : event.target.value
    });

    console.log("onFieldChange:", this.state);
  }

  onLogin(event) {
    event.preventDefault();
    this.login();
  }

  login() {
    console.log("Login", this.state);
  }

  render() {
    return (
      <LoginForm formState={this.state} onFieldChange={this.onFieldChange} onLogin={this.onLogin} />
    )
  }
}