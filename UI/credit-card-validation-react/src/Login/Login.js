import React from "react";
import "./Login.css";
import LoginForm from "./LoginForm";
import { connect } from "react-redux";
import { Constants } from "../Constants";
import { withRouter } from "react-router-dom";


class Login extends React.Component {
  constructor(props) {
    super(props);
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
    this.props.onLogin();
  }

  render() {
    return (
      <LoginForm formState={this.state} onFieldChange={this.onFieldChange} onLogin={this.onLogin} />
    )
  }
}

function mapStateToProps(state = {}) {
  return { }
}

function mapDispatchToProps(dispatch, props) {
  return {
    onLogin: () => {
      dispatch({ type: Constants.login.AUTHENTICATE});
      props.history.push("/");
    }
  }
}

export default withRouter((connect(mapStateToProps, mapDispatchToProps)(Login)));