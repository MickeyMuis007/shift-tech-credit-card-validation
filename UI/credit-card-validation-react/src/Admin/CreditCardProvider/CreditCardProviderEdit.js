import React from "react";
import { SharedEditForm } from "../Shared/SharedEditForm";
import { ArrowBack } from "@material-ui/icons"
import { connect } from "react-redux";
import { withRouter } from "react-router-dom";
import { Constants } from "../../Constants";
import objectPath from "object-path";

class CreditCardProviderEdit extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      schema: {
        "title": "Edit Credit Card Provider",
        "type": "object",
        "required": [
          "name",
          "code"
        ],
        "properties": {
          "name": {
            "type": "string",
            "title": "Name"
          },
          "code": {
            "type": "string",
            "title": "Code"
          },
          "startsWith": {
            "type": "string",
            "title": "StartsWith"
          },
          "length": {
            "type": "string",
            "title": "Length"
          }
        }
      },
      isEdit: false
    }

    this.onBack = this.onBack.bind(this);
  }

  componentDidMount() {
    if (objectPath.has(this.props, "match.params.id")) {
      this.props.fetchSingleCreditCardProvider(this.props.match.params.id, {});
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
        id: results.Id, 
        name: results.Name, 
        code: results.Code,
        startsWith: results.StartsWith,
        length: results.Length
      } : null;

    const title = this.state.isEdit ? "Edit Credit Card Provider" : "Add Credit Card Provider";
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
  const fetchSingle = state && state.creditCardProvider && state.creditCardProvider.fetchSingle;
  return {
    fetchSingle
  };
}

function mapDispatchToProps (dispatch) {
  return {
    fetchSingleCreditCardProvider: (id, qry = {}) => {
      dispatch({ type: Constants.creditCardProvider.FETCH_SINGLE_REQUEST});
      const url = `${Constants.api.HOST}/creditCardProvider/${id}`;
      try {
        fetch(url)
          .then(res => res.json())
          .then(res => {
            dispatch({ type: Constants.creditCardProvider.FETCH_SINGLE_SUCCESS, data: res})
          })
          .catch(err => {
            dispatch({ type: Constants.creditCardProvider.FETCH_SINGLE_FAILURE, data: err});
          })
      } catch (err) {
        dispatch({ type: Constants.creditCardProvider.FETCH_SINGLE_FAILURE, data: err })
      }
    }
  };
}

export default withRouter(connect(mapStateToProps, mapDispatchToProps)(CreditCardProviderEdit));