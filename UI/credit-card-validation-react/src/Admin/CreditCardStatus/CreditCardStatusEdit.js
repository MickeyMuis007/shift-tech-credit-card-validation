import React from "react";
import { SharedEditForm } from "../Shared/SharedEditForm";
import { ArrowBack } from "@material-ui/icons"
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import { Constants } from "../../Constants";
import objectPath from "object-path";

class CreditCardStatusEdit extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      schema: {
        "title": "Edit Credit Card Status",
        "type": "object",
        "required": [
          "status"
        ],
        "properties": {
          "status": {
            "type": "string",
            "title": "Status"
          },
          "description": {
            "type": "string",
            "title": "Description"
          }
        }
      },
      formData: {
        status: "Test",
        description: "Test Description"
      },
      isEdit: false
    }

    this.onBack = this.onBack.bind(this);
    this.onSubmit = this.onSubmit.bind(this);
  }

  componentDidMount() {
    if (objectPath.has(this.props, "match.params.id")) {
      this.props.fetchSingleCreditCardStatus(this.props.match.params.id, {});
      this.setState({
        isEdit: true,
        save: this.props.onUpdateCreditStatus
      })
    } else {
      this.setState({
        isEdit: false,
        save: this.props.onAddCreditCardStatus
      })
    }
  }

  onSubmit(results) {
    if (objectPath.has(results, "formData.status")) {
      this.state.save(results.formData, this.props.match.params.id);
      this.props.history.goBack();
    }
  }

  onBack() {
    if (this.props.history) this.props.history.goBack();
  }
  
  render() {
    const results = objectPath.get(this.props, "fetchSingle.results.results");
    const formData = results && this.state.isEdit ? 
      { id: results.Id, status: results.Status, description: results.Description || ""} : null;

    const title = this.state.isEdit ? "Edit Credit Card Status" : "Add Credit Card Status";
    const schema = {...this.state.schema, title: title};

    return (
      <React.Fragment>
        <span className="float-left mt-1" onClick={this.onBack} title="Go back"  style={{cursor: "pointer"}}><ArrowBack /></span>
        <SharedEditForm schema={schema} formData={formData} onSubmit={this.onSubmit} />
      </React.Fragment>
    )
  }
}

function mapStateToProps (state) {
  const fetchSingle = state && state.creditCardStatus && state.creditCardStatus.fetchSingle;
  return {
    fetchSingle
  };
}

function mapDispatchToProps (dispatch) {
  const restUrl = `${Constants.api.HOST}/creditCardStatus`;
  return {
    fetchSingleCreditCardStatus: (id, qry = {}) => {
      dispatch({ type: Constants.creditCardStatus.FETCH_SINGLE_REQUEST});
      const url = `${Constants.api.HOST}/creditCardStatus/${id}`;
      try {
        fetch(url)
          .then(res => res.json())
          .then(res => {
            dispatch({ type: Constants.creditCardStatus.FETCH_SINGLE_SUCCESS, data: res})
          })
          .catch(err => {
            dispatch({ type: Constants.creditCardStatus.FETCH_SINGLE_FAILURE, data: err});
          })
      } catch (err) {
        dispatch({ type: Constants.creditCardStatus.FETCH_SINGLE_FAILURE, data: err })
      }
    },
    onAddCreditCardStatus: (data) => {
      dispatch({ type: Constants.creditCardStatus.POST_SINGLE_REQUEST});
      try {
        fetch(`${restUrl}`, { method: "POST", headers: { "Content-Type": "application/json"}, body: JSON.stringify(data)})
          .then(res => res.json())
          .then((res) => {
            dispatch({ type: Constants.creditCardStatus.POST_SINGLE_SUCCESS, data: res});
          })
      } catch(err) {
        dispatch({ type: Constants.creditCardStatus.POST_SINGLE_FAILURE, data: err});
      }
    },
    onUpdateCreditStatus: (data, id) => {
      dispatch({ type: Constants.creditCardStatus.UPDATE_SINGLE_REQUEST});
      try {
        fetch(`${restUrl}/${id}`, { method: "PUT", headers: { "Content-Type": "application/json"}, body: JSON.stringify(data)})
          .then(res => res.status)
          .then(res => {
            dispatch({ type: Constants.creditCardStatus.UPDATE_SINGLE_SUCCESS, data: res});
          })
      } catch (err) {
        dispatch({ type: Constants.creditCardStatus.UPDATE_SINGLE_FAILURE, data: err});
      }
    }
  };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(CreditCardStatusEdit));