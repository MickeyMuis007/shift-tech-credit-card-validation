import React from "react";
import { TablePagination } from "@material-ui/core";
import { connect } from "react-redux";
import { Constants } from "../../Constants";
import objectPath from "object-path";

class MaterialPagination extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
   
    }

    this.handleChangePage = this.handleChangePage.bind(this);
    this.handleChangeRowsPerPage = this.handleChangeRowsPerPage.bind(this);
  }

  handleChangePage = (event, newPage) => {
    const qry = {
      pageNumber: newPage + 1,
      pageSize: this.props.pagination.pageSize
    };
    this.props.reload(qry);

    this.props.setPagination({
      ...this.props.pagination,
      pageNumber: newPage + 1
    });
  };

  handleChangeRowsPerPage = (event) => {
    const pageSize = event.target.value;
    const qry = {
      pageNumber: this.props.pagination.pageNumber,
      pageSize: pageSize
    };
    this.props.reload(qry);

    this.props.setPagination({
      ...this.props.pagination,
      pageSize: pageSize
    });
  };

  render() {
    return (
      <TablePagination
        component="div"
        count={objectPath.get(this.props, "pagination.totalCount", 0)}
        page={objectPath.get(this.props, "pagination.pageNumber", 1) - 1}
        onChangePage={this.handleChangePage}
        rowsPerPage={objectPath.get(this.props, "pagination.pageSize", 10)}
        onChangeRowsPerPage={this.handleChangeRowsPerPage}
        rowsPerPageOptions={[5, 10, 25, 50, 100]}
      />
    )
  }
}


function mapToStateProp(state) {
  return {
    pagination: objectPath.get(state, "creditCardStatus.pagination")
  }
}

function mapDispatchToProps(dispatch) {
  return {
    setPagination: (pagination) => {
      dispatch({ type: Constants.creditCardStatus.SET_GRID_PAGINATION, data: pagination });
    }
  }
}

export default connect(mapToStateProp, mapDispatchToProps)(MaterialPagination);

