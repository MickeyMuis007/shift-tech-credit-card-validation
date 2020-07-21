import React from "react";
import { SharedEditForm } from "../Shared/SharedEditForm";
import { ArrowBack } from "@material-ui/icons"
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import { Constants } from "../../Constants";
import objectPath from "object-path";

class CreditCardEdit extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      schema: {
        "title": "Edit Credit Card",
        "type": "object",
        "required": [
          "no"
        ],
        "properties": {
          "no": {
            "type": "string",
            "title": "No"
          },
          "creditCardStatusId": {
            "type": "string",
            "title": "Credit Card Status"
          },
          "creditCardProviderId": {
            "type": "string",
            "title": "Credit Card Provider"
          }
        }
      },
      isEdit: false
    }

    this.onBack = this.onBack.bind(this);
  }

  componentDidMount() {
    if (objectPath.has(this.props, "match.params.id")) {
      this.props.fetchSingleCreditCard(this.props.match.params.id, {});
      this.setState({
        isEdit: true
      })
    } else {
      this.setState({
        isEdit: false
      })
    }
  }

  onSubmit(results) {
    console.log("CreditCardStatusOnSubmit", results);
  }

  onBack() {
    if (this.props.history) this.props.history.goBack();
  }

  render() {
    const results = objectPath.get(this.props, "fetchSingle.results.results");
    const formData = results && this.state.isEdit ?
      {
        id: results.Id, no: results.No,
        creditCardStatusId: results.CreditCardStatusId,
        creditCardProviderId: results.CreditCardProviderId
      } : null;

    const title = this.state.isEdit ? "Edit Credit Card Status" : "Add Credit Card Status";
    const schema = { ...this.state.schema, title: title };

    return (
      <React.Fragment>
        <span className="float-left mt-1" onClick={this.onBack} title="Go back" style={{ cursor: "pointer" }}><ArrowBack /></span>
        <SharedEditForm schema={schema} formData={formData} onSubmit={this.onSubmit} />
      </React.Fragment>
    )
  }
}

function mapStateToProps(state) {
  const fetchSingle = state && state.creditCard && state.creditCard.fetchSingle;
  return {
    fetchSingle
  };
}

function mapDispatchToProps(dispatch) {
  return {
    fetchSingleCreditCard: (id, qry = {}) => {
      dispatch({ type: Constants.creditCard.FETCH_SINGLE_REQUEST });
      const url = `${Constants.api.HOST}/creditCard/${id}`;
      try {
        fetch(url)
          .then(res => res.json())
          .then(res => {
            dispatch({ type: Constants.creditCard.FETCH_SINGLE_SUCCESS, data: res })
          })
          .catch(err => {
            dispatch({ type: Constants.creditCard.FETCH_SINGLE_FAILURE, data: err });
          })
      } catch (err) {
        dispatch({ type: Constants.creditCard.FETCH_SINGLE_FAILURE, data: err })
      }
    }
  };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(CreditCardEdit));