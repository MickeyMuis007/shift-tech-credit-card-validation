
import React from "react";
import { Container, Table, Card } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import { MDBIcon } from "mdbreact";
import { IconButton } from "@material-ui/core";
import { Fab } from "@material-ui/core";
import { Cached, ArrowDropDown } from "@material-ui/icons";
import AddIcon from "@material-ui/icons/Add";
import MaterialPagination from "../Shared/MaterialPagination";
import "./CreditCardStatusTable.css";

export function CreditCardStatusTable({ creditCardStatus, props }) {
  let [sortCnt, setSortCnt] = React.useState(0);
  function onReload(qry, isSort) {
    if (!isSort) qry["sort"] = sortStatus[sortCnt];
    props.onLoadCreditCardState(qry);
  }

  const sortStatus = {
    "1": "status",
    "0": "",
    "-1": "status desc"
  }

  const sortLabel = {
    "1": "Sorted Status Asc",
    "0": "Default",
    "-1": "Sorted Status Desc"
  }

  const onStatusSort = () => {
    const sortByCnt = sortCnt === 1 ? -1 : (1 + sortCnt);
    if (sortCnt === 1) setSortCnt(sortByCnt);
    else setSortCnt(sortByCnt);

    const qry = props.creditCardStatus.pagination;
    qry["sort"] = sortStatus[sortByCnt];
    onReload(qry, true);
  }

  const creditCardStatuses = creditCardStatus ? creditCardStatus.results : [];
  const metaData = creditCardStatus ? creditCardStatus.metaData : {};
  const pagination = Object.keys(metaData).length !== 0 ? <MaterialPagination reload={onReload} /> : null;
  return (
    <Container>
      <Card className="main-card">
        <Card.Header as="h5">Credit Card Status</Card.Header>
        <Card.Body>
          <IconButton aria-label="reload" title="Reload credit card statuses" onClick={() => onReload(props.creditCardStatus.pagination)} style={{ outline: "none" }}>
            <Cached />
          </IconButton>
          <Fab color="primary" size="small" aria-label="add" title="Add credit card status" style={{ outline: "none" }}>
            <AddIcon />
          </Fab>
          <Table striped bordered hover variant="dark">
            <thead>
              <tr>
                <th></th>
                <th onClick={onStatusSort} title={sortLabel[sortCnt]}>Status <ArrowDropDown className={sortCnt === -1 ? "sortDesc" : sortCnt === 1 ? "sortAsc" : ""} /></th>
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