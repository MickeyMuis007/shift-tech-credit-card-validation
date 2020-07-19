
import React from "react";
import { Container, Table, Card } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { MDBIcon } from "mdbreact";
import { IconButton } from "@material-ui/core";
import { Cached } from "@material-ui/icons";
import MaterialPagination from "../Shared/MaterialPagination";

export function CreditCardStatusTable({ creditCardStatus, props }) {
  console.log("Credit card status:", props);
  function onReload(qry) {
    props.onLoadCreditCardState(qry);
  }

  const creditCardStatuses = creditCardStatus ? creditCardStatus.results : [];
  const metaData = creditCardStatus ? creditCardStatus.metaData : {};
  const pagination = Object.keys(metaData).length !== 0 ? <MaterialPagination reload={onReload}/> : null;
  return (
    <Container>
      <Card className="main-card">
        <Card.Header as="h5">Credit Card Status</Card.Header>
        <Card.Body>
          <IconButton aria-label="reload" title="reload" onClick={() => onReload(props.creditCardStatus.pagination)} style={{ outline: "none"}}>
            <Cached />
          </IconButton>
          <Table striped bordered hover variant="dark">
            <thead>
              <tr>
                <th></th>
                <th>Status</th>
                <th>Description</th>
              </tr>
            </thead>
            <tbody>
              <CreditCardStatusRows creditCardStatuses={creditCardStatuses} />
            </tbody>
          </Table>

          {pagination}
        </Card.Body>
      </Card>
    </Container>
  );
}

function CreditCardStatusRow({ rowData }) {
  return (
    <tr>
      <td>
        <LinkContainer to={"/admin/credit-card-status/edit/" + rowData.Id}><a href className="text-light p-1" title="Edit"><MDBIcon icon="pencil-alt" /></a></LinkContainer>
        <LinkContainer to={"/admin/credit-card-status/view/" + rowData.Id}><a href className="text-light p-1" title="View"><MDBIcon icon="eye" /></a></LinkContainer>
        <LinkContainer to={"/admin/credit-card-status/view/" + rowData.Id}><a href className="text-light p-1" title="Delete"><MDBIcon icon="trash-alt" /></a></LinkContainer>
      </td>
      <td>{rowData.Status}</td>
      <td>{rowData.Description}</td>
    </tr>
  )
}

function CreditCardStatusRows({ creditCardStatuses }) {
  return creditCardStatuses.map(t => <CreditCardStatusRow key={t.Id} rowData={t} />);
}