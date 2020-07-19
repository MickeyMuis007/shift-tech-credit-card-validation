import React from "react";
import { TablePagination } from "@material-ui/core";
import { connect } from "react-redux";
import { Constants } from "../../Constants";

export const PageConstants = {
  FIRST: "FIRST",
  PREVIOUS: "PREVIOUS",
  CURRENT: "CURRENT",
  NEXT: "NEXT",
  LAST: "LAST"
}

const MaterialPagination = ({ pagination, setPagination, metaData, reload }) => {
  console.log("Material Props:", reload);
  let [totalCount] = React.useState(metaData.totalCount);
  let [page, setPage] = React.useState(pagination.pageNumber - 1);
  let [rowsPerPage, setRowsPerPage] = React.useState(pagination.pageSize);

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
    const qry = {
      pageNumber: newPage + 1,
      pageSize: pagination.pageSize
    };
    setPagination(qry);
    reload(qry);
  };

  const handleChangeRowsPerPage1 = (event) => {
    const pageSize = event.target.value;
    setRowsPerPage(event.target.value);
    const qry = {
      pageNumber: pagination.pageNumber,
      pageSize: pageSize
    };
    setPagination(qry);
    reload(qry);
  };

  return (
    <TablePagination
      component="div"
      count={totalCount}
      page={page}
      onChangePage={handleChangePage}
      rowsPerPage={rowsPerPage}
      onChangeRowsPerPage={handleChangeRowsPerPage1}
      rowsPerPageOptions={[5, 10, 25, 50, 100]}
    />
  )
}

function mapToStateProp(state) {
  return {
    pagination: {...state.creditCardStatus.pagination},
    metaData: {...state.creditCardStatus.fetchAll.results.metaData}
  }
}

function mapDispatchToProps(dispatch) {
  return {
    setPagination: (pagination) => {
      dispatch({ type: Constants.creditCardStatus.SET_GRID_PAGINATION, data: pagination});
    }
  }
}

export default connect(mapToStateProp, mapDispatchToProps)(MaterialPagination);

