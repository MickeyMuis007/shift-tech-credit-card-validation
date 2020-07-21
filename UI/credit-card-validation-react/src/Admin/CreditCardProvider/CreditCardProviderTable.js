import React from "react";
import { Container, Table, Card } from "react-bootstrap";
import { MDBIcon } from "mdbreact";
import { IconButton } from "@material-ui/core";
import { Fab } from "@material-ui/core";
import { Cached, ArrowDropDown } from "@material-ui/icons";
import AddIcon from "@material-ui/icons/Add";
import MaterialPagination from "../Shared/MaterialPagination";
import { Link } from "react-router-dom";
import DeleteModal from "../Shared/DeleteModal";
import objectPath from "object-path";

export function CreditCardProviderTable({ props }) {
  let [sortNameCnt, setSortNameCnt] = React.useState(0);
  let [sortCodeCnt, setSortCodeCnt] = React.useState(0);
  const [open, setOpen ] = React.useState(false);
  const [selectedRow, setSelectedRow ] = React.useState();
  
  function onReload(qry) {
    props.onLoadCreditCardProvider(qry);
  }

  const sortName = {
    "1": "name",
    "0": "",
    "-1": "name desc"
  }

  const sortCode = {
    "1": "code",
    "0": "",
    "-1": "code desc"
  }

  const sortLabelName = {
    "1": "Sorted Name Asc",
    "0": "Default",
    "-1": "Sorted Name Desc"
  }

  const sortLabelCode = {
    "1": "Sorted Code Asc",
    "0": "Default",
    "-1": "Sorted Code Desc"
  }

  const onNameSort = () => {
    setSortCodeCnt(0);
    const sortByCnt = sortNameCnt === 1 ? -1 : (1 + sortNameCnt);
    if (sortNameCnt === 1) setSortNameCnt(sortByCnt);
    else setSortNameCnt(sortByCnt);

    const qry = props.creditCardProvider.pagination;
    qry["sort"] = sortName[sortByCnt];
    onReload(qry);
  }

  const onCodeSort = () => {
    setSortNameCnt(0);
    const sortByCnt = sortCodeCnt === 1 ? -1 : (1 + sortCodeCnt);
    if (sortCodeCnt === 1) setSortCodeCnt(sortByCnt);
    else setSortCodeCnt(sortByCnt);

    const qry = props.creditCardProvider.pagination;
    qry["sort"] = sortCode[sortByCnt];
    onReload(qry);
  }

  const onDeleteClick = (selectedRow) => {
    setSelectedRow({
      id: selectedRow.Id,
      name: selectedRow.Name
    });
    setOpen(true);
  }

  const onClose = () => {
    setOpen(false);
  }  

  const onDelete = (id) => {
    setOpen(false);
    props.onDeleteCreditCardProvider(id);
  }

  const creditCardProviders = objectPath.get(props, "creditCardProvider.fetchAll.results.results", []);
  const pagination = <MaterialPagination props={{...props}} />;
  return (
    <Container>
      <DeleteModal open={open} onClose={onClose} selected={selectedRow} onDelete={onDelete}/>
      <Card className="main-card">
        <Card.Header as="h5" className="d-flex justify-content-between align-items-center">
          <span>Credit Card Provider</span>
          <span className="pull-right">
            <IconButton aria-label="reload" title="Reload credit card statuses" onClick={() => onReload(props.creditCardProvider.pagination)} style={{ outline: "none" }}>
              <Cached />
            </IconButton>
            <Link to={"/admin/credit-card-provider/edit"}>
              <Fab color="primary" size="small" aria-label="add" title="Add credit card status" style={{ outline: "none" }}>
                <AddIcon />
              </Fab>
            </Link>
          </span>
        </Card.Header>
        <Card.Body>
          <Table striped bordered hover variant="dark">
            <thead>
              <tr>
                <th></th>
                <th onClick={onNameSort} title={sortLabelName[sortNameCnt]}>Name <ArrowDropDown className={sortNameCnt === -1 ? "sortDesc" : sortNameCnt === 1 ? "sortAsc" : ""} /></th>
                <th onClick={onCodeSort} title={sortLabelCode[sortCodeCnt]}>Code <ArrowDropDown className={sortCodeCnt === -1 ? "sortDesc" : sortCodeCnt === 1 ? "sortAsc" : ""} /></th>
                <th>StartsWith</th>
                <th>Length</th>
              </tr>
            </thead>
            <tbody>
              <CreditCardProviderRows creditCardProviders={creditCardProviders} onDeleteClick={(onDeleteClick)} />
            </tbody>
          </Table>

          {pagination}
        </Card.Body>
      </Card>
    </Container>
  );
}

function CreditCardProviderRow({ rowData, onDeleteClick }) {
  return (
    <tr>
      <td>
        <Link to={"/admin/credit-card-provider/edit/" + rowData.Id} className="text-light p-1" title="Edit"><MDBIcon icon="pencil-alt" /></Link>
        {/* <Link to={"/admin/credit-card-provider/view/" + rowData.Id} className="text-light p-1" title="View"><MDBIcon icon="eye" /></Link> */}
        <span style={{cursor: "pointer"}} onClick={() => onDeleteClick(rowData)} className="text-light p-1" title="Delete"><MDBIcon icon="trash-alt" /></span>
      </td>
      <td>{rowData.Name}</td>
      <td>{rowData.Code}</td>
      <td>{rowData.StartsWith}</td>
      <td>{rowData.Length}</td>
    </tr>
  )
}

function CreditCardProviderRows({ creditCardProviders, onDeleteClick }) {
  return creditCardProviders.map(t => <CreditCardProviderRow key={t.Id} rowData={t} onDeleteClick={onDeleteClick} />);
}