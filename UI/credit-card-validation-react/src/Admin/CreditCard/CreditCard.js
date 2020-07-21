import React from "react";
import { connect } from "react-redux";
import { Constants } from "../../Constants";
import objectPath from "object-path";
import { CreditCardTable } from "./CreditCardTable";
import { LinearProgress } from "@material-ui/core"

class CreditCard extends React.Component {
  constructor(props) {
    super(props);
    this.state = {

    }
  }

  componentDidMount() {
    this.props.onLoadCreditCard();
    this.props.onLoadCreditCardStatus();
    this.props.onLoadCreditCardProvider();
  }

  render() {
    let fetchAll;
    let loading;
    if (objectPath.has(this.props, "creditCard.fetchAll")) {
      fetchAll = this.props.creditCard.fetchAll;
      loading = fetchAll.isFetching ? <LinearProgress /> : null;
    }


    return (
      <React.Fragment>
        {loading}
        <CreditCardTable props={{ ...this.props }} />
      </React.Fragment>
    )
  }
}

function mapStateToProps(state) {
  return {
    creditCard: { ...state.creditCard },
    pagination: objectPath.get(state, "creditCard.pagination"),
    creditCardStatus: { ...state.creditCardStatus },
    creditCardProvider: { ...state.creditCardProvider}
  }
}

function mapDispatchToProps(dispatch) {
  const restUrl = `${Constants.api.HOST}/creditCard`;
  const loadCreditCard = (qry) => {
    dispatch({ type: Constants.creditCard.FETCH_ALL_REQUEST });
    try {
      let sort = qry && qry.sort ? `&OrderBy=${qry.sort}` : "";
      const pageSize = qry && qry.pageSize ? qry.pageSize : 10;
      const pageNumber = qry && qry.pageNumber ? qry.pageNumber : 1;
      fetch(`${restUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}${sort}`)
        .then(res => res.json())
        .then(res => {
          dispatch({ type: Constants.creditCard.FETCH_ALL_SUCCESS, data: res });
          dispatch({
            type: Constants.creditCard.SET_GRID_PAGINATION, data: {
              pageSize: res.metaData.pageSize,
              pageNumber: res.metaData.currentPage,
              totalCount: res.metaData.totalCount
            }
          })

        });
    } catch (err) {
      dispatch({ type: Constants.creditCard.FETCH_ALL_FAILURE, data: err });
    }
  }

  const loadCreditCardStatus = () => {
    dispatch({ type: Constants.creditCardStatus.FETCH_ALL_REQUEST });
    try {
      fetch(`${Constants.api.HOST}/creditCardStatus`)
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

  const loadCreditCardProvider = () => {
    dispatch({ type: Constants.creditCardProvider.FETCH_ALL_REQUEST });
    try {
      fetch(`${Constants.api.HOST}/creditCardProvider`)
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
      loadCreditCard(qry)
    },
    onLoadCreditCard: (qry) => {
      loadCreditCard(qry)
    },
    onDeleteCreditCard: (id) => {
      dispatch({ type: Constants.creditCard.DELETE_SINGLE_REQUEST });
      try {
        fetch(`${restUrl}/${id}`, { method: 'DELETE' })
          .then((res) => res.status)
          .then((res) => {
            dispatch({ type: Constants.creditCard.DELETE_SINGLE_SUCCESS, data: res });
            loadCreditCard({
              pageNumber: 1,
              pageSize: 10
            });
          }).catch((err) => {
            dispatch({ type: Constants.creditCard.DELETE_SINGLE_FAILURE, data: err });
          })
      } catch (err) {
        dispatch({ type: Constants.creditCard.DELETE_SINGLE_FAILURE, data: err });
      }
    },
    setPagination: (pagination) => {
      dispatch({ type: Constants.creditCard.SET_GRID_PAGINATION, data: pagination });
    },
    onLoadCreditCardStatus: () => {
      loadCreditCardStatus()
    },
    onLoadCreditCardProvider: () => {
      loadCreditCardProvider()
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(CreditCard);