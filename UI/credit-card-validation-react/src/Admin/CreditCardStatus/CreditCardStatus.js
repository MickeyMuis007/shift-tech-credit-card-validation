import React from "react";
import { connect } from "react-redux";
import { Constants } from "../../Constants";
import objectPath from "object-path";
import { CreditCardStatusTable } from "./CreditCardStatusTable";
import { LinearProgress } from "@material-ui/core"

class CreditCardStatus extends React.Component {
  constructor(props) {
    super(props);
    this.state = {

    }
  }

  componentDidMount() {
    this.props.onLoadCreditCardState();
  }

  render() {
    let fetchAll;
    let loading;
    if (objectPath.has(this.props, "creditCardStatus.fetchAll")) {
      fetchAll = this.props.creditCardStatus.fetchAll;
      loading = fetchAll.isFetching ? <LinearProgress /> : null;
    }


    return (
      <React.Fragment>
        {loading}
        <CreditCardStatusTable props={{...this.props}} />
      </React.Fragment>
    )
  }
}

function mapStateToProps(state) {
  return {
    creditCardStatus: { ...state.creditCardStatus },
    pagination: objectPath.get(state, "creditCardStatus.pagination")
  }
}

function mapDispatchToProps(dispatch) {
  const restUrl = `${Constants.api.HOST}/creditCardStatus`;
  const loadCreditCardStatus = (qry) => {
    dispatch({ type: Constants.creditCardStatus.FETCH_ALL_REQUEST });
    try {
      let sort = qry && qry.sort ? `&OrderBy=${qry.sort}` : "";
      const pageSize = qry && qry.pageSize ? qry.pageSize : 10;
      const pageNumber = qry && qry.pageNumber ? qry.pageNumber : 1;
      fetch(`${restUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}${sort}`)
        .then(res => res.json())
        .then(res => {
          dispatch({ type: Constants.creditCardStatus.FETCH_ALL_SUCCESS, data: res });
          dispatch({
            type: Constants.creditCardStatus.SET_GRID_PAGINATION, data: {
              pageSize: res.metaData.pageSize,
              pageNumber: res.metaData.currentPage,
              totalCount: res.metaData.totalCount
            }
          })

        });
    } catch (err) {
      dispatch({ type: Constants.creditCardStatus.FETCH_ALL_FAILURE, data: err });
    }
  }

  const paginationInit = {
    pageNumber: 1, 
    pageSize: 10
  };
  return {
    onReload: (qry) => {
      loadCreditCardStatus(qry)
    },
    onLoadCreditCardState: (qry) => {
      loadCreditCardStatus(qry)
    },
    onDeleteCreditCardStatus: (id) => {
      dispatch({ type: Constants.creditCardStatus.DELETE_SINGLE_REQUEST });
      try {
        fetch(`${restUrl}/${id}`, { method: 'DELETE' })
          .then((res) => res.status)
          .then((res) => {
            dispatch({ type: Constants.creditCardStatus.DELETE_SINGLE_SUCCESS, data: res });
            loadCreditCardStatus(paginationInit);
          }).catch((err) => {
            dispatch({ type: Constants.creditCardStatus.DELETE_SINGLE_FAILURE, data: err });
          })
      } catch (err) {
        dispatch({ type: Constants.creditCardStatus.DELETE_SINGLE_FAILURE, data: err });
      }
    },
    setPagination: (pagination) => {
      dispatch({ type: Constants.creditCardStatus.SET_GRID_PAGINATION, data: pagination });
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(CreditCardStatus);