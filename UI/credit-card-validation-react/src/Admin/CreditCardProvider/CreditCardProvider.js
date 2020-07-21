import React from "react";
import { CreditCardProviderTable } from "./CreditCardProviderTable";
import { connect } from "react-redux";
import objectPath from "object-path";
import { Constants } from "../../Constants";
import { LinearProgress } from "@material-ui/core"

class CreditCardProvider extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      
    }
  }

  componentDidMount() {
    this.props.onLoadCreditCardProvider();
  }

  render() {
    let fetchAll;
    let loading;
    if (objectPath.has(this.props, "creditCardProvider.fetchAll")) {
      fetchAll = this.props.creditCardProvider.fetchAll;
      loading = fetchAll.isFetching ? <LinearProgress /> : null;
    }

    return (
      <React.Fragment>
        {loading}
        <CreditCardProviderTable props={this.props}/>
      </React.Fragment>
    )
  }
}

function mapStateToProps(state) {
  return {
    creditCardProvider: { ...state.creditCardProvider },
    pagination: objectPath.get(state, "creditCardProvider.pagination")
  }
}

function mapDispatchToProps(dispatch) {
  const restUrl = `${Constants.api.HOST}/creditCardProvider`;
  const loadCreditCardProvider = (qry) => {
    dispatch({ type: Constants.creditCardProvider.FETCH_ALL_REQUEST });
    try {
      let sort = qry && qry.sort ? `&OrderBy=${qry.sort}` : "";
      const pageSize = qry && qry.pageSize ? qry.pageSize : 10;
      const pageNumber = qry && qry.pageNumber ? qry.pageNumber : 1;
      fetch(`${restUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}${sort}`)
        .then(res => res.json())
        .then(res => {
          dispatch({ type: Constants.creditCardProvider.FETCH_ALL_SUCCESS, data: res });
          dispatch({
            type: Constants.creditCardProvider.SET_GRID_PAGINATION, data: {
              pageSize: res.metaData.pageSize,
              pageNumber: res.metaData.currentPage,
              totalCount: res.metaData.totalCount
            }
          })

        });
    } catch (err) {
      dispatch({ type: Constants.creditCardProvider.FETCH_ALL_FAILURE, data: err });
    }
  }
  return {
    onReload: (qry) => {
      loadCreditCardProvider(qry);
    },
    onLoadCreditCardProvider: (qry) => {
      loadCreditCardProvider(qry)
    },
    onDeleteCreditCardProvider: (id) => {
      dispatch({ type: Constants.creditCardProvider.DELETE_SINGLE_REQUEST });
      try {
        fetch(`${restUrl}/${id}`, { method: 'DELETE' })
          .then((res) => res.status)
          .then((res) => {
            dispatch({ type: Constants.creditCardProvider.DELETE_SINGLE_SUCCESS, data: res });
            loadCreditCardProvider({
              pageNumber: 1, 
              pageSize: 10
            });
          }).catch((err) => {
            dispatch({ type: Constants.creditCardProvider.DELETE_SINGLE_FAILURE, data: err });
          })
      } catch (err) {
        dispatch({ type: Constants.creditCardProvider.DELETE_SINGLE_FAILURE, data: err });
      }
    },
    setPagination: (pagination) => {
      dispatch({ type: Constants.creditCardProvider.SET_GRID_PAGINATION, data: pagination });
    }
  }
}


export default connect(mapStateToProps, mapDispatchToProps)(CreditCardProvider)