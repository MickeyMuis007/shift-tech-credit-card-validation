import React from "react";
import { connect } from "react-redux";
import { Container, Form } from "react-bootstrap";
import { Constants } from "../Constants";
import TextField from "@material-ui/core/TextField";
import SendIcon from '@material-ui/icons/Send';
import Fab from "@material-ui/core/Fab";
import InputLabel from "@material-ui/core/InputLabel";
import FormControl from "@material-ui/core/FormControl";
import MenuItem from "@material-ui/core/MenuItem";
import Select from "@material-ui/core/Select";
import objectPath from "object-path";
import Alert from "@material-ui/lab/Alert";
import Collapse from '@material-ui/core/Collapse';

class Home extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      provider: "",
      no: ""
    }

    this.onProviderChange = this.onProviderChange.bind(this);
    this.onCreditCardNoChange = this.onCreditCardNoChange.bind(this);
    this.onSubmit = this.onSubmit.bind(this);
    this.onAlertErrorClose = this.onAlertErrorClose.bind(this);
    this.onAlertSuccessClose = this.onAlertSuccessClose.bind(this);
  }

  componentDidMount() {
    this.props.onLoadCreditCardProvider();
  }

  onProviderChange(event) {
    this.setState({
      [event.target.name]: event.target.value
    });
  }

  onCreditCardNoChange(event) {
    this.setState({
      [event.target.name]: event.target.value
    });
  }

  onSubmit(event) {
    event.preventDefault();
    this.props.onAlertCloseError();
    this.props.onAlertCloseSuccess();
    this.props.onValidateCreditCardNo({
      no: this.state.no,
      creditCardProviderId: this.state.provider
    });

  }

  onAlertSuccessClose() {
    this.props.onAlertCloseSuccess();
  }

  onAlertErrorClose() {
    this.props.onAlertCloseError();
  }

  render() {
    const providers = objectPath.get(this.props, "providers", []);
    const menuItems = providers.map(t => <MenuItem key={t.Id} value={t.Id}>{t.Code}</MenuItem>)

    return (
      <React.Fragment>
        <Collapse in={this.props.showAlertSuccess}>
          <Alert onClose={this.onAlertSuccessClose}>Credit card is valid!!!</Alert>
        </Collapse>
        <Collapse in={this.props.showAlertError}>
          <Alert severity="error" onClose={this.onAlertErrorClose}>Credit Card is invalid!!!</Alert>
        </Collapse>
        <Form onSubmit={this.onSubmit}>
          <Container className="container-center-abs">
            <TextField type="number" className="mx-1" required variant="outlined" label="Enter Credit Card No?" name="no"
              onChange={this.onCreditCardNoChange} value={this.state.no} />
            <FormControl className="mx-1" variant="outlined">
              <InputLabel htmlFor="provider">Provider</InputLabel>
              <Select label="Test" value={this.state.provider} required
                onChange={this.onProviderChange} style={{ width: "150px" }}
                inputProps={{
                  name: 'provider',
                  id: 'provider',
                }}>
                {menuItems}
              </Select>
            </FormControl>
            <Fab variant="extended" type="submit" style={{ outline: "none" }}>
              Validate <SendIcon className="ml-2" />
            </Fab>
          </Container>
        </Form>
      </React.Fragment>
    )
  }
}

function mapStateToProps(state) {
  const providers = objectPath.get(state, "creditCardProvider.fetchAll.results.results");
  const showAlertSuccess = objectPath.get(state, "creditCard.alert.showSuccess");
  const showAlertError = objectPath.get(state, "creditCard.alert.showError");
  return {
    providers,
    showAlertError,
    showAlertSuccess
  };
}

function mapDispatchToProps(dispatch) {
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

  const validateCreditCardNo = (data) => {
    dispatch({ type: Constants.creditCard.VALIDATE_NO_REQUEST });
    try {
      fetch(`${Constants.api.HOST}/creditCard/validateCreditCardNo`, { method: "POST", headers: { "Content-Type": "application/json" }, body: JSON.stringify(data) })
        .then(res => res && res.json && res.json())
        .then(res => {
          dispatch({ type: Constants.creditCard.VALIDATE_NO_SUCCESS, data: res });

          if (objectPath.get(res, "valid")) {
            dispatch({ type: Constants.creditCard.ALERT_SHOW_SUCCESS });
          } else {
            dispatch({ type: Constants.creditCard.ALERT_SHOW_ERROR });
          }
        }).catch(err => {
          dispatch({ type: Constants.creditCard.VALIDATE_NO_FAILURE, data: err });
          dispatch({ type: Constants.creditCard.ALERT_SHOW_ERROR });
        })
    } catch (err) {
      dispatch({ type: Constants.creditCard.VALIDATE_NO_FAILURE, data: err });
      dispatch({ type: Constants.creditCard.ALERT_SHOW_ERROR });
    }
  }

  return {
    onLoadCreditCardProvider: () => {
      loadCreditCardProvider()
    },
    onValidateCreditCardNo: (data) => {
      validateCreditCardNo(data);
    },
    onAlertCloseSuccess: () => {
      dispatch({ type: Constants.creditCard.ALERT_CLOSE_SUCCESS })
    },
    onAlertCloseError: () => {
      dispatch({ type: Constants.creditCard.ALERT_CLOSE_ERROR });
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Home);