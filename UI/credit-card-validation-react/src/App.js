import React from 'react';
import { BrowserRouter, Route } from "react-router-dom";
import { Container } from "react-bootstrap";
import './App.css';
import { MainNavBar } from "./Navbar/MainNavbar";
import { CreditCard, CreditCardProvider, CreditCardStatus, CreditCardStatusEdit, CreditCardProviderEdit } from "./Admin";
import { SpeedDailer, TestFormSchema } from "./RND";
import Login from "./Login/Login";

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
          <Route exact path="/rnd/test-form-schema" component={TestFormSchema} />
          <Route exact path="/rnd/speed-dailer" component={SpeedDailer} />
          <Route exact path="/test" component={Test} />
          <Route exact path="/login" component={Login} />
          <Route exact path="/admin/credit-card" component={CreditCard} />
          <Route exact path="/admin/credit-card-status" component={CreditCardStatus} />
          <Route exact path="/admin/credit-card-status/edit/:id" component={CreditCardStatusEdit} />
          <Route exact path="/admin/credit-card-status/edit" component={CreditCardStatusEdit} />
          <Route exact path="/admin/credit-card-provider" component={CreditCardProvider} />
          <Route exact path="/admin/credit-card-provider/edit/:id" component={CreditCardProviderEdit} />
          <Route exact path="/admin/credit-card-provider/edit" component={CreditCardProviderEdit} />
        </Container>
      </React.Fragment>
    </BrowserRouter>
  );
}

export default App;
