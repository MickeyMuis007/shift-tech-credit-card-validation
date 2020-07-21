
import React from "react";
import { Container, Table, Card } from "react-bootstrap";
import { MDBIcon } from "mdbreact";
import { IconButton } from "@material-ui/core";
import { Cached, ArrowDropDown } from "@material-ui/icons";
import MaterialPagination from "../Shared/MaterialPagination";
import DeleteModal from "../Shared/DeleteModal";
import objectPath from "object-path";

export function CreditCardTable({ props }) {
  let [sortCnt, setSortCnt] = React.useState(0);
  const [open, setOpen ] = React.useState(false);
  const [selectedRow, setSelectedRow ] = React.useState();
  
  function onReload(qry) {
    setSortCnt(0);
    props.onLoadCreditCard(qry);
  }

  const sortNo = {
    "1": "no",
    "0": "",
    "-1": "no desc"
  }

  const sortNoLabel = {
    "1": "Sorted No Asc",
    "0": "Default",
    "-1": "Sorted No Desc"
  }

  const onStatusSort = () => {
    const sortByCnt = sortCnt === 1 ? -1 : (1 + sortCnt);
    if (sortCnt === 1) setSortCnt(sortByCnt);
    else setSortCnt(sortByCnt);

    const qry = props.creditCard.pagination;
    qry["sort"] = sortNo[sortByCnt];
    onReload(qry);
  }

  const onDeleteClick = (selectedRow) => {
    setSelectedRow({
      id: selectedRow.Id,
      name: selectedRow.No
    });
    setOpen(true);
  }

  const onClose = () => {
    setOpen(false);
  }  

  const onDelete = (id) => {
    setOpen(false);
    props.onDeleteCreditCard(id);
  }

  const creditCards = objectPath.get(props, "creditCard.fetchAll.results.results", []);
  const creditCardStatuses = objectPath.get(props, "creditCardStatus.fetchAll.results.results", []);
  const creditCardProviders = objectPath.get(props, "creditCardProvider.fetchAll.results.results", []);
  const creditCardStatusDict = creditCardStatuses.reduce((acc, cur) => {
    if (acc && cur) {
      acc[cur.Id] = cur.Status;
    }
    return acc;
  }, {});
  const creditCardProviderDict = creditCardProviders.reduce((acc, cur) => {
    if (acc && cur) {
      acc[cur.Id] = cur.Code;
    }
    return acc;
  }, {});
  
  const pagination = <MaterialPagination props={{...props}} />;
  return (
    <Container>
      <DeleteModal open={open} onClose={onClose} selected={selectedRow} onDelete={onDelete}/>
      <Card className="main-card">
        <Card.Header as="h5" className="d-flex justify-content-between align-items-center">
          <span>Credit Card Status</span>
          <span className="pull-right">
            <IconButton aria-label="reload" title="Reload credit card statuses" onClick={() => onReload(props.creditCard.pagination)} style={{ outline: "none" }}>
              <Cached />
            </IconButton>
            {/* <Link to={"/admin/credit-card/edit"}>
              <Fab color="primary" size="small" aria-label="add" title="Add credit card status" style={{ outline: "none" }}>
                <AddIcon />
              </Fab>
            </Link> */}
          </span>
        </Card.Header>
        <Card.Body>
          <Table striped bordered hover variant="dark">
            <thead>
              <tr>
                <th></th>
                <th onClick={onStatusSort} title={sortNoLabel[sortCnt]}>No <ArrowDropDown className={sortCnt === -1 ? "sortDesc" : sortCnt === 1 ? "sortAsc" : ""} /></th>
                <th>Credit Card Status</th>
                <th>Credit Card Provider</th>
              </tr>
            </thead>
            <tbody>
              <CreditCardRows creditCards={creditCards} onDeleteClick={(onDeleteClick)} creditCardStatusDict={creditCardStatusDict} creditCardProviderDict={creditCardProviderDict} />
            </tbody>
          </Table>

          {pagination}
        </Card.Body>
      </Card>
    </Container>
  );
}

function CreditCardRow({ rowData, onDeleteClick, creditCardStatusDict, creditCardProviderDict }) {
  return (
    <tr>
      <td>
        {/* <Link to={"/admin/credit-card/edit/" + rowData.Id} className="text-light p-1" title="Edit"><MDBIcon icon="pencil-alt" /></Link> */}
        {/* <Link to={"/admin/credit-card/view/" + rowData.Id} className="text-light p-1" title="View"><MDBIcon icon="eye" /></Link> */}
        <span style={{cursor: "pointer"}} onClick={() => onDeleteClick(rowData)} className="text-light p-1" title="Delete"><MDBIcon icon="trash-alt" /></span>
      </td>
      <td>{rowData.No}</td>
      <td>{creditCardStatusDict[rowData.CreditCardStatusId]}</td>
      <td>{creditCardProviderDict[rowData.CreditCardProviderId]}</td>
    </tr>
  )
}

function CreditCardRows({ creditCards, onDeleteClick, creditCardStatusDict, creditCardProviderDict }) {
  return creditCards.map(t => <CreditCardRow key={t.Id} rowData={t} onDeleteClick={onDeleteClick} creditCardStatusDict={creditCardStatusDict} creditCardProviderDict={creditCardProviderDict}/>);
}