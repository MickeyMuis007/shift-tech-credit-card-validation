import React from 'react';
import { BrowserRouter, Route } from "react-router-dom";
import { Container } from "react-bootstrap";
import './App.css';
import { MainNavBar } from "./Navbar/MainNavbar";
import { TestFormSchema, CreditCard, CreditCardProvider, CreditCardStatus } from "./Admin";
import { Login } from "./Login/Login";

function Test() {
  return (
    <Container>
      <h1>Hello World</h1>
    </Container>
  )
}

function App() {
  return (
    <BrowserRouter>
      <React.Fragment>
        <MainNavBar />
        <Container className="mt-2">
          <Route exact path="/admin/test-form-schema" component={TestFormSchema} />
          <Route exact path="/test" component={Test} />
          <Route exact path="/login" component={Login} />
          <Route exact path="/admin/credit-card" component={CreditCard} />
          <Route exact path="/admin/credit-card-status" component={CreditCardStatus} />
          <Route exact path="/admin/credit-card-provider" component={CreditCardProvider} />
        </Container>
      </React.Fragment>
    </BrowserRouter>
  );
}

export default App;
