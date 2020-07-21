import React from "react";
import { TablePagination } from "@material-ui/core";
import objectPath from "object-path";

function MaterialPagination({props}) {
  const handleChangePage = (event, newPage) => {
    const qry = {
      pageNumber: newPage + 1,
      pageSize: props.pagination.pageSize
    };
    props.onReload(qry);

    props.setPagination({
      ...props.pagination,
      pageNumber: newPage + 1
    });
  };

  const handleChangeRowsPerPage = (event) => {
    const pageSize = event.target.value;
    const qry = {
      pageNumber: props.pagination.pageNumber,
      pageSize: pageSize
    };
    props.onReload(qry);

    props.setPagination({
      ...props.pagination,
      pageSize: pageSize
    });
  };

  return (
    <TablePagination
      component="div"
      count={objectPath.get(props, "pagination.totalCount", 0)}
      page={objectPath.get(props, "pagination.pageNumber", 1) - 1}
      onChangePage={handleChangePage}
      rowsPerPage={objectPath.get(props, "pagination.pageSize", 10)}
      onChangeRowsPerPage={handleChangeRowsPerPage}
      rowsPerPageOptions={[5, 10, 25, 50, 100]}
    />
  )
}

export default MaterialPagination;

