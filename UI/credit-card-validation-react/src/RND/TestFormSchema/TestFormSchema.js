import React from "react";
import { MDBBreadcrumb, MDBBreadcrumbItem } from 'mdbreact';
import Form from "@rjsf/core";



export class TestFormSchema extends React.Component {
  constructor() {
    super();
    this.state = {
      schema: {}
    }
  }

  componentDidMount() {
    this.setState({
      schema: {
        "title": "Identity",
        "type": "object",
        "required": [
          "firstName",
          "lastName"
        ],
        "properties": {
          "firstName": {
            "type": "string",
            "title": "First name",
            "minLength": 1,
            "maxLength": 6
          },
          "lastName": {
            "type": "string",
            "title": "Last name"
          },
          "age": {
            "type": "number",
            "title": "Age"
          }
        }
      }
    });
  }

  render() {
    return (
    <React.Fragment>
      <MDBBreadcrumb>
        <MDBBreadcrumbItem>Home</MDBBreadcrumbItem>
        <MDBBreadcrumbItem>Library</MDBBreadcrumbItem>
        <MDBBreadcrumbItem active>Data</MDBBreadcrumbItem>
      </MDBBreadcrumb>
      <Form schema={this.state.schema} noHtml5Validate onSubmit={(event) => {console.log(event.target.value);}} showErrorList={false} />
    </React.Fragment>
    )
  }
}