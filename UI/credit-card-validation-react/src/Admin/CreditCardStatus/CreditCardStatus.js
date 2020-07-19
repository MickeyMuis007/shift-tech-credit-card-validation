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
    console.log("Render Props:", this.props);
    let creditCardStatusResults;
    let fetchAll;
    let loading;
    if (objectPath.has(this.props, "creditCardStatus.fetchAll")) {
      fetchAll = this.props.creditCardStatus.fetchAll;
      loading = fetchAll.isFetching ? <LinearProgress /> : null;
      creditCardStatusResults = this.props.creditCardStatus.fetchAll.results;
      
    } 


    return (
      <React.Fragment>
        {loading}
        <CreditCardStatusTable creditCardStatus={creditCardStatusResults} props={this.props}/>
      </React.Fragment>
    )
  }
}

function mapStateToProps(state) {
  return {
    creditCardStatus: { ...state.creditCardStatus }
  }
}

function mapDispatchToProps(dispatch, ownProps, stateProps) {
  console.log("Own Props:", ownProps);
  console.log("Own stateProps:", stateProps);
  console.log("Own dispatch:", dispatch);
  return {
    onReload: () => {
      dispatch({ type: Constants.creditCardStatus.RELOAD, data: "Some data" });
    },
    onLoadCreditCardState: (qry) => {
      dispatch({ type: Constants.creditCardStatus.FETCH_ALL_REQUEST });
      try {
        const pageSize = qry && qry.pageSize ? qry.pageSize : 10;
        const pageNumber = qry && qry.pageNumber ? qry.pageNumber : 1;
        fetch(`http://localhost:6003/api/creditCardStatus?pageNumber=${pageNumber}&pageSize=${pageSize}`)
          .then(res => res.json())
          .then(res => {
            dispatch({ type: Constants.creditCardStatus.FETCH_ALL_SUCCESS, data: res });

          });
      } catch (err) {
        dispatch({ type: Constants.creditCardStatus.FETCH_ALL_FAILURE, data: err });
      }
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(CreditCardStatus);