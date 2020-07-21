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
  }

  componentDidMount() {
    this.props.onLoadCreditCardProvider();
  }

  onProviderChange(event) {
    console.log(event.target.value);
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
    console.log(this.state);
  }

  render() {
    console.log(this.props);

    const providers = objectPath.get(this.props, "providers", []);
    const menuItems = providers.map(t => <MenuItem key={t.Id} value={t.Id}>{t.Code}</MenuItem>)

    return (
      <React.Fragment>
        <Form onSubmit={this.onSubmit}>
          <Container className="container-center-abs">
            <TextField className="mx-1" required variant="outlined" label="Enter Credit Card No?" name="no"
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
            <Fab variant="extended" type="submit" style={{outline: "none"}}>
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
  return {
    providers
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

  return {
    onLoadCreditCardProvider: () => {
      loadCreditCardProvider()
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Home);